var angular = require('angular');
angular.module('ui.smithReview')
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
	ctrl.stripAsc = stripAsc;
	ctrl.asc = asc;
	ctrl.orderBy = orderBy;
	ctrl.unorder = unorder;
	ctrl.reverse = reverse;
	function onInit() {

	}
	function orderBy(col) {
		ctrl.unorder(col);
		ctrl.order.by.unshift(ctrl.order.options[ctrl.stripAsc(col)].defaultAsc + col);
		ctrl.update();
	}
	function reverse(col) {
		var i = get(ctrl.stripAsc(col));
		ctrl.order.by[i] = (asc(col) ? '-' : '+') + ctrl.stripAsc(col);
		ctrl.update();
	}
	function unorder(col) {
		var i = get(col);
		ctrl.order.by.splice(i, 1);
	}
	function get(col) {
		for (var i = 0; i < ctrl.order.by.length; i++) {
			if (stripAsc(ctrl.order.by[i]) === col) {
				return i;
			}
		}
	}
	function asc(col) {
		if (col[0] === '-') return false;
		return true;
	}
	function stripAsc(col) {
		if (col[0] === '-' || col[0] === '+') return col.substring(1);
		return col;
	}
}