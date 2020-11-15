using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.ModeloDeDatos;
namespace Negocio.Modelos
{
  public class VacanteModel
    {
        public int id { get; set; }
        public int idTipoPostulacion { get; set; }
        public int idEspecialidad { get; set; }
        public Nullable<int> idUnidad { get; set; }
        public virtual Especialidad Especialidad { get; set; }
        public virtual TipoPostulacion TipoPostulacion { get; set; }
    }
}
