using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Negocio.Modelos;

namespace SAC.Models
{
    public class ConsultaIvaVentaModelView
    {

        public string Abreviatura { get; set; }
        public int CodigoAfip { get; set; }
        public int PuntoVenta { get; set; }
        public int NumeroFactura { get; set; }
        public DateTime Fecha { get; set; }
        public string Nombre { get; set; }
        public decimal Neto { get; set; }
        public decimal Gasto { get; set; }
        public decimal Iva { get; set; }
        public double Isib { get; set; }
        public decimal Total { get; set; }



        public List<ConsultaIvaVentaModelView> ListaConsultaIva { get; set; }

   

    }
}