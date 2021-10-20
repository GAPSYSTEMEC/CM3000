using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using His.Entidades.Reportes;
using His.Negocio;
using Recursos;
using His.Parametros;
using His.DatosReportes;
using Infragistics;
using System.Data.SqlClient;


namespace His.Formulario
{
    public partial class frm_Protocolo : Form
    {
        #region Variables

        ATENCIONES atencion = null;
        ASEGURADORAS_EMPRESAS aseguradora = null;
        PACIENTES paciente = null;
        MEDICOS medico = null;
        Boolean nuevo = false;
        HC_PROTOCOLO_OPERATORIO nuevoProtocolo = new HC_PROTOCOLO_OPERATORIO();               
        private int contlrP1 = 0;
        private int contlrP2;
        private string proced1 = "";
        private string proced2 = "";

        private int CodigoAtencion; // Almacena el Codigo de la Atencion / Giovanny Tapia
        private int CodigoProtocolo; // Almacena el Codigo del protocolo / Giovanny Tapia

        internal static string medicopatologo; //recibe el nombre del medico p.
        internal static string cie10; //recibe el procedimiento
        internal static string tarifario; //recibe el tarifario elegido
        internal static string personal; //recibe el nombre del personal

        //ATENCIONES atencion = new ATENCIONES();
        #endregion
        

        public frm_Protocolo()
        {
            InitializeComponent();
            CargarAnestesia();
        }

        public void CargarAnestesia()
        {
            NegQuirofano quir = new NegQuirofano();
            txtTipoAnestesia.DataSource = quir.MostrarAnestesia();
            txtTipoAnestesia.ValueMember = "CODIGO";
            txtTipoAnestesia.DisplayMember = "DESCRIPCION";
        }
        private void refrescarSolicitudes()
        {
            gridSol.DataSource = NegImagen.getProtocolos(CodigoAtencion);
            gridSol.Columns["ID"].Visible = false;
            gridSol.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            if (gridSol.RowCount > 1)
                habilitarBotones(true, false, true, true, true);
            else
                habilitarBotones(true, false, false, false, true);
        }

        public frm_Protocolo(Int32 codigoAtencion, Int32 CodigoFormulario)
        {
            InitializeComponent();
            CargarAnestesia();
            btnNuevo.Image = Archivo.imgBtnAdd2;
            btnGuardar.Image = Archivo.imgBtnGoneSave48;
            btnImprimir.Image = Archivo.imgBtnGonePrint48;
            //btnExportarExcel.Image = Archivo.imgOfficeExcel;
            btnSalir.Image = Archivo.imgBtnGoneExit48;

            CodigoAtencion = codigoAtencion;
            CodigoProtocolo = CodigoFormulario;

            if (codigoAtencion != 0)
                cargarAtencion(codigoAtencion, CodigoFormulario);


            NegAtenciones atenciones = new NegAtenciones();
            string estado = atenciones.EstadoCuenta(Convert.ToString(codigoAtencion));
            refrescarSolicitudes();
            if (estado != "1")
            {
                Bloquear();
            }
            ValidarEnfermeria();
            BloquearTabulador(false);
        }

        public void ValidarEnfermeria()
        {
            //Cambios Edgar 20210303 Requerimientos de la pasteur por Alex
            if (His.Entidades.Clases.Sesion.codDepartamento == 6)
            {
                btnNuevo.Enabled = false;
                btnGuardar.Enabled = false;
                btnImprimir.Enabled = false;
            }
        }
        #region Métodos
        public void Bloquear()
        {
            btnNuevo.Enabled = false;
            //btnImprimir.Enabled = false;
            btnGuardar.Enabled = false;
            //ugbPaciente.Enabled = false;
            //ultraTabPageControl1.Enabled = false;
            //ultraTabPageControl2.Enabled = false;
            //ultraTabPageControl3.Enabled = false;
            //ultraTabPageControl4.Enabled = false;
            //ultraTabPageControl5.Enabled = false;
            //ultraTabPageControl6.Enabled = false;
        }
        private void habilitarBotones(bool nuevo, bool modificar, bool imprimir, bool exportar, bool salir) 
        {
            btnNuevo.Enabled = nuevo;
            btnGuardar.Enabled = modificar;
            btnImprimir.Enabled = imprimir;
            btnExportarExcel.Enabled = exportar;
            btnSalir.Enabled = salir; 
        }
        public void LimpiarCampos()
        {
            txtServicio.Text = "";
            txtSala.Text = "";
            txtPreOperatorio.Text = "";
            txtPostOperatorio.Text = "";
            txtProyectada.Text = "";

                rdbElectiva.Checked = false;
                rdbEmergencia.Checked = false;
                rdbPaleativa.Checked = false;
            txtRealizada.Text = "";
            txtCirujano.Text = "";
            txtPAyudante.Text = "";
            txtSAyudante.Text = "";
            txtTAyudante.Text = "";
            txtInstrumentista.Text = "";
            txtCircundante.Text = "";
            txtAnestesista.Text = "";
            txtAyuAnestesista.Text = "";
            dtpFecha.Value = DateTime.Now;
            txtHoraInicio.Text = "";
            txtHoraTerm.Text = "";
            txtTipoAnestesia.SelectedIndex = 1;
            //txtTipoAnestesia.Text = protocolo.PROT_TIPOANEST;
            rtbDieresis.Text = "";
            rtbExposicion.Text = "";
            rtbExploracion.Text = "";
            rtbProcedimientos.Text = "";
            rtbSintesis.Text = "";
            rtbComplicaciones.Text = "";
            txtHoraAnestecia.Text = "";
                rdbSi.Checked = false;
                rdbNo.Checked = false;
            rtbDiagHispa.Text = "";
            txtDictada.Text = "";
            dtpFechaDictada.Value = DateTime.Now;
            txtHoraDictada.Text = "";
            if (gridSol.Rows.Count > 1)
                txtEscrita.Text = His.Entidades.Clases.Sesion.nomUsuario;
            txtProfesional.Text = "";
        }

