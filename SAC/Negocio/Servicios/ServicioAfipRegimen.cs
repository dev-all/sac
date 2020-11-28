using System;
using Datos.Repositorios;
using Datos.ModeloDeDatos;
using Ninject;
using System.Collections.Generic;
using Negocio.Modelos;
using AutoMapper;
using System.Net.Mail;
using System.IO;
using System.Net;
using Negocio.Servicios;
using System.Net.Mime;
using System.Text;

namespace Negocio.Servicios
{
   public class ServicioAfipRegimen : ServicioBase
    {
        private AfipRegimenRepositorio oAfipRegimenRepositorio;

        public ServicioAfipRegimen()
        {
           oAfipRegimenRepositorio = kernel.Get<AfipRegimenRepositorio>();
        }


        public List<AfipRegimenModel> GetAllAfipRegimen()
        {
            try
            {
                var AfipRegimens = Mapper.Map<List<AfipRegimen>, List<AfipRegimenModel>>(oAfipRegimenRepositorio.GetAllAfipRegimen());
                return AfipRegimens;
            }
            catch (Exception ex)
            {
                _mensaje(ex.Message, "error");
                return null;
            }
        }


        public AfipRegimenModel ObtenerAfipRegimenPorId(int _id)
        {
            AfipRegimen oAfipRegimen = oAfipRegimenRepositorio.ObtenerAfipRegimenPorId(_id);
            AfipRegimenModel oAfipRegimenModel = new AfipRegimenModel();

            oAfipRegimenModel.Id = oAfipRegimen.Id;
            oAfipRegimenModel.Descripcion = oAfipRegimen.Descripcion;
            oAfipRegimenModel.Concepto = oAfipRegimen.Concepto;
            oAfipRegimenModel.Aliri = oAfipRegimen.Aliri;
            oAfipRegimenModel.Alirni = oAfipRegimen.Alirni;
            oAfipRegimenModel.Minimo = oAfipRegimen.Minimo;
            oAfipRegimenModel.Imputacion = oAfipRegimen.Imputacion;
            oAfipRegimenModel.Activo = oAfipRegimen.Activo;

            return oAfipRegimenModel;
        }

        public int ActualizarPais(AfipRegimenModel oAfipRegimenModel)
        {
            //controlar que no exista 
            AfipRegimen oAfipRegimen = oAfipRegimenRepositorio.ObtenerAfipRegimenPorNombre(oAfipRegimenModel.Descripcion, oAfipRegimenModel.Concepto, oAfipRegimenModel.Id);
            if (oAfipRegimen != null) //significa que existe
            {
                return -2;
            }
            else //significa que no existe el dato a ingresar
            {
                AfipRegimen oPaisNuevo = new AfipRegimen();
                AfipRegimen oPaisRespuesta = new AfipRegimen();

                oPaisNuevo.Id = oAfipRegimenModel.Id;
                oPaisNuevo.Descripcion = oAfipRegimenModel.Descripcion;
                oPaisNuevo.Concepto = oAfipRegimenModel.Concepto;
                oPaisNuevo.Aliri = oAfipRegimenModel.Aliri;
                oPaisNuevo.Alirni = oAfipRegimenModel.Alirni;
                oPaisNuevo.Minimo = oAfipRegimenModel.Minimo;
                oPaisNuevo.Minimo = oAfipRegimenModel.Imputacion;
                oPaisNuevo.Activo = true;

                oPaisRespuesta = oAfipRegimenRepositorio.ActualizarAfipRegimen(oPaisNuevo);

                if (oPaisRespuesta == null)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }

            }
        }

        public int GuardarAfipRegimen(AfipRegimenModel oAfipRegimenModel)
        {
            //controlar que no exista 
            AfipRegimen oAfipRegimen = oAfipRegimenRepositorio.ObtenerAfipRegimenPorNombre(oAfipRegimenModel.Descripcion, oAfipRegimenModel.Concepto);
            if (oAfipRegimen != null)
            {
                return -2;
            }
            else
            {
                AfipRegimen oAfipRegimenNuevo = new AfipRegimen();
                AfipRegimen oAfipRegimenRespuesta = new AfipRegimen();

                oAfipRegimenNuevo.Descripcion = oAfipRegimenModel.Descripcion;
                oAfipRegimenNuevo.Concepto = oAfipRegimenModel.Concepto;
                oAfipRegimenNuevo.Aliri = oAfipRegimenModel.Aliri;
                oAfipRegimenNuevo.Alirni = oAfipRegimenModel.Alirni;
                oAfipRegimenNuevo.Minimo = oAfipRegimenModel.Minimo;
                oAfipRegimenNuevo.Imputacion= oAfipRegimenModel.Imputacion;
                oAfipRegimenNuevo.Activo = true;
                oAfipRegimenNuevo.IdUsuario = oAfipRegimenModel.IdUsuario;
                oAfipRegimenNuevo.UltimaModificacion = oAfipRegimenModel.UltimaModificacion;

                oAfipRegimenRespuesta = oAfipRegimenRepositorio.InsertarAfipRegimen(oAfipRegimenNuevo);
                if (oAfipRegimenRespuesta == null)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }

            }
        }

        public int Eliminar(int idAfipRegimen)
        {
            var retorno = oAfipRegimenRepositorio.EliminarAfipRegimen(idAfipRegimen);
            if (retorno == 1)
            {
                return 0; //ok
            }
            else
            {
                return -1;//paso algo
            }
        }


    }
}
