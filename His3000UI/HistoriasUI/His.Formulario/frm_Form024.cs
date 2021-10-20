using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using His.Negocio;
using Recursos;

namespace His.Formulario
{
    public partial class frm_Form024 : Form
    {
        ATENCIONES atencion = null;
        ASEGURADORAS_EMPRESAS aseguradora = null;
        PACIENTES paciente = null;
        MEDICOS medico = null;
        public Int64 CodigoAtencion;
        public string tespecialidad;
        public string ttelefono;
        public string cespecialidad;
        public string ctelefono;
        public string aespecialidad;
        public string atelefono;
        
        public frm_Form024()
        {
            InitializeComponent();
        }
        public frm_Form024(Int64 ate_codigo, string hc)
        {
            InitializeComponent();
            btnNuevo.Image = Archivo.imgBtnAdd2;
            btnGuardar.Image = Archivo.imgBtnGoneSave48;
            btnImprimir.Image = Archivo.imgBtnGonePrint48;
            btnSalir.Image = Archivo.imgBtnGoneExit48;
            CodigoAtencion = ate_codigo;
            if (ate_codigo != 0)
                CargarAtencion(ate_codigo);
        }

        private void CargarAtencion(Int64 codAtencion)
        {
            try
            {
                atencion = NegAtenciones.RecuperarAtencionID(codAtencion);
                aseguradora = NegAseguradoras.recuperaAseguradoraPorAtencion(Convert.ToInt32(codAtencion));
                lbl_aseguradora.Text = aseguradora.ASE_NOMBRE;
                //txtEscrita.Text = His.Entidades.Clases.Sesion.nomUsuario;
                //txtDictada.Text = His.Entidades.Clases.Sesion.nomUsuario;
                cargarPaciente(atencion.PACIENTES.PAC_CODIGO);
                List<MEDICOS> medicos = NegMedicos.listaMedicos();
                int codigoMedico = medicos.FirstOrDefault(m => m.EntityKey == atencion.MEDICOSReference.EntityKey).MED_CODIGO;
                if (codigoMedico != 0)
                    cargarMedico(codigoMedico);
                HABITACIONES hab = NegHabitaciones.listaHabitaciones().FirstOrDefault(h => h.EntityKey == atencion.HABITACIONESReference.EntityKey);
                if (hab != null)
                    txtCama.Text = hab.hab_Numero;
                //cargarProtocolo(codAtencion, CodigoProtocolo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo cargar los datos de la atencion, error: " + ex.Message, "error");
            }
        }
        public PACIENTES_DATOS_ADICIONALES adicionales;
        private void cargarPaciente(int codPac)
        {
            paciente = NegPacientes.RecuperarPacienteID(codPac);

            if (paciente != null)
            {
                txt_pacNombre.Text = paciente.PAC_APELLIDO_PATERNO + " " +
                                     paciente.PAC_APELLIDO_MATERNO + " " +
                                     paciente.PAC_NOMBRE1 + " " +
                                     paciente.PAC_NOMBRE2;
                txt_pacHCL.Text = paciente.PAC_HISTORIA_CLINICA;
                txt_sexo.Text = paciente.PAC_GENERO;
            }
            else
            {
                txt_pacHCL.Text = string.Empty;
                txt_pacNombre.Text = string.Empty;
                txt_sexo.Text = string.Empty;
            }

            DataTable Datos = new DataTable();
            Datos = NegConsentimiento.CargarDatos(CodigoAtencion);
            if(Datos != null)
            {
                txtServicio.Text = Datos.Rows[0][2].ToString();
                txtSala.Text = Datos.Rows[0][3].ToString();
                txtproposito1.Text = Datos.Rows[0][4].ToString();
                txtresultado1.Text = Datos.Rows[0][5].ToString();
                txtprocedimientos.Text = Datos.Rows[0][6].ToString();
                txtriegosc.Text = Datos.Rows[0][7].ToString();
                txtpropositos2.Text = Datos.Rows[0][8].ToString();
                txtresultados2.Text = Datos.Rows[0][9].ToString();
                txtquirurgica.Text = Datos.Rows[0][10].ToString();
                txtriesgoq.Text = Datos.Rows[0][11].ToString();
                txtpropositos3.Text = Datos.Rows[0][12].ToString();
                txtresultados3.Text = Datos.Rows[0][13].ToString();
                txtanestesia.Text = Datos.Rows[0][14].ToString();
                txtriegosa.Text = Datos.Rows[0][15].ToString();
                dtpFecha.Value = Convert.ToDateTime(Datos.Rows[0][16].ToString());
                dtpHora.Value = Convert.ToDateTime(Datos.Rows[0][17].ToString());
                txtProfesionalT.Text = Datos.Rows[0][18].ToString();
                tespecialidad = Datos.Rows[0][19].ToString();
                ttelefono = Datos.Rows[0][20].ToString();
                txtcodigo1.Text = Datos.Rows[0][21].ToString();
                txtcirujano.Text = Datos.Rows[0][22].ToString();
                cespecialidad = Datos.Rows[0][23].ToString();
                ctelefono = Datos.Rows[0][24].ToString();
                txtcodigo2.Text = Datos.Rows[0][25].ToString();
                txtanestesiologo.Text = Datos.Rows[0][26].ToString();
                aespecialidad = Datos.Rows[0][27].ToString();
                atelefono = Datos.Rows[0][28].ToString();
                txtcodigo3.Text = Datos.Rows[0][29].ToString();
                txtrepresentante.Text = Datos.Rows[0][30].ToString();
                txtparentesco.Text = Datos.Rows[0][31].ToString();
                txtidentificacion.Text = Datos.Rows[0][32].ToString();
                txttelefono.Text = Datos.Rows[0][33].ToString();

                HabilitarBotones(false, true, true);
                Desbloquear();
            }
            else
            {
                txtrepresentante.Text = paciente.PAC_REFERENTE_NOMBRE;
                txtparentesco.Text = paciente.PAC_REFERENTE_PARENTESCO;
                txttelefono.Text = paciente.PAC_REFERENTE_TELEFONO;
                HabilitarBotones(true, false, false);
                Bloquear();
            }

            adicionales = new PACIENTES_DATOS_ADICIONALES();
            adicionales = NegPacienteDatosAdicionales.RecuperarDatosAdicionalesPaciente(codPac);
        }
        private void cargarMedico(int cod)
        {
            medico = NegMedicos.RecuperaMedicoId(cod);
            lbl_medico.Text = medico.MED_APELLIDO_PATERNO.Trim() + " " +
                medico.MED_APELLIDO_MATERNO.Trim() + " " +
                medico.MED_NOMBRE1.Trim() + " " + medico.MED_NOMBRE2.Trim();
        }
        private void CargarConsentimiento()
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_Form024_Load(object sender, EventArgs e)
        {
            
        }
        public void Bloquear()
        {
            txtServicio.Enabled = false;
            txtSala.Enabled = false;
            txtrepresentante.Enabled = false;
            txtidentificacion.Enabled = false;
            txtparentesco.Enabled = false;
            txttelefono.Enabled = false;
            tabulador.Enabled = false;
        }
        public void Desbloquear()
        {
            txtServicio.Enabled = true;
            txtSala.Enabled = true;
            txtrepresentante.Enabled = true;
            txtidentificacion.Enabled = true;
            txtparentesco.Enabled = true;
            txttelefono.Enabled = true;
            tabulador.Enabled = true;
        }
        public void HabilitarBotones(bool nuevo, bool guardar, bool imprimir)
        {
            btnNuevo.Enabled = nuevo;
            btnGuardar.Enabled = guardar;
            btnImprimir.Enabled = imprimir;
        }

