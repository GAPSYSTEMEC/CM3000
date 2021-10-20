using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using System.Data;
using System.Data.SqlClient;

namespace His.Datos
{
    public class DatInterconsulta
    {
        public int ultimoCodigo()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                int max = contexto.HC_INTERCONSULTA.Max(x => x.HIN_CODIGO);

                return max;
            }
        }
        public int ultimoCodigoDiagnostico()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from d in contexto.HC_INTERCONSULTA_DIAGNOSTICO
                             select d.HID_CODIGO).ToList();

                if (lista.Count > 0)
                    return lista.Max();

                return 0;
            }
        }

        public void GuardarCamaInterconsulta(int hin_codigo, string cama, string medico, string med_codigo, string interconsul_id)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("UPDATE HC_INTERCONSULTA SET HIN_CAMA_NUEVO = @cama, HIN_MEDICO = @medico, HIN_MEDICO_CODIGO = @med_codigo, HIN_INTERCONSU_ID = @hin_interconsu_id WHERE HIN_CODIGO = @hin_codigo", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@hin_codigo", hin_codigo);
            command.Parameters.AddWithValue("@cama", cama);
            command.Parameters.AddWithValue("@medico", medico);
            command.Parameters.AddWithValue("@med_codigo", med_codigo);
            command.Parameters.AddWithValue("@hin_interconsu_id", interconsul_id);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();
        }

        //SE CREA EN DATATABLE POR EL HECHO DE QUE PUEDE NECESITAR ALGUNOS DATOS QUE SE CREARAN EN SU FUTURO
        public DataTable RecoverDataInterconsulta(int hin_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("SELECT * FROM HC_INTERCONSULTA WHERE HIN_CODIGO = @hin_codigo", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@hin_codigo", hin_codigo);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return Tabla;

        }
        public DataTable RecuperarDatosInterconsulta(int ate_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            DataTable Tabla = new DataTable();
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("SELECT HIN_CAMA_NUEVO AS CAMA, HIN_MEDICO AS MEDICO, HIN_MEDICO_CODIGO AS CODIGO, HIN_FECHACREACION AS FECHA FROM HC_INTERCONSULTA WHERE ATE_CODIGO = @ate_codigo", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return Tabla;
        }

        public HC_INTERCONSULTA recuperarInterconsulta(int codAtencion)
        {
            HC_INTERCONSULTA interconsulta;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                interconsulta = (from i in contexto.HC_INTERCONSULTA
                                 where i.ATENCIONES.ATE_CODIGO == codAtencion
                                 select i).FirstOrDefault();                               
                return interconsulta;
            }
        }

        public HC_INTERCONSULTA UltimarecuperarInterconsulta(int codAtencion, int hin_codigo)
        {
            HC_INTERCONSULTA interconsulta;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                interconsulta = (from i in contexto.HC_INTERCONSULTA
                                 where i.ATENCIONES.ATE_CODIGO == codAtencion && i.HIN_CODIGO == hin_codigo
                                 select i).FirstOrDefault();
                return interconsulta;
            }
        }
        public bool EstadoInterconsulta(int hin_codigo)
        {
            bool valido = false;
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("select HIN_ESTADO from HC_INTERCONSULTA where HIN_CODIGO = @hin_codigo", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@hin_codigo", hin_codigo);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                valido = Convert.ToBoolean(reader["HIN_ESTADO"].ToString());
            }
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return valido;
        }

        public void EditarEstado(int hin_codigo, bool valor)
        {
            bool valido = false;
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("UPDATE HC_INTERCONSULTA SET HIN_ESTADO = @valor where HIN_CODIGO = @hin_codigo", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@hin_codigo", hin_codigo);
            command.Parameters.AddWithValue("@valor", valor);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();
        }
        public HC_INTERCONSULTA UltimoCodigoAtencion(int ate_codigo)
        {
            HC_INTERCONSULTA objInter = new HC_INTERCONSULTA();
            using(var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                objInter = contexto.HC_INTERCONSULTA
                            .Where(x
                                  => x.ATENCIONES.ATE_CODIGO == ate_codigo)
                              .OrderByDescending(x => x.HIN_CODIGO)
                              .FirstOrDefault();
                return objInter;
            }
        }

        public List<HC_INTERCONSULTA_DIAGNOSTICO> recuperarDiagnosticosIntercIng(int codInterc)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {               
                var diagnosticos = (from i in contexto.HC_INTERCONSULTA_DIAGNOSTICO
                                    where i.HC_INTERCONSULTA.HIN_CODIGO == codInterc && i.HID_TIPO == "I"
                                    select i).ToList();
                return diagnosticos;
            }
        }

        public List<HC_INTERCONSULTA_DIAGNOSTICO> recuperarDiagnosticosIntercEgre(int codInterc)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {                
                var diagnosticos = (from i in contexto.HC_INTERCONSULTA_DIAGNOSTICO
                                    where i.HC_INTERCONSULTA.HIN_CODIGO == codInterc && i.HID_TIPO == "E"
                                    select i).ToList();
                return diagnosticos;
            }
        }

        public void actualizarInterconculta(HC_INTERCONSULTA interconsulta)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                HC_INTERCONSULTA intercDestino = contexto.HC_INTERCONSULTA.FirstOrDefault(i => i.HIN_CODIGO == interconsulta.HIN_CODIGO);
                intercDestino.HIN_ESTABLECIMIENTO_DESTINO = interconsulta.HIN_ESTABLECIMIENTO_DESTINO;
                intercDestino.HIN_SERV_CONSULTADO = interconsulta.HIN_ESTABLECIMIENTO_DESTINO;
                intercDestino.HIN_SERV_SOLICITA = interconsulta.HIN_SERV_SOLICITA;
                intercDestino.HIN_SALA = interconsulta.HIN_SALA;
                intercDestino.HIN_CAMA = interconsulta.HIN_CAMA;
                intercDestino.HIN_TIPO = interconsulta.HIN_TIPO;
                intercDestino.HIN_MED_INTERCONSULTADO = interconsulta.HIN_MED_INTERCONSULTADO;
                intercDestino.HIN_DESCRIPCION_MOTIVO = interconsulta.HIN_DESCRIPCION_MOTIVO;
                intercDestino.HIN_CUADRO_CLINICO = interconsulta.HIN_CUADRO_CLINICO;
                intercDestino.HIN_RESULTADO_EXAMENES = interconsulta.HIN_RESULTADO_EXAMENES;
                intercDestino.HIN_PLANES_TERAPEUTICOS = interconsulta.HIN_PLANES_TERAPEUTICOS;
                intercDestino.HIN_CUADRO_INTERCONSULTA = interconsulta.HIN_CUADRO_INTERCONSULTA;
                intercDestino.HIN_RESUMEN_CRITERIO = interconsulta.HIN_RESUMEN_CRITERIO;
                intercDestino.HIN_PLAN_DIAGNOSTICO = interconsulta.HIN_PLAN_DIAGNOSTICO;
                intercDestino.HIN_PLAN_TRATAMIENTO = interconsulta.HIN_PLAN_TRATAMIENTO;
                //intercDestino.PAC_CODIGO
                    //interconsulta.ATE_CODIGO
                interconsulta.ID_USUARIO = interconsulta.ID_USUARIO;
                
                contexto.SaveChanges();
            }
        }


        public void actualizarDiagnosticos(HC_INTERCONSULTA_DIAGNOSTICO diagnostico)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                HC_INTERCONSULTA_DIAGNOSTICO diagnosticoDestino = contexto.HC_INTERCONSULTA_DIAGNOSTICO.FirstOrDefault(i => i.HID_CODIGO == diagnostico.HID_CODIGO);
                diagnosticoDestino.HID_DIAGNOSTICO = diagnostico.HID_DIAGNOSTICO;
                diagnosticoDestino.HID_ESTADO = diagnostico.HID_ESTADO;
                diagnosticoDestino.HID_TIPO = diagnostico.HID_TIPO;
                diagnosticoDestino.ID_USUARIO = diagnostico.ID_USUARIO;
                diagnosticoDestino.CIE_CODIGO = diagnostico.CIE_CODIGO;
                contexto.SaveChanges();
            }
        }

       
    }
}
