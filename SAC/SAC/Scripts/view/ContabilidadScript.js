
$(document).ready(function ()
{
    $("#Anio").on('change', function () {
        $("#Anio option:selected").each(function ()
        {
            anio = $(this).val();

            mes = $("#Mes").val();

           // alert("Año" + anio + "Mes" + mes);

            var elegido = anio + mes

            if (elegido.length == 4) {

                $('.btn').prop('disabled', false);

                $("#Periodo").val(elegido);
            }

            else {

                $("#Periodo").val("Selecciones Año y Mes");
                $('.btn').prop('disabled', true);
             }

           


        });
    });


    $("#Mes").on('change', function () {
        $("#Mes option:selected").each(function ()
        {
           
            anio = $("#Anio").val();

            mes = $(this).val();


            var elegido = anio + mes

           
            if (elegido.length == 4)
            {
                              
                $('.btn').prop('disabled', false);

                $("#Periodo").val(elegido);
            }

            else {

                $("#Periodo").val("Selecciones Año y Mes");
                $('.btn').prop('disabled', true);

            }



        });
    });

});