        private void llenarCamposProtocolo(HC_PROTOCOLO_OPERATORIO protocolo) 
        {
            txtServicio.Text = nuevoProtocolo.PROT_SERVICIO;
            txtSala.Text = nuevoProtocolo.PROT_SALA; 
            txtPreOperatorio.Text = protocolo.PROT_PREOPERATORIO;
            txtPostOperatorio.Text = protocolo.PROT_POSTOPERATORIO;
            txtProyectada.Text = protocolo.PROT_PROYECTADA;
            if(protocolo.PROT_ELECTIVA == 1)            
                rdbElectiva.Checked = true;            
            if (protocolo.PROT_EMERGENTE == 1)            
                rdbEmergencia.Checked = true;            
            if (protocolo.PROT_PALEATIVA == 1)            
                rdbPaleativa.Checked = true;
            txtRealizada.Text = protocolo.PROT_REALIZADO;
            txtCirujano.Text = protocolo.PROT_CIRUJANO;
            txtPAyudante.Text = protocolo.PROT_PAYUDANTE;
            txtSAyudante.Text = protocolo.PROT_SAYUDANTE;
            txtTAyudante.Text = protocolo.PROT_TAYUDANTE;
            txtInstrumentista.Text = protocolo.PROT_INSTRUMENTISTA;
            txtCircundante.Text = protocolo.PROT_CIRCULANTE;
            txtAnestesista.Text = protocolo.PROT_ANESTESISTA;
            txtAyuAnestesista.Text = protocolo.PROT_AYUANESTESIA;
            dtpFecha.Value = protocolo.PROT_FECHA.Value;
            txtHoraInicio.Text = protocolo.PROT_HORAINICIO;
            txtHoraTerm.Text = protocolo.PROT_HORAFIN;
            int index = txtTipoAnestesia.FindString(protocolo.PROT_TIPOANEST);
            txtTipoAnestesia.SelectedIndex = index;
            if (txtTipoAnestesia.SelectedIndex == -1)
            {
                txtTipoAnestesia.Text = protocolo.PROT_TIPOANEST;
            }
            //txtTipoAnestesia.Text = protocolo.PROT_TIPOANEST;
            rtbDieresis.Text = protocolo.PROT_DIERESIS;
            rtbExposicion.Text = protocolo.PROT_EXPOSICION;
            rtbExploracion.Text = protocolo.PROT_EXPLORACION;
            rtbProcedimientos.Text = protocolo.PROT_PROCEDIMIENTO;
            rtbSintesis.Text = protocolo.PROT_SINTESIS;
            rtbComplicaciones.Text = protocolo.PROT_COMPLICACIONES;
            if (protocolo.PROT_EXAMENHIS == true)
                rdbSi.Checked = true;
            else
                rdbNo.Checked = true;
            rtbDiagHispa.Text = protocolo.PROT_DIAGNOSTICOH;
            txtDictada.Text = protocolo.PROT_DICTADO;
            dtpFechaDictada.Value = protocolo.PROT_FECHADIC.Value;
            txtHoraDictada.Text = protocolo.PROT_HORADIC;
            txtEscrita.Text = protocolo.PROT_ESCRITA;
            txtProfesional.Text = protocolo.PROT_PROFESIONAL;          

        }

        private HC_PROTOCOLO_OPERATORIO llenarProtocolo()
        {
            nuevoProtocolo.PROT_SERVICIO = txtServicio.Text;
            nuevoProtocolo.PROT_SALA = txtSala.Text;            
            nuevoProtocolo.PROT_PREOPERATORIO = txtPreOperatorio.Text;
            nuevoProtocolo.PROT_POSTOPERATORIO = txtPostOperatorio.Text;
            nuevoProtocolo.PROT_PROYECTADA = txtProyectada.Text;
            nuevoProtocolo.ADF_CODIGO = CodigoProtocolo; ;
            if (rdbElectiva.Checked == true) 
            {
                nuevoProtocolo.PROT_ELECTIVA = 1;
                nuevoProtocolo.PROT_EMERGENTE = 0;
                nuevoProtocolo.PROT_PALEATIVA = 0;
            }
            if (rdbEmergencia.Checked == true)
            {
                nuevoProtocolo.PROT_ELECTIVA = 0;
                nuevoProtocolo.PROT_EMERGENTE = 1;
                nuevoProtocolo.PROT_PALEATIVA = 0;
            }
            if (rdbPaleativa.Checked == true)
            {
                nuevoProtocolo.PROT_ELECTIVA = 0;
                nuevoProtocolo.PROT_EMERGENTE = 0;
                nuevoProtocolo.PROT_PALEATIVA = 1;
            }             
            nuevoProtocolo.PROT_REALIZADO = txtRealizada.Text;
            nuevoProtocolo.PROT_CIRUJANO = txtCirujano.Text;
            nuevoProtocolo.PROT_PAYUDANTE = txtPAyudante.Text;
            nuevoProtocolo.PROT_SAYUDANTE = txtSAyudante.Text;
            nuevoProtocolo.PROT_TAYUDANTE = txtTAyudante.Text;
            nuevoProtocolo.PROT_INSTRUMENTISTA = txtInstrumentista.Text;
            nuevoProtocolo.PROT_CIRCULANTE = txtCircundante.Text;
            nuevoProtocolo.PROT_ANESTESISTA = txtAnestesista.Text;
            nuevoProtocolo.PROT_AYUANESTESIA = txtAyuAnestesista.Text;
            nuevoProtocolo.PROT_FECHA = dtpFecha.Value;
            nuevoProtocolo.PROT_HORAINICIO = txtHoraInicio.Text;
            nuevoProtocolo.PROT_HORAFIN = txtHoraTerm.Text;
            nuevoProtocolo.PROT_TIPOANEST = txtTipoAnestesia.Text;

            nuevoProtocolo.PROT_DIERESIS = rtbDieresis.Text;
            nuevoProtocolo.PROT_EXPOSICION = rtbExposicion.Text;
            nuevoProtocolo.PROT_EXPLORACION = rtbExploracion.Text;
            nuevoProtocolo.PROT_PROCEDIMIENTO = rtbProcedimientos.Text;
            nuevoProtocolo.PROT_SINTESIS = rtbSintesis.Text;
            nuevoProtocolo.PROT_COMPLICACIONES = rtbComplicaciones.Text;
            if (rdbSi.Checked == true)
                nuevoProtocolo.PROT_EXAMENHIS = true;
            else
                nuevoProtocolo.PROT_EXAMENHIS = false;
            nuevoProtocolo.PROT_DIAGNOSTICOH = rtbDiagHispa.Text;
            nuevoProtocolo.PROT_DICTADO = txtDictada.Text;
            nuevoProtocolo.PROT_FECHADIC = dtpFechaDictada.Value;
            nuevoProtocolo.PROT_HORADIC = txtHoraDictada.Text;
            nuevoProtocolo.PROT_ESCRITA = txtEscrita.Text;
            nuevoProtocolo.PROT_PROFESIONAL = txtProfesional.Text;
            return nuevoProtocolo;
        }

