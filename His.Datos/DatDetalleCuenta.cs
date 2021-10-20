using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;
using His.Entidades.Pedidos;
using System.Data.EntityClient;
using System.Data.Common;
using Microsoft.Data.Extensions;
using System.Data.SqlClient;
using System.Data;

namespace His.Datos
{
    public class DatDetalleCuenta
    {
        public List<PEDIDOS> recuperarPedidos(int codigoAtencion)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from p in contexto.PEDIDOS
                        where p.ATE_CODIGO == codigoAtencion
                        select p).ToList();
            }
        }

        public List<PEDIDOS_AREAS> recuperarPedidosAreas()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from p in contexto.PEDIDOS_AREAS
                        where p.PEA_ESTADO == true
                        select p).ToList();
            }
        }

        public PEDIDOS_AREAS recuperarPedidosAreas(int codPedidoA)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from p in contexto.PEDIDOS_AREAS
                        where p.PEA_CODIGO == codPedidoA
                        select p).FirstOrDefault();
            }
        }

        public List<DtoDetalleCuentaPaciente> recuperarPedidosPaciente(int codigoAtencion)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return
                        (from pa in contexto.PEDIDOS_AREAS
                         join p in contexto.PEDIDOS on pa.PEA_CODIGO equals p.PEDIDOS_AREAS.PEA_CODIGO
                         join r in contexto.RUBROS on pa.RUB_CODIGO equals r.RUB_CODIGO
                         join d in contexto.PEDIDOS_DETALLE on p.PED_CODIGO equals d.PEDIDOS.PED_CODIGO
                         where p.ATE_CODIGO == codigoAtencion
                         select new DtoDetalleCuentaPaciente
                         {
                             INDICE = p.PED_CODIGO,
                             //PEA_CODIGO = p.PEDIDOS_AREAS.PEA_CODIGO,
                             AREA = r.RUB_GRUPO,
                             SUBAREA = r.RUB_NOMBRE,
                             //PED_FECHA = p.PED_FECHA.Value,                       
                             CODIGO = (d.PRODUCTO.PRO_CODIGO).ToString(),
                             DESCRIPCION = d.PRO_DESCRIPCION,
                             CANTIDAD = d.PDD_CANTIDAD.Value,
                             VALOR = d.PDD_VALOR.Value,
                             IVA = d.PDD_IVA.Value,
                             TOTAL = d.PDD_TOTAL.Value
                         }).ToList();


                    //(from p in contexto.PEDIDOS
                    // join pa in contexto.PEDIDOS_AREAS on p.PEDIDOS_AREAS.PEA_CODIGO equals pa.PEA_CODIGO
                    // join d in contexto.PEDIDOS_DETALLE on p.PED_CODIGO equals d.PEDIDOS.PED_CODIGO                                                  
                    // where p.ATE_CODIGO == codigoAtencion
                    // select new DtoDetalleCuentaPaciente
                    // {
                    //     INDICE = p.PED_CODIGO,
                    //     //PEA_CODIGO = p.PEDIDOS_AREAS.PEA_CODIGO,
                    //     AREA = pa.PEA_NOMBRE,                             
                    //     //PED_FECHA = p.PED_FECHA.Value,                       
                    //     CODIGO = d.PRODUCTO.PRO_CODIGO,
                    //     DESCRIPCION = d.PRO_DESCRIPCION,
                    //     CANTIDAD = d.PDD_CANTIDAD.Value,
                    //     VALOR = d.PDD_VALOR.Value,
                    //     IVA = d.PDD_IVA.Value,
                    //     TOTAL = d.PDD_TOTAL.Value
                    // }).ToList();
                }
            }
            catch (Exception err) { throw err; }
        }

        /// <summary>
        /// Método que permite recuperar los Detalles de la Cuenta Paciente, según Áreas, Servicios, Productos
        /// </summary>
        /// <param name="codigoAtencion"> el parametro recibido es el código de la atención</param>
        /// <returns>una lista con los datos de la cuenta según el detalle cuenta</returns>
        /// 

        public DataTable ListaNuevos(Int64 ateCodigo)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts;
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
            Sqlcmd = new SqlCommand("sp_MuestraItemsNuevosAuditoria", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ATE_CODIGO", SqlDbType.BigInt);
            Sqlcmd.Parameters["@ATE_CODIGO"].Value = ateCodigo;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataTable();
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
        public DataTable MuestraItemsModificados(Int64 ateCodigo)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts;
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
            Sqlcmd = new SqlCommand("sp_MuestraItemsModificadosAuditoria", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ATE_CODIGO", SqlDbType.BigInt);
            Sqlcmd.Parameters["@ATE_CODIGO"].Value = ateCodigo;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataTable();
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

        public DataTable RecuperaCuentaPacinteSP()
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts;
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
            Sqlcmd = new SqlCommand("sp_RecuperarCuentaPaciente", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataTable();
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

        public List<DtoDetalleCuentaPaciente> recuperarCuentaPaciente(int codigoAtencion)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return
                        (from cp in contexto.CUENTAS_PACIENTES
                         join a in contexto.ATENCIONES on cp.ATE_CODIGO equals a.ATE_CODIGO
                         join r in contexto.RUBROS on cp.RUB_CODIGO equals r.RUB_CODIGO //aumenta la subarea en el detalle de cuentas david mantilla 04-09-2012
                         join m in contexto.MEDICOS on cp.MED_CODIGO equals m.MED_CODIGO
                         where cp.ATE_CODIGO == codigoAtencion && cp.CUE_ESTADO == 1 && cp.CUE_CANTIDAD > 0
                         select new DtoDetalleCuentaPaciente
                         {
                             INDICE = cp.CUE_CODIGO,
                             AREA = r.RUB_GRUPO,
                             SUBAREA = r.RUB_NOMBRE,
                             FECHA = cp.CUE_FECHA.Value,
                             DESCRIPCION = cp.CUE_DETALLE,
                             CODIGO = cp.PRO_CODIGO_BARRAS,
                             VALOR = cp.CUE_VALOR_UNITARIO.Value,
                             CANTIDAD = cp.CUE_CANTIDAD.Value,
                             TOTAL = cp.CUE_VALOR.Value,
                             IVA = cp.CUE_IVA.Value,
                             RUBRO = cp.RUB_CODIGO.Value,
                             RUBRO_NOMBRE = r.RUB_GRUPO,
                             MEDICO_COD = m.MED_CODIGO,
                             MEDICO_NOMBRE = m.MED_APELLIDO_PATERNO + " " + m.MED_APELLIDO_MATERNO + " " + m.MED_NOMBRE1 + " " + m.MED_APELLIDO_MATERNO,
                             NumeroControl = cp.CUE_NUM_CONTROL,
                             TipoMedico = cp.Id_Tipo_Medico.Value != null ? cp.Id_Tipo_Medico.Value : 0
                         }).ToList();
                }
            }
            catch (Exception err) { throw err; }
        }

        public int VerificaCambioCuenta(long ATE_CODIGO, string PRO_CODIGO_BARRAS, long DetalleCodigo)
        {
            // Verifico si un item tiene registrado cambios / GIOVANNY TAPIA / 20/08/2012

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
            Sqlcmd = new SqlCommand("sp_VerificaCambioCuenta", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ATE_CODIGO", SqlDbType.BigInt);
            Sqlcmd.Parameters["@ATE_CODIGO"].Value = ATE_CODIGO;

            Sqlcmd.Parameters.Add("@PRO_CODIGO_BARRAS", SqlDbType.VarChar);
            Sqlcmd.Parameters["@PRO_CODIGO_BARRAS"].Value = PRO_CODIGO_BARRAS;

            Sqlcmd.Parameters.Add("@CUE_CODIGO", SqlDbType.BigInt);
            Sqlcmd.Parameters["@CUE_CODIGO"].Value = DetalleCodigo;


            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");

            if (Dts.Tables["tabla"].Rows.Count > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public DataTable CargaItemsModificados(long ATE_CODIGO)
        {
            // Verifico si un item tiene registrado cambios / GIOVANNY TAPIA / 20/08/2012

            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts;
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
            Sqlcmd = new SqlCommand("sp_CargaCuentasModificadas", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@CodigoAtencion", SqlDbType.BigInt);
            Sqlcmd.Parameters["@CodigoAtencion"].Value = ATE_CODIGO;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataTable();
            Sqldap.Fill(Dts);

            return Dts;
        }

        public DataTable MuestraItemsModificados(long ATE_CODIGO, string PRO_CODIGO_BARRAS, long DetalleCodigo)
        {
            // Muestra los items modificados en una cuenta / GIOVANNY TAPIA / 20/08/2012

            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts;
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
            Sqlcmd = new SqlCommand("sp_MuestraItemsModificados", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ATE_CODIGO", SqlDbType.BigInt);
            Sqlcmd.Parameters["@ATE_CODIGO"].Value = ATE_CODIGO;

            Sqlcmd.Parameters.Add("@PRO_CODIGO_BARRAS", SqlDbType.VarChar);
            Sqlcmd.Parameters["@PRO_CODIGO_BARRAS"].Value = PRO_CODIGO_BARRAS;

            Sqlcmd.Parameters.Add("@CUE_CODIGO", SqlDbType.BigInt);
            Sqlcmd.Parameters["@CUE_CODIGO"].Value = DetalleCodigo;


            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataTable();
            Sqldap.Fill(Dts);

            return Dts;
        }

        public DataTable MuestraItemsModificados1(long ATE_CODIGO)
        {
            // Muestra los items modificados en una cuenta / GIOVANNY TAPIA / 20/08/2012

            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts;
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
            Sqlcmd = new SqlCommand("sp_MuestraItemsModificados1", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ATE_CODIGO", SqlDbType.BigInt);
            Sqlcmd.Parameters["@ATE_CODIGO"].Value = ATE_CODIGO;


            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataTable();
            Sqldap.Fill(Dts);

            return Dts;
        }

        public DataTable MuestraItemsNuevos(long ATE_CODIGO)
        {
            // Muestra los items modificados en una cuenta / GIOVANNY TAPIA / 20/08/2012

            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts;
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
            Sqlcmd = new SqlCommand("sp_MuestraItemsNuevos", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ATE_CODIGO", SqlDbType.BigInt);
            Sqlcmd.Parameters["@ATE_CODIGO"].Value = ATE_CODIGO;


            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataTable();
            Sqldap.Fill(Dts);

            return Dts;
        }

        public DataTable ListaItemsEliminadosCuenta(long CodigoCuenta)
        {
            // Lista todos los items eliminados de una cuenta / GIOVANNY TAPIA / 20/08/2012

            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts;
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
            Sqlcmd = new SqlCommand("sp_VerificaItemsEliminadosCuenta", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@ATE_CODIGO", SqlDbType.BigInt);
            Sqlcmd.Parameters["@ATE_CODIGO"].Value = CodigoCuenta;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataTable();
            Sqldap.Fill(Dts);

            return Dts;

        }

        public DataTable RecuperaOtroCliente(string Ruc)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts;
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
            Sqlcmd = new SqlCommand("sp_RecuperaOtroPaciente", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Ruc", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Ruc"].Value = Ruc;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataTable();
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

        public DataTable RecuperaClienteSIC(string Nombre)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts;
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
            Sqlcmd = new SqlCommand("sp_RecuperaClienteSIC", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Nombre", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Nombre"].Value = Nombre;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataTable();
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

        public DataTable ReferidoPaciente(Int64 ate_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_ReferidoPaciente", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return Tabla;
        }

        public DataTable ConvenioPago(string cat_nombre)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_ConvenioPago", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@cat_nombre", cat_nombre);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return Tabla;
        }

    }
}
