var angular = require('angular');
angular.module('ui.review')
	.component('help', {
		template: require('./help.template.html')
	})
	.config(function ($routeProvider) {
		$routeProvider.when('/help', {
			template: '<help></help>'
		})
	});