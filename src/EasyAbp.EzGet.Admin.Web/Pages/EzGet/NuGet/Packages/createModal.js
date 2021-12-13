var abp = abp || {};
$(function () {
    abp.modals.createNuGetPackage = function () {
        var initModal = function (publicApi, args) {
            var $form = publicApi.getForm();

            var _selectFeedModal = new abp.ModalManager({
                viewUrl: abp.appPath + 'EzGet/Feeds/SelectFeedModal',
                scriptUrl: '/Pages/EzGet/Feeds/selectFeedModal.js',
                modalClass: 'selectFeedModal'
            });

            $("#file").fileinput({
                showUpload: false,
                minFileSize: -1,
                maxFileCount: 1,
                allowedFileExtensions: ['nupkg'],
                previewFileType: "any",
                theme: "fa",
                uploadAsync: false
            });

            $form.find('button.select-feed').click(function () {
                _selectFeedModal.open();
            });

            _selectFeedModal.onResult(function (arg) {
                $form.find('input.sreach-feed-id').val(arg.id);
            });
        };

        return {
            initModal: initModal
        };
    }
});
