
$(function () {





    // Switchery
    //var elems = Array.prototype.slice.call(document.querySelectorAll('.js-switch'));
    //$('.js-switch').each(function () {
    //    new Switchery($(this)[0], $(this).data());
    //});

    //var changeCheckbox = document.querySelector('.js-check-activo')
    //    , changeField = document.querySelector('.js-check-activo-field');
    //changeCheckbox.onchange = function () {
    //    $("input[type='hidden'][name='tipo']").val(changeCheckbox.checked);
    //};


$('#example23').DataTable({
    "language": { "url": "../Content/assets/plugins/datatables/es.txt" },
    "order": [[2, 'asc']],
    'paging': false,
    'lengthChange': false,
    'searching': false,
    'ordering': false,
    'info': false,
    'autoWidth': true,
    'scrollY': '200px',
    'scrollCollapse': true,
    'dom': 'Bfrtip',
    'buttons': []
});

    //$("#birthday").datepicker({ dateFormat: "dd/mm/yyyy", 'setDate': new Date())}).mask("99/99/9999");
    //$.validator.addMethod('date',
    //    function (value, element, params) {
    //        if (this.optional(element)) {
    //            return true;
    //        }
    //        var ok = true;
    //        try {
    //            $.datepicker.parseDate('dd/mm/yyyy', value);
    //        }
    //        catch (err) {
    //            ok = false;
    //        }
    //        return ok;
    //    });

$('.fechadatepicker').datepicker({
    language: 'es',
    autoclose: true,
    format: 'dd/mm/yyyy',
    todayHighlight: true
}).datepicker('setDate', new Date());

});





function getTotales()
{
    setNeto();
    setTotalIva();
    setTotalPercepciones();
    setTotal();
}
function setTotal() {
    $('#CompraIva_Total').val(
        getNro($("#CompraIva_SubTotal").val()) +
        getNro($('#CompraIva_TotalIva').val()) +
        getNro($('#CompraIva_TotalPercepciones').val()) 
    );

}
function setNeto() {
 
    $('#CompraIva_NetoGravado').val(
        getNro($("#CompraIva_Importe25").val()) +
        getNro($('#CompraIva_Importe5').val()) +
        getNro($('#CompraIva_Importe105').val()) +
        getNro($('#CompraIva_Importe21').val()) +
        getNro($("#CompraIva_Importe27").val())
    );

    $('#CompraIva_SubTotal').val(
        getNro($('#CompraIva_NetoGravado').val()) +
        getNro($('#CompraIva_NetoNoGravado').val()) 
        );

}

function setTotalIva() {
    $('#CompraIva_TotalIva').val(
        getNro($("#CompraIva_Iva25").val()) +
        getNro($('#CompraIva_Iva5').val()) +
        getNro($('#CompraIva_Iva105').val()) +
        getNro($('#CompraIva_Iva21').val()) +
        getNro($("#CompraIva_Iva27").val())
    );

}

function setTotalPercepciones() {
    $('#CompraIva_TotalPercepciones').val(
        getNro($("#CompraIva_PercepcionImporteIva").val()) +
        getNro($('#CompraIva_PercepcionImporteIB').val()) +
        getNro($('#CompraIva_PercepcionImporteProvincia').val()) +
        getNro($('#CompraIva_OtrosImpuestos').val()) 
    );

}

function getNro(x)
{
    x = parseFloat(x);
    return (x == null || isNaN(x) || x == undefined || x == "") ? 0 : x;
}





if ($("#IdProveedor").val() > 0) {
  getProvedor($("#IdProveedor").val());
 }
  




