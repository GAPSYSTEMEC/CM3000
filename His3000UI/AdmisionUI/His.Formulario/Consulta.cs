using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using His.Entidades.Clases;
using His.Entidades;
using His.Formulario;

namespace His.ConsultaExterna
{
    public partial class Consulta : Form
    {
        bool band = true;
        public bool nuevaConsulta = false;
        DtoForm002 datos = new DtoForm002();
        public Int64 id_form002 = 0;
        public Consulta()
        {
            InitializeComponent();
            inicializarGridPrescripciones();
            txt_horaAltaEmerencia.Text = DateTime.Now.ToString("hh:mm");
            txt_profesionalEmergencia.Text = Sesion.nomUsuario;
        }

        #region FUNCIONES Y OBJETOS


        private void inicializarGridPrescripciones()
        {            
            gridPrescripciones.EditMode = DataGridViewEditMode.EditOnKeystroke;
            PRES_FARMACOTERAPIA_INDICACIONES.SortMode = DataGridViewColumnSortMode.NotSortable;
            PRES_FARMACOS_INSUMOS.SortMode = DataGridViewColumnSortMode.NotSortable;
            PRES_FECHA.SortMode = DataGridViewColumnSortMode.NotSortable;
            PRES_ESTADO.SortMode = DataGridViewColumnSortMode.NotSortable;
            PRES_CODIGO.Visible = false;
            ID_USUARIO.Visible = false;
            NOM_USUARIO.Visible = false;
            PRES_ESTADO.Width = 20;
            PRES_FECHA.Width = 130;
            PRES_FARMACOTERAPIA_INDICACIONES.Width = 350;
            PRES_FARMACOS_INSUMOS.Width = 200;
        }


        Boolean permitir = true;
        public bool txtKeyPress(TextBox textbox, int code)
        {

            bool resultado;

            if (code == 46 && textbox.Text.Contains("."))//se evalua si es punto y si es punto se revisa si ya existe en el textbox
            {
                resultado = true;
            }
            else if ((((code >= 48) && (code <= 57)) || (code == 8) || code == 46)) //se evaluan las teclas validas
            {
                resultado = false;
            }
            else if (!permitir)
            {
                resultado = permitir;
            }
            else
            {
                resultado = true;
            }

            return resultado;

        }
        public void ValidaError(Control control, string campo)
        {
            error.SetError(control, campo);
        }

        public bool Valida()
        {
            if (txtMotivo.Text == "")
            {
                ValidaError(txtMotivo, "INGRESE MOTIVO DE CONSULTA");
                return false;
            }
            error.Clear();
            if (txtAntecedentesPersonales.Text == "")
            {
                ValidaError(txtAntecedentesPersonales, "INGRESE ANTECEDENTES PERSONALES");
                return false;
            }
            error.Clear();
            if(!chbCardiopatia.Checked && !chbDiabetes.Checked && !chbVascular.Checked && !chbHiperT.Checked && !chbCancer.Checked && !chbTuberculosis.Checked && !chbMental.Checked && !chbInfeccionsa.Checked && !chbMalFormado.Checked && !chbOtro.Checked )
            {
                ValidaError(label3, "DEBE SELECCIONAR POR LO MENOS UNO");
                return false;
            }
            if (chbOtro.Checked)
            {
                if (txtAntecedentesFamiliares.Text == "")
                {
                    ValidaError(txtAntecedentesFamiliares, "INGRESE ANTECEDENTES FAMILIARES");
                    return false;
                }
            }
            error.Clear();
            if (txtEnfermedadProblema.Text == "")
            {
                ValidaError(txtEnfermedadProblema, "INGRESE ENFERMEDAD O PROBLEMA ACTUAL");
                return false;
            }
            error.Clear();
            if (txtRevisionActual.Text == "")
            {
                ValidaError(txtRevisionActual, "INGRESE REVISIÓN ACTUAL DE ÓRGANOS Y SISTEMAS");
                return false;
            }
            error.Clear();
            if (txtTemperatura.Text == "")
            {
                ValidaError(txtTemperatura, "INGRESE TEMPERATURA");
                return false;
            }
            error.Clear();
            if (txtPresionArteria1.Text == "")
            {
                ValidaError(txtPresionArteria1, "INGRESE PRESIÓN ARTERIAL");
                return false;
            }
            error.Clear();
            if (txtPresionArteria2.Text == "")
            {
                ValidaError(txtPresionArteria2, "INGRESE PRESIÓN ARTERIAL");
                return false;
            }
            error.Clear();
            if (txtPeso.Text == "")
            {
                ValidaError(txtPeso, "INGRESE PESO");
                return false;
            }
            error.Clear();
            if (txtFrecuenciaRespiratoria.Text == "")
            {
                ValidaError(txtFrecuenciaRespiratoria, "INGRESE FRECUENCIA RESPIRATORIA");
                return false;
            }
            error.Clear();
            if (txtPulso.Text == "")
            {
                ValidaError(txtPulso, "INGRESE PULSO");
                return false;
            }
            error.Clear();
            if (txtTalla.Text == "")
            {
                ValidaError(txtTalla, "INGRESE TALLA");
                return false;
            }
            error.Clear();
            if (txtExamenFisico.Text == "")
            {
                ValidaError(txtExamenFisico, "INGRESE EXAMEN FÍSICO REGIONAL");
                return false;
            }
            error.Clear();
            if (txtPlanesTratamiento.Text == "")
            {
                ValidaError(txtPlanesTratamiento, "INGRESE PLANES DE TRATAMIENTO");
                return false;
            }
            error.Clear();
            if (txtEvolucion.Text == "")
            {
                ValidaError(txtEvolucion, "INGRESE EVOLUCIÓN");
                return false;
            }
            error.Clear();
            if (txtDiagnostico1.Text != "" && txtCieDiagnostico1.Text=="")
            {
                ValidaError(txtCieDiagnostico1, "INGRESE DIAGNOSTICO CIE10");
                return false;
            }
            error.Clear();
            if (txtDiagnostico2.Text != "" && txtCieDiagnostico2.Text == "")
            {
                ValidaError(txtCieDiagnostico2, "INGRESE DIAGNOSTICO CIE10");
                return false;
            }
            error.Clear();
            if (txtDiagnostico3.Text != "" && txtCieDiagnostico3.Text == "")
            {
                ValidaError(txtCieDiagnostico3, "INGRESE DIAGNOSTICO CIE10");
                return false;
            }
            error.Clear();
            if (txtDiagnostico4.Text != "" && txtCieDiagnostico4.Text == "")
            {
                ValidaError(txtCieDiagnostico4, "INGRESE DIAGNOSTICO CIE10");
                return false;
            }
            error.Clear();
            return true;
        }

