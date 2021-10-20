using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;
using His.Entidades;

namespace His.Negocio
{
    public class NegPerfil
    {
        public static Int16 RecuperaMaximoPerfil()
        {
            return new DatPerfiles().RecuperaMaximoPerfil();
        }
        public static List<PERFILES> RecuperaPerfiles()
        {
            return new DatPerfiles().RecuperaPerfiles();
        }
        public static void CrearPerfil(PERFILES perfil)
        {
            new DatPerfiles().CrearPerfil(perfil);

        }
        public static void GrabarPerfil(PERFILES perfilModificada, PERFILES perfilOriginal)
        {
            new DatPerfiles().GrabarPerfil(perfilModificada, perfilOriginal);

        }
        public static void EliminarPerfil(PERFILES perfil)
        {
            new DatPerfiles().EliminarPerfil(perfil);
        }
        /// <summary>
        /// Metodo que retorna el perfil de un usuario
        /// </summary>
        /// <param name="codigoUsuario">Codigo de usuario</param>
        /// <returns>Objeto PERFILES</returns>
        public List<PERFILES> RecuperarPerfil(int codigoUsuario)
        {
            return new DatPerfiles().RecuperarPerfil(codigoUsuario); 
        }
    }
}
