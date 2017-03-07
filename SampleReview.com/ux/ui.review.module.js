var angular = require('angular');
require('angular-bootstrap-npm');
require('angular-route');
var smithReviewApp = require('../app/app.review.module');
angular.module('ui.smithReview', ['app.smithReview', 'ui.bootstrap', 'ngRoute']);

angular.element(function () {
	angular.bootstrap(document, ['ui.review']);
});
require('./catalog');
require('./review');
require('./order-ui');