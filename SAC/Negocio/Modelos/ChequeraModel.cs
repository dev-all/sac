using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
    public class ChequeraModel
    {
        public int Id { get; set; }

        public int NumeroCheque { get; set; }

        public int IdBancoCuenta { get; set; }

        public Nullable<System.DateTime> Fecha { get; set; }

        public decimal Importes { get; set; }

        public string IdProveedor { get; set; }

        public string NumeroRecibo { get; set; }

        public Nullable<System.DateTime> FechaIngreso { get; set; }

        public string FechaEgreso { get; set; }

        public string Destino { get; set; }

        public Nullable<int> NumeroOperacion { get; set; }

        public string IdMoneda { get; set; }

        public string Registro { get; set; }

        public Nullable<bool> Usado { get; set; }

        public Nullable<bool> Activo { get; set; }

        public Nullable<int> IdUsuario { get; set; }

        public Nullable<System.DateTime> UltimaModificacion { get; set; }

        public BancoCuenta BancoCuenta { get; set; }


        public TipoMoneda TipoMoneda { get; set; }

        //prop agregada
       // public string tipoMonedaDescripcion { get; set; }

    }
}
