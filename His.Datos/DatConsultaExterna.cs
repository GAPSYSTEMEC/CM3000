using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Core.Datos;
using His.Entidades;
using His.Entidades.Clases;

namespace His.Datos
{
    public class DatConsultaExterna
    {
        public DataTable RecuperaPaciente(Int64 Ate_Codigo)
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

            Sqlcmd = new SqlCommand("sp_RecuperaPacienteConsultaExterna", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Ate_Codigo", SqlDbType.Int);
            Sqlcmd.Parameters["@Ate_Codigo"].Value = (Ate_Codigo);

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
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


        public DataTable PacienteExiste(Int64 Ate_Codigo)
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

            Sqlcmd = new SqlCommand("select Historia as Hc, fecha as Fecha, Apellido + ' ' + Nombre as Paciente, Edad, Sexo, ID_FORM002 from Form002MSP where AteCodigo = " + Ate_Codigo, Sqlcon);

            Sqlcmd.CommandType = CommandType.Text;
            SqlDataReader reader = Sqlcmd.ExecuteReader();
            Sqlcmd.CommandTimeout = 180;
            Dts.Load(reader);
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

        public DataTable PacientesConsultaExterna(Int64 Ate_Codigo)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
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

            Sqlcmd = new SqlCommand("select Historia as Hc, Apellido + ' ' + Nombre as Paciente, Edad, Sexo from Form002MSP where AteCodigo = "+ Ate_Codigo + " group by Historia, Apellido + ' ' + Nombre, Edad, Sexo", Sqlcon);

            Sqlcmd.CommandType = CommandType.Text;
            SqlDataReader reader = Sqlcmd.ExecuteReader();
            Sqlcmd.CommandTimeout = 180;
            Dts.Load(reader);
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
        public DataTable PacienteConsultaExterna(int id)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
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

            Sqlcmd = new SqlCommand("select * from Form002MSP where ID_FORM002 = " + id, Sqlcon);

            Sqlcmd.CommandType = CommandType.Text;
            SqlDataReader reader = Sqlcmd.ExecuteReader();
            Sqlcmd.CommandTimeout = 180;
            Dts.Load(reader);
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

        public Int64 RecuperarId()
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            BaseContextoDatos obj = new BaseContextoDatos();
            Int64 id_form002 = 0;
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Sqlcmd = new SqlCommand("select max(ID_FORM002) as codigo from Form002MSP", Sqlcon);

            Sqlcmd.CommandType = CommandType.Text;
            Sqlcmd.CommandTimeout = 180;
            SqlDataReader reader = Sqlcmd.ExecuteReader();
            while (reader.Read())
            {
                id_form002 = Convert.ToInt64(reader["codigo"].ToString());
            }
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return id_form002;
        }
        public void GuardarPrescripciones(Int64 id_form002, int id_usuario, string usuario, string indicacion, string farmaco, DateTime fecha_admin, bool administrado)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
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

            Sqlcmd = new SqlCommand("INSERT INTO PRESCRIPCIONES_CONSULTA_EXTERNA VALUES(@id_usuario, @usuario, @indicacion, @farmacos, @fecha_admin, @administrado, @id_form002)", Sqlcon);

            Sqlcmd.CommandType = CommandType.Text;

            Sqlcmd.Parameters.AddWithValue("@id_usuario", id_usuario);
            Sqlcmd.Parameters.AddWithValue("@usuario", usuario);
            Sqlcmd.Parameters.AddWithValue("@indicacion", indicacion);
            Sqlcmd.Parameters.AddWithValue("@farmacos", farmaco);
            Sqlcmd.Parameters.AddWithValue("@fecha_admin", fecha_admin);
            Sqlcmd.Parameters.AddWithValue("@administrado", administrado);
            Sqlcmd.Parameters.AddWithValue("@id_form002", id_form002);
            Sqlcmd.CommandTimeout = 180;
            Sqlcmd.ExecuteNonQuery();
            Sqlcmd.Parameters.Clear();
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public DataTable RecuperaEspecialidades()
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

