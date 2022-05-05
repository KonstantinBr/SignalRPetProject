$(document).ready(function () {

    function endChange() {
        var endDate = end.value();
        endDate = new Date(endDate);
        end.max(Date.now);
    }

    var end = $("#DayOfBirth").kendoDatePicker({
        change: endChange,
        format: "dd.MM.yyyy",
        max: new Date(Date.now())
    }).data("kendoDatePicker");

});


$('.updateUserForm').submit(function (e) {
    e.preventDefault();
    submitAjaxForm($(e.target));
});


function submitAjaxForm(form) {
    var isDate = false;
    var dataValue = $(".userData", form).val();
    if (dataValue == "") {
        dataValue = $("#DayOfBirth", form).val();
        isDate = true;
    }
    var url = form.attr("action");
    var field = form.attr("field");
    var user = {};
    user[field] = dataValue;
    $.ajax({
        type: "POST",
        url: url,
        data: user,
        success: function (data) {
            if (!isDate)
                form.find(".userData").val(data.returnedText);
            else
                $("#DayOfBirth", form).val(data.returnedText);
        }
    });
}