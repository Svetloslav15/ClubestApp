$("#export-btn-pdf").on("click", function () {
    html2canvas($('#my-events-table')[0], {
        onrendered: function (canvas) {
            var data = canvas.toDataURL();
            var docDefinition = {
                content: [{
                    image: data,
                    width: 500
                }]
            };
            pdfMake.createPdf(docDefinition).download("MyEvents.pdf");
        }
    });
});

$(".export-poll-btn-pdf").on("click", function () {
    html2canvas($(this).parent()[0], {
        onrendered: function (canvas) {
            var data = canvas.toDataURL();
            var docDefinition = {
                content: [{
                    image: data,
                    width: 500
                }]
            };
            pdfMake.createPdf(docDefinition).download("Polls-Results.pdf");
        }
    });
});