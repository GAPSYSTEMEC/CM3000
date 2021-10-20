using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using System.Data.SqlClient;
using System.Data;

namespace His.Datos
{
    public class DatHC_Consentimiento
    {
        public void GuardarConsentimiento(Int64 ate_codigo, string servicio, string sala, string proposito1,
            string resultado1, string procedimiento, string riesgo1, string proposito2, string resultado2,
            string quirurgico, string riesgo2, string proposito3, string resultado3, string anestesia,
            string riesgo3, DateTime fecha, DateTime hora, string tratante, string tespecialidad, string ttelefono,
            string tcodigo, string cirujano, string cespecialidad, string ctelefono, string ccodigo,
            string anestesista, string aespecialidad, string atelefono, string acodigo, string representante,
            string parentesco, string identificacion, string telefono)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_GuardarForm024", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@servicio", servicio);
            command.Parameters.AddWithValue("@sala", sala);
            command.Parameters.AddWithValue("@proposito1", proposito1);
            command.Parameters.AddWithValue("@resultado1", resultado1);
            command.Parameters.AddWithValue("@procedimiento", procedimiento);
            command.Parameters.AddWithValue("@riesgo1", riesgo1);
            command.Parameters.AddWithValue("@proposito2", proposito2);
            command.Parameters.AddWithValue("@resultado2", resultado2);
            command.Parameters.AddWithValue("@quirurgico", quirurgico);
            command.Parameters.AddWithValue("@riesgo2", riesgo2);
            command.Parameters.AddWithValue("@proposito3", proposito3);
            command.Parameters.AddWithValue("@resultado3", resultado3);
            command.Parameters.AddWithValue("@anestesia", anestesia);
            command.Parameters.AddWithValue("@riesgo3", riesgo3);
            command.Parameters.AddWithValue("@fecha", fecha);
            command.Parameters.AddWithValue("@hora", hora);

            command.Parameters.AddWithValue("@tratante", tratante);
            command.Parameters.AddWithValue("@tespecialidad", tespecialidad);
            command.Parameters.AddWithValue("@ttelefono", ttelefono);
            command.Parameters.AddWithValue("@tcodigo", tcodigo);
            command.Parameters.AddWithValue("@cirujano", cirujano);
            command.Parameters.AddWithValue("@cespecialidad", cespecialidad);
            if(ctelefono == null)
            {
                ctelefono = "0999999999";
            }
            else
            {
                if(ctelefono.Length > 10)
                {
                    ctelefono = "0999999999";
                }
            }
            command.Parameters.AddWithValue("@ctelefono", ctelefono);
            command.Parameters.AddWithValue("@ccodigo", ccodigo);
            command.Parameters.AddWithValue("@anestesista", anestesia);
            command.Parameters.AddWithValue("@aespecialidad", aespecialidad);
            if (atelefono == null)
            {
                atelefono = "0999999999";
            }
            else
            {
                if (atelefono.Length > 10)
                {
                    atelefono = "0999999999";
                }
            }
            command.Parameters.AddWithValue("@atelefono", atelefono);
            command.Parameters.AddWithValue("@acodigo", acodigo);

            command.Parameters.AddWithValue("@representante", representante);
            command.Parameters.AddWithValue("@parentesco", parentesco);
            command.Parameters.AddWithValue("@identificacion", identificacion);
            command.Parameters.AddWithValue("@telefono", telefono);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();

        }

        public DataTable CargarDatos(Int64 ate_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();
            DataTable Tabla = new DataTable();

            command = new SqlCommand("Select * From HC_CONSENTIMIENTO_INFORMADO where ATE_CODIGO = @ate_codigo", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            SqlDataReader reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            if (Tabla.Rows.Count > 0)
            {
                return Tabla;
            }
            else
            {
                return Tabla = null;
            }
        }
    }
}
