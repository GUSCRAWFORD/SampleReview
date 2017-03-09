var angular = require('angular');
angular.module('app.review').service('reviewResource', reviewResource);
reviewResource.$inject = ['$resource', 'restEndpoint', 'constraints'];
function reviewResource($resource, restEndpoint, constraints) {
	return $resource(restEndpoint + 'reviews', {
		item:'@item'
	});
}