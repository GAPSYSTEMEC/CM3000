using His.Entidades;
using His.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace His.ConsultaExterna
{
    public partial class frm_CitaMedica : Form
    {
        public frm_CitaMedica()
        {
            InitializeComponent();
        }
        PACIENTES dtoPaciente = new PACIENTES();
        USUARIOS Usuario = NegUsuarios.RecuperaUsuario(Entidades.Clases.Sesion.codUsuario);
        PACIENTES_DATOS_ADICIONALES dtoAdiciones = new PACIENTES_DATOS_ADICIONALES();
        public bool noExiste = false;
        List<TIPO_REFERIDO> tipoReferido = new List<TIPO_REFERIDO>();
        public void CargarDatos()
        {
            dtoPaciente = NegPacientes.RecuperarPacienteCedula(txtcedula.Text.Trim());
            if (dtoPaciente != null)
            {
                dtoAdiciones = NegPacienteDatosAdicionales.RecuperarDatosAdicionalesPaciente(dtoPaciente.PAC_CODIGO);
                txtapellido1.Text = dtoPaciente.PAC_APELLIDO_PATERNO;
                txtapellido2.Text = dtoPaciente.PAC_APELLIDO_MATERNO;
                txtnombre1.Text = dtoPaciente.PAC_NOMBRE1;
                txtnombre2.Text = dtoPaciente.PAC_NOMBRE2;
                var now = DateTime.Now;
                var birthday = dtoPaciente.PAC_FECHA_NACIMIENTO.Value;
                var yearsOld = now - birthday;

                int years = (int)(yearsOld.TotalDays / 365.25);
                int months = (int)(((yearsOld.TotalDays / 365.25) - years) * 12);

                TimeSpan age = now - birthday;
                DateTime totalTime = new DateTime(age.Ticks);

                lbledad.Text = years + " AÑOS, " + months + " MESES, " + totalTime.Day + " DIAS";
                txtDireccion.Text = dtoAdiciones.DAP_DIRECCION_DOMICILIO;
                txtcedular.Text = dtoAdiciones.DAP_TELEFONO2;
                txtemail.Text = dtoPaciente.PAC_EMAIL;
                dtp_fecnac.Value = (DateTime)dtoPaciente.PAC_FECHA_NACIMIENTO;
                switch (dtoPaciente.PAC_GENERO)
                {
                    case "M":
                        rbn_h.Checked = true;
                        break;
                    case "F":
                        rbn_m.Checked = true;
                        break;
                }
                noExiste = false;
                TablaMotivos.Enabled = true;
            }
            else
            {
                MessageBox.Show("Paciente no registrado", "CM3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                noExiste = true;
                HabilitarCampos(true);
            }
        }
        private void txtcedula_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (txtcedula.Text.Length == 10)
                {
                    if (NegValidaciones.esCedulaValida(txtcedula.Text.Trim()))
                    {
                        CargarDatos();
                        txtapellido1.Focus();
                    }
                }
                else if (txtcedula.Text.Length == 13)
                {
                    if (NegValidaciones.ValidarRuc(txtcedula.Text.Trim()))
                    {
                        CargarDatos();
                        txtapellido1.Focus();
                    }
                }
                else if (txtcedula.Text.Length < 10)
                    Errores.SetError(txtcedula, "Campo Obligatorio");
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarDatos();
        }

        public void CargarSintomas()
        {
            try
            {
                TablaMotivos.Rows.Clear();

                DataTable Sintomas = new DataTable();
                Sintomas = NegConsultaExterna.VerSintomas();

                for (int i = 0; i < Sintomas.Rows.Count; i++)
                {
                    TablaMotivos.Rows.Add(Sintomas.Rows[i][0].ToString(), Sintomas.Rows[i][1].ToString(), false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void frm_CitaMedica_Load(object sender, EventArgs e)
        {
            CargarSintomas();
            HabilitarCampos(false);
        }
        public void HabilitarCampos(bool estado)
        {
            txtnombre1.Enabled = estado;
            txtnombre2.Enabled = estado;
            txtapellido1.Enabled = estado;
            txtapellido2.Enabled = estado;
            txtcedular.Enabled = estado;
            txtemail.Enabled = estado;
            txtDireccion.Enabled = estado;
            dtp_fecnac.Enabled = estado;
            TablaMotivos.Enabled = estado;
        }

        public void LimpiarCampos()
        {
            txtcedula.Text = "";
            txtapellido1.Text = "";
            txtapellido2.Text = "";
            txtcedular.Text = "";
            txtDireccion.Text = "";
            txtemail.Text = "";
            txtnombre1.Text = "";
            txtnombre2.Text = "";
            CargarSintomas();
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            lbledad.Focus();
            int contador = 0;
            foreach (DataGridViewRow item in TablaMotivos.Rows)
            {
                if ((bool)item.Cells["seleccionar"].Value)
                {
                    contador++;
                }
            }
            if(contador > 0)
            {
                saveAtencion();
                saveSintomas();
                //MessageBox.Show("Atencion guardada con exito.", "CM3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnGuardar.Enabled = false;
            }
            else
            {
                Errores.SetError(TablaMotivos, "Debe elegir al menos un sintoma.");
            }
        }
        private void saveAtencion()
        {
            //if (!validar())
            //    return;

            tipoReferido = NegTipoReferido.listaTipoReferido();

                #region Genero Atencion
                string NumeroHistoria = "";
                int CodigoPaciente = 0;
                int CodigoAtencion = 0;

                // USUARIOS Usuario = new USUARIOS();
                TIPO_REFERIDO TR = new TIPO_REFERIDO();
                PACIENTES Paciente = new PACIENTES();

                //  Usuario = NegUsuarios.RecuperaUsuario(Sesion.codUsuario);  // verifico el usuario que ingresa la atencion

                if (noExiste) // Verifica si es un paciente nuevo inserta datos del paciente
                {
                    CodigoPaciente = NegPacientes.RecuperaMaximoPacienteCodigo();//Genero el codigo del paciente
                    Paciente.PAC_CODIGO = CodigoPaciente; // Genero el numero de historia clinica del paciente 
                    //NUMERO_CONTROL numerocontrol = new NUMERO_CONTROL();

                    if (NegNumeroControl.NumerodeControlAutomatico(6))
                    {
                        //numerocontrol = NegNumeroControl.RecuperaNumeroControl().Where(cod => cod.CODCON == 6).FirstOrDefault();
                        DataTable numHC = new DataTable();
                        numHC = NegPacientes.RecuperaMaximoPacienteHistoriaClinica();
                        string historia = numHC.Rows[0][0].ToString();
                        Paciente.PAC_HISTORIA_CLINICA = historia;
                        DataTable pacienteJire = new DataTable();
                        pacienteJire = NegPacientes.PacienteJire(txtcedula.Text.Trim());
                        if (pacienteJire.Rows.Count > 0)
                        {
                            NumeroHistoria = pacienteJire.Rows[0][1].ToString();
                            if (NumeroHistoria != "0")
                                Paciente.PAC_HISTORIA_CLINICA = NumeroHistoria;
                            else
                                NumeroHistoria = Paciente.PAC_HISTORIA_CLINICA;
                        }
                        else
                            NumeroHistoria = Paciente.PAC_HISTORIA_CLINICA;

                        NegNumeroControl.LiberaNumeroControl(6);
                    }
                    else
                    {
                        // Paciente.PAC_HISTORIA_CLINICA = txt_historiaclinica.Text.Trim();
                    }

                    Paciente.USUARIOSReference.EntityKey = Usuario.EntityKey;
                    Paciente.DIPO_CODIINEC = "17";
                    Paciente.PAC_FECHA_CREACION = Convert.ToDateTime(DateTime.Now.ToString());
                    Paciente.PAC_NOMBRE1 = this.txtnombre1.Text;
                    Paciente.PAC_NOMBRE2 = this.txtnombre2.Text;
                    Paciente.PAC_APELLIDO_PATERNO = this.txtapellido1.Text;
                    Paciente.PAC_APELLIDO_MATERNO = this.txtapellido2.Text;
                    Paciente.PAC_FECHA_NACIMIENTO = dtp_fecnac.Value;
                    Paciente.PAC_NACIONALIDAD = "ECUATORIANO";

                    // verifico el tipo de identificacion del paciente / Giovanny Tapia / 05/11/2012
                    if (rbCedula.Checked == true)
                    {
                        Paciente.PAC_TIPO_IDENTIFICACION = "C";
                    }
                    if (rbPasaporte.Checked == true)
                    {
                        Paciente.PAC_TIPO_IDENTIFICACION = "P";
                    }
                    //if (rbRuc.Checked == true)
                    //{
                    //    Paciente.PAC_TIPO_IDENTIFICACION = "R";
                    //}
                    //if (rbSid.Checked == true)
                    //{
                    //    Paciente.PAC_TIPO_IDENTIFICACION = "S";
                    //}

                    //if (rbSid.Checked)
                    //{
                    //    Paciente.PAC_IDENTIFICACION = NumeroHistoria;
                    //}
                    //else
                    //{
                     Paciente.PAC_IDENTIFICACION = this.txtcedula.Text;
                    //}

                    Paciente.PAC_EMAIL = (this.txtemail.Text).Trim();
                    if (this.rbn_h.Checked == true)
                    {
                        Paciente.PAC_GENERO = "M";
                    }
                    if (this.rbn_m.Checked == true)
                    {
                        Paciente.PAC_GENERO = "F";
                    }
                    Paciente.PAC_ESTADO = true;
                    Paciente.PAC_REFERENTE_NOMBRE = "";
                    Paciente.PAC_REFERENTE_PARENTESCO = "";
                    Paciente.PAC_REFERENTE_TELEFONO = "";
                    Paciente.PAC_ALERGIAS = "";
                    Paciente.PAC_OBSERVACIONES = "";
                    Paciente.PAC_REFERENTE_DIRECCION = "";
                    Paciente.PAC_DATOS_INCOMPLETOS = false;
                    string xId = Paciente.PAC_IDENTIFICACION;
                    NegPacientes.crearPacienteSP(Paciente); //Almaceno los datos del paciente / giovanny tapia / 24/10/2012            
                    Paciente = null;
                    Paciente = NegPacientes.pacientePorIdentificacion(xId); // Creo un objeto de paciente para obtener el codigo / giovanny tapia / 24/10/2012

                }
                // ATENCION
                PACIENTES_DATOS_ADICIONALES DatosAdicionales = new PACIENTES_DATOS_ADICIONALES();
                int DapCodigo = 0;
                DapCodigo = NegPacienteDatosAdicionales.ultimoCodigoDatos(); // Genero el codigo para los datos adicionales del paciente / giovanny tapia / 24/10/2012
                DatosAdicionales.DAP_CODIGO = DapCodigo;

                DtoPacienteDatosAdicionales2 datosPaciente123 = new DtoPacienteDatosAdicionales2();
                datosPaciente123.COD_PACIENTE = Paciente.PAC_CODIGO;
                datosPaciente123.FALLECIDO = false;
                datosPaciente123.FOLIO = "";

                datosPaciente123.FEC_FALLECIDO = DateTime.Now.ToString();
                datosPaciente123.REF_TELEFONO_2 = "";//txt_telfRef2.Text.Replace("-", string.Empty).ToString();
                datosPaciente123.email = "";//txt_emailAcomp.Text;
                datosPaciente123.id_usuario = Entidades.Clases.Sesion.codUsuario;
                //se almacena el email del acompañante

                NegPacienteDatosAdicionales.PDA2_save(datosPaciente123);

                ATENCIONES Atencion = new ATENCIONES();
                Atencion.ATE_CODIGO = 0;
                Atencion.ATE_NUMERO_ATENCION = "0";
                Atencion.ATE_FECHA = Convert.ToDateTime(DateTime.Now.ToString());
                Atencion.ATE_NUMERO_CONTROL = "0";
                Atencion.ATE_FACTURA_PACIENTE = "";
                Atencion.ATE_FACTURA_FECHA = Convert.ToDateTime(DateTime.Now.ToString());
                Atencion.ATE_FECHA_INGRESO = Convert.ToDateTime(DateTime.Now.ToString());
                Atencion.ATE_FECHA_ALTA = Convert.ToDateTime(DateTime.Now.ToString());
                Atencion.ATE_REFERIDO_DE = "1";
                Atencion.ATE_EDAD_PACIENTE = 0;
                Atencion.ATE_ACOMPANANTE_NOMBRE = "";
                Atencion.ATE_ACOMPANANTE_CEDULA = "";
                Atencion.ATE_ACOMPANANTE_PARENTESCO = "";
                Atencion.ATE_ACOMPANANTE_TELEFONO = txtcedular.Text.Trim(); // Telefono Cliente
                Atencion.ATE_ACOMPANANTE_DIRECCION = txtDireccion.Text;// Direccion Paciente
                Atencion.ATE_ACOMPANANTE_CIUDAD = "";
                Atencion.ATE_GARANTE_NOMBRE = "";
                Atencion.ATE_GARANTE_CEDULA = "";
                Atencion.ATE_GARANTE_PARENTESCO = "";
                Atencion.ATE_GARANTE_MONTO_GARANTIA = 0;
                Atencion.ATE_GARANTE_TELEFONO = txtcedular.Text.Trim();// celular paciente
            Atencion.ATE_GARANTE_DIRECCION = "";
                Atencion.ATE_GARANTE_CIUDAD = "";
                Atencion.ATE_DIAGNOSTICO_INICIAL = "";
                Atencion.ATE_DIAGNOSTICO_FINAL = "";
                Atencion.ATE_OBSERVACIONES = "";
                Atencion.ATE_FACTURA_NOMBRE = "PACIENTE";
                Atencion.ATE_DIRECTORIO = "";

                if (noExiste)
                {
                    //Paciente = new PACIENTES();
                    Atencion.PACIENTESReference.EntityKey = Paciente.EntityKey;
                }
                else
                {
                    Atencion.PACIENTESReference.EntityKey = dtoPaciente.EntityKey;
                }

                Atencion.PACIENTES_DATOS_ADICIONALESReference.EntityKey = DatosAdicionales.EntityKey;
                Atencion.USUARIOSReference.EntityKey = Usuario.EntityKey;
                int codTipoReferido = Convert.ToInt16("1");
                Atencion.TIPO_REFERIDOReference.EntityKey = tipoReferido.FirstOrDefault(t => t.TIR_CODIGO == codTipoReferido).EntityKey;
                Atencion.TIF_OBSERVACION = "";
                Atencion.ATE_NUMERO_ADMISION = 1;
                Atencion.ATE_QUIEN_ENTREGA_PAC = "";
                Atencion.ATE_CIERRE_HC = true;
                //if (cmbTipoAtencion.Text == "Servicio de Farmacia" || cmbTipoAtencion.SelectedValue.ToString() == "1015")
                //{
                //    Atencion.ESC_CODIGO = 2; //lxlx se muestre en facturacion
                //}
                //else
                //{
                    Atencion.ESC_CODIGO = 1;
                //}
                Atencion.TipoAtencion = "9";//valor de consulta externa

                DataTable ResultadoAtenccion = new DataTable();
                ResultadoAtenccion = NegAtenciones.CrearAtencionSPConsultaExterna(Atencion, DapCodigo, noExiste); // Guardo los datos de la atencion / giovanny tapia / 24/10/2012

                Int64 Xate_codigo = Convert.ToInt64(ResultadoAtenccion.Rows[0][0]);
                Int64 Xnumero_atencion = Convert.ToInt64(ResultadoAtenccion.Rows[0][2]);
            ate_codigo = Xate_codigo;

                if (noExiste)
                {
                    MessageBox.Show("Los datos han sido almacenados correctamente. Historia: " + NumeroHistoria + " Atención: " + Xnumero_atencion.ToString(), "His3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Los datos han sido almacenados correctamente Atención: " + Xnumero_atencion.ToString(), "His3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                #endregion


                object[] s = new object[] { "0" };
                NegDietetica.setROW("setAsCategoria0", s, Xnumero_atencion.ToString());

                // vacio los objetos / giovanny tapia / 24/10/2012
                //Usuario = null;
                Paciente = null;
                Atencion = null;
                DatosAdicionales = null;
                TR = null;

                //btnAddProd.Enabled = false;
        }

        public Int64 ate_codigo = 0;
        public void saveSintomas()
        {
            foreach (DataGridViewRow item in TablaMotivos.Rows)
            {
                if ((bool)item.Cells["seleccionar"].Value)
                {
                    try
                    {
                        NegConsultaExterna.GuardarAtencionSintomas(ate_codigo, Convert.ToInt64(item.Cells["codigo"].Value));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void txtcedular_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            HabilitarCampos(false);
            btnGuardar.Enabled = true;
        }

        private void txtapellido1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                txtapellido2.Focus();
            }
        }

        private void txtapellido2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtnombre1.Focus();
            }
        }

        private void txtnombre1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtnombre2.Focus();
            }
        }

        private void txtnombre2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDireccion.Focus();
            }
        }

        private void txtDireccion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtcedular.Focus();
            }
        }

        private void txtcedular_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                rbn_h.Focus();
            }
        }

        private void rbn_h_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtemail.Focus();
            }
        }

        private void rbn_m_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtemail.Focus();
            }
        }

        private void txtemail_KeyDown(object sender, KeyEventArgs e)
        {
            dtp_fecnac.Focus();
        }
    }
}
