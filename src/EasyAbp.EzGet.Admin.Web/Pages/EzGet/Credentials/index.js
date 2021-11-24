(function ($) {
    var l = abp.localization.getResource('EzGet');
    var _credentialAdminAppService = easyAbp.ezGet.admin.credentials.credentialAdmin;

    var _dataTable = null;

    abp.ui.extensions.entityActions.get('ezGet.credential').addContributor(
        function (actionList) {
            return actionList.addManyTail(
                [
                    {
                        text: l('Edit'),
                        visible: abp.auth.isGranted(
                            'EzGet.Credentials.Update'
                        ),
                        action: function (data) {
                            _editModal.open({
                                id: data.record.id,
                            });
                        },
                    },
                    {
                        text: l('Delete'),
                        visible: abp.auth.isGranted(
                            'EzGet.Credentials.Delete'
                        ),
                        confirmMessage: function (data) {
                            return l(
                                'CredentialDeletionConfirmationMessage',
                                data.record.id
                            );
                        },
                        action: function (data) {
                            _credentialAdminAppService
                                .delete(data.record.id)
                                .then(function () {
                                    _dataTable.ajax.reload();
                                    abp.notify.success(l('SuccessfullyDeleted'));
                                });
                        },
                    }
                ]
            );
        }
    );
})(jQuery);
