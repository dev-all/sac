using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
  public class BancoCuentaBancariaModel
    {

        public int Id { get; set; }

        public int NumeroOperacion { get; set; }

        public int IdBancoCuenta { get; set; }

        public string CuentaDescripcion { get; set; }

        public double Fecha { get; set; }

        public Nullable<System.DateTime> FechaEfectiva { get; set; }

        public string DiaClearing { get; set; }

        public decimal Importe { get; set; }

        public string IdCliente { get; set; }

        public string Conciliacion { get; set; }

        public double FechaIngreso { get; set; }

        public string IdImputacion { get; set; }

        public Nullable<bool> Activo { get; set; }

        public Nullable<int> IdUsuario { get; set; }

        public Nullable<System.DateTime> UltimaModificacion { get; set; }



        public virtual BancoCuenta BancoCuenta { get; set; }


    }
}
