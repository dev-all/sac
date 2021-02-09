using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace SAC.Helpers
{
    public class Utilidades
    {


        public String[] GetMeses()
        {
            System.Globalization.CultureInfo cultura = new System.Globalization.CultureInfo("es-ar");
            var qry = from m in cultura.DateTimeFormat.MonthNames select cultura.TextInfo.ToTitleCase(m);

            //foreach (var mes in qry)
            //{
            //    Console.WriteLine(mes);
            //}

            return qry.ToArray();
        }

        //private void CargarMes()
        //{
        //    List<Meses> ListaMes = new List<Meses>()
        //    {
        //        new Meses(){ Id = "0", Descripcion = "Selecionar" },
        //        new Meses(){ Id = "01", Descripcion = "Enero" },
        //        new Meses(){ Id = "02", Descripcion = "Febrero" },
        //        new Meses(){ Id = "03", Descripcion = "Marzo" },
        //        new Meses(){ Id = "04", Descripcion = "Abril" },
        //        new Meses(){ Id = "05", Descripcion = "Mayo" },
        //        new Meses(){ Id = "06", Descripcion = "Junio" },
        //        new Meses(){ Id = "07", Descripcion = "Julio" },
        //        new Meses(){ Id = "08", Descripcion = "Agosto" },
        //        new Meses(){ Id = "09", Descripcion = "Septiembre" },
        //        new Meses(){ Id = "10", Descripcion = "Octubre" },
        //        new Meses(){ Id = "11", Descripcion = "Noviembre" },
        //        new Meses(){ Id = "12", Descripcion = "Diciembre" }};
        //    StringBuilder sb = new StringBuilder();
        //    foreach (var type in ListaMes)
        //    {
        //        sb.Append("<option value='" + type.Id + "'>" + type.Descripcion + "</option>");
        //    }
        //    ViewBag.ListaMes = sb.ToString();
        //}

        //private void CargarAnio()
        //{
        //    List<Anios> ListaAnio = new List<Anios>()
        //    {
        //        new Anios(){ Id = "0", Descripcion = "Selecionar" },
        //        new Anios(){ Id = "19", Descripcion = "2019" },
        //        new Anios(){ Id = "20", Descripcion = "2020" },
        //        new Anios(){ Id = "21", Descripcion = "2021" },
        //        new Anios(){ Id = "22", Descripcion = "2022" },
        //        new Anios(){ Id = "23", Descripcion = "2023" },
        //        new Anios(){ Id = "24", Descripcion = "2024" }};

        //    StringBuilder sb = new StringBuilder();
        //    foreach (var type in ListaAnio)
        //    {
        //        sb.Append("<option value='" + type.Id + "'>" + type.Descripcion + "</option>");
        //    }
        //    ViewBag.ListaAnio = sb.ToString();
        //}


    }
}