using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using His.Entidades;
using His.Entidades.Clases;

namespace His.Formulario
{
    public partial class frm_Certificados : Form
    {
        NegCertificadoMedico Certificado = new NegCertificadoMedico();
        NegQuirofano Quirofano = new NegQuirofano();
        MaskedTextBox codMedico;
        MEDICOS medico = null;
        internal static string hc; //Historial Clinico del paciente
        internal static string ate_codigo; //almacena el codigo del paciente elegido
        internal static string identificacion; //almacena el nro de cedula
        internal static string nom_medico; //Almacena el nombre del medico
        internal static string fechaalta;
        internal static string fechaingreso;
        internal static string med_email;
        internal static string med_identificacion;
        private static string tipo_ingreso; //contiene el tipo de ingreso del paciente
        private static string FechadeIngreso; //Contiene la fecha en palabra ejem: 1 (Uno) de Enero del 2020
        private static string FechadeAlta; //Contiene la fecha en palabras
        private string dias_reposo; //contiene los dias de reposo
        private static bool hosp_sin_alta;
        private static int cont = 0;
        public bool Pacientes = false;
        public frm_Certificados()
        {
            InitializeComponent();
        }

        private void ayudaPacientes_Click(object sender, EventArgs e)
        {
            frm_Ayuda_Certificado.id_usuario = Convert.ToString(His.Entidades.Clases.Sesion.codUsuario);
            frm_Ayuda_Certificado x = new frm_Ayuda_Certificado();
            if (Pacientes)
                x.emergencia = true;
            x.Show();
            x.FormClosed += X_FormClosed;
        }

        private void X_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (hc != null)
            {
                txt_historiaclinica.Text = hc;
                ultraGroupBox1.Enabled = true;
                ultraGroupBox2.Enabled = true;
                Tratamiento.Enabled = true;
                ayudaPacientes.Enabled = false;
            }
            else
            {
                limpiar();
            }
        }

        public void limpiar()
        {
            txt_apellido1.Text = "";
            txt_apellido2.Text = "";
            txt_nombre1.Text = "";
            txt_nombre2.Text = "";
            txt_historiaclinica.Text = "";
            txtfechaalta.Text = "";
            txtfechaingreso.Text = "";
            TablaDiagnostico.Rows.Clear();
            txtdias.Text = "1";
            txtobservacion.Text = "";
            cbTratamiento.SelectedIndex = 1;
            txtprocedimiento.Text = "";
            txtactividad.Text = "";
            txtcontigencia.Text = "";
            txt_historiaclinica.Text = hc;
            ultraGroupBox1.Enabled = false;
            ultraGroupBox2.Enabled = false;
            Tratamiento.Enabled = false;
            ayudaPacientes.Enabled = true;
        }
        private void txt_historiaclinica_TextChanged(object sender, EventArgs e)
        {

        }

        public void CargarCie10Hosp() //Cargara el cie10 de acorde al tipo de atencion que tuvo el paciente
        {
            //cbCie10.DataSource = Certificado.CargarCie10Hosp(ate_codigo);
            //cbCie10.DisplayMember = "CIE_DESCRIPCION";
            //cbCie10.ValueMember = "CIE_CODIGO";
        }
        public void CargarCie10Emerg()
        {
            //cbCie10.DataSource = Certificado.CargarCie10Emerg(ate_codigo);
            //cbCie10.DisplayMember = "CIE_DESCRIPCION";
            //cbCie10.ValueMember = "CIE_CODIGO";
        }
        public void CargarCie10ConsultaE()
        {
            //cbCie10.DataSource = Certificado.CargarCie10Consulta(ate_codigo);
            //cbCie10.DisplayMember = "CIE_DESCRIPCION";
            //cbCie10.ValueMember = "CIE_CODIGO";
        }

