using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;

namespace His.Datos
{
    public class DatPerfiles
    {
        public Int16 RecuperaMaximoPerfil()
        {
            Int16 maxim;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<PERFILES> perfiles = contexto.PERFILES.ToList();
                if (perfiles.Count > 0)
                    maxim = contexto.PERFILES.Max(emp => emp.ID_PERFIL);
                else
                    maxim = 0;
                return maxim;
            }
            
        }
        public List<PERFILES> RecuperaPerfiles()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.PERFILES.ToList();
            }
        }
        public void CrearPerfil(PERFILES perfil)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Crear("PERFILES", perfil);

            }
        }
        public void GrabarPerfil(PERFILES perfilModificada, PERFILES perfilOriginal)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Grabar(perfilModificada, perfilOriginal);
            }
        }
        public void EliminarPerfil(PERFILES perfil)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Eliminar(perfil);
            }
        }
        /// <summary>
        /// Metodo que retorna el perfil de un usuario
        /// </summary>
        /// <param name="codigoUsuario">Codigo de usuario</param>
        /// <returns>Objeto PERFILES</returns>
        public List<PERFILES> RecuperarPerfil(int codigoUsuario)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<PERFILES> perfil = (from p in contexto.PERFILES
                                   join u in contexto.USUARIOS_PERFILES on p.ID_PERFIL equals u.ID_PERFIL
                                   join us in contexto.USUARIOS on u.ID_USUARIO equals us.ID_USUARIO
                                   where us.ID_USUARIO == codigoUsuario
                                   select p).ToList();
                return perfil; 
            }
        }
    }
}
