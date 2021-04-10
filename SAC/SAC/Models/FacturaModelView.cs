using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAC.Models
{
    public class FacturaModelView
    {
        //public FacturaModelView()
        //{
        //    Cotizacion = 1;
        //}

        [Display(Name = "Id Cliente")]
        public int IdCliente { get; set; }

        [Display(Name = "Tipo Comprobante")]
        public int IdTipoComprobante { get; set; }

        [Display(Name = "Punto Venta ")]
        [DisplayFormat(DataFormatString = "{0:0000}", ApplyFormatInEditMode = true)]
        public int IdPuntoVenta { get; set; }

        [Display(Name = "Número")]
        [DisplayFormat(DataFormatString = "{0:00000000}", ApplyFormatInEditMode = true)]
        public int NumeroFactura { get; set; }
        public DateTime Fecha { get; set; }
        public int Cuit { get; set; }

        public int IdDireccionCliente { get; set; }
        public int IdTipoPago { get; set; }
        public int IdTipoFactura { get; set; }
        public int IdTipoIdioma { get; set; }
        public int idDepartamento { get; set; }
        public int idTipoMoneda { get; set; }
        public int IdCuentaBancaria { get; set; }
        public int idTipoComprobanteSeleccionado { get; set; }

        public bool FacturaManual { get; set; }
        public int nroCarpera { get; set; }
        public int nroCarperaFinal { get; set; }

        public int idTipoIva { get; set; }
        public bool mipyme { get; set; }

        public int IdArticulo { get; set; }

        public string ORef { get; set; }
        public string YREf { get; set; }

        public string AplicaNC { get; set; }

        public string Atencion { get; set; }

        public decimal Cotizacion { get; set; }
        public string DireccionCompuesta { get; set; }
        public string NombreComp { get; set; }
        public string PaisComp { get; set; }
        [DataType(DataType.MultilineText)]
        public string EncabezadoFact { get; set; }
        
        //List<DetalleFacturacion> DetalleFacturacion { get; set; }

        public decimal TotalFactura { get; set; }

        public string Nota { get; set; }
       

        //drops
        public List<SelectListItem> TipoMonedas { get; set; }

        public List<SelectListItem> ClienteDirecciones { get; set; }

        public List<SelectListItem> SelectPuntoVenta { get; set; }

        public List<SelectListItem> Departamentos { get; set; }

        public List<SelectListItem> TipoComprobante { get; set; }
        public List<SelectListItem> FormaPago { get; set; }
        public List<SelectListItem> CuentaBancaria { get; set; }

        public List<SelectListItem> TipoIdioma { get; set; }

        public string hdnArticulos { get; set; }

        //relaciones

        public ClienteModelView Cliente { get; set; }

    }
}