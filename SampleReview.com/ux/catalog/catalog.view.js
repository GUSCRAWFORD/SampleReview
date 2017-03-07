var angular = require('angular');
angular.module('ui.smithReview')
	.component('catalog', {
		template: require('./catalog.template.html'),
		controller: catalogController
	})
	.config(function ($routeProvider) {
		$routeProvider.when('/', {
			template: '<catalog></catalog>'
		})
	});

catalogController.$inject = ['itemResource', 'constraints'];
function catalogController(itemResource, constraints) {
	var ctrl = this;
	ctrl.busy = true;
	ctrl.$readOnly = true;
	ctrl.$onInit = onInit;
	ctrl.constraints = constraints;
	ctrl.refresh = refresh;
	ctrl.defaultOrderBy = ['-Popularity', '-AverageRating', '-Date', '+Name'];

	ctrl.orderItems = {
		options: {
			Popularity: { label:'Popular', prefix:{asc:'Least',desc:'Most'}, type: 'amount', defaultAsc:'-' },
			AverageRating: { label: 'Average Rating', prefix: { asc: 'Lowest', desc: 'Highest' }, type: 'amount', defaultAsc: '-' },
			Date: { label: 'Reviewed', prefix: { asc: 'Oldest', desc: 'Most Recent' }, type: 'amount', defaultAsc: '-' },
			Name: { label: 'Name', prefix: { asc: '(Ascending)', desc: '(Descending)' }, type: 'alpha', defaultAsc: '+' }
		},
		by: ctrl.defaultOrderBy,
		page: constraints.defaultPage,
		perPage: constraints.defaultPerPage,
		totalItems:0
	};
	function onInit() {
		ctrl.refresh();
	}

	function refresh() {
		itemResource.get({
			page: ctrl.orderItems.page,
			perPage: ctrl.orderItems.perPage,
			orderBy: ctrl.orderItems.by
		}).$promise.then(function (itemsPage) {
			ctrl.items = itemsPage.Collection;
			ctrl.busy = false;
			ctrl.orderItems.totalItems = itemsPage.OfTotal;
		});
	}
}