            Sqlcmd = new SqlCommand("sp_EspecialidadesMedicas", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
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

        public DataTable RecuperaConsultorios()
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

            Sqlcmd = new SqlCommand("sp_RecuperaConsultorios", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
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

        public DataTable RecuperaHorario(string Tiempo, DateTime fechaCita, string consultorio)
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

            Sqlcmd = new SqlCommand("sp_RecuperaHorario", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Tiempo", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Tiempo"].Value = (Tiempo);

            Sqlcmd.Parameters.Add("@fechaCita", SqlDbType.Date);
            Sqlcmd.Parameters["@fechaCita"].Value = (fechaCita);

            Sqlcmd.Parameters.Add("@consultorio", SqlDbType.VarChar);
            Sqlcmd.Parameters["@consultorio"].Value = (consultorio);

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
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

        public DataTable BuscaPaciente(string cedula)
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

            Sqlcmd = new SqlCommand("sp_BuscaPacienteConsultaExterna", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@cedula", SqlDbType.VarChar);
            Sqlcmd.Parameters["@cedula"].Value = (cedula);

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
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

        public bool GuardaAgendamientoPaciente(string txtIdentificacion, string txtNombres, string txtApellidos, string txtEmail, string txtTelefono, string txtCelular, string txtDireccion, DateTime dtpFechaCita, string cmbEspecialidades, string lblMedico, string lblMailMed, string cmbConsultorios, string cmbHora, string txtMotivo, string txtNotas)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataSet Dts;
            SqlTransaction transaction;
            BaseContextoDatos obj = new BaseContextoDatos();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch
            {
                return false;
            }

            transaction = Sqlcon.BeginTransaction();

            try
            {

                Sqlcmd = new SqlCommand("sp_GrabaPacienteConsultaExterna", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;
                Sqlcmd.Transaction = transaction;

                Sqlcmd.Parameters.Add("@txtIdentificacion", SqlDbType.VarChar);
                Sqlcmd.Parameters["@txtIdentificacion"].Value = (txtIdentificacion);

                Sqlcmd.Parameters.Add("@txtNombres", SqlDbType.VarChar);
                Sqlcmd.Parameters["@txtNombres"].Value = (txtNombres);

                Sqlcmd.Parameters.Add("@txtApellidos", SqlDbType.VarChar);
                Sqlcmd.Parameters["@txtApellidos"].Value = (txtApellidos);

                Sqlcmd.Parameters.Add("@txtEmail", SqlDbType.VarChar);
                Sqlcmd.Parameters["@txtEmail"].Value = (txtEmail);

                Sqlcmd.Parameters.Add("@txtTelefono", SqlDbType.VarChar);
                Sqlcmd.Parameters["@txtTelefono"].Value = (txtTelefono);

                Sqlcmd.Parameters.Add("@txtCelular", SqlDbType.VarChar);
                Sqlcmd.Parameters["@txtCelular"].Value = (txtCelular);

                Sqlcmd.Parameters.Add("@txtDireccion", SqlDbType.VarChar);
                Sqlcmd.Parameters["@txtDireccion"].Value = (txtDireccion);

                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180;
                Sqldap.SelectCommand = Sqlcmd;
                Dts = new DataSet();
                Sqldap.Fill(Dts);

                //GUARDA AGENDAMIENTO SIEMPRE QUE SE GUARDE DATOS DEL PACIENTE

                Sqlcmd = new SqlCommand("sp_GrabaAgendaConsultaExterna", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;
                Sqlcmd.Transaction = transaction;

                Sqlcmd.Parameters.Add("@dtpFechaCita", SqlDbType.Date);
                Sqlcmd.Parameters["@dtpFechaCita"].Value = (dtpFechaCita);

                Sqlcmd.Parameters.Add("@cmbEspecialidades", SqlDbType.VarChar);
                Sqlcmd.Parameters["@cmbEspecialidades"].Value = (cmbEspecialidades);

                Sqlcmd.Parameters.Add("@lblMedico", SqlDbType.VarChar);
                Sqlcmd.Parameters["@lblMedico"].Value = (lblMedico);

                Sqlcmd.Parameters.Add("@lblMailMed", SqlDbType.VarChar);
                Sqlcmd.Parameters["@lblMailMed"].Value = (lblMailMed);

                Sqlcmd.Parameters.Add("@cmbConsultorios", SqlDbType.VarChar);
                Sqlcmd.Parameters["@cmbConsultorios"].Value = (cmbConsultorios);

                Sqlcmd.Parameters.Add("@cmbHora", SqlDbType.VarChar);
                Sqlcmd.Parameters["@cmbHora"].Value = (cmbHora);

                Sqlcmd.Parameters.Add("@txtMotivo", SqlDbType.VarChar);
                Sqlcmd.Parameters["@txtMotivo"].Value = (txtMotivo);

                Sqlcmd.Parameters.Add("@txtNotas", SqlDbType.VarChar);
                Sqlcmd.Parameters["@txtNotas"].Value = (txtNotas);

                Sqlcmd.Parameters.Add("@txtIdentificacion", SqlDbType.VarChar);
                Sqlcmd.Parameters["@txtIdentificacion"].Value = (txtIdentificacion);

                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180;
                Sqldap.SelectCommand = Sqlcmd;
                Dts = new DataSet();
                Sqldap.Fill(Dts);
                transaction.Commit();
                try
                {
                    Sqlcon.Close();
                }
                catch 
                {
                    return false;
                }
                
                return true;
            }
            catch(Exception ex)
            {
                transaction.Rollback();
                return false; // los datos no se almacenaron
            }
        }

        public DataTable RecuperaNumAgenda()
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

            Sqlcmd = new SqlCommand("sp_RecuperaNumAgenda", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
                       
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
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

        public bool GuardaTriajeSignosVitales(string lblHistoria, Int64 lblAtencion, int nourgente, int urgente, int critico, int muerto, int alcohol, int drogas, int otros, string txtOtrasActual, string txtObserEnfer, decimal txt_PresionA1, decimal txt_PresionA2, decimal txt_FCardiaca, decimal txt_FResp, decimal txt_TBucal, decimal txt_TAxilar, decimal txt_SaturaO, decimal txt_PesoKG, decimal txt_Talla, decimal txtIMCorporal, decimal txt_PerimetroC, decimal txt_Glicemia, decimal txt_TotalG, int cmb_Motora, int cmb_Verbal, int cmb_Ocular, int txt_DiamPDV, string cmb_ReacPDValor, int txt_DiamPIV, string cmb_ReacPIValor, int txt_Gesta, int txt_Partos, int txt_Abortos, int txt_Cesareas, DateTime dtp_ultimaMenst1, decimal txt_SemanaG, int movFetal, int txt_FrecCF, int memRotas, string txt_Tiempo, int txt_AltU, int txt_Presentacion, int txt_Dilatacion, int txt_Borramiento, string txt_Plano, int pelvis, int sangrado, string txt_Contracciones, int urgente2)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataSet Dts;
            SqlTransaction transaction;
            BaseContextoDatos obj = new BaseContextoDatos();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch
            {
                return false;
            }

            transaction = Sqlcon.BeginTransaction();

            try
            {

                Sqlcmd = new SqlCommand("sp_GrabaTriajeConsultaExterna", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;
                Sqlcmd.Transaction = transaction;

                Sqlcmd.Parameters.Add("@lblHistoria", SqlDbType.VarChar);
                Sqlcmd.Parameters["@lblHistoria"].Value = (lblHistoria);

                Sqlcmd.Parameters.Add("@lblAtencion", SqlDbType.BigInt);
                Sqlcmd.Parameters["@lblAtencion"].Value = (lblAtencion);

                Sqlcmd.Parameters.Add("@nourgente", SqlDbType.Int);
                Sqlcmd.Parameters["@nourgente"].Value = (nourgente);

                Sqlcmd.Parameters.Add("@urgente", SqlDbType.Int);
                Sqlcmd.Parameters["@urgente"].Value = (urgente);

                Sqlcmd.Parameters.Add("@critico", SqlDbType.Int);
                Sqlcmd.Parameters["@critico"].Value = (critico);

                Sqlcmd.Parameters.Add("@muerto", SqlDbType.Int);
                Sqlcmd.Parameters["@muerto"].Value = (muerto);

                Sqlcmd.Parameters.Add("@alcohol", SqlDbType.Int);
                Sqlcmd.Parameters["@alcohol"].Value = (alcohol);

                Sqlcmd.Parameters.Add("@drogas", SqlDbType.Int);
                Sqlcmd.Parameters["@drogas"].Value = (drogas);

                Sqlcmd.Parameters.Add("@otros", SqlDbType.Int);
                Sqlcmd.Parameters["@otros"].Value = (otros);

                Sqlcmd.Parameters.Add("@txtOtrasActual", SqlDbType.VarChar);
                Sqlcmd.Parameters["@txtOtrasActual"].Value = (txtOtrasActual);

                Sqlcmd.Parameters.Add("@txtObserEnfer", SqlDbType.VarChar);
                Sqlcmd.Parameters["@txtObserEnfer"].Value = (txtObserEnfer);

                Sqlcmd.Parameters.Add("@urgente2", SqlDbType.Int);
                Sqlcmd.Parameters["@urgente2"].Value = (urgente2);

                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180;
                Sqldap.SelectCommand = Sqlcmd;
                Dts = new DataSet();
                Sqldap.Fill(Dts);

                //GUARDA SIGNOS VITALES SIEMPRE QUE SE GUARDE TRIAJE DEL PACIENTE

                Sqlcmd = new SqlCommand("sp_GrabaSignosVitalesConsultaExterna", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;
                Sqlcmd.Transaction = transaction;

                Sqlcmd.Parameters.Add("@lblHistoria", SqlDbType.VarChar);
                Sqlcmd.Parameters["@lblHistoria"].Value = (lblHistoria);

                Sqlcmd.Parameters.Add("@lblAtencion", SqlDbType.BigInt);
                Sqlcmd.Parameters["@lblAtencion"].Value = (lblAtencion);

                Sqlcmd.Parameters.Add("@txt_PresionA1", SqlDbType.Decimal);
                Sqlcmd.Parameters["@txt_PresionA1"].Value = (txt_PresionA1);

                Sqlcmd.Parameters.Add("@txt_PresionA2", SqlDbType.Decimal);
                Sqlcmd.Parameters["@txt_PresionA2"].Value = (txt_PresionA2);

                Sqlcmd.Parameters.Add("@txt_FCardiaca", SqlDbType.Decimal);
                Sqlcmd.Parameters["@txt_FCardiaca"].Value = (txt_FCardiaca);

                Sqlcmd.Parameters.Add("@txt_FResp", SqlDbType.Decimal);
                Sqlcmd.Parameters["@txt_FResp"].Value = (txt_FResp);

                Sqlcmd.Parameters.Add("@txt_TBucal", SqlDbType.Decimal);
                Sqlcmd.Parameters["@txt_TBucal"].Value = (txt_TBucal);

                Sqlcmd.Parameters.Add("@txt_TAxilar", SqlDbType.Decimal);
                Sqlcmd.Parameters["@txt_TAxilar"].Value = (txt_TAxilar);

                Sqlcmd.Parameters.Add("@txt_SaturaO", SqlDbType.Decimal);
                Sqlcmd.Parameters["@txt_SaturaO"].Value = (txt_SaturaO);

                Sqlcmd.Parameters.Add("@txt_PesoKG", SqlDbType.Decimal);
                Sqlcmd.Parameters["@txt_PesoKG"].Value = (txt_PesoKG);

                Sqlcmd.Parameters.Add("@txt_Talla", SqlDbType.Decimal);
                Sqlcmd.Parameters["@txt_Talla"].Value = (txt_Talla);

                Sqlcmd.Parameters.Add("@txtIMCorporal", SqlDbType.Decimal);
                Sqlcmd.Parameters["@txtIMCorporal"].Value = (txtIMCorporal);

                Sqlcmd.Parameters.Add("@txt_PerimetroC", SqlDbType.Decimal);
                Sqlcmd.Parameters["@txt_PerimetroC"].Value = (txt_PerimetroC);

                Sqlcmd.Parameters.Add("@txt_Glicemia", SqlDbType.Decimal);
                Sqlcmd.Parameters["@txt_Glicemia"].Value = (txt_Glicemia);

                Sqlcmd.Parameters.Add("@txt_TotalG", SqlDbType.Decimal);
                Sqlcmd.Parameters["@txt_TotalG"].Value = (txt_TotalG);

                Sqlcmd.Parameters.Add("@cmb_Ocular", SqlDbType.Int);
                Sqlcmd.Parameters["@cmb_Ocular"].Value = (cmb_Ocular);
                
                Sqlcmd.Parameters.Add("@cmb_Verbal", SqlDbType.Int);
                Sqlcmd.Parameters["@cmb_Verbal"].Value = (cmb_Verbal);

                Sqlcmd.Parameters.Add("@cmb_Motora", SqlDbType.Int);
                Sqlcmd.Parameters["@cmb_Motora"].Value = (cmb_Motora);

                Sqlcmd.Parameters.Add("@txt_DiamPDV", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_DiamPDV"].Value = (txt_DiamPDV);

                Sqlcmd.Parameters.Add("@cmb_ReacPDValor", SqlDbType.VarChar);
                Sqlcmd.Parameters["@cmb_ReacPDValor"].Value = (cmb_ReacPDValor);

                Sqlcmd.Parameters.Add("@txt_DiamPIV", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_DiamPIV"].Value = (txt_DiamPIV);

                Sqlcmd.Parameters.Add("@cmb_ReacPIValor", SqlDbType.VarChar);
                Sqlcmd.Parameters["@cmb_ReacPIValor"].Value = (cmb_ReacPIValor);
                
                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180;
                Sqldap.SelectCommand = Sqlcmd;
                Dts = new DataSet();
                Sqldap.Fill(Dts);

                //GUARDA OBSTETRICA SI SE GUARDA LO ANTERIOR

                Sqlcmd = new SqlCommand("sp_GrabaObstetricaConsultaExterna", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;
                Sqlcmd.Transaction = transaction;

                Sqlcmd.Parameters.Add("@lblHistoria", SqlDbType.VarChar);
                Sqlcmd.Parameters["@lblHistoria"].Value = (lblHistoria);

                Sqlcmd.Parameters.Add("@lblAtencion", SqlDbType.BigInt);
                Sqlcmd.Parameters["@lblAtencion"].Value = (lblAtencion);

                Sqlcmd.Parameters.Add("@txt_Gesta", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_Gesta"].Value = (txt_Gesta);

                Sqlcmd.Parameters.Add("@txt_Partos", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_Partos"].Value = (txt_Partos);

                Sqlcmd.Parameters.Add("@txt_Abortos", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_Abortos"].Value = (txt_Abortos);

                Sqlcmd.Parameters.Add("@txt_Cesareas", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_Cesareas"].Value = (txt_Cesareas);

                Sqlcmd.Parameters.Add("@dtp_ultimaMenst1", SqlDbType.Date);
                Sqlcmd.Parameters["@dtp_ultimaMenst1"].Value = (dtp_ultimaMenst1);

                Sqlcmd.Parameters.Add("@txt_SemanaG", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_SemanaG"].Value = (txt_SemanaG);

                Sqlcmd.Parameters.Add("@movFetal", SqlDbType.Int);
                Sqlcmd.Parameters["@movFetal"].Value = (movFetal);

                Sqlcmd.Parameters.Add("@txt_FrecCF", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_FrecCF"].Value = (txt_FrecCF);

                Sqlcmd.Parameters.Add("@memRotas", SqlDbType.Int);
                Sqlcmd.Parameters["@memRotas"].Value = (memRotas);

                Sqlcmd.Parameters.Add("@txt_Tiempo", SqlDbType.VarChar);
                Sqlcmd.Parameters["@txt_Tiempo"].Value = (txt_Tiempo);

                Sqlcmd.Parameters.Add("@txt_AltU", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_AltU"].Value = (txt_AltU);

                Sqlcmd.Parameters.Add("@txt_Presentacion", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_Presentacion"].Value = (txt_Presentacion);

                Sqlcmd.Parameters.Add("@txt_Dilatacion", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_Dilatacion"].Value = (txt_Dilatacion);

                Sqlcmd.Parameters.Add("@txt_Borramiento", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_Borramiento"].Value = (txt_Borramiento);

                Sqlcmd.Parameters.Add("@txt_Plano", SqlDbType.VarChar);
                Sqlcmd.Parameters["@txt_Plano"].Value = (txt_Plano);

                Sqlcmd.Parameters.Add("@pelvis", SqlDbType.Int);
                Sqlcmd.Parameters["@pelvis"].Value = (pelvis);

                Sqlcmd.Parameters.Add("@sangrado", SqlDbType.Int);
                Sqlcmd.Parameters["@sangrado"].Value = (sangrado);

                Sqlcmd.Parameters.Add("@txt_Contracciones", SqlDbType.VarChar);
                Sqlcmd.Parameters["@txt_Contracciones"].Value = (txt_Contracciones);

                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180;
                Sqldap.SelectCommand = Sqlcmd;
                Dts = new DataSet();
                Sqldap.Fill(Dts);


                transaction.Commit();
                try
                {
                    Sqlcon.Close();
                }
                catch
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return false; // los datos no se almacenaron
            }
        }

        public bool EditarTriajeSignosVitales(string lblHistoria, Int64 lblAtencion, int nourgente, int urgente, int critico, int muerto, int alcohol, int drogas, int otros, string txtOtrasActual, string txtObserEnfer, decimal txt_PresionA1, decimal txt_PresionA2, decimal txt_FCardiaca, decimal txt_FResp, decimal txt_TBucal, decimal txt_TAxilar, decimal txt_SaturaO, decimal txt_PesoKG, decimal txt_Talla, decimal txtIMCorporal, decimal txt_PerimetroC, decimal txt_Glicemia, decimal txt_TotalG, int cmb_Motora, int cmb_Verbal, int cmb_Ocular, int txt_DiamPDV, string cmb_ReacPDValor, int txt_DiamPIV, string cmb_ReacPIValor, int txt_Gesta, int txt_Partos, int txt_Abortos, int txt_Cesareas, DateTime dtp_ultimaMenst1, decimal txt_SemanaG, int movFetal, int txt_FrecCF, int memRotas, string txt_Tiempo, int txt_AltU, int txt_Presentacion, int txt_Dilatacion, int txt_Borramiento, string txt_Plano, int pelvis, int sangrado, string txt_Contracciones, int urgente2)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataSet Dts;
            SqlTransaction transaction;
            BaseContextoDatos obj = new BaseContextoDatos();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch
            {
                return false;
            }

            transaction = Sqlcon.BeginTransaction();

            try
            {

                Sqlcmd = new SqlCommand("sp_EditaTriajeConsultaExterna", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;
                Sqlcmd.Transaction = transaction;

                Sqlcmd.Parameters.Add("@lblHistoria", SqlDbType.VarChar);
                Sqlcmd.Parameters["@lblHistoria"].Value = (lblHistoria);

                Sqlcmd.Parameters.Add("@lblAtencion", SqlDbType.BigInt);
                Sqlcmd.Parameters["@lblAtencion"].Value = (lblAtencion);

                Sqlcmd.Parameters.Add("@nourgente", SqlDbType.Int);
                Sqlcmd.Parameters["@nourgente"].Value = (nourgente);

                Sqlcmd.Parameters.Add("@urgente", SqlDbType.Int);
                Sqlcmd.Parameters["@urgente"].Value = (urgente);

                Sqlcmd.Parameters.Add("@critico", SqlDbType.Int);
                Sqlcmd.Parameters["@critico"].Value = (critico);

                Sqlcmd.Parameters.Add("@muerto", SqlDbType.Int);
                Sqlcmd.Parameters["@muerto"].Value = (muerto);

                Sqlcmd.Parameters.Add("@alcohol", SqlDbType.Int);
                Sqlcmd.Parameters["@alcohol"].Value = (alcohol);

                Sqlcmd.Parameters.Add("@drogas", SqlDbType.Int);
                Sqlcmd.Parameters["@drogas"].Value = (drogas);

                Sqlcmd.Parameters.Add("@otros", SqlDbType.Int);
                Sqlcmd.Parameters["@otros"].Value = (otros);

                Sqlcmd.Parameters.Add("@txtOtrasActual", SqlDbType.VarChar);
                Sqlcmd.Parameters["@txtOtrasActual"].Value = (txtOtrasActual);

                Sqlcmd.Parameters.Add("@txtObserEnfer", SqlDbType.VarChar);
                Sqlcmd.Parameters["@txtObserEnfer"].Value = (txtObserEnfer);

                Sqlcmd.Parameters.Add("@urgente2", SqlDbType.Int);
                Sqlcmd.Parameters["@urgente2"].Value = (urgente2);

                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180;
                Sqldap.SelectCommand = Sqlcmd;
                Dts = new DataSet();
                Sqldap.Fill(Dts);

                //GUARDA SIGNOS VITALES SIEMPRE QUE SE GUARDE TRIAJE DEL PACIENTE

                Sqlcmd = new SqlCommand("sp_EditaSignosVitalesConsultaExterna", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;
                Sqlcmd.Transaction = transaction;

                Sqlcmd.Parameters.Add("@lblHistoria", SqlDbType.VarChar);
                Sqlcmd.Parameters["@lblHistoria"].Value = (lblHistoria);

                Sqlcmd.Parameters.Add("@lblAtencion", SqlDbType.BigInt);
                Sqlcmd.Parameters["@lblAtencion"].Value = (lblAtencion);

                Sqlcmd.Parameters.Add("@txt_PresionA1", SqlDbType.Decimal);
                Sqlcmd.Parameters["@txt_PresionA1"].Value = (txt_PresionA1);

                Sqlcmd.Parameters.Add("@txt_PresionA2", SqlDbType.Decimal);
                Sqlcmd.Parameters["@txt_PresionA2"].Value = (txt_PresionA2);

                Sqlcmd.Parameters.Add("@txt_FCardiaca", SqlDbType.Decimal);
                Sqlcmd.Parameters["@txt_FCardiaca"].Value = (txt_FCardiaca);

                Sqlcmd.Parameters.Add("@txt_FResp", SqlDbType.Decimal);
                Sqlcmd.Parameters["@txt_FResp"].Value = (txt_FResp);

                Sqlcmd.Parameters.Add("@txt_TBucal", SqlDbType.Decimal);
                Sqlcmd.Parameters["@txt_TBucal"].Value = (txt_TBucal);

                Sqlcmd.Parameters.Add("@txt_TAxilar", SqlDbType.Decimal);
                Sqlcmd.Parameters["@txt_TAxilar"].Value = (txt_TAxilar);

                Sqlcmd.Parameters.Add("@txt_SaturaO", SqlDbType.Decimal);
                Sqlcmd.Parameters["@txt_SaturaO"].Value = (txt_SaturaO);

                Sqlcmd.Parameters.Add("@txt_PesoKG", SqlDbType.Decimal);
                Sqlcmd.Parameters["@txt_PesoKG"].Value = (txt_PesoKG);

                Sqlcmd.Parameters.Add("@txt_Talla", SqlDbType.Decimal);
                Sqlcmd.Parameters["@txt_Talla"].Value = (txt_Talla);

                Sqlcmd.Parameters.Add("@txtIMCorporal", SqlDbType.Decimal);
                Sqlcmd.Parameters["@txtIMCorporal"].Value = (txtIMCorporal);

                Sqlcmd.Parameters.Add("@txt_PerimetroC", SqlDbType.Decimal);
                Sqlcmd.Parameters["@txt_PerimetroC"].Value = (txt_PerimetroC);

                Sqlcmd.Parameters.Add("@txt_Glicemia", SqlDbType.Decimal);
                Sqlcmd.Parameters["@txt_Glicemia"].Value = (txt_Glicemia);

                Sqlcmd.Parameters.Add("@txt_TotalG", SqlDbType.Decimal);
                Sqlcmd.Parameters["@txt_TotalG"].Value = (txt_TotalG);

                Sqlcmd.Parameters.Add("@cmb_Ocular", SqlDbType.Int);
                Sqlcmd.Parameters["@cmb_Ocular"].Value = (cmb_Ocular);

                Sqlcmd.Parameters.Add("@cmb_Verbal", SqlDbType.Int);
                Sqlcmd.Parameters["@cmb_Verbal"].Value = (cmb_Verbal);

                Sqlcmd.Parameters.Add("@cmb_Motora", SqlDbType.Int);
                Sqlcmd.Parameters["@cmb_Motora"].Value = (cmb_Motora);

                Sqlcmd.Parameters.Add("@txt_DiamPDV", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_DiamPDV"].Value = (txt_DiamPDV);

                Sqlcmd.Parameters.Add("@cmb_ReacPDValor", SqlDbType.VarChar);
                Sqlcmd.Parameters["@cmb_ReacPDValor"].Value = (cmb_ReacPDValor);

                Sqlcmd.Parameters.Add("@txt_DiamPIV", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_DiamPIV"].Value = (txt_DiamPIV);

                Sqlcmd.Parameters.Add("@cmb_ReacPIValor", SqlDbType.VarChar);
                Sqlcmd.Parameters["@cmb_ReacPIValor"].Value = (cmb_ReacPIValor);

                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180;
                Sqldap.SelectCommand = Sqlcmd;
                Dts = new DataSet();
                Sqldap.Fill(Dts);

                //GUARDA OBSTETRICA SI SE GUARDA LO ANTERIOR

                Sqlcmd = new SqlCommand("sp_EditarObstetricaConsultaExterna", Sqlcon);
                Sqlcmd.CommandType = CommandType.StoredProcedure;
                Sqlcmd.Transaction = transaction;

                Sqlcmd.Parameters.Add("@lblHistoria", SqlDbType.VarChar);
                Sqlcmd.Parameters["@lblHistoria"].Value = (lblHistoria);

                Sqlcmd.Parameters.Add("@lblAtencion", SqlDbType.BigInt);
                Sqlcmd.Parameters["@lblAtencion"].Value = (lblAtencion);

                Sqlcmd.Parameters.Add("@txt_Gesta", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_Gesta"].Value = (txt_Gesta);

                Sqlcmd.Parameters.Add("@txt_Partos", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_Partos"].Value = (txt_Partos);

                Sqlcmd.Parameters.Add("@txt_Abortos", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_Abortos"].Value = (txt_Abortos);

                Sqlcmd.Parameters.Add("@txt_Cesareas", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_Cesareas"].Value = (txt_Cesareas);

                Sqlcmd.Parameters.Add("@dtp_ultimaMenst1", SqlDbType.Date);
                Sqlcmd.Parameters["@dtp_ultimaMenst1"].Value = (dtp_ultimaMenst1);

                Sqlcmd.Parameters.Add("@txt_SemanaG", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_SemanaG"].Value = (txt_SemanaG);

                Sqlcmd.Parameters.Add("@movFetal", SqlDbType.Int);
                Sqlcmd.Parameters["@movFetal"].Value = (movFetal);

                Sqlcmd.Parameters.Add("@txt_FrecCF", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_FrecCF"].Value = (txt_FrecCF);

                Sqlcmd.Parameters.Add("@memRotas", SqlDbType.Int);
                Sqlcmd.Parameters["@memRotas"].Value = (memRotas);

                Sqlcmd.Parameters.Add("@txt_Tiempo", SqlDbType.VarChar);
                Sqlcmd.Parameters["@txt_Tiempo"].Value = (txt_Tiempo);

                Sqlcmd.Parameters.Add("@txt_AltU", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_AltU"].Value = (txt_AltU);

                Sqlcmd.Parameters.Add("@txt_Presentacion", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_Presentacion"].Value = (txt_Presentacion);

                Sqlcmd.Parameters.Add("@txt_Dilatacion", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_Dilatacion"].Value = (txt_Dilatacion);

                Sqlcmd.Parameters.Add("@txt_Borramiento", SqlDbType.Int);
                Sqlcmd.Parameters["@txt_Borramiento"].Value = (txt_Borramiento);

                Sqlcmd.Parameters.Add("@txt_Plano", SqlDbType.VarChar);
                Sqlcmd.Parameters["@txt_Plano"].Value = (txt_Plano);

                Sqlcmd.Parameters.Add("@pelvis", SqlDbType.Int);
                Sqlcmd.Parameters["@pelvis"].Value = (pelvis);

                Sqlcmd.Parameters.Add("@sangrado", SqlDbType.Int);
                Sqlcmd.Parameters["@sangrado"].Value = (sangrado);

                Sqlcmd.Parameters.Add("@txt_Contracciones", SqlDbType.VarChar);
                Sqlcmd.Parameters["@txt_Contracciones"].Value = (txt_Contracciones);

                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180;
                Sqldap.SelectCommand = Sqlcmd;
                Dts = new DataSet();
                Sqldap.Fill(Dts);


                transaction.Commit();
                try
                {
                    Sqlcon.Close();
                }
                catch
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return false; // los datos no se almacenaron
            }
        }
        public DataTable RecuperaTriaje(Int64 Ate_Codigo)
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

            Sqlcmd = new SqlCommand("sp_RecuperaTriajeConsultaExterna", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Ate_Codigo", SqlDbType.BigInt);
            Sqlcmd.Parameters["@Ate_Codigo"].Value = (Ate_Codigo);

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
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

        public DataTable RecuperaSignos(Int64 Ate_Codigo)
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

            Sqlcmd = new SqlCommand("sp_RecuperaSignosVitalesConsultaExterna", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Ate_Codigo", SqlDbType.BigInt);
            Sqlcmd.Parameters["@Ate_Codigo"].Value = (Ate_Codigo);

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
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

        public DataTable RecuperaObstetrica(Int64 Ate_Codigo)
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

            Sqlcmd = new SqlCommand("sp_RecuperaObstetricaConsultaExterna", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Ate_Codigo", SqlDbType.BigInt);
            Sqlcmd.Parameters["@Ate_Codigo"].Value = (Ate_Codigo);

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
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

        public void GuardaDatos002(DtoForm002 datos)
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

            Sqlcmd = new SqlCommand("sp_GuardaForm002ConsultaExterna", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Nombre", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Nombre"].Value = (datos.nombrePaciente);

            Sqlcmd.Parameters.Add("@Apellido", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Apellido"].Value = (datos.apellidoPaciemte);

            Sqlcmd.Parameters.Add("@Sexo", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Sexo"].Value = (datos.sexoPaciente);

            Sqlcmd.Parameters.Add("@Edad", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Edad"].Value = (datos.edadPaciente);

            Sqlcmd.Parameters.Add("@Historia", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Historia"].Value = (datos.historiaClinica);

            Sqlcmd.Parameters.Add("@AteCodigo", SqlDbType.VarChar);
            Sqlcmd.Parameters["@AteCodigo"].Value = (datos.ateCodigo);

            Sqlcmd.Parameters.Add("@Motivo", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Motivo"].Value = (datos.motivoConsulta);

            Sqlcmd.Parameters.Add("@AntecedentesPersonales", SqlDbType.VarChar);
            Sqlcmd.Parameters["@AntecedentesPersonales"].Value = (datos.antecedentesPersonales);

            Sqlcmd.Parameters.Add("@Cardiopatia", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Cardiopatia"].Value = (datos.cardiopatia);

            Sqlcmd.Parameters.Add("@Diabetes", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Diabetes"].Value = (datos.diabetes);

            Sqlcmd.Parameters.Add("@Vascular", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Vascular"].Value = (datos.vascular);

            Sqlcmd.Parameters.Add("@Hipertencion", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Hipertencion"].Value = (datos.hipertension);

            Sqlcmd.Parameters.Add("@Cancer", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Cancer"].Value = (datos.cancer);

            Sqlcmd.Parameters.Add("@Tuberculosis", SqlDbType.VarChar);
            Sqlcmd.Parameters["@Tuberculosis"].Value = (datos.tuberculosis);

            Sqlcmd.Parameters.Add("@mental", SqlDbType.VarChar);
            Sqlcmd.Parameters["@mental"].Value = (datos.mental);

            Sqlcmd.Parameters.Add("@infecciosa", SqlDbType.VarChar);
            Sqlcmd.Parameters["@infecciosa"].Value = (datos.infeccionsa);

            Sqlcmd.Parameters.Add("@malformacion", SqlDbType.VarChar);
            Sqlcmd.Parameters["@malformacion"].Value = (datos.malFormado);

            Sqlcmd.Parameters.Add("@otro", SqlDbType.VarChar);
            Sqlcmd.Parameters["@otro"].Value = (datos.otro);

            Sqlcmd.Parameters.Add("@antecedentesFamiliares", SqlDbType.VarChar);
            Sqlcmd.Parameters["@antecedentesFamiliares"].Value = (datos.antecedentesFamiliares);

            Sqlcmd.Parameters.Add("@enfermedadActual", SqlDbType.VarChar);
            Sqlcmd.Parameters["@enfermedadActual"].Value = (datos.enfermedadProblemaActual);

            Sqlcmd.Parameters.Add("@sentidos", SqlDbType.VarChar);
            Sqlcmd.Parameters["@sentidos"].Value = (datos.sentidos);

            Sqlcmd.Parameters.Add("@sentidossp", SqlDbType.VarChar);
            Sqlcmd.Parameters["@sentidossp"].Value = (datos.sentidossp);

            Sqlcmd.Parameters.Add("@respiratorio", SqlDbType.VarChar);
            Sqlcmd.Parameters["@respiratorio"].Value = (datos.respiratorio);

            Sqlcmd.Parameters.Add("@respiratoriosp", SqlDbType.VarChar);
            Sqlcmd.Parameters["@respiratoriosp"].Value = (datos.respiratoriosp);

            Sqlcmd.Parameters.Add("@cardioVascular", SqlDbType.VarChar);
            Sqlcmd.Parameters["@cardioVascular"].Value = (datos.cardioVascular);

            Sqlcmd.Parameters.Add("@cardioVascularsp", SqlDbType.VarChar);
            Sqlcmd.Parameters["@cardioVascularsp"].Value = (datos.cardioVascularsp);

            Sqlcmd.Parameters.Add("@digestivo", SqlDbType.VarChar);
            Sqlcmd.Parameters["@digestivo"].Value = (datos.digestivo);

            Sqlcmd.Parameters.Add("@digestivosp", SqlDbType.VarChar);
            Sqlcmd.Parameters["@digestivosp"].Value = (datos.digestivosp);

            Sqlcmd.Parameters.Add("@genital", SqlDbType.VarChar);
            Sqlcmd.Parameters["@genital"].Value = (datos.genital);

            Sqlcmd.Parameters.Add("@genitalsp", SqlDbType.VarChar);
            Sqlcmd.Parameters["@genitalsp"].Value = (datos.genitalsp);

            Sqlcmd.Parameters.Add("@urinario", SqlDbType.VarChar);
            Sqlcmd.Parameters["@urinario"].Value = (datos.urinario);

            Sqlcmd.Parameters.Add("@urinariosp", SqlDbType.VarChar);
            Sqlcmd.Parameters["@urinariosp"].Value = (datos.urinariosp);

            Sqlcmd.Parameters.Add("@esqueletico", SqlDbType.VarChar);
            Sqlcmd.Parameters["@esqueletico"].Value = (datos.esqueletico);

            Sqlcmd.Parameters.Add("@esqueleticosp", SqlDbType.VarChar);
            Sqlcmd.Parameters["@esqueleticosp"].Value = (datos.esqueleticosp);

            Sqlcmd.Parameters.Add("@endocrino", SqlDbType.VarChar);
            Sqlcmd.Parameters["@endocrino"].Value = (datos.endocrino);

            Sqlcmd.Parameters.Add("@endocrinosp", SqlDbType.VarChar);
            Sqlcmd.Parameters["@endocrinosp"].Value = (datos.endocrinosp);

            Sqlcmd.Parameters.Add("@linfatico", SqlDbType.VarChar);
            Sqlcmd.Parameters["@linfatico"].Value = (datos.linfatico);

            Sqlcmd.Parameters.Add("@linfaticosp", SqlDbType.VarChar);
            Sqlcmd.Parameters["@linfaticosp"].Value = (datos.linfaticosp);

            Sqlcmd.Parameters.Add("@nervioso", SqlDbType.VarChar);
            Sqlcmd.Parameters["@nervioso"].Value = (datos.nervioso);

            Sqlcmd.Parameters.Add("@nerviososp", SqlDbType.VarChar);
            Sqlcmd.Parameters["@nerviososp"].Value = (datos.nerviososp);

            Sqlcmd.Parameters.Add("@revisionactual", SqlDbType.VarChar);
            Sqlcmd.Parameters["@revisionactual"].Value = (datos.detalleRevisionOrganos);

            Sqlcmd.Parameters.Add("@fechamedicion", SqlDbType.VarChar);
            Sqlcmd.Parameters["@fechamedicion"].Value = (datos.fechaMedicion);

            Sqlcmd.Parameters.Add("@temperatura", SqlDbType.VarChar);
            Sqlcmd.Parameters["@temperatura"].Value = (datos.temperatura);

            Sqlcmd.Parameters.Add("@presion1", SqlDbType.VarChar);
            Sqlcmd.Parameters["@presion1"].Value = (datos.presionArterial1);

            Sqlcmd.Parameters.Add("@presion2", SqlDbType.VarChar);
            Sqlcmd.Parameters["@presion2"].Value = (datos.presionArterial2);

            Sqlcmd.Parameters.Add("@pulso", SqlDbType.VarChar);
            Sqlcmd.Parameters["@pulso"].Value = (datos.pulso);

            Sqlcmd.Parameters.Add("@frecuenciaRespiratoria", SqlDbType.VarChar);
            Sqlcmd.Parameters["@frecuenciaRespiratoria"].Value = (datos.frecuenciaRespiratoria);

            Sqlcmd.Parameters.Add("@peso", SqlDbType.VarChar);
            Sqlcmd.Parameters["@peso"].Value = (datos.peso);

            Sqlcmd.Parameters.Add("@talla", SqlDbType.VarChar);
            Sqlcmd.Parameters["@talla"].Value = (datos.talla);

            Sqlcmd.Parameters.Add("@cabeza", SqlDbType.VarChar);
            Sqlcmd.Parameters["@cabeza"].Value = (datos.cabeza);

            Sqlcmd.Parameters.Add("@cabezasp", SqlDbType.VarChar);
            Sqlcmd.Parameters["@cabezasp"].Value = (datos.cabezasp);

            Sqlcmd.Parameters.Add("@cuello", SqlDbType.VarChar);
            Sqlcmd.Parameters["@cuello"].Value = (datos.cuello);

            Sqlcmd.Parameters.Add("@cuellosp", SqlDbType.VarChar);
            Sqlcmd.Parameters["@cuellosp"].Value = (datos.cuellosp);

            Sqlcmd.Parameters.Add("@torax", SqlDbType.VarChar);
            Sqlcmd.Parameters["@torax"].Value = (datos.torax);

            Sqlcmd.Parameters.Add("@toraxsp", SqlDbType.VarChar);
            Sqlcmd.Parameters["@toraxsp"].Value = (datos.toraxsp);

            Sqlcmd.Parameters.Add("@abdomen", SqlDbType.VarChar);
            Sqlcmd.Parameters["@abdomen"].Value = (datos.abdomen);

            Sqlcmd.Parameters.Add("@abdomensp", SqlDbType.VarChar);
            Sqlcmd.Parameters["@abdomensp"].Value = (datos.abdomensp);

            Sqlcmd.Parameters.Add("@pelvis", SqlDbType.VarChar);
            Sqlcmd.Parameters["@pelvis"].Value = (datos.pelvis);

            Sqlcmd.Parameters.Add("@pelvissp", SqlDbType.VarChar);
            Sqlcmd.Parameters["@pelvissp"].Value = (datos.pelvissp);

            Sqlcmd.Parameters.Add("@extremidades", SqlDbType.VarChar);
            Sqlcmd.Parameters["@extremidades"].Value = (datos.extremidades);

            Sqlcmd.Parameters.Add("@extremidadessp", SqlDbType.VarChar);
            Sqlcmd.Parameters["@extremidadessp"].Value = (datos.extremidadessp);

            Sqlcmd.Parameters.Add("@examenFisico", SqlDbType.VarChar);
            Sqlcmd.Parameters["@examenFisico"].Value = (datos.examenFisico);

            Sqlcmd.Parameters.Add("@diagnostico1", SqlDbType.VarChar);
            Sqlcmd.Parameters["@diagnostico1"].Value = (datos.diagnosticco1);

            Sqlcmd.Parameters.Add("@diagnostico1cie", SqlDbType.VarChar);
            Sqlcmd.Parameters["@diagnostico1cie"].Value = (datos.diagnosticco1cie);

            Sqlcmd.Parameters.Add("@diagnostico1pre", SqlDbType.VarChar);
            Sqlcmd.Parameters["@diagnostico1pre"].Value = (datos.diagnosticco1prepre);

            Sqlcmd.Parameters.Add("@diagnostico1def", SqlDbType.VarChar);
            Sqlcmd.Parameters["@diagnostico1def"].Value = (datos.diagnosticco1predef);

            Sqlcmd.Parameters.Add("@diagnostico2", SqlDbType.VarChar);
            Sqlcmd.Parameters["@diagnostico2"].Value = (datos.diagnosticco2);

            Sqlcmd.Parameters.Add("@diagnostico2cie", SqlDbType.VarChar);
            Sqlcmd.Parameters["@diagnostico2cie"].Value = (datos.diagnosticco2cie);

            Sqlcmd.Parameters.Add("@diagnostico2pre", SqlDbType.VarChar);
            Sqlcmd.Parameters["@diagnostico2pre"].Value = (datos.diagnosticco2prepre);

            Sqlcmd.Parameters.Add("@diagnostico2def", SqlDbType.VarChar);
            Sqlcmd.Parameters["@diagnostico2def"].Value = (datos.diagnosticco2predef);

            Sqlcmd.Parameters.Add("@diagnostico3", SqlDbType.VarChar);
            Sqlcmd.Parameters["@diagnostico3"].Value = (datos.diagnosticco3);

            Sqlcmd.Parameters.Add("@diagnostico3cie", SqlDbType.VarChar);
            Sqlcmd.Parameters["@diagnostico3cie"].Value = (datos.diagnosticco3cie);

            Sqlcmd.Parameters.Add("@diagnostico3pre", SqlDbType.VarChar);
            Sqlcmd.Parameters["@diagnostico3pre"].Value = (datos.diagnosticco3prepre);

            Sqlcmd.Parameters.Add("@diagnostico3def", SqlDbType.VarChar);
            Sqlcmd.Parameters["@diagnostico3def"].Value = (datos.diagnosticco3predef);

            Sqlcmd.Parameters.Add("@diagnostico4", SqlDbType.VarChar);
            Sqlcmd.Parameters["@diagnostico4"].Value = (datos.diagnosticco4);

            Sqlcmd.Parameters.Add("@diagnostico4cie", SqlDbType.VarChar);
            Sqlcmd.Parameters["@diagnostico4cie"].Value = (datos.diagnosticco4cie);

            Sqlcmd.Parameters.Add("@diagnostico4pre", SqlDbType.VarChar);
            Sqlcmd.Parameters["@diagnostico4pre"].Value = (datos.diagnosticco4prepre);

            Sqlcmd.Parameters.Add("@diagnostico4def", SqlDbType.VarChar);
            Sqlcmd.Parameters["@diagnostico4def"].Value = (datos.diagnosticco4predef);

            Sqlcmd.Parameters.Add("@planesTratamiento", SqlDbType.VarChar);
            Sqlcmd.Parameters["@planesTratamiento"].Value = (datos.planesTratamiento);

            Sqlcmd.Parameters.Add("@evolucion", SqlDbType.VarChar);
            Sqlcmd.Parameters["@evolucion"].Value = (datos.evolucion);

            Sqlcmd.Parameters.Add("@prescripciones", SqlDbType.VarChar);
            Sqlcmd.Parameters["@prescripciones"].Value = (datos.prescripciones);

            Sqlcmd.Parameters.Add("@fecha", SqlDbType.VarChar);
            Sqlcmd.Parameters["@fecha"].Value = (DateTime.Now);

            Sqlcmd.Parameters.Add("@hora", SqlDbType.VarChar);
            Sqlcmd.Parameters["@hora"].Value = ("");

            Sqlcmd.Parameters.Add("@dr", SqlDbType.VarChar);
            Sqlcmd.Parameters["@dr"].Value = (Entidades.Clases.Sesion.nomUsuario);

            Sqlcmd.Parameters.Add("@codigo", SqlDbType.VarChar);
            Sqlcmd.Parameters["@codigo"].Value = ("");
            try
            {
                Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts);
            
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        public DataTable getConsultasExternas(int atecodigo)
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
            Sqlcmd = new SqlCommand("SELECT ID_FORM002 AS ID,  '[MEDICO: ' + dr + '] - [MOTIVO: ' + Motivo + ']' AS DATOS FROM Form002MSP WHERE AteCodigo = " + atecodigo, Sqlcon);
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
        public DataTable VerSintomas()
        {
            SqlCommand command;
            SqlDataReader reader;
            SqlConnection connection;
            DataTable Tabla = new DataTable();

            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();
            command = new SqlCommand("sp_VerSintomas", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            connection.Close();
            return Tabla;

        }

        public void GuardaAtencionSintomas(Int64 ate_codigo, Int64 sc_codigo)
        {
            SqlCommand command;
            SqlConnection connection;

            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_GuardaAtencionSintomas", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@sc_codigo", sc_codigo);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();
        }
    }
}