        public void limpiaCampos()
        {

            //lblNombre.Text = "";
            //lblApellido.Text = "";
            //lblSexo.Text = "";

            //lblEdad.Text = "";
            //lblHistoria.Text = "";
            //lblAteCodigo.Text = "";


            txtMotivo.Text = "";
            txtAntecedentesPersonales.Text = "";

            txtAntecedentesFamiliares.Text = "";
            txtEnfermedadProblema.Text = "";
            
            txtRevisionActual.Text = "";
            dtpMedicion.Text = "";
            txtTemperatura.Text = "";
            txtPresionArteria1.Text = "";
            txtPresionArteria2.Text = "";
            txtPulso.Text = "";
            txtFrecuenciaRespiratoria.Text = "";
            txtPeso.Text = "";
            txtTalla.Text = "";
            
            txtExamenFisico.Text = "";
            txtDiagnostico1.Text = "";
            txtCieDiagnostico1.Text = "";

            txtCieDiagnostico2.Text = "";
            txtCieDiagnostico2.Text = "";
            chbDef1.Checked = false;
            chbPre1.Checked = false;
            chbDef2.Checked = false;
            chbPre2.Checked = false;
            chbPre3.Checked = false;
            chbDef3.Checked = false;
            chbDef4.Checked = false;
            chbPre4.Checked = false;
            txtDiagnostico3.Text = "";
            txtCieDiagnostico3.Text = "";

            txtDiagnostico4.Text = "";
            txtCieDiagnostico4.Text = "";

            txtPlanesTratamiento.Text = "";
            //txt_profesionalEmergencia.Text = "";
            txt_CodMSPE.Text = "";
            txtEvolucion.Text = "";
            txtindicaciones.Text = "";

        }


