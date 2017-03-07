var angular = require('angular');
require('angular-mocks');
require('angular-bootstrap-npm');
require('angular-route');
var smithReviewApp = require('../app/app.smithReview.module');
var smithReviewUx = require('../ux/ui.smithReview.module');

angular.element(function () {
	angular.bootstrap(document, ['ui.smithReview']);
});