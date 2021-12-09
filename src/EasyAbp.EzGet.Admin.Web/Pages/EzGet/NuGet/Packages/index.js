(function ($) {
    var l = abp.localization.getResource('EzGet');
    var _nuGetPackageAdminAppService = easyAbp.ezGet.admin.nuGet.packages.nuGetPackageAdmin;

    var _editModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'EzGet/NuGet/Packages/EditModal',
        modalClass: 'editNuGetPackage'
    });

    var _createModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'EzGet/NuGet/Packages/CreateModal',
        modalClass: 'createNuGetPackage'
    });

    var _selectFeedModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'EzGet/Feeds/SelectFeedModal',
        scriptUrl: '/Pages/EzGet/Feeds/selectFeedModal.js',
        modalClass: 'selectFeedModal'
    });

    $(function () {
        var getFilter = function () {
            return {
                feedId: $('#EzGetNuGetPacakgesWrapper input.sreach-feed-id').val(),
                packageName: $('#EzGetNuGetPacakgesWrapper input.package-name').val()
            };
        };

        var _$table = $('#NuGetPackagesTable');
        _dataTable = _$table.DataTable(
            abp.libs.datatables.normalizeConfiguration({
                order: [[1, 'asc']],
                processing: true,
                serverSide: true,
                scrollX: true,
                paging: true,
                searching: false,
                ajax: abp.libs.datatables.createAjax(_nuGetPackageAdminAppService.getList, getFilter),
                columnDefs: abp.ui.extensions.tableColumns.get('ezGet.nuGetPackage').columns.toArray()
            })
        );

        $('#EzGetNuGetPacakgesWrapper button.select-feed').click(function () {
            _selectFeedModal.open();
        });

        $('#EzGetNuGetPacakgesWrapper form.nugetpackages-search-form').submit(function (e) {
            e.preventDefault();
            _dataTable.ajax.reload();
        });

        $('#AbpContentToolbar button[name=CreateFeed]').click(function (e) {
            _createModal.open();
        });
    });

    abp.ui.extensions.entityActions.get('ezGet.nuGetPackage').addContributor(
        function (actionList) {
            return actionList.addManyTail(
                [
                    {
                        text: l('Edit'),
                        visible: abp.auth.isGranted('EzGet.Admin.NuGetPackage.Update'),
                        action: function (data) {
                            _editModal.open({
                                id: data.record.id,
                            });
                        },
                    }
                ]
            );
        }
    );

    abp.ui.extensions.tableColumns.get('ezGet.nuGetPackage').addContributor(
        function (columnList) {
            columnList.addManyTail(
                [
                    {
                        title: l("Actions"),
                        rowAction: {
                            items: abp.ui.extensions.entityActions.get('ezGet.nuGetPackage').actions.toArray()
                        }
                    },
                    {
                        title: l('PackageName'),
                        data: 'packageName',
                    },
                    {
                        title: l('NormalizedVersion'),
                        data: 'normalizedVersion',
                    },
                    {
                        title: l('Downloads'),
                        data: 'downloads',
                    },
                    {
                        title: l('Listed'),
                        data: 'listed',
                    },
                ]
            );
        },
        0 //adds as the first contributor
    );
})(jQuery);
