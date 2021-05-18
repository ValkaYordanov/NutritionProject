
function getjson() {
    var json = null;
    $.ajax({
        'async': false,
        'global': false,
        'url': "../js/eans.json",
        'dataType': "json",
        'success': function (data) {
            json = data;
        }
    });
    return json;
};
var eans = getjson();
getjson();

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
const scanner = document.getElementById("barcode-scanner");

function load_quagga() {
    scanner.classList.toggle('visible');
    if ($('#barcode-scanner').length > 0 && navigator.mediaDevices && typeof navigator.mediaDevices.getUserMedia === 'function') {

        var last_result = [];
        if (Quagga.initialized == undefined) {
            Quagga.onDetected(function (result) {

                var last_code = result.codeResult.code;
                last_result.push(last_code);
                console.log(last_code);
                $('#GTIN').val(last_code);
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
                    scanner.classList.remove('visible');
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
            debug: {
                drawBoundingBox: true,
                showFrequency: true,
                drawScanline: true,
                showPattern: true
            },
            locator: {
                halfSample: false,
                patchSize: "large", // x-small, small, medium, large, x-large
                debug: {
                    showCanvas: false,
                    showPatches: false,
                    showFoundPatches: false,
                    showSkeleton: false,
                    showLabels: false,
                    showPatchLabels: false,
                    showRemainingPatchLabels: false,
                    boxFromPatches: {
                        showTransformed: false,
                        showTransformedBox: false,
                        showBB: false
                    }
                }
            },
            decoder: {
                readers: ['ean_reader', 'code_93_reader']
                //readers : ['ean_reader','ean_8_reader','code_39_reader','code_39_vin_reader','codabar_reader','upc_reader','upc_e_reader']
            }
        }, function (err) {
                if (err) {
                    scanner.classList.add('error');
                    console.log(err);
                    return
                }
            scanner.classList.remove('error');
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