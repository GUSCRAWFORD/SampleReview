var angular = require('angular');
angular.module('ui.review')
	.component('itemUiEditComment', {
		template: require('./item-ui-edit-comment.template.html'),
		controller: itemUiEditCommentController,
		bindings: {
            alteredModel:'=',
			form:'<',
			onDiscard:'&',
			onSave:'&'
		}
	});

itemUiEditCommentController.$inject = ['constraints','reviewResource'];
function itemUiEditCommentController(constraints, reviewResource) {
    var ctrl = this;
	ctrl.saveEdit = saveEdit;
	ctrl.$onInit= function () {
		ctrl;
	}

	function saveEdit() {
		if (ctrl.form.$valid) {
			ctrl.invalidBlock = false;
			return reviewResource.save(ctrl.alteredModel).$promise;
		}
		else {
			ctrl.invalidBlock = true;
		}
	}	
}