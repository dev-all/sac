




jQuery('.fechadatepicker').datepicker({
    language: 'es',
    autoclose: true,
    format: 'dd/mm/yyyy',
    todayHighlight: true

}).datepicker('setDate', new Date());





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
  




