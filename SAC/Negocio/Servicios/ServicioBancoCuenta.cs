using Datos.Interfaces;
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

namespace Negocio.Servicios
{
   public class ServicioBancoCuenta : ServicioBase
    {
        private BancoCuentaRepositorio oBancoCuentaRepositorio;
        public Action<string, string> _mensaje;

        public ServicioBancoCuenta()
        {
            oBancoCuentaRepositorio = kernel.Get<BancoCuentaRepositorio>();
        }

        public List<BancoCuentaModel> GetAllCuenta()
        {
            return Mapper.Map<List<BancoCuenta>, List<BancoCuentaModel>>(oBancoCuentaRepositorio.GetAllCuenta());
        }

        public BancoCuentaModel GetCuentaPorId(int id)
        {
            return Mapper.Map<BancoCuenta, BancoCuentaModel>(oBancoCuentaRepositorio.GetCuentaPorId(id));
        }

     public List<BancoCuentaModel> GetBancoPorNombre(string strBanco)
        {

            try
            {
                return Mapper.Map<List<BancoCuenta>, List<BancoCuentaModel>>(oBancoCuentaRepositorio.GetBancoPorNombre(strBanco));
            }
            catch (Exception ex)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
                return null;
            }


        }

        public BancoModel GetBancoPorId(int id)
        {
            try
            {
                return Mapper.Map<Banco, BancoModel>(oBancoCuentaRepositorio.GetBancoPorId(id));
            }
            catch (Exception ex)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
                return null;
            }
        }

        
      public List<BancoCuentaBancariaModel> GetmMovimientosPendientesCuentaBancaria(int idBanco, DateTime fecha)
        {
            try
            {
                return Mapper.Map<List<BancoCuentaBancaria>, List<BancoCuentaBancariaModel>>(oBancoCuentaRepositorio.GetmMovimientosPendientesCuentaBancaria(idBanco, fecha));
            }
            catch (Exception ex)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador" + ex.Message, "erro");
                return null;
            }
        }
        public List<BancoCuentaModel> GetBancoPorFecha(int idBanco, DateTime fecha)
        {
            try
            {
                return Mapper.Map<List<BancoCuenta>, List<BancoCuentaModel>>(oBancoCuentaRepositorio.GetBancoPorFecha(idBanco, fecha));
            }
            catch (Exception ex)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador" + ex.Message, "erro");
                return null;
            }
        }

        public BancoCuentaModel GetBancoCuentaPorId(int idBancoCuenta)
        {
            return Mapper.Map<BancoCuenta, BancoCuentaModel>(oBancoCuentaRepositorio.GetCuentaPorId(idBancoCuenta));
        }

        public BancoCuentaModel CierreDeCuentaBancaria(int id, decimal saldoCierre)
        {
            try
            {

                return Mapper.Map<BancoCuenta, BancoCuentaModel>(oBancoCuentaRepositorio.CierreDeCuentaBancaria(id, saldoCierre));
            }
            catch (Exception ex)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador" + ex.Message, "erro");
                return null;
            }
        }
    }
}
