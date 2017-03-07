var angular = require('angular');
angular.module('app.smithReview').service('reviewResource', reviewResource);
reviewResource.$inject = ['$resource', 'restEndpoint', 'smithConstraints'];
function reviewResource($resource, restEndpoint, smithConstraints) {
	return $resource(restEndpoint + 'reviews', {
		item:'@item'
	});
}