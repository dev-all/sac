using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.ModeloDeDatos;
namespace Negocio.Modelos
{
    public class CajaGrupoModel

    {

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Concepto { get; set; }
        public Nullable<decimal> NetoTotalP { get; set; }
        public Nullable<decimal> NetoTotalD { get; set; }
        public Nullable<decimal> NetoTotalC { get; set; }
        public Nullable<decimal> NetoTotalT { get; set; }
        public Nullable<decimal> NetoTotalB { get; set; }
        public Nullable<decimal> NetoParcialP { get; set; }
        public Nullable<decimal> NetoParcialD { get; set; }
        public Nullable<decimal> NetoParcialC { get; set; }
        public Nullable<decimal> NetoParcialT { get; set; }
        public Nullable<decimal> NParcialB { get; set; }
        public string Ltraa { get; set; }
        public string Ltram { get; set; }
        public Nullable<int> IdImputacion { get; set; }
        public string Especial { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }

    }

}
