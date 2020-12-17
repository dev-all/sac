using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Datos.Repositorios
{
    public class DiarioRepositorio : RepositorioBase<Diario>
    {
        private SAC_Entities context;

        public DiarioRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

        public Diario InsertarDiario(Diario Diario)
        {
            return Insertar(Diario);
        }


        public List<Diario>  GetAllDiario()
        {
            List<Diario> diarios = context.Diario.Where(p => p.Activo == true).ToList();
            return diarios;
        }


        public Diario GetDiarioPorId(int id)
        {
            var Diario = context.Diario.Where(p => p.Id == id).FirstOrDefault();
            return Diario;
        }

        public Diario ActualizarDiario(Diario model)
        {
            Diario diario = GetDiarioPorId(model.Id);
            //diario.Codigo = model.Codigo;
            //diario.Activo = model.Activo;
            //context.SaveChanges();
            return diario;
        }

        public void EliminarDiario(int id)
        {
            Diario paisExistente = GetDiarioPorId(id);
            paisExistente.Activo = false;
            context.SaveChanges();            
        }


    }
}