$(document).ready(function () {
    $('#cmd').click(function () {
        var pdf = new jsPDF('p', 'pt', 'a4');
        var options = {
            //pagesplit: true,
            format:"PNG",
            background:"#FFF"
        };

        pdf.addHTML($('#page1')[0], 0, 0, options, function () { pdf.addPage() });
        pdf.addHTML($('#page2')[0], 0, 0, options, function () {
            pdf.save("test.pdf");
        });
    });
});