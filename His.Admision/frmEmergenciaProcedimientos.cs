using System;
using System.Windows.Forms;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using Infragistics.Win;
using System.Linq;
using System.Text;
using His.Parametros;
using His.Negocio;
using His.Entidades;
using His.Entidades.Clases;
using His.Entidades.Pedidos;
using His.DatosReportes;
using Infragistics.Win.UltraWinGrid;

namespace His.Admision
{
    public partial class frmEmergenciaProcedimientos : Form
    {

        USUARIOS Usuario = NegUsuarios.RecuperaUsuario(Sesion.codUsuario);
        String DepartamentoUser = "";


        List<TIPO_REFERIDO> tipoReferido = new List<TIPO_REFERIDO>();
        Boolean TipoIngreso = false;
        PACIENTES PacienteRecupera = new PACIENTES();
        List<TIPO_EMPRESA> tipoEmpresa = new List<TIPO_EMPRESA>();
        private static Int64 Xate_codigo;
        private static Int64 Xnumero_atencion;
        private static Int64 codigo_pedido;
        private static Int64 area;
        private static int valida = 0;
        public static string id = "";
        public double bodega;
        //int cod_ate = 0;

#pragma warning disable CS0414 // El campo 'frmEmergenciaProcedimientos.HC' está asignado pero su valor nunca se usa
        int HC = 0;
#pragma warning restore CS0414 // El campo 'frmEmergenciaProcedimientos.HC' está asignado pero su valor nunca se usa

        public frmEmergenciaProcedimientos()
        {
            InitializeComponent();
        }

        private void frmEmergenciaProcedimientos_Load(object sender, EventArgs e)
        {


            DataTable x = NegDietetica.getDataTable("GetUSERdepartamento", Sesion.codUsuario.ToString());
            DepartamentoUser = x.Rows[0][0].ToString();
            if (DepartamentoUser.Trim().ToUpper() == "ENFERMERIA")
            {
                btnNUEVO.Visible = false;
                btnSAVE.Visible = false;
                btnCANCELAR.Visible = false;
            }
            tipoEmpresa = NegAseguradoras.ListaTiposEmpresa();
            tipoReferido = NegTipoReferido.listaTipoReferido();
            TipoIngreso = true;// Crea una nueva historia y atencion / 20121105
            cmb_tiporeferido.DataSource = tipoReferido;
            cmb_tiporeferido.ValueMember = "TIR_CODIGO";
            cmb_tiporeferido.DisplayMember = "TIR_NOMBRE";
            cmb_tiporeferido.AutoCompleteSource = AutoCompleteSource.ListItems;

            cmbConvenioPago.DataSource = tipoEmpresa;
            cmbConvenioPago.DisplayMember = "TE_DESCRIPCION";
            cmbConvenioPago.ValueMember = "TE_CODIGO";
            cmbConvenioPago.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbConvenioPago.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;


            cmbTipoAtencion.DataSource = NegAtenciones.TiposAtenciones("8");
            cmbTipoAtencion.DisplayMember = "name";
            cmbTipoAtencion.ValueMember = "id";
            cmbTipoAtencion.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbTipoAtencion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;

            cmb_Areas.DataSource = NegPedidos.recuperarListaAreasTodas().OrderBy(a => a.PEA_NOMBRE).ToList();
            cmb_Areas.ValueMember = "PEA_CODIGO";
            cmb_Areas.DisplayMember = "PEA_NOMBRE";
            cmb_Areas.SelectedIndex = 0;

            cmb_Rubros.DataSource = NegRubros.recuperarRubros();
            cmb_Rubros.DisplayMember = "RUB_NOMBRE".Trim();
            cmb_Rubros.ValueMember = "RUB_CODIGO";
            cmb_Rubros.SelectedIndex = 0;

            limpiar();

        }

        private void limpiar()
        {
            Xate_codigo = 0;
            TipoIngreso = true;// Crea una nueva historia y atencion / 20121105
            btnSAVE.Enabled = true;
            txtRuc.Text = string.Empty;
            txtApellidoPaterno.Text = string.Empty;
            txtApellidoMaterno.Text = string.Empty;
            txtNombre1.Text = string.Empty;
            txtNombre2.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtCelular.Text = string.Empty;
            txt_email.Text = string.Empty;
            dtp_fecnac.Value = DateTime.Now;
            cmb_tiporeferido.SelectedIndex = 0;
            cmbTipoAtencion.SelectedIndex = 0;
            btnSAVE.Enabled = false;
            gbId.Enabled = false;
            gbDatos.Enabled = false;
            gbAtencion.Enabled = false;
            cmbTipoAtencion.Enabled = true;
            cmbConvenioPago.Enabled = false;
            cmb_tiporeferido.Enabled = false;
            cb_seguros.Enabled = false;
            btnAddProd.Enabled = false;
            //txtRuc.Focus();
            txtRuc.Text = String.Empty;
            gridDetalleFactura.DataSource = null;

            btnNUEVO.Enabled = true;
            btnMODIFICA.Enabled = true;
            btnSAVE.Enabled = false;
            btnCANCELAR.Enabled = false;
            valida = 0;
            lblAtencion.Text = "";
        }

