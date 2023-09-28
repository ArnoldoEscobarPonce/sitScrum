$(document).ready(function () {
    InitializeJSComponents();
});

function InitializeJSComponents() {

    // Iniciamos tablas
    InitializeTables();

    // Iniciamos calendarios
    InitializeCalendars();

    // Iniciamos botones responsive
    InitializeResponsiveButton();

    // Iniciamos las máscaras
    InitializeInputMask();

    // Iniciamos los select-picker
    InitializeSelectPicker();

    initAjaxBackgroundNotification();
}

/// Esta función inicializa todas las tablas que se encuentran en el DOM
function InitializeTables() {
    // Asignamos el texto que aparecerá en la opción todos
    var allSelection = 'Todos';

    // Iniciamos las tablas que se encuentren en el DOM
    if ($('.isDataTable').length > 0) {
        $('.isDataTable').each(function () {
            var $curTable = $(this);
            if ($curTable.attr('role') == undefined) {
                var withOutPagin = $curTable.hasClass("withOutPagin");
                var noAutoFit = $curTable.hasClass("withOutAutoFit");
                var responsiveTable = $curTable.hasClass("isResponsiveTable");
                var stateSave = $curTable.hasClass("isStateSave");
                var filterColumn = $curTable.hasClass("WithColumnFilter");
                var addButtons = $curTable.hasClass("isExportTable");

                var dataTableSetup = {
                    "autoWidth": !noAutoFit,
                    "responsive": responsiveTable,
                    "bStateSave": stateSave,
                    "bPaginate": !withOutPagin,
                    "aaSorting": [],
                    "scrollCollapse": true,
                    "aLengthMenu": [[-1, 10, 25, 50, 100], [allSelection, 10, 25, 50, 100]],
                    "iDisplayLength": 10,
                    "fixedcolumns": true,
                    "ordercellstop": true,
                    "language": {
                        "url": getDomainIMSA() +"/lib/i18n/es.json"
                    },
                    "ordering": !filterColumn,
                    "dom": "Rlfrtip",
                    "initComplete": function () {
                        if (filterColumn) {
                            this.api().columns().every(function () {
                                var column = this;
                                if ($(column.header()).hasClass('filter-header')) {
                                    var select = $('<select class="filter-table-control"><option value="">-Todos-</option></select>')
                                        .appendTo($(column.header()))
                                        .on('change', function () {
                                            var val = $.fn.dataTable.util.escapeRegex(
                                                $(this).val()
                                            );

                                            column
                                                .search(val ? '^' + val + '$' : '', true, false)
                                                .draw();
                                        });

                                    column.data().unique().sort().each(function (d, _j) {
                                        select.append('<option value="' + d + '">' + d + '</option>')
                                    });
                                }
                            });
                        }
                    }
                };

                if (addButtons) {
                    dataTableSetup.dom = 'Blfrtip';
                    dataTableSetup.buttons = [
                        {
                            extend: 'pdf',
                            text: 'PDF',
                            className: 'btn btn-info'
                        },
                        {
                            extend: 'excel',
                            text: 'Excel',
                            className: 'btn btn-info'
                        }
                    ]
                }

                if (!$.fn.dataTable.isDataTable($curTable)) {
                    $curTable.DataTable(dataTableSetup);
                    if (!responsiveTable) {
                        $($curTable.dataTable.tables(true)).DataTable().columns.adjust().draw();                        
                        $($curTable.dataTable.tables(true)).DataTable().scroller.measure();
                    }
                }
                
            }

        })
    }
}

/// Esta función inicializa todos los picker o calendarios que se encuentran en el DOM
function InitializeCalendars() {
    // Definimos idioma estandar para Picker
    $.datepicker.regional['es'] = {
        closeText: 'Cerrar',
        prevText: '< Ant',
        nextText: 'Sig >',
        currentText: 'Hoy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
        dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
        dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        weekHeader: 'Sm',
        dateFormat: 'dd/mm/yy',
        firstDay: 1,
        isRTL: false,
        showMonthAfterYear: false,
        yearSuffix: ''
    };

    // Asignamos idioma standar para picker
    $.datepicker.setDefaults($.datepicker.regional["es"]);

    // Iniciamos todos los picker
    if ($(".isDatepicker").length > 0) {
        $(".isDatepicker").each(function () {
            var $picker = $(this);
            var maxToday = $picker.hasClass("maxToday")
            $picker.datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                maxDate: (maxToday ? '0' : '')
            });

            $picker.mask('00/00/0000', { placeholder: '__/__/____' });
        });
    }
}

