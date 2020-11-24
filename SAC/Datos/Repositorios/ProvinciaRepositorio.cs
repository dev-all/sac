using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using Z.EntityFramework.Plus;

namespace Datos.Repositorios
{
    public class ProvinciaRepositorio : RepositorioBase<Provincia>
    {
       private SAC_Entities context;

        public ProvinciaRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }


        public List<Provincia> GetAllProvincia(int idPais)
        {
            List<Provincia> listaProvincia = context.Provincia.Where(p => p.Activo == true && p.IdPais == idPais).ToList();
            return listaProvincia;
        }

        public List<Provincia> GetAllProvincia()
        {

            List<Provincia> listaProvincia = context.Provincia.Where(p => p.Activo == true ).ToList();
            return listaProvincia;

            //es para que no cargue todo por defecto
            //context.Configuration.LazyLoadingEnabled = false;
            //var listaProvincia = context.Provincia
            //                            .Include(x => x.Pais)                                        
            //                            .Where(p => p.Activo == true) 
            //                            .ToList();
            //return listaProvincia;
        }


        public Provincia InsertarProvincia(Provincia provincia)
        {
            return Insertar(provincia);
        }

        public Provincia ActualizarProvincia(Provincia model)
        {
            Provincia oProvincia= ObtenerporId(model.Id);
            oProvincia.Id = model.Id;
            oProvincia.Nombre = model.Nombre;
            oProvincia.CodigoAfip = model.CodigoAfip;
            oProvincia.CodigoNumero = model.CodigoNumero;
            oProvincia.IdPais = model.IdPais;
            oProvincia.Activo = model.Activo;
            oProvincia.IdUsuario = model.IdUsuario;
            oProvincia.UltimaModificacion = model.UltimaModificacion;
            
            context.SaveChanges();
            return oProvincia;
        }

        public int EliminarPais(int idProvincia)
        {
            Provincia paisExistente = ObtenerporId(idProvincia);
            paisExistente.Activo = false;
            context.SaveChanges();
            return 1;

            //var oPais = context.Pais.Where(r => r.Id == idPais).FirstOrDefault();
            //context.Pais.Remove(oPais);
            //var retorno = context.SaveChanges();
            //return retorno;
        }

        public Provincia ObtenerIdPais(int idPais)
        {
            var provincia = context.Provincia.FirstOrDefault(p => p.IdPais == idPais);
            return context.Provincia.FirstOrDefault(l => l.IdPais == idPais);
        }


        public Provincia ObtenerporId(int idprovincia)
        {
            var Provincia = context.Provincia.Where(p => p.Id == idprovincia && p.Activo==true).FirstOrDefault();
            return Provincia;
            //// var provincia = context.Provincia.FirstOrDefault(p => p.Id == idprovincia);
            // return context.Provincia.FirstOrDefault(l => l.Id == idprovincia && l.Activo == true);
        }


        public Provincia ObtenerporNOMBRE(string nombre)
        {

            var provincia = context.Provincia.FirstOrDefault(p => p.Nombre == nombre);
            return context.Provincia.FirstOrDefault(l => l.Nombre == nombre);
        }

        public Provincia ObtenerporNOMBRE(string nombre, string codigo)
        {
           // var provincia = context.Provincia.FirstOrDefault(p => p.Nombre == nombre );
            return context.Provincia.FirstOrDefault(l => l.Nombre == nombre && l.Codigo == codigo);
        }

        /// <summary>
        /// verifica que el nombre, codigo ingresado no existan para otro id que no sea el enviado
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="codigo"></param>
        /// <param name="idProvincia"></param>
        /// <returns></returns>
        public Provincia ObtenerporNOMBRE(string nombre, string codigo, int idProvincia)
        {
            // var provincia = context.Provincia.FirstOrDefault(p => p.Nombre == nombre );
            return context.Provincia.FirstOrDefault(l => l.Nombre == nombre && l.Codigo == codigo && l.Id !=idProvincia);
        }

        public Provincia ObtenerporCodigoAfip(int codigoafip)
        {
            var provincia = context.Provincia.FirstOrDefault(p => p.CodigoAfip == codigoafip);
            return context.Provincia.FirstOrDefault(l => l.CodigoAfip == codigoafip);
        }




    }
}