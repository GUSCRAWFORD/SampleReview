var angular = require('angular');
require('angular-mocks');
require('angular-bootstrap-npm');
require('angular-route');
var reviewApp = require('../app/app.review.module');
var reviewUi = require('../ux/ui.review.module');
reviewApp.constant('restEndpoint', "http://rest.api");