        public bool Validador()
        {
            errorProvider1.Clear();
            bool valido = true;
            if(txtproposito1.Text.Trim() == "")
            {
                valido = false;
                errorProvider1.SetError(txtproposito1, "Campo Obligatorio");
            }
            if (txtresultado1.Text.Trim() == "")
            {
                valido = false;
                errorProvider1.SetError(txtresultado1, "Campo Obligatorio");
            }
            if (txtprocedimientos.Text.Trim() == "")
            {
                valido = false;
                errorProvider1.SetError(txtprocedimientos, "Campo Obligatorio");
            }
            if (txtriegosc.Text.Trim() == "")
            {
                valido = false;
                errorProvider1.SetError(txtriegosc, "Campo Obligatorio");
            }

            if (txtProfesionalT.Text.Trim() == "" && txtcodigo1.Text.Trim() == "")
            {
                valido = false;
                errorProvider1.SetError(txtProfesionalT, "Campo Obligatorio");
                errorProvider1.SetError(txtcodigo1, "Campo Obligatorio");
            }

            if (txtpropositos2.Text.Trim() == "")
            {
                valido = false;
                errorProvider1.SetError(txtpropositos2, "Campo Obligatorio");
            }
            if (txtresultados2.Text.Trim() == "")
            {
                valido = false;
                errorProvider1.SetError(txtresultados2, "Campo Obligatorio");
            }
            if (txtquirurgica.Text.Trim() == "")
            {
                valido = false;
                errorProvider1.SetError(txtquirurgica, "Campo Obligatorio");
            }
            if (txtriesgoq.Text.Trim() == "")
            {
                valido = false;
                errorProvider1.SetError(txtriesgoq, "Campo Obligatorio");
            }

            if (txtcirujano.Text.Trim() == "" && txtcodigo2.Text.Trim() == "")
            {
                valido = false;
                errorProvider1.SetError(txtcirujano, "Campo Obligatorio");
                errorProvider1.SetError(txtcodigo2, "Campo Obligatorio");
            }
            if (txtpropositos3.Text.Trim() == "")
            {
                valido = false;
                errorProvider1.SetError(txtpropositos3, "Campo Obligatorio");
            }
            if (txtresultados3.Text.Trim() == "")
            {
                valido = false;
                errorProvider1.SetError(txtresultados3, "Campo Obligatorio");
            }
            if (txtanestesia.Text.Trim() == "")
            {
                valido = false;
                errorProvider1.SetError(txtanestesia, "Campo Obligatorio");
            }
            if (txtriegosa.Text.Trim() == "")
            {
                valido = false;
                errorProvider1.SetError(txtriegosa, "Campo Obligatorio");
            }
            if (txtanestesiologo.Text.Trim() == "" && txtcodigo3.Text.Trim() == "")
            {
                valido = false;
                errorProvider1.SetError(txtanestesiologo, "Campo Obligatorio");
                errorProvider1.SetError(txtcodigo3, "Campo Obligatorio");
            }
            if (txtrepresentante.Text.Trim() == "")
            {
                valido = false;
                errorProvider1.SetError(txtrepresentante, "Campo Obligatorio");
            }
            if (txtparentesco.Text.Trim() == "")
            {
                valido = false;
                errorProvider1.SetError(txtparentesco, "Campo Obligatorio");
            }

            if (txttelefono.Text.Trim() == "")
            {
                valido = false;
                errorProvider1.SetError(txttelefono, "Campo Obligatorio");
            }
            if (txtidentificacion.Text.Trim() == "")
            {
                valido = false;
                errorProvider1.SetError(txtidentificacion, "Campo Obligatorio");
            }
            if(txtServicio.Text.Trim() == "")
            {
                valido = false;
                errorProvider1.SetError(txtServicio, "Campo Obligatorio");
            }
            if(txtSala.Text.Trim() == "")
            {
                valido = false;
                errorProvider1.SetError(txtSala, "Campo Obligatorio");
            }
            if(dtpFecha.Value.Date > DateTime.Now.Date)
            {
                valido = false;
                errorProvider1.SetError(dtpFecha, "La fecha no puede ser mayor a la fecha actual.");
            }
            if(dtpFecha.Value.Date == DateTime.Now.Date)
            {
                if(dtpHora.Value.Hour > DateTime.Now.Hour)
                {
                    valido = false;
                    errorProvider1.SetError(dtpHora, "La hora no puede ser mayor a la hora actual");
                }
            }
            return valido;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Desbloquear();
            HabilitarBotones(false, true, false);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validador())
                {
                    NegConsentimiento.GuardarConsentimiento(CodigoAtencion, txtServicio.Text.Trim(),
                        txtSala.Text.Trim(), txtproposito1.Text.Trim(), txtresultado1.Text.Trim(),
                        txtprocedimientos.Text.Trim(), txtriegosc.Text.Trim(), txtpropositos2.Text.Trim(),
                        txtresultados2.Text.Trim(), txtquirurgica.Text.Trim(), txtriesgoq.Text.Trim(),
                        txtpropositos3.Text.Trim(), txtresultados3.Text.Trim(), txtanestesia.Text, txtriegosa.Text,
                        dtpFecha.Value.ToShortDateString(), dtpHora.Value.ToShortTimeString(), txtProfesionalT.Text.Trim(), tespecialidad, ttelefono,
                        txtcodigo1.Text.Trim(), txtcirujano.Text.Trim(), cespecialidad, ctelefono, txtcodigo2.Text.Trim(),
                        txtanestesiologo.Text.Trim(), aespecialidad, atelefono, txtcodigo3.Text.Trim(),
                        txtrepresentante.Text.Trim(), txtparentesco.Text.Trim(), txtidentificacion.Text.Trim(), txttelefono.Text.Trim());
                    MessageBox.Show("Datos almacenados correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    HabilitarBotones(false, true, true);
                    ImprimirFormulario();
                }
            }
            catch (Exception EX)
            {
                //MessageBox.Show(ex.Message);
                MessageBox.Show("Algo ocurrio al guardar los datos. Consulte con el Administrador.\r\nMas detalles: " + EX.Message, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirFormulario();
        }

        public void ImprimirFormulario()
        {

            NegCertificadoMedico medico = new NegCertificadoMedico();
            DS_Form024 form = new DS_Form024();
            DataRow dr;

            dr = form.Tables["Form024"].NewRow();
            dr["Empresa"] = His.Entidades.Clases.Sesion.nomEmpresa;
            dr["Logo"] = medico.path();
            dr["Unidad"] = "";
            dr["Cod_UO"] = "";
            dr["Parroquia"] = adicionales.COD_PARROQUIA;
            dr["Canton"] = adicionales.COD_CANTON;
            dr["Provincia"] = adicionales.COD_PROVINCIA;
            dr["HC"] = txt_pacHCL.Text.Trim();
            dr["ApellidoP"] = paciente.PAC_APELLIDO_PATERNO;

            dr["ApellidoM"] = paciente.PAC_APELLIDO_MATERNO;
            dr["Nombres"] = paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2;
            dr["Servicio"] = txtServicio.Text.Trim();
            dr["Sala"] = txtSala.Text.Trim();
            dr["Cama"] = txtCama.Text.Trim();
            dr["Fecha"] = dtpFecha.Text;
            dr["Hora"] = dtpHora.Text;
            dr["Propositos1"] = txtproposito1.Text.Trim();
            dr["ResultadoE1"] = txtresultado1.Text.Trim();
            dr["Terapia_Proce"] = txtprocedimientos.Text.Trim();
            dr["Riesgos1"] = txtriegosc.Text.Trim();
            dr["ProfesionalT1"] = txtProfesionalT.Text.Trim();
            dr["Especialidad1"] = tespecialidad;
            dr["Telefono1"] = ttelefono;
            dr["Codigo1"] = txtcodigo1.Text.Trim();
            dr["Propositos2"] = txtpropositos2.Text.Trim();
            dr["Resultado2"] = txtresultados2.Text.Trim();
            dr["Intervenciones"] = txtquirurgica.Text.Trim();
            dr["Riesgos2"] = txtriesgoq.Text.Trim();

            dr["ProfesionalT2"] = txtcirujano.Text.Trim();
            dr["Especialidad2"] = cespecialidad;
            dr["Codigo2"] = txtcodigo2.Text.Trim();
            dr["Propositos3"] = txtpropositos3.Text.Trim();
            dr["Resultado3"] = txtresultados3.Text.Trim();
            dr["Anestesia"] = txtanestesia.Text.Trim();
            dr["Riesgo3"] = txtriegosa.Text.Trim();
            dr["ProfesionalT3"] = txtanestesiologo.Text.Trim();
            dr["Telefono3"] = atelefono;
            dr["Telefono2"] = ctelefono;
            dr["Codigo3"] = txtcodigo3.Text.Trim();
            dr["Representante"] = txtrepresentante.Text.Trim();

            dr["Parentesco"] = txtparentesco.Text.Trim();
            dr["TelefonoR"] = txttelefono.Text.Trim();
            dr["IdentificacionR"] = txtidentificacion.Text.Trim();
            dr["Especialidad3"] = aespecialidad;
            form.Tables["Form024"].Rows.Add(dr);

            frmReportes x = new frmReportes(1, "Consentimiento", form);
            x.Show();
        }
        MaskedTextBox codMedico;
        MEDICOS med = null;
        private void txtanestesiologo_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F1)
            {
                //List<MEDICOS> medicos = NegMedicos.listaMedicos();
                //frm_Ayudas frm = new frm_Ayudas(medicos);
                //frm.bandCampo = true;
                //frm.ShowDialog();
                //if (frm.campoPadre2.Text != string.Empty)
                //{
                //    codMedico = (frm.campoPadre2);
                //    med = NegMedicos.RecuperaMedicoId(Convert.ToInt32(codMedico.Text));
                //    agregarAnestesista(med);
                //}
                List<MEDICOS> listaMedicos;

                listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
                frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
                ayuda.ShowDialog();
                if (ayuda.campoPadre.Text != string.Empty)
                {
                    med = NegMedicos.RecuperaMedicoId(Convert.ToInt32(ayuda.campoPadre.Text.ToString()));
                    agregarAnestesista(med);
                }
            }
        }
        private void agregarMedico(MEDICOS medicoTratante)
        {
            if ((medicoTratante != null))
            {
                txtProfesionalT.Text = medicoTratante.MED_APELLIDO_PATERNO.Trim() + " " + medicoTratante.MED_APELLIDO_MATERNO.Trim()
                    + " " + medicoTratante.MED_NOMBRE1.Trim() + " " + medicoTratante.MED_NOMBRE2.Trim();
                if (medicoTratante.MED_CODIGO_MEDICO != null)
                    txtcodigo1.Text = medicoTratante.MED_CODIGO_MEDICO.ToString();
                else
                    txtcodigo1.Text = "0"; //no tiene codigo
                tespecialidad = NegEspecialidades.Especialidad(medicoTratante.MED_CODIGO);
                ttelefono = medicoTratante.MED_TELEFONO_CONSULTORIO;
            }
        }

