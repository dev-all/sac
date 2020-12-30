using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Datos.ModeloDeDatos;

namespace SAC.Models
{
    public class ChequeModelView
    {

        public int Id { get; set; }

        public int NumeroCheque { get; set; }
        public int IdBanco { get; set; }
        public DateTime Fecha { get; set; }
        public string DiaClearing { get; set; }

        public decimal Importe { get; set; }

        public int IdCliente { get; set; }

        public string Descripcion { get; set; }

        public string NumeroRecibo { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaEgreso { get; set; }
        public string Destino { get; set; }
        public int IdMoneda { get; set; }
        public int IdGrupoCaja { get; set; }
        public string IdFactura { get; set; }

        public string NumeroPago { get; set; }

        public string Registro { get; set; }

        public string Proveedor { get; set; }
        public Nullable<bool> Endosado { get; set; }
        public Nullable<bool> Activo { get; set; }

        public Nullable<int> IdUsuario { get; set; }

        public Nullable<System.DateTime> UltimaModificacion { get; set; }

        public Banco Banco { get; set; }
        public TipoMoneda TipoMoneda { get; set; }

        public ICollection<CompraFacturaPago> CompraFacturaPago { get; set; }
        //propiedad agregada
        public string tipoMonedaDescripcion { get; set; }


    }
}