        private void cargarAtencion(int codAtencion,int CodigoProtocolo)
        {
            try
            {
                atencion = NegAtenciones.RecuperarAtencionID(codAtencion);
                aseguradora = NegAseguradoras.recuperaAseguradoraPorAtencion(codAtencion);
                lbl_aseguradora.Text = aseguradora.ASE_NOMBRE;
                txtEscrita.Text = His.Entidades.Clases.Sesion.nomUsuario;
                txtDictada.Text = His.Entidades.Clases.Sesion.nomUsuario;
                cargarPaciente(atencion.PACIENTES.PAC_CODIGO);
                List<MEDICOS> medicos = NegMedicos.listaMedicos();
                int codigoMedico = medicos.FirstOrDefault(m => m.EntityKey == atencion.MEDICOSReference.EntityKey).MED_CODIGO;
               // if (codigoMedico != 0)
                    cargarMedico(codigoMedico);
                HABITACIONES hab = NegHabitaciones.listaHabitaciones().FirstOrDefault(h => h.EntityKey == atencion.HABITACIONESReference.EntityKey);
                if (hab != null)
                    txtCama.Text = hab.hab_Numero;
                cargarProtocolo(codAtencion, CodigoProtocolo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo cargar los datos de la atencion, error: " + ex.Message, "error");
            }
        }

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
        }

        private void cargarMedico(int cod)
        {
            medico = NegMedicos.RecuperaMedicoId(cod);
            lbl_medico.Text = medico.MED_APELLIDO_PATERNO.Trim() + " " +
                medico.MED_APELLIDO_MATERNO.Trim() + " " +
                medico.MED_NOMBRE1.Trim() + " " + medico.MED_NOMBRE2.Trim(); 
        }

