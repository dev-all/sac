﻿using Datos.Interfaces;
using Datos.ModeloDeDatos;
using Negocio.Interfaces;
using Negocio.Modelos;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Negocio.Helpers;
using Entidad.Modelos;
using Datos.Repositorios;
using System.Reflection;

namespace Negocio.Servicios
{
  public class ServicioDto : ServicioBase
    {
        private DtoRepositorio oDtoRepositorio;

        public ServicioDto()
        {
            oDtoRepositorio = kernel.Get<DtoRepositorio>();
        }

        public List<DtoModel> GetAllDto()
        {
            List<DtoModel> listaDto = Mapper.Map<List<Dto>, List<DtoModel>>(oDtoRepositorio.GetAllDto());
            return listaDto;
        }

        public DtoModel obtenerDto(int anio, int codigoDto, int codigoArticulo)
        {
            return Mapper.Map<Dto, DtoModel>(oDtoRepositorio.obtenerDto(anio, codigoDto, codigoArticulo));
        }

     
        public DtoModel Agregar(DtoModel oDtoModel)
        {
            try
            {
                var oModel = Mapper.Map<DtoModel, Dto>(oDtoModel);
                return Mapper.Map<Dto, DtoModel>(oDtoRepositorio.Agregar(oModel));
            }
            catch (Exception ex)
            {
                _mensaje("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return null;
            }
        }


        public DtoModel ActualizarDatosDto (DateTime fechaFactura, ItemImprModel itemImpre,int codigoArticulo, int idTipoIva, int dto, int nFactor, int idmoneda, decimal cotizacion, ArticuloModel articuloModel)
        {
            string fecha = fechaFactura.ToString();
            string[] FechaDesarmada = fecha.Split('/');
            string[] AnioDesarmado;
            string anio;
            string mes;
            AnioDesarmado = FechaDesarmada[2].Split(' ');
            anio = AnioDesarmado[0];

            if (int.Parse(FechaDesarmada[1]) < 10)
            {
                mes = "0" + FechaDesarmada[1];
            }
            else
            {
                mes = FechaDesarmada[1];
            }

            //verifico si existe el registro a actualizar
            DtoModel dtoVerifica = Mapper.Map<Dto,DtoModel>( oDtoRepositorio.obtenerDto(int.Parse(anio), dto, codigoArticulo));

            if (dtoVerifica == null) //si no esta creado lo creo
            {
                DtoModel DtoInsertar = new DtoModel();
                DtoInsertar.Periodo = int.Parse(anio);
                DtoInsertar.CodDto = dto.ToString();
                DtoInsertar.CodArt = itemImpre.Codigo;
                DtoInsertar.Activo = true;
                DtoInsertar.IdUsuario = 1;
                DtoInsertar.UltimaModificacion = DateTime.Now;
                decimal valor;
                if (idTipoIva != 4) //es local
                {
                    if (idmoneda ==2) // moneda dolares
                    {
                        valor = itemImpre.Precio * nFactor;
                        switch (int.Parse(mes))
                        {
                            case 1:
                                DtoInsertar.Loc01 += valor;
                                break;
                            case 2:
                                DtoInsertar.Loc02 += valor;
                                break;
                            case 3:
                                DtoInsertar.Loc03 += valor;
                                break;
                            case 4:
                                DtoInsertar.Loc04 += valor;
                                break;
                            case 5:
                                DtoInsertar.Loc05 += valor;
                                break;
                            case 6:
                                DtoInsertar.Loc06 += valor;
                                break;
                            case 7:
                                DtoInsertar.Loc07 += valor;
                                break;
                            case 8:
                                DtoInsertar.Loc08 += valor;
                                break;
                            case 9:
                                DtoInsertar.Loc09 += valor;
                                break;
                            case 10:
                                DtoInsertar.Loc10 += valor;
                                break;
                            case 11:
                                DtoInsertar.Loc11 += valor;
                                break;
                            case 12:
                                DtoInsertar.Loc12 += valor;
                                break;
                        }
                    }
                    else //moneda pesos
                    {
                        valor = (itemImpre.Precio / cotizacion) * nFactor;

                        switch (int.Parse(mes))
                        {
                            case 1:
                                DtoInsertar.Loc01 += valor;
                                break;
                            case 2:
                                DtoInsertar.Loc02 += valor;
                                break;
                            case 3:
                                DtoInsertar.Loc03 += valor;
                                break;
                            case 4:
                                DtoInsertar.Loc04 += valor;
                                break;
                            case 5:
                                DtoInsertar.Loc05 += valor;
                                break;
                            case 6:
                                DtoInsertar.Loc06 += valor;
                                break;
                            case 7:
                                DtoInsertar.Loc07 += valor;
                                break;
                            case 8:
                                DtoInsertar.Loc08 += valor;
                                break;
                            case 9:
                                DtoInsertar.Loc09 += valor;
                                break;
                            case 10:
                                DtoInsertar.Loc10 += valor;
                                break;
                            case 11:
                                DtoInsertar.Loc11 += valor;
                                break;
                            case 12:
                                DtoInsertar.Loc12 += valor;
                                break;
                        }
                    }
                }
                else //exterior
                {
                    valor = itemImpre.Precio * nFactor;
                    switch (int.Parse(mes))
                    {
                        case 1:
                            DtoInsertar.Ext01 += valor;
                            break;
                        case 2:
                            DtoInsertar.Ext02 += valor;
                            break;
                        case 3:
                            DtoInsertar.Ext03 += valor;
                            break;
                        case 4:
                            DtoInsertar.Ext04 += valor;
                            break;
                        case 5:
                            DtoInsertar.Ext05 += valor;
                            break;
                        case 6:
                            DtoInsertar.Ext06 += valor;
                            break;
                        case 7:
                            DtoInsertar.Ext07 += valor;
                            break;
                        case 8:
                            DtoInsertar.Ext08 += valor;
                            break;
                        case 9:
                            DtoInsertar.Ext09 += valor;
                            break;
                        case 10:
                            DtoInsertar.Ext10 += valor;
                            break;
                        case 11:
                            DtoInsertar.Ext11 += valor;
                            break;
                        case 12:
                            DtoInsertar.Ext12 += valor;
                            break;
                    }
                }

                DtoModel dtoCreado = Mapper.Map<Dto, DtoModel>(oDtoRepositorio.Insertar(Mapper.Map<DtoModel,Dto> (DtoInsertar)));
                return dtoCreado;
            }
            else // esto es si ya existe el registro
            {
              ServicioDto servicioDto = new ServicioDto();

                DtoModel DtoActualizar = new DtoModel();
                DtoActualizar.Periodo = int.Parse(anio);
                DtoActualizar.CodDto = dto.ToString();
                DtoActualizar.CodArt = itemImpre.Codigo;
                decimal valor;
                if (idTipoIva != 4) //es local
                {
                    if (idmoneda == 2) // moneda dolares
                    {
                        valor = itemImpre.Precio * nFactor;
                        switch (int.Parse(mes))
                        {
                            case 1:
                                DtoActualizar.Loc01 += valor;
                                break;
                            case 2:
                                DtoActualizar.Loc02 += valor;
                                break;
                            case 3:
                                DtoActualizar.Loc03 += valor;
                                break;
                            case 4:
                                DtoActualizar.Loc04 += valor;
                                break;
                            case 5:
                                DtoActualizar.Loc05 += valor;
                                break;
                            case 6:
                                DtoActualizar.Loc06 += valor;
                                break;
                            case 7:
                                DtoActualizar.Loc07 += valor;
                                break;
                            case 8:
                                DtoActualizar.Loc08 += valor;
                                break;
                            case 9:
                                DtoActualizar.Loc09 += valor;
                                break;
                            case 10:
                                DtoActualizar.Loc10 += valor;
                                break;
                            case 11:
                                DtoActualizar.Loc11 += valor;
                                break;
                            case 12:
                                DtoActualizar.Loc12 += valor;
                                break;
                        }
                    }
                    else //moneda pesos
                    {
                        valor = (itemImpre.Precio / cotizacion) * nFactor;

                        switch (int.Parse(mes))
                        {
                            case 1:
                                DtoActualizar.Loc01 += valor;
                                break;
                            case 2:
                                DtoActualizar.Loc02 += valor;
                                break;
                            case 3:
                                DtoActualizar.Loc03 += valor;
                                break;
                            case 4:
                                DtoActualizar.Loc04 += valor;
                                break;
                            case 5:
                                DtoActualizar.Loc05 += valor;
                                break;
                            case 6:
                                DtoActualizar.Loc06 += valor;
                                break;
                            case 7:
                                DtoActualizar.Loc07 += valor;
                                break;
                            case 8:
                                DtoActualizar.Loc08 += valor;
                                break;
                            case 9:
                                DtoActualizar.Loc09 += valor;
                                break;
                            case 10:
                                DtoActualizar.Loc10 += valor;
                                break;
                            case 11:
                                DtoActualizar.Loc11 += valor;
                                break;
                            case 12:
                                DtoActualizar.Loc12 += valor;
                                break;
                        }
                    }
                }
                else //exterior
                {
                    valor = itemImpre.Precio * nFactor;
                    switch (int.Parse(mes))
                    {
                        case 1:
                            DtoActualizar.Ext01 += valor;
                            break;
                        case 2:
                            DtoActualizar.Ext02 += valor;
                            break;
                        case 3:
                            DtoActualizar.Ext03 += valor;
                            break;
                        case 4:
                            DtoActualizar.Ext04 += valor;
                            break;
                        case 5:
                            DtoActualizar.Ext05 += valor;
                            break;
                        case 6:
                            DtoActualizar.Ext06 += valor;
                            break;
                        case 7:
                            DtoActualizar.Ext07 += valor;
                            break;
                        case 8:
                            DtoActualizar.Ext08 += valor;
                            break;
                        case 9:
                            DtoActualizar.Ext09 += valor;
                            break;
                        case 10:
                            DtoActualizar.Ext10 += valor;
                            break;
                        case 11:
                            DtoActualizar.Ext11 += valor;
                            break;
                        case 12:
                            DtoActualizar.Ext12 += valor;
                            break;
                    }
                }

                DtoModel dtoActuralizado = servicioDto.Actualizar(dtoVerifica);
                return dtoActuralizado;
            }

        }

     

        public DtoModel Actualizar(DtoModel oDtoModel)
        {
            try
            {
                var oModel = Mapper.Map<DtoModel, Dto>(oDtoModel);
                return Mapper.Map<Dto, DtoModel>(oDtoRepositorio.Actualizar(oModel));

            }
            catch (Exception ex)
            {
                _mensaje("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return null;
            }
        }


    }
}
