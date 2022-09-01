$(document).ready(function () {
    $(".cServerNm strong").text($("[jid='ddlServerNm'] option:selected")[0].text);
    //alert($("[jid='ddlServerNm'] option:selected")[0].value);
    $("[jid='ddlServerNm'] option").click(function () {
        $(".cServerNm strong").text($(this)[0].text);
    });
});