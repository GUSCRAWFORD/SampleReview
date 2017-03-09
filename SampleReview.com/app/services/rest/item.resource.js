var angular = require('angular');
angular.module('app.review').service('itemResource', itemResource);
itemResource.$inject = ['$resource', 'restEndpoint', 'constraints'];
function itemResource($resource, restEndpoint, constraints) {
	return $resource(restEndpoint + 'items', {
	});
}