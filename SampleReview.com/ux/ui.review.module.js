var angular = require('angular');
require('angular-bootstrap-npm');
require('angular-route');
var smithReviewApp = require('../app/app.review.module');
angular.module('ui.review', ['app.review', 'ui.bootstrap', 'ngRoute']);

angular.element(function () {
	angular.bootstrap(document, ['ui.review']);
});

require('./catalog');
require('./review');
require('./gus-plus-tfs');
require('./help');

require('./components');