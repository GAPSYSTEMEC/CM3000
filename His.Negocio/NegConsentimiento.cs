using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;
using System.Data;

namespace His.Negocio
{
    public class NegConsentimiento
    {
        public static void GuardarConsentimiento(Int64 ate_codigo, string servicio, string sala, string proposito1,
            string resultado1, string procedimiento, string riesgo1, string proposito2, string resultado2,
            string quirurgico, string riesgo2, string proposito3, string resultado3, string anestesia,
            string riesgo3, string fecha, string hora, string tratante, string tespecialidad, string ttelefono,
            string tcodigo, string cirujano, string cespecialidad, string ctelefono, string ccodigo,
            string anestesista, string aespecialidad, string atelefono, string acodigo, string representante,
            string parentesco, string identificacion, string telefono)
        {
            new DatHC_Consentimiento().GuardarConsentimiento(ate_codigo, servicio, sala, proposito1, resultado1,
                procedimiento, riesgo1, proposito2, resultado2, quirurgico, riesgo2, proposito3, resultado3,
                anestesia, riesgo3, Convert.ToDateTime(fecha), Convert.ToDateTime(hora), tratante, tespecialidad, ttelefono, tcodigo, 
                cirujano, cespecialidad, ctelefono, ccodigo, anestesia, aespecialidad, atelefono, acodigo, representante,
                parentesco, identificacion, telefono);
        }

        public static DataTable CargarDatos(Int64 ate_codigo)
        {
            return new DatHC_Consentimiento().CargarDatos(ate_codigo);
        }
    }
}
