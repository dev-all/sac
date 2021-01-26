using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Datos.Repositorios
{
 public class TarjetaOperacionRepositorio : RepositorioBase<TarjetaOperacion>
    {
        private SAC_Entities context;

        public TarjetaOperacionRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

        public List<TarjetaOperacion> GetAllTarjetasOperacion()
        {

            return context.TarjetaOperacion.ToList();

        }

        public List<TarjetaOperacion> GetTarjetasOperacionGastos(int idTipoTarjeta, DateTime cfechadesde, DateTime cfechahasta)
        {


            List<TarjetaOperacion> listaCheque = context.TarjetaOperacion
                                       .Include("Tarjetas")
                                       .Where(p => p.Activo == true && p.IdTarjeta == idTipoTarjeta && p.UltimaModificacion >= cfechadesde && p.UltimaModificacion <= cfechahasta)
                                       .OrderBy(p => p.Id)
                                       .ToList();

            return listaCheque;



        }

        public List<TarjetaOperacion> GetTarjetasOperacionGastos(int idTipoTarjeta)
        {


            List<TarjetaOperacion> listaCheque = context.TarjetaOperacion
                                       .Include("Tarjetas")
                                       .Where(p => p.Activo == true && p.IdTarjeta == idTipoTarjeta)
                                       .OrderBy(p => p.Id)
                                       .ToList();

            return listaCheque;



        }



    }
}
