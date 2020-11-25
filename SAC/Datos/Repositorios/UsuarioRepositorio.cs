using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.Interfaces;
using Datos.ModeloDeDatos;
using Entidad.Modelos;
using Z.EntityFramework.Plus;
namespace Datos.Repositorios
{
    public class UsuarioRepositorio : RepositorioBase<Usuario>
        , IUsuarioRepositorio
        , IRepositorio<Usuario>
    {
        private SAC_Entities contexto;

        public UsuarioRepositorio(SAC_Entities contextoDeDatos) : base(contextoDeDatos)
        {
            this.contexto = contextoDeDatos;
        }

        public void ActualizarRolDeUsaurio(int idUsuario, int idRol, int idUsuarioLogueado)
        {
            Usuario usuario = ObtenerPorID(idUsuario);
            usuario.IdRol = idRol;            
            contexto.Entry(usuario).State = EntityState.Modified;
            contexto.SaveChanges();
        }

     
        public void CambiarPassword(int idUsuario, string password)
        {
            Usuario usuario = ObtenerPorID(idUsuario);
            usuario.Password = password;
            contexto.Entry(usuario).State = EntityState.Modified;
            contexto.SaveChanges();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public object logLogin(int idUsuario, string IP)
        {
            throw new NotImplementedException();
        }

        public bool Obtener(string documento, string password, int idRolInvitado)
        {
            return !( contexto.Usuario
                              .Where(x => x.Persona.Documento == documento
                              &&  x.Password  == password 
                              && x.IdRol != idRolInvitado).Count() == 0);           
        }

        public List<Usuario> GetAllUsuario()
        {
            return contexto.Usuario
                   .Include(x => x.Rol)
                   .Include(x => x.Persona)
                   .Where(x => x.Activo == true).ToList();
        }


        List<MenuItemModel> IUsuarioRepositorio.ObtenerMenuUsuario(int idUsuario, MenuItemModel[] menuItems)
        {
            var items = contexto.Usuario
                          .Where(x => x.IdUsuario == idUsuario)                         
                          .Include(x => x.Rol)
                          .Include(x => x.Rol.AccionPorRol)
                          .Include(x => x.Rol.AccionPorRol.Select(AccPorRol => AccPorRol.Accion))
                          .Select(x => x.Rol.AccionPorRol)
                          .Select(accionPorRol => accionPorRol.Select(acc => acc.Accion.Controlador.ToLower() + acc.Accion.Nombre.ToLower())).FirstOrDefault().ToArray();
             //.Include(x => x.Persona)
            return menuItems.Where(item => items.Contains(item.Controller.ToLower() + item.Metodo.ToLower())).ToList();
        }
     
        List<MenuSidebar> IUsuarioRepositorio.ObtenerMenuUsuario(int idUsuario)
        {
            var items = contexto.Usuario
                          .Where(x => x.IdUsuario == idUsuario)
                          .Include(x => x.Rol)
                          .Include(x => x.Rol.AccionPorRol)
                          .Include(x => x.Rol.AccionPorRol.Select(AccPorRol => AccPorRol.Accion))
                          .Select(x => x.Rol.AccionPorRol)
                          .Select(accionPorRol => accionPorRol.Select(acc => acc.Accion.Controlador.ToLower() + acc.Accion.Nombre.ToLower())).FirstOrDefault().ToArray();
            
            var side = contexto.MenuSidebar
                                 .Include(m => m.Accion)
                                 .IncludeFilter(m => m.MenuSidebar1.Where(p => p.Activo == true))
                                 .Where(menu => menu.Activo == true 
                                            && menu.IdParent == null
                                            && items.Contains(menu.Accion.Controlador.ToLower() + menu.Accion.Nombre.ToLower()))
                                 .OrderBy(acc => acc.Orden).ToList();
            return side;
        }

        public Usuario ObtenerPorDocumento(String documento)
        {
           Usuario usuario = contexto.Usuario
                            .Include(x => x.Persona)
                            .Include(x => x.Rol)
                             .Where(x => x.Persona.Documento == documento).FirstOrDefault();
            return usuario;
        }

        public Usuario ObtenerPorID(int idUsuario)
        {
            Usuario usuario = contexto.Usuario
                            .Include(x => x.Persona)
                            .Include(x => x.Rol)
                             .Where(x => x.IdUsuario == idUsuario).FirstOrDefault();

            return usuario;
        }

        public Rol ObtenerRol(int idUsuario)
        {
            Usuario usuario = contexto.Usuario
                            .Include(x => x.Persona)
                            .Include(x => x.Rol)
                            .Where(x => x.IdUsuario == idUsuario).FirstOrDefault();

            return usuario.Rol;
        }

        public void CreateUsuario(Usuario usuario)
        {
             Insertar(usuario);
        }
       
        public void UpdateUsuario(Usuario model)
        {
          
            contexto.Usuario.Attach(model);

            contexto.Entry(model).Property(x => x.UserName).IsModified = true;
            contexto.Entry(model).Property(x => x.Password).IsModified = true;
            contexto.Entry(model).Property(x => x.IdRol).IsModified = true;
            contexto.Entry(model).Property(x => x.Activo).IsModified = true;
            contexto.Entry(model).Property(x => x.Persona.Email).IsModified = true;
            contexto.Entry(model).Property(x => x.Persona.Documento).IsModified = true;
            contexto.Entry(model).Property(x => x.Persona.Nombre).IsModified = true;
            contexto.Entry(model).Property(x => x.Persona.Apellido).IsModified = true;
            contexto.Entry(model).Property(x => x.Persona.Cuil).IsModified = true;
            contexto.Entry(model).Property(x => x.Persona.TelefonoMovil).IsModified = true;
            contexto.Entry(model).Property(x => x.Persona.Activo).IsModified = true;

            contexto.SaveChanges();

        }



    }
}