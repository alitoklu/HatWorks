
var utilities = {
    alert: function (header, body) {
        //$('#alertmodal .modal-header .modal-title').html(header);
        //$('#alertmodal .modal-body').html(body);
        //$('#alertmodal').modal();
        this.onerror(body);
    },
    dil_ceviri: function (text, key) {
        return text;
    }
    ,
    sqldecimal: function (value) {
        if (value == "")
            return 0;
        else
            return parseFloat(value.replace(/\./gi, "").replace(/\,/, "."));
    },
    jsdecimal: function (number, decimal) {
        if (decimal == null)
            decimal = 2;
        return number.toFixed(decimal).replace(/\./, ",")
    },
    onscreen: function (text, bilgi) {
        toastr.success(text, bilgi == null ? 'Başarılı' : bilgi);
        //jq_alert(text, bilgi);
    },
    onerror: function (text, bilgi) {
        toastr.error(text, bilgi == null ? 'Dikkat' : bilgi);
        //jq_alert(text, bilgi);
    },
    oninfo: function (text, bilgi) {
        toastr.info(text, bilgi == null ? 'Bilgi' : bilgi);
        //jq_alert(text, bilgi);
    },
    onwarning: function (text, bilgi) {
        toastr.warning(text, bilgi == null ? 'Uyarı' : bilgi);
        //jq_alert(text, bilgi);
    }
};

var loader = {
    start: function (title) {
        $(".preloader").fadeIn();
    },
    stop: function () {
        $(".preloader").fadeOut();
    }
};

var datatableResponsive = true;
var datatablescrollX = true;




$(function () {
    $("[data-type=date]").attr("data-provide", "datepicker").attr("data-date-format", "dd.mm.yyyy").attr("data-date-start-date", "0d").attr("data-date-language", "tr");

    $("[data-type=datefull]").attr("data-provide", "datepicker").attr("data-date-format", "dd.mm.yyyy").attr("data-date-orientation", "bottom").attr("data-date-language", "tr");
    $("[data-type=datepast]").attr("data-provide", "datepicker").attr("data-date-format", "dd.mm.yyyy").attr("data-date-orientation", "bottom").attr("data-date-language", "tr");

    $(".logo-src").click(function () {
        window.location.href = "/";
    });
});

function jq_alert(text) {
    utilities.alert("Dikkat", text);
    //$("#alertmodal .modal-footer").html("<a href='javascript:;' class='btn btn-dark' data-dismiss='modal'>Kapat</a>");
}

function jq_confirm(title, text, func) {
    $('#confirmmodal .modal-header .modal-title').html(title);
    $('#confirmmodal .modal-body').html(text);
    $('#confirmmodal').modal({
        backdrop: 'static',
        keyboard: false
    });
    $("#confirmmodal .modal-footer").html("<a href='javascript:;' class='btn btn-success btnok'>Tamam</a><a href='javascript:;' class='btn btn-dark' data-dismiss='modal'>Vazgeç</a>");

    $("[data-type=date]").attr("data-provide", "datepicker").attr("data-date-format", "dd.mm.yyyy").attr("data-date-start-date", "0d").attr("data-date-language", "tr");
    //$("[data-type=datetime]").datetimepicker({
    //    format : "DD.MM.YYYY HH:mm"
    //});

    $(".btnok").off().click(function () {
        func();
    });
}

function jq_dialog(title, text, beforefunction, myfunction, modalclass) {
    $('#confirmmodal .modal-header .modal-title').html(title);

    if (modalclass == null || modalclass == "") {
        $('#confirmmodal .modal-dialog').removeClass("modal-lg");
    }
    else
        $('#confirmmodal .modal-dialog').addClass("modal-lg");


    $('#confirmmodal .modal-body').html(text);
    $('#confirmmodal').modal({
        backdrop: 'static',
        keyboard: false
    });
    $("#confirmmodal .modal-footer").html("<a href='javascript:;' class='btn btn-success btnok'>Tamam</a><a href='javascript:;' class='btn btn-dark' data-dismiss='modal'>Vazgeç</a>");

    $("[data-type=date]").attr("data-provide", "datepicker").attr("data-date-format", "dd.mm.yyyy").attr("data-date-start-date", "0d").attr("data-date-language", "tr");

    //$("[data-type=datetime]").datetimepicker({
    //    format: "DD.MM.YYYY HH:mm"
    //});

    beforefunction();

    $(".btnok").off().click(function () {
        var status = myfunction();
        if (status === true)
            $('#confirmmodal').modal("toggle");
    });

}


