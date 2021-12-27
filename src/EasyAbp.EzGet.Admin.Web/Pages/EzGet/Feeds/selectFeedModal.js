var abp = abp || {};
(function ($) {
    abp.modals.selectFeedModal = function () {
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
                    columnDefs: [
                        {
                            title: l('Actions'),
                            rowAction: {
                                items: [
                                    {
                                        text: l('Select'),
                                        visible: abp.auth.isGranted('EzGet.Admin.Feeds'),
                                        action: function (data) {
                                            _publicApi.setResult(data.record);
                                            _publicApi.close();
                                        }
                                    }
                                ]
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
                })
            );
        };

        return {
            initModal: initModal
        };
    };
})(jQuery);