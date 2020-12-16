﻿using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Datos.Repositorios
{
    public class TipoComprobanteRepositorio : RepositorioBase<TipoComprobante>
    {
        private SAC_Entities context;

        public TipoComprobanteRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }
        public TipoComprobante InsertarTipoComprobante(TipoComprobante tipoComprobante)
        {
            return Insertar(tipoComprobante);
        }


        public TipoComprobante GetTipoComprobantePorId(int id)
        {
            var TipoComprobante = context.TipoComprobante.Where(p => p.Id == id).FirstOrDefault();
            return TipoComprobante;
        }

        public TipoComprobante ActualizarTipoComprobante(TipoComprobante model)
        {
            TipoComprobante TipoComprobanteExistente = GetTipoComprobantePorId(model.Id);

            TipoComprobanteExistente.Id = model.Id;
            TipoComprobanteExistente.Denominacion = model.Denominacion;
            TipoComprobanteExistente.Activo = model.Activo;

            context.SaveChanges();
            return TipoComprobanteExistente;
        }

        public TipoComprobante ObtenerPorNombre(string nombre)
        {
            return context.TipoComprobante.Where(p => p.Denominacion == nombre).FirstOrDefault();
        }

        public List<TipoComprobante> GetTipoComprobantePorTipoIvaProveedor(int idTipoIva)
        {
             return (from c in context.TipoComprobante
                    join ci in context.TipoComprobanteTipoIva on c.Id equals ci.IdTipoComprobante
                    where c.Activo == true && ci.IdTipoIva == idTipoIva
                    select c).ToList();
          
        }

        public List<TipoComprobante> GetAllTipoComprobante()
        {
            List<TipoComprobante> listModel = context.TipoComprobante.Where(p => p.Activo == true).ToList();
            return listModel;
        }
       

        public int EliminarTipoComprobanteTipoMoneda(int id)
        {
            TipoComprobante model = GetTipoComprobantePorId(id);
            model.Activo = false;
            context.SaveChanges();
            return 1;

        }

    }
}
