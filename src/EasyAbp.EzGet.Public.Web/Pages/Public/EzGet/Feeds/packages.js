(function ($) {
    $(".item").each(function () {
        var id = $(this).find(".item-id").val();
        $(this).find(".item-actions li:nth-child(1)").click(function () {
            var thisId = id;
            console.log(thisId);
        });
    });
})(jQuery);
