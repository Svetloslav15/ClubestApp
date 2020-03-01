$('#export-btn').click(function () {
    let titles = [];
    let data = [];

    $('.dataTable th').each(function () {
        titles.push($(this).text());
    });

    $('.dataTable td').each(function () {
        data.push($(this).text());
    });

    let CSVString = prepCSVRow(titles, titles.length, '');
    CSVString = prepCSVRow(data, titles.length, CSVString);

    let fileName = $('#event-name-hidden').val();
    console.log(fileName)
    if (!fileName) {
        fileName = 'MyEvents.csv';
    }
    else {
        fileName = `${fileName}.csv`;
    }

    let downloadLink = document.createElement("a");
    let blob = new Blob(["\ufeff", CSVString]);
    let url = URL.createObjectURL(blob);
    downloadLink.href = url;
    downloadLink.download = fileName;

    document.body.appendChild(downloadLink);
    downloadLink.click();
    document.body.removeChild(downloadLink);
});

function prepCSVRow(arr, columnCount, initial) {
    let row = '';
    let delimeter = ','; 
    let newLine = '\r\n';

    function splitArray(_arr, _count) {
        let splitted = [];
        let result = [];
        _arr.forEach(function (item, idx) {
            if ((idx + 1) % _count === 0) {
                splitted.push(item);
                result.push(splitted);
                splitted = [];
            } else {
                splitted.push(item);
            }
        });
        return result;
    }
    let plainArr = splitArray(arr, columnCount);
    plainArr.forEach(function (arrItem) {
        arrItem.forEach(function (item, idx) {
            row += item + ((idx + 1) === arrItem.length ? '' : delimeter);
        });
        row += newLine;
    });
    return initial + row;
}