var angular = require('angular');
angular.module('app.smithReview').constant('smithConstraints', {
	defaultPage: 1,
	defaultPerPage: 10,
	defaultOrderBy: "Id",
	maxRating: 5,
	minRating: 1,
	minCommentLength: 4
});
