$('#NombreProveedor').autocomplete({
   
    source: function (request, response) {
        $.getJSON("/Compras/GetListProveedorJson/", request, function (data) {
            response($.map(data, function (item) {
            
                return {
                    id: item.id,
                    value: item.label
                }
                //return {
                //    label: item,
                //    value: item + ""
                //}
            }))
        })
    },
    minLength: 3,

    select: function (event, ui) {
        getProvedor(ui.item.id)
        $("#IdProveedor").val(ui.item.id);      
        this.value = ui.item.value;
        return false;
    }

});

$("#IdTipoComprobante").change(function () {    
    // check class
    alert($(this).val());
    alert($(this).html());
    if ($("#AplicaFactura").hasClass('invisible')) {
        $("#AplicaFactura").removeClass("invisible");
        $("#AplicaFactura").addClass("visible");
    } else {
        $("#AplicaFactura").removeClass("visible");
        $("#AplicaFactura").addClass("invisible");
    }

});


$("#IdTipoComprobante").change(function () {

});

function getProvedor(prov) {
 
    $.getJSON("/Compras/GetProveedorJson",
        { idProveedor: prov },
        function (data) {
            var proveedor = JSON.parse(data);

            $('#ivaProv').html(proveedor.Nombre);
            $('#telProv').html(proveedor.Telefono);
            $('#dirProv').html(proveedor.Direccion);

            $('#IdImputacion').val(proveedor.Direccion);
            
            $("#IdTipoComprobante").empty();
            $("#IdTipoComprobante").append("<option value> Seleccionar ...</option>")
            $.each(proveedor.ListTipoComprobante, function (index, row) {
                $("#IdTipoComprobante").append("<option value='" + row.Id + "'>" + row.Denominacion + "</option>")
            });
        });

}


jQuery('.fechadatepicker').datepicker({
    language: 'es',
    autoclose: true,
    format: 'dd/mm/yyyy',
    todayHighlight: true

}).datepicker('setDate', new Date());







