using Core.Datos;
using His.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace His.Datos
{
    public class DatMaintenance
    {

        public void setROW(string tabla, object[] values, string code = "")
        {
            string query = "";
            switch (tabla)
            {
                case "SetTipoIngreso":

                    if (values[0].ToString() == "-1")
                    {
                        query = "INSERT INTO TIPO_INGRESO(TIP_CODIGO,TIP_DESCRIPCION,TIP_ESTADO)  VALUES((SELECT MAX(TIP_CODIGO)+1 FROM TIPO_INGRESO) ,'" + values[1].ToString() + "'," + values[2].ToString() + " )";
                    }
                    else
                    {
                        query = "update TIPO_INGRESO set TIP_DESCRIPCION = '" + values[1].ToString() + "' \n" +
                                     ", TIP_ESTADO = " + values[2].ToString() + " where TIP_CODIGO=" + values[0].ToString() + " ";
                    }
                    break;

                default:
                    query = ("Nothing");
                    break;
            }
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlConnection Sqlcon = obj.ConectarBd();
            Sqlcon.Open();
            SqlCommand Sqlcmd = new SqlCommand(query, Sqlcon);

            try
            {
                Sqlcmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


        public object getQuery(string tabla, int codigo = 0)
        {
            object query = null;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                switch (tabla)
                {

                    case "ParametrosArchivos":
                        query = (from c in contexto.PARAMETROS_DETALLE
                                 where (c.PARAMETROS.PAR_CODIGO) == codigo
                                 select c).ToList();
                        break;
                    case "POR_FTP":
                        query = (from X in contexto.PARAMETROS_DETALLE
                                 where (X.PAD_NOMBRE) == "CONEXION FTP"
                                 select X).ToList();
                        break;
                    case "Parametros":
                        query = (from c in contexto.PARAMETROS
                                 select c).ToList();
                        break;
                    case "Ingreso":
                        query = (from c in contexto.TIPO_INGRESO
                                 select c).ToList();
                        break;

                    default:
                        query = ("Nothing");
                        break;
                }


            }
            return query;
        }



        public void setQuery(string tabla, PARAMETROS_DETALLE pd)
        {


            //using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            //{

            //            //var query = (from c in contexto.PARAMETROS_DETALLE
            //            //         where (c.PARAMETROS.PAR_CODIGO) == pd.PAD_CODIGO
            //            //         select c).FirstOrDefault();
            //            //query.PAD_VALOR = pd.PAD_VALOR;
            //            contexto.AttachUpdated(pd);
            //            contexto.SaveChanges();

            //}

            string cadena_sql;

            switch (tabla)
            {

                case "DetalleParametros":
                    cadena_sql = ("update PARAMETROS_DETALLE set pad_valor='" + pd.PAD_VALOR + "' where PAD_CODIGO=" + pd.PAD_CODIGO + " ");
                    break;



                default:
                    cadena_sql = ("");
                    break;
            }



            BaseContextoDatos obj = new BaseContextoDatos();
            SqlConnection Sqlcon = obj.ConectarBd();
            Sqlcon.Open();
            SqlCommand Sqlcmd = new SqlCommand(cadena_sql, Sqlcon);

            try
            {
                Sqlcmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //throw ex;
                Console.WriteLine(ex.Message);

            }





        }

        public DataTable getDataTable(string tabla)
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
            string query = "";
            switch (tabla)
            {

                case "CatalogoCostos":
                    query = ("SELECT dbo.CATALOGO_COSTOS.CAC_CODIGO AS CODIGO, dbo.CATALOGO_COSTOS.CAC_NOMBRE AS CATALOGO, dbo.CATALOGO_COSTOS_TIPO.CCT_NOMBRE AS TIPO\n" +
                                        "FROM dbo.CATALOGO_COSTOS INNER JOIN dbo.CATALOGO_COSTOS_TIPO ON dbo.CATALOGO_COSTOS.CCT_CODIGO = dbo.CATALOGO_COSTOS_TIPO.CCT_CODIGO ORDER BY CODIGO");
                    break;
                case "TipoIngreso":
                    query = ("SELECT TIP_CODIGO,TIP_DESCRIPCION,TIP_ESTADO from TIPO_INGRESO ORDER BY 1");
                    break;
                case "Usuarios":
                    query = ("select ID_USUARIO AS ID, CONCAT(APELLIDOS,' ',NOMBRES) AS NOMBRE from USUARIOS order by 2");
                    break;
                case "TipoCatalogoCostos":
                    query = ("SELECT [CCT_CODIGO] as CODIGO ,[CCT_NOMBRE] AS TIPO  FROM [His3000].[dbo].[CATALOGO_COSTOS_TIPO] order by [CCT_NOMBRE]");
                    break;
                case "TiposAtenciones":
                    query = ("SELECT        dbo.tipos_atenciones.id AS CODIGO,dbo.tipos_atenciones.name AS DESCRIPCION ,  dbo.TIPO_INGRESO.TIP_DESCRIPCION AS INGRESO " +
                               " FROM dbo.TIPO_INGRESO right JOIN dbo.tipos_atenciones ON dbo.TIPO_INGRESO.TIP_CODIGO = dbo.tipos_atenciones.list");
                    break;
                case "TiposIngresos":
                    query = ("select * from TIPO_INGRESO");
                    break;
                case "DescripcionCatalogoCostos":
                    query = ("select despro as PRODUCTO from (select sp.despro from sic3000..PRODUCTO sp inner join His3000..PRODUCTO hp on hp.PRO_DESCRIPCION=sp.despro) tabla " +
                             " left join (select CAC_NOMBRE from His3000..CATALOGO_COSTOS) hcc on tabla.despro = hcc.CAC_NOMBRE where hcc.CAC_NOMBRE is null");
                    break;

                default:
                    query = ("Nothing");
                    break;
            }



            Sqlcmd = new SqlCommand(query, Sqlcon);
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

        public bool delete(string tabla, int codigo)
        {
            string cadena_sql;



            switch (tabla)
            {

                case "CatalogoCostos":
                    cadena_sql = ("DELETE FROM [dbo].[CATALOGO_COSTOS] WHERE CAC_CODIGO= " + codigo + " ");
                    break;

                case "TipoCatalogoCostos":
                    cadena_sql = ("DELETE  FROM [His3000].[dbo].[CATALOGO_COSTOS_TIPO]  WHERE [CCT_CODIGO]= " + codigo + " ");
                    break;


                case "TipoAtencion":
                    cadena_sql = ("DELETE  FROM [His3000].[dbo].[tipos_atenciones]  WHERE [id]= " + codigo + " ");
                    break;
                case "TipoIngreso":
                    cadena_sql = ("DELETE  FROM [His3000].[dbo].[TIPO_INGRESO]  WHERE [TIP_CODIGO]= " + codigo + " ");
                    break;
                default:
                    cadena_sql = ("");
                    break;
            }



            BaseContextoDatos obj = new BaseContextoDatos();
            SqlConnection Sqlcon = obj.ConectarBd();
            Sqlcon.Open();
            SqlCommand Sqlcmd = new SqlCommand(cadena_sql, Sqlcon);

            try
            {
                Sqlcmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                //throw ex;
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        public void save_CatalogoCostos(int cod, int tipo, string desc)
        {
            string cadena_sql;
            if (cod == -1)
            {
                cadena_sql = "INSERT INTO [dbo].[CATALOGO_COSTOS] ([CAC_CODIGO] ,[CCT_CODIGO] ,[CAC_NOMBRE]) VALUES ( (select MAX(cac_codigo)+1 from [CATALOGO_COSTOS]), " +
                               "" + tipo + "" +
                               ",'" + desc + "')";
            }
            else
            {
                cadena_sql = "UPDATE [dbo].[CATALOGO_COSTOS]\n" +
                               "SET [CCT_CODIGO] = " + tipo + "\n" +
                                ", [CAC_NOMBRE] = '" + desc + "'\n" +
                             "WHERE CAC_CODIGO= '" + cod + "' ";
            }
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlConnection Sqlcon = obj.ConectarBd();
            Sqlcon.Open();
            SqlCommand Sqlcmd = new SqlCommand(cadena_sql, Sqlcon);

            try
            {
                Sqlcmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void save_TipoAtencion(int cod, int tipo, string desc)
        {
            string cadena_sql;
            if (cod == -1)
            {
                cadena_sql = "INSERT INTO [dbo].[tipos_atenciones] ([name] ,[list]) VALUES('"
                               + desc + "','" + tipo + "')";
            }
            else
            {
                cadena_sql = "UPDATE [dbo].[tipos_atenciones]\n" +
                               "SET [name] = '" + desc + "'\n" +
                                ", [list] = '" + tipo + "'\n" +
                             "WHERE id= '" + cod + "' ";
            }
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlConnection Sqlcon = obj.ConectarBd();
            Sqlcon.Open();
            SqlCommand Sqlcmd = new SqlCommand(cadena_sql, Sqlcon);

            try
            {
                Sqlcmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void save_TipoCosto(int cod, string desc)
        {
            string cadena_sql;
            if (cod == -1)
            {
                cadena_sql = "INSERT INTO [dbo].[CATALOGO_COSTOS_TIPO] ([CCT_CODIGO] ,[CCT_NOMBRE]) VALUES ( (select max([CCT_CODIGO])+1 from [dbo].[CATALOGO_COSTOS_TIPO]), '" + desc + "')";
            }
            else
            {
                cadena_sql = "UPDATE [dbo].[CATALOGO_COSTOS_TIPO]\n" +
                               "SET [CCT_NOMBRE] = '" + desc + "'\n" +
                             "WHERE CCT_CODIGO= '" + cod + "' ";
            }
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlConnection Sqlcon = obj.ConectarBd();
            Sqlcon.Open();
            SqlCommand Sqlcmd = new SqlCommand(cadena_sql, Sqlcon);

            try
            {
                Sqlcmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public int ultimoCodigoTipoCiudadano()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from p in contexto.TIPO_CIUDADANO
                             select p.TC_CODIGO).ToList();

                if (lista.Count > 0)
                    return lista.Max();

                return 0;
            }
        }

        public int ultimaNacionalidad()
        {
            using(var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from p in contexto.PAIS
                             select p.CODPAIS).ToList();
                if (lista.Count > 0)
                    return lista.Max();
                return 0;
            }
        }
        public DataTable GetPaises()
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
            Sqlcmd = new SqlCommand("select CODPAIS as CODIGO, NOMPAIS as PAIS, NACIONALIDAD, CODAREA as 'CODIGO AREA' from PAIS order by 2", Sqlcon);
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
        public void CrearCiudadano(TIPO_CIUDADANO ciudadano)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    contexto.AddToTIPO_CIUDADANO(ciudadano);
                    contexto.SaveChanges();
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
            }
        }
        public void CreaNacionalidad(PAIS paises)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    contexto.AddToPAIS(paises);
                    contexto.SaveChanges();
                }
            }
            catch (Exception error)
            {
                Console.Write(error);
            }
        }
        public void ModificarTipoCiudadano(int tc_codigo, string tc_descripcion)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                TIPO_CIUDADANO ciudadano = contexto.TIPO_CIUDADANO.FirstOrDefault(p => p.TC_CODIGO == tc_codigo);
                ciudadano.TC_DESCRIPCION = tc_descripcion;
                contexto.SaveChanges();
            }
        }
        public void EditarNacionalidad(short codigo, string pais, string nacionalidad)
        {
            using(var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                PAIS paises = contexto.PAIS.FirstOrDefault(p => p.CODPAIS == codigo);
                paises.NOMPAIS = pais;  paises.NACIONALIDAD = nacionalidad;
                contexto.SaveChanges();
            }
        }

        public void EliminarTipoCiudadano(int tc_codigo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                TIPO_CIUDADANO ciudadano = contexto.TIPO_CIUDADANO.SingleOrDefault(x => x.TC_CODIGO == tc_codigo);
                if (ciudadano != null)
                {
                    contexto.DeleteObject(ciudadano);
                    contexto.SaveChanges();
                }
            }
        }
        public void EliminarNacionalidad(short codigo)
        {
            using(var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                PAIS paises = contexto.PAIS.SingleOrDefault(x => x.CODPAIS == codigo);
                if(paises != null)
                {
                    contexto.DeleteObject(paises);
                    contexto.SaveChanges();
                }
            }
        }
        public DataTable GetCiudadano()
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
            Sqlcmd = new SqlCommand("SELECT TC_CODIGO AS CODIGO, TC_DESCRIPCION AS DESCRIPCION FROM TIPO_CIUDADANO", Sqlcon);
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
        public static bool validadorEmail(string correo)
        {
            bool ok = false;
            try
            {
                String expresion;
                expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
                if (Regex.IsMatch(correo, expresion))
                {
                    if (Regex.Replace(correo, expresion, String.Empty).Length == 0)
                    {
                        ok = true;
                    }
                    else
                    {
                        ok = false;
                    }
                }
                else
                {
                    ok = false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return ok;
        }
        public void EnviarCorreo(int cat_codigo, DateTime fechacaducar, string cat_nombre, bool enviar)
        {
            SqlConnection connection;
            SqlCommand command;
            DataTable Dts = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            string mail = "";
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "select CCE_CORREORECIBE from CONVENIO_CORREO_ENVIA";
            command.CommandType = CommandType.Text;
            SqlDataReader reader1 = command.ExecuteReader();
            if(reader1.Read() == true)
            {
                mail = reader1.GetString(0);
            }
            reader1.Close();


            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "select * from CONVENIO_CORREO where CAT_CODIGO =@cat_codigo and CC_FECHA_ENVIO between @fecha and GETDATE()";
            command.Parameters.AddWithValue("@cat_codigo", cat_codigo);
            command.Parameters.AddWithValue("@fecha", DateTime.Now.Date);
            command.CommandType = CommandType.Text;

            SqlDataReader reader = command.ExecuteReader();
            bool caduco = false;
            int dias = DateTime.Now.Day - fechacaducar.Day ; 
            if (reader.Read() != true)
            {
                foreach (var address in mail.Split(';'))
                {
                    if (address != "")
                        if (validadorEmail(address.Trim()))
                        {
                            if (fechacaducar > DateTime.Now)
                            {
                                DatSoporteMail mailService = new DatSoporteMail();
                                mailService.sendMail(
                                  subject: "HIS3000 : CADUCIDAD DE CONVENIO - " + cat_nombre,
                                          body: "Se comunica que el convenio " + cat_nombre + " esta próximo a caducar en " + Math.Abs(dias) + "día(s) , por favor ponerse en contacto con el encargado de los convenios o consulte con el administrador.\r\nGRACIAS POR USAR NUESTRO SISTEMA",
                                          recipientMail: new List<string> { address }
                                          );
                                caduco = true;
                            }
                            else
                            {
                                DatSoporteMail mailService = new DatSoporteMail();
                                mailService.sendMail(
                                  subject: "HIS3000 : CADUCIDAD DE CONVENIO - " + cat_nombre,
                                          body: "Se comunica que el convenio " + cat_nombre + " esta próximo a caducar en " + Math.Abs(dias) + "día(s) , por favor ponerse en contacto con el encargado de los convenios o consulte con el administrador.\r\nGRACIAS POR USAR NUESTRO SISTEMA",
                                          recipientMail: new List<string> { address }
                                          );
                                caduco = true;
                            }
                        }
                }

            }
            reader.Close();
            command.Parameters.Clear();


            if(caduco == true && enviar == true)
            {
                command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "INSERT INTO CONVENIO_CORREO VALUES(@cat_codigo, @cat_nombre, @fechahoy)";
                command.Parameters.AddWithValue("@cat_codigo", cat_codigo);
                command.Parameters.AddWithValue("@cat_nombre", cat_nombre);
                command.Parameters.AddWithValue("@fechahoy", DateTime.Now);
                command.CommandType = CommandType.Text;
                command.CommandTimeout = 180;
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                connection.Close();
            }
            else
                connection.Close();
        }
        public DataTable ConveniosPorCaducar(DateTime FechaInicio, DateTime FechaFin)
        {
            DataTable TablaConvenios = new DataTable();
            SqlConnection connection;
            SqlCommand command;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "select CAT_CODIGO, CAT_NOMBRE, CAT_FECHA_FIN from CATEGORIAS_CONVENIOS where CAT_FECHA_FIN BETWEEN @fechainicio AND @fechafin AND CAT_ESTADO = 1";
            command.Parameters.AddWithValue("@fechainicio", FechaInicio);
            command.Parameters.AddWithValue("@fechafin", FechaFin);
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            SqlDataReader reader = command.ExecuteReader();
            TablaConvenios.Load(reader);
            try
            {
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return TablaConvenios;
        }
    }
}
