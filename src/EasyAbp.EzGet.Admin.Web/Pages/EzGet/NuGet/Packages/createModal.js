var abp = abp || {};
$(function () {
    abp.modals.createNuGetPackage = function () {
        var initModal = function (publicApi, args) {

            var $form = publicApi.getForm();

            var _selectUserModal = new abp.ModalManager({
                viewUrl: abp.appPath + 'EzGet/Users/SelectUserModal',
                scriptUrl: '/Pages/EzGet/Users/selectUserModal.js',
                modalClass: 'selectUserModal'
            });

            var _selectFeedModal = new abp.ModalManager({
                viewUrl: abp.appPath + 'EzGet/Feeds/SelectFeedModal',
                scriptUrl: '/Pages/EzGet/Feeds/selectFeedModal.js',
                modalClass: 'selectFeedModal'
            });


        }
    }
});
