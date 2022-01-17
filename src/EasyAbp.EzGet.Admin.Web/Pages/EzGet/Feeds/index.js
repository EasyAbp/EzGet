(function ($) {
    var l = abp.localization.getResource('EzGet');
    var _feedAdminAppService = easyAbp.ezGet.admin.feeds.feedAdmin;

    var _editModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'EzGet/Feeds/EditModal',
        modalClass: 'editFeed'
    });

    var _createModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'EzGet/Feeds/CreateModal',
        modalClass: 'createFeed'
    });

    var _selectUserModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'EzGet/Users/SelectUserModal',
        scriptUrl: '/Pages/EzGet/Users/selectUserModal.js',
        modalClass: 'selectUserModal'
    });

    var _dataTable = null;

    $(function () {
        var getFilter = function () {
            return {
                userId: $('#EzGetFeedsWrapper input.sreach-user-id').val(),
                feedName: $('#EzGetFeedsWrapper input.feed-name').val()
            };
        };

        var _$table = $('#FeedsTable');
        _dataTable = _$table.DataTable(
            abp.libs.datatables.normalizeConfiguration({
                order: [[1, 'asc']],
                processing: true,
                serverSide: true,
                scrollX: true,
                paging: true,
                searching: false,
                ajax: abp.libs.datatables.createAjax(_feedAdminAppService.getList, getFilter),
                columnDefs: abp.ui.extensions.tableColumns.get('ezGet.feed').columns.toArray()
            })
        );

        $('#EzGetFeedsWrapper button.select-user').click(function () {
            _selectUserModal.open();
        });

        $('#EzGetFeedsWrapper form.feeds-search-form').submit(function (e) {
            e.preventDefault();
            _dataTable.ajax.reload();
        });

        $('#AbpContentToolbar button[name=CreateFeed]').click(function (e) {
            _createModal.open();
        });
    });

    _selectUserModal.onResult(function (arg) {
        $('#EzGetFeedsWrapper input.sreach-user-id').val(arg.id);
    });

    _createModal.onResult(function (arg) {
        _dataTable.ajax.reload();
    });

    _editModal.onResult(function (arg) {
        _dataTable.ajax.reload();
    });

    abp.ui.extensions.entityActions.get('ezGet.feed').addContributor(
        function (actionList) {
            return actionList.addManyTail(
                [
                    {
                        text: l('Edit'),
                        visible: abp.auth.isGranted('EzGet.Admin.Feeds.Update'),
                        action: function (data) {
                            _editModal.open({
                                id: data.record.id,
                            });
                        },
                    },
                    {
                        text: l('Delete'),
                        visible: abp.auth.isGranted('EzGet.Admin.Feeds.Delete'),
                        confirmMessage: function (data) {
                            return l(
                                'FeedDeletionConfirmationMessage',
                                data.record.feedName
                            );
                        },
                        action: function (data) {
                            _feedAdminAppService
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

    abp.ui.extensions.tableColumns.get('ezGet.feed').addContributor(
        function (columnList) {
            columnList.addManyTail(
                [
                    {
                        title: l("Actions"),
                        rowAction: {
                            items: abp.ui.extensions.entityActions.get('ezGet.feed').actions.toArray()
                        }
                    },
                    {
                        title: l('FeedName'),
                        data: 'feedName',
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
                        title: l('FeedType'),
                        data: 'feedType',
                        render: function (data) {
                            switch (data) {
                                case 0:
                                    return l("Public");
                                case 1:
                                    return l("Private");
                                default:
                                    return l("Unknow");
                            }
                        }
                    }
                ]
            );
        },
        0 //adds as the first contributor
    );
})(jQuery);