        public bool GuardaFormulario()
        {

            datos.historiaClinica = lblHistoria.Text;
            datos.ateCodigo = lblAteCodigo.Text;
            datos.nombrePaciente = lblNombre.Text;
            datos.apellidoPaciemte = lblApellido.Text;
            datos.edadPaciente = lblEdad.Text;
            datos.sexoPaciente = lblSexo.Text;
            datos.motivoConsulta = txtMotivo.Text;
            datos.antecedentesPersonales = txtAntecedentesPersonales.Text;
            if (chbCardiopatia.Checked)
                datos.cardiopatia = "X";
            else
                datos.cardiopatia = "O";
            if (chbDiabetes.Checked)
                datos.diabetes = "X";
            else
                datos.diabetes = "O";
            if (chbVascular.Checked)
                datos.vascular = "X";
            else
                datos.vascular = "O";
            if (chbHiperT.Checked)
                datos.hipertension = "X";
            else
                datos.hipertension = "O";
            if (chbCancer.Checked)
                datos.cancer = "X";
            else
                datos.cancer = "O";
            if (chbTuberculosis.Checked)
                datos.tuberculosis = "X";
            else
                datos.tuberculosis = "O";
            if (chbMental.Checked)
                datos.mental = "X";
            else
                datos.mental = "O";
            if (chbInfeccionsa.Checked)
                datos.infeccionsa = "X";
            else
                datos.infeccionsa = "O";
            if (chbMalFormado.Checked)
                datos.malFormado = "X";
            else
                datos.malFormado = "O";
            if (chbOtro.Checked)
                datos.otro = "X";
            else
                datos.otro = "O";
            datos.antecedentesFamiliares = txtAntecedentesFamiliares.Text;
            datos.enfermedadProblemaActual = txtEnfermedadProblema.Text;
            if (chb5cp1.Checked)
            {
                datos.sentidos = "X";
                datos.sentidossp = "O";
            }
            else
            {
                datos.sentidos = "O";
                datos.sentidossp = "X";
            }
            if (chb5cp2.Checked)
            {
                datos.respiratorio = "X";
                datos.respiratoriosp = "O";
            }
            else
            {
                datos.respiratorio = "O";
                datos.respiratoriosp = "X";
            }
            if (chb5cp3.Checked)
            {
                datos.cardioVascular = "X";
                datos.cardioVascularsp = "O";
            }
            else
            {
                datos.cardioVascular = "O";
                datos.cardioVascularsp = "X";
            }
            if (chb5cp4.Checked)
            {
                datos.digestivo = "X";
                datos.digestivosp = "O";
            }
            else
            {
                datos.digestivo = "O";
                datos.digestivosp = "X";
            }
            if (chb5cp5.Checked)
            {
                datos.genital = "X";
                datos.genitalsp = "O";
            }
            else
            {
                datos.genital = "O";
                datos.genitalsp = "X";
            }
            if (chb5cp6.Checked)
            {
                datos.urinario = "X";
                datos.urinariosp = "O";
            }
            else
            {
                datos.urinario = "O";
                datos.urinariosp = "X";
            }
            if (chb5cp7.Checked)
            {
                datos.esqueletico = "X";
                datos.esqueleticosp = "O";
            }
            else
            {
                datos.esqueletico = "O";
                datos.esqueleticosp = "X";
            }
            if (chb5cp8.Checked)
            {
                datos.endocrino = "X";
                datos.endocrinosp = "O";
            }
            else
            {
                datos.endocrino = "O";
                datos.endocrinosp = "X";
            }
            if (chb5cp9.Checked)
            {
                datos.linfatico = "X";
                datos.linfaticosp = "O";
            }
            else
            {
                datos.linfatico = "O";
                datos.linfaticosp = "X";
            }
            if (chb5cp10.Checked)
            {
                datos.nervioso = "X";
                datos.nerviososp = "O";
            }
            else
            {
                datos.nervioso = "O";
                datos.nerviososp = "X";
            }
            datos.detalleRevisionOrganos = txtRevisionActual.Text;
            datos.fechaMedicion = Convert.ToString(dtpMedicion.Value);
            datos.temperatura = txtTemperatura.Text;
            datos.presionArterial1 = txtPresionArteria1.Text;
            datos.presionArterial2 = txtPresionArteria2.Text;
            datos.pulso = txtPulso.Text;
            datos.frecuenciaRespiratoria = txtFrecuenciaRespiratoria.Text;
            datos.peso = txtPeso.Text;
            datos.talla = txtTalla.Text;
            if (chb7cp1.Checked)
            {
                datos.cabeza = "X";
                datos.cabezasp = "O";
            }
            else
            {
                datos.cabezasp = "X";
                datos.cabeza = "O";
            }
            if (chb7cp2.Checked)
            {
                datos.cuello = "X";
                datos.cuellosp = "O";
            }
            else
            {
                datos.cuellosp = "X";
                datos.cuello = "O";
            }
            if (chb7cp3.Checked)
            {
                datos.torax = "X";
                datos.toraxsp = "O";
            }
            else
            {
                datos.toraxsp = "X";
                datos.torax = "O";
            }
            if (chb7cp4.Checked)
            {
                datos.abdomen = "X";
                datos.abdomensp = "O";
            }
            else
            {
                datos.abdomensp = "X";
                datos.abdomen = "O";
            }
            if (chb7cp5.Checked)
            {
                datos.pelvis = "X";
                datos.pelvissp = "O";
            }
            else
            {
                datos.pelvissp = "X";
                datos.pelvis = "O";
            }
            if (chb7cp6.Checked)
            {
                datos.extremidades = "X";
                datos.extremidadessp = "O";
            }
            else
            {
                datos.extremidadessp = "X";
                datos.extremidades = "O";
            }
            datos.examenFisico = txtExamenFisico.Text;
            datos.diagnosticco1 = txtDiagnostico1.Text;
            datos.diagnosticco1cie = txtCieDiagnostico1.Text;
            if (txtCieDiagnostico1.Text == "")
            {
                datos.diagnosticco1prepre = "O";
                datos.diagnosticco1predef = "O";
            }
            else
            {
                if (chbPre1.Checked)
                {
                    datos.diagnosticco1prepre = "X";
                    datos.diagnosticco1predef = "O";
                }
                else
                {
                    datos.diagnosticco1prepre = "O";
                    datos.diagnosticco1predef = "X";
                }
            }
            datos.diagnosticco2 = txtDiagnostico2.Text;
            datos.diagnosticco2cie = txtCieDiagnostico2.Text;
            if (txtCieDiagnostico2.Text == "")
            {
                datos.diagnosticco2prepre = "O";
                datos.diagnosticco2predef = "O";
            }
            else
            {
                if (chbPre2.Checked)
                {
                    datos.diagnosticco2prepre = "X";
                    datos.diagnosticco2predef = "O";
                }
                else
                {
                    datos.diagnosticco2prepre = "O";
                    datos.diagnosticco2predef = "X";
                }
            }
            datos.diagnosticco3 = txtDiagnostico3.Text;
            datos.diagnosticco3cie = txtCieDiagnostico3.Text;

            if (txtCieDiagnostico3.Text == "")
            {
                datos.diagnosticco3prepre = "O";
                datos.diagnosticco3predef = "O";
            }
            else
            {
                if (chbPre3.Checked)
                {
                    datos.diagnosticco3prepre = "X";
                    datos.diagnosticco3predef = "O";
                }
                else
                {
                    datos.diagnosticco3prepre = "O";
                    datos.diagnosticco3predef = "X";
                }
            }
            datos.diagnosticco4 = txtDiagnostico4.Text;
            datos.diagnosticco4cie = txtCieDiagnostico4.Text;
            if (txtCieDiagnostico4.Text == "")
            {
                datos.diagnosticco4prepre = "O";
                datos.diagnosticco4predef = "O";
            }
            else
            {
                if (chbPre4.Checked)
                {
                    datos.diagnosticco4prepre = "X";
                    datos.diagnosticco4predef = "O";
                }
                else
                {
                    datos.diagnosticco4prepre = "O";
                    datos.diagnosticco4predef = "X";
                }
            }
            datos.planesTratamiento = txtPlanesTratamiento.Text;
            datos.evolucion = txtEvolucion.Text;
            datos.prescripciones = txtindicaciones.Text.Trim();
            datos.drTratatnte = Sesion.nomUsuario;

            NegConsultaExterna.GuardaDatos002(datos);
            id_form002 = NegConsultaExterna.RecuperarId();
            //if(id_form002 != 0)
            //{
            //    //GUARDAR EL GRID
            //    if (gridPrescripciones.Rows.Count > 0)
            //    {
            //        foreach (DataGridViewRow item in gridPrescripciones.Rows)
            //        {
            //            if(item.Cells[0].Value.ToString() != "")
            //            {
            //                if (item.Cells[5].Value.ToString() != "" && item.Cells[4].Value.ToString() != "")
            //                {
            //                    NegConsultaExterna.GuardarPrescripcion(id_form002, Sesion.codUsuario, Sesion.nomUsuario, item.Cells[3].Value.ToString(), item.Cells[4].Value.ToString(), Convert.ToDateTime(item.Cells[5].Value.ToString()), true);
            //                }
            //                else
            //                    NegConsultaExterna.GuardarPrescripcion(id_form002, Sesion.codUsuario, Sesion.nomUsuario, item.Cells[3].Value.ToString(), "", DateTime.Now, true);

            //            }
            //            else
            //            {
            //                break;
            //            }


            //        }
            //    }
            //}
            return true;
        }



        #endregion