function jq_dialogfullscreen(title, text, beforefunction, myfunction, modalclass) {
    $('#fullmodal .modal-header .modal-title').html(title);

    if (modalclass == null || modalclass == "") {
        $('#fullmodal .modal-dialog').removeClass("modal-lg");
    }
    else
        $('#fullmodal .modal-dialog').addClass("modal-lg");


    $('#fullmodal .modal-body').html(text);
    $('#fullmodal').modal({
        backdrop: 'static',
        keyboard: false
    });
    $("#fullmodal .modal-footer").html("<a href='javascript:;' class='btn btn-success btnok'>Tamam</a><a href='javascript:;' class='btn btn-dark' data-dismiss='modal'>Kapat</a>");

    $("[data-type=date]").attr("data-provide", "datepicker").attr("data-date-format", "dd.mm.yyyy").attr("data-date-start-date", "0d").attr("data-date-language", "tr");

    //$("[data-type=datetime]").datetimepicker({
    //    format: "DD.MM.YYYY HH:mm"
    //});

    beforefunction();

    $(".btnok").off().click(function () {
        var status = myfunction();
        if (status === true)
            $('#confirmmodal').modal("toggle");
    });

}
function getParameterByName(name, url) {

    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

$(function () {
    $(document).keypress(function (e) {
        if ($(e.target).is(".onlynumber")) {
            if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                return false;
            }
        }
    });

    var url = window.location + "";
    var path = url.replace(window.location.protocol + "//" + window.location.host + "/", "");
    var element = $('ul#sidebarnav a').filter(function () {
        return this.href === url || this.href === path;// || url.href.indexOf(this.href) === 0;
    });
    element.parentsUntil(".vertical-nav-menu").each(function (index) {
        if ($(this).is("li") && $(this).children("a").length !== 0) {
            $(this).children("a").addClass("mm-active");
            $(this).parent("ul#sidebarnav").length === 0
                ? $(this).addClass("mm-active")
                : $(this).addClass("mm-open");
        }
        else if (!$(this).is("ul") && $(this).children("a").length === 0) {
            $(this).addClass("mm-open");
        }
        else if ($(this).is("ul")) {
            $(this).addClass('in');
        }

    });

});


function logout() {
    //$.ajax({
    //    type: "POST",
    //    url: "/admin/logout",
    //    contentType: "application/json; charset=utf-8",
    //    success: function (data) {
    //        if (data.status)
    //            window.location.href = "/admin";
    //    }
    //});
}

