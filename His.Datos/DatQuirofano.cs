using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Core.Datos;

namespace His.Datos
{
    public class DatQuirofano
    {
        SqlConnection conexion;
        SqlCommand command = new SqlCommand();
        SqlDataReader reader;
        BaseContextoDatos obj = new BaseContextoDatos();

        public DataTable MostrarProductos(double codsub)
        {
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
            command.CommandText = "sp_QuirofanoProductos";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            command.Parameters.AddWithValue("@codsub", codsub);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable Patologo()
        {
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
            command.CommandText = "sp_MedicoPatologo";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }

        public DataTable MostrarGrupos()
        {
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
            command.CommandText = "select codsub as CODIGO, dessub AS DESCRIPCION from Sic3000..ProductoSubdivision WHERE Pea_Codigo_His = 1 OR Pea_Codigo_His = 13 OR Pea_Codigo_His = 27 OR Pea_Codigo_His = 19 order by 2 asc";
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable MostrarProcedimientos()
        {
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
            command.CommandText = "sp_QuirofanoProcedimientos";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable MostrarCie10()
        {
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
            command.CommandText = "SELECT CIE_CODIGO AS Codigo, CIE_DESCRIPCION AS Descripcion FROM CIE10 order by 2 asc";
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable MostrarProductosAgregados(string busqueda, bool codigo, bool descripcion)
        {
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
            if(busqueda == "")
            {
                command = new SqlCommand("SELECT QP.CODPRO AS CODIGO, P.despro AS DESCRIPCION FROM QUIROFANO_PRODUCTOS QP INNER JOIN Sic3000..Producto P ON QP.CODPRO = P.codpro order by 2 asc", conexion);
                command.CommandType = CommandType.Text;
                command.CommandTimeout = 180;
                reader = command.ExecuteReader();
                Tabla.Load(reader);
                reader.Close();
                conexion.Close();
            }
            else
            {
                if (descripcion)
                {
                    command = new SqlCommand("SELECT CODIGO, DESCRIPCION FROM VistaQuirofanoProductos WHERE DESCRIPCION like '%' + @filtro + '%' ORDER BY 2 ASC", conexion);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@filtro", busqueda);
                    command.CommandTimeout = 180;
                    reader = command.ExecuteReader();
                    Tabla.Load(reader);
                    reader.Close();
                    command.Parameters.Clear();
                    conexion.Close();
                }
                else if (codigo)
                {
                    command = new SqlCommand("SELECT CODIGO, DESCRIPCION FROM VistaQuirofanoProductos WHERE CODIGO = @filtro ORDER BY 2 ASC", conexion);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@filtro", busqueda);
                    command.CommandTimeout = 180;
                    reader = command.ExecuteReader();
                    Tabla.Load(reader);
                    reader.Close();
                    command.Parameters.Clear();
                    conexion.Close();
                }
            }
            return Tabla;
        }

        public DataTable MostrarAnestesias(string busqueda, bool codigo, bool descripcion)
        {
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
            if (busqueda == "")
            {
                command = new SqlCommand("select PCI_CODIGO as CODIGO, PCI_DESCRIPCION as DESCRIPCION from PROCEDIMIENTOS_CIRUGIA where PCI_ESTADO = 0 order by 2 asc", conexion);
                command.CommandType = CommandType.Text;
                command.CommandTimeout = 180;
                reader = command.ExecuteReader();
                Tabla.Load(reader);
                reader.Close();
                conexion.Close();
            }
            else
            {
                if (descripcion)
                {
                    command = new SqlCommand("select PCI_CODIGO as CODIGO, PCI_DESCRIPCION as DESCRIPCION from PROCEDIMIENTOS_CIRUGIA where PCI_ESTADO = 0 and PCI_DESCRIPCION like '%' + @filtro + '%' order by 2 asc", conexion);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@filtro", busqueda);
                    command.CommandTimeout = 180;
                    reader = command.ExecuteReader();
                    Tabla.Load(reader);
                    reader.Close();
                    command.Parameters.Clear();
                    conexion.Close();
                }
                else if (codigo)
                {
                    command = new SqlCommand("select PCI_CODIGO as CODIGO, PCI_DESCRIPCION as DESCRIPCION from PROCEDIMIENTOS_CIRUGIA where PCI_ESTADO = 0 and PCI_CODIGO =  @filtro order by 2 asc", conexion);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@filtro", busqueda);
                    command.CommandTimeout = 180;
                    reader = command.ExecuteReader();
                    Tabla.Load(reader);
                    reader.Close();
                    command.Parameters.Clear();
                    conexion.Close();
                }
            }
            return Tabla;
        }

        
        public void AgregarProducto(string codpro, string grupo)
        {
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
            command.CommandText = "sp_QuirofanoAgregarProducto";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@codpro", codpro);
            command.Parameters.AddWithValue("@grupo", grupo);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }

        public DataTable Productos()
        {
            conexion = obj.ConectarBd();
            DataTable Tabla = new DataTable();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command = new SqlCommand ("select QP.CODPRO as CODIGO, P.despro AS DESCRIPCION, QP.QP_GRUPO AS GRUPO, QP.QP_CODIGO from QUIROFANO_PRODUCTOS QP INNER JOIN Sic3000..Producto P on qp.CODPRO = p.codpro", conexion);
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            conexion.Close();
            return Tabla;
        }
        public void EliminarProducto(int codigo)
        {
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
            command.CommandText = "sp_QuirofanoEliminarProducto";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@qp_codigo", codigo);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public void ActualizarProducto(int codigo, string grupo)
        {
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
            command.CommandText = "sp_QuirofanoActualizarProducto";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@qp_codigo", codigo);
            command.Parameters.AddWithValue("@grupo", grupo);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public void AgregarProcedimientos(int orden, string codpro, Int64 cie_codigo, int cantidad)
        {
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
            command.CommandText = "sp_QuirofanoAgregarProcedimiento";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@orden", orden);
            command.Parameters.AddWithValue("@codpro", codpro);
            command.Parameters.AddWithValue("@cie_codigo", cie_codigo);
            command.Parameters.AddWithValue("@cantidad", cantidad);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public DataTable TodosProcedimientos()
        {
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
            command.CommandText = "sp_QuirofanoTodosProcedimientos";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable TodosProductos(Int64 cie_codigo)
        {
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
            command.CommandText = "sp_QuirofanoTodosProductos";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@cie_codigo", cie_codigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public void EiminarProcedimiento(string cie_codigo)
        {
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
            command.CommandText = "sp_QuirofanoProcedimientoEliminar";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@cie_codigo", cie_codigo);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public void ActualizarProcedimiento(string cie_codigo, string codpro, string qpp_orden, int cantidad)
        {
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
            command.CommandText = "sp_QuirofanoEditarProcedimiento";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@cie_codigo", cie_codigo);
            command.Parameters.AddWithValue("@codpro", codpro);
            command.Parameters.AddWithValue("@qpp_orden", qpp_orden);
            command.Parameters.AddWithValue("@qpp_cantidad", cantidad);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public void EliminarProduProce(string cie_codigo, string codpro)
        {
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
            command.CommandText = "sp_QuirofanoEliminarProdu_Proce";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@cie_codigo", cie_codigo);
            command.Parameters.AddWithValue("@codpro", codpro);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public DataTable QuirofanoPacientes()
        {
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
            command.CommandText = "sp_QuirofanoPacientes";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public void PedidoPaciente(Int64 cie, int paciente, int atencion, string fecha)
        {
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
            command.CommandText = "sp_QuirofanoPedidoPacienteAgregar";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@cie_codigo", cie);
            command.Parameters.AddWithValue("@paciente", paciente);
            command.Parameters.AddWithValue("@atencion", atencion);
            command.Parameters.AddWithValue("@fecha", fecha);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public DataTable SoloProcedimientos()
        {
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
            command.CommandText = "sp_QuirofanoSoloProcedimientos";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public void AgregarProcedimientoPaciente(int orden, string cie_codigo, string codpro, int cantidad, int paciente,
            int atencion, int usada, string usuario, int cerrado)
        {
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
            command.CommandText = "sp_QuirofanoPacienteProcedimiento";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@orden", orden);
            command.Parameters.AddWithValue("@cie_codigo", cie_codigo);
            command.Parameters.AddWithValue("@codpro", codpro);
            command.Parameters.AddWithValue("@cantidad", cantidad);
            command.Parameters.AddWithValue("@paciente", paciente);
            command.Parameters.AddWithValue("@atencion", atencion);
            command.Parameters.AddWithValue("@usada", usada);
            command.Parameters.AddWithValue("@usuario", usuario);
            command.Parameters.AddWithValue("@cerrado", cerrado);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public void ModificarProcedimientoPaciente(int orden, string cie_codigo, string codpro,
            int paciente, int atencion, int usado, string usuario)
        {
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
            command.CommandText = "sp_QuirofanoCambioProcedimientoPaciente";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@orden", orden);
            command.Parameters.AddWithValue("@cie_codigo", cie_codigo);
            command.Parameters.AddWithValue("@codpro", codpro);
            command.Parameters.AddWithValue("@paciente", paciente);
            command.Parameters.AddWithValue("@atencion", atencion);
            command.Parameters.AddWithValue("@cantidadusada", usado);
            command.Parameters.AddWithValue("@usuario", usuario);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public DataTable PacienteProcedimiento(Int64 cie_codigo, int atencion)
        {
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
            command.CommandText = "sp_QuirofanoMostrarProcedimientoPaciente";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@cie_codigo", cie_codigo);
            command.Parameters.AddWithValue("@atencion", atencion);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            conexion.Close();
            return Tabla;
        }
        public string Cantidad(int atencion, int codigo)
        {
            string valor = null;
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
            command.CommandText = "sp_QuirofanoNumero";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@atencion", atencion);
            command.Parameters.AddWithValue("@codigo", codigo);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                valor = reader["Valor"].ToString();
            }
            reader.Close();
            command.Parameters.Clear();
            conexion.Close();
            return valor;
        }
        public DataTable FiltrarPorProcedimiento(string cie_codigo)
        {
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
            command.CommandText = "sp_QuirofanoPorProcedimiento";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@cie_codigo", cie_codigo);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            conexion.Close();
            return Tabla;
        }
        public string ProductoRepetido(string codpro, Int64 cie_codigo)
        {
            string producto = null;
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
            command.CommandText = "sp_QuirofanoProductoRepetido";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@codpro", codpro);
            command.Parameters.AddWithValue("@cie_codigo", cie_codigo);
            command.CommandTimeout = 180;
            SqlDataReader reader =  command.ExecuteReader();
            while (reader.Read())
            {
                producto = reader["CODPRO"].ToString();
            }
            command.Parameters.Clear();
            conexion.Close();
            return producto;
        }
        public string SoloProductoRepetido(string codpro)
        {
            string producto = null;
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
            command.CommandText = "sp_QuirofanoNoRepetirProductos";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@codpro", codpro);
            SqlParameter v = new SqlParameter("@codigoproducto", 0); v.Direction = ParameterDirection.Output;
            command.Parameters.Add(v);
            command.ExecuteNonQuery();
            producto = command.Parameters["@codigoproducto"].Value.ToString();
            command.Parameters.Clear();
            conexion.Close();
            return producto;
        }
        public void ProductoBodega(string codpro, double existe, double bodega)
        {
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
            command.CommandText = "sp_QuirofanoBodega";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@codpro", codpro);
            command.Parameters.AddWithValue("@existe", existe);
            command.Parameters.AddWithValue("@bodega", bodega);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public DataTable ProcedimientosPaciente(int ate_codigo, int pac_codigo)
        {
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
            command.CommandText = "sp_QuirofanoProcedimientosPaciente";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@pac_codigo", pac_codigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            conexion.Close();
            return Tabla;
        }
        public string Proces(int ate_codigo, int pac_codigo, string cie_codigo) //funcion que ayuda a ver si existe procedimiento ya agregado al paciente
        {
            string valor = null;
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
            command.CommandText = "sp_QuirofanoExisteProcedimiento";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@pac_codigo", pac_codigo);
            command.Parameters.AddWithValue("@cie_codigo", cie_codigo);
            SqlParameter v = new SqlParameter("@valor", 0); v.Direction = ParameterDirection.Output;
            command.Parameters.Add(v);
            command.ExecuteNonQuery();
            valor = command.Parameters["@valor"].Value.ToString();
            command.Parameters.Clear();
            conexion.Close();
            return valor;
        }
        public void PedidoAdicional(int atencion, int paciente, string cie_codigo, int cant_adicional, string codpro)
        {
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
            command.CommandText = "sp_QuirofanoPedidoAdicional";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@atencion", atencion);
            command.Parameters.AddWithValue("@paciente", paciente);
            command.Parameters.AddWithValue("@cie_codigo", cie_codigo);
            command.Parameters.AddWithValue("@cant_adicional", cant_adicional);
            command.Parameters.AddWithValue("@codpro", codpro);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }

        public DataTable HabitacionQuirofano()
        {
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
            command.CommandText = "sp_QuirofanoHabitacion";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable MostrarMedicos()
        {
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
            command.CommandText = "sp_QuirofanoMedicos";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable MostrarAnestesiologo()
        {
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
            command.CommandText = "sp_QuirofanoAnestesiologo";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable MostrarUsuario()
        {
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
            command.CommandText = "sp_QuirofanoUsuarios";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable MostrarAnestesia()
        {
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
            command.CommandText = "sp_QuirofanoAnestesia";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public void AgregarRegistro(int hab_quirofano, int cirujano, int ayudante, int ayudantia, int tipo_anestesia,
            int recuperacion, int anestesiologo, string horainicio, string horafin, string duracion, int circulante, 
            int instrumentista, int patologo, string tipo_atencion, int ate_codigo, int pac_codigo, Int64 cie_codigo)
        {
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
            command.CommandText = "sp_QuirofanoRegistroAgregar";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@hab_quirofano", hab_quirofano);
            command.Parameters.AddWithValue("@cirujano", cirujano);
            command.Parameters.AddWithValue("@ayudante", ayudante);
            command.Parameters.AddWithValue("@ayudantia", ayudantia);
            command.Parameters.AddWithValue("@tipo_anestesia", tipo_anestesia);
            command.Parameters.AddWithValue("@recuperacion", recuperacion);
            command.Parameters.AddWithValue("@anestesiologo", anestesiologo);
            command.Parameters.AddWithValue("@horainicio", horainicio);
            command.Parameters.AddWithValue("@horafin", horafin);
            command.Parameters.AddWithValue("@duracion", duracion);
            command.Parameters.AddWithValue("@circulante", circulante);
            command.Parameters.AddWithValue("@instrumentista", instrumentista);
            command.Parameters.AddWithValue("@patologo", patologo);
            command.Parameters.AddWithValue("@tipo_atencion", tipo_atencion);
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@pac_codigo", pac_codigo);
            command.Parameters.AddWithValue("@cie_codigo", cie_codigo);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public DataTable CargarRegistroPaciente(int ate_codigo, int pac_codigo, Int64 cie_codigo)
        {
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
            command.CommandText = "sp_QuirofanoRegistro";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@pac_codigo", pac_codigo);
            command.Parameters.AddWithValue("@cie_codigo", cie_codigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            conexion.Close();
            return Tabla;
        }
        public void CerrarProcedimiento(int ate_codigo, int pac_codigo, string cie_codigo)
        {
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
            command.CommandText = "sp_QuirofanoCierreProcedimiento";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@pac_codigo", pac_codigo);
            command.Parameters.AddWithValue("@cie_codigo", cie_codigo);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public string ProcedimientoCerrado(int ate_codigo, int pac_codigo, string cie_codigo)
        {
            string estado;
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
            command.CommandText = "sp_QuirofanoProcedimientoCerrado";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@pac_codigo", pac_codigo);
            command.Parameters.AddWithValue("@cie_codigo", cie_codigo);
            SqlParameter v = new SqlParameter("@estado", 0); v.Direction = ParameterDirection.Output;
            command.Parameters.Add(v);
            command.ExecuteNonQuery();
            estado = command.Parameters["@estado"].Value.ToString();
            command.Parameters.Clear();
            conexion.Close();
            return estado;
        }
        public void ActualizarPedidoAdicional(int ate_codigo, string cie_codigo, string codpro, int cantadicional)
        {
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
            command.CommandText = "sp_QuirofanoControlPedidoAdicional";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@cie_codigo", cie_codigo);
            command.Parameters.AddWithValue("codpro", codpro);
            command.Parameters.AddWithValue("@cantadicional", cantadicional);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public void AgregarPedidoPaciente(string ped_fecha, int id_usuario, int ate_codigo, int hab_codigo)
        {
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
            command.CommandText = "sp_QuirofanoAgregarPedido";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ped_fecha", ped_fecha);
            command.Parameters.AddWithValue("@id_usuario", id_usuario);
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@hab_codigo", hab_codigo);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public string RecuperarPedidoNum(int ate_codigo) //funcion que ayuda a ver si existe procedimiento ya agregado al paciente
        {
            string numpedido = null;
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
            command.CommandText = "sp_QuirofanoRecuperarPedido";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            SqlParameter v = new SqlParameter("@numpedido", 0); v.Direction = ParameterDirection.Output;
            command.Parameters.Add(v);
            command.ExecuteNonQuery();
            numpedido = command.Parameters["@numpedido"].Value.ToString();
            command.Parameters.Clear();
            conexion.Close();
            return numpedido;
        }
        public void PedidoDetalle(string codpro, string prodesc, int cantidad, double valor, double total, int ped_codigo, double iva)
        {
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
            command.CommandText = "sp_QuirofanoAgregarPedidoProducto";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@codpro", codpro);
            command.Parameters.AddWithValue("@prodesc", prodesc);
            command.Parameters.AddWithValue("@cantidad", cantidad);
            command.Parameters.AddWithValue("@valor", valor);
            command.Parameters.AddWithValue("@total",total);
            command.Parameters.AddWithValue("@iva", iva);
            command.Parameters.AddWithValue("@ped_codigo", ped_codigo);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public DataTable VerTicketPaciente(int ate_codigo)
        {
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
            command.CommandText = "sp_QuirofanoVerTickets";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            conexion.Close();
            return Tabla;
        }
        public DataTable ProductosPaciente(int ate_codigo, int ped_codigo)
        {
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
            command.CommandText = "sp_QuirofanoProductosPedido";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@ped_codigo", ped_codigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            conexion.Close();
            return Tabla;
        }
        public DataTable RecuperarPacientePedidoInfo(int ate_codigo)
        {
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
            command.CommandText = "sp_QuirofanoPacientePedidoInfo";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            conexion.Close();
            return Tabla;
        }
        public DataTable CargarProductosUsados(DateTime desde, DateTime hasta, int usuario)
        {
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
            command.CommandText = "sp_QuirofanoDetalleProducto";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@desde", desde);
            command.Parameters.AddWithValue("@hasta", hasta);
            command.Parameters.AddWithValue("@usuario", usuario);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            conexion.Close();
            return Tabla;
        }

        public DataTable CargarProcedimientosCirugia(string busqueda, bool codigo, bool descripcion)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
            SqlDataReader reader;

            connection = obj.ConectarBd();
            connection.Open();
            if (busqueda == "")
            {
                command = new SqlCommand("select PCI_CODIGO AS CODIGO, PCI_DESCRIPCION AS DESCRIPCION from PROCEDIMIENTOS_CIRUGIA ORDER BY 2", connection);
                command.CommandType = CommandType.Text;
                command.CommandTimeout = 180;
                reader = command.ExecuteReader();
                Tabla.Load(reader);
                reader.Close();
                connection.Close();
            }
            else
            {
                if (descripcion)
                {
                    command = new SqlCommand("select PCI_CODIGO AS CODIGO, PCI_DESCRIPCION AS DESCRIPCION from PROCEDIMIENTOS_CIRUGIA  WHERE PCI_DESCRIPCION LIKE '%' + @filtro + '%' ORDER BY 2", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@filtro", busqueda);
                    command.CommandTimeout = 180;
                    reader = command.ExecuteReader();
                    Tabla.Load(reader);
                    reader.Close();
                    command.Parameters.Clear();
                    connection.Close();
                }
                else if (codigo)
                {
                    command = new SqlCommand("select PCI_CODIGO AS CODIGO, PCI_DESCRIPCION AS DESCRIPCION from PROCEDIMIENTOS_CIRUGIA  WHERE PCI_CODIGO = @filtro ORDER BY 2", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@filtro", busqueda);
                    command.CommandTimeout = 180;
                    reader = command.ExecuteReader();
                    Tabla.Load(reader);
                    reader.Close();
                    command.Parameters.Clear();
                    connection.Close();
                }
            }

            return Tabla;
        }
        public void ActualizarKardexSic(string numdoc)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_ActualizaKardexSic", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@numdoc", numdoc);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();

        }
        public DataTable AnestesiaSolicitada(int pci_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlDataReader reader;
            DataTable Tabla = new DataTable();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("select * from QUIROFANO_PROCE_PRODU where PCI_CODIGO = @pci_codigo and ATE_CODIGO is null", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@pci_codigo", pci_codigo);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return Tabla;

        }

        public DataTable ListaPedidosQuirofano(int ate_codigo)
        {
            SqlCommand command;
            SqlDataReader reader;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_ListadoPedidosQuirofano", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return Tabla;
        }

        public void QuirofanoActualizarProductos(string codpro, int pci_codigo, int cantidad, Int64 ate_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_QuirofanoActulizarCantidades", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@codpro", codpro);
            command.Parameters.AddWithValue("@pci_codigo", pci_codigo);
            command.Parameters.AddWithValue("@cantidad", cantidad);
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();
        }

        public void QuirofanoEliminaRegistro(string codpro, int pci_codigo, Int64 ate_codigo)//elimina producto del procedimiento
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_QuirofanoEliminaRegistro", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@codpro", codpro);
            command.Parameters.AddWithValue("@pci_codigo", pci_codigo);
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();
        }

        public void DatosReposicion(Int64 ate_codigo, int pci_codigo, int cantidad, DateTime fecha, int ped_codigo, 
            string codpro, int usuario)//guarda el registro para tener datos como fecha en la reposicion
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_DatosReposicion", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@pci_codigo", pci_codigo);
            command.Parameters.AddWithValue("@cantidad", cantidad);
            command.Parameters.AddWithValue("@fechacreacion", fecha);
            command.Parameters.AddWithValue("@ped_codigo", ped_codigo);
            command.Parameters.AddWithValue("@codpro", codpro);
            command.Parameters.AddWithValue("@usuario", usuario);

            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();
        }
        public DataTable RecuperoReposicion(DateTime desde, DateTime hasta, int usuario )//elimina producto del procedimiento
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
            SqlDataReader reader;
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_Reposicion", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@desde", desde);
            command.Parameters.AddWithValue("@hasta", hasta);
            command.Parameters.AddWithValue("@usuario", usuario);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return Tabla;
        }

        public void FechaReposicion(DateTime fecha, int pci_codigo, Int64 ate_codigo, Int64 numdoc)//elimina producto del procedimiento
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_FechaReposicion", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@fechareposicion", fecha);
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@pci_codigo", pci_codigo);
            command.Parameters.AddWithValue("@numdoc", numdoc);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();
        }

        public int NumeroControl()
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlDataReader reader;
            connection = obj.ConectarBd();
            connection.Open();
            int numdoc = 0;
            command = new SqlCommand("select numcon from Sic3000..Numero_Control where codcon = 44 and ocupado = 0", connection);
            command.CommandType = CommandType.Text;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                numdoc = Convert.ToInt32(reader["numcon"].ToString());
            }
            reader.Close();
            connection.Close();
            return numdoc;
        }


        public void EliminarRegistro(Int64 ate_codigo, int pci_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("DELETE FROM QUIROFANO_PROCE_PRODU WHERE ATE_CODIGO = @ate_codigo AND PCI_CODIGO = @pci_codigo", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@pci_codigo", pci_codigo);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();
        }

        public void NumeroOcupado()
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("Update numero_control set ocupado=1 where codcon =44", connection);
            command.CommandType = CommandType.Text;
            command.ExecuteNonQuery();
            connection.Close();

        }

        public void NumeroLiberar()
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("sp_QuirofanoLiberaControl", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.ExecuteNonQuery();
            connection.Close();

        }
        public void CreaPedidoReposicion(Int64 numdoc, DateTime fecha, string hora, double origen, Double destino,
            string observacion, char estado, double usuario)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("sp_ReposicionSic3000", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@numdoc", numdoc);
            command.Parameters.AddWithValue("@fecha", fecha);
            command.Parameters.AddWithValue("@hora", hora);
            command.Parameters.AddWithValue("@bodegaOrigen", origen);
            command.Parameters.AddWithValue("@bodegaDestino", destino);
            command.Parameters.AddWithValue("@observacion", observacion);
            command.Parameters.AddWithValue("@estado", estado);
            command.Parameters.AddWithValue("@usuario", usuario);

            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();

        }

        public void DetalleReposicion(string codpro, string despro, double cant, int linea, Int64 numdoc)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("sp_DetalleReposicion", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@codpro", codpro);
            command.Parameters.AddWithValue("@despro", despro);
            command.Parameters.AddWithValue("@cant", cant);
            command.Parameters.AddWithValue("@linea", linea);
            command.Parameters.AddWithValue("@numdoc", numdoc);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();
        }

        public DataTable DetalleExportar(DateTime desde, DateTime hasta)//elimina producto del procedimiento
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
            SqlDataReader reader;
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_QuirofanoDetalleProductoExportar", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@desde", desde);
            command.Parameters.AddWithValue("@hasta", hasta);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return Tabla;
        }

        public DataTable UsuariosReposicion()
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlDataReader reader;
            connection = obj.ConectarBd();
            connection.Open();
            DataTable Tabla = new DataTable();
            command = new SqlCommand("SELECT U.ID_USUARIO AS CODIGO, U.APELLIDOS + ' ' + U.NOMBRES AS USUARIO FROM USUARIOS U INNER JOIN REPOSICION_QUIROFANO RQ ON U.ID_USUARIO = RQ.ID_USUARIO WHERE RQ_FECHAREPOSICION IS NULL GROUP BY U.ID_USUARIO, U.APELLIDOS + ' ' + U.NOMBRES", connection);
            command.CommandType = CommandType.Text;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            connection.Close();
            return Tabla;
        }
        public bool NombreProcedimiento(string procedimiento)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
            connection = obj.ConectarBd();
            connection.Open();
            bool valido = false;

            command = new SqlCommand("sp_QuirofanoNombreProcedimiento", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@pci_descripcion", procedimiento);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (reader["ESTADO"].ToString() == "1")
                    valido = true;
                else
                    valido = false;
            }
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return valido;

        }
        public DataTable ProductosSic()
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("sp_QuirofanoProductoSic", connection);
            command.CommandType = CommandType.StoredProcedure;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            connection.Close();
            return Tabla;
        }

        public DataTable ProcedimientosVarios(Int64 ate_codigo, int pac_codigo, string procedimiento)//VERIFICO CUANTOS PROCEDIMIENTOS TIENE
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("sp_QuirofanoExisteVariosProce", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@pac_codigo", pac_codigo);
            command.Parameters.AddWithValue("@procedimiento", procedimiento);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            connection.Close();
            return Tabla;
        }

        public DataTable Creado(string procedimiento)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("sp_QuiroCrearVariosProcedimientos", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@procedimiento", procedimiento);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            connection.Close();
            return Tabla;
        }

        public int UltimoOrden(int pci_codigo, Int64 ate_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlDataReader reader;
            int ultimoOrden = 0;
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("select top 1 QPP_ORDEN from QUIROFANO_PROCE_PRODU where ATE_CODIGO = @ate_codigo and PCI_CODIGO = @pci_codigo order by QPP_ORDEN desc", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@pci_codigo", pci_codigo);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                ultimoOrden = Convert.ToInt32(reader["QPP_ORDEN"].ToString());
            }
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return ultimoOrden;
        }
    }
}