        private void agregarCirujano(MEDICOS medicoTratante)
        {
            if ((medicoTratante != null))
            {
                txtcirujano.Text = medicoTratante.MED_APELLIDO_PATERNO.Trim() + " " + medicoTratante.MED_APELLIDO_MATERNO.Trim()
                    + " " + medicoTratante.MED_NOMBRE1.Trim() + " " + medicoTratante.MED_NOMBRE2.Trim();
                if (medicoTratante.MED_CODIGO_MEDICO != null)
                    txtcodigo2.Text = medicoTratante.MED_CODIGO_MEDICO.ToString();
                else
                    txtcodigo2.Text = "0"; //no tiene codigo
                cespecialidad = NegEspecialidades.Especialidad(medicoTratante.MED_CODIGO);
                ctelefono = medicoTratante.MED_TELEFONO_CONSULTORIO;
            }
        }

        private void agregarAnestesista(MEDICOS medicoTratante)
        {
            if ((medicoTratante != null))
            {
                txtanestesiologo.Text = medicoTratante.MED_APELLIDO_PATERNO.Trim() + " " + medicoTratante.MED_APELLIDO_MATERNO.Trim()
                    + " " + medicoTratante.MED_NOMBRE1.Trim() + " " + medicoTratante.MED_NOMBRE2.Trim();
                if (medicoTratante.MED_CODIGO_MEDICO != null)
                    txtcodigo3.Text = medicoTratante.MED_CODIGO_MEDICO.ToString();
                else
                    txtcodigo3.Text = "0"; //no tiene codigo
                aespecialidad = NegEspecialidades.Especialidad(medicoTratante.MED_CODIGO);
                atelefono = medicoTratante.MED_TELEFONO_CONSULTORIO;
            }
        }