if ($.fn.DataTable) {

    $.extend(true, $.fn.DataTable.defaults, {
        "initComplete": function (settings, json) {
            //$(".dataTables_filter input[type=search]").focus();
            $('[data-toggle="tooltip"], a.dt-button').tooltip();

            var api = this.api();
            api.columns.adjust();

            if (typeof initCompleteAfter_Local == "function") {
                initCompleteAfter_Local();
            }
            $('.dt-buttons button').tooltip();
        },
        "drawCallback": function (settings) {
            var api = this.api();
            api.columns.adjust();

            $('[data-toggle="tooltip"]').tooltip();
            //$('[data-toggle="popover"]').popover();

            //$('[data-toggle="popover"]').popover();

            if (typeof drawCallBackAfter_Local == "function") {
                drawCallBackAfter_Local(settings);
            }
        },
        buttons: [
            {
                titleAttr: 'Yazdır',
                extend: 'print',
                text: '<i class="fa fa-print"></i>',
                className: "btn btn-warning",
                title: document.title.split("|")[0].trim(),
                messageTop: moment().format("DD.MM.YYYY HH:mm")
            },
            {
                titleAttr: "Excel'e Aktar",
                extend: 'excelHtml5',
                text: '<i class="fa fa-file-excel"></i>',
                className: "btn btn-success",
                title: document.title.split("|")[0].trim(),
                filename: document.title.split("|")[0].trim() + "_" + moment().format("YYYY.MM.DD_HH.mm")
            }
        ],
        "stateSave": true,
        "lengthMenu": [[10, 25, 50, 100, -1], [10, 25, 50, 100, "Hepsi"]],
        "stateSaveParams": function (settings, data) {
            data.search.search = "";
        },
        "processing": true,
        "jQueryUI": false,
        "pagingType": "full_numbers",
        //"order": [[0, "ASC"]],
        //"dom": '<"H"CB<"clear">lfr>t<"F"ip>',//form-control
        "dom": 'Blfrtip',
        "language": {
            "url": "/assets/datatables/tr.js"
        }
    });
}




function isEmail(emailAddress) {
    //var pattern = new RegExp(/^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i);
    //return pattern.test(emailAddress);

    return /^(?:[\w-]+\.?)*[\w-]+@(?:[\w-]+\.)+[\w]{2,3}$/.test(emailAddress);
}

function isTel(p) {
    var phoneRe = /^[2-9]\d{2}[2-9]\d{2}\d{4}$/;
    var digits = p.replace(/\D/g, "");
    return phoneRe.test(digits);
}

function isDate(value, userFormat) {
    var userFormat = userFormat || 'dd.mm.yyyy', // default format

        delimiter = /[^mdy]/.exec(userFormat)[0],
        theFormat = userFormat.split(delimiter),
        theDate = value.split(delimiter),

        isMyDate = function (date, format) {
            var m, d, y;
            for (var i = 0, len = format.length; i < len; i++) {
                if (/m/.test(format[i])) m = date[i];
                if (/d/.test(format[i])) d = date[i];
                if (/y/.test(format[i])) y = date[i];
            }
            return (
                m > 0 && m < 13 &&
                y && y.length === 4 &&
                d > 0 && d <= (new Date(y, m, 0)).getDate()
            );
        };

    return isMyDate(theDate, theFormat);
}


function jsonDateToTurkishTime(jDate) {
    try {
        date = new Date(parseInt(jDate.substr(6)));

        d = " " + (date.getHours() < 10 ? "0" + (date.getHours()) : date.getHours()) + ":" + (date.getMinutes() < 10 ? "0" + (date.getMinutes()) : date.getMinutes()) + ":" + (date.getSeconds() < 10 ? "0" + (date.getSeconds()) : date.getSeconds());
        return d;
    } catch (e) {
        console.log(e);
        return "";
    }

}
function IsDataExist(datatablename, datacolumn, value) {
    $.ajax({
        type: "POST",
        url: "/ajaxservices/settings.asmx/isdataexist",
        data: JSON.stringify({ "tablename": tablename, "columns": columns, "kosul": kosul }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var jsondata = JSON.parse(msg.d);
            if (jsondata.returnvalue != "") {
                return [false, "Aynı firma kodu ile kaydedilmiş kullanıcı mevcut"];;
            }
            else {
                return [true, ""];
            }

        }
    });
}

function jsonDateToTurkishDate(jDate, withTime) {
    try {
        date = new Date(parseInt(jDate.substr(6)));
        if (date.getFullYear() == 1) return "";
        d = (date.getDate() < 10 ? "0" + (date.getDate()) : date.getDate()) + "." + (date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1) + "." + date.getFullYear();
        if (withTime)
            d += " " + (date.getHours() < 10 ? "0" + (date.getHours()) : date.getHours()) + ":" + (date.getMinutes() < 10 ? "0" + (date.getMinutes()) : date.getMinutes()) + ":" + (date.getSeconds() < 10 ? "0" + (date.getSeconds()) : date.getSeconds());
        return d;
    } catch (e) {
        //console.log(e);
        return "";
    }

}

