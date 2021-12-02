var abp = abp || {};
$(function () {
    abp.modals.createFeed = function () {
        var initModal = function (publicApi, args) {
            var $form = publicApi.getForm();

            var _selectUserModal = new abp.ModalManager({
                viewUrl: abp.appPath + 'EzGet/Users/SelectUserModal',
                scriptUrl: '/Pages/EzGet/Users/selectUserModal.js',
                modalClass: 'selectUserModal'
            });

            $form.find('button.select-user').click(function () {
                _selectUserModal.open();
            });

            _selectUserModal.onResult(function (arg) {
                $form.find('input.sreach-user-id').val(arg.id);
            });
        };

        return {
            initModal: initModal
        };
    }
});