        private void cargarProtocolo(int codAtencion, int CodigoProtocolo)
        {
            nuevoProtocolo = NegProtocoloOperatorio.recuperarProtocolo(codAtencion, CodigoProtocolo);
            try
            {
                txtHoraAnestecia.Text = NegProtocoloOperatorio.RecuperarHoraAnestesia(CodigoProtocolo, codAtencion);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo recuperar la hora de anestesia. Más Detalles en: \r\n" + ex.Message, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            if (nuevoProtocolo == null)
            {
                habilitarBotones(true, false, false, false, true);                                
            }
            else 
            {
                habilitarBotones(false, true, true,true, true);
                tabulador.Enabled = true;
                llenarCamposProtocolo(nuevoProtocolo);
            }
        }

        private bool validarFormulario()
        {
            errorProtocolo.Clear();
            bool valido = false;
            if (txtServicio.Text.Trim() == string.Empty) 
            {
                AgregarError(txtServicio);
                valido = true;
            }
            if (txtSala.Text.Trim() == string.Empty)
            {
                AgregarError(txtSala);
                valido = true;
            }
            if (txtPreOperatorio.Text.Trim() == string.Empty) 
            {
                AgregarError(txtPreOperatorio);
                valido = true;
            }

            if (txtPostOperatorio.Text.Trim() == string.Empty)
            {
                AgregarError(txtPostOperatorio);
                valido = true;
            }

            if (txtProyectada.Text.Trim() == string.Empty)
            {
                AgregarError(txtProyectada);
                valido = true;
            }
            if (txtRealizada.Text.Trim() == string.Empty)
            {
                AgregarError(txtRealizada);
                valido = true;
            }

            if(rtbComplicaciones.Text.Trim()==string.Empty)
            {
                AgregarError(rtbComplicaciones);
                valido = true;
            }

            if(rtbExploracion.Text.Trim()==string.Empty)
            {
                AgregarError(rtbExploracion);
                valido = true;
            }

            if (rdbElectiva.Checked == false)
            {
                if (rdbEmergencia.Checked == false)
                {
                    if (rdbPaleativa.Checked == false)
                    {
                        AgregarError(gbOperacion);
                        valido = true;
                    }
                }
            }

            if (txtCirujano.Text.Trim() == string.Empty) 
            {
                AgregarError(txtCirujano);
                valido = true;
            }

            if (txtAnestesista.Text.Trim() == string.Empty)
            {
                AgregarError(txtAnestesista);
                valido = true;
            }
            if (txtTipoAnestesia.Text.Trim() == string.Empty)
            {
                AgregarError(txtTipoAnestesia);
                valido = true;
            }

            if (rdbSi.Checked == false)
            {
                if (rdbNo.Checked == false)
                {
                    AgregarError(gbExamenHis);
                    valido = true;
                }
            }
            
            //if (txtDictada.Text.Trim() == string.Empty)
            //{
            //    AgregarError(txtDictada);
            //    valido = true;
            //} 
            if (txtEscrita.Text.Trim() == string.Empty)
            {
                AgregarError(txtEscrita);
                valido = true;
            }
            if (txtProfesional.Text.Trim() == string.Empty)
            {
                AgregarError(txtProfesional);
                valido = true;
            } 
            if(valido == true)
            {
                MessageBox.Show("Datos Incompletos, Revise todos los campos e intente guardar nuevamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return valido;
        }

        private void AgregarError(Control control)
        {
            errorProtocolo.SetError(control, "Campo Requerido");
        }

        private void guardarProtocolo() 
        {
            int prot_codigo;
            if (!validarFormulario())
            {
                if (!controlarCamposExtensos())
                {
                    try
                    {
                        if (nuevo == true)
                        {
                            if(gridSol.Rows.Count > 1)
                            {
                                if (NegImagen.getProtocoloExiste(Convert.ToInt64(CodigoProtocolo)))
                                {
                                    Int64 adf_codigo = NegImagen.getCodigoProtocolo(Convert.ToInt64(CodigoAtencion));
                                    CodigoProtocolo = Convert.ToInt32(adf_codigo);
                                }
                            }                           
                            nuevoProtocolo = new HC_PROTOCOLO_OPERATORIO();
                            nuevoProtocolo.PROT_CODIGO = NegProtocoloOperatorio.ultimoCodigo() + 1;
                            prot_codigo = NegProtocoloOperatorio.ultimoCodigo() + 1;
                            nuevoProtocolo.ATENCIONESReference.EntityKey = atencion.EntityKey;
                            nuevoProtocolo = llenarProtocolo();
                            NegProtocoloOperatorio.crearProtocolo(nuevoProtocolo);

                            //Guardamos la hora de anestesia
                            NegProtocoloOperatorio.GuardarHoraAnestesia(prot_codigo, txtHoraAnestecia.Text);

                            MessageBox.Show("Registro Guardado", "PROTOCOLO OPERATORIO", MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);
                            nuevo = false;
                            habilitarBotones(true, false, true, true, false );
                            BloquearTabulador(false);
                            refrescarSolicitudes();
                        }
                        else
                        {
                            nuevoProtocolo = llenarProtocolo();
                            NegProtocoloOperatorio.actualizarProtocolo(nuevoProtocolo);
                            MessageBox.Show("Registro Guardado", "PROTOCOLO OPERATORIO", MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);
                            habilitarBotones(true, false, true, true, true);
                            BloquearTabulador(false);
                            refrescarSolicitudes();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese todos los campos", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private bool controlarCamposExtensos()
        {
            bool flag = false;
            if (rtbProcedimientos.TextLength <= 2000)
            {
                int cont = 0;
                foreach (string s in rtbProcedimientos.Text.Split('\n'))
                    cont++;
                if (cont > 32)
                {
                    AgregarError(rtbProcedimientos);
                    flag = true;
                }
            }
            else
            {
                AgregarError(rtbProcedimientos);
                flag = true;
            }
            return flag;
        }


        private void validarTextoProcedimiento()
        {
            proced1 = "";
            proced2 = "";
            int firstLine = rtbProcedimientos.GetLineFromCharIndex(0);
            int lastLine = rtbProcedimientos.GetLineFromCharIndex(rtbProcedimientos.Text.Length);
            List<string> lineas = new List<string>();

            for (int i = firstLine; i <= lastLine; i++)
            {
                int firstIndexFromLine = rtbProcedimientos.GetFirstCharIndexFromLine(i);
                int firstIndexFromNextLine = rtbProcedimientos.GetFirstCharIndexFromLine(i + 1);

                if (firstIndexFromNextLine == -1)
                {
                    // Get character index of last character in this line:
                    Point pt = new Point(rtbProcedimientos.ClientRectangle.Width, rtbProcedimientos.GetPositionFromCharIndex(firstIndexFromLine).Y);
                    firstIndexFromNextLine = rtbProcedimientos.GetCharIndexFromPosition(pt);
                    firstIndexFromNextLine += 1;
                }

                lineas.Add(rtbProcedimientos.Text.Substring(firstIndexFromLine, firstIndexFromNextLine - firstIndexFromLine));
                if(i<9)
                {
                    proced1 = proced1 +
                              rtbProcedimientos.Text.Substring(firstIndexFromLine,
                                                               firstIndexFromNextLine - firstIndexFromLine);
                }else
                {
                    proced2 = proced2 +
                              rtbProcedimientos.Text.Substring(firstIndexFromLine,
                                                               firstIndexFromNextLine - firstIndexFromLine);
                }


            }
        }

        #endregion

        #region Eventos

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            nuevo = true;
            habilitarBotones(false,true, false, false, true);
            LimpiarCampos();
            BloquearTabulador(true);
            tabulador.Enabled = true;
        }   
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            guardarProtocolo();
            //imprimirReporte("pdf");
        }

        private void txtServicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                txtSala.Focus();
            }
        }

        private void txtSala_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                txtPreOperatorio.Focus();
            }
        }

        //private void txtCama_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
        //    {
        //        e.Handled = true;
        //        SendKeys.SendWait("{TAB}");
        //        txtPreOperatorio.Focus();
        //    }
        //} 
        private void txtPreOperatorio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txtPostOperatorio.Focus();
            }
        }

