
function getjson() {
    var json = null;
    $.ajax({
        'async': false,
        'global': false,
        'url': "eans.json",
        'dataType': "json",
        'success': function (data) {
            json = data;
        }
    });
    console.log(json);
    return json;
};
var eans = getjson();
getjson();


//const indicator = document.getElementById('indicator');
//const nean = document.getElementById('myean');

function order_by_occurrence(arr) {
    var counts = {};
    arr.forEach(function (value) {
        if (!counts[value]) {
            counts[value] = 0;
        }
        counts[value]++;
    });

    return Object.keys(counts).sort(function (curKey, nextKey) {
        return counts[curKey] < counts[nextKey];
    });
}

function load_quagga() {
    //$('#pnavn').text('Produktnavn: XXXXXXXXXXXXX');
    //$('#result').text('Kode: XXXXXXXXXXXXX');
    //$('#indicator').css('background', '#f00');
    if ($('#barcode-scanner').length > 0 && navigator.mediaDevices && typeof navigator.mediaDevices.getUserMedia === 'function') {

        var last_result = [];
        if (Quagga.initialized == undefined) {
            Quagga.onDetected(function (result) {

                //$('#indicator').css('background', '#fff200');
                var last_code = result.codeResult.code;
                last_result.push(last_code);
                console.log(last_code);
                if (last_result.length > 3) {
                    code = order_by_occurrence(last_result)[0];
                    last_result = [];
                    console.log(code);
                    console.log(last_result);

                    var ptitle;

                    //nean.value = '' + code + '';
                    //$('#indicator').css('background', '#0f0');
                    for (var i = 0; i < eans.length; i++) {
                        //console.log(eans[i].product.title);
                        if (eans[i].ean == code) {
                            var ptitle = eans[i].title;
                            console.log(ptitle);
                            $('#ProductName').text(ptitle);
                        }
                    }
                    $('#GTIN').val(code);

                    Quagga.stop();
                    //load_quagga();
                }
            });
        }

        Quagga.init({
            inputStream: {
                name: "Live",
                type: "LiveStream",
                numOfWorkers: navigator.hardwareConcurrency,
                target: document.querySelector('#barcode-scanner')
            },
            decoder: {
                readers: ['ean_reader', 'code_93_reader', 'ean_8_reader']
                //readers : ['ean_reader','ean_8_reader','code_39_reader','code_39_vin_reader','codabar_reader','upc_reader','upc_e_reader']
            }
        }, function (err) {
            if (err) { console.log(err); return }
            Quagga.initialized = true;
            Quagga.start();
        });

    }
};
$('#barcode').click(function () {
    load_quagga();
});
$(document).ready(function () {

    
});