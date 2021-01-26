using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Datos.Repositorios
{
  public class ClienteRepositorio : RepositorioBase<Cliente>
    {
        private SAC_Entities context;

        public ClienteRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

         #region "Metodos de Actualizacion"

        /// <summary>
        ///  Agregar Cliente
        /// </summary>

        public Cliente Agregar(Cliente oCliente)

        {

            try
            {

                return Insertar(oCliente);
            }


            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }

            }



            catch (Exception ex)
            {
               
                        System.Diagnostics.Debug.WriteLine("Property: " + ex.InnerException + " Error: " + ex.Message);
               

            }

            return Insertar(oCliente);

        }

        /// <summary>
        ///  Actualizar Cliente
        /// </summary>
        public Cliente ActualizarCliente(Cliente oCliente)
        {
            Cliente nCliente = GetClientePorId (oCliente.Id);
            nCliente.Id = oCliente.Id;
            nCliente.Codigo = oCliente.Codigo;
            nCliente.Nombre = oCliente.Nombre;
            nCliente.IdTipoiva = oCliente.IdTipoiva;
            nCliente.Cuit = oCliente.Cuit;

            nCliente.DiasFactura = oCliente.DiasFactura;
            nCliente.IdImputacion = oCliente.IdImputacion;
            nCliente.Observaciones = oCliente.Observaciones;


            nCliente.Email = oCliente.Email;
            nCliente.IdPieNota = oCliente.IdPieNota;
            nCliente.IdIdioma = oCliente.IdIdioma;

            nCliente.IdTipoCliente = oCliente.IdTipoCliente;
            nCliente.Visible = true;

            nCliente.IdNotaPieB = oCliente.IdNotaPieB;
         

            nCliente.IdTipoMoneda = oCliente.IdTipoMoneda;
            nCliente.IdGrupoPresupuesto = oCliente.IdGrupoPresupuesto;
            nCliente.MiPyme = oCliente.MiPyme;

            nCliente.Activo = oCliente.Activo;
            nCliente.IdUsuario = oCliente.IdUsuario;
            nCliente.UltimaModificacion = oCliente.UltimaModificacion;


            try
            {

                context.SaveChanges();


            }



            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }

            }

            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine("Property: " + ex.InnerException + " Error: " + ex.Message);


            }







            return nCliente;
        }

       

        /// <summary>
        ///  Ocultar un Cliente
        /// </summary>
        public Cliente OcultarCliente(Cliente oCliente)
        {
            Cliente nCliente = GetClientePorId(oCliente.Id);

            nCliente.Visible =true;

            nCliente.IdUsuario = oCliente.IdUsuario;
            nCliente.UltimaModificacion = oCliente.UltimaModificacion;
            context.SaveChanges();
            return nCliente;
        }




        public object HabilitarCliente(int IdCliente)
        {
            Cliente OCliente = GetClientePorId(IdCliente);
            OCliente.Activo = true;

            OCliente.UltimaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
            context.SaveChanges();
            return 1;
        }

        public object BloquearCliente(int IdCliente)
        {

            Cliente OCliente = GetClientePorId(IdCliente);
            OCliente.Activo = false;

            OCliente.UltimaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
            context.SaveChanges();
            return 1;

        }

        public int DeleteCliente(int IdCliente)
        {
            Cliente OCliente = GetClientePorId(IdCliente);
            OCliente.Activo = false;

            OCliente.UltimaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
            context.SaveChanges();
            return 1;

        }

        #endregion


        #region "Metodos de Lectura"

        /// <summary>
        ///  Listado completo de todos lo Clientes
        /// </summary>

        public List<Cliente> GetAllCliente()
        {
            List<Cliente> listaCliente = context.Cliente
                .Include("TipoCliente")
                .Where(p => p.Activo == true).ToList();
       
            return listaCliente;



          

        }


       


        /// <summary>
        ///  Obtener un Cliente por el Id
        /// </summary>
        public Cliente GetClientePorId(int IdCliente)
        {
            return context.Cliente
                .Include("TipoCliente")
                .Where(p => p.Id == IdCliente).First();
        }


        /// <summary>
        ///  Obtener Listado de Cliente Por Nombre y tipo de cliente
        /// </summary>

     


        /// <summary>
        ///  Obtener Listado de Cliente Por idTipoCliente
        /// </summary>

        public List<Cliente> GetClientePorTipoCliente(int idTipoCliente)
        {


            List<Cliente> listaCliente = context.Cliente
               .Include("TipoCliente")
               .Where(p => p.Activo == true && p.IdTipoCliente == idTipoCliente).ToList();

            return listaCliente;




            //List<Cliente> p = (from c in context.Cliente
            //                   where c.Activo == true &&  c.IdTipoCliente == idTipoCliente
            //                   select c).ToList();
            //return p;

        }

      


        /// <summary>
        ///  obtener el Cliente por el Nombre o Codigo
        /// </summary>
        public List<Cliente> GetClientePorNombre(string strCliente)





        {

            List<Cliente> listaCliente = context.Cliente
              .Include("TipoCliente")
              .Where(p => p.Activo == true && p.Nombre.Contains(strCliente)).ToList();

            return listaCliente;

           
        }


        public List<Cliente> GetClientePorIdNombre(int idTipoCliente, string strCliente)
        {


            List<Cliente> p = (from c in context.Cliente
                               where c.Activo == true && c.Nombre == strCliente && c.IdTipoCliente == idTipoCliente
                               select c).ToList();
            return p;

        }

        

    #endregion


}
}
