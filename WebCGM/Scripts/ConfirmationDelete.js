// delete Link
$('.delete-link').click(function () {
    var deleteLinkObj = $(this);  //for future use

    $("#delete-dialog").dialog({
        title: "Confirmation",
        buttons: {
            Continue: function () {
                $.post(deleteLinkObj[0].href, function (data) {  //Post to action
                    if (data == 'True') {
                        var tr = deleteLinkObj.parents('tr:first');
                        tr.hide('fast'); //Hide Row
                    }
                    else {
                        //(optional) Display Error
                    }
                });
                $(this).dialog('close');
            },
            Close: function () {
                $(this).dialog('close');
            }
        },
        width: 400,
        closeOnEscape: false,
        draggable: false,
        resizable: false,
        modal: true
    });
    return false; // prevents the default behaviour
});