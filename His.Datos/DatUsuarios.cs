using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;
using System.Data;
using System.Data.SqlClient;

namespace His.Datos
{
    public class DatUsuarios
    {

        public USUARIOS RecuperaUsuario(Int16 codusu)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from u in contexto.USUARIOS
                        where u.ID_USUARIO == codusu
                        select u).FirstOrDefault();
                //return contexto.USUARIOS.FirstOrDefault(usu => usu.ID_USUARIO == codusu);
            }

        }

        public USUARIOS RecuperaUsuarioNombres(string Datos) // Encuentra el codigo de un usuario apartir del nombre / Giovanny Tapia /18/09/2012
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from u in contexto.USUARIOS
                        where u.NOMBRES + " " + u.APELLIDOS == Datos
                        select u).FirstOrDefault();
                //return contexto.USUARIOS.FirstOrDefault(usu => usu.ID_USUARIO == codusu);
            }

        }


        public List<USUARIOS> RecuperaUsuarios()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.USUARIOS.ToList();
            }
        }
        public List<DtoUsuarios> RecuperaUsuariosFormulario()
        {
            List<DtoUsuarios> usuariogrid = new List<DtoUsuarios>();
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                //var usuarios = (from u in contexto.USUARIOS
                //                join d in contexto.DEPARTAMENTOS on u.DEPARTAMENTOS.DEP_CODIGO equals d.DEP_CODIGO
                //                select new
                //                {
                //                    //ENTITYSETNAME = u.EntityKey.GetFullEntitySetName(),
                //                    //ENTITYID= u.EntityKey.EntityKeyValues[0].Value,
                //                    u.APELLIDOS,
                //                    d.DEP_CODIGO,
                //                    u.DIRECCION,
                //                    u.ESTADO,
                //                    u.FECHA_INGRESO,
                //                    u.FECHA_VENCIMIENTO,
                //                    u.ID_USUARIO,
                //                    u.IDENTIFICACION,
                //                    u.LOGEADO,
                //                    u.NOMBRES,
                //                    u.PWD,
                //                    u.USR
                //                }).ToList();

                List<USUARIOS> usuarios = new List<USUARIOS>();
                usuarios = contexto.USUARIOS.Include("DEPARTAMENTOS").ToList();
                foreach (var acceso in usuarios)
                {
                    usuariogrid.Add(new DtoUsuarios()
                    {
                        ENTITYSETNAME = acceso.EntityKey.GetFullEntitySetName(),
                        ENTITYID = acceso.EntityKey.EntityKeyValues[0].Key,
                        APELLIDOS = acceso.APELLIDOS,
                        DEP_CODIGO = acceso.DEPARTAMENTOS.DEP_CODIGO,

                        DIRECCION = acceso.DIRECCION,
                        ESTADO = acceso.ESTADO,

                        FECHA_INGRESO = acceso.FECHA_INGRESO,
                        FECHA_VENCIMIENTO = acceso.FECHA_VENCIMIENTO == null ? DateTime.Parse("01/01/2010") : DateTime.Parse(acceso.FECHA_VENCIMIENTO.ToString()),

                        ID_USUARIO = acceso.ID_USUARIO,
                        IDENTIFICACION = acceso.IDENTIFICACION,
                        LOGEADO = acceso.LOGEADO,
                        NOMBRES = acceso.NOMBRES,
                        PWD = acceso.PWD,
                        USR = acceso.USR,
                        Codigo_Rol = Convert.ToInt64(acceso.Codigo_Rol)
                    });
                }
                return usuariogrid;
            }
        }
        public List<DtoUsuariosPerfil> ListaConsultaTablasOpciones(Int16 codusuario)
        {
            List<DtoUsuariosPerfil> datos = new List<DtoUsuariosPerfil>();
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<PERFILES> perfiles = contexto.PERFILES.ToList();
                List<USUARIOS_PERFILES> usuarioperfil = contexto.USUARIOS_PERFILES.Where(per => per.ID_USUARIO == codusuario).ToList();
                foreach (var acceso in perfiles)
                {
                    bool valor = true;
                    if (usuarioperfil.Where(per => per.ID_PERFIL == acceso.ID_PERFIL).FirstOrDefault() == null)
                        valor = false;
                    datos.Add(new DtoUsuariosPerfil() { DESCRIPCION = acceso.DESCRIPCION, ID_PERFIL = acceso.ID_PERFIL, TIENEACCESO = valor });
                }
            }

            return datos;
        }
        public Int16 RecuperaMaximoUsuario()
        {
            Int16 maxim;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<USUARIOS> usuarios = contexto.USUARIOS.ToList();
                if (usuarios.Count > 0)
                    maxim = contexto.USUARIOS.Max(emp => emp.ID_USUARIO);
                else
                    maxim = 0;
                return maxim;
            }

        }
        public void CrearUsuario(USUARIOS usuario)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Crear("USUARIOS", usuario);
            }
        }
        public void GrabarUsuario(USUARIOS usuarioModificada, USUARIOS usuarioOriginal)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Grabar(usuarioModificada, usuarioOriginal);
            }
        }
        public void ActualizarUsuario(USUARIOS usuario)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    USUARIOS usuarioModificado = contexto.USUARIOS.FirstOrDefault(u => u.ID_USUARIO == usuario.ID_USUARIO);
                    //contexto.Attach(usuario);
                    //usuario.SetAllModified(contexto);
                    usuarioModificado.PWD = usuario.PWD;
                    contexto.SaveChanges();
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        public void EliminarUsuario(USUARIOS usuario)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Eliminar(usuario);
            }
        }
        public List<USUARIOS_PERFILES> ListaUsuarioPerfiles()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.USUARIOS_PERFILES.ToList();
            }
        }
        public void EliminaUsuarioPerfiles(List<USUARIOS_PERFILES> upModificado, List<USUARIOS_PERFILES> upOriginal)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.EliminarLista(upOriginal);
            }
        }
        public void CrearUsuarioPerfiles(USUARIOS_PERFILES upNuevo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Crear("USUARIOS_PERFILES", upNuevo);
            }
        }
        /// <summary>
        /// Metodo que valida el login y password  de un usuario
        /// </summary>
        /// <param name="usr">login</param>
        /// <param name="pwd">clave</param>
        /// <returns>Si/No el usuario existe</returns>
        public USUARIOS ValidarUsuario(string usr, string pwd)
        {
            try
            {
                using (HIS3000BDEntities contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    //USUARIOS usuario = new USUARIOS(); 
                    USUARIOS usuario = (from u in contexto.USUARIOS
                                        where u.USR == usr && u.PWD == pwd
                                        select u).FirstOrDefault();

                    //usuario =contexto.USUARIOS.Where(u => u.USR == usr && u.PWD == pwd).FirstOrDefault();
                    return usuario;
                }
            }
            catch (Exception ex)
            {
                return null;
                throw (ex);
            }
        }

        public USUARIOS_PERFILES perfilUsuario(int codUsuario)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from p in contexto.USUARIOS_PERFILES
                        join u in contexto.USUARIOS on p.ID_USUARIO equals u.ID_USUARIO
                        where u.ID_USUARIO == codUsuario
                        select p).FirstOrDefault();
            }
        }

        public USUARIOS RecuperarUsuarioID(int codUsuario)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from u in contexto.USUARIOS
                        where u.ID_USUARIO == codUsuario
                        select u).FirstOrDefault();
            }
        }

        public DataTable RecuperaUsuariosCajeros()
        {

            // PABLO ROCHA / 26/04/2013

            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Sqlcmd = new SqlCommand("SP_RetornaCajero", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;


            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);

            if (Dts.Rows.Count > 0)
            {
                return Dts;
            }
            else
            {
                return Dts;
            }


        }

        public DataTable NickName()   //Recupera todo los usuarios
        {
            SqlCommand command;
            SqlConnection connection;
            DataTable Tabla = new DataTable();
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("select USR from USUARIOS", connection);
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            connection.Close();
            return Tabla;
        }

        public DataTable ConsultaDepartamento(int dep)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Sqlcmd = new SqlCommand("select DEP_NOMBRE from DEPARTAMENTOS where DEP_CODIGO = " + dep, Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Dts;
        }

        public int ConsultaUsuario(string cedula)
        {
            USUARIOS obj = new USUARIOS();
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    obj = (from u in contexto.USUARIOS
                           where u.IDENTIFICACION == cedula
                           select u).FirstOrDefault();
                    if (obj != null)
                    {
                        return 1;
                    }
                    else
                        return 0;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable ConsultaUsuarioDep(string cedula)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Sqlcmd = new SqlCommand("select DEP_CODIGO from USUARIOS where IDENTIFICACION = '" + cedula +"'", Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Dts;
        }
    }
}