        #region manejo del focus con el TAB
        private void txtRuc_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            //{
            //    e.Handled = true;
            //    SendKeys.SendWait("{TAB}");
            //}
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                Generar();
            }
        }



        private void txtNombre1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txtNombre2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txtApellidoPaterno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txtApellidoMaterno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txtCelular_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void cmbTipoAtencion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }
        #endregion


        private void Generar()
        {
            PacienteRecupera = null;
            DataTable pacienteJire = new DataTable();
            DataTable Empleado = new DataTable();

            if (rbCedula.Checked || rbEmp.Checked)
            {
                PacienteRecupera = NegPacientes.pacientePorIdentificacion(txtRuc.Text);
                if (PacienteRecupera == null)
                {
                    pacienteJire = NegPacientes.PacienteJire(txtRuc.Text.Trim());
                }
                if (rbEmp.Checked && pacienteJire.Rows.Count == 0)
                {
                    Empleado = NegAtenciones.RecuperaEmpleadoSC(txtRuc.Text);
                }
            }

            if (PacienteRecupera != null || pacienteJire.Rows.Count > 0 || Empleado.Rows.Count > 0)
            #region cargarPaciente
            {
                if (PacienteRecupera != null)
                {
                    DataTable dtFallecido = NegDietetica.getDataTable("Fallecido", PacienteRecupera.PAC_CODIGO.ToString());
                    if (dtFallecido.Rows.Count > 0)
                    {
                        MessageBox.Show("El paciente esta registrado como fallecido, no puede generar atención.", "HIS3000");
                        return;
                    }
                }


                //DialogResult result;
                //result = MessageBox.Show("El paciente ya esta registrado. Desea cargar los datos.?.", "His3000", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                //if (result == System.Windows.Forms.DialogResult.Yes)
                //{
                #region carga Datos del Paciente en formulario
                if (PacienteRecupera != null)
                {
                    txtRuc.Text = PacienteRecupera.PAC_IDENTIFICACION;
                    txtApellidoPaterno.Text = PacienteRecupera.PAC_APELLIDO_PATERNO;
                    txtApellidoMaterno.Text = PacienteRecupera.PAC_APELLIDO_MATERNO;
                    txtNombre1.Text = PacienteRecupera.PAC_NOMBRE1;
                    txtNombre2.Text = PacienteRecupera.PAC_NOMBRE2;
                    txt_email.Text = PacienteRecupera.PAC_EMAIL;
                    dtp_fecnac.Value = PacienteRecupera.PAC_FECHA_NACIMIENTO.Value.Date;
                    switch (PacienteRecupera.PAC_GENERO)
                    {
                        case "M":
                            rbn_h.Checked = true;
                            break;
                        case "F":
                            rbn_m.Checked = true;
                            break;
                    }
                    switch (PacienteRecupera.PAC_TIPO_IDENTIFICACION)
                    {
                        case "R":
                            rbRuc.Checked = true;
                            break;
                        case "C":
                            rbCedula.Checked = true;
                            break;
                        case "P":
                            rbPasaporte.Checked = true;
                            break;
                        default:
                            rbSid.Checked = true;
                            break;
                    }
                    //Datos Adicionales
                    PACIENTES_DATOS_ADICIONALES DatosAdicionales = new PACIENTES_DATOS_ADICIONALES();


                    DatosAdicionales = NegPacienteDatosAdicionales.RecuperarDatosAdicionalesPaciente(PacienteRecupera.PAC_CODIGO);
                    if (DatosAdicionales != null)
                    {
                        txtDireccion.Text = DatosAdicionales.DAP_DIRECCION_DOMICILIO;
                        txtTelefono.Text = DatosAdicionales.DAP_TELEFONO1;
                        txtCelular.Text = DatosAdicionales.DAP_TELEFONO2;
                    }
                    gbDatos.Enabled = false;
                    cmbTipoAtencion.Focus();
                    TipoIngreso = false; // Crea solamente la atencion / 20121105                  

                }
                else if (pacienteJire.Rows.Count > 0)
                {
                    txtRuc.Text = pacienteJire.Rows[0][0].ToString();
                    txtApellidoPaterno.Text = pacienteJire.Rows[0][2].ToString();
                    txtApellidoMaterno.Text = pacienteJire.Rows[0][3].ToString();
                    txtNombre1.Text = pacienteJire.Rows[0][4].ToString();
                    txtNombre2.Text = pacienteJire.Rows[0][5].ToString();
                    txtDireccion.Text = pacienteJire.Rows[0][6].ToString();
                    txt_email.Text = pacienteJire.Rows[0][9].ToString();
                    dtp_fecnac.Value = Convert.ToDateTime(pacienteJire.Rows[0][7].ToString());
                    gbDatos.Enabled = true;
                    txtTelefono.Focus();
                }
                else if (Empleado.Rows.Count > 0)
                {
                    string nombre = Empleado.Rows[0][0].ToString();
                    string[] nombres = nombre.Split(' ');
                    if (nombres.Length > 3)
                    {
                        txtApellidoPaterno.Text = nombres[0].ToString();
                        txtApellidoMaterno.Text = nombres[1].ToString();
                        txtNombre1.Text = nombres[2].ToString();
                        txtNombre2.Text = nombres[3].ToString();
                    }
                    else if (nombres.Length == 3)
                    {
                        txtApellidoPaterno.Text = nombres[0].ToString();
                        txtApellidoMaterno.Text = nombres[1].ToString();
                        txtNombre1.Text = nombres[2].ToString();
                    }
                    else
                    {
                        txtApellidoPaterno.Text = nombres[0].ToString();
                        txtNombre1.Text = nombres[1].ToString();
                    }
                    txtDireccion.Text = Empleado.Rows[0][1].ToString();
                    txtCelular.Text = Empleado.Rows[0][2].ToString();
                    txt_email.Text = Empleado.Rows[0][4].ToString();
                    dtp_fecnac.Value = Convert.ToDateTime("1990-01-01");
                    if (rbEmp.Checked)
                    {
                        cmbTipoAtencion.SelectedValue = 1015;
                        cmbConvenioPago.SelectedIndex = 1;
                    }
                    gbDatos.Enabled = true;
                }

                #endregion
                btnSAVE.Enabled = true;
                btnCANCELAR.Enabled = true;
                gbId.Enabled = false;

                gbAtencion.Enabled = true;
                cmbTipoAtencion.Enabled = true;
                cmbConvenioPago.Enabled = true;
                cmb_tiporeferido.Enabled = true;
                cb_seguros.Enabled = true;
               
                return;
            }
            #endregion

            //SI NO EXISTE PRIMERO VALIDA CEDULA O RUC
            if (rbCedula.Checked == true || rbRuc.Checked == true)
            {
                if (this.txtRuc.Text.ToString() != string.Empty)
                {
                    if (NegValidaciones.esCedulaValida(txtRuc.Text) != true)
                    {
                        MessageBox.Show("Cédula Incorrecta");
                        txtRuc.Focus();
                        txtRuc.SelectAll();
                        return;
                    }
                    else
                    {
                        btnSAVE.Enabled = true;
                        gbId.Enabled = false;
                        gbDatos.Enabled = true;
                        gbAtencion.Enabled = true;
                        //btnAddProd.Enabled = true;
                        txtApellidoPaterno.Focus();
                        gbAtencion.Enabled = true;
                        cmbTipoAtencion.Enabled = true;
                        cmbConvenioPago.Enabled = true;
                        cmb_tiporeferido.Enabled = true;
                        cb_seguros.Enabled = true;
                        return;
                    }
                }
            }
            if (rbSid.Checked)
            {
                btnSAVE.Enabled = true;
                gbId.Enabled = false;
                gbDatos.Enabled = true;
                gbAtencion.Enabled = true;
                cmbConvenioPago.Enabled = true;
                cmb_tiporeferido.Enabled = true;
                cb_seguros.Enabled = true;
                //   btnAddProd.Enabled = true;
                txtApellidoPaterno.Focus();
                return;
            }
            if (rbPasaporte.Checked)
            {
                if (txtRuc.Text.Trim().Length > 0)
                {
                    btnSAVE.Enabled = true;
                    gbId.Enabled = false;
                    gbDatos.Enabled = true;
                    gbAtencion.Enabled = true;
                    cmbConvenioPago.Enabled = true;
                    cmb_tiporeferido.Enabled = true;
                    cb_seguros.Enabled = true;
                    //   btnAddProd.Enabled = true;
                    txtApellidoPaterno.Focus();
                }
                else
                {
                    txtRuc.Focus();
                }

                return;
            }
        }




        private void AgregarError(Control control)
        {
            erroresPaciente.SetError(control, "Campo Requerido");
        }


        private Boolean validar()
        {
            try
            {
                erroresPaciente.Clear();

                bool valido = true;
                if (txtNombre1.Text.Trim() == string.Empty)
                {
                    AgregarError(txtNombre1);
                    valido = false;
                }
                if (txtApellidoPaterno.Text.Trim() == string.Empty)
                {
                    AgregarError(txtApellidoPaterno);
                    valido = false;
                }
                if (txtCelular.Text.Replace("(", "").Replace(")", "").Replace("-", "").Trim() == string.Empty && txtTelefono.Text.Replace("(", "").Replace(")", "").Replace("-", "").Trim() == string.Empty)
                {
                    AgregarError(txtCelular);
                    valido = false;
                }
                if ((cb_seguros.SelectedItem == null))
                {
                    AgregarError(cb_seguros);
                    valido = false;
                }
                if ((txtDireccion.Text.Trim() == string.Empty))
                {
                    AgregarError(txtDireccion);
                    valido = false;
                }
                return valido;
            }
            catch (System.Exception err)
            {
                MessageBox.Show(err.Message);
                return false;
            }
        }


        private void saveAtencion()
        {
            if (!validar())
                return;


            if (Xate_codigo == 0)
            {
                #region Genero Atencion
                string NumeroHistoria = "";
                int CodigoPaciente = 0;
#pragma warning disable CS0219 // La variable 'CodigoAtencion' está asignada pero su valor nunca se usa
                int CodigoAtencion = 0;
#pragma warning restore CS0219 // La variable 'CodigoAtencion' está asignada pero su valor nunca se usa

                // USUARIOS Usuario = new USUARIOS();
                TIPO_REFERIDO TR = new TIPO_REFERIDO();
                PACIENTES Paciente = new PACIENTES();

                //  Usuario = NegUsuarios.RecuperaUsuario(Sesion.codUsuario);  // verifico el usuario que ingresa la atencion

                if (TipoIngreso) // Verifica si es un paciente nuevo inserta datos del paciente
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
                        pacienteJire = NegPacientes.PacienteJire(txtRuc.Text.Trim());
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
                    Paciente.PAC_NOMBRE1 = this.txtNombre1.Text;
                    Paciente.PAC_NOMBRE2 = this.txtNombre2.Text;
                    Paciente.PAC_APELLIDO_PATERNO = this.txtApellidoPaterno.Text;
                    Paciente.PAC_APELLIDO_MATERNO = this.txtApellidoMaterno.Text;
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
                    if (rbRuc.Checked == true)
                    {
                        Paciente.PAC_TIPO_IDENTIFICACION = "R";
                    }
                    if (rbSid.Checked == true)
                    {
                        Paciente.PAC_TIPO_IDENTIFICACION = "S";
                    }

                    if (rbSid.Checked)
                    {
                        Paciente.PAC_IDENTIFICACION = NumeroHistoria;
                    }
                    else
                    {
                        Paciente.PAC_IDENTIFICACION = this.txtRuc.Text;
                    }

                    Paciente.PAC_EMAIL = (this.txt_email.Text).Trim();
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
                datosPaciente123.id_usuario = Sesion.codUsuario;
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
                Atencion.ATE_ACOMPANANTE_TELEFONO = txtTelefono.Text.Replace("(", "").Replace(")", "").Replace("-", ""); // Telefono Cliente
                Atencion.ATE_ACOMPANANTE_DIRECCION = txtDireccion.Text;// Direccion Paciente
                Atencion.ATE_ACOMPANANTE_CIUDAD = "";
                Atencion.ATE_GARANTE_NOMBRE = "";
                Atencion.ATE_GARANTE_CEDULA = "";
                Atencion.ATE_GARANTE_PARENTESCO = "";
                Atencion.ATE_GARANTE_MONTO_GARANTIA = 0;
                Atencion.ATE_GARANTE_TELEFONO = txtCelular.Text.Replace("(", "").Replace(")", "").Replace("-", ""); // celular paciente
                Atencion.ATE_GARANTE_DIRECCION = "";
                Atencion.ATE_GARANTE_CIUDAD = "";
                Atencion.ATE_DIAGNOSTICO_INICIAL = "";
                Atencion.ATE_DIAGNOSTICO_FINAL = "";
                Atencion.ATE_OBSERVACIONES = "";
                Atencion.ATE_FACTURA_NOMBRE = "PACIENTE";
                Atencion.ATE_DIRECTORIO = "";

                if (TipoIngreso == true)
                {
                    //Paciente = new PACIENTES();
                    Atencion.PACIENTESReference.EntityKey = Paciente.EntityKey;
                }
                else
                {
                    Atencion.PACIENTESReference.EntityKey = PacienteRecupera.EntityKey;
                }

                Atencion.PACIENTES_DATOS_ADICIONALESReference.EntityKey = DatosAdicionales.EntityKey;
                Atencion.USUARIOSReference.EntityKey = Usuario.EntityKey;
                int codTipoReferido = Convert.ToInt16(cmb_tiporeferido.SelectedValue);
                Atencion.TIPO_REFERIDOReference.EntityKey = tipoReferido.FirstOrDefault(t => t.TIR_CODIGO == codTipoReferido).EntityKey;
                Atencion.TIF_OBSERVACION = "";
                Atencion.ATE_NUMERO_ADMISION = 1;
                Atencion.ATE_QUIEN_ENTREGA_PAC = "";
                Atencion.ATE_CIERRE_HC = true;
                if (cmbTipoAtencion.Text == "Servicio de Farmacia" || cmbTipoAtencion.SelectedValue.ToString() == "1015")
                {
                    Atencion.ESC_CODIGO = 2; //lxlx se muestre en facturacion
                }
                else
                {
                    Atencion.ESC_CODIGO = 1;
                }
                Atencion.TipoAtencion = (cmbTipoAtencion.SelectedValue).ToString();

                DataTable ResultadoAtenccion = new DataTable();
                ResultadoAtenccion = NegAtenciones.CrearAtencionSP(Atencion, DapCodigo, TipoIngreso); // Guardo los datos de la atencion / giovanny tapia / 24/10/2012

                Xate_codigo = Convert.ToInt64(ResultadoAtenccion.Rows[0][0]);
                Xnumero_atencion = Convert.ToInt64(ResultadoAtenccion.Rows[0][2]);

                if (TipoIngreso == true)
                {
                    MessageBox.Show("Los datos han sido almacenados correctamente. Historia: " + NumeroHistoria + " Atención: " + Xnumero_atencion.ToString(), "His3000");
                }
                else
                {
                    MessageBox.Show("Los datos han sido almacenados correctamente Atención: " + Xnumero_atencion.ToString(), "His3000");
                }
                #endregion


                object[] s = new object[] { cb_seguros.SelectedValue };
                NegDietetica.setROW("setAsCategoria0", s, Xnumero_atencion.ToString());

                // vacio los objetos / giovanny tapia / 24/10/2012
                //Usuario = null;
                Paciente = null;
                Atencion = null;
                DatosAdicionales = null;
                TR = null;
                btnSAVE.Enabled = false;
                PacienteRecupera = null;
                gbAtencion.Enabled = true;
                cmbTipoAtencion.Enabled = true;
                cmbConvenioPago.Enabled = false;
                cmb_tiporeferido.Enabled = false;
                cb_seguros.Enabled = false;
                //btnAddProd.Enabled = false;
                gbDatos.Enabled = false;
                gbId.Enabled = false;
            }
        }

        private void btnNuevo_Click_1/*GUARDAR*/(object sender, EventArgs e)
        {
            if (His.Parametros.FacturaPAR.BodegaPorDefecto == 1)
            {
                if (NegAccesoOpciones.ParametroBodega())
                {
                    His.Parametros.FacturaPAR.BodegaPorDefecto = 10;
                }
            }
            saveAtencion();
            btnAddProd.Enabled = true;
            lblAtencion.Text = "Atención " + Xnumero_atencion;
            ////REFRESCAR///GRID///PRODUCTOS////



            DataTable auxProductos = NegDietetica.getDataTable("GetProductos", Xate_codigo.ToString());
            if (auxProductos.Rows.Count > 0)
            {
                gridDetalleFactura.DataSource = auxProductos;
            }
            else
                gridDetalleFactura.DataSource = null;

        }



        private void toolStripButton2_Click/*SALIR*/(object sender, EventArgs e)
        {
            this.Dispose();
            valida = 0;
        }

        private void limpiar(object sender, EventArgs e)
        {

            limpiar();
            lblAtencion.Text = "Nueva Atención";
            gbId.Enabled = true;
            btnNUEVO.Enabled = false;
            btnMODIFICA.Enabled = false;
            btnSAVE.Enabled = true;
            btnCANCELAR.Enabled = true;
            txtRuc.Focus();
        }

        private void rbSid_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSid.Checked)
            {
                txtRuc.Text = string.Empty;
                txtRuc.ReadOnly = true;
                txtRuc.Focus();
            }

        }

        private void cmbConvenioPago_SelectedValueChanged(object sender, EventArgs e)
        {
            TIPO_EMPRESA tfp = (TIPO_EMPRESA)cmbConvenioPago.SelectedItem;

            //if (btnGuardar.Enabled == true)
            {
                cb_seguros.DataSource = NegCategorias.ListaCategorias(tfp);
                cb_seguros.ValueMember = "CAT_CODIGO";
                cb_seguros.DisplayMember = "CAT_NOMBRE";
                cb_seguros.AutoCompleteSource = AutoCompleteSource.ListItems;
                cb_seguros.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
                //  btnAddAseg.Enabled = true;
                cb_seguros.Enabled = true;
                //if (tfp.TIF_CODIGO == 3 || tfp.TIF_CODIGO == 4)
                //{
                //    btnAddAseg.Enabled = true;
                //    cb_seguros.Enabled = true;
                //}
                //else
                //{
                //    btnAddAseg.Enabled = false;
                //    cb_seguros.Enabled = false;
                //}
            }
        }

        private void txtApellidoPaterno_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTelefono_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txtCelular_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void dtp_fecnac_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_email_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }

            e.Handled = char.IsWhiteSpace(e.KeyChar);
        }

        private void rbn_h_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void rbn_m_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void cmbTipoAtencion_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void cmb_tiporeferido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void cmbConvenioPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void cb_seguros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void menu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }
        NegQuirofano Quirofano = new NegQuirofano();
        private void btnAddProd_Click(object sender, EventArgs e)
        {
            DataTable auxProductos = new DataTable();
            Int32 Pedido = 0;
            //Guardo antes de agregar los productos
            if (Xate_codigo == 0)
            {
                saveAtencion();

            }

            //lblAtencion.Text = "Atención " + Xnumero_atencion;
            #region Generar pedido e imprime tcket pedido
            //if (!btnSAVE.Enabled)
            {
                /////////////////////////////////
                //Instancio la ventana de pedidos para armar List
                //////////////////////////////////
                Int16 rubcodigo = 0;
                Int16 pedcodigo = 0;
                try
                {
                    DataTable x = NegDietetica.getDataTable("GetTipoAtencion", cmbTipoAtencion.SelectedValue.ToString());
                    rubcodigo = Convert.ToInt16(x.Rows[0]["RUB_CODIGO"]);
                    pedcodigo = Convert.ToInt16(x.Rows[0]["PEA_CODIGO"]);

                }
                catch
                {
                    Console.WriteLine("Sin instancia de  tip0 atencion");
                }

                if (rubcodigo != 0)
                    cmb_Rubros.SelectedValue = rubcodigo;
                if (pedcodigo != 0)
                    cmb_Areas.SelectedValue = pedcodigo;

                PEDIDOS_AREAS ar = new PEDIDOS_AREAS();
                RUBROS Rubro1 = new RUBROS();
                Rubro1 = (RUBROS)cmb_Rubros.SelectedItem;
                ar = (PEDIDOS_AREAS)cmb_Areas.SelectedItem;
                area = ar.PEA_CODIGO;
                var Rubro = (Int16)Rubro1.RUB_CODIGO;

                frmAyudaProductos form;

                if (rubcodigo == 0 && pedcodigo == 0)
                    form = new frmAyudaProductos(null, null, true);
                else if (rubcodigo == 0 && pedcodigo != 0)
                {
                    form = new frmAyudaProductos((PEDIDOS_AREAS)(cmb_Areas.SelectedItem), null, false);
                }
                else
                    form = new frmAyudaProductos((PEDIDOS_AREAS)(cmb_Areas.SelectedItem), (RUBROS)(this.cmb_Rubros.SelectedItem), false);
                form.ShowDialog();
                ///////////////recibo listado de productos desde el FormDialog
                List<PEDIDOS_DETALLE> listaProductosSolicitados = form.PedidosDetalle;
                /////////////////////////////////
                //Proceso para guardar
                /////////////////////////////////
                if (listaProductosSolicitados != null && listaProductosSolicitados.Count > 0)
                {
                    Double totales = 0.00;
                    Double valores = 0.00;
                    Double ivaTotal = 0.00;
                    PEDIDOS pedido = new PEDIDOS();
                    pedido.PED_CODIGO = NegPedidos.ultimoCodigoPedidos() + 1;
                    pedido.PED_FECHA = DateTime.Now; /*PARA GUARDAR EL PEDIDO SE NECESITA FECHA Y HORA/ GIOVANNY TAPIA / 12/04/2013*/
                    pedido.PED_DESCRIPCION = form.descripcion;
                    pedido.PED_ESTADO = 2;
                    pedido.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                    pedido.ATE_CODIGO = Convert.ToInt32(Xate_codigo);
                    ArchivoIni archivo = new ArchivoIni(Environment.CurrentDirectory + "\\his3000.ini");
                    byte codigoEstacion = Convert.ToByte(archivo.IniReadValue("Pedidos", "estacion"));
                    pedido.PEE_CODIGO = codigoEstacion; /* CODIGO DE LA ESTACION DESDE DONDE SE REALIZA EL PEDIDO / GIOVANNY TAPIA / 04/01/2012 */
                    pedido.TIP_PEDIDO = His.Parametros.FarmaciaPAR.PedidoMedicamentos;
                    pedido.PED_PRIORIDAD = 1;
                    pedido.MED_CODIGO = 0;
                    pedido.PEDIDOS_AREASReference.EntityKey = ((PEDIDOS_AREAS)(cmb_Areas.SelectedItem)).EntityKey;
                    if (form.codTransaccion != 0)
                    {
                        pedido.PED_TRANSACCION = form.codTransaccion;
                    }
                    else
                    {
                        pedido.PED_TRANSACCION = pedido.PED_CODIGO;
                    }
                    Pedido = pedido.PED_CODIGO;
                    codigo_pedido = pedido.PED_CODIGO;
                    NegPedidos.crearPedido(pedido);

                    #region CreaPedido
                    foreach (var solicitud in listaProductosSolicitados)
                    {
                        if (Convert.ToInt32(ar.PEA_CODIGO) != 1)
                        {
                            if (Convert.ToInt32(ar.PEA_CODIGO) == 16)
                            {
                                DtoDetalleHonorariosMedicos HonorariosMedico = new DtoDetalleHonorariosMedicos();
                                HonorariosMedico = form.DetalleHonorarios.FirstOrDefault(h => h.CODPRO == solicitud.PRODUCTOReference.EntityKey.EntityKeyValues[0].Value.ToString());
                                if (HonorariosMedico != null)
                                {
                                    pedido.MED_CODIGO = HonorariosMedico.MED_CODIGO;
                                }
                            }
                            solicitud.PEDIDOSReference.EntityKey = pedido.EntityKey;
                            solicitud.PDD_CODIGO = NegPedidos.ultimoCodigoPedidosDetalles() + 1;
                            // --alexalexalex
                            if ((rubcodigo == 0 || pedcodigo == 100 || rubcodigo == 100 || pedcodigo == 0))
                            {
                                Int32 codpro = Convert.ToInt32(solicitud.PRO_CODIGO_BARRAS.ToString());
                                Int32 xcodDiv = 0;
                                Int16 XRubro = 0;
                                DataTable auxDT = NegFactura.recuperaCodRubro(codpro);
                                foreach (DataRow row in auxDT.Rows)
                                {
                                    XRubro = Convert.ToInt16(row[0].ToString());
                                    xcodDiv = Convert.ToInt32(row[1].ToString());
                                }
                                NegPedidos.crearDetallePedido(solicitud, pedido, XRubro, xcodDiv, form.NumVale);
                                Quirofano.ProductoBodega(codpro.ToString(), solicitud.PDD_CANTIDAD.ToString(), FacturaPAR.BodegaPorDefecto);
                            }
                            else
                            {
                                NegPedidos.crearDetallePedido(solicitud, pedido, Rubro, Convert.ToInt32(ar.DIV_CODIGO), form.NumVale);
                                Quirofano.ProductoBodega(solicitud.PRO_CODIGO_BARRAS, solicitud.PDD_CANTIDAD.ToString(), FacturaPAR.BodegaPorDefecto);
                            }
                        }
                        else
                        {
                            string CodigoProducto = "";
                            decimal ValorIva = 0;
                            solicitud.PEDIDOSReference.EntityKey = pedido.EntityKey;
                            solicitud.PDD_CODIGO = NegPedidos.ultimoCodigoPedidosDetalles() + 1;
                            CodigoProducto = Convert.ToString(solicitud.PRODUCTOReference.EntityKey.EntityKeyValues[0].Value).Substring(0, 1);
                            ValorIva = Convert.ToDecimal(solicitud.PDD_IVA);
                            if (ValorIva > 0)
                            {
                                NegPedidos.crearDetallePedido(solicitud, pedido, 27/*SUMINISTROS*/ , 1, form.NumVale);
                                Quirofano.ProductoBodega(CodigoProducto, solicitud.PDD_CANTIDAD.ToString(), FacturaPAR.BodegaPorDefecto);
                            }
                            else
                            {
                                NegPedidos.crearDetallePedido(solicitud, pedido, 1/*MEDICAMENTOS*/ , 1, form.NumVale);
                                Quirofano.ProductoBodega(CodigoProducto, solicitud.PDD_CANTIDAD.ToString(), FacturaPAR.BodegaPorDefecto);

                            }
                        }
                        #endregion

                        #region /*HONORARIOS MEDICOS*/


                        DtoDetalleHonorariosMedicos Honorarios = new DtoDetalleHonorariosMedicos();
                        Honorarios = form.DetalleHonorarios.FirstOrDefault(h => h.CODPRO == solicitud.PRODUCTOReference.EntityKey.EntityKeyValues[0].Value.ToString());
                        if (Honorarios != null)
                        {
                            Honorarios.PED_CODIGO = Pedido;
                            Honorarios.ID_LINEA = solicitud.PDD_CODIGO;
                            NegPedidos.GuardaDetalleHonorarios(Honorarios);
                        }
                        #endregion
                        totales = totales + Convert.ToDouble(solicitud.PDD_TOTAL);
                        valores = valores + Convert.ToDouble(solicitud.PDD_VALOR);
                        ivaTotal = ivaTotal + Convert.ToDouble(solicitud.PDD_IVA);
                    }
                    List<DtoDetalleCuentaPaciente> listaCuenta = NegDetalleCuenta.recuperarCuentaPaciente(Convert.ToInt32(Xate_codigo));

                    //cargarDetalleFactura();
                    MessageBox.Show("Pedido No." + Pedido.ToString() + " fue ingresado correctamente.", "His3000");
                    /*impresion*/
                    frmImpresionPedidos frmPedidos = new frmImpresionPedidos(Pedido, ar.PEA_CODIGO, 1, 3);
                    frmPedidos.Show();

                    ////REFRESCAR///GRID///PRODUCTOS////
                    auxProductos = NegDietetica.getDataTable("GetProductos", Xate_codigo.ToString());
                    gridDetalleFactura.DataSource = auxProductos;
                }
            }
            #endregion

            #region AGENDANDO CITA IMAGEN
            if (cmbTipoAtencion.Text.Contains("Imagen"))
            {
                if (auxProductos.Rows.Count > 0)
                {
                    DataTable dtRad = NegDietetica.getDataTable("getTurnoRadiologo");
                    DataTable dtTec = NegDietetica.getDataTable("getTurnoTecnologo");
                    string codRadiologo = "0";
                    string codTecnologo = "0";
                    if (dtRad.Rows.Count == 1)
                        codRadiologo = dtRad.Rows[0][0].ToString();
                    if (dtTec.Rows.Count == 1)
                        codTecnologo = dtTec.Rows[0][0].ToString();
                    string[] a = new string[] { DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"), Xate_codigo.ToString(), DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"),
                                                codTecnologo,  //txtCODTecnologo.Text.Trim(),
                                                codRadiologo,  //txtCODRadiologo.Text.Trim(),
                                                "Agendamiento automático desde Servicios Externos",  His.Entidades.Clases.Sesion.codUsuario.ToString()
                                                };
                    List<PedidoImagen_estudios> ListEstudios = new List<PedidoImagen_estudios>();
                    DataTable xix = NegDietetica.getDataTable("GetProductos1", Xate_codigo.ToString());
                    foreach (DataRow row in xix.Rows)
                    {
                        PedidoImagen_estudios estudio = new PedidoImagen_estudios();
                        estudio.PRO_CODIGO = Convert.ToInt32(row["CUE_CODIGO"]);
                        estudio.PRO_CODSUB = Convert.ToInt32(row["CUE_CODIGO"]);
                        ListEstudios.Add(estudio);
                    }
                    NegImagen.saveAgendamiento(0, a, ListEstudios);

                    //MessageBox.Show("Se genero el agendamiento exitosamente");

                    ////GENERANDO TICKET
                    DataTable auxLTD = NegDietetica.getDataTable("getLastTurnoDietetica", Xate_codigo.ToString());
                    string[] LTD = new string[] { auxLTD.Rows[0]["HC"].ToString(),
                                                auxLTD.Rows[0]["ATE_COD"].ToString(),
                                                auxLTD.Rows[0]["PACIENTE"].ToString(),
                                                auxLTD.Rows[0]["FECHA"].ToString(),
                                                auxLTD.Rows[0]["TECNOLOGO"].ToString(),
                                                auxLTD.Rows[0]["RAIOLOGO"].ToString(),
                                                auxLTD.Rows[0]["USUARIO"].ToString()
                                                };
                    ReportesHistoriaClinica r3x = new ReportesHistoriaClinica(); r3x.DeleteTable("rptTurnoDietetica");
                    ReportesHistoriaClinica r4 = new ReportesHistoriaClinica(); r4.InsertTable("rptTurnoDietetica", LTD);
                    //llamo al reporte
                    frmReportes form = new frmReportes();
                    form.reporte = "rptTurnoDietetica";
                    form.Show();

                }
            }
            #endregion

        }

        private void gridDetalleFactura_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            UltraGridRow fila = gridDetalleFactura.ActiveRow;
            frm_Devolucion.ate_codigo = Convert.ToInt64(fila.Cells["ATE_CODIGO"].Value.ToString());
            frm_Devolucion.codigopedido = Convert.ToInt64(fila.Cells["Codigo_Pedido"].Value.ToString());
            frm_Devolucion x = new frm_Devolucion();
            x.ShowDialog();

            DataTable auxProductos = NegDietetica.getDataTable("GetProductos", Xate_codigo.ToString());
            gridDetalleFactura.DataSource = auxProductos;

            List<DtoDetalleCuentaPaciente> listaCuenta = new List<DtoDetalleCuentaPaciente>();
            listaCuenta = NegDetalleCuenta.recuperarCuentaPaciente(Convert.ToInt32(Xate_codigo));

            if (x.DevolucionNumero != 0)
            {
                His.Admision.frmImpresionPedidos z = new frmImpresionPedidos(x.DevolucionNumero, 100, 1, 0);
                z.Show();
            }

            //frmImpresionPedidos frmPedidos = new frmImpresionPedidos(Convert.ToInt32(codigo_pedido), Convert.ToInt32(area), 1, 1);
            //frmPedidos.Show();

            //cargarDetalleFactura();
            //MessageBox.Show("Pedido No." + Pedido.ToString() + " fue ingresado correctamente.", "His3000");
            /*impresion*/

            //if (DevolucionNumero != 0)
            //{

            //    frmImpresionPedidos frmPedidos = new frmImpresionPedidos(DevolucionNumero, codigoArea1, 1, 0);
            //    frmPedidos.Show();

            //}
            //else
            //{
            //    MessageBox.Show("La devolución no se a guardado. Consulte con el administrador del sistema.", "His3000");
            //}
        }

        private void txtApellidoPaterno_Leave(object sender, EventArgs e)
        {
            txtApellidoPaterno.Text = txtApellidoPaterno.Text.Trim();
        }

        private void txtNombre1_Leave(object sender, EventArgs e)
        {
            txtNombre1.Text = txtNombre1.Text.Trim();
        }

        private void txtApellidoMaterno_Leave(object sender, EventArgs e)
        {
            txtApellidoMaterno.Text = txtApellidoMaterno.Text.Trim();
        }

        private void txtNombre2_Leave(object sender, EventArgs e)
        {
            txtNombre2.Text = txtNombre2.Text.Trim();
        }

        private void btnModificaAtencion_Click(object sender, EventArgs e)
        {
            try
            {

                frmAyudaPacientesFacturacion form = new frmAyudaPacientesFacturacion();
                //form.campoPadre = txt_Historia_Pc;
                //form.campoAtencion = txt_Atencion;

                form.ShowDialog();


                if (form.campoAtencion.Text.Trim() != string.Empty)
                {
                    Xate_codigo = Convert.ToInt64(form.campoAtencion.Text);
                    btnAddProd.Enabled = true;
                    gbId.Enabled = false;
                    lblAtencion.Text = "Atención " + form.campoAtencionNumero.Text.Trim();
                    //txtRuc.Text = form.cam
                    //Generar();
                    PACIENTES xPcte = NegPacientes.recuperarPacientePorAtencion(Convert.ToInt32(form.campoAtencion.Text));
                    txtRuc.Text = xPcte.PAC_IDENTIFICACION;

                    switch (xPcte.PAC_TIPO_IDENTIFICACION)
                    {
                        case "C":
                            rbCedula.Checked = true;
                            break;
                        case "P":
                            rbPasaporte.Checked = true;
                            break;
                        case "S":
                            rbSid.Checked = true;
                            break;
                        default:
                            break;
                    }
                    txt_email.Text = xPcte.PAC_EMAIL;
                    txtNombre1.Text = xPcte.PAC_NOMBRE1;
                    txtNombre2.Text = xPcte.PAC_NOMBRE2;
                    txtApellidoPaterno.Text = xPcte.PAC_APELLIDO_PATERNO;
                    txtApellidoMaterno.Text = xPcte.PAC_APELLIDO_MATERNO;
                    dtp_fecnac.Text = xPcte.PAC_FECHA_NACIMIENTO.ToString();
                    if (xPcte.PAC_GENERO.Trim() == "M")
                        rbn_h.Checked = true;
                    else
                        rbn_h.Checked = false;
                    //Datos Adicionales
                    PACIENTES_DATOS_ADICIONALES DatosAdicionales = new PACIENTES_DATOS_ADICIONALES();
                    DatosAdicionales = NegPacienteDatosAdicionales.RecuperarDatosAdicionalesPaciente(xPcte.PAC_CODIGO);
                    if (DatosAdicionales != null)
                    {
                        txtDireccion.Text = DatosAdicionales.DAP_DIRECCION_DOMICILIO;
                        txtTelefono.Text = DatosAdicionales.DAP_TELEFONO1;
                        txtCelular.Text = DatosAdicionales.DAP_TELEFONO2;
                    }


                    DataTable tipos = NegDietetica.getDataTable("Paciente_Convenio", Xate_codigo.ToString());
                    foreach (DataRow row in tipos.Rows)
                    {
                        cmb_tiporeferido.SelectedValue = row["id_referido"].ToString();
                        cmb_tiporeferido.Text = row["referido"].ToString();

                        cmbTipoAtencion.SelectedValue = row["id_tipo_atencion"].ToString();
                        cmbTipoAtencion.Text = row["tipo_atencion"].ToString();

                        //cmbConvenioPago.SelectedValue = row["tipo_convenio"].ToString();
                        cmbConvenioPago.Text = row["id_tipo_convenio"].ToString();

                        cb_seguros.SelectedValue = row["id_convenio"].ToString();
                        cb_seguros.Text = row["convenio"].ToString();
                    };

                    ////REFRESCAR///GRID///PRODUCTOS////


                    DataTable auxProductos = NegDietetica.getDataTable("GetProductos", Xate_codigo.ToString());
                    if (auxProductos.Rows.Count > 0)
                    {
                        gridDetalleFactura.DataSource = auxProductos;
                    }
                    else
                        gridDetalleFactura.DataSource = null;


                    // btnAddProd.Enabled = true;
                }
                //else
                //{
                //    limpiar();
                //}

                //Console.WriteLine(form.campoPadre.Text + " - " + form.campoAtencion.Text);
                //btn_generar.Enabled = true;
                //  btnGenerar.Enabled = true;
                gbAtencion.Enabled = true;
                gbAtencion.Enabled = true;
                cmbConvenioPago.Enabled = false;
                cmb_tiporeferido.Enabled = false;
                cb_seguros.Enabled = false;

            }
            catch (System.Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rbPasaporte_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPasaporte.Checked)
            {
                txtRuc.ReadOnly = false;
                txtRuc.MaxLength = 13;
                txtRuc.Focus();
            }

        }

        private void rbCedula_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCedula.Checked)
            {
                txtRuc.ReadOnly = false;
                txtRuc.MaxLength = 10;
                txtRuc.Focus();
                if (txtRuc.Text.Length > 10)
                {
                    txtRuc.Text = txtRuc.Text.Substring(0, 9);
                }

            }

        }

        private void btnCANCELAR_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void txtDireccion_Leave(object sender, EventArgs e)
        {
            txtDireccion.Text = txtDireccion.Text.Trim();
        }

        private void txt_email_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_email_Leave(object sender, EventArgs e)
        {

        }

        private void ayudaPacientes_Click(object sender, EventArgs e)
        {
            frm_AyudaPacientes form = new frm_AyudaPacientes();
            form.ShowDialog();
            if (id != "")
            {
                txtRuc.Text = id;
                Generar();
            }
        }

        private void btnImpresion_Click(object sender, EventArgs e)
        {
            if (gridDetalleFactura.Rows.Count > 0)
            {
                UltraGridRow fila = gridDetalleFactura.ActiveRow;

                var codigoArea = 100;
                //DataTable obtieneCodPedido = new DataTable();
                //obtieneCodPedido = NegHabitaciones.ObtieneCodPedido(Convert.ToInt64(Item.PED_CODIGO.ToString()));

                frmImpresionPedidos frmPedidos = new frmImpresionPedidos(Convert.ToInt32(fila.Cells["Codigo_Pedido"].Value.ToString()), codigoArea, 1, 3);
                frmImpresionPedidos.reimpresion = true;
                frmPedidos.Show();
            }
            else
            {
                MessageBox.Show("Debe Elegir un pedido..", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void rbEmp_CheckedChanged(object sender, EventArgs e)
        {
            //if (txtRuc.Text != "")
            //{
            //    //Generar();
            //    //rbCedula.Checked = true;
            //    //rbEmp.Checked = true;
            //}

            if(valida==0)
            {
                valida = 1;
                cmbTipoAtencion.SelectedValue = 1015;
                cmbConvenioPago.SelectedIndex = 1;
                frm_AyudaPacientes form = new frm_AyudaPacientes(true);
                form.ShowDialog();
                if (id != "")
                {
                    txtRuc.Text = id;
                    Generar();
                }                
            }
        }
    }
}