/// Esta función inicializa todos los botone que muestran el loader que se encuentran en el DOM
function InitializeResponsiveButton() {
    if ($(".isResponsiveButton").length > 0) {
        $(".isResponsiveButton").each(function () {
            var $respBtn = $(this);
            if (!$respBtn.hasClass("isValidateButton"))
                $respBtn.click(ShowWaitingAnimation);
        });
    }
}


function InitializeInputMaskNumeric() {
    var $defaultSize = 10;
    if ($(".isNumeric").length > 0) {
        $(".isNumeric").each(function () {
            var sizeMask = { mask: '', placeholder: '' };

            var $inputNumeric = $(this);
            var $size = $inputNumeric[0].hasAttribute("data-size") ? parseInt($inputNumeric[0].attributes.getNamedItem("data-size").value) : $defaultSize;
            var $scale = $inputNumeric[0].hasAttribute("data-scale") ? parseInt($inputNumeric[0].attributes.getNamedItem("data-scale").value) : 0;

            // Armamos el tamaño de enteros
            for (var i = 1; i <= $size; i++) {
                sizeMask.mask = sizeMask.mask + (i == 1 ? '0' : '9');
                sizeMask.placeholder = sizeMask.placeholder + '_';
            }

            // Agregamos la escala
            if ($scale !== 0) {
                sizeMask.mask = sizeMask.mask + '.';
                sizeMask.placeholder = sizeMask.placeholder + '.';

                for (var i2 = 1; i2 <= $scale; i2++) {
                    sizeMask.mask = sizeMask.mask + '0';
                    sizeMask.placeholder = sizeMask.placeholder + '_';
                }
            }

            $inputNumeric.mask(sizeMask.mask, { placeholder: sizeMask.placeholder });
        });
    }

}


function InitializeInputMaskInput() {

    if ($(".isMaskedInput").length > 0) {
        $(".isMaskedInput").each(function () {
            var inputMask = { mask: '', placeholder: '' };
            var $inputMasked = $(this);

            inputMask.mask = $inputMasked[0].hasAttribute("data-mask") ? $inputMasked[0].attributes.getNamedItem("data-mask").value : null;
            inputMask.placeholder = $inputMasked[0].hasAttribute("data-place-holder") ? $inputMasked[0].attributes.getNamedItem("data-place-holder").value : null;

            if (inputMask.mask !== null && inputMask.mask !== undefined) {
                // Si no existe un place holder, lo generalizamos basandonos en la mascara
                if (inputMask.placeholder === null || inputMask.placeholder === undefined) {
                    inputMask.placeholder = '';
                    for (var i = 1; i <= inputMask.mask.length; i++) {
                        inputMask.placeholder = inputMask.placeholder + '_';
                    }
                }

                // Iniciamos la mascara
                $inputMasked.mask(inputMask.mask, { placeholder: inputMask.placeholder });
            }
        });
    }
}

/// Esta función inicializa todas las máscaras aplicadas a los input que se encuentran en el DOM
function InitializeInputMask() {

    InitializeInputMaskNumeric();
    InitializeInputMaskInput();
 
}

/// Esta función inicializa todos los select-picker que se encuentran en el DOM
function InitializeSelectPicker() {
    if ($(".isSelectPicker").length > 0) {
        $(".isSelectPicker").each(function () {
            var $selectPicker = $(this);
            var selectPickerSetup = {
                liveSearch: true,
                liveSearchStyle: 'contains',
                showTick: true,
                liveSearchPlaceholder: 'Ingrese el texto que desea buscar',
                noneSelectedText: "- Seleccione Uno-",
                noneResultsText: "Sin resultados encontrados {0}",
                selectedTextFormat: "count"
            };

            if (!(/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent))) {
                $selectPicker.selectpicker(selectPickerSetup);
            }            
        });
    }
}

