var abp = abp || {};
(function ($) {
    abp.modals.selectCredentialModal = function () {
        var l = abp.localization.getResource('EzGet');
        var _feedAdminAppService = easyAbp.ezGet.admin.feeds.feedAdmin;
        var _publicApi = null;

        var initModal = async function (publicApi, args) {
            var $modal = publicApi.getModal();
            var $table = $modal.find('.feeds-table');
            _publicApi = publicApi;

            $table.DataTable(
                abp.libs.datatables.normalizeConfiguration({
                    order: [[1, 'asc']],
                    processing: true,
                    serverSide: true,
                    scrollX: true,
                    paging: true,
                    searching: true,
                    ajax: abp.libs.datatables.createAjax(_feedAdminAppService.getList),
                    columnDefs: abp.ui.extensions.tableColumns.get('ezGet.selectFeed').columns.toArray()
                })
            );
        };

        abp.ui.extensions.entityActions.get('ezGet.selectFeed').addContributor(
            function (actionList) {

                if (actionList.size > 0) {
                    return actionList;
                }

                return actionList.addManyTail(
                    [
                        {
                            text: l('Select'),
                            visible: abp.auth.isGranted('EzGet.Admin.Feeds'),
                            action: function (data) {
                                _publicApi.setResult(data.record);
                                _publicApi.close();
                            }
                        }
                    ]
                );
            }
        );

        abp.ui.extensions.tableColumns.get('ezGet.selectFeed').addContributor(
            function (columnList) {

                if (columnList.size > 0) {
                    return columnList;
                }

                columnList.addManyTail(
                    [
                        {
                            title: l("Actions"),
                            rowAction: {
                                items: abp.ui.extensions.entityActions.get('ezGet.selectFeed').actions.toArray()
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
                                        return l("Private");
                                    case 1:
                                        return l("Public");
                                    default:
                                        return l("Unknow");
                                }
                            }
                        }
                    ]
                );
            },
            0
        );

        return {
            initModal: initModal
        };
    };
})(jQuery);