using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            context.Configuration.LazyLoadingEnabled = false;

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
            context.Configuration.LazyLoadingEnabled = false;
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
            context.Configuration.LazyLoadingEnabled = false;
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
            context.Configuration.LazyLoadingEnabled = false;
            List<Cliente> listaCliente = context.Cliente
              .Include("TipoCliente")
              .Where(p => p.Activo == true && p.Nombre.Contains(strCliente)).ToList();

            return listaCliente;

           
        }

        public List<Cliente> GetClientePorCodigo(string strCodigo)
        {
            context.Configuration.LazyLoadingEnabled = false;
            List<Cliente> listaCliente = context.Cliente
              .Include("TipoCliente")
              .Where(p => p.Activo == true && p.Codigo.Contains(strCodigo)).ToList();

            return listaCliente;
        }

        public void ActualizarPresupuesto(Cliente model)
        {
            Cliente cliente = GetClientePorId(model.Id);
            cliente.IdGrupoPresupuesto = model.IdGrupoPresupuesto;
            cliente.Activo = true;
            cliente.IdUsuario = model.IdUsuario;
            cliente.UltimaModificacion = model.UltimaModificacion;
            context.SaveChanges();
        }

        public List<Cliente> GetClientePorIdNombre(int idTipoCliente, string strCliente)
        {
            context.Configuration.LazyLoadingEnabled = false;
            List<Cliente> p = (from c in context.Cliente
                               where c.Activo == true && c.Nombre == strCliente && c.IdTipoCliente == idTipoCliente
                               select c).ToList();
            return p;

        }

        public List<FactVenta> GetFacturasImpagasClientePorId(int idCliente, DateTime fecha)
        {
            return context.FactVenta.Where(f => f.IdCliente == idCliente
                                                && f.Fecha <= fecha
                                                && f.Activo == true
                                                && f.TipoComprobanteVenta.Denominacion.Contains("FACTURA")
                                                && f.NumeroCobro == 0).ToList();
        }


        public List<FactVenta> GetFacturasCobradasClientePorId(int idCliente, DateTime fecha)
        {
            return context.FactVenta.Where(f => f.IdCliente == idCliente
                                                && f.Fecha <= fecha
                                                && f.Activo == true
                                                && f.TipoComprobanteVenta.Denominacion.Contains("FACTURA")
                                                && f.NumeroCobro > 0).ToList();
        }



        #endregion



        #region "Reportes"

        // 1 Cuenta Corriente Detalle


        public List<FactVenta> GetCtaCteDetalle(int IdCliente,DateTime fecha)
        {

            context.Configuration.LazyLoadingEnabled = false;
            List<FactVenta> listaCteCteDetalle = context.FactVenta
                                               .Include("TipoComprobanteVenta")
                                               .Where(f => f.IdCliente == IdCliente &&
                                                        f.Activo == true &&                                                        
                                                        f.Fecha <= fecha
                                                        ).ToList();
           
            return listaCteCteDetalle;


        }


        // 2 Cuenta Corriente Resumen


        public List<CteCteClienteResumen> GetCtaCteResumen(DateTime fechaHasta)
        {
            var r=  context.FactVenta
                         .Include("Cliente")
                         .Where(p => p.Activo == true &&
                         p.Fecha.Month < fechaHasta.Month && p.Fecha.Year < fechaHasta.Year)
                         .GroupBy(c => new { c.Cliente.Codigo, c.Cliente.Nombre })
                         .Select(c => new CteCteClienteResumen ()
                         {
                             Codigo = c.Key.Codigo,
                             Nombre = c.Key.Nombre,
                             TotalPesos = c.Sum(x => x.Total),
                             TotalDolares = c.Sum(x => x.TotalDolares),
                             FechaUltimoMov = c.Max(x => x.Fecha),
                         }).ToList();

            return r;
            
        }





        //3  Registro de Ventas Mensuales

        public List<ConsultaIvaVenta> GetIvaVentas(string Periodo)
        {

                context.Configuration.LazyLoadingEnabled = false;


            int periodo = Convert.ToInt32(Periodo);


            var query = from a in context.FactVenta
                        join s in context.IvaVenta on a.NumeroFactura equals s.NumeroFactura
                        where a.Activo == true  && a.Periodo == periodo

                        select new ConsultaIvaVenta
                        {

                            Abreviatura = a.TipoComprobanteVenta.Abreviatura,
                            CodigoAfip = a.TipoComprobanteVenta.CodigoAfip,
                            PuntoVenta = a.TipoComprobanteVenta.PuntoVenta,
                            NumeroFactura = a.NumeroFactura,
                            Fecha = a.Fecha,
                            Nombre = a.Cliente.Nombre,
                            Neto = s.Neto,
                            Gasto = s.Gasto,
                            Iva = s.Iva,
                            Isib = s.Isib,
                            Total = s.Total
                           

                        };


            return query.ToList();


         





        }


        //4  Registro de Ventas Totales

        public ConsultaIvaTotales GetIvaTotales(string Periodo)
        {

            ConsultaIvaTotales lista = new ConsultaIvaTotales();

            int periodo = Convert.ToInt32(Periodo);

            try
            {
                SqlParameter param1 = new SqlParameter("@Periodo", periodo);
                lista = context.Database.SqlQuery<ConsultaIvaTotales>("GetIvaVentasTotalPesos @Periodo", param1).First();
            }
            catch (Exception ex)
            {
                lista = null;
            }

            return lista;



        }








        #endregion



      


    }
}