        private void txtPostOperatorio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txtProyectada.Focus();
            }            
        }

        private void txtProyectada_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                rdbElectiva.Focus();
            }            
        }
        private void rdbElectiva_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                txtRealizada.Focus();
            }
        }

        //private void rdbEmergencia_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
        //    {
        //        e.Handled = true;
        //        SendKeys.SendWait("{TAB}");
        //        rdbPaleativa.Focus();
        //    }
        //}

        //private void rdbPaleativa_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
        //    {
        //        e.Handled = true;
        //        SendKeys.SendWait("{TAB}");
        //        txtRealizada.Focus();
        //    }
        //}
        private void txtRealizada_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                tabulador.SelectedTab = tabulador.Tabs["equipo"];
                txtCirujano.Focus();
            }              
        }       

        private void txtCirujano_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                txtPAyudante.Focus();
                txtProfesional.Text = txtCirujano.Text;
            }
        }

        private void txtPAyudante_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                txtSAyudante.Focus();
            }
        }

        private void txtSAyudante_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                txtTAyudante.Focus();
            }
        }

        private void txtTAyudante_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                txtInstrumentista.Focus();
            }
        }

        private void txtInstrumentista_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                txtCircundante.Focus();
            }
        }

        private void txtCircundante_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                txtAnestesista.Focus();
            }
        }

        private void txtAnestesista_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                txtAyuAnestesista.Focus();
            }
        }

        private void txtAyuAnestesista_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                dtpFecha.Focus();
            }
        }

        private void dtpFecha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                txtHoraInicio.Focus();
            }
        }

        //private void txtHoraI_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
        //    {
        //        e.Handled = true;
        //        SendKeys.SendWait("{TAB}");
        //        txtHoraFin.Focus();
        //    }
        //}

        //private void txtHoraFin_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
        //    {
        //        e.Handled = true;
        //        SendKeys.SendWait("{TAB}");
        //        txtTipoAnestesia.Focus();
        //    }
        //}

        private void txtHoraInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                txtHoraTerm.Focus();
            } 
        }

        private void txtHoraTerm_KeyPress(object sender, KeyPressEventArgs e)
        {
          if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                txtTipoAnestesia.Focus();
            }
        }

        private void txtTipoAnestesia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                tabulador.SelectedTab = tabulador.Tabs["tiempos"];
                SendKeys.SendWait("{TAB}");
                rtbDieresis.Focus();
            }
        }
        
        private void rtbDieresis_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                rtbExposicion.Focus();
            }             
        }

        private void rtbExposicion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                rtbExploracion.Focus();
            }            
        }

        private void rtbExploracion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                rtbProcedimientos.Focus();
            }            
        }

        private void rtbProcedimientos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                tabulador.SelectedTab = tabulador.Tabs["resumen"];
                rtbSintesis.Focus();

            }
            int linea = rtbProcedimientos.GetLineFromCharIndex(rtbProcedimientos.TextLength) + 1;
            if (linea > 35)
            {
                e.Handled = true;
                tabulador.SelectedTab = tabulador.Tabs["resumen"];
                rtbSintesis.Focus();
            }
        }

        private void rtbSintesis_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                rtbComplicaciones.Focus();
            }             
        }

        private void rtbComplicaciones_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                rdbSi.Focus();
            }            
        }

        private void rdbSi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Tab))
            {               
                e.Handled = true;
                rtbDiagHispa.Focus();
            }            
        }


        private void rtbDiagHispa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txtDictada.Focus();
            }             
        }

        private void txtDictada_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                dtpFechaDictada.Focus();
            }             
        }

        private void dtpFechaDictada_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txtHoraDictada.Focus();
            }
        }

        private void txtHoraDictad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txtEscrita.Focus();
            }            
        }
        
        private void txtEscrita_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txtProfesional.Focus();
            }
        }

        private void txtProfesional_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txtPreOperatorio.Focus();
            }            
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            imprimirReporte("reporte");
        }

        private void imprimirReporte(string accion)
        {
            try
            {
                string FullPath;
                FullPath = "C:\\Sic3000\\Iconos\\LogoEmpresa.png";
                ReporteProtocoloOperatorio reporte = new ReporteProtocoloOperatorio();
                NegCertificadoMedico m = new NegCertificadoMedico();
                reporte.PROT_HISTORIA_CLINICA = paciente.PAC_HISTORIA_CLINICA;
                reporte.PROT_EMPRESA = His.Entidades.Clases.Sesion.nomEmpresa;
                reporte.PROT_NUMHOJA = 1;
                reporte.PROT_LOGO = m.path();//envia la direccion del logo de la empresa
                reporte.PROT_NOMBREPACIENTE = paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO;
                reporte.PROT_APELLIDOPACIENTE = paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2;
                reporte.PROT_GENERO = txt_sexo.Text;
                reporte.PROT_NUMHOJA = 1;
                reporte.PROT_MEDICO = medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + " " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
                reporte.PROT_SERVICIO = txtServicio.Text;
                reporte.PROT_SALA = txtSala.Text;
                reporte.PROT_HABITACION = txtCama.Text;
                reporte.PROT_PREOPERATORIO = txtPreOperatorio.Text;
                reporte.PROT_POSTOPERATORIO = txtPostOperatorio.Text;
                reporte.PROT_PROYECTADA = txtProyectada.Text;
                if (rdbElectiva.Checked == true)
                {
                    reporte.PROT_ELECTIVA = "X";
                    reporte.PROT_EMERGENCIA = " ";
                    reporte.PROT_PALEATIVA = " ";
                }
                if (rdbEmergencia.Checked == true)
                {
                    reporte.PROT_ELECTIVA = " ";
                    reporte.PROT_EMERGENCIA = "X";
                    reporte.PROT_PALEATIVA = " ";
                }
                if (rdbPaleativa.Checked == true)
                {
                    reporte.PROT_ELECTIVA = " ";
                    reporte.PROT_EMERGENCIA = " ";
                    reporte.PROT_PALEATIVA = "X";
                }
                reporte.PROT_REALIZADO = txtRealizada.Text;
                reporte.PROT_CIRUJANO = txtCirujano.Text;
                reporte.PROT_PAYUDANTE = txtPAyudante.Text;
                reporte.PROT_SAYUDANTE = txtSAyudante.Text;
                reporte.PROT_TAYUDANTE = txtTAyudante.Text;
                reporte.PROT_INSTRUMENTISTA = txtInstrumentista.Text;
                reporte.PROT_CIRCULANTE = txtCircundante.Text;
                reporte.PROT_ANESTESISTA = txtAnestesista.Text;
                reporte.PROT_AYUANESTESISTA = txtAyuAnestesista.Text;
                reporte.PROT_FECHA = dtpFecha.Value;
                reporte.PROT_HORAI = txtHoraInicio.Text;
                reporte.PROT_HORAT = txtHoraTerm.Text;
                reporte.PROT_TIPOANESTESIA = txtTipoAnestesia.Text;
                reporte.PROT_DIERESIS = validarTexto(rtbDieresis.Text).Trim();
                reporte.PROT_EXPOSICION = rtbExposicion.Text;
                reporte.PROT_EXPLORACION = rtbExploracion.Text;
                validarTextoProcedimiento();

                

            reporte.PROT_PROCEDIMIENTO = proced1;
            reporte.PROT_PROCEDIMIENTO2 = proced2;
            //string cadena = rtbProcedimientos.Text;
            //string texto1 = "\n";
            //    int lon = 0;
            //    int espacioUlt = 0;
            //int contln = 0;
            //    int posicion = 0;
            //for (int i = 0; i < cadena.Length; i++)
            //{
            //    {
            //        posicion = i;
            //        if (contln < 9 && lon < 710)
            //        {
            //            if (contln == 8)
            //            {
            //                if (cadena.Length - texto1.Length > 117)
            //                {
            //                    string texto2 = "";
            //                    for (int j = i; j < i + 117; j++)
            //                    {
            //                        if ((cadena.Substring(j, 1) == "\n"))
            //                        {
            //                            posicion = j;
            //                            break;
            //                        }
            //                        else
            //                        {
            //                            if (cadena.Substring(j, 1) == " ")
            //                                posicion = j;
            //                            texto2 = texto2 + cadena.Substring(j, 1);
            //                        }
            //                    }
            //                    texto1 = texto1 + espacioUlt;
            //                    i = posicion;
            //                    posicion = i;
            //                }
            //                contln++;
                            
            //            }else
            //            {
            //                if(contln == 1){}
            //                if ((cadena.Substring(i, 1) == "\n"))
            //                {
            //                    contln++;
            //                    lon = cadena.Length -posicion;
            //                }
            //                if (cadena.Substring(i, 1) == " ")
            //                    espacioUlt = i;
            //                if(i> 960)
            //                    break;
            //            }
            //        }else
            //                break;
                   
            //    }
            //}
            //reporte.PROT_PROCEDIMIENTO = "\n" + cadena.Substring(0, posicion);
            //    reporte.PROT_PROCEDIMIENTO2 = "\n"+cadena.Substring(posicion, (cadena.Length-posicion));
            reporte.PROT_SINTESIS = rtbSintesis.Text;
            reporte.PROT_COMPLICACIONES = rtbComplicaciones.Text;
            if (rdbSi.Checked == true)
            {
                reporte.PROT_EXAMENHISSI = "X";
                reporte.PROT_EXAMENHISNO = " ";
            }
            if (rdbNo.Checked == true)
            {
                reporte.PROT_EXAMENHISSI = " ";
                reporte.PROT_EXAMENHISNO = "X";
            }

            reporte.PROT_DIAGNOSTICOSHIS = rtbDiagHispa.Text;
            reporte.PROT_DICTADO = txtDictada.Text;
            reporte.PROT_FECHADIC = dtpFechaDictada.Value;
            reporte.PROT_HORADIC = txtHoraDictada.Text;
            reporte.PROT_ESCRITA = txtEscrita.Text;
            reporte.PROT_PROFESIONAL = txtProfesional.Text;
                reporte.PROT_FECHA_OD = Convert.ToString(dtpFecha.Value).Substring(0, 2);
                reporte.PROT_FECHA_OM = Convert.ToString(dtpFecha.Value).Substring(3, 2);
                reporte.PROT_FECHA_OA = Convert.ToString(dtpFecha.Value).Substring(6, 4);
           
            reporte.PROT_FECHA_DH = Convert.ToString(dtpFecha.Value).Substring(11, 5);
            reporte.PROT_FECHA_DM = Convert.ToString(dtpFecha.Value).Substring(0, 2);
            reporte.PROT_FECHA_DD = Convert.ToString(dtpFecha.Value).Substring(3, 2);
            reporte.PROT_FECHA_DA = Convert.ToString(dtpFecha.Value).Substring(6, 4);


            //ReportesHistoriaClinica reporteAnamnesis = new ReportesHistoriaClinica();
            //reporteAnamnesis.ingresarAnamnesis(reporte);
            //frmReportes ventana = new frmReportes(1, "anamnesis");
            ////ventana.Show();
            //if (accion.Equals("reporte"))
            //    ventana.Show();
            //else
            //{
            //    CrearCarpetas_Srvidor("anamnesis");
            //}


                ReportesHistoriaClinica reporteOperatorio = new ReportesHistoriaClinica();
                reporteOperatorio.ingresarProtocolo(reporte);
                frmReportes ventana = new frmReportes(1, "protocolo");
                if (accion.Equals("reporte"))
                    ventana.Show();
                else
                {
                    CrearCarpetas_Srvidor("protocolo");
                }

            //ventana.Show();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string validarTexto(string campo, int indice)
        {
            int posicion = 0;
            string cadena = "";
            string texto = campo.Substring(indice, (campo.Length - indice));
            int cont = 0;
            for (int i = indice; i < campo.Length; i++)
            {
                if ((posicion < campo.Length - 2) && (cont <= 500))
                {
                    if (!(campo.Substring(i, 2) == "\r\n"))
                    {
                        cadena = cadena + campo.Substring(i, 1);
                        cont++;
                        posicion++;
                    }
                    else
                    {
                        posicion = i + 2;
                        return cadena;
                    }
                }
                else
                {
                    //int pos = CampoValidado(campo, cadena);
                    return cadena;
                }
                if ((posicion + 2 == campo.Length))
                {
                    cadena = cadena + "" + campo.Substring(posicion, 2);
                }
            }
            return cadena;
        }



        public void CrearCarpetas_Srvidor(string modo_formulario)
        {
            try
            {
                His.DatosReportes.Datos.GenerarPdf pdf = new His.DatosReportes.Datos.GenerarPdf();
                pdf.reporte = modo_formulario;
                pdf.campo1 = atencion.ATE_CODIGO.ToString();
                pdf.nuemro_atencion = atencion.ATE_NUMERO_ATENCION.ToString();
                pdf.clinica = paciente.PAC_HISTORIA_CLINICA.ToString();
                pdf.generar();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private string validarTexto(string campo)
        {
            string cadena = "";
            int cont = 0;
            for (int i = 0; i < campo.Length; i++)
            {
                if (!(campo.Substring(i,1) == "\n"))
                {
                    cadena = cadena + campo.Substring(i, 1);
                }
                else
                {
                    cadena = cadena + "  -  ";
                }
            }
            return cadena;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnF1Cirujano_Click(object sender, EventArgs e)
        {
            List<MEDICOS> listaMedicos;

            listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
            frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
            //ayuda.campoPadre = txt;
            ayuda.ShowDialog();
            string cadena = txtCirujano.Text;
            
            if (ayuda.campoPadre.Text != string.Empty)
                txtCirujano.Text = cargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()), cadena);
            txtProfesional.Text = txtCirujano.Text;
        }

        private string MedicoNombre(int codMedico, string cadena)
        {
            DataTable med = NegMedicos.MedicoIDValida(codMedico);
            if (med.Rows[0][0].ToString() == "7")
            {
                MessageBox.Show("MEDICO SUSPENDIDO", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return "";
            }
            medico = NegMedicos.MedicoID(codMedico);

            if (medico != null)
                cadena = cadena + "" + medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + " " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
            else
                cadena = string.Empty;
            return cadena;
        }
        private string cargarMedico(int codMedico, string cadena)
        {
            DataTable med = NegMedicos.MedicoIDValida(Convert.ToInt16(codMedico));
            if (med.Rows[0][0].ToString() == "7")
            {
                MessageBox.Show("MEDICO SUSPENDIDO", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                return "";
            }
            medico = NegMedicos.MedicoID(codMedico);

            if (medico != null)
            {
                if(cadena != "")
                {
                    cadena = cadena + "/" + medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + " " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
                }
                else
                {
                    cadena = cadena + medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + " " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
                }
            }
            else
                cadena= string.Empty;
            return cadena;
        }

        private void btnPAyudante_Click(object sender, EventArgs e)
        {
            List<MEDICOS> listaMedicos;

            listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
            frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
            //ayuda.campoPadre = txt;
            ayuda.ShowDialog();
            string cadena = txtPAyudante.Text;
            if (ayuda.campoPadre.Text != string.Empty)
                txtPAyudante.Text = cargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()), cadena);
        }

        private void btnSAyudante_Click(object sender, EventArgs e)
        {
            List<MEDICOS> listaMedicos;

            listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
            frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
            //ayuda.campoPadre = txt;
            ayuda.ShowDialog();
            string cadena = txtSAyudante.Text;
            if (ayuda.campoPadre.Text != string.Empty)
                txtSAyudante.Text = cargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()), cadena);
        }

        private void btnCirculante_Click(object sender, EventArgs e)
        {
            //List<MEDICOS> listaMedicos;

            //listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
            //frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
            ////ayuda.campoPadre = txt;
            //ayuda.ShowDialog();
            //string cadena = txtCircundante.Text;
            //if (ayuda.campoPadre.Text != string.Empty)
            //    txtCircundante.Text = cargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()), cadena);

            frm_Ayuda_Certificado.Personal = true;
            frm_Ayuda_Certificado x = new frm_Ayuda_Certificado();
            x.ShowDialog();

            frm_Ayuda_Certificado.Personal = false;

            if (txtCircundante.Text.Length <= 0)
            {
                txtCircundante.Text = personal;
            }
            else
            {
                string cadena = txtCircundante.Text;
                txtCircundante.Text = "";
                txtCircundante.Text = cadena + "/" + personal;
            }
        }

        private void txtAnestesista_Click(object sender, EventArgs e)
        {
            List<MEDICOS> listaMedicos;

            listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
            frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
            //ayuda.campoPadre = txt;
            ayuda.ShowDialog();
            string cadena = txtAnestesista.Text;
            if (ayuda.campoPadre.Text != string.Empty)
                txtAnestesista.Text = cargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()), cadena);
        }

        private void btnAAnest_Click(object sender, EventArgs e)
        {
            List<MEDICOS> listaMedicos;

            listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
            frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
            //ayuda.campoPadre = txt;
            ayuda.ShowDialog();
            string cadena = txtAyuAnestesista.Text;
            if (ayuda.campoPadre.Text != string.Empty)
                txtAyuAnestesista.Text = cargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()), cadena);
        }

        private void rtbProcedimientos_KeyDown(object sender, KeyEventArgs e)
        {
            //if(e.KeyCode.Equals("\r"))
            //{
            //    contlrP1++;
            //}
            //if(contlrP1>8)
            //{
            //    e.Handled = true;
            //    rtbProcedimientos.Focus();
            //}else
            //{
            //    if(rtbProcedimientos.TextLength > 800)
            //    {
            //        MessageBox.Show("Campo demasiado Extenso");
            //    }
            //}

        }

        private void rtbProcedimientos2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == (char)(Keys.Tab))
            //{
            //    e.Handled = true;
            //    tabulador.SelectedTab = tabulador.Tabs["resumen"];
            //    rtbSintesis.Focus();
            //}
            //int linea = rtbProcedimientos2.GetLineFromCharIndex(rtbProcedimientos2.TextLength) + 1;
            //if (linea <= 25) return;
            //e.Handled = true;
            //tabulador.SelectedTab = tabulador.Tabs["resumen"];
            //rtbSintesis.Focus();
        }

        private void btnInstru_Click(object sender, EventArgs e)
        {
            //List<MEDICOS> listaMedicos;

            //listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
            //frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
            ////ayuda.campoPadre = txt;
            //ayuda.ShowDialog();
            //string cadena = txtInstrumentista.Text;
            //if (ayuda.campoPadre.Text != string.Empty)
            //    txtInstrumentista.Text = cargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()), cadena);

            frm_Ayuda_Certificado.Personal = true;
            frm_Ayuda_Certificado x = new frm_Ayuda_Certificado();
            x.ShowDialog();

            frm_Ayuda_Certificado.Personal = false;

            if (txtInstrumentista.Text.Length <= 0)
            {
                txtInstrumentista.Text = personal;
            }
            else
            {
                string cadena = txtInstrumentista.Text;
                txtInstrumentista.Text = "";
                txtInstrumentista.Text = cadena + "/" + personal;
            }
        }

        private void btnAnest_Click(object sender, EventArgs e)
        {
            List<MEDICOS> listaMedicos;

            listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
            frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
            //ayuda.campoPadre = txt;
            ayuda.ShowDialog();
            string cadena = txtAnestesista.Text;
            if (ayuda.campoPadre.Text != string.Empty)
                txtAnestesista.Text = cargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()), cadena);
        }

        private void btnTAyudante_Click(object sender, EventArgs e)
        {
            List<MEDICOS> listaMedicos;

            listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
            frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
            //ayuda.campoPadre = txt;
            ayuda.ShowDialog();
            string cadena = txtTAyudante.Text;
            if (ayuda.campoPadre.Text != string.Empty)
                txtTAyudante.Text = cargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()), cadena);
        }

        private void rdbNo_Click(object sender, EventArgs e)
        {
            txtDrPatologo.Enabled = false;
        }

        private void rdbSi_Click(object sender, EventArgs e)
        {
            txtDrPatologo.Enabled = true;
        }

        private void btnf1dr_Click(object sender, EventArgs e)
        {
            txtDrPatologo.Text = "";
            frm_Ayuda_Certificado.patologo = true;
            frm_Ayuda_Certificado x = new frm_Ayuda_Certificado();
            x.ShowDialog();
            txtDrPatologo.Text = medicopatologo;
            frm_Ayuda_Certificado.patologo = false;




            //List<MEDICOS> listaMedicos;

            //listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
            //frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
            ////ayuda.campoPadre = txt;
            //ayuda.ShowDialog();
            //string cadena = txtDrPatologo.Text;
            //if (ayuda.campoPadre.Text != string.Empty)
            //    txtDrPatologo.Text = MedicoNombre(Convert.ToInt32(ayuda.campoPadre.Text.ToString()), cadena);
        }

        private void rdbSi_CheckedChanged(object sender, EventArgs e)
        {
            if(rdbSi.Checked == true)
            {
                btnf1dr.Enabled = true;
                rdbNo.Checked = false;
            }
            else
            {
                btnf1dr.Enabled = false;
            }
        }

        private void rdbNo_CheckedChanged(object sender, EventArgs e)
        {
            if(rdbNo.Checked == true)
            {
                btnf1dr.Enabled = false;
                rdbSi.Checked = false;
                rtbDiagHispa.Enabled = false;
            }
            else
            {
                btnf1dr.Enabled = true;
                rtbDiagHispa.Enabled = true;
            }
        }

        private void btnf1dic_Click(object sender, EventArgs e)
        {
            txtDictada.Text = "";
            List<MEDICOS> listaMedicos;

            listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
            frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
            //ayuda.campoPadre = txt;
            ayuda.ShowDialog();
            string cadena = txtDictada.Text;

            if (ayuda.campoPadre.Text != string.Empty)
                txtDictada.Text = MedicoNombre(Convert.ToInt32(ayuda.campoPadre.Text.ToString()), cadena);
        }

        private void btncie1_Click(object sender, EventArgs e)
        {
            frm_BusquedaCIE10 busqueda = new frm_BusquedaCIE10();
            busqueda.ShowDialog();
            if (busqueda.codigo != null)
            {
                if (txtPreOperatorio.Text.Length <= 0)
                {
                    txtPreOperatorio.Text = busqueda.resultado;
                }
                else
                {
                    string cadena = txtPreOperatorio.Text;
                    txtPreOperatorio.Text = "";
                    txtPreOperatorio.Text = cadena + " -- " + busqueda.resultado;
                }
            }
        }

        private void btncie2_Click(object sender, EventArgs e)
        {
            frm_BusquedaCIE10 busqueda = new frm_BusquedaCIE10();
            busqueda.ShowDialog();
            if (busqueda.codigo != null)
            {
                if (txtPostOperatorio.Text.Length <= 0)
                {
                    txtPostOperatorio.Text = busqueda.resultado;
                }
                else
                {
                    string cadena = txtPostOperatorio.Text;
                    txtPostOperatorio.Text = "";
                    txtPostOperatorio.Text = cadena + " -- " + busqueda.resultado;
                }
            }
        }

        private void btntarifario_Click(object sender, EventArgs e)
        {
            frm_AyudaGeneral x = new frm_AyudaGeneral();
            x.tarifario = true;
            x.ShowDialog();

            if(x.resultado != "")
            {
                if(txtProyectada.Text.Length <= 0)
                {
                    txtProyectada.Text = x.resultado;
                }
                else
                {
                    string cadena = txtProyectada.Text;
                    txtProyectada.Text = "";
                    txtProyectada.Text = cadena + " -- " + x.resultado;
                }
            }
            x.tarifario = false;
        }

        private void btntarifario2_Click(object sender, EventArgs e)
        {
            frm_AyudaGeneral x = new frm_AyudaGeneral();
            x.tarifario = true;
            x.ShowDialog();

            if (x.resultado != "")
            {
                if (txtRealizada.Text.Length <= 0)
                {
                    txtRealizada.Text = x.resultado;
                }
                else
                {
                    string cadena = txtRealizada.Text;
                    txtRealizada.Text = "";
                    txtRealizada.Text = cadena + " -- " + x.resultado;
                }
            }
            x.tarifario = false;
        }
        #endregion

        private void gridSol_CellDoubleClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("¿Desea cargar el Protocolo?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (dialogResult == DialogResult.Yes)
            {
                CodigoProtocolo = Convert.ToInt32(gridSol.Rows[gridSol.CurrentRow.Index].Cells["ID"].Value.ToString());
                cargarAtencion(CodigoAtencion, CodigoProtocolo);
                NegAtenciones atenciones = new NegAtenciones();
                string estado = atenciones.EstadoCuenta(Convert.ToString(CodigoAtencion));
                if (estado != "1")
                {
                    Bloquear();
                }
                else
                    habilitarBotones(true, false, true, true, true);
                ValidarEnfermeria();
                //do something
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            nuevo = false;
            habilitarBotones(false, true, false, false, true);
            BloquearTabulador(true);
        }

        public void BloquearTabulador(bool estado)
        {
            txtSala.Enabled = estado;
            txtServicio.Enabled = estado;
            ultraTabPageControl1.Enabled = estado;
            ultraTabPageControl2.Enabled = estado;
            ultraTabPageControl3.Enabled = estado;
            ultraTabPageControl4.Enabled = estado;
            ultraTabPageControl5.Enabled = estado;
            ultraTabPageControl6.Enabled = estado;
        }

        private void ugbPaciente_Click(object sender, EventArgs e)
        {

        }
    }
}
