using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SAC.Models
{
    public class CompraIvaModelView
    {
        public int Id { get; set; }
        public int IdFacturaCompra { get; set; }

        [Display(Name = "Neto Gravado")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        [Range(0, 9999999999999999.99)]
        public decimal NetoGravado { get; set; }
        [Display(Name = "Neto No Gravado")]
        [Range(0, 9999999999999999.99)]
        public decimal NetoNoGravado { get; set; }
        [Display(Name = "Sub Total ")]
        [Range(0, 9999999999999999.99)]
        public decimal SubTotal { get; set; }
        [Display(Name = "Total IVA ")]
        [Range(0, 9999999999999999.99)]
        public decimal TotalIva { get; set; }

        [Display(Name = " Total Percepciones")]
        [Range(0, 9999999999999999.99)]
        public decimal TotalPercepciones { get; set; }
        [Display(Name = "Total ")]
        [Range(0, 9999999999999999.99)]
        public decimal Total { get; set; }

        [Range(0, 9999999999999999.99)]
        public decimal Importe25 { get; set; }

        [Range(0, 9999999999999999.99)]
        public decimal Importe5 { get; set; }

        [Range(0, 9999999999999999.99)]
        public decimal Importe105 { get; set; }

        [Range(0, 9999999999999999.99)]
        public decimal Importe21 { get; set; }

        [Range(0, 9999999999999999.99)]
        public decimal Importe27 { get; set; }

        [Range(0, 9999999999999999.99)]
        public decimal Iva25 { get; set; }

        [Range(0, 9999999999999999.99)]
        public decimal Iva5 { get; set; }

        [Range(0, 9999999999999999.99)]
        public decimal Iva105 { get; set; }

        [Range(0, 9999999999999999.99)]
        public decimal Iva21 { get; set; }

        [Range(0, 9999999999999999.99)]
        public decimal Iva27 { get; set; }

        [Range(0, 9999999999999999.99)]
        public decimal PercepcionIva { get; set; }

        [Range(0, 9999999999999999.99)]
        public decimal PercepcionIB { get; set; }

        [Range(0, 9999999999999999.99)]
        public decimal PercepcionProvincia { get; set; }

        [Range(0, 9999999999999999.99)]
        public decimal PercepcionImporteIva { get; set; }

        [Range(0, 9999999999999999.99)]
        public decimal PercepcionImporteIB { get; set; }

        [Range(0, 9999999999999999.99)]
        public decimal PercepcionImporteProvincia { get; set; }

        [Range(0, 9999999999999999.99)]
        public decimal OtrosImpuestos { get; set; }

        [Range(0, 9999999999999999.99)]
        public decimal ISIB { get; set; }

        public bool Activo { get; set; }
        public int Idusuario { get; set; }
        public System.DateTime UltimaModificacion { get; set; }



    }
}