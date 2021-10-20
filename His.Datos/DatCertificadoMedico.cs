using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Core.Datos;

namespace His.Datos
{
    public class DatCertificadoMedico
    {
        

        public DataTable BuscarPaciente(Int64 ate_codigo)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_CertificadoPaciente";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable Medico_Paciente()
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_CertificadoMedicoPaciente";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }

        public DataTable CertificadosMedicos(DateTime fechainicio, DateTime fechafin, bool estado)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_CertificadosMedicos";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@fechainicio", fechainicio);
            command.Parameters.AddWithValue("@fechafin", fechafin);
            command.Parameters.AddWithValue("@estado", estado);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }

        public DataTable CertificadoXmedicos(DateTime fechainicio, DateTime fechafin, int codMedico, bool estado)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_CertificadosXmedico";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@fechainicio", fechainicio);
            command.Parameters.AddWithValue("@fechafin", fechafin);
            command.Parameters.AddWithValue("@idMedico", codMedico);
            command.Parameters.AddWithValue("@estado", estado);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public void InsertarCertificado(int ate_codigo, int med_codigo, string observacion, int reposo
            ,string actividad, string contingencia, string tratamiento, string procedimiento, int ingreso, DateTime fechaCirugia)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            BaseContextoDatos obj = new BaseContextoDatos();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_Certificado_InsertarPaciente";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@med_codigo", med_codigo);
            command.Parameters.AddWithValue("@observacion", observacion);
            command.Parameters.AddWithValue("@reposo", reposo);
            command.Parameters.AddWithValue("@actividad", actividad);
            command.Parameters.AddWithValue("@contingencia", contingencia);
            command.Parameters.AddWithValue("@tratamiento", tratamiento);
            command.Parameters.AddWithValue("@procedimiento", procedimiento);
            command.Parameters.AddWithValue("@ingreso",ingreso);
            command.Parameters.AddWithValue("@fechaCirugia", fechaCirugia);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public void InsertarCertificadoDetalle(string cie_codigo)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_Certificado_InsertarDetallePaciente";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@cie_codigo", cie_codigo);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public DataTable CargarDatosCertificado(int ate_codigo)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_Certificado_Mostrar";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable CargarDatosCertificado_Detalle(Int64 cer_codigo)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_Certificado_Mostrar_Detalle";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@cer_codigo", cer_codigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public string PathImagen()
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            BaseContextoDatos obj = new BaseContextoDatos();
            string path = "";
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "SELECT EMP_PATHIMAGEN FROM EMPRESA";
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())//Obtenemos los datos de la columna y asignamos a los campos de la Cache de Usuario
                {
                    path = reader.GetString(0);
                }
                conexion.Close();
                return path;
            }
            else
            {
                conexion.Close();
                return path;
            }
        }

        public string PathImagenPre()
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            BaseContextoDatos obj = new BaseContextoDatos();
            string path = "";
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "SELECT EMP_CONTADOR_DIRECCION FROM EMPRESA";
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())//Obtenemos los datos de la columna y asignamos a los campos de la Cache de Usuario
                {
                    path = reader.GetString(0);
                }
                conexion.Close();
                return path;
            }
            else
            {
                conexion.Close();
                return path;
            }
        }
        public DataTable CargarCie10Hosp(Int64 ate_codigo)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_CertificadoCie10Hosp";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable CargarCie10Emerg(Int64 ate_codigo)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_CertificadoCie10Emerg";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable CargarCie10Consulta(Int64 ate_codigo)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_CertificadoCie10Consulta";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }

        public DataTable CargarHoras()
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "SELECT * FROM MEDICAMENTOS_HORAS";
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.ExecuteNonQuery();
            conexion.Close();
            return Tabla;
        }
        public DataTable CargarDias()
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "SELECT * FROM MEDICAMENTOS_DIAS";
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.ExecuteNonQuery();
            conexion.Close();
            return Tabla;
        }
        public DataTable CargarTipoContingencia()
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "select TC_CODIGO AS CODIGO, TC_DESCRIPCION AS DESCRIPCION from TIPO_CONTINGENCIA";
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.ExecuteNonQuery();
            conexion.Close();
            return Tabla;
        }

        public DataTable ReimpresionCertificado(int cer_codigo)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "select * from CERTIFICADO_MEDICO CM INNER JOIN CERTIFICADO_MEDICO_DETALLE CMD ON CM.CER_CODIGO = CMD.CER_CODIGO where CM.CER_CODIGO = @cer_codigo";
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@cer_codigo", cer_codigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.ExecuteNonQuery();
            conexion.Close();
            return Tabla;
        }

        public bool InhabilitaCertificado( string motivo, string medico, Int32 codigoCertificado)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = " UPDATE CERTIFICADO_MEDICO SET CER_ESTADO =0, CER_OBSERVACION += " + "' | " + motivo + "' WHERE CER_CODIGO= " + codigoCertificado;
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.ExecuteNonQuery();
            conexion.Close();
            return true;
        }

        public DataTable VerificaEstado(Int64 ateCodigo)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = " SELECT CER_ESTADO FROM CERTIFICADO_MEDICO WHERE ATE_CODIGO = " + ateCodigo;
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.ExecuteNonQuery();
            conexion.Close();
            return Tabla;
        }
    }
}
