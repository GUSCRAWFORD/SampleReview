var angular = require('angular');
angular.module('ui.review')
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
	ctrl.defaultOrderBy = ['-popularity', '-averageRating', '-date', '+name'];

	ctrl.orderItems = {
		options: {
			popularity: { label:'Popular', prefix:{asc:'Least',desc:'Most'}, type: 'amount', defaultAsc:'-' },
			averageRating: { label: 'Average Rating', prefix: { asc: 'Lowest', desc: 'Highest' }, type: 'amount', defaultAsc: '-' },
			date: { label: 'Reviewed', prefix: { asc: 'Oldest', desc: 'Most Recent' }, type: 'amount', defaultAsc: '-' },
			name: { label: 'Name', prefix: { asc: '(Ascending)', desc: '(Descending)' }, type: 'alpha', defaultAsc: '+' }
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
		ctrl.failed = false;
		itemResource.get({
			page: ctrl.orderItems.page,
			perPage: ctrl.orderItems.perPage,
			orderBy: ctrl.orderItems.by.join(',')
		}).$promise.then(function (itemsPage) {
			ctrl.items = itemsPage.collection;
			ctrl.busy = false;
			ctrl.orderItems.totalItems = itemsPage.totalItems;
		}, function () { ctrl.busy = !(ctrl.failed = true); });
	}
}