/// Esta función muestra el loader en la página
function ShowWaitingAnimation() {
    var message = 'Por favor, espere un momento.';
    $.blockUI({ message: '<div class="waitingMessageContainer><div class="imgContainer"></div><div class="txtContainer"><img src="' + getDomainIMSA() + '/images/working.gif" /> ' + message + '</div></div>' });
}

/// Esta funcion oculta el loader en la pagina
function HideWaitingAnimation() {
    $.unblockUI();
}

/// Esta función muestra un confirmation box o mensaje de confirmación
var continueEvent = false;
function ConfirmationDialog(senderObj, message) {
    var retValue = true;

    if (!continueEvent) {
        if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
            var r = confirm(message);
            if (r) {
                retValue = true;
                continueEvent = true;
                ShowWaitingAnimation();
                //senderObj.click(); no es necesario para el dialogo sincrono
            } else {
                retValue = false;
                continueEvent = false;
                $.unblockUI();
            }
        } else {
            event.preventDefault();
            swal({
                title: "Confirmación",
                text: message,
                icon: "info",
                buttons: {
                    cancel: {
                        text: "No",
                        visible: true,
                        className: "",
                        closeModal: true,
                    },
                    confirm: {
                        text: "Si",                        
                        visible: true,
                        className: ""
                              }
                },
                dangerMode: true,
            }).then((isConfirm) => {
                if (isConfirm) {
                    continueEvent = true;
                    ShowWaitingAnimation();
                    senderObj.click();
                } else {
                    retValue = false;
                    $.unblockUI();
                    return false;
                }
            });                
            retValue = false;
        }
    } else {
        continueEvent = false;
        retValue = true;
    }
    return retValue;
}
/**
 * Muestra un cuadro de dialogo, utilizando la libreria sweet alert
 * @param {any} objMessage El modelo del objeto para poder dibujar el cuadro de dialogo
 */
function ShowDialog(objMessage) {
    return swal({
        title: objMessage.TituloMensaje,
        text: objMessage.TextoMensaje,
        icon: objMessage.TipoMensaje
    });
}

/**
 * Muestra un cuadro de dialogo de advertencia
 * @param {any} message El mensaje que será mostrado en pantalla
 */
function ShowWarningDialog(message) {
    var objMessage = {
        TituloMensaje: '¡Advertencia!',
        TipoMensaje: 'warning',
        TextoMensaje: message
    };
    return ShowDialog(objMessage);
}

/**
 * Muestra un cuadro de dialogo de error
 * @param {any} message El mensaje que será mostrado en pantalla
 */
function ShowErrorDialog(message) {
    var objMessage = {
        TituloMensaje: '¡Error!',
        TipoMensaje: 'error',
        TextoMensaje: message
    };
    return ShowDialog(objMessage);
}

/**
 * Muestra un cuadro de dialogo de exito o finalizacion
 * @param {any} message El mensaje que será mostrado en pantalla
 */
function ShowSuccessDialog(message) {
    var objMessage = {
        TituloMensaje: '¡Correcto!',
        TipoMensaje: 'success',
        TextoMensaje: message
    };
    return ShowDialog(objMessage);
}

/**
 * Valida si una fecha en formato string DD/MM/YYYY es una fecha válida
 * @param {any} dateString La fecha en formato DD/MM/YYYY
 */
function isValidDate(dateString) {
    // revisar el patrón
    if (!/^\d{1,2}\/\d{1,2}\/\d{4}$/.test(dateString))
        return false;

    // convertir los numeros a enteros
    var parts = dateString.split("/");
    var day = parseInt(parts[0], 10);
    var month = parseInt(parts[1], 10);
    var year = parseInt(parts[2], 10);

    // Revisar los rangos de año y mes
    if ((year < 1000) || (year > 3000) || (month == 0) || (month > 12))
        return false;

    var monthLength = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

    // Ajustar para los años bisiestos
    if (year % 400 == 0 || (year % 100 != 0 && year % 4 == 0))
        monthLength[1] = 29;

    // Revisar el rango del dia
    return day > 0 && day <= monthLength[month - 1];
}

function initAjaxBackgroundNotification() {
    // Notificacion al iniciar y finalizar una peticion ajax
    $(document).ajaxStart(function () {
        ShowWaitingAnimation();
    }).ajaxStop(function () {
        HideWaitingAnimation();
    });
}