﻿var abp = abp || {};
(function ($) {
    var l = abp.localization.getResource('EzGet');
    var _credentialAdminAppService = easyAbp.ezGet.admin.credentials.credentialAdmin;

    abp.modals.selectUserModal = function () {
        var initModal = async function (publicApi, args) {
            var $modal = publicApi.getModal();
            var $table = $modal.find('.credential-table');
            var userId = args.userId;

            $table.DataTable(
                abp.libs.datatables.normalizeConfiguration({
                    order: [[1, 'asc']],
                    processing: true,
                    serverSide: true,
                    scrollX: true,
                    paging: true,
                    searching: true,
                    ajax: abp.libs.datatables.createAjax(_credentialAdminAppService.getList, { userId : userId }),
                    columnDefs: abp.ui.extensions.tableColumns.get('ezGet.selectCredential').columns.toArray()
                })
            );

            abp.ui.extensions.entityActions.get('ezGet.selectCredential').addContributor(
                function (actionList) {

                    if (actionList.size > 0) {
                        return actionList;
                    }

                    return actionList.addManyTail(
                        [
                            {
                                text: l('Select'),
                                visible: abp.auth.isGranted('EzGet.Admin.Credentials'),
                                action: function (data) {
                                    publicApi.setResult({ id: data.record.id });
                                    publicApi.close();
                                }
                            }
                        ]
                    );
                }
            );

            abp.ui.extensions.tableColumns.get('ezGet.selectCredential').addContributor(
                function (columnList) {

                    if (columnList.size > 0) {
                        return columnList;
                    }

                    columnList.addManyTail(
                        [
                            {
                                title: l("Actions"),
                                rowAction: {
                                    items: abp.ui.extensions.entityActions.get('ezGet.selectCredential').actions.toArray()
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
                0
            );
        };

        return {
            initModal: initModal
        };
    };

})(jQuery);