using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace Datos.Repositorios
{
   public class BancoCuentaRepositorio : RepositorioBase<BancoCuenta>
    {

        private SAC_Entities context;

        public BancoCuentaRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }


        public List<BancoCuenta> GetAllCuenta()
        {
            // context.Configuration.LazyLoadingEnabled = false;
            return context.BancoCuenta.ToList();

        }

        public BancoCuenta GetCuentaPorId(int id)
        {
            return context.BancoCuenta.Where(p => p.Id == id && p.Activo == true ).First();           
        }


        public BancoCuenta GetBancoCuentaPorId(int id)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return context.BancoCuenta.Where(p => p.Id == id && p.Activo == true).First();
        }

        public List<BancoCuenta> GetBancoPorNombre(string strBanco)
        {
            List<BancoCuenta> p = (from c in context.BancoCuenta
                                   where c.Activo == true && c.Banco.Nombre.Contains(strBanco)
                                   select c).ToList();
            return p;
        }

        public Banco GetBancoPorId(int id)
        {
            return context.Banco.Where(p => p.Id == id).First();
        }

        public Banco GetBancoPorIdLazy(int id)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return context.Banco.Where(p => p.Id == id).First();
        }


        public List<BancoCuentaBancaria> GetmMovimientosPendientesCuentaBancaria(int idBanco, DateTime fecha)
        {
            context.Configuration.LazyLoadingEnabled = false;
            List<BancoCuentaBancaria> listaCliente = context.BancoCuentaBancaria
                                                    .Include("GrupoCaja") // armar la relacion
                                                    .Where(p => p.Activo == true
                                                   && p.NumeroCierre == 0
                                                   && p.IdBancoCuenta == idBanco
                                                  && p.FechaEfectiva <= fecha).ToList();
            return listaCliente;

        }

        public List<BancoCuenta> GetBancoPorFecha(int idBanco, DateTime fecha)
        {
            context.Configuration.LazyLoadingEnabled = false;
            List<BancoCuenta> listaCliente = context.BancoCuenta
             .Include("Banco")
             .Include("BancoCuentaBancaria")
             .Include("Imputacion")
           .Where(p => p.Activo == true && p.NumeroCierre == 0 && p.IdBanco == idBanco).ToList(); //  && p.Fecha <= Fecha).ToList();


            return listaCliente;

        }

        public BancoCuenta CierreDeCuentaBancaria(int id, decimal saldoCierre)
        {
            BancoCuenta bancoCuenta = GetBancoCuentaPorId(id);
            bancoCuenta.Saldo = saldoCierre;
            bancoCuenta.NumeroCierre += 1;
            bancoCuenta.Fecha = DateTime.Now;
            context.SaveChanges();
            return bancoCuenta;
        }

        public List<Banco> GetAllBanco()
        {
            return context.Banco.Where(p => p.Activo == true).ToList();
        }
    }
}
