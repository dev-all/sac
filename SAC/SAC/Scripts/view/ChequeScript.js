$(function () {

   
    $('#NombreBanco').autocomplete({  
    source: function (request, response) {
        $.getJSON("/Banco/GetListBancoJson/", request, function (data) {
            response($.map(data, function (item) {
            
                return {
                    id: item.id,
                    value: item.label
                }
               
            }))
        })
    },
    minLength: 3,
        select: function (event, ui)
        {
       // getProvedor(ui.item.id)
                $("#IdBanco").val(ui.item.id); 
               // $("#Proveedor_Id").val(ui.item.id); 
        
        this.value = ui.item.value;
        return false;
    }

    });


    $('#NombreCliente').autocomplete({
        source: function (request, response) {
            $.getJSON("/Banco/GetListClienteJson/", request, function (data) {
                response($.map(data, function (item) {

                    return {
                        id: item.id,
                        value: item.label
                    }

                }))
            })
        },
        minLength: 3,
        select: function (event, ui) {
            // getProvedor(ui.item.id)
            $("#CidCliente").val(ui.item.id);
           

            this.value = ui.item.value;
            return false;
        }

    });

});