function pageLoad(sender, args) {
    $(document).ready(function () {
        var datepick = $('.date');
        $(datepick).datepicker({
            currentText: "Now",
            dateFormat: "dd-mm-yy",
            showButtonPanel: true,
            showOn: "both",

            buttonImage: "images/calendar.gif",
            buttonImageOnly: true,
            buttonText: "Select date"
        });

    });
}