        private void txtMotivo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)09)
            {
                txtAntecedentesPersonales.Focus();
            }
        }

        private void txtAntecedentesPersonales_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)09)
            {
                txtAntecedentesFamiliares.Focus();
            }
        }

        private void txtAntecedentesFamiliares_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)09)
            {
                pantab1.SelectedTab = pantab1.Tabs["CuatroCinco"];
                SendKeys.SendWait("{TAB}");
                txtEnfermedadProblema.Focus();
            }
        }

        private void txtEnfermedadProblema_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)09)
            {
                txtRevisionActual.Focus();
            }
        }

        private void txtRevisionActual_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)09)
            {
                pantab1.SelectedTab = pantab1.Tabs["SeisSiete"];
                SendKeys.SendWait("{TAB}");
                txtTemperatura.Focus();
            }
        }

        private void txtTemperatura_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textbox = (TextBox)sender; // Convierto el sender a TextBox
            e.Handled = txtKeyPress(textbox, Convert.ToInt32(e.KeyChar));
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                txtPresionArteria1.Focus();
            }
        }

        private void txtPresionArteria1_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            e.Handled = txtKeyPress(textbox, Convert.ToInt32(e.KeyChar));
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                txtPresionArteria2.Focus();
            }
        }

        private void txtPresionArteria2_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            e.Handled = txtKeyPress(textbox, Convert.ToInt32(e.KeyChar));
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                txtPulso.Focus();
            }
        }

        private void txtPulso_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            e.Handled = txtKeyPress(textbox, Convert.ToInt32(e.KeyChar));
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                txtFrecuenciaRespiratoria.Focus();
            }
        }

        private void txtFrecuenciaRespiratoria_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            e.Handled = txtKeyPress(textbox, Convert.ToInt32(e.KeyChar));
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                txtPeso.Focus();
            }
        }

        private void txtPeso_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            e.Handled = txtKeyPress(textbox, Convert.ToInt32(e.KeyChar));
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                txtTalla.Focus();
            }
        }

        private void txtTalla_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            e.Handled = txtKeyPress(textbox, Convert.ToInt32(e.KeyChar));
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                txtExamenFisico.Focus();
            }
        }

        private void txtExamenFisico_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)09)
            {
                pantab1.SelectedTab = pantab1.Tabs["OchoNueve"];
                SendKeys.SendWait("{TAB}");
                txtDiagnostico1.Focus();
            }
        }

        private void txtDiagnostico1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                if (txtDiagnostico1.Text != "")
                {
                    //txtCieDiagnostico1.Enabled = true;
                    //txtCieDiagnostico1.Focus();
                    txtDiagnostico2.Focus();
                }
            }
        }

        private void txtCieDiagnostico1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                chbPre1.Focus();
            }
        }

        private void chbPre1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                chbDef1.Focus();
            }
        }

        private void chbDef1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                txtDiagnostico2.Focus();
            }
        }

        private void txtDiagnostico2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                if (txtDiagnostico2.Text != "")
                {
                    //txtCieDiagnostico2.Enabled = true;
                    //txtCieDiagnostico2.Focus();
                    txtDiagnostico3.Focus();
                }
            }
        }

        private void txtCieDiagnostico2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                chbPre2.Focus();
            }
        }

        private void chbPre2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                chbDef2.Focus();
            }
        }

        private void chbDef2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                txtDiagnostico3.Focus();
            }
        }

        private void txtDiagnostico3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                if (txtDiagnostico3.Text != "")
                {
                    //txtCieDiagnostico3.Enabled = true;
                    //txtCieDiagnostico3.Focus();
                    txtDiagnostico4.Focus();
                }
            }
        }

        private void txtCieDiagnostico3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                chbPre3.Focus();
            }
        }

        private void chbPre3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                chbDef3.Focus();
            }
        }

        private void chbDef3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                txtDiagnostico4.Focus();
            }
        }

        private void txtDiagnostico4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                if (txtDiagnostico4.Text != "")
                {
                    //txtCieDiagnostico4.Enabled = true;
                    //txtCieDiagnostico4.Focus();
                    txtPlanesTratamiento.Focus();
                }
            }
        }

        private void txtCieDiagnostico4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                chbPre4.Focus();
            }
        }

        private void chbPre4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                chbDef4.Focus();
            }
        }

        private void chbDef4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                txtPlanesTratamiento.Focus();
            }
        }

        private void txtPlanesTratamiento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)09)
            {
                pantab1.SelectedTab = pantab1.Tabs["Evolucion"];
                SendKeys.SendWait("{TAB}");
                txtEvolucion.Focus();
            }
        }

        private void chb5cp1_CheckedChanged(object sender, EventArgs e)
        {
            if (chb5cp1.Checked)
                chb5sp1.Checked = false;
            else
                chb5sp1.Checked = true;
        }

        private void chb5cp2_CheckedChanged(object sender, EventArgs e)
        {
            if (chb5cp2.Checked)
                chb5sp2.Checked = false;
            else
                chb5sp2.Checked = true;
        }

        private void chb5cp3_CheckedChanged(object sender, EventArgs e)
        {
            if (chb5cp3.Checked)
                chb5sp3.Checked = false;
            else
                chb5sp3.Checked = true;
        }

        private void chb5cp4_CheckedChanged(object sender, EventArgs e)
        {
            if (chb5cp4.Checked)
                chb5sp4.Checked = false;
            else
                chb5sp4.Checked = true;
        }

        private void chb5cp5_CheckedChanged(object sender, EventArgs e)
        {
            if (chb5cp5.Checked)
                chb5sp5.Checked = false;
            else
                chb5sp5.Checked = true;
        }

        private void chb5cp6_CheckedChanged(object sender, EventArgs e)
        {
            if (chb5cp6.Checked)
                chb5sp6.Checked = false;
            else
                chb5sp6.Checked = true;
        }

        private void chb5cp7_CheckedChanged(object sender, EventArgs e)
        {
            if (chb5cp7.Checked)
                chb5sp7.Checked = false;
            else
                chb5sp7.Checked = true;
        }

        private void chb5cp8_CheckedChanged(object sender, EventArgs e)
        {
            if (chb5cp8.Checked)
                chb5sp8.Checked = false;
            else
                chb5sp8.Checked = true;
        }

        private void chb5cp9_CheckedChanged(object sender, EventArgs e)
        {
            if (chb5cp9.Checked)
                chb5sp9.Checked = false;
            else
                chb5sp9.Checked = true;
        }

        private void chb5cp10_CheckedChanged(object sender, EventArgs e)
        {
            if (chb5cp10.Checked)
                chb5sp10.Checked = false;
            else
                chb5sp10.Checked = true;
        }

        private void chb5sp1_CheckedChanged(object sender, EventArgs e)
        {
            if (chb5sp1.Checked)
                chb5cp1.Checked = false;
            else
                chb5cp1.Checked = true;
        }

        private void chb5sp2_CheckedChanged(object sender, EventArgs e)
        {
            if (chb5sp2.Checked)
                chb5cp2.Checked = false;
            else
                chb5cp2.Checked = true;
        }

        private void chb5sp3_CheckedChanged(object sender, EventArgs e)
        {
            if (chb5sp3.Checked)
                chb5cp3.Checked = false;
            else
                chb5cp3.Checked = true;
        }

        private void chb5sp4_CheckedChanged(object sender, EventArgs e)
        {
            if (chb5sp4.Checked)
                chb5cp4.Checked = false;
            else
                chb5cp4.Checked = true;
        }

        private void chb5sp5_CheckedChanged(object sender, EventArgs e)
        {
            if (chb5sp5.Checked)
                chb5cp5.Checked = false;
            else
                chb5cp5.Checked = true;
        }

        private void chb5sp6_CheckedChanged(object sender, EventArgs e)
        {
            if (chb5sp6.Checked)
                chb5cp6.Checked = false;
            else
                chb5cp6.Checked = true;
        }

        private void chb5sp7_CheckedChanged(object sender, EventArgs e)
        {
            if (chb5sp7.Checked)
                chb5cp7.Checked = false;
            else
                chb5cp7.Checked = true;
        }

        private void chb5sp8_CheckedChanged(object sender, EventArgs e)
        {
            if (chb5sp8.Checked)
                chb5cp8.Checked = false;
            else
                chb5cp8.Checked = true;
        }

        private void chb5sp9_CheckedChanged(object sender, EventArgs e)
        {
            if (chb5sp9.Checked)
                chb5cp9.Checked = false;
            else
                chb5cp9.Checked = true;
        }

        private void chb5sp10_CheckedChanged(object sender, EventArgs e)
        {
            if (chb5sp10.Checked)
                chb5cp10.Checked = false;
            else
                chb5cp10.Checked = true;
        }

        private void chb7cp1_CheckedChanged(object sender, EventArgs e)
        {
            if (chb7cp1.Checked)
                chb7sp1.Checked = false;
            else
                chb7sp1.Checked = true;
        }

        private void chb7cp2_CheckedChanged(object sender, EventArgs e)
        {
            if (chb7cp2.Checked)
                chb7sp2.Checked = false;
            else
                chb7sp2.Checked = true;
        }

        private void chb7cp3_CheckedChanged(object sender, EventArgs e)
        {
            if (chb7cp3.Checked)
                chb7sp3.Checked = false;
            else
                chb7sp3.Checked = true;
        }

        private void chb7cp4_CheckedChanged(object sender, EventArgs e)
        {
            if (chb7cp4.Checked)
                chb7sp4.Checked = false;
            else
                chb7sp4.Checked = true;
        }

        private void chb7cp5_CheckedChanged(object sender, EventArgs e)
        {
            if (chb7cp5.Checked)
                chb7sp5.Checked = false;
            else
                chb7sp5.Checked = true;
        }

        private void chb7cp6_CheckedChanged(object sender, EventArgs e)
        {
            if (chb7cp6.Checked)
                chb7sp6.Checked = false;
            else
                chb7sp6.Checked = true;
        }

        private void chb7sp1_CheckedChanged(object sender, EventArgs e)
        {
            if (chb7sp1.Checked)
                chb7cp1.Checked = false;
            else
                chb7cp1.Checked = true;
        }

        private void chb7sp2_CheckedChanged(object sender, EventArgs e)
        {
            if (chb7sp2.Checked)
                chb7cp2.Checked = false;
            else
                chb7cp2.Checked = true;
        }

        private void chb7sp3_CheckedChanged(object sender, EventArgs e)
        {
            if (chb7sp3.Checked)
                chb7cp3.Checked = false;
            else
                chb7cp3.Checked = true;
        }

        private void chb7sp4_CheckedChanged(object sender, EventArgs e)
        {
            if (chb7sp4.Checked)
                chb7cp4.Checked = false;
            else
                chb7cp4.Checked = true;
        }

        private void chb7sp5_CheckedChanged(object sender, EventArgs e)
        {
            if (chb7sp5.Checked)
                chb7cp5.Checked = false;
            else
                chb7cp5.Checked = true;
        }

        private void chb7sp6_CheckedChanged(object sender, EventArgs e)
        {
            if (chb7sp6.Checked)
                chb7cp6.Checked = false;
            else
                chb7cp6.Checked = true;
        }

        private void chbPre1_CheckedChanged(object sender, EventArgs e)
        {
            if (chbPre1.Checked)
                chbDef1.Checked = false;
            else
                chbDef1.Checked = true;
        }

        private void chbPre2_CheckedChanged(object sender, EventArgs e)
        {
            if (chbPre2.Checked)
                chbDef2.Checked = false;
            else
                chbDef2.Checked = true;
        }

        private void chbPre3_CheckedChanged(object sender, EventArgs e)
        {
            if (chbPre3.Checked)
                chbDef3.Checked = false;
            else
                chbDef3.Checked = true;
        }

        private void chbPre4_CheckedChanged(object sender, EventArgs e)
        {
            if (chbPre4.Checked)
                chbDef4.Checked = false;
            else
                chbDef4.Checked = true;
        }

        private void chbDef1_CheckedChanged(object sender, EventArgs e)
        {
            if (chbDef1.Checked)
                chbPre1.Checked = false;
            else
                chbPre1.Checked = true;
        }

        private void chbDef2_CheckedChanged(object sender, EventArgs e)
        {
            if (chbDef2.Checked)
                chbPre2.Checked = false;
            else
                chbPre2.Checked = true;
        }

        private void chbDef3_CheckedChanged(object sender, EventArgs e)
        {
            if (chbDef3.Checked)
                chbPre3.Checked = false;
            else
                chbPre3.Checked = true;
        }

        private void chbDef4_CheckedChanged(object sender, EventArgs e)
        {
            if (chbDef4.Checked)
                chbPre4.Checked = false;
            else
                chbPre4.Checked = true;
        }

        private void btnGuarda_Click(object sender, EventArgs e)
        {
            if (Valida())
            {
                if (GuardaFormulario())
                {
                    MessageBox.Show("Información Guardad Con Exito", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    P_Central.Enabled = false;
                    btnImprimir.Visible = true;
                    btnreceta.Visible = true;
                    btnCertificado.Visible = true;
                }
                else
                    MessageBox.Show("Información No Se Guardo Comuniquese Con Sistemas", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            btnGuarda.Enabled = false;
            if (datos.sexoPaciente == "Masculino")
            {
                datos.sexoPaciente = "M";
            }
            else
                datos.sexoPaciente = "F";

            NegCertificadoMedico neg = new NegCertificadoMedico();
            HCU_form002MSP Ds = new HCU_form002MSP();
            Ds.Tables[0].Rows.Add
                (new object[]
                {
                    datos.nombrePaciente.ToString(),
                    datos.apellidoPaciemte.ToString(),
                    datos.sexoPaciente.ToString(),
                    datos.edadPaciente.ToString(),
                    datos.historiaClinica.ToString(),
                    datos.ateCodigo.ToString(),
                    datos.motivoConsulta.ToString(),
                    datos.antecedentesPersonales.ToString(),
                    datos.cardiopatia.ToString(),
                    datos.diabetes.ToString(),
                    datos.vascular.ToString(),
                    datos.hipertension.ToString(),
                    datos.cancer.ToString(),
                    datos.tuberculosis.ToString(),
                    datos.mental.ToString(),
                    datos.infeccionsa.ToString(),
                    datos.malFormado.ToString(),
                    datos.otro.ToString(),
                    datos.antecedentesFamiliares.ToString(),
                    datos.enfermedadProblemaActual.ToString(),
                    datos.sentidos.ToString(),
                    datos.sentidossp.ToString(),
                    datos.respiratorio.ToString(),
                    datos.respiratoriosp.ToString(),
                    datos.cardioVascular.ToString(),
                    datos.cardioVascularsp.ToString(),
                    datos.digestivo.ToString(),
                    datos.digestivosp.ToString(),
                    datos.genital.ToString(),
                    datos.genitalsp.ToString(),
                    datos.urinario.ToString(),
                    datos.urinariosp.ToString(),
                    datos.esqueletico.ToString(),
                    datos.esqueleticosp.ToString(),
                    datos.endocrino.ToString(),
                    datos.endocrinosp.ToString(),
                    datos.linfatico.ToString(),
                    datos.linfaticosp.ToString(),
                    datos.nervioso.ToString(),
                    datos.nerviososp.ToString(),
                    datos.detalleRevisionOrganos.ToString(),
                    datos.fechaMedicion.ToString(),
                    datos.temperatura.ToString(),
                    datos.presionArterial1.ToString(),
                    datos.presionArterial2.ToString(),
                    datos.pulso.ToString(),
                    datos.frecuenciaRespiratoria.ToString(),
                    datos.peso.ToString(),
                    datos.talla.ToString(),
                    datos.cabeza.ToString(),
                    datos.cabezasp.ToString(),
                    datos.cuello.ToString(),
                    datos.cuellosp.ToString(),
                    datos.torax.ToString(),
                    datos.toraxsp.ToString(),
                    datos.abdomen.ToString(),
                    datos.abdomensp.ToString(),
                    datos.pelvis.ToString(),
                    datos.pelvissp.ToString(),
                    datos.extremidades.ToString(),
                    datos.extremidadessp.ToString(),
                    datos.examenFisico.ToString(),
                    datos.diagnosticco1.ToString(),
                    datos.diagnosticco1cie.ToString(),
                    datos.diagnosticco1prepre.ToString(),
                    datos.diagnosticco1predef.ToString(),
                    datos.diagnosticco2.ToString(),
                    datos.diagnosticco2cie.ToString(),
                    datos.diagnosticco2prepre.ToString(),
                    datos.diagnosticco2predef.ToString(),
                    datos.diagnosticco3.ToString(),
                    datos.diagnosticco3cie.ToString(),
                    datos.diagnosticco3predef.ToString(),
                    datos.diagnosticco3prepre.ToString(),
                    datos.diagnosticco4.ToString(),
                    datos.diagnosticco4cie.ToString(),
                    datos.diagnosticco4predef.ToString(),
                    datos.diagnosticco4prepre.ToString(),
                    datos.planesTratamiento.ToString(),
                    datos.evolucion.ToString(),
                    datos.prescripciones.ToString(),
                    Convert.ToString(dtp_fechaAltaEmerencia.Value),
                    Convert.ToString(DateTime.Now.ToString("hh:mm")),
                    datos.drTratatnte.ToString(),
                    Sesion.codMedico.ToString(),
                    neg.path(),
                    Sesion.nomEmpresa
                });
            frmReportes x = new frmReportes(1, "ConsultaExterna", Ds);
            x.Show();
            //HCU_Form002MSPrpt report = new HCU_Form002MSPrpt();
            //report.SetDataSource(Ds);
            //CrystalDecisions.Windows.Forms.CrystalReportViewer vista = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            //vista.ReportSource = report;
            //vista.PrintReport();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Usted Va Salir Del Formulario", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                this.Close();
        }


        public TextBox txthistoria = new TextBox();
        public TextBox txtatecodigo = new TextBox();
        public TextBox txtaseguradora = new TextBox();
        private void btnBuscarPaciente_Click(object sender, EventArgs e)
        {
            DataTable paciente = new DataTable();
            frmAyudaPacientesFacturacion ayuda = new frmAyudaPacientesFacturacion();
            ayuda.campoPadre = txthistoria;
            ayuda.campoAtencion = txtatecodigo;
            ayuda.campoAseguradora = txtaseguradora;
            ayuda.ShowDialog();
            if (txthistoria.Text != "")
            {
                paciente = NegConsultaExterna.RecuperaPaciente(Convert.ToInt64(txtatecodigo.Text));
                lblHistoria.Text = txthistoria.Text;
                lblAteCodigo.Text = txtatecodigo.Text;
                //lblAseguradora.Text = txtaseguradora.Text;
                lblNombre.Text = paciente.Rows[0][0].ToString();
                lblApellido.Text = paciente.Rows[0][1].ToString();
                lblEdad.Text = paciente.Rows[0][2].ToString();
                if (paciente.Rows[0][3].ToString().Trim() == "M")
                {
                    lblSexo.Text = "Masculino";
                    
                }
                else
                {
                    lblSexo.Text = "Femenino";
                    
                }
                //CargarPaciente();
                P_Central.Visible = true;
                P_Datos.Visible = true;
                btnBuscarPaciente.Visible = false;
                btnGuarda.Visible = true;
                btnNuevo.Visible = true;           
            }
        }
        public void CargarPaciente() //Cambios Edgar 20210203 antes no rescataba la informacion.
        {
            try
            {
                btnImprimir.Visible = true;
                btnGuarda.Visible = false;
                DataTable TPaciente = new DataTable(); //Trae el paciente con todas las consultas externas
                DataTable DatosPaciente = new DataTable(); //Contiene los datos del paciente
                TPaciente = NegConsultaExterna.ExistePaciente(Convert.ToInt64(txtatecodigo.Text));

                if(TPaciente.Rows.Count > 0)
                {
                    frm_PacientesConsultaExterna x = new frm_PacientesConsultaExterna();
                    x.Pacientes = TPaciente;
                    x.ShowDialog();
                    if (x.codigoConsultaExterna != "")
                    {
                        DatosPaciente = NegConsultaExterna.DatosPaciente(Convert.ToInt32(x.codigoConsultaExterna));
                        if(DatosPaciente.Rows.Count > 0)
                        {
                            txtMotivo.Text = DatosPaciente.Rows[0][7].ToString();
                            txtAntecedentesPersonales.Text = DatosPaciente.Rows[0][8].ToString();
                            //cardiopatia
                            if (DatosPaciente.Rows[0][9].ToString() == "X")
                            {
                                chbCardiopatia.Checked = true;
                            }
                            else
                                chbCardiopatia.Checked = false;
                            //diabetes
                            if (DatosPaciente.Rows[0][10].ToString() == "X")
                                chbDiabetes.Checked = true;
                            else
                                chbDiabetes.Checked = false;
                            //Vascular
                            if (DatosPaciente.Rows[0][11].ToString() == "X")
                                chbVascular.Checked = true;
                            else
                                chbVascular.Checked = false;
                            //HiperTension
                            if (DatosPaciente.Rows[0][12].ToString() == "X")
                                chbHiperT.Checked = true;
                            else
                                chbHiperT.Checked = false;
                            //Cancer
                            if (DatosPaciente.Rows[0][13].ToString() == "X")
                                chbCancer.Checked = true;
                            else
                                chbCancer.Checked = false;
                            //tuberculosis
                            if (DatosPaciente.Rows[0][14].ToString() == "X")
                                chbTuberculosis.Checked = true;
                            else
                                chbTuberculosis.Checked = false;
                            //mental
                            if (DatosPaciente.Rows[0][15].ToString() == "X")
                                chbMental.Checked = true;
                            else
                                chbMental.Checked = false;
                            //infecciosa
                            if (DatosPaciente.Rows[0][16].ToString() == "X")
                                chbInfeccionsa.Checked = true;
                            else
                                chbInfeccionsa.Checked = false;
                            //malformacion
                            if (DatosPaciente.Rows[0][17].ToString() == "X")
                                chbMalFormado.Checked = true;
                            else
                                chbMalFormado.Checked = false;
                            //otro
                            if (DatosPaciente.Rows[0][18].ToString() == "X")
                                chbOtro.Checked = true;
                            else
                                chbOtro.Checked = false;
                            //antecedentes familiares
                            txtAntecedentesFamiliares.Text = DatosPaciente.Rows[0][19].ToString();
                            //enfermedad actual
                            txtEnfermedadProblema.Text = DatosPaciente.Rows[0][20].ToString();
                            //sentidos
                            if (DatosPaciente.Rows[0][21].ToString() == "X")
                                chb5cp1.Checked = true;
                            else
                                chb5cp1.Checked = false;
                            //respiratorio
                            if (DatosPaciente.Rows[0][23].ToString() == "X")
                                chb5cp2.Checked = true;
                            else
                                chb5cp2.Checked = false;
                            //CardioVascular
                            if (DatosPaciente.Rows[0][25].ToString() == "X")
                                chb5cp3.Checked = true;
                            else
                                chb5cp3.Checked = false;
                            //digestivo
                            if (DatosPaciente.Rows[0][27].ToString() == "X")
                                chb5cp4.Checked = true;
                            else
                                chb5cp4.Checked = false;
                            //genital
                            if (DatosPaciente.Rows[0][29].ToString() == "X")
                                chb5cp5.Checked = true;
                            else
                                chb5cp5.Checked = false;
                            //urinario
                            if (DatosPaciente.Rows[0][31].ToString() == "X")
                                chb5cp6.Checked = true;
                            else
                                chb5cp6.Checked = false;
                            //esqueletico
                            if (DatosPaciente.Rows[0][33].ToString() == "X")
                                chb5cp7.Checked = true;
                            else
                                chb5cp7.Checked = false;
                            //endocrino
                            if (DatosPaciente.Rows[0][35].ToString() == "X")
                                chb5cp8.Checked = true;
                            else
                                chb5cp8.Checked = false;
                            //linfatico
                            if (DatosPaciente.Rows[0][37].ToString() == "X")
                                chb5cp9.Checked = true;
                            else
                                chb5cp9.Checked = false;
                            //nervioso
                            if (DatosPaciente.Rows[0][39].ToString() == "X")
                                chb5cp10.Checked = true;
                            else
                                chb5cp10.Checked = false;
                            //revision actual
                            txtRevisionActual.Text = DatosPaciente.Rows[0][41].ToString();
                            dtpMedicion.Value = Convert.ToDateTime(DatosPaciente.Rows[0][42].ToString());
                            txtTemperatura.Text = DatosPaciente.Rows[0][43].ToString();
                            txtPresionArteria1.Text = DatosPaciente.Rows[0][44].ToString();
                            txtPresionArteria2.Text = DatosPaciente.Rows[0][45].ToString();
                            txtPulso.Text = DatosPaciente.Rows[0][46].ToString();
                            txtFrecuenciaRespiratoria.Text = DatosPaciente.Rows[0][47].ToString();
                            txtPeso.Text = DatosPaciente.Rows[0][48].ToString();
                            txtTalla.Text = DatosPaciente.Rows[0][49].ToString();
                            //cabeza
                            if (DatosPaciente.Rows[0][50].ToString() == "X")
                                chb7cp1.Checked = true;
                            else
                                chb7cp1.Checked = false;
                            //cuello
                            if (DatosPaciente.Rows[0][52].ToString() == "X")
                                chb7cp2.Checked = true;
                            else
                                chb7cp2.Checked = false;
                            //torax
                            if (DatosPaciente.Rows[0][54].ToString() == "X")
                                chb7cp3.Checked = true;
                            else
                                chb7cp3.Checked = false;
                            //abdomen
                            if (DatosPaciente.Rows[0][56].ToString() == "X")
                                chb7cp4.Checked = true;
                            else
                                chb7cp4.Checked = false;
                            //pelvis
                            if (DatosPaciente.Rows[0][58].ToString() == "X")
                                chb7cp5.Checked = true;
                            else
                                chb7cp5.Checked = false;
                            //extremidades
                            if (DatosPaciente.Rows[0][60].ToString() == "X")
                                chb7cp6.Checked = true;
                            else
                                chb7cp5.Checked = false;
                            //examen fisico
                            txtExamenFisico.Text = DatosPaciente.Rows[0][62].ToString();
                            //Cie10
                            txtDiagnostico1.Text = DatosPaciente.Rows[0][63].ToString();
                            txtCieDiagnostico1.Text = DatosPaciente.Rows[0][64].ToString();
                            if (DatosPaciente.Rows[0][65].ToString() == "X")
                                chbPre1.Checked = true;
                            else
                                chbPre1.Checked = false;

                            txtDiagnostico2.Text = DatosPaciente.Rows[0][67].ToString();
                            txtCieDiagnostico2.Text = DatosPaciente.Rows[0][68].ToString();
                            if (DatosPaciente.Rows[0][69].ToString() == "X")
                                chbPre2.Checked = true;
                            else
                                chbPre2.Checked = false;


                            txtDiagnostico3.Text = DatosPaciente.Rows[0][71].ToString();
                            txtCieDiagnostico3.Text = DatosPaciente.Rows[0][72].ToString();
                            if (DatosPaciente.Rows[0][73].ToString() == "X")
                                chbPre3.Checked = true;
                            else
                                chbPre3.Checked = false;


                            txtDiagnostico4.Text = DatosPaciente.Rows[0][75].ToString();
                            txtCieDiagnostico4.Text = DatosPaciente.Rows[0][76].ToString();
                            if (DatosPaciente.Rows[0][77].ToString() == "X")
                                chbPre4.Checked = true;
                            else
                                chbPre4.Checked = false;
                            //tratamiento
                            txtPlanesTratamiento.Text = DatosPaciente.Rows[0][79].ToString();
                            txtEvolucion.Text = DatosPaciente.Rows[0][80].ToString();

                            txtindicaciones.Text = DatosPaciente.Rows[0][81].ToString();
                            dtp_fechaAltaEmerencia.Value = Convert.ToDateTime(DatosPaciente.Rows[0][82].ToString());
                            txt_horaAltaEmerencia.Text = DatosPaciente.Rows[0][83].ToString();
                            txt_profesionalEmergencia.Text = DatosPaciente.Rows[0][84].ToString();
                            txt_CodMSPE.Text = DatosPaciente.Rows[0][85].ToString();
                        }
                    }
                    nuevaConsulta = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void txtCieDiagnostico1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {                
                
                frm_BusquedaCIE10 busqueda = new frm_BusquedaCIE10();
                busqueda.ShowDialog();
                if (busqueda.codigo != null)
                {

                    txtCieDiagnostico1.Text = busqueda.codigo;
                    chbPre1.Checked = true;
                }
                
            }
        }
        private void txtCieDiagnostico2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {

                frm_BusquedaCIE10 busqueda = new frm_BusquedaCIE10();
                busqueda.ShowDialog();
                if (busqueda.codigo != null)
                {

                    txtCieDiagnostico2.Text = busqueda.codigo;
                    chbPre2.Checked = true;
                }

            }
        }

        private void txtCieDiagnostico3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {

                frm_BusquedaCIE10 busqueda = new frm_BusquedaCIE10();
                busqueda.ShowDialog();
                if (busqueda.codigo != null)
                {

                    txtCieDiagnostico3.Text = busqueda.codigo;
                    chbPre3.Checked = true;
                }

            }
        }

        
        private void txtCieDiagnostico4_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {

                frm_BusquedaCIE10 busqueda = new frm_BusquedaCIE10();
                busqueda.ShowDialog();
                if (busqueda.codigo != null)
                {

                    txtCieDiagnostico4.Text = busqueda.codigo;
                    chbPre4.Checked = true;
                }

            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Usted va generar una nueva consulta externa. ¿Desea Continuar?","HIS3000",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning)==DialogResult.OK)
            {
                nuevaConsulta = true;
                limpiaCampos();
                //P_Central.Visible = false;
                P_Datos.Visible = false;
                btnNuevo.Visible = false;
                btnBuscarPaciente.Visible = false;                
            }
        }

        private void gridPrescripciones_CellValueChanged(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            try
            {
                if (band == false)
                {
                    if (e.ColumnIndex == gridPrescripciones.Columns["PRES_ESTADO"].Index)
                    {
                        DataGridViewCheckBoxCell chkCell = (DataGridViewCheckBoxCell)gridPrescripciones.Rows[e.RowIndex].Cells["PRES_ESTADO"];
                        if (chkCell.Value != null)
                        {
                            if ((bool)chkCell.Value == true)
                            {
                                gridPrescripciones.Rows[e.RowIndex].Cells["PRES_FARMACOS_INSUMOS"].ReadOnly = false;
                                gridPrescripciones.Rows[e.RowIndex].Cells["PRES_FECHA"].ReadOnly = false;
                                gridPrescripciones.Rows[e.RowIndex].Cells["PRES_FECHA"].Value = DateTime.Now;
                            }
                            else
                            {
                                gridPrescripciones.Rows[e.RowIndex].Cells["PRES_FARMACOS_INSUMOS"].ReadOnly = false;
                                gridPrescripciones.Rows[e.RowIndex].Cells["PRES_FARMACOS_INSUMOS"].Value = string.Empty;
                                gridPrescripciones.Rows[e.RowIndex].Cells["PRES_FECHA"].ReadOnly = true;
                                gridPrescripciones.Rows[e.RowIndex].Cells["PRES_FECHA"].Value = null;
                            }
                        }
                        else
                        {
                            gridPrescripciones.Rows[e.RowIndex].Cells["PRES_FARMACOS_INSUMOS"].ReadOnly = false;
                            gridPrescripciones.Rows[e.RowIndex].Cells["PRES_FARMACOS_INSUMOS"].Value = string.Empty;
                            gridPrescripciones.Rows[e.RowIndex].Cells["PRES_FECHA"].ReadOnly = true;
                            gridPrescripciones.Rows[e.RowIndex].Cells["PRES_FECHA"].Value = null;
                        }

                    }
                    else
                    {
                        gridPrescripciones.Rows[e.RowIndex].Cells["PRES_FARMACOS_INSUMOS"].ReadOnly = false;
                        gridPrescripciones.Rows[e.RowIndex].Cells["PRES_FECHA"].ReadOnly = false;
                    }
                }
            }
            catch
            {

            }
        }

        private void btnreceta_Click(object sender, EventArgs e)
        {
            frm_RecetaMedica x = new frm_RecetaMedica();
            x.ShowDialog();
        }

        private void btnCertificado_Click(object sender, EventArgs e)
        {
            frm_Certificados x = new frm_Certificados();
            x.ShowDialog();
        }

        private void txtDiagnostico1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                frm_BusquedaCIE10 busqueda = new frm_BusquedaCIE10();
                busqueda.ShowDialog();
                if (busqueda.codigo != null)
                {

                    txtCieDiagnostico1.Text = busqueda.codigo;
                    txtDiagnostico1.Text = busqueda.resultado;
                    chbPre1.Checked = true;
                }
            }
        }

        private void txtDiagnostico2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                frm_BusquedaCIE10 busqueda = new frm_BusquedaCIE10();
                busqueda.ShowDialog();
                if (busqueda.codigo != null)
                {

                    txtCieDiagnostico2.Text = busqueda.codigo;
                    txtDiagnostico2.Text = busqueda.resultado;
                    chbPre2.Checked = true;
                }
            }
        }

        private void txtDiagnostico3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                frm_BusquedaCIE10 busqueda = new frm_BusquedaCIE10();
                busqueda.ShowDialog();
                if (busqueda.codigo != null)
                {

                    txtCieDiagnostico3.Text = busqueda.codigo;
                    txtDiagnostico3.Text = busqueda.resultado;
                    chbPre3.Checked = true;
                }
            }
        }

        private void txtDiagnostico4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                frm_BusquedaCIE10 busqueda = new frm_BusquedaCIE10();
                busqueda.ShowDialog();
                if (busqueda.codigo != null)
                {

                    txtCieDiagnostico4.Text = busqueda.codigo;
                    txtDiagnostico4.Text = busqueda.resultado;
                    chbPre4.Checked = true;
                }
            }
        }
    }
}
