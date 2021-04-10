using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
   public class FacturaVentaModel
    {
        public int Id { get; set; }
        public int IdTipoComprobante { get; set; }
        public string PuntoVenta { get; set; }
        public int NumeroFactura { get; set; }
        public string Codigo { get; set; }
        public decimal TotalD { get; set; }
        public decimal Total { get; set; }
        public decimal Parcial { get; set; }
        public decimal Saldo { get; set; }
        public System.DateTime Fecha { get; set; }
        public System.DateTime Vencimiento { get; set; }
        public string Tipo { get; set; }
        public string Baja { get; set; }
        public string Impre { get; set; }
        public Nullable<System.DateTime> FPago { get; set; }
        public string TipoIva { get; set; }
        public string Concepto { get; set; }
        public string Marca { get; set; }
        public string Condic { get; set; }
        public int IdProvincia { get; set; }
        public int IdPais { get; set; }
        public string IdImputacion { get; set; }
        public string NumeroPago { get; set; }
        public decimal Cotiza { get; set; }
        public string Moneda { get; set; }
        public string ORef { get; set; }
        public string YRef { get; set; }
        public decimal Gasto { get; set; }
        public string Descuento { get; set; }
        public decimal CotizaP { get; set; }
        public string TipoFac { get; set; }
        public string Diario { get; set; }
        public string Recibo { get; set; }
        public int Periodo { get; set; }
        public string NumeroTra { get; set; }
        public string Anula { get; set; }
        public double Combria { get; set; }
        public double IngBr { get; set; }
        public string Efectivo { get; set; }
        public string Cheque { get; set; }
        public string Debito { get; set; }
        public string Cuenta { get; set; }
        public string Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }


    }
}