isNS = (document.layers || (document.getElementById && !document.all)) ? true : false;

function onlyNumber(e) {
    var keyCode = (isNS) ? e.which : e.keyCode;
    if ((keyCode < 48 || keyCode > 57) && keyCode != 8 && keyCode != 0 && keyCode != 44) {
        return false;
    }
}

Number.prototype.formatMoney = function (places, symbol, thousand, decimal) {
    places = !isNaN(places = Math.abs(places)) ? places : 2;
    symbol = symbol !== undefined ? symbol : "₺";
    thousand = thousand || ".";
    decimal = decimal || ",";
    var number = this,
        negative = number < 0 ? "-" : "",
        i = parseInt(number = Math.abs(+number || 0).toFixed(places), 10) + "",
        j = (j = i.length) > 3 ? j % 3 : 0;
    return negative + (j ? i.substr(0, j) + thousand : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thousand) + (places ? decimal + Math.abs(number - i).toFixed(places).slice(2) : "") + symbol;
};

$.strPad = function (i, l, s) {
    var o = i.toString();
    if (!s) { s = '0'; }
    while (o.length < l) {
        o = s + o;
    }
    return o;
};


function Translator(key, text) {
    return text;
}

function createColumnObject(columndisplayname, columnname, columnvalue, columntype, columnlength, columnclasses, columncheck, columnstatus, columncontrolgroup) {
    var obj = {};
    obj["columnname"] = columnname;
    obj["columnvalue"] = columnvalue;
    obj["columntype"] = columntype;
    obj["columnmaxlength"] = columnlength;
    obj["columnclasses"] = columnclasses;
    obj["columndisplayname"] = columndisplayname;
    obj["columncheck"] = columncheck ? columncheck : false;
    obj["columnstatus"] = columnstatus ? columnstatus : false;
    obj["columncontrolgroup"] = columncontrolgroup ? columncontrolgroup : false;
    return obj;
}

function loadFormFromData(requestData, tablename) {

    $.each(requestData, function (key, value) {
        var element = $("*[data-table=" + tablename + "][data-column=" + key + "]");

        if (element.prop("tagName"))
            //console.log(key + "--" + element.prop("tagName"));
            if (element.prop("tagName").toUpperCase() == "INPUT")
                switch (element.prop("type")) {
                    case "hidden":
                    case "text":
                    case "email":
                    case "number":
                    case "password":
                        if (element.attr("data-type") == 'date') {
                            element.val(moment(value).format("DD.MM.YYYY"));
                        }
                        else if (element.attr("data-type") == 'decimal') {
                            element.val(utilities.jsdecimal(value));
                        }
                        else if (element.attr("data-type") == 'float') {
                            element.val(value.toFixed(4));
                        }
                        else {
                            element.val(value.toString());
                        }
                        break;
                    case "checkbox":
                        element.prop("checked", value > 0);
                        break;
                    case "radio":
                        //if (value == element.val()) {
                            //element.prop("checked", true);
                            $("*[data-table=" + tablename + "][data-column=" + key + "][value=" + value + "]").prop("checked", true);
                        //}
                        break;
                }
            else if (element.prop("tagName").toUpperCase() == "SELECT") {
                //var exists = 0 != element.children("option[value=" + value + "]").length;
                //console.log(value + " - " + exists);
                try {
                    //if (element.children("option[value=" + value + "]").get(0))
                    element.val(value.toString().trim());
                }
                catch (e) { }
            }
            else if (element.prop("tagName").toUpperCase() == "TEXTAREA") {
                element.val(value);
            }
    });
}

