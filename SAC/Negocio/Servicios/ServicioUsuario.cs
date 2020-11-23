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

namespace Negocio.Servicios
{

    public class ServicioUsuarios : ServicioBase, IServicioUsuarios
    {
        private IUsuarioRepositorio repositorio { get; set; }      
        public ServicioUsuarios()
        {
            repositorio = kernel.Get<IUsuarioRepositorio>();
        }
        public UsuarioModel Agregar(UsuarioModel usuario)
        {
            try
            {
               var usuarioDTO = Mapper.Map<UsuarioModel, Usuario>(usuario);
            usuarioDTO = repositorio.Insertar(usuarioDTO);
            usuario.IdUsuario = usuarioDTO.IdUsuario;
            return usuario;
            }
            catch (Exception)
            {
                _mensaje("Ops!, A ocurriodo un error. Intente mas tarde por favor", "erro");
                return null;
            }
           
        }
        public IEnumerable<UsuarioModel> Obtener()
        {
            try
            {
          
                var usuarios = from usuario in repositorio.Obtener()
                               select new UsuarioModel();
                return usuarios;
            }
            catch (Exception)
            {
                _mensaje("Ops!, A ocurriodo un error. Intente mas tarde por favor", "erro");
                return null;
            }
         
        }

        public List<UsuarioModel> GetAllUsuario()
        {
            try
            {
                var usuarios = Mapper.Map< List<Usuario>, List<UsuarioModel> >(repositorio.GetAllUsuario());
                return usuarios;
            }
            catch (Exception ex)
            {
                _mensaje("Ops!, A ocurriodo un error. Intente mas tarde por favor" + ex.Message, "erro");
                return null;
            }

        }


        public UsuarioModel ObtenerPorID(int idUsuario)
        {
            try
            {
                var usuario = repositorio.ObtenerPorID(idUsuario);
                UsuarioModel usuarioModel = new UsuarioModel() { IdUsuario = usuario.IdUsuario };
                return usuarioModel;
            }
            catch (Exception)
            {
                _mensaje("Ops!, A ocurriodo un error. Intente mas tarde por favor", "erro");
                return null;
            }
            
        }
        public UsuarioModel ObtenerUsuario(string documento, int IdModulo)
        {

            try
            {
   var usuario = repositorio.ObtenerPorDocumento(documento);

            UsuarioModel usuarioModel = new UsuarioModel() { IdUsuario = usuario.IdUsuario, Documento = documento };

            var rolModel = Mapper.Map<Rol, RolModel>(usuario.Rol);
            rolModel.Acciones = new Dictionary<string, List<string>>();
            foreach (var accion in usuario.Rol.AccionPorRol)
            {
                if (accion.Accion.IdModulo == IdModulo)
                    continue;

                if (!rolModel.Acciones.ContainsKey(accion.Accion.Controlador))
                    rolModel.Acciones.Add(accion.Accion.Controlador, new List<string>());

                rolModel.Acciones[accion.Accion.Controlador].Add(accion.Accion.Nombre);
            }

            usuarioModel.EsAdministrador = rolModel.EsAdministrador;
            usuarioModel.CargarRoles(rolModel);
            return usuarioModel;
            }
            catch (Exception)
            {
                _mensaje("Ops!, A ocurriodo un error. Intente mas tarde por favor", "erro");
                return null;
            }
         
        }
        public bool Obtener(string documento, string password, int idRolInvitado)
        {
            try
            {
                return this.repositorio.Obtener(documento, StringHelper.ObtenerMD5(password), idRolInvitado);
            }
            catch (Exception ex)
            {
                _mensaje("Ops!, A ocurriodo un error. Intente mas tarde por favor", "erro");
                return false;
            }
           
        }

        public void UpdateUsuario(UsuarioModel usuarioModel)
        {
            throw new NotImplementedException();
        }

        public void AddUsuario(UsuarioModel usuarioModel)
        {
            throw new NotImplementedException();
        }

        public void ActualizarRolDeUsaurio(int idUsuario, int idRol, int idUsuarioLogueado)
        {
              try
            {
                repositorio.ActualizarRolDeUsaurio(idUsuario, idRol, idUsuarioLogueado);         
                _mensaje("Se Actualizo correctamente", "sucesso");
            }
            catch (Exception ex)
            {
                _mensaje("Ops!, A ocurriodo un error. Intente mas tarde por favor", "erro");
               
            }
            
        }
        public List<MenuItemModel> ObtenerMenuUsuario(int idUsuario, MenuItemModel[] menuItems)
        {
            try
            {
                return repositorio.ObtenerMenuUsuario(idUsuario, menuItems);               
            }
            catch (Exception ex)
            {
                _mensaje("Ops!, A ocurriodo un error. Intente mas tarde por favor", "erro");
                return null;
            }
            
        }
        public List<MenuSideBarModel> ObtenerMenuUsuario(int idUsuario)
        {
            try
            {
                var lista = Mapper.Map<List<MenuSidebar>, List<MenuSideBarModel>>(repositorio.ObtenerMenuUsuario(idUsuario));
                return lista;
            }
            catch (Exception ex)
            { 
                _mensaje("Ops!, A ocurriodo un error. Intente mas tarde por favor", "erro");
                return null;
            }

        }
        public void CambiarPassword(int idUsuario, string password)
        {
            try
            {
            var passwordHasheado = StringHelper.ObtenerMD5(password);
            repositorio.CambiarPassword(idUsuario, passwordHasheado);
                _mensaje("Se Actualizo correctamente", "sucesso");
            }
            catch (Exception ex)
            {
                _mensaje("Ops!, A ocurriodo un error. Intente mas tarde por favor", "erro");

            }
          
        }
        public RolModel ObtenerRol(int idUsuario)
        {
            try
            {
                return Mapper.Map<Rol, RolModel>(repositorio.ObtenerRol(idUsuario));
            }
            catch (Exception)
            {
                _mensaje("Ops!, A ocurriodo un error. Intente mas tarde por favor", "erro");
                return null;
            }


        }
        public void LogLogin(int idUsuario, string ip)
        {
            try
            {
 repositorio.logLogin(idUsuario, ip);
               
            }
            catch (Exception)
            {
                _mensaje("Ops!, A ocurriodo un error. Intente mas tarde por favor", "erro");

            }
           
        }

        //public RolModel AgregarRol(RolModel rol)
        //{
        //    var rolDTO = Mapper.Map<RolModel, Rol>(rol);
        //    rolDTO = repositorio.AgregarRol(rolDTO);
        //    rol.IdRol = rolDTO.idRol;
        //    return rol;
        //}



        //public void ActualizarUnidadDeUsaurio(int idPersona, int idUnidad, int idUsuarioLogueado)
        //{
        //    repositorio.ActualizarUnidadDeUsaurio(idPersona, idUnidad, idUsuarioLogueado);
        //}


    }

}
