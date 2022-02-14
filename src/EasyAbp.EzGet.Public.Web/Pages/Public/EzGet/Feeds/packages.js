(function ($) {
    var _packageRegistrationPublicAppService = easyAbp.ezGet.public.packageRegistrations.packageRegistrationPublic;

    var _addOwnerModal = new abp.ModalManager(
        abp.appPath + 'Public/EzGet/Feeds/AddOwnerModal'
    );
    
    var _removeOwnerModal = new abp.ModalManager(
        abp.appPath + 'Public/EzGet/Feeds/RemoveOwnerModal'
    );
    
    $(".item").each(function () {
        let id = $(this).find(".item-id").val();
        
        for (let i = 0; i < 3; i++) {
            $(this).find(`.item-delete-actions li:nth-child(${i+1})`).click(function () {
                _packageRegistrationPublicAppService
                    .delete(id, { type: i})
                    .then(function () {
                        window.location.reload();
                    });
            });
        }

        $(this).find(`.item-owners-actions li:nth-child(1)`).click(function () {
            _addOwnerModal.open({id});
        });
        
        $(this).find(`.item-owners-actions li:nth-child(2)`).click(function () {
            _removeOwnerModal.open({id});
        });
    });
})(jQuery);
