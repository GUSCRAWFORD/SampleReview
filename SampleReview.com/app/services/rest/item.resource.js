var angular = require('angular');
angular.module('app.smithReview').service('itemResource', itemResource);
itemResource.$inject = ['$resource', 'restEndpoint', 'smithConstraints'];
function itemResource($resource, restEndpoint, smithConstraints) {
	return $resource(restEndpoint + 'items', {
	});
}