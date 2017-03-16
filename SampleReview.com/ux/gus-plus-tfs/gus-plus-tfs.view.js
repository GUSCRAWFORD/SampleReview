var angular = require('angular');
angular.module('ui.review')
	.component('gusPlusTfs', {
		template: require('./gus-plus-tfs.template.html'),
		bindings: {
			item: '<'
		}
	})
	.config(function ($routeProvider) {
		$routeProvider.when('/gus-plus-tfs', {
			template: '<gus-plus-tfs></gus-plus-tfs>'
		})
	});