        public void CargarTipoContingencia()
        {
            try
            {
                cmbTipos.DataSource = NegCertificadoMedico.TiposContingencia();
                cmbTipos.DisplayMember = "DESCRIPCION";
                cmbTipos.ValueMember = "CODIGO";
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se encontraron Tipos de contingencia.\r\nMás detalle: " + ex.Message, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //throw;
            }
        }
        public int ingreso;
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            bool sinmedico = false;
            if (txt_historiaclinica.Text == "")
            {
                errorProvider1.SetError(txt_historiaclinica, "Por Favor eliga el HC del Paciente");
                return;
            }
            else if (txtdias.Text == "0")
            {
                txtdias.Text = "1";
                return;
            }
            else if (TablaDiagnostico.Rows.Count <= 0)
            {
                MessageBox.Show("El Paciente debe tener minimo un Diagnostico", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (cbTratamiento.SelectedIndex == -1 && rbHospitalizacion.Checked == true)
            {
                errorProvider1.SetError(cbTratamiento, "Campo Obligatorio");
                if (txtprocedimiento.Text == "")
                {
                    errorProvider1.SetError(txtprocedimiento, "Campo Obligatorio");
                    return;
                }
                return;
            }
            else if (txtmedico.Text == "")
            {
                errorProvider1.SetError(txtmedico, "Campo Obligatorio");
                return;
            }
            else if (txtobservacion.Text == "")
            {
                errorProvider1.SetError(txtobservacion, "Campo Obligatorio");
                return;
            }
            else if (txtactividad.Text == "")
            {
                errorProvider1.SetError(txtactividad, "Campo Obligatorio");
                return;
            }
            else
            {
                try
                {

                    if (rbEmergencia.Checked)
                        ingreso = 1;
                    else if (rbHospitalizacion.Checked)
                        ingreso = 2;
                    else if (rbConsultaExterna.Checked)
                        ingreso = 4;




                    //Guardamos el certificado dentro de la tabla para despues recuperar
                    string path = Certificado.path();
                    if (medico == null)
                    {
                        if (txtcontigencia.Visible)
                            Certificado.InsertarCertificado(ate_codigo, Convert.ToString(His.Entidades.Clases.Sesion.codMedico), txtobservacion.Text, txtdias.Text,
                                txtactividad.Text, cmbTipos.GetItemText(cmbTipos.SelectedItem) + " - " + txtcontigencia.Text, cbTratamiento.SelectedItem.ToString(), txtprocedimiento.Text, ingreso, dtpCirugia.Value);
                        else
                        {
                            if (cbTratamiento.SelectedItem != null)
                                Certificado.InsertarCertificado(ate_codigo, Convert.ToString(His.Entidades.Clases.Sesion.codMedico), txtobservacion.Text, txtdias.Text,
                                txtactividad.Text, cmbTipos.GetItemText(cmbTipos.SelectedItem), cbTratamiento.SelectedItem.ToString(), txtprocedimiento.Text, ingreso, dtpCirugia.Value);
                            else
                                Certificado.InsertarCertificado(ate_codigo, Convert.ToString(His.Entidades.Clases.Sesion.codMedico), txtobservacion.Text, txtdias.Text,
                                txtactividad.Text, cmbTipos.GetItemText(cmbTipos.SelectedItem), "", txtprocedimiento.Text, ingreso, dtpCirugia.Value);
                        }
                    }
                    else
                    {
                        if (txtcontigencia.Visible)
                            Certificado.InsertarCertificado(ate_codigo, medico.MED_CODIGO.ToString(), txtobservacion.Text, txtdias.Text,
                                txtactividad.Text, cmbTipos.GetItemText(cmbTipos.SelectedItem) + " - " + txtcontigencia.Text, cbTratamiento.GetItemText(cmbTipos.SelectedItem), txtprocedimiento.Text, ingreso, dtpCirugia.Value);
                        else
                        {
                            if (cbTratamiento.SelectedItem != null)
                                Certificado.InsertarCertificado(ate_codigo, medico.MED_CODIGO.ToString(), txtobservacion.Text, txtdias.Text,
                                txtactividad.Text, cmbTipos.GetItemText(cmbTipos.SelectedItem), cbTratamiento.SelectedItem.ToString(), txtprocedimiento.Text, ingreso, dtpCirugia.Value);
                            else
                                Certificado.InsertarCertificado(ate_codigo, medico.MED_CODIGO.ToString(), txtobservacion.Text, txtdias.Text,
                                txtactividad.Text, cmbTipos.GetItemText(cmbTipos.SelectedItem), "", txtprocedimiento.Text, ingreso, dtpCirugia.Value);
                        }
                    }

                    //Guardanos el detalle del certificado
                    for (int i = 0; i < TablaDiagnostico.Rows.Count; i++)
                    {
                        Certificado.InsertarCertificadoDetalle(TablaDiagnostico.Rows[i].Cells["cod"].Value.ToString());
                    }

                    //Cargamos los datos al Reporte
                    DataTable reporteDatos = new DataTable();
                    DataTable detalleReporteDatos = new DataTable();
                    reporteDatos = Certificado.CargarDatosCertificado(ate_codigo);
                    detalleReporteDatos = Certificado.CargarDatosCertificado_Detalle(Convert.ToInt64(reporteDatos.Rows[0][9].ToString()));

                    Certificado_Medico CM = new Certificado_Medico();

                    PACIENTES_DATOS_ADICIONALES pacien = new PACIENTES_DATOS_ADICIONALES();
                    PACIENTES pacienteActual = new PACIENTES();
                    pacienteActual = NegPacientes.recuperarPacientePorAtencion(Convert.ToInt32(ate_codigo));

                    pacien = NegPacienteDatosAdicionales.RecuperarDatosAdicionalesPaciente(pacienteActual.PAC_CODIGO);

                    EMPRESA empresa = new EMPRESA();
                    empresa = NegEmpresa.RecuperaEmpresa();

                    if (rbHospitalizacion.Checked == true && txtfechaalta.Text != "" && radioButton1.Checked == true && cbTratamiento.SelectedItem.ToString() == "QUIRURGICO")
                    {
                        DataRow drCertificado;
                        foreach (DataRow item in detalleReporteDatos.Rows)
                        {
                            drCertificado = CM.Tables["HOSPITALARIO"].NewRow();
                            drCertificado["Paciente"] = reporteDatos.Rows[0][0].ToString();
                            drCertificado["Identificacion"] = reporteDatos.Rows[0][1].ToString();
                            drCertificado["HC"] = reporteDatos.Rows[0][2].ToString();
                            FechadeIngreso = Fecha_En_Palabra(reporteDatos.Rows[0][3].ToString());
                            drCertificado["FechaIngreso"] = FechadeIngreso;
                            dias_reposo = Dia_En_Palabras(reporteDatos.Rows[0][5].ToString());
                            drCertificado["Dias_Reposo"] = drCertificado["Dias_Reposo"] = reporteDatos.Rows[0][5].ToString() + " (" + dias_reposo + ")";
                            if (reporteDatos.Rows[0][4].ToString() == "")
                            {
                                FechadeAlta = Fecha_En_Palabra(txtfechaalta.Text);
                                drCertificado["FechaAlta"] = FechadeAlta;
                                DateTime Fecha = Convert.ToDateTime(txtfechaalta.Text);
                                string resultado = Fecha_En_Palabra(Convert.ToString(Fecha.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString()) - 1)));
                                drCertificado["Dias_FinReposo"] = resultado;
                            }
                            else
                            {
                                FechadeAlta = Fecha_En_Palabra(reporteDatos.Rows[0][4].ToString());
                                drCertificado["FechaAlta"] = FechadeAlta;
                                DateTime Fecha = Convert.ToDateTime(reporteDatos.Rows[0][4].ToString());
                                string resultado = Fecha_En_Palabra(Convert.ToString(Fecha.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString()) - 1)));
                                drCertificado["Dias_FinReposo"] = resultado;
                            }
                            if (sinmedico == true)
                            {
                                USUARIOS objUsuario = NegUsuarios.RecuperaUsuario(Entidades.Clases.Sesion.codUsuario);

                                drCertificado["Nombre_Medico"] = objUsuario.APELLIDOS + " " + objUsuario.NOMBRES;
                                drCertificado["Email_Medico"] = objUsuario.EMAIL;
                                if (objUsuario.IDENTIFICACION.Length > 10)
                                    drCertificado["Identificacion_Medico"] = objUsuario.IDENTIFICACION.Substring(0, 10);
                                else
                                    drCertificado["Identificacion_Medico"] = objUsuario.IDENTIFICACION;
                                drCertificado["telefonoMedico"] = pacien.DAP_TELEFONO2;
                            }
                            else
                            {
                                drCertificado["Nombre_Medico"] = txtmedico.Text;
                                drCertificado["Email_Medico"] = txtemail.Text;
                                if (txtmedcedula.Text.Length > 10)
                                    drCertificado["Identificacion_Medico"] = txtmedcedula.Text.Substring(0, 10);
                                else
                                    drCertificado["Identificacion_Medico"] = txtmedcedula.Text;
                                drCertificado["telefonoMedico"] = telfmedico;
                            }
                            drCertificado["Empresa"] = reporteDatos.Rows[0][11].ToString();
                            drCertificado["Direccion_Empresa"] = reporteDatos.Rows[0][12].ToString();
                            drCertificado["Telefono_Empresa"] = reporteDatos.Rows[0][13].ToString();
                            drCertificado["emailEmpresa"] = empresa.EMP_EMAIL;
                            drCertificado["Num_Certificado"] = reporteDatos.Rows[0][9].ToString();
                            if (txtcontigencia.Visible)
                                drCertificado["Observacion"] = reporteDatos.Rows[0][10].ToString() + "\r\n\r\nACTIVIDAD LABORAL: " +
                                txtactividad.Text + "\r\nTIPO CONTINGENCIA: " + cmbTipos.GetItemText(cmbTipos.SelectedItem) + " - " + txtcontigencia.Text;
                            else
                                drCertificado["Observacion"] = reporteDatos.Rows[0][10].ToString() + "\r\nACTIVIDAD LABORAL: " +
                               txtactividad.Text + "\r\nTIPO CONTINGENCIA: " + cmbTipos.GetItemText(cmbTipos.SelectedItem);

                            drCertificado["Cie_Codigo"] = item["CIE_CODIGO"];
                            drCertificado["Cie_Descripcion"] = item["CIE_DESCRIPCION"];
                            drCertificado["PathImagen"] = Certificado.path();
                            drCertificado["Tratamiento"] = "TIPO DE TRATAMIENTO: " + cbTratamiento.SelectedItem.ToString();
                            drCertificado["FechaTratamiento"] = "FECHA: " + dtpCirugia.Value.ToShortDateString();
                            drCertificado["Procedimiento"] = "PROCEDIMIENTO: " + txtprocedimiento.Text;
                            CM.Tables["HOSPITALARIO"].Rows.Add(drCertificado);
                        }
                        frmReportes myreport = new frmReportes(1, "CertificadoHA", CM);
                        myreport.Show();
                    }
                    else if (rbHospitalizacion.Checked == true && txtfechaalta.Text != "" && radioButton1.Checked == true && cbTratamiento.SelectedItem.ToString() == "CLINICO")
                    {
                        DataRow drCertificado;
                        foreach (DataRow item in detalleReporteDatos.Rows)
                        {

                            drCertificado = CM.Tables["HOSPITALARIO"].NewRow();
                            drCertificado["Paciente"] = reporteDatos.Rows[0][0].ToString();
                            drCertificado["Identificacion"] = reporteDatos.Rows[0][1].ToString();
                            drCertificado["HC"] = reporteDatos.Rows[0][2].ToString();
                            FechadeIngreso = Fecha_En_Palabra(reporteDatos.Rows[0][3].ToString());
                            drCertificado["FechaIngreso"] = FechadeIngreso;
                            dias_reposo = Dia_En_Palabras(reporteDatos.Rows[0][5].ToString());
                            drCertificado["Dias_Reposo"] = drCertificado["Dias_Reposo"] = reporteDatos.Rows[0][5].ToString() + " (" + dias_reposo + ")";
                            if (reporteDatos.Rows[0][4].ToString() == "")
                            {
                                FechadeAlta = Fecha_En_Palabra(txtfechaalta.Text);
                                drCertificado["FechaAlta"] = FechadeAlta;
                                DateTime Fecha = Convert.ToDateTime(txtfechaalta.Text);
                                string resultado = Fecha_En_Palabra(Convert.ToString(Fecha.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString()) - 1)));
                                drCertificado["Dias_FinReposo"] = resultado;
                            }
                            else
                            {
                                FechadeAlta = Fecha_En_Palabra(reporteDatos.Rows[0][4].ToString());
                                drCertificado["FechaAlta"] = FechadeAlta;
                                DateTime Fecha = Convert.ToDateTime(reporteDatos.Rows[0][4].ToString());
                                string resultado = Fecha_En_Palabra(Convert.ToString(Fecha.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString()) - 1)));
                                drCertificado["Dias_FinReposo"] = resultado;
                            }
                            if (sinmedico == true)
                            {
                                USUARIOS objUsuario = NegUsuarios.RecuperaUsuario(Entidades.Clases.Sesion.codUsuario);
                                drCertificado["Nombre_Medico"] = objUsuario.APELLIDOS + " " + objUsuario.NOMBRES;
                                drCertificado["Email_Medico"] = objUsuario.EMAIL;
                                if (objUsuario.IDENTIFICACION.Length > 10)
                                    drCertificado["Identificacion_Medico"] = objUsuario.IDENTIFICACION.Substring(0, 10);
                                else
                                    drCertificado["Identificacion_Medico"] = objUsuario.IDENTIFICACION;
                                drCertificado["telefonoMedico"] = pacien.DAP_TELEFONO2;
                            }
                            else
                            {
                                drCertificado["Nombre_Medico"] = txtmedico.Text;
                                drCertificado["Email_Medico"] = txtemail.Text;
                                if (txtmedcedula.Text.Length > 10)
                                    drCertificado["Identificacion_Medico"] = txtmedcedula.Text.Substring(0, 10);
                                else
                                    drCertificado["Identificacion_Medico"] = txtmedcedula.Text;
                                drCertificado["telefonoMedico"] = telfmedico;
                            }
                            drCertificado["Empresa"] = reporteDatos.Rows[0][11].ToString();
                            drCertificado["Direccion_Empresa"] = reporteDatos.Rows[0][12].ToString();
                            drCertificado["Telefono_Empresa"] = reporteDatos.Rows[0][13].ToString();
                            drCertificado["emailEmpresa"] = empresa.EMP_EMAIL;
                            drCertificado["Num_Certificado"] = reporteDatos.Rows[0][9].ToString();
                            if (txtcontigencia.Visible)
                                drCertificado["Observacion"] = reporteDatos.Rows[0][10].ToString() + "\r\n\r\nACTIVIDAD LABORAL: " +
                                txtactividad.Text + "\r\nTIPO CONTINGENCIA: " + cmbTipos.GetItemText(cmbTipos.SelectedItem) + " - " + txtcontigencia.Text;
                            else
                                drCertificado["Observacion"] = reporteDatos.Rows[0][10].ToString() + "\r\nACTIVIDAD LABORAL: " +
                               txtactividad.Text + "\r\nTIPO CONTINGENCIA: " + cmbTipos.GetItemText(cmbTipos.SelectedItem);
                            drCertificado["Cie_Codigo"] = item["CIE_CODIGO"];
                            drCertificado["Cie_Descripcion"] = item["CIE_DESCRIPCION"];
                            drCertificado["PathImagen"] = Certificado.path();
                            drCertificado["Tratamiento"] = "TIPO DE TRATAMIENTO: " + cbTratamiento.SelectedItem.ToString();
                            drCertificado["FechaTratamiento"] = "";
                            drCertificado["Procedimiento"] = "";
                            CM.Tables["HOSPITALARIO"].Rows.Add(drCertificado);
                        }
                        frmReportes myreport = new frmReportes(1, "CertificadoHA", CM);
                        myreport.Show();
                    }
                    else if (rbEmergencia.Checked == true && radioButton1.Checked == true)
                    {
                        DataRow drCertificado;
                        foreach (DataRow item in detalleReporteDatos.Rows)
                        {
                            drCertificado = CM.Tables["EMERGENCIA"].NewRow();
                            drCertificado["Paciente"] = reporteDatos.Rows[0][0].ToString();
                            drCertificado["Identificacion"] = reporteDatos.Rows[0][1].ToString();
                            FechadeIngreso = Fecha_En_Palabra(reporteDatos.Rows[0][3].ToString());
                            drCertificado["FechaIngreso"] = FechadeIngreso;
                            drCertificado["Cie_Codigo"] = item["CIE_CODIGO"];
                            drCertificado["Cie_Descripcion"] = item["CIE_DESCRIPCION"];
                            if (txtcontigencia.Visible)
                                drCertificado["Observacion"] = reporteDatos.Rows[0][10].ToString() + "\r\nACTIVIDAD LABORAL: " +
                                txtactividad.Text + "\r\nTIPO CONTINGENCIA: " + cmbTipos.GetItemText(cmbTipos.SelectedItem) + " - " + txtcontigencia.Text;
                            else
                                drCertificado["Observacion"] = reporteDatos.Rows[0][10].ToString() + "\r\nACTIVIDAD LABORAL: " +
                               txtactividad.Text + "\r\nTIPO CONTINGENCIA: " + cmbTipos.GetItemText(cmbTipos.SelectedItem);
                            dias_reposo = Dia_En_Palabras(reporteDatos.Rows[0][5].ToString());
                            drCertificado["Dias_Reposo"] = reporteDatos.Rows[0][5].ToString() + " (" + dias_reposo + ")";
                            DateTime FechaHoy = DateTime.Now;
                            FechadeAlta = Fecha_En_Palabra(FechaHoy.ToShortDateString());
                            FechaHoy = FechaHoy.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString()) - 1);
                            drCertificado["FechaAlta"] = FechadeAlta;
                            drCertificado["Dias_FinReposo"] = FechaHoy.ToString("dd") + " (" + Dia_En_Palabras(FechaHoy.ToString("dd")) + ")" + " de " + FechaHoy.ToString("MMMM") + " del " + FechaHoy.ToString("yyyy");
                            drCertificado["Num_Certificado"] = reporteDatos.Rows[0][9].ToString();
                            drCertificado["Empresa"] = reporteDatos.Rows[0][11].ToString();
                            drCertificado["Direccion_Empresa"] = reporteDatos.Rows[0][12].ToString();
                            drCertificado["Telefono_Empresa"] = reporteDatos.Rows[0][13].ToString();
                            drCertificado["emailEmpresa"] = empresa.EMP_EMAIL;
                            if (sinmedico == true)
                            {
                                USUARIOS objUsuario = NegUsuarios.RecuperaUsuario(Entidades.Clases.Sesion.codUsuario);
                                drCertificado["Nombre_Medico"] = objUsuario.APELLIDOS + " " + objUsuario.NOMBRES;
                                drCertificado["Email_Medico"] = objUsuario.EMAIL;
                                if (objUsuario.IDENTIFICACION.Length > 10)
                                    drCertificado["Identificacion_Medico"] = objUsuario.IDENTIFICACION.Substring(0, 10);
                                else
                                    drCertificado["Identificacion_Medico"] = objUsuario.IDENTIFICACION;
                                drCertificado["telefonoMedico"] = pacien.DAP_TELEFONO2;
                            }
                            else
                            {
                                drCertificado["Nombre_Medico"] = txtmedico.Text;
                                drCertificado["Email_Medico"] = txtemail.Text;
                                if (txtmedcedula.Text.Length > 10)
                                    drCertificado["Identificacion_Medico"] = txtmedcedula.Text.Substring(0, 10);
                                else
                                    drCertificado["Identificacion_Medico"] = txtmedcedula.Text;
                                drCertificado["telefonoMedico"] = telfmedico;
                            }
                            drCertificado["Tipo"] = "emergencia";
                            drCertificado["PathImagen"] = Certificado.path();
                            CM.Tables["EMERGENCIA"].Rows.Add(drCertificado);
                        }
                        frmReportes reporte = new frmReportes(1, "CertificadoEA", CM);
                        reporte.Show();
                    }
                    else if (rbHospitalizacion.Checked == true && txtfechaalta.Text == "" && cbTratamiento.SelectedItem.ToString() == "QUIRURGICO")
                    {
                        DataRow drCertificado;
                        foreach (DataRow item in detalleReporteDatos.Rows)
                        {
                            drCertificado = CM.Tables["HOSPITALARIO"].NewRow();
                            drCertificado["Paciente"] = reporteDatos.Rows[0][0].ToString();
                            drCertificado["Identificacion"] = reporteDatos.Rows[0][1].ToString();
                            drCertificado["HC"] = reporteDatos.Rows[0][2].ToString();
                            FechadeIngreso = Fecha_En_Palabra(reporteDatos.Rows[0][3].ToString());
                            drCertificado["FechaIngreso"] = FechadeIngreso;
                            drCertificado["Cie_Codigo"] = item["CIE_CODIGO"];
                            drCertificado["Cie_Descripcion"] = item["CIE_DESCRIPCION"];
                            if (sinmedico == true)
                            {
                                USUARIOS objUsuario = NegUsuarios.RecuperaUsuario(Entidades.Clases.Sesion.codUsuario);
                                drCertificado["Nombre_Medico"] = objUsuario.APELLIDOS + " " + objUsuario.NOMBRES;
                                drCertificado["Email_Medico"] = objUsuario.EMAIL;
                                if (objUsuario.IDENTIFICACION.Length > 10)
                                    drCertificado["Identificacion_Medico"] = objUsuario.IDENTIFICACION.Substring(0, 10);
                                else
                                    drCertificado["Identificacion_Medico"] = objUsuario.IDENTIFICACION;
                                drCertificado["telefonoMedico"] = pacien.DAP_TELEFONO2;
                            }
                            else
                            {
                                drCertificado["Nombre_Medico"] = txtmedico.Text;
                                drCertificado["Email_Medico"] = txtemail.Text;
                                if (txtmedcedula.Text.Length > 10)
                                    drCertificado["Identificacion_Medico"] = txtmedcedula.Text.Substring(0, 10);
                                else
                                    drCertificado["Identificacion_Medico"] = txtmedcedula.Text;
                                drCertificado["telefonoMedico"] = telfmedico;
                            }
                            drCertificado["Empresa"] = reporteDatos.Rows[0][11].ToString();
                            drCertificado["Direccion_Empresa"] = reporteDatos.Rows[0][12].ToString();
                            drCertificado["Telefono_Empresa"] = reporteDatos.Rows[0][13].ToString();
                            drCertificado["emailEmpresa"] = empresa.EMP_EMAIL;
                            drCertificado["Num_Certificado"] = reporteDatos.Rows[0][9].ToString();
                            if (txtcontigencia.Visible)
                                drCertificado["Observacion"] = reporteDatos.Rows[0][10].ToString() + "\r\n\r\nACTIVIDAD LABORAL: " +
                                txtactividad.Text + "\r\nTIPO CONTINGENCIA: " + cmbTipos.GetItemText(cmbTipos.SelectedItem) + " - " + txtcontigencia.Text;
                            else
                                drCertificado["Observacion"] = reporteDatos.Rows[0][10].ToString() + "\r\n\r\nACTIVIDAD LABORAL: " +
                               txtactividad.Text + "\r\nTIPO CONTINGENCIA: " + cmbTipos.GetItemText(cmbTipos.SelectedItem);
                            drCertificado["PathImagen"] = Certificado.path();
                            drCertificado["Tratamiento"] = "TIPO DE TRATAMIENTO: " + cbTratamiento.SelectedItem.ToString();
                            drCertificado["FechaTratamiento"] = "FECHA: " + dtpCirugia.Value.ToShortDateString();
                            drCertificado["Procedimiento"] = "PROCEDIMIENTO: " + txtprocedimiento.Text;
                            CM.Tables["HOSPITALARIO"].Rows.Add(drCertificado);
                        }
                        frmReportes reporte = new frmReportes(1, "CertificadoHSA", CM);
                        reporte.Show();
                    }
                    else if (rbHospitalizacion.Checked == true && txtfechaalta.Text == "" && cbTratamiento.SelectedItem.ToString() == "CLINICO")
                    {
                        DataRow drCertificado;
                        foreach (DataRow item in detalleReporteDatos.Rows)
                        {
                            drCertificado = CM.Tables["HOSPITALARIO"].NewRow();
                            drCertificado["Paciente"] = reporteDatos.Rows[0][0].ToString();
                            drCertificado["Identificacion"] = reporteDatos.Rows[0][1].ToString();
                            drCertificado["HC"] = reporteDatos.Rows[0][2].ToString();
                            FechadeIngreso = Fecha_En_Palabra(reporteDatos.Rows[0][3].ToString());
                            drCertificado["FechaIngreso"] = FechadeIngreso;
                            drCertificado["Cie_Codigo"] = item["CIE_CODIGO"];
                            drCertificado["Cie_Descripcion"] = item["CIE_DESCRIPCION"];
                            if (sinmedico == true)
                            {
                                USUARIOS objUsuario = NegUsuarios.RecuperaUsuario(Entidades.Clases.Sesion.codUsuario);
                                drCertificado["Nombre_Medico"] = objUsuario.APELLIDOS + " " + objUsuario.NOMBRES;
                                drCertificado["Email_Medico"] = objUsuario.EMAIL;
                                if (objUsuario.IDENTIFICACION.Length > 10)
                                    drCertificado["Identificacion_Medico"] = objUsuario.IDENTIFICACION.Substring(0, 10);
                                else
                                    drCertificado["Identificacion_Medico"] = objUsuario.IDENTIFICACION;
                                drCertificado["telefonoMedico"] = pacien.DAP_TELEFONO2;
                            }
                            else
                            {
                                drCertificado["Nombre_Medico"] = txtmedico.Text;
                                drCertificado["Email_Medico"] = txtemail.Text;
                                if (txtmedcedula.Text.Length > 10)
                                    drCertificado["Identificacion_Medico"] = txtmedcedula.Text.Substring(0, 10);
                                else
                                    drCertificado["Identificacion_Medico"] = txtmedcedula.Text;
                                drCertificado["telefonoMedico"] = telfmedico;
                            }
                            drCertificado["Empresa"] = reporteDatos.Rows[0][11].ToString();
                            drCertificado["Direccion_Empresa"] = reporteDatos.Rows[0][12].ToString();
                            drCertificado["Telefono_Empresa"] = reporteDatos.Rows[0][13].ToString();
                            drCertificado["emailEmpresa"] = empresa.EMP_EMAIL;
                            drCertificado["Num_Certificado"] = reporteDatos.Rows[0][9].ToString();
                            if (txtcontigencia.Visible)
                                drCertificado["Observacion"] = reporteDatos.Rows[0][10].ToString() + "\r\n\r\nACTIVIDAD LABORAL: " +
                                txtactividad.Text + "\r\nTIPO CONTINGENCIA: " + cmbTipos.GetItemText(cmbTipos.SelectedItem) + " - " + txtcontigencia.Text;
                            else
                                drCertificado["Observacion"] = reporteDatos.Rows[0][10].ToString() + "\r\n\r\nACTIVIDAD LABORAL: " +
                               txtactividad.Text + "\r\nTIPO CONTINGENCIA: " + cmbTipos.GetItemText(cmbTipos.SelectedItem);
                            drCertificado["PathImagen"] = Certificado.path();
                            drCertificado["Tratamiento"] = "TIPO DE TRATAMIENTO: " + cbTratamiento.SelectedItem.ToString();
                            drCertificado["FechaTratamiento"] = "";
                            drCertificado["Procedimiento"] = "";
                            CM.Tables["HOSPITALARIO"].Rows.Add(drCertificado);
                        }
                        frmReportes reporte = new frmReportes(1, "CertificadoHSA", CM);
                        reporte.Show();
                    }
                    else if (rbConsultaExterna.Checked == true && radioButton1.Checked == true)
                    {
                        DataRow drCertificado;
                        foreach (DataRow item in detalleReporteDatos.Rows)
                        {
                            drCertificado = CM.Tables["EMERGENCIA"].NewRow();
                            drCertificado["Paciente"] = reporteDatos.Rows[0][0].ToString();
                            drCertificado["Identificacion"] = reporteDatos.Rows[0][1].ToString();
                            FechadeIngreso = Fecha_En_Palabra(reporteDatos.Rows[0][3].ToString());
                            drCertificado["FechaIngreso"] = FechadeIngreso;
                            drCertificado["Cie_Codigo"] = item["CIE_CODIGO"];
                            drCertificado["Cie_Descripcion"] = item["CIE_DESCRIPCION"];
                            DateTime FechaHoy = DateTime.Now;
                            FechadeAlta = FechaHoy.ToString("dd") + " de " + FechaHoy.ToString("MMMM") + " del " + FechaHoy.ToString("yyyy");
                            drCertificado["FechaAlta"] = FechadeAlta;
                            if (txtcontigencia.Visible)
                                drCertificado["Observacion"] = reporteDatos.Rows[0][10].ToString() + "\r\n\r\nACTIVIDAD LABORAL: " +
                                txtactividad.Text + "\r\nTIPO CONTINGENCIA: " + cmbTipos.GetItemText(cmbTipos.SelectedItem) + " - " + txtcontigencia.Text;
                            else
                                drCertificado["Observacion"] = reporteDatos.Rows[0][10].ToString() + "\r\n\r\nACTIVIDAD LABORAL: " +
                               txtactividad.Text + "\r\nTIPO CONTINGENCIA: " + cmbTipos.GetItemText(cmbTipos.SelectedItem);
                            dias_reposo = Dia_En_Palabras(reporteDatos.Rows[0][5].ToString());
                            drCertificado["Dias_Reposo"] = reporteDatos.Rows[0][5].ToString() + " (" + dias_reposo + ")";
                            FechadeAlta = Fecha_En_Palabra(FechaHoy.ToShortDateString());
                            drCertificado["FechaAlta"] = FechadeAlta;
                            FechaHoy = FechaHoy.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString()) - 1);
                            drCertificado["Dias_FinReposo"] = FechaHoy.ToString("dd") + " (" + Dia_En_Palabras(FechaHoy.ToString("dd")) + ")" + " de " + FechaHoy.ToString("MMMM") + " del " + FechaHoy.ToString("yyyy");
                            drCertificado["Num_Certificado"] = reporteDatos.Rows[0][9].ToString();
                            drCertificado["Empresa"] = reporteDatos.Rows[0][11].ToString();
                            drCertificado["Direccion_Empresa"] = reporteDatos.Rows[0][12].ToString();
                            drCertificado["Telefono_Empresa"] = reporteDatos.Rows[0][13].ToString();
                            drCertificado["emailEmpresa"] = empresa.EMP_EMAIL;
                            if (sinmedico == true)
                            {
                                USUARIOS objUsuario = NegUsuarios.RecuperaUsuario(Entidades.Clases.Sesion.codUsuario);
                                drCertificado["Nombre_Medico"] = objUsuario.APELLIDOS + " " + objUsuario.NOMBRES;
                                drCertificado["Email_Medico"] = objUsuario.EMAIL;
                                if (objUsuario.IDENTIFICACION.Length > 10)
                                    drCertificado["Identificacion_Medico"] = objUsuario.IDENTIFICACION.Substring(0, 10);
                                else
                                    drCertificado["Identificacion_Medico"] = objUsuario.IDENTIFICACION;
                                drCertificado["telefonoMedico"] = pacien.DAP_TELEFONO2;
                            }
                            else
                            {
                                drCertificado["Nombre_Medico"] = txtmedico.Text;
                                drCertificado["Email_Medico"] = txtemail.Text;
                                if (txtmedcedula.Text.Length > 10)
                                    drCertificado["Identificacion_Medico"] = txtmedcedula.Text.Substring(0, 10);
                                else
                                    drCertificado["Identificacion_Medico"] = txtmedcedula.Text;
                                drCertificado["telefonoMedico"] = telfmedico;
                            }
                            drCertificado["Tipo"] = "consulta externa";
                            drCertificado["PathImagen"] = Certificado.path();
                            CM.Tables["EMERGENCIA"].Rows.Add(drCertificado);
                        }
                        frmReportes reporte = new frmReportes(1, "CertificadoEA", CM);
                        reporte.Show();
                    }
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void frm_Certificados_Load(object sender, EventArgs e)
        {
            //CargarCie10();

            CargarTipoContingencia();
            //medico = NegMedicos.RecuperaMedicoId(Sesion.codMedico);
            //if (medico != null)
            //    agregarMedico(medico);
            //else
            //{
            //    USUARIOS objUsuario = NegUsuarios.RecuperaUsuario(Entidades.Clases.Sesion.codUsuario);
            //    txtmedico.Text = objUsuario.APELLIDOS + " " + objUsuario.NOMBRES;
            //    txtemail.Text = objUsuario.EMAIL;
            //    txtmedcedula.Text = objUsuario.IDENTIFICACION;
            //}
        }
        //public void CargarCie10()
        //{
        //    cbCie10.DataSource = Quirofano.MostrarProcedimientos();
        //    cbCie10.DisplayMember = "Descripcion";
        //    cbCie10.ValueMember = "Codigo";
        //}

        private void btnañadir_Click(object sender, EventArgs e)
        {
            if (txt_historiaclinica.Text != "")
            {
                errorProvider1.Clear();
                foreach (DataGridViewRow fila1 in TablaDiagnostico.Rows)
                {
                    if (fila1.Cells[1].Value != null)
                    {
                        cont++;
                    }

                }
                if (cont < 3)
                {
                    frm_BusquedaCIE10 busqueda = new frm_BusquedaCIE10();
                    busqueda.ShowDialog();
                    if (busqueda.codigo != null)
                    {
                        cont = 0;
                        TablaDiagnostico.Rows.Add(busqueda.codigo, busqueda.resultado);

                    }
                    TablaDiagnostico.Focus();
                }
                else
                {
                    MessageBox.Show("Solo puede ingresar un maximo de 3 DIAGNOSTICOS DE INGRESO");
                    cont = 0;
                }
            }
            else
            {
                errorProvider1.SetError(txt_historiaclinica, "Por favor elija el HC del Paciente");
            }

        }
        #region Fecha en Palabras
        public string Fecha_En_Palabra(string fecha)
        {
            string fechaprueba;
            DateTime FI = Convert.ToDateTime(fecha);
            if (Convert.ToInt32(FI.ToString("dd")) == 1)
            {
                fechaprueba = "1 (UNO) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            else if (Convert.ToInt32(FI.ToString("dd")) == 2)
            {
                fechaprueba = "2 (DOS) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 3)
            {
                fechaprueba = "3 (TRES) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 4)
            {
                fechaprueba = "4 (CUATRO) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 5)
            {
                fechaprueba = "5 (CINCO) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 6)
            {
                fechaprueba = "6 (SEIS) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 7)
            {
                fechaprueba = "7 (SIETE) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 8)
            {
                fechaprueba = "8 (OCHO) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 9)
            {
                fechaprueba = "9 (NUEVE) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 10)
            {
                fechaprueba = "10 (DIEZ) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 11)
            {
                fechaprueba = "11 (ONCE) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 12)
            {
                fechaprueba = "12 (DOCE) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 13)
            {
                fechaprueba = "13 (TRECE) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 14)
            {
                fechaprueba = "14 (CATORCE) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 15)
            {
                fechaprueba = "15 (QUINCE) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 16)
            {
                fechaprueba = "16 (DIECISEIS) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 17)
            {
                fechaprueba = "17 (DIECISIETE) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 18)
            {
                fechaprueba = "18 (DIECIOCHO) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 19)
            {
                fechaprueba = "19 (DIECINUEVE) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 20)
            {
                fechaprueba = "20 (VEINTE) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 21)
            {
                fechaprueba = "21 (VEINTIUNO) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 22)
            {
                fechaprueba = "22 (VEINTIDOS) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 23)
            {
                fechaprueba = "23 (VEINTITRES) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 24)
            {
                fechaprueba = "24 (VEINTICUATRO) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 25)
            {
                fechaprueba = "25 (VEINTICINCO) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 26)
            {
                fechaprueba = "26 (VEINTISEIS) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 27)
            {
                fechaprueba = "27 (VEINTISIETE) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 28)
            {
                fechaprueba = "28 (VEINTIOCHO) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 29)
            {
                fechaprueba = "29 (VEINTINUEVE) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 30)
            {
                fechaprueba = "30 (TREINTA) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 31)
            {
                fechaprueba = "31 (TREINTA Y UNO) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            else
                return "";
        }
        #endregion
        private static void OnlyNumber(KeyPressEventArgs e, bool isdecimal)
        {
            String aceptados = null;
            if (!isdecimal)
            {
                aceptados = "0123456789" + Convert.ToChar(8);
            }
            if (aceptados.Contains("" + e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtdias_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNumber(e, false);
        }

        #region Dias en Palabras
        public string Dia_En_Palabras(string dia)
        {
            string prueba;
            string num2Text = ""; int value = Convert.ToInt32(dia);
            if (value == 0) num2Text = "CERO";
            else if (value == 1) num2Text = "UN";
            else if (value == 2) num2Text = "DOS";
            else if (value == 3) num2Text = "TRES";
            else if (value == 4) num2Text = "CUATRO";
            else if (value == 5) num2Text = "CINCO";
            else if (value == 6) num2Text = "SEIS";
            else if (value == 7) num2Text = "SIETE";
            else if (value == 8) num2Text = "OCHO";
            else if (value == 9) num2Text = "NUEVE";
            else if (value == 10) num2Text = "DIEZ";
            else if (value == 11) num2Text = "ONCE";
            else if (value == 12) num2Text = "DOCE";
            else if (value == 13) num2Text = "TRECE";
            else if (value == 14) num2Text = "CATORCE";
            else if (value == 15) num2Text = "QUINCE";
            else if (value < 20) num2Text = "DIECI" + Dia_En_Palabras(Convert.ToString(value - 10));
            else if (value == 20) num2Text = "VEINTE";
            else if (value < 30) num2Text = "VEINTI" + Dia_En_Palabras(Convert.ToString(value - 20));
            else if (value == 30) num2Text = "TREINTA";
            else if (value < 40) num2Text = "TREINTA Y " + Dia_En_Palabras(Convert.ToString(value - 30));
            else if (value == 40) num2Text = "CUARENTA";
            else if (value < 50) num2Text = "CUARENTA Y " + Dia_En_Palabras(Convert.ToString(value - 40));
            else if (value == 50) num2Text = "CINCUENTA";
            else if (value == 60) num2Text = "SESENTA";
            else if (value == 70) num2Text = "SETENTA";
            else if (value == 80) num2Text = "OCHENTA";
            else if (value == 90) num2Text = "NOVENTA";
            return num2Text;

        }

        #endregion
        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            ATENCIONES atencionActual = NegAtenciones.RecuperarAtencionID(Convert.ToInt64(ate_codigo));
            string fechaalta = txtfechaalta.Text;
            if (atencionActual.ATE_FECHA_ALTA.ToString() != "null")
                txtfechaalta.Text = atencionActual.ATE_FECHA_ALTA.ToString();
            if (txtfechaalta.Text == "")
            {
                if (radioButton1.Checked)
                {
                    DateTime Hoy = DateTime.Now;
                    txtfechaalta.Text = "";
                    txtfechaalta.Text = Hoy.ToString();
                    hosp_sin_alta = true;
                    txtdias.Enabled = true;
                    atencionActual.ATE_FECHA_ALTA = Hoy;
                }
                else
                {
                    //if (atencionActual.ATE_FECHA_ALTA.ToString() != "NULL")
                    //    txtfechaalta.Text = atencionActual.ATE_FECHA_ALTA.ToString();
                    //else
                    txtdias.Enabled = false;
                    txtdias.Text = "1";
                }
            }
        }
        private void txtdias_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtobservacion.Focus();
            }
        }

        private void txt_historiaclinica_TextChanged_1(object sender, EventArgs e)
        {
            if (txt_historiaclinica.Text != "")
            {
                DataTable vigentes = new DataTable();
                vigentes = NegCertificadoMedico.VerificaEstado(Convert.ToInt64(ate_codigo));
                if (vigentes.Rows.Count > 0)
                {
                    if (vigentes.Rows[0][0].ToString() == "True")
                    {
                        MessageBox.Show("Paciente tiene un certificado habilitado, Revise en el explorador de certificados médicos", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                        return;
                    }
                }
                DataTable Tabla = new DataTable(); //Almacenara los nombres y apellidos del paciente por hc
                Tabla = Certificado.BuscarPaciente(ate_codigo);
                ATENCIONES atencionActual = NegAtenciones.RecuperarAtencionID(Convert.ToInt64(ate_codigo));
                foreach (DataRow item in Tabla.Rows)
                {
                    txt_apellido1.Text = item[0].ToString();
                    txt_apellido2.Text = item[1].ToString();
                    txt_nombre1.Text = item[2].ToString();
                    txt_nombre2.Text = item[3].ToString();
                    tipo_ingreso = item[5].ToString();
                    identificacion = item[6].ToString();
                }
                txtfechaingreso.Text = fechaingreso;
                if (atencionActual.ATE_FECHA_ALTA != null)
                    txtfechaalta.Text = atencionActual.ATE_FECHA_ALTA.ToString();
                if (tipo_ingreso == "EMERGENCIA")
                {
                    if (txtfechaalta.Text != "")
                    {
                        radioButton1.Enabled = true;
                        radioButton1.Checked = true;
                    }
                    else
                    {
                        radioButton1.Enabled = false;
                        radioButton1.Checked = false;
                    }
                    rbEmergencia.Checked = true;
                    rbHospitalizacion.Checked = false;
                    rbConsultaExterna.Checked = false;
                    Tratamiento.Visible = false;
                    CargarCie10Emerg();
                }
                else if (tipo_ingreso == "CONSULTA EXTERNA")
                {
                    rbConsultaExterna.Checked = true;
                    rbEmergencia.Checked = false;
                    rbHospitalizacion.Checked = false;
                    radioButton1.Enabled = false;
                    Tratamiento.Visible = false;
                    CargarCie10ConsultaE();
                }
                else
                {
                    rbHospitalizacion.Checked = true;
                    rbEmergencia.Checked = false;
                    rbConsultaExterna.Checked = false;
                    radioButton1.Enabled = true;
                    Tratamiento.Visible = true;
                    CargarCie10Hosp();
                }
                if (txtfechaalta.Text == "")
                {
                    radioButton1.Enabled = true;
                }
                else
                {
                    radioButton1.Enabled = false;
                    radioButton1.Checked = true;
                }
                //if (cbCie10.DataSource == null || cbCie10.SelectedIndex == -1)
                //{
                //    CargarCie10();
                //}
            }
        }

        private void cbCie10_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public string telfmedico = "";
        private void agregarMedico(MEDICOS medicoTratante)
        {
            if ((medicoTratante != null))
            {
                txtmedico.Text = medicoTratante.MED_APELLIDO_PATERNO.Trim() + " " + medicoTratante.MED_APELLIDO_MATERNO.Trim()
                    + " " + medicoTratante.MED_NOMBRE1.Trim() + " " + medicoTratante.MED_NOMBRE2.Trim();
                txtmedcedula.Text = medicoTratante.MED_RUC;
                txtemail.Text = medicoTratante.MED_EMAIL;
                telfmedico = medicoTratante.MED_TELEFONO_CASA;
            }
        }

        private void btnmedico_Click_1(object sender, EventArgs e)
        {
            List<MEDICOS> medicos = NegMedicos.listaMedicos();
            frm_Ayudas x = new frm_Ayudas(medicos);
            x.ShowDialog();
            if (x.campoPadre.Text != string.Empty)
            {
                codMedico = (x.campoPadre2);
                string cod = x.campoPadre.Text;
                medico = NegMedicos.RecuperaMedicoId(Convert.ToInt32(cod));
                agregarMedico(medico);
            }
        }

        private void cbTratamiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTratamiento.SelectedIndex == 0)
            {
                lblfecha.Visible = false;
                dtpCirugia.Visible = false;
                lblproce.Visible = false;
                txtprocedimiento.Visible = false;
            }
            else if (cbTratamiento.SelectedIndex == 1)
            {
                lblfecha.Visible = true;
                dtpCirugia.Visible = true;
                lblproce.Visible = true;
                txtprocedimiento.Visible = true;

                HC_PROTOCOLO_OPERATORIO protocolo = new HC_PROTOCOLO_OPERATORIO();
                protocolo = NegProtocoloOperatorio.recuperarProtocolo(Convert.ToInt32(ate_codigo));
                if (protocolo != null)
                {
                    txtprocedimiento.Text = protocolo.PROT_POSTOPERATORIO;
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            limpiar();

        }

        private void cmbTipos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipos.SelectedValue.ToString() == "5")//cuando el tipo de contingencia es otros.
            {
                txtcontigencia.Visible = true;
            }
            else
            {
                txtcontigencia.Visible = false;
            }
        }

        private void txtobservacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Tab)
            {
                txtactividad.Focus();
            }
        }

        private void txtactividad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                cmbTipos.Focus();
            }
        }
    }
}