        private void txtProfesionalT_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F1)
            {
                //List<MEDICOS> medicos = NegMedicos.listaMedicos();
                //frm_Ayudas frm = new frm_Ayudas(medicos);
                //frm.bandCampo = true;
                //frm.ShowDialog();
                //if (frm.campoPadre2.Text != string.Empty)
                //{
                //    codMedico = (frm.campoPadre2);
                //    med = NegMedicos.RecuperaMedicoId(Convert.ToInt32(codMedico.Text));
                //    agregarMedico(med);
                //}

                List<MEDICOS> listaMedicos;

                listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
                frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
                ayuda.ShowDialog();
                if (ayuda.campoPadre.Text != string.Empty)
                {
                    med = NegMedicos.RecuperaMedicoId(Convert.ToInt32(ayuda.campoPadre.Text.ToString()));
                    agregarMedico(med);
                }
            }
        }
        private void txtcirujano_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                //List<MEDICOS> medicos = NegMedicos.listaMedicos();
                //frm_Ayudas frm = new frm_Ayudas(medicos);
                //frm.bandCampo = true;
                //frm.ShowDialog();
                //if (frm.campoPadre2.Text != string.Empty)
                //{
                //    codMedico = (frm.campoPadre2);
                //    med = NegMedicos.RecuperaMedicoId(Convert.ToInt32(codMedico.Text));
                //    agregarCirujano(med);
                //}
                List<MEDICOS> listaMedicos;

                listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
                frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
                ayuda.ShowDialog();
                if (ayuda.campoPadre.Text != string.Empty)
                {
                    med = NegMedicos.RecuperaMedicoId(Convert.ToInt32(ayuda.campoPadre.Text.ToString()));
                    agregarCirujano(med);
                }
            }
        }
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

        private void txttelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNumber(e, false);
        }
    }
}
