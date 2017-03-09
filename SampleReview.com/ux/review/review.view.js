﻿var angular = require('angular');
angular.module('ui.review')
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
	ctrl.defaultOrderBy = ['-rating', '-date'];
	ctrl.$onInit = onInit;
	ctrl.edit = edit;
	ctrl.discardEdit = discardEdit;
	ctrl.saveEdit = saveEdit;
	ctrl.refresh = refresh;
	ctrl.constraints = constraints;

	ctrl.orderReview = {
		options: {
			rating: { label: 'Rating', type: 'numeric', defaultAsc: '-', prefix: { asc: 'Lowest', desc: 'Highest' } },
			date: { label: 'Review', type: 'amount', defaultAsc: '-', prefix: {asc:'Oldest',desc:'Newest'} }
		},
		by: ctrl.defaultOrderBy,
		page: constraints.defaultPage,
		perPage: constraints.defaultPerPage,
		totalItems: 0
	};
	function edit(val) {
		ctrl.editing = {
			rating: val,
			comment: '',
			reviewing: ctrl.item.id
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
			ctrl.failed = false;
			return reviewResource.save(ctrl.editing).$promise.then(function () {
				ctrl.busy = ctrl.busyMessage = false;
				ctrl.item.AverageRating = ctrl.defaultRating;
				ctrl.refresh(true);
				ctrl.editing = null;
			}, function () { ctrl.busy = !(ctrl.failed = true); });
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
		ctrl.failed = false;
		var promises = {};
		if (itemToo) promises.updatedItem = itemResource.get({ id: ctrl.item.id }).$promise;
		promises.reviewsPage = reviewResource.get({
			item: ctrl.item.id,
			page: ctrl.orderReview.page,
			perPage: ctrl.orderReview.perPage,
			orderBy: ctrl.orderReview.by.join(',')
		}).$promise;
		return $q.all(promises)
			.then(function (result) {
				ctrl.reviews = result.reviewsPage.collection;
				ctrl.orderReview.totalItems = result.reviewsPage.totalItems
				if (result.updatedItem)	ctrl.item = result.updatedItem;
				ctrl.busy = false;
			}, function () { ctrl.busy = !(ctrl.failed = true); });

	}
}