(function ($) {
    var _packageRegistrationPublicAppService = easyAbp.ezGet.public.packageRegistrations.packageRegistrationPublic;
    console.log(_packageRegistrationPublicAppService);
    $(".item").each(function () {
        let id = $(this).find(".item-id").val();
        
        for (let i = 0; i < 3; i++) {
            $(this).find(`.item-actions li:nth-child(${i+1})`).click(function () {
                _packageRegistrationPublicAppService
                    .delete(id, { type: i})
                    .then(function () {
                        //window.location.reload();
                    });
            });
        }
    });
})(jQuery);
