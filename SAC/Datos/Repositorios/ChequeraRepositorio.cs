using System;
using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Datos.Repositorios
{
   public class ChequeraRepositorio : RepositorioBase<Chequera>
    {
        private SAC_Entities context;

        public ChequeraRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

        public List<Chequera> GetAllChequera()
        {
            List<Chequera> listaChequera = new List<Chequera>();
            listaChequera = context.Chequera.Where(p =>p.IdProveedor == null && p.NumeroRecibo ==null).ToList();
            listaChequera = listaChequera.OrderBy(p => p.NumeroCheque).ToList();
            return listaChequera;
        }

        public Chequera obtenerCheque(int idCheque)
        {
            return context.Chequera.Where(p => p.Id == idCheque).First();
        }

        public Chequera Actualizar(Chequera oChequera)
        {
            Chequera nChequera = obtenerCheque(oChequera.Id);
            nChequera.Id = oChequera.Id;
            nChequera.NumeroCheque = oChequera.NumeroCheque;
            nChequera.IdBancoCuenta = oChequera.IdBancoCuenta;
            nChequera.Fecha = oChequera.Fecha;
            nChequera.Importes = oChequera.Importes;
            nChequera.IdProveedor = oChequera.IdProveedor;
            nChequera.NumeroRecibo = oChequera.NumeroRecibo;
            nChequera.FechaIngreso = oChequera.FechaIngreso;
            nChequera.FechaEgreso = oChequera.FechaEgreso;
            nChequera.Destino = oChequera.Destino;
            nChequera.IdMoneda = oChequera.IdMoneda;
            nChequera.NumeroOperacion = oChequera.NumeroOperacion;
            nChequera.Registro = oChequera.Registro;
            nChequera.Activo = oChequera.Activo;
            nChequera.IdUsuario = oChequera.IdUsuario;
            nChequera.UltimaModificacion = oChequera.UltimaModificacion;
            context.SaveChanges();
            return nChequera;
        }

    }
   
}
