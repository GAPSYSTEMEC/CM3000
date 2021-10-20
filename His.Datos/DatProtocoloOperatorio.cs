using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;
using System.Data.SqlClient;
using System.Data;

namespace His.Datos
   
{
    public class DatProtocoloOperatorio
    {
        /// <summary>
        /// Método que recupera el último Codido de la tabla HC_PROTOCOLO_OPERATORIO de Base de Datos
        /// </summary>
        /// <returns>Código HC_PROTOCOLO_OPERATORIO</returns>
        public int ultimoCodigo()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from d in contexto.HC_PROTOCOLO_OPERATORIO
                             select d.PROT_CODIGO).ToList();

                if (lista.Count > 0)
                    return lista.Max();

                return 0;
            }
        }
              
        /// <summary>
        /// Método que permite Crear un Elemeto en la tabla HC_PROTOCOLO_OPERATORIO de la Base de Datos
        /// </summary>
        /// <param name="protocolo">Recibe como parametro un Objeto HC_PROTOCOLO_OPERATORIO</param>
        public void crearProtocolo(HC_PROTOCOLO_OPERATORIO protocolo)
        {          
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    contexto.Crear("HC_PROTOCOLO_OPERATORIO", protocolo);
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void GuardarHoraAnestesia(int prot_codigo,string hora)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            try
            {
                connection = obj.ConectarBd();
                command = new SqlCommand("update HC_PROTOCOLO_OPERATORIO set PROT_HORA_ANESTESIA = @hora where PROT_CODIGO = @protcodigo", connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@hora", hora);
                command.Parameters.AddWithValue("@protcodigo", prot_codigo);
                command.CommandTimeout = 180;
                connection.Open();
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                connection.Close();
            }catch(Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        public string RecupararHoraAnestesia(int prot_codigo, int  ate_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            string horaanestesia = "";
            BaseContextoDatos obj = new BaseContextoDatos();
            try
            {
                connection = obj.ConectarBd();
                command = new SqlCommand("select PROT_HORA_ANESTESIA from HC_PROTOCOLO_OPERATORIO where ATE_CODIGO = @ate_codigo and ADF_CODIGO = @protcodigo", connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
                command.Parameters.AddWithValue("@protcodigo", prot_codigo);
                command.CommandTimeout = 180;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    horaanestesia = reader["PROT_HORA_ANESTESIA"].ToString();
                }
                reader.Close();
                command.Parameters.Clear();
                connection.Close();
                return horaanestesia;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return horaanestesia;
            }
        }
        /// <summary>
        /// Método que permite actualizar los datos en la tabla HC_PROTOCOLO_OPERATORIO 
        /// según el Protocolo Operatorio Modificado
        /// </summary>
        /// <param name="protocolo">Recibe como parametro un Objeto HC_PROTOCOLO_OPERATORIO</param>
        public void actualizarProtocolo(HC_PROTOCOLO_OPERATORIO protocolo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                HC_PROTOCOLO_OPERATORIO protocoloDestino = contexto.HC_PROTOCOLO_OPERATORIO.FirstOrDefault(p => p.PROT_CODIGO == protocolo.PROT_CODIGO);
                protocoloDestino.PROT_SERVICIO = protocolo.PROT_SERVICIO;
                protocoloDestino.PROT_SALA = protocolo.PROT_SALA;
                protocoloDestino.PROT_PREOPERATORIO = protocolo.PROT_PREOPERATORIO;
                protocoloDestino.PROT_POSTOPERATORIO = protocolo.PROT_POSTOPERATORIO;
                protocoloDestino.PROT_PROYECTADA = protocolo.PROT_PROYECTADA;
                protocoloDestino.PROT_ELECTIVA = protocolo.PROT_ELECTIVA;
                protocoloDestino.PROT_EMERGENTE = protocolo.PROT_EMERGENTE;
                protocoloDestino.PROT_PALEATIVA = protocolo.PROT_PALEATIVA;
                protocoloDestino.PROT_REALIZADO = protocolo.PROT_REALIZADO;
                protocoloDestino.PROT_CIRUJANO = protocolo.PROT_CIRUJANO;
                protocoloDestino.PROT_PAYUDANTE = protocolo.PROT_PAYUDANTE;
                protocoloDestino.PROT_SAYUDANTE = protocolo.PROT_SAYUDANTE;
                protocoloDestino.PROT_TAYUDANTE = protocolo.PROT_TAYUDANTE;
                protocoloDestino.PROT_INSTRUMENTISTA = protocolo.PROT_INSTRUMENTISTA;
                protocoloDestino.PROT_CIRCULANTE = protocolo.PROT_CIRCULANTE;
                protocoloDestino.PROT_ANESTESISTA = protocolo.PROT_ANESTESISTA;
                protocoloDestino.PROT_AYUANESTESIA = protocolo.PROT_AYUANESTESIA;
                protocoloDestino.PROT_FECHA = protocolo.PROT_FECHA;
                protocoloDestino.PROT_HORAINICIO = protocolo.PROT_HORAINICIO;
                protocoloDestino.PROT_HORAFIN = protocolo.PROT_HORAFIN;
                protocoloDestino.PROT_TIPOANEST = protocolo.PROT_TIPOANEST;
                protocoloDestino.PROT_DIERESIS = protocolo.PROT_DIERESIS;
                protocoloDestino.PROT_EXPOSICION = protocolo.PROT_EXPOSICION;
                protocoloDestino.PROT_EXPLORACION = protocolo.PROT_EXPLORACION;
                protocoloDestino.PROT_PROCEDIMIENTO = protocolo.PROT_PROCEDIMIENTO;
                protocoloDestino.PROT_SINTESIS = protocolo.PROT_SINTESIS;
                protocoloDestino.PROT_COMPLICACIONES = protocolo.PROT_COMPLICACIONES;
                protocoloDestino.PROT_EXAMENHIS = protocolo.PROT_EXAMENHIS;
                protocoloDestino.PROT_DIAGNOSTICOH = protocolo.PROT_DIAGNOSTICOH;
                protocoloDestino.PROT_DICTADO = protocolo.PROT_DICTADO;
                protocoloDestino.PROT_FECHADIC = protocolo.PROT_FECHADIC;
                protocoloDestino.PROT_HORADIC = protocolo.PROT_HORADIC;
                protocoloDestino.PROT_ESCRITA = protocolo.PROT_ESCRITA;
                protocoloDestino.PROT_PROFESIONAL = protocolo.PROT_PROFESIONAL;
                contexto.SaveChanges();
            }
        }

        /// <summary>
        /// Método que permite recuperar Protocolo Operatorio según la atención
        /// </summary>
        /// <param name="codAtencion">Recibe como parametro el Código de Atención</param>
        /// <returns>Retorna un objeto HC_PROTOCOLO_OPERATORIO</returns>
        public HC_PROTOCOLO_OPERATORIO recuperarProtocolo(int codAtencion, int CodigoProtocolo)
        {
            HC_PROTOCOLO_OPERATORIO protocolo;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                protocolo = (from e in contexto.HC_PROTOCOLO_OPERATORIO
                             where e.ATENCIONES.ATE_CODIGO == codAtencion && e.ADF_CODIGO == CodigoProtocolo
                             select e).FirstOrDefault();

                return protocolo;
            }
        }

        public HC_PROTOCOLO_OPERATORIO recuperarProtocolo(int codAtencion)
        {
            HC_PROTOCOLO_OPERATORIO protocolo;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                protocolo = (from e in contexto.HC_PROTOCOLO_OPERATORIO
                             where e.ATENCIONES.ATE_CODIGO == codAtencion
                             select e).FirstOrDefault();

                return protocolo;
            }
        }

        public DataTable ProtocolosEpicrisis(Int64 ate_codigo)
        {
            SqlCommand command;
            SqlDataReader reader;
            SqlConnection connection;
            DataTable Tabla = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();

            connection.Open();
            command = new SqlCommand("sp_ProtocoloEpicrisis", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            connection.Close();

            return Tabla;
        }

    }
}
