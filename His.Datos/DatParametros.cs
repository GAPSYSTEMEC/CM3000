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
    public class DatParametros
    {
        public List<DtoParametros> RecuperaParametros(Int16 codigopar)
        {
            try
            {
                List<DtoParametros> parametros = new List<DtoParametros>();
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    List<PARAMETROS_DETALLE> medicos = new List<PARAMETROS_DETALLE>();
                    medicos = contexto.PARAMETROS_DETALLE.Include("PARAMETROS").Where(cod => cod.PARAMETROS.PAR_CODIGO == codigopar).ToList();
                    foreach (var acceso in medicos)
                    {
                        parametros.Add(new DtoParametros()
                        {
                            PAD_ACTIVO = acceso.PAD_ACTIVO == null ? false : bool.Parse(acceso.PAD_ACTIVO.ToString()),
                            PAD_CODIGO = acceso.PAD_CODIGO,
                            PAD_NOMBRE = acceso.PAD_NOMBRE,
                            PAD_TIPO = acceso.PAD_TIPO,
                            PAD_VALOR = acceso.PAD_VALOR,
                            PAR_CODIGO = acceso.PARAMETROS.PAR_CODIGO,

                            ENTITYSETNAME = acceso.EntityKey.GetFullEntitySetName(),
                            ENTITYID = acceso.EntityKey.EntityKeyValues[0].Key
                        });
                    }
                    return parametros;
                }
            }
            catch (Exception err)
            {
                throw err; 
            }

        }

        public DataTable RecuepraHorasyLitros(int hora)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataSet Dts;
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

            Sqlcmd = new SqlCommand("sp_RecuepraHorasyLitros", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
            Sqlcmd.Parameters.AddWithValue("@hora", hora);

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Dts.Tables["tabla"];
        }

        public bool EDITAEVOLUCION()
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            bool valido = false;
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Sqlcmd = new SqlCommand("SELECT PAD_ACTIVO FROM PARAMETROS_DETALLE WHERE PAD_CODIGO = 23 ", Sqlcon); //SIRVE PARA EDITAR LA EVOLUCION
            Sqlcmd.CommandType = CommandType.Text;
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            reader = Sqlcmd.ExecuteReader();
            while(reader.Read())
            {
                valido = Convert.ToBoolean(reader["PAD_ACTIVO"].ToString());
            }
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return valido;
        }
        public bool ParametroAdmisionDesactivacion()
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            bool valido = false;
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("sp_ParametroAdmisionDesactivar", connection);
            command.CommandType = CommandType.StoredProcedure;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                valido = Convert.ToBoolean(reader["PAD_ACTIVO"].ToString());
            }
            reader.Close();
            connection.Close();
            return valido;
        }

        public double ParametroIva()
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            double valido = 0;
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("select iva from Sic3000..Parametros", connection);
            command.CommandType = CommandType.Text;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                valido = Convert.ToDouble(reader["iva"].ToString());
            }
            reader.Close();
            connection.Close();
            return valido;
        }

        public bool ParametroDevolucionBienes()
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            bool valido = false;
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("sp_ParametroDevolucionBienes", connection);
            command.CommandType = CommandType.StoredProcedure;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                valido = Convert.ToBoolean(reader["PAD_ACTIVO"].ToString());
            }
            reader.Close();
            connection.Close();
            return valido;
        }

        public double ParametroBodegaQuirofano()
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            double valido = 0;
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("sp_ParametroBodegaQuirofano", connection);
            command.CommandType = CommandType.StoredProcedure;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                valido = Convert.ToDouble(reader["PAD_VALOR"].ToString());
            }
            reader.Close();
            connection.Close();
            return valido;
        }

        public string RutaLogo(string tipo)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            string valido = "";
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("RutasLogo", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@tipo", tipo);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                valido = reader["LEM_RUTA"].ToString();
            }
            command.Parameters.Clear();
            reader.Close();
            connection.Close();
            return valido;
        }
    }
}
