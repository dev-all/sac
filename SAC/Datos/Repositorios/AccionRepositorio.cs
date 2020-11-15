using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace Datos.Repositorios
{
    public class AccionRepositorio : RepositorioBase<Accion>
    {
       private SAC_Entities context;
    
        public AccionRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }


        #region Create...
        /// <summary>
        /// Metodos para Gestion de Acciones
        /// </summary>
        /// <returns></returns>

        public Accion CreateAccion(Accion accion)
        {
           return  Insertar(accion);
        }

        public List<Accion> GetAccion()
        {
            return context.Accion.Where(acc => acc.Activo == true).OrderBy(acc => acc.Controlador).ToList();
        }

        public Accion GetAccionPorId(int id)
        {           
            return context.Accion.Where(acc => acc.IdAccion == id && acc.Activo == true).FirstOrDefault(); 
        }

      
        public Accion ActualizarAccion(Accion AccionParaActualizar)
        {

            Accion Accion = GetAccionPorId(AccionParaActualizar.IdAccion);
            Accion.Controlador = AccionParaActualizar.Controlador ?? Accion.Controlador;
            Accion.Nombre = AccionParaActualizar.Nombre ?? Accion.Nombre;
            Accion.Descripcion = AccionParaActualizar.Descripcion ?? Accion.Descripcion;
            context.SaveChanges();

            return Accion;
        }
        public Accion DeleteAccion(int IdAccion)
        {

            Accion Accion = GetAccionPorId(IdAccion);
            Accion.Activo = false;
            Accion.fechaModificacion = Convert.ToDateTime(DateTime.Now.ToString()); ;
            context.SaveChanges();

            return Accion;
        }

     
        #endregion


    }
}