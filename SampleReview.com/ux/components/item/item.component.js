var angular = require('angular');
angular.module('ui.review')
	.component('item', {
		template: require('./item.template.html'),
        controller: itemController,
		bindings: {
			itemModel:'=',
            readOnly:'@',
            change:'&',
            reviewModel: '=',
            titleLinksToItem:'@'
		}
	});

itemController.$inject = ['constraints'];
function itemController(constraints) {
    var ctrl = this;
    ctrl.constraints = constraints;
    ctrl.onChange = function (val) {
        ctrl.change({ rating: val })
    };
}