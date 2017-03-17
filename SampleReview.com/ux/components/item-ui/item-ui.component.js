var angular = require('angular');
angular.module('ui.review')
	.component('itemUi', {
		template: require('./item-ui.template.html'),
		controller: itemUiController,
		bindings: {
			itemModel:'=',
            readOnly:'@',
            change:'&',
            reviewModel:'='
		}
	});

itemUiController.$inject = ['constraints'];
function itemUiController(constraints) {
    var ctrl = this;
    ctrl.constraints = constraints;
}