function createTableObject(tablename) {
    var radiobtn = "";

    var jsonObj = [];
    var obj = {};

    $("*[data-table=" + tablename + "]").each(function () {
        obj = {};
        if ($(this).prop("tagName").toUpperCase() == "INPUT")
            switch ($(this).prop("type")) {
                case "text":
                case "hidden":
                case "email":
                case "password":
                case "number":
                case "tel":
                    obj["columnname"] = $(this).attr("data-column");
                    obj["columntype"] = $(this).attr("data-type");
                    obj["columnclasses"] = $(this).attr("class");
                    obj["columncheck"] = $(this).attr("data-check") || false;
                    obj["columnstatus"] = $(this).attr("data-check") ? $(this).parent().parent().find("span").data("status") : true; // başka amaçla kullanılan span varsa hata verir
                    obj["columndisplayname"] = $(this).siblings("label").html() == null ? $(this).closest(".form-group").find("label:first").html() : $(this).siblings("label").html();

                    obj["columnvalue"] = obj["columntype"] == "decimal" ? utilities.sqldecimal($(this).val()) : $(this).val()//encodeURIComponent($(this).val());
                    obj["columnmaxlength"] = $(this).attr("data-maxlength");
                    obj["columncontrolgroup"] = $(this).attr("data-controlgroup");

                    break;
                case "checkbox":
                    obj["columnname"] = $(this).attr("data-column");
                    obj["columntype"] = $(this).attr("data-type");
                    obj["columnclasses"] = $(this).attr("class");
                    obj["columncheck"] = $(this).attr("data-check") || false;
                    obj["columnstatus"] = $(this).attr("data-check") ? $(this).parent().parent().find("span").data("status") : true;
                    obj["columndisplayname"] = $(this).siblings("label").html();

                    obj["columnvalue"] = ($(this).prop("checked") ? 1 : 0);
                    obj["columnmaxlength"] = "";
                    obj["columncontrolgroup"] = $(this).attr("data-controlgroup");
                    break;
                case "radio":
                    if (radiobtn != $(this).attr("data-column")) {
                        obj["columnname"] = $(this).attr("data-column");
                        obj["columntype"] = $(this).attr("data-type");
                        obj["columnclasses"] = $(this).attr("class");
                        obj["columncheck"] = $(this).attr("data-check") || false;
                        obj["columnstatus"] = $(this).attr("data-check") ? $(this).parent().parent().find("span").data("status") : true;
                        obj["columndisplayname"] = $(this).siblings("label").html();

                        obj["columnvalue"] = $("input[data-table=" + tablename + "][data-column=" + $(this).attr("data-column") + "]:checked").val();
                        obj["columnmaxlength"] = "";
                        obj["columncontrolgroup"] = $(this).attr("data-controlgroup");
                    }
                    radiobtn = $(this).attr("data-column");
                    break;
            }
        else if ($(this).prop("tagName").toUpperCase() == "SELECT") {
            obj["columnname"] = $(this).attr("data-column");
            obj["columntype"] = $(this).attr("data-type");
            obj["columnclasses"] = $(this).attr("class");
            obj["columncheck"] = $(this).attr("data-check") || false;
            obj["columnstatus"] = $(this).attr("data-check") ? $(this).parent().parent().find("span").data("status") : true;
            obj["columndisplayname"] = $(this).siblings("label").html();

            obj["columnvalue"] = $(this).val();
            obj["columnmaxlength"] = "";
            obj["columncontrolgroup"] = $(this).attr("data-controlgroup");
        }
        else if ($(this).prop("tagName").toUpperCase() == "TEXTAREA") {
            obj["columnname"] = $(this).attr("data-column");
            obj["columntype"] = $(this).attr("data-type");
            obj["columnclasses"] = $(this).attr("class");
            obj["columncheck"] = $(this).attr("data-check") || false;
            obj["columnstatus"] = $(this).attr("data-check") ? $(this).parent().parent().find("span").data("status") : true;
            obj["columndisplayname"] = $(this).siblings("label").html() == null ? $(this).closest(".form-group").find("label:first").html() : $(this).siblings("label").html();
            obj["columnvalue"] = $(this).val();
            obj["columnmaxlength"] = $(this).attr("data-maxlength");
            obj["columncontrolgroup"] = $(this).attr("data-controlgroup");
        }
        if (obj["columnname"] != "" && !$.isEmptyObject(obj))
            jsonObj.push(obj);

    });

    var maxlength;
    var classes;

    $.each(jsonObj, function (j, item) {
        maxlength = 0;
        if (item.columntype == "string" && item.columnmaxlength != "") {
            try {
                maxlength = parseInt(item.columnmaxlength);
                if (decodeURIComponent(item.columnvalue).length > maxlength) {
                    jq_alert(item.columndisplayname + " " + utilities.dil_ceviri("Maksimum karakter sayısı :" + maxlength, "max_karakter_sayisi"), "*[data-table=" + tablename + "][data-column=" + item.columnname + "]");
                    jsonObj = false;
                    return false;
                }
            }
            catch (u) { console.log(u); }
        }

        classes = [];
        if (item.columnclasses != null && item.columnclasses != "")
            classes = item.columnclasses.split(' ');
        for (var i = 0; i < classes.length; i++) {
            switch (classes[i]) {
                case "notnull":
                    if ($.trim(item.columnvalue) == "") {
                        jq_alert(item.columndisplayname + " " + utilities.dil_ceviri("boş geçilemez", "bos_gecilemez"), "*[data-table=" + tablename + "][data-column=" + item.columnname + "]");
                        jsonObj = false;
                        return false;
                    }
                    break;
                case "emailcheck":
                    if ($.trim(item.columnvalue) != "" && !isEmail(item.columnvalue)) {
                        jq_alert(item.columndisplayname + " " + utilities.dil_ceviri("geçerli e-posta değil.", "gecerli_eposta_degil"), "*[data-table=" + tablename + "][data-column=" + item.columnname + "]");
                        jsonObj = false;
                        return false;
                    }
                    break;
                case "telcheck":

                    if ($.trim(item.columnvalue) != "" && !isTel(item.columnvalue)) {
                        jq_alert(item.columndisplayname + " " + utilities.dil_ceviri("geçerli telefon numara değil.", "gecerli_telefon_degil"), "*[data-table=" + tablename + "][data-column=" + item.columnname + "]");
                        jsonObj = false;
                        return false;
                    }

                    break;

                case "numeric":
                    if (item.columnvalue != "") {
                        var intVal = parseInt(item.columnvalue);
                        if (isNaN(intVal)) {
                            jq_alert(item.columndisplayname + " " + utilities.dil_ceviri(" nümerik karakterler içermeli", "numerik_icermeli"), "*[data-table=" + tablename + "][data-column=" + item.columnname + "]");
                            jsonObj = false;
                            return false;
                        }
                    }
                    break;
            }
        }
        //console.log(item.columnname + "-" + item.columncheck);
        if (item.columnstatus == false) {
            jq_alert(item.columndisplayname + " " + utilities.dil_ceviri("kullanılmaktadır", "kullanilmaktadir"), "*[data-table=" + tablename + "][data-column=" + item.columnname + "]");
            jsonObj = false;
            return false;
        }
    });

    return jsonObj;
}


function SetDropDownList(datatablename, datacolumnname, tablename, columns, kosul, defaultvalue, addprependoption, orderby) {

    $.ajax({
        type: "POST",
        url: "/helper/selectoptionvalues",
        data: JSON.stringify({ "tablename": tablename, "columns": columns, "kosul": kosul, orderby: orderby }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var htmldata = msg.message;
            if (addprependoption != "") {
                htmldata = addprependoption + htmldata;
            }
            $("select[data-table=" + datatablename + "][data-column=" + datacolumnname + "]").html(htmldata);
        },
        complete: function () {
            if (defaultvalue) {
                $("select[data-table=" + datatablename + "][data-column=" + datacolumnname + "]").val(defaultvalue.toString());
            }
        }
    });
}

Array.prototype.remove = function () {
    var what, a = arguments, L = a.length, ax;
    while (L && this.length) {
        what = a[--L];
        while ((ax = this.indexOf(what)) !== -1) {
            this.splice(ax, 1);
        }
    }
    return this;
};