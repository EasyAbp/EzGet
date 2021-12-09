var ezGet = ezGet || {};
(function ($) {
    ezGet.ui = ezGet.ui || {};
    ezGet.ui.extensions = ezGet.ui.extensions || {};

    ezGet.ui.extensions.tableRootToggle = function (show, className) {
        if (show) {
            $(`.${className}`).parent().parent().parent().parent(":first").css("display", "block");
        } else {
            $(`.${className}`).parent().parent().parent().parent(":first").css("display", "none");
        }
    }

    ezGet.ui.extensions.addTableData = function (dataTable, data, className) {
        if (data === undefined || data === null) {
            return;
        }
        ezGet.ui.extensions.tableRootToggle(true, className);
        var row = dataTable.row.add(data).draw().node();
        initRemoveTableDataBtn(dataTable, row, className);
    }

    ezGet.ui.extensions.addTableDatas = function (dataTable, addDatas, className) {
        if (addDatas === undefined || addDatas === null || addDatas.length <= 0) {
            return;
        }

        ezGet.ui.extensions.tableRootToggle(true, className);
        for (var i = 0; i < addDatas.length; i++) {
            var row = dataTable.row.add(addDatas[i]).node();
            initRemoveTableDataBtn(dataTable, row, className);
        }
        dataTable.draw();
    }

    ezGet.ui.extensions.drawTalbeOnTabShown = function (id, dataTable) {
        $(`#${id}`).on("shown.bs.tab", function () {
            dataTable.draw();
        });
    }

    function initRemoveTableDataBtn (dataTable, row) {
        $(row).find("button").click(function () {
            dataTable.row(row).remove().draw();
        });
    }
})(jQuery);