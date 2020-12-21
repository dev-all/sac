using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Datos.Repositorios
{
  public class ChequeRepositorio : RepositorioBase<Cheque>
    {
        private SAC_Entities context;

        public ChequeRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

        public List<Cheque> GetAllCheque()
        {
            List<Cheque> listaCheque = context.Cheque.Where(p => p.Activo == true && p.NumeroPago == null && p.IdFactura == null).ToList();
            //List<Cheque> listaCheque = context.Cheque.ToList();
            listaCheque = listaCheque.OrderBy(p => p.NumeroCheque).ToList();

           // List<Cheque> listaCheque = context.Cheque.Where(p => p.Activo == true && p.Endosado == false ).ToList();
           // return listaCheque.OrderBy(p => p.NumeroCheque).ToList();
            return listaCheque;
        }


        public Cheque obtenerCheque (int idCheque)
        {
            return context.Cheque.Where(p => p.Id == idCheque).First();
        }


        public Cheque Agregar(Cheque oCheque)
        {
            return Insertar(oCheque);
        }

        public Cheque Actualizar (Cheque oCheque)
        {
            Cheque nCheque = obtenerCheque(oCheque.Id);
            nCheque.Id = oCheque.Id;
            nCheque.NumeroCheque = oCheque.NumeroCheque;
            nCheque.IdBanco = oCheque.IdBanco;
            nCheque.Fecha = oCheque.Fecha;
            nCheque.DiaClearing = oCheque.DiaClearing;
            nCheque.Importe = oCheque.Importe;
            nCheque.IdCliente = oCheque.IdCliente;
            nCheque.Descripcion = oCheque.Descripcion;
            nCheque.NumeroRecibo = oCheque.NumeroRecibo;
            nCheque.FechaIngreso = oCheque.FechaIngreso;
            nCheque.FechaEgreso = oCheque.FechaEgreso;
            nCheque.Destino = oCheque.Destino;
            nCheque.IdMoneda = oCheque.IdMoneda;
            nCheque.GrupoCaja = oCheque.GrupoCaja;
            nCheque.IdFactura = oCheque.IdFactura;
            nCheque.NumeroPago = oCheque.NumeroPago;
            nCheque.Registro = oCheque.Registro;
            nCheque.Proveedor = oCheque.Proveedor;
            nCheque.Endosado = oCheque.Endosado;
            nCheque.Activo = oCheque.Activo;
            nCheque.IdUsuario = oCheque.IdUsuario;
            nCheque.UltimaModificacion = oCheque.UltimaModificacion;
            context.SaveChanges();
            return nCheque;
        }


    }
}
