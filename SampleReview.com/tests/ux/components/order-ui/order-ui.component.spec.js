describe('orderUi component', function () {
    var constraints, $componentController, ctrl, updateSpy, bindings;
    beforeEach(function () { module('ui.review') });
    beforeEach(inject(function (_$componentController_, _constraints_) {
        constraints = _constraints_;
        $componentController = _$componentController_;
        var defaultOrderBy = ['-x', '-y'];
        bindings = {
            order: {
                options: {
                    x: { label: 'X', type: 'numeric', defaultAsc: '-', prefix: { asc: 'Lowest', desc: 'Highest' } },
                    y: { label: 'Y', type: 'amount', defaultAsc: '-', prefix: { asc: 'Oldest', desc: 'Newest' } }
                },
                by: defaultOrderBy,
                page: constraints.defaultPage,
                perPage: constraints.defaultPerPage,
                totalItems: 0
            },
            update: updateSpy = jasmine.createSpy('update'),
            id: '@',
            disabled: '<'
        };
        ctrl = $componentController('orderUi', null, bindings);
    }));
    describe('`.orderBy`', function () {
        it('calls `.unorder`', function () {
            var col = 'x';
            spyOn(ctrl.order.by, 'unshift');
            ctrl.orderBy(col);
            expect(ctrl.order.by.unshift).toHaveBeenCalledWith('-' + col);
            expect(updateSpy).toHaveBeenCalled();
        });
        it('calls api a particular way', function () {

        });
    });
    describe('.reverse', function () {
        it('calls a particular way', function () { });
    });
    describe('.unorder', function () {
        it('calls a particular way', function () { });
    });
    describe('.get', function () {
        it('calls a particular way', function () { });
    });
    describe('.asc', function () {
        it('calls a particular way', function () { });
    });
    describe('.stripAsc', function () {
        it('calls a particular way', function () { });
    });
});
/*var angular = require('angular');
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
}*/