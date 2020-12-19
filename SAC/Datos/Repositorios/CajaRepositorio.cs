using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace Datos.Repositorios
{
    public class CajaRepositorio : RepositorioBase<Caja>
    {
       private SAC_Entities context;
    
        public CajaRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }
            
        public Caja CrearCaja(Caja model)
        {
           return  Insertar(model);
        }

        public List<Caja> GetAllCaja()
        {
            return context.Caja.Where(acc => acc.Activo == true && acc.IdCajaSaldo==0).OrderBy(acc => acc.Id).ToList();
        }

        public Caja GetCajaPorId(int id)
        {           
            return context.Caja.Where(acc => acc.Id == id && acc.Activo == true).FirstOrDefault(); 
        }


        public Caja GetCajaPorGrupo(int idgrupocaja)
        {
            return context.Caja.Where(acc => acc.IdGrupoCaja == idgrupocaja && acc.Activo == true).FirstOrDefault();
        }



        public Caja ActualizarCaja(Caja Model)
        {

            Caja GrupoCajaExistente = GetCajaPorId(Model.Id);

            GrupoCajaExistente.Id = Model.Id;
            GrupoCajaExistente.Concepto = Model.Concepto;
            GrupoCajaExistente.IdGrupoCaja = Model.IdGrupoCaja;

            GrupoCajaExistente.UltimaModificacion = Model.UltimaModificacion;
            GrupoCajaExistente.IdUsuario= Model.IdUsuario;

            GrupoCajaExistente.ImporteCheque= Model.ImporteCheque;
            GrupoCajaExistente.ImporteDeposito = Model.ImporteDeposito;
            GrupoCajaExistente.ImporteDolar = Model.ImporteDolar;
            GrupoCajaExistente.ImporteTarjeta = Model.ImporteTarjeta;
            GrupoCajaExistente.ImportePesos = Model.ImportePesos;



            context.SaveChanges();

            return GrupoCajaExistente;





        }



        public int DeleteCaja(int IdCaja)
        {
            Caja CajaCaja = GetCajaPorId(IdCaja);
            CajaCaja.Activo = false;
            CajaCaja.UltimaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
            context.SaveChanges();
            return 1;

        }


      
    
       
    }
}