var abp = abp || {};
$(function () {
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


            function getUserId() {
                return $form.find('[name="FeedInfo.UserId"]').val();
            }

            var credentialIndex = $('#CredentialsStartIndex').val();
            var credentialCount = $('#CredentialsStartIndex').val();

            var getCredentialTableRow = function (id, value, description, expires) {
                return "<tr>\r\n<td>\r\n" +
                    "                                        " + value + "</td> <td> \r\n" +
                    "                                        " + description + "</td> <td>\r\n" +
                    "                                       " + expires + " </td>\r\n<td hidden>\r\n " +
                    "                                       <input type=\"text\" name=\"FeedInfo.CredentialIds[" + credentialIndex + "]\" value=\"" + id + "\"/></td><td>" +
                    "                                       <button type=\"button\" class=\"btn btn-danger btn-sm float-right deleteCredentialButton\"><i class=\"fa fa-trash\"></i></button>" +
                    "</td></tr>";
            }

            _selectCredentialModal.onResult(function (arg) {
                var html = getCredentialTableRow(arg.id, arg.value, arg.description, arg.expires);
                $("#CredentialTableBodyId").append(html);
                credentialIndex++;
                credentialCount++;
                $("#CredentialTableId").show();
            });

            $(document).on('click', '.deleteCredentialButton', function (event) {
                event.preventDefault();
                var tag = $(this).parent().parent();
                var inputs = tag.find("input");
                $(inputs).each(function (i) {
                    $(this).val("");
                });

                tag.hide();
                credentialCount--;

                if (credentialCount == 0) {
                    $("#CredentialTableId").hide();
                }
            });

            $form.find('button.select-user').click(function () {
                _selectUserModal.open();
            });

            _selectUserModal.onResult(function (arg) {
                $form.find('input.sreach-user-id').val(arg.id);
            });

            $('#AddNewCredentialButton').on('click', function (event) {
                event.preventDefault();
                _selectCredentialModal.open({ userId: getUserId()})
            });


        };

        return {
            initModal: initModal
        };
    }
});
