﻿var abp = abp || {};
(function ($) {
    var l = abp.localization.getResource('EzGet');
    var _ezGetUserAppService = easyAbp.ezGet.admin.users.ezGetUser;

    abp.modals.selectUserModal = function () {
        var initModal = async function (publicApi, args) {
            var $modal = publicApi.getModal();
            var $table = $modal.find('.users-table');

            $table.DataTable(
                abp.libs.datatables.normalizeConfiguration({
                    order: [[1, 'asc']],
                    processing: true,
                    serverSide: true,
                    scrollX: true,
                    paging: true,
                    searching: true,
                    ajax: abp.libs.datatables.createAjax(_ezGetUserAppService.getList),
                    columnDefs: abp.ui.extensions.tableColumns.get('ezGet.selectUser').columns.toArray()
                })
            );

            abp.ui.extensions.entityActions.get('ezGet.selectUser').addContributor(
                function (actionList) {

                    if (actionList.size > 0) {
                        return actionList;
                    }

                    return actionList.addManyTail(
                        [
                            {
                                text: l('Select'),
                                visible: abp.auth.isGranted('EzGet.Admin.Users'),
                                action: function (data) {
                                    publicApi.setResult({ id: data.record.id });
                                    publicApi.close();
                                }
                            }
                        ]
                    );
                }
            );

            abp.ui.extensions.tableColumns.get('ezGet.selectUser').addContributor(
                function (columnList) {

                    if (columnList.size > 0) {
                        return columnList;
                    }

                    columnList.addManyTail(
                        [
                            {
                                title: l("Actions"),
                                rowAction: {
                                    items: abp.ui.extensions.entityActions.get('ezGet.selectUser').actions.toArray()
                                }
                            },
                            {
                                title: l('UserName'),
                                data: 'userName',
                            },
                            {
                                title: l('Name'),
                                data: 'name',
                            },
                            {
                                title: l('Email'),
                                data: 'email',
                            },
                            {
                                title: l('PhoneNumber'),
                                data: 'phoneNumber',
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
    }
})(jQuery);