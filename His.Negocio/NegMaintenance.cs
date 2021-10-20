using His.Datos;
using His.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace His.Negocio
{
    public class NegMaintenance
    {
        public static DataTable getDataTable(string Tabla)
        {
            return new DatMaintenance().getDataTable(Tabla);
        }

        public static object getQuery(string tabla, int codigo = 0)
        {
            return new DatMaintenance().getQuery(tabla, codigo);
        }

        public static bool delete(string Tabla, int cod)
        {
            return new DatMaintenance().delete(Tabla, cod);
        }


        public static void setQuery(string tabla, PARAMETROS_DETALLE x)
        {
            new DatMaintenance().setQuery(tabla, x);
        }

        public static void save_CatalogoCostos(int cod, int tipo, string desc)
        {
            new DatMaintenance().save_CatalogoCostos(cod, tipo, desc);
        }

        public static void save_tipoAtencion(int cod, int tipo, string desc)
        {
            new DatMaintenance().save_TipoAtencion(cod, tipo, desc);
        }

        public static void setROW(string tabla, object[] values, string code = "")
        {
            new DatMaintenance().setROW(tabla, values, code);
        }
        public static void save_TipoCosto(int cod,  string desc)
        {
            new DatMaintenance().save_TipoCosto(cod, desc);
        }

        public static int ultimoCodigoTipoCiudadano()
        {
            return new DatMaintenance().ultimoCodigoTipoCiudadano();
        }
        public static int ultimaNacionalidad()
        {
            return new DatMaintenance().ultimaNacionalidad();
        }
        public static void CrearTipoCiudadano(TIPO_CIUDADANO ciudadano)
        {
            new DatMaintenance().CrearCiudadano(ciudadano);
        }
        public static void ModificarTipoCiudadano(string tc_codigo, string tc_descripcion)
        {
            new DatMaintenance().ModificarTipoCiudadano(Convert.ToInt32(tc_codigo), tc_descripcion);
        }
        public static void CrearNacionalidad(PAIS paises)
        {
            new DatMaintenance().CreaNacionalidad(paises);
        }
        public static void EditarNacionalidad(short codigo, string pais, string nacionalidad)
        {
            new DatMaintenance().EditarNacionalidad(codigo, pais, nacionalidad);
        }
        public static DataTable GetCiudadanos()
        {
            return new DatMaintenance().GetCiudadano();
        }
        public static DataTable GetNacionalidad()
        {
            return new DatMaintenance().GetPaises();
        }

        public static DataTable ConveniosPorCaducar(DateTime FechaInicio, DateTime FechaFin)
        {
            return new DatMaintenance().ConveniosPorCaducar(FechaInicio, FechaFin);
        }
        public static void EliminarTipoCiudadano(string tc_codigo)
        {
            new DatMaintenance().EliminarTipoCiudadano(Convert.ToInt32(tc_codigo));
        }
        public static void EliminarNacionalidad(string codigo)
        {
            new DatMaintenance().EliminarNacionalidad(Convert.ToInt16(codigo));
        }
    }
}
