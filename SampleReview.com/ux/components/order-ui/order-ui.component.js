var angular = require('angular');
angular.module('ui.review')
	.component('orderUi', {
		template: require('./order-ui.template.html'),
		controller: orderUiController,
		bindings: {
			order: '=',
			update:'&',
			id: '@',
			disabled:'<'
		}
	});

orderUiController.$inject = ['constraints'];
function orderUiController(constraints) {
	var ctrl = this;
	ctrl.$onInit = onInit;
    ctrl.constraints = constraints;
    ctrl.stripOrder = stripOrder;
    ctrl.asc = asc;
    ctrl.desc = desc;
	ctrl.orderBy = orderBy;
	ctrl.unorder = unorder;
    ctrl.reverse = reverse;
    ctrl.get = get;
    ctrl.colOptions = colOptions;
	function onInit() {

	}
	function orderBy(col) {
		ctrl.unorder(col);
        ctrl.order.by.unshift(ctrl.order.options[ctrl.stripOrder(col)].defaultAsc + col);
		ctrl.update();
	}
    function reverse(col) {
		var i = ctrl.get(col);
        ctrl.order.by[i] = (ctrl.asc(col) ? '-' : '+') + ctrl.stripOrder(col);
		ctrl.update();
	}
	function unorder(col) {
		var i = ctrl.get(col);
		ctrl.order.by.splice(i, 1);
	}
	function get(col) {
		for (var i = 0; i < ctrl.order.by.length; i++) {
            if (ctrl.stripOrder(ctrl.order.by[i]) === ctrl.stripOrder(col)) {
				return i;
			}
		}
	}
	function asc(col) {
		if (col[0] === '-') return false;
		return true;
    }
    function desc(col) {
        if (col[0] == '+') return false;
        return true;
    }
	function stripOrder(col) {
		if (!asc(col) || !desc(col)) return col.substring(1);
		return col;
    }
    function colOptions(col) {
        return ctrl.order.options[ctrl.stripOrder(col)];
    }
}