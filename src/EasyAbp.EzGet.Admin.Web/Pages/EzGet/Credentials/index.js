(function ($) {
    var l = abp.localization.getResource('EzGet');
    var _credentialAdminAppService = easyAbp.ezGet.admin.credentials.credentialAdmin;

    var _editModal = new abp.ModalManager(
        abp.appPath + 'EzGet/Credentials/EditModal'
    );

    var _createModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'EzGet/Credentials/CreateModal',
        modalClass: 'createCredential'
    });

    var _selectUserModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'EzGet/Users/SelectUserModal',
        scriptUrl: '/Pages/EzGet/Users/selectUserModal.js',
        modalClass: 'selectUserModal'
    });

    var _dataTable = null;

    abp.ui.extensions.entityActions.get('ezGet.credential').addContributor(
        function (actionList) {
            return actionList.addManyTail(
                [
                    {
                        text: l('Edit'),
                        visible: abp.auth.isGranted('EzGet.Admin.Credentials.Update'),
                        action: function (data) {
                            _editModal.open({
                                id: data.record.id,
                            });
                        },
                    },
                    {
                        text: l('Delete'),
                        visible: abp.auth.isGranted('EzGet.Admin.Credentials.Delete'),
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

    abp.ui.extensions.tableColumns.get('ezGet.credential').addContributor(
        function (columnList) {
            columnList.addManyTail(
                [
                    {
                        title: l("Actions"),
                        rowAction: {
                            items: abp.ui.extensions.entityActions.get('ezGet.credential').actions.toArray()
                        }
                    },
                    {
                        title: l('UserId'),
                        data: 'userId',
                    },
                    {
                        title: l('Description'),
                        data: 'description',
                    },
                    {
                        title: l('Expires'),
                        data: 'expires',
                    }
                ]
            );
        },
        0 //adds as the first contributor
    );

    $(function () {
        var getFilter = function () {
            return {
                userId: $('#EzGetCredentialsWrapper input.sreach-user-id').val()
            };
        };

        var _$table = $('#CredentialsTable');
        _dataTable = _$table.DataTable(
            abp.libs.datatables.normalizeConfiguration({
                order: [[1, 'asc']],
                processing: true,
                serverSide: true,
                scrollX: true,
                paging: true,
                searching: false,
                ajax: abp.libs.datatables.createAjax(_credentialAdminAppService.getList, getFilter),
                columnDefs: abp.ui.extensions.tableColumns.get('ezGet.credential').columns.toArray()
            })
        );

        $('#EzGetCredentialsWrapper button.select-user').click(function () {
            _selectUserModal.open();
        });

        $('#EzGetCredentialsWrapper form.credentials-search-form').submit(function (e) {
            e.preventDefault();
            _dataTable.ajax.reload();
        });

        $('#AbpContentToolbar button[name=CreateCredential]').click(function (e) {
            e.preventDefault();
            _createModal.open();
        });
    });

    _selectUserModal.onResult(function (arg) {
        $('#EzGetCredentialsWrapper input.sreach-user-id').val(arg.id);
    });
})(jQuery);
