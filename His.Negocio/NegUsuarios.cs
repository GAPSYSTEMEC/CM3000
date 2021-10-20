using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using His.Entidades;
using His.Datos;

namespace His.Negocio
{
    public class NegUsuarios
    {
        #region Consultas
        public static void CrearUsuarioPerfiles(USUARIOS_PERFILES upNuevo)
        {
            new DatUsuarios().CrearUsuarioPerfiles(upNuevo);
        }
        public static void EliminaUsuarioPerfiles(List<USUARIOS_PERFILES> upModificada, List<USUARIOS_PERFILES> upOriginal)
        {
            new DatUsuarios().EliminaUsuarioPerfiles(upModificada, upOriginal);
        }

        public static List<USUARIOS_PERFILES> ListaUsuarioPerfiles()
        {
            return new DatUsuarios().ListaUsuarioPerfiles();
        }
        public static void CrearUsuario(USUARIOS usuario)
        {
            new DatUsuarios().CrearUsuario(usuario);
        }
        public static void GrabarUsuario(USUARIOS usuarioModificada, USUARIOS usuarioOriginal)
        {
            new DatUsuarios().GrabarUsuario(usuarioModificada, usuarioOriginal);
        }
        public static void ActualizarUsuario(USUARIOS usuario)
        {
            new DatUsuarios().ActualizarUsuario(usuario);
        }
        public static void EliminarUsuario(USUARIOS usuario)
        {
            new DatUsuarios().EliminarUsuario(usuario);
        }
        public static Int16 RecuperaMaximoUsuario()
        {
            return new DatUsuarios().RecuperaMaximoUsuario();
        }
        public static List<DtoUsuariosPerfil> ListaConsultaTablasOpciones(Int16 codusuario)
        {
            return new DatUsuarios().ListaConsultaTablasOpciones(codusuario);
        }
        public static USUARIOS RecuperaUsuario(Int16 codusu)
        {
            return new DatUsuarios().RecuperaUsuario(codusu);
        }

        public static USUARIOS RecuperaUsuarioNombres(string Datos) // Encuentra el codigo de un usuario apartir del nombre / Giovanny Tapia /18/09/2012
        {
            return new DatUsuarios().RecuperaUsuarioNombres(Datos);
        }

        public static List<USUARIOS> RecuperaUsuarios()
        {
            return new DatUsuarios().RecuperaUsuarios();
        }
        public static List<DtoUsuarios> RecuperaUsuariosFormulario()
        {
            return new DatUsuarios().RecuperaUsuariosFormulario();
        }
        public List<DtoAccesosPorPerfil> ListaConsultaTablasOpciones(int idModulo, int idPerfil)
        {

            His.Datos.DatDatos datCliente = new His.Datos.DatDatos();
            return datCliente.ListaConsultaTablasOpciones(idModulo, idPerfil);
        }

        public DataSet RecuperarClientesDataSet()
        {
            
            His.Datos.DatDatos datCliente = new His.Datos.DatDatos();
            return datCliente.RecuperarClientesDataSet();
        }
        //public List<His.Entidades.Usuarios> RecuperarClientesClientes()
        //{
        //    His.Datos.DatDatos datCliente = new His.Datos.DatDatos();
        //    return datCliente.RecuperarClientesClientes();
        //}
        //public DataSet RecuperaUsuario(Int16 codusu)
        //{
        //    His.Datos.DatDatos datCliente = new His.Datos.DatDatos();
        //    return datCliente.RecuperaUsuario(codusu);
        //}
    
        public DataSet RecuperaNumeroControl(int a) {
            His.Datos.DatDatos datCliente = new His.Datos.DatDatos();
            return datCliente.RecuperaNumeroControl(a);
        }
        /// <summary>
        /// Verifica si existe un usuario
        /// </summary>
        /// <param name="procedimiento">nombre del procedimeinto</param>
        /// <param name="usuario">datos de usuario</param>
        /// <returns>dataset</returns>
        //public int ExisteUsuario(string procedimiento, Usuarios usuario) {
        //    His.Datos.DatDatos datCliente = new His.Datos.DatDatos();
        //    return datCliente.ExisteUsuario(procedimiento, usuario);
        //}
        public DataSet ListaConsultaTablas(string consulta) 
        {
            His.Datos.DatDatos datCliente = new His.Datos.DatDatos();
            return datCliente.ListaConsultaTablas(consulta);
        }
   
        #endregion
        #region Afectaciones
        ///// <summary>
        ///// Inserta usuarios
        ///// </summary>
        ///// <param name="procedimiento">Nombre del procedimiento</param>
        ///// <param name="usuario">Datos del ususario</param>
        //public void InsertaUsuarios(string procedimietno, Usuarios usuario) 
        //{
        //    His.Datos.DatDatos datCliente = new His.Datos.DatDatos();
        //    datCliente.InsertaUsuarios(procedimietno, usuario);
        //}
        ///// <summary>
        ///// Modifica usuarios
        ///// </summary>
        ///// <param name="procedimiento">Nombre del procedimiento</param>
        ///// <param name="usuario">Datos del ususario</param>
        //public void ModificaUsuarios(string procedimietno, Usuarios usuario)
        //{
        //    His.Datos.DatDatos datCliente = new His.Datos.DatDatos();
        //    datCliente.ModificaUsuarios(procedimietno, usuario);
        //}
        //public void AumentaNumeroControl(int a) {
        //    His.Datos.DatDatos datCliente = new His.Datos.DatDatos();
        //    datCliente.AumentaNumeroControl(a);
        //}
        /// <summary>
        /// inserta campos en una tabla cualquiera
        /// </summary>
        /// <param name="procedimiento">nombre del procedimiento</param>
        /// <param name="Args">argumentos del sp</param>
        public void InsertaenTabla(string procedimiento, List<object> Args) 
        {
            His.Datos.DatDatos datCliente = new His.Datos.DatDatos();
            datCliente.InsertaenTabla(procedimiento, Args);
        }
        #endregion
        /// <summary>
        /// Metodo que valida el login y password  de un usuario
        /// </summary>
        /// <param name="usr">login</param>
        /// <param name="pwd">clave</param>
        /// <returns>Si/No el usuario existe</returns>
        public static USUARIOS  ValidarUsuario(string usr, string pwd)
        {
            try
            {
                USUARIOS  usuario;
                usuario = new DatUsuarios().ValidarUsuario(usr,pwd);
                return usuario;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }

        public static USUARIOS_PERFILES perfilUsuario(int codUsuario)
        {
            return new DatUsuarios().perfilUsuario(codUsuario);
        }
        public static USUARIOS RecuperarUsuarioID(int codUsuario)
        {
            return new DatUsuarios().RecuperarUsuarioID(codUsuario);
        }

        public static DataTable RecuperaUsuariosCajeros()
        {
            return new DatUsuarios().RecuperaUsuariosCajeros();
        }
        public static DataTable NickName()
        {
            return new DatUsuarios().NickName();
        }

        public static DataTable ConsultaDepartamento(int dep)
        {
            return new DatUsuarios().ConsultaDepartamento(dep);
        }
        public static int ConsultaUsuario(string cedula)
        {
            return new DatUsuarios().ConsultaUsuario(cedula);
        }

        public static DataTable ConsultaUsuarioDep(string cedula)
        {
            return new DatUsuarios().ConsultaUsuarioDep(cedula);
        }
    }
    
}
