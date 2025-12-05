function GetTodayDateWithFormat() {
    const today = new Date();

    const day = String(today.getDate()).padStart(2, '0');
    const month = String(today.getMonth() + 1).padStart(2, '0');
    const year = today.getFullYear();

    const formattedDate = year + '-' + month + '-' + day

    return formattedDate;

}

function GetTomorrowDateWithFormat() {
    const today = new Date();

    const tomorrow = new Date(today);
    tomorrow.setDate(today.getDate() + 1);

    const day = String(tomorrow.getDate()).padStart(2, '0');
    const month = String(tomorrow.getMonth() + 1).padStart(2, '0');
    const year = tomorrow.getFullYear();

    const formattedTomorrow = year + '-' +month +'-'+ day

    return formattedTomorrow;
}

function FormatDate(dateStr) {
    const datim = new Date(dateStr);
    const day = String(datim.getDate()).padStart(2, '0');
    const month = String(datim.getMonth() + 1).padStart(2, '0');
    const year = datim.getFullYear();

    const formattedDate = year + '-' + month + '-' + day

    return formattedDate;
}
function isPastDate(inputDateStr) {
    const parts = inputDateStr.split('-');
    const day = parseInt(parts[0], 10);
    const month = parseInt(parts[1], 10) - 1;
    const year = parseInt(parts[2], 10);

    const inputDate = new Date(year, month, day);

    const today = new Date();
    today.setHours(0, 0, 0, 0);

    return inputDate < today;
}
function GetInpOriginOnReady() {
    $.ajax({
        url: '/Home/GetBusLocations',
        type: 'GET',
        success: function (data) {

            var $sel = $('#inpOrigin');
            $sel.empty();
            $sel.append(new Option("Lütfen seçiniz", '0', false, false));

            data = data.filter(function (item) {
                return item.id !== $("#inpTarget").val();
            });


            data.forEach(function (item) {

                $sel.append(new Option(item.name, item.id, false, false));
            });

            $sel.select2({
                placeholder: "Nereden",
                allowClear: true
            });
        }
    });
}

function GetInpTargetOnReady() {
    $.ajax({
        url: '/Home/GetBusLocations',
        type: 'GET',
        success: function (data) {

            var $sel = $('#inpTarget');
            $sel.empty();
            $sel.append(new Option("Lütfen seçiniz", '0', false, false));

            data = data.filter(function (item) {
                return item.id !== $("#inpTarget").val();
            });


            data.forEach(function (item) {

                $sel.append(new Option(item.name, item.id, false, false));
            });

            $sel.select2({
                placeholder: "Nereden",
                allowClear: true
            });
        }
    });
}



function GetinpOrigin() {
    $('#inpOrigin').select2({
        placeholder: 'Nereden',
        ajax: {
            url: '/Home/GetBusLocations',
            dataType: 'json',
            data: function (params) {
                return {
                    q: params.term
                };
            },
            processResults: function (data) {
                data.splice(data.map(e => e.id).indexOf(parseInt($('#inpTarget').val())), 1);
                return {
                    results: data.map(function (item) {
                        return {
                            id: item.id,
                            text: item.name
                        };
                    })
                };
            },
            cache: true
        }
    });
}

function swapLocations() {

    const originId = $('#inpOrigin').val();
    const targetId = $('#inpTarget').val();

    const originName = $('#inpOriginName').val();
    const targetName = $('#inpTargetName').val();

    if (targetId) {
        $('#inpOrigin').append(new Option(targetName, targetId, true, true)).trigger("change");
    }

    if (originId) {
        $('#inpTarget').append(new Option(originName, originId, true, true)).trigger("change");
    }

    $('#inpOriginName').val(targetName);
    $('#inpTargetName').val(originName);
}


function htmlDecode(str) {
    const parser = new DOMParser();
    const doc = parser.parseFromString(str, 'text/html');
    return doc.documentElement.textContent;
}

function getCookie(name) {
    const cookies = document.cookie.split(';');
    for (let c of cookies) {
        const [key, value] = c.trim().split('=');
        if (key === name) {
            return decodeURIComponent(value);
        }
    }
    return null;
}

function FormatTurkishDate(dateStr) {
    const [day, month, year] = dateStr.split('-').map(Number);

    const date = new Date(year, month - 1, day);
    const months = [
        "Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran",
        "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık"
    ];

    const days = [
        "Pazar", "Pazartesi", "Salı", "Çarşamba",
        "Perşembe", "Cuma", "Cumartesi"
    ];

    const dayName = days[date.getDay()];
    const monthName = months[date.getMonth()];

    return `${day} ${monthName} ${dayName}`;
}
