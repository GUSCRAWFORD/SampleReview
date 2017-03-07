var angular = require('angular');
angular.module('ui.smithReview')
	.component('review', {
		template: require('./review.template.html'),
		controller: reviewController,
		bindings: {
			item: '<',
			form:'<'
		}
	})
	.config(function ($routeProvider) {
		$routeProvider.when('/review/:itemId', {
			template: '<form name="itemReviewForm"><review item="$resolve.item" form="itemReviewForm"></review></form>',
			resolve: {
				item:function (itemResource, $route) {
					return itemResource.get({ id: $route.current.params.itemId }).$promise;
				}
			}
		})
	});

reviewController.$inject = ['$scope','reviewResource','itemResource', 'constraints','$q'];
function reviewController($scope, reviewResource, itemResource, constraints, $q) {
	var ctrl = this;
	ctrl.busy = true;
	ctrl.busyMessage;
	ctrl.defaultOrderBy = ['-Rating', '-Date'];
	ctrl.$onInit = onInit;
	ctrl.edit = edit;
	ctrl.discardEdit = discardEdit;
	ctrl.saveEdit = saveEdit;
	ctrl.refresh = refresh;
	ctrl.constraints = constraints;

	ctrl.orderReview = {
		options: {
			Rating: { label: 'Rating', type: 'numeric', defaultAsc: '-', prefix: { asc: 'Lowest', desc: 'Highest' } },
			Date: { label: 'Review', type: 'amount', defaultAsc: '-', prefix: {asc:'Oldest',desc:'Newest'} }
		},
		by: ctrl.defaultOrderBy,
		page: smithContstraints.defaultPage,
		perPage: smithContstraints.defaultPerPage,
		totalItems: 0
	};
	function edit(val) {
		ctrl.editing = {
			Rating: val,
			Comment: '',
			Reviewing: { Id: ctrl.item.Id }
		};
	}
	function discardEdit() {
		ctrl.editing = null;
		ctrl.item.AverageRating = ctrl.defaultRating;
	}
	function saveEdit() {
		if (ctrl.form.$valid) {
			ctrl.invalidBlock = false;
			ctrl.busy = true;
			ctrl.busyMessage = "Saving your review...";
			return reviewResource.save(ctrl.editing).$promise.then(function () {
				ctrl.busy = ctrl.busyMessage = false;
				ctrl.item.AverageRating = ctrl.defaultRating;
				ctrl.refresh(true);
				ctrl.editing = null;
			});
		}
		else {
			ctrl.invalidBlock = true;
		}
	}
	function onInit() {
		ctrl.defaultRating = ctrl.item.AverageRating;
		ctrl.refresh();
	}

	function refresh(itemToo) {
		var promises = {};
		if (itemToo) promises.updatedItem = itemResource.get({ id: ctrl.item.Id }).$promise;
		promises.reviewsPage = reviewResource.get({
			item: ctrl.item.Id,
			page: ctrl.orderReview.page,
			perPage: ctrl.orderReview.perPage,
			orderBy: ctrl.orderReview.by
		}).$promise;
		return $q.all(promises)
			.then(function (result) {
				ctrl.reviews = result.reviewsPage.Collection;
				ctrl.orderReview.totalItems = result.reviewsPage.OfTotal
				if (result.updatedItem)	ctrl.item = result.updatedItem;
				ctrl.busy = false;
			});

	}
}