var abp = abp || {};
$(function () {
    var l = abp.localization.getResource('EzGet');
    var _credentialsTable;

    abp.modals.editFeed = function () {
        var initModal = function (publicApi, args) {
            var $form = publicApi.getForm();

            var _selectUserModal = new abp.ModalManager({
                viewUrl: abp.appPath + 'EzGet/Users/SelectUserModal',
                scriptUrl: '/Pages/EzGet/Users/selectUserModal.js',
                modalClass: 'selectUserModal'
            });

            var _selectCredentialModal = new abp.ModalManager({
                viewUrl: abp.appPath + 'EzGet/Credentials/SelectCredentialModal',
                scriptUrl: '/Pages/EzGet/Credentials/selectCredentialModal.js',
                modalClass: 'selectCredentialModal'
            });

            initSaveButtonEvent($form);
            initCredentials($form);

            function getUserId() {
                return $form.find('[name="FeedInfo.UserId"]').val();
            }

            _selectCredentialModal.onResult(function (arg) {
                addTableData(_credentialsTable, arg, "credentials-table");
            });

            $form.find('button.select-user').click(function () {
                _selectUserModal.open();
            });

            _selectUserModal.onResult(function (arg) {
                $form.find('input.sreach-user-id').val(arg.id);
                _credentialsTable.clear().draw();
            });

            $('#AddNewCredentialButton').on('click', function (event) {
                _selectCredentialModal.open({ userId: getUserId()})
            });
        };

        return {
            initModal: initModal
        };
    }

    function initSaveButtonEvent($form) {
        $form.find("button[type='submit']").click(function (e) {
            generateDynamicForm();
        });
    }

    function generateDynamicForm() {
        var $dynamicForm = $("#dynamicForm");
        $dynamicForm.html("");
        var credentialsHtml = getCredentialsFormHtml();
        $dynamicForm.append(credentialsHtml);
    }

    function initCredentials($form) {
        const className = "credentials-table";
        var table = initCredentialsTable($form, className);
        _credentialsTable = table.dataTable;
        var dataList = getCredentialDatasFromDom();
        addTableDatas(_credentialsTable, dataList, className);
        drawTalbeOnTabShown("FeedEditTabs_1-tab", _credentialsTable);
    }

    function getCredentialsFormHtml() {
        var credential = _credentialsTable.rows().data();
        if (credential === undefined || credential === null || credential.length <= 0) {
            return "";
        }
        var html = "";
        for (var i = 0; i < credential.length; i++) {
            html += `<input type="text" name="FeedInfo.CredentialIds[${i}]" value="${credential[i].id}"/>`;
        }
        return html;
    }

    function getCredentialDatasFromDom() {
        var datas = [];
        $("#credentials > div").each(function () {
            var data = {
                id: $(this).find(".credential-id").val(),
                value: $(this).find(".credential-value").val(),
                description: $(this).find(".credential-description").val(),
                expires: $(this).find(".credential-expires").val()
            }
            datas.push(data);
        });
        return datas;
    }

    function initCredentialsTable($el, className) {
        var $table = $el.find(`.${className}`);
        var dataTable = $table.DataTable(abp.libs.datatables.normalizeConfiguration({
            processing: false,
            serverSide: false,
            scrollX: false,
            paging: false,
            searching: false,
            ordering: false,
            info: false,
            columnDefs: [
                {
                    data: "value",
                },
                {
                    data: "description"
                },
                {
                    data: "expires"
                },
                {
                    render: function (data, type, row) {
                        var html = `<button class="btn btn-danger btn-sm delete-${className}" type="button">${l("Delete")}</buttom>`;
                        return html;
                    }
                }
            ]
        }));

        dataTable.on('draw', function () {
            if (dataTable.rows().data().length <= 0) {
                tableRootToggle(false, className);
            }
        });

        return { $table, dataTable };
    }

    //functional table
    function tableRootToggle(show, className) {
        if (show) {
            $(`.${className}`).parent().parent().parent().parent(":first").css("display", "block");
        } else {
            $(`.${className}`).parent().parent().parent().parent(":first").css("display", "none");
        }
    }

    function initRemoveTableDataBtn(dataTable, row, className) {
        $(row).find("button").click(function () {
            dataTable.row(row).remove().draw();
        });
    }

    function addTableData(dataTable, data, className) {
        if (data === undefined || data === null) {
            return;
        }
        tableRootToggle(true, className);
        var row = dataTable.row.add(data).draw().node();
        initRemoveTableDataBtn(dataTable, row, className);
    }

    function addTableDatas(dataTable, addDatas, className) {
        if (addDatas === undefined || addDatas === null || addDatas.length <= 0) {
            return;
        }

        tableRootToggle(true, className);
        for (var i = 0; i < addDatas.length; i++) {
            var row = dataTable.row.add(addDatas[i]).node();
            initRemoveTableDataBtn(dataTable, row, className);
        }
        dataTable.draw();
    }

    function drawTalbeOnTabShown(id, dataTable) {
        $(`#${id}`).on("shown.bs.tab", function () {
            dataTable.draw();
        });
    }
});
