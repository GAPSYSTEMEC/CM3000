using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using Recursos;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using His.Parametros;
using His.Entidades.Reportes;
using His.Entidades.Clases;
using His.DatosReportes;

using His.Entidades;
using GeneralApp.ControlesWinForms;

namespace His.Formulario
{
    public partial class frm_Epicrisis : Form
    {
        string diagnostico = String.Empty;
        string codigoCIE = string.Empty;
        string diagnostico2 = String.Empty;
        string codigoCIE2 = string.Empty;
        string modo = "SAVE";
        MaskedTextBox codMedico;
        MEDICOS medico = null;
        MEDICOS medicoTratante = null;
        ATENCIONES atencion = null;
        PACIENTES paciente = null;
        HC_EPICRISIS epicrisis = null;
        HC_ANAMNESIS anamnesis = new HC_ANAMNESIS();
        ASEGURADORAS_EMPRESAS aseguradora = null;
        HC_CATALOGOS catalogo = new HC_CATALOGOS();
        HC_ANAMNESIS_DETALLE detalle = new HC_ANAMNESIS_DETALLE();
        List<HC_ANAMNESIS_DETALLE> listaDetalleEF = new List<HC_ANAMNESIS_DETALLE>();
        List<HC_CATALOGOS> listaCatalogo = new List<HC_CATALOGOS>();
        int index3 = 0;
        int codigoMedico;
        int ate_codigop = 0;
        TextBox txt_Atencion = new TextBox();
        public frm_Epicrisis()
        {
            InitializeComponent();
            frm_AyudaPacientes form = new frm_AyudaPacientes();

            form.ShowDialog();
            txt_Atencion = form.campoAtencion;
            //form.campoAtencion = txt_Atencion;
            int codigoAtencion = Convert.ToInt16(txt_Atencion.Text);
            if (codigoAtencion != 0)
                cargarAtencion(codigoAtencion);
            else
            {
                MessageBox.Show("Debe Seleccionar Un Paciente, Vuelva a ingresar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

            inicializar();
        }

        public frm_Epicrisis(int codigoAtencion)
        {
            //aqui va el estado de cuenta que tiene el paciente para validar el bloqueo
            InitializeComponent();
            inicializar();
            if (codigoAtencion != 0)
                cargarAtencion(codigoAtencion);
            ate_codigop = codigoAtencion;
            NegAtenciones atenciones = new NegAtenciones();
            string estado = atenciones.EstadoCuenta(Convert.ToString(codigoAtencion));
            if (estado != "1")
            {
                Bloquear();
            }
            else
            {
                //no hace nada
            }

            //Cambios Edgar 20210303    Requerimientos de la pasteur por Alex
            if (Sesion.codDepartamento == 6)
            {
                btnNuevo.Enabled = false;
                btnGuardar.Enabled = false;
                btnImprimir.Enabled = false;
                btnModificar.Enabled = false;
            }
        }
        public void Bloquear()
        {
            btnModificar.Enabled = false;
            //btnImprimir.Enabled = false;
            btnNuevo.Enabled = false;
            btnGuardar.Enabled = false;
        }
        private void inicializar()
        {

            btnNuevo.Image = Archivo.imgBtnAdd2;
            btnGuardar.Image = Archivo.imgBtnGoneSave48;
            btnImprimir.Image = Archivo.imgBtnGonePrint48;
            btnSalir.Image = Archivo.imgBtnGoneExit48;
            btnModificar.Image = Archivo.btnEditar16;

            cmb_tipoEgreso.DataSource = NegTipoEgreso.ListaTipoIngreso();
            cmb_tipoEgreso.ValueMember = "TIE_CODIGO";
            cmb_tipoEgreso.DisplayMember = "TIE_DESCRIPCION";

            pictureBox1.Image = Archivo.F1;
            pictureBox2.Image = Archivo.F1;

            panellResumenes.Enabled = false;
            panelDiagnosticos.Enabled = false;
            panelCondiciones.Enabled = false;
            panelEgreso.Enabled = false;

        }

        private void cargarHora()
        {
            DateTime dt = new DateTime();
            txt_fecha.Text = System.DateTime.Now.ToString("yyyy/MM/dd");
            txt_hora.Text = System.DateTime.Now.ToString("HH:mm:ss");

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            //tabControl1.Enabled = true;
            panellResumenes.Enabled = true;
            panelDiagnosticos.Enabled = true;
            panelCondiciones.Enabled = true;
            panelEgreso.Enabled = true;
            btnGuardar.Enabled = true;
            btnImprimir.Enabled = false;
            btnModificar.Enabled = false;
            btnNuevo.Enabled = false;
        }

        private void txt_codmedico_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            //DataGridViewTextBoxCell txtcell = (DataGridViewTextBoxCell)this.dtg_DIngreso.Rows[e.RowIndex].Cells[0];
            //DataGridViewTextBoxCell txtcell2 = (DataGridViewTextBoxCell)this.dtg_DIngreso.Rows[e.RowIndex].Cells[1];
            //if (diagnostico != null)
            //{
            //    txtcell.Value = diagnostico;
            //    txtcell2.Value = codigoCIE;
            //}
        }

        private void dataGridView1_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (dtg_DIngreso.CurrentRow.Cells[1].Value != null)
            {
                if (e.ColumnIndex == this.dtg_DIngreso.Columns[2].Index)
                {
                    DataGridViewCheckBoxCell chkCell = (DataGridViewCheckBoxCell)this.dtg_DIngreso.Rows[e.RowIndex].Cells[2];
                    if (chkCell.Value == null)
                        chkCell.Value = false;
                    else
                        chkCell.Value = true;


                    DataGridViewCheckBoxCell chkCell2 = (DataGridViewCheckBoxCell)this.dtg_DIngreso.Rows[e.RowIndex].Cells[3];
                    chkCell2.Value = false;
                }
                else
                {
                    if (e.ColumnIndex == this.dtg_DIngreso.Columns[3].Index)
                    {
                        DataGridViewCheckBoxCell chkCell = (DataGridViewCheckBoxCell)this.dtg_DIngreso.Rows[e.RowIndex].Cells[3];
                        if (chkCell.Value == null)
                            chkCell.Value = false;
                        else
                            chkCell.Value = true;

                        DataGridViewCheckBoxCell chkCell2 = (DataGridViewCheckBoxCell)this.dtg_DIngreso.Rows[e.RowIndex].Cells[2];
                        chkCell2.Value = false;
                    }

                }
            }
            else
            {
                MessageBox.Show("No se ha agregado diagnostico en esta fila.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtg_DIngreso.Rows.RemoveAt(dtg_DIngreso.CurrentRow.Index);
            }
        }
        int index = 0;
        private void dtg_DIngreso_CellEndEdit(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {

        }

        private void dtg_DIngreso_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                frm_BusquedaCIE10 busqueda = new frm_BusquedaCIE10();
                busqueda.ShowDialog();
                //if()
                diagnostico = busqueda.resultado;
                codigoCIE = busqueda.codigo;
            }
        }

        private void dtg_DEgresos_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                frm_BusquedaCIE10 busqueda = new frm_BusquedaCIE10();
                busqueda.ShowDialog();
                diagnostico2 = busqueda.resultado;
                codigoCIE2 = busqueda.codigo;
            }
        }

        private void dtg_DEgresos_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
        }

        private void dtg_DEgresos_CellEndEdit(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
        }

        private void dtg_DEgresos_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (dtg_DEgresos.CurrentRow.Cells[1].Value != null)
            {
                if (e.ColumnIndex == this.dtg_DEgresos.Columns[2].Index)
                {
                    DataGridViewCheckBoxCell chkCell = (DataGridViewCheckBoxCell)this.dtg_DEgresos.Rows[e.RowIndex].Cells[2];
                    if (chkCell.Value == null)
                        chkCell.Value = false;
                    else
                        chkCell.Value = true;


                    DataGridViewCheckBoxCell chkCell2 = (DataGridViewCheckBoxCell)this.dtg_DEgresos.Rows[e.RowIndex].Cells[3];
                    chkCell2.Value = false;
                }
                else
                {
                    if (e.ColumnIndex == this.dtg_DEgresos.Columns[3].Index)
                    {
                        DataGridViewCheckBoxCell chkCell = (DataGridViewCheckBoxCell)this.dtg_DEgresos.Rows[e.RowIndex].Cells[3];
                        if (chkCell.Value == null)
                            chkCell.Value = false;
                        else
                            chkCell.Value = true;

                        DataGridViewCheckBoxCell chkCell2 = (DataGridViewCheckBoxCell)this.dtg_DEgresos.Rows[e.RowIndex].Cells[2];
                        chkCell2.Value = false;
                    }

                }
            }
            else
            {
                MessageBox.Show("No se ha agregado diagnostico en esta fila.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtg_DEgresos.Rows.RemoveAt(dtg_DEgresos.CurrentRow.Index);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F1)
                {
                    List<MEDICOS> medicos = NegMedicos.listaMedicos();
                    frm_Ayudas frm = new frm_Ayudas(medicos);
                    frm.bandCampo = true;
                    frm.ShowDialog();
                    if (frm.campoPadre2.Text != string.Empty)
                    {
                        codMedico = (frm.campoPadre2);
                        medicoTratante = NegMedicos.RecuperaMedicoId(Convert.ToInt32(codMedico.Text));
                        agregarMedico(medicoTratante);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Médico, error: " + ex.Message, "error");
            }
        }

        private void dataGridView1_CellEndEdit(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
        }

        private void dtg_medicos_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
        }

        private void cargarAtencion(int codAtencion)
        {
            try
            {
                txt_Atencion.Text = codAtencion.ToString();
                DataTable atenciones = new DataTable();
                atenciones = NegAtenciones.Atencion(codAtencion);
                cargarPaciente(Convert.ToInt16(atenciones.Rows[0][0].ToString()));
                cargarHora();
                //List<MEDICOS> medicos = NegMedicos.listaMedicos();
                codigoMedico = (Convert.ToInt16(atenciones.Rows[0][1].ToString()));
                //if (codigoMedico >= 0)
                cargarMedico(codigoMedico);
                cargarEpicrisis();
                aseguradora = NegAseguradoras.recuperaAseguradoraPorAtencion(codAtencion);
                if (aseguradora != null)
                    lbl_aseguradora.Text = aseguradora.ASE_NOMBRE;


            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo cargar los datos de la atencion, error: " + ex.Message, "error");
            }

        }

        private void cargarEpicrisis()
        {
            atencion = NegAtenciones.RecuperarAtencionID(Convert.ToInt16(txt_Atencion.Text));
            epicrisis = NegEpicrisis.recuperarEpicrisisPorAtencion(atencion.ATE_CODIGO);
            DataTable evolucionAnalisis = new DataTable();
            evolucionAnalisis = NegEpicrisis.RecuperaEvolucionAnalisis(atencion.ATE_CODIGO);
            DataTable Protocolo = new DataTable();
            Protocolo = NegProtocoloOperatorio.ProtocoloEpicrisis(atencion.ATE_CODIGO);
            if (epicrisis == null)
            {
                btnNuevo.Enabled = true;
                btnGuardar.Enabled = false;
                //btnImprimir.Enabled = true;
                btnModificar.Enabled = false;
                anamnesis = NegAnamnesis.recuperarAnamnesisPorAtencion(atencion.ATE_CODIGO);
                if (anamnesis != null)
                {
                    string cuadroClinico = " ";
                    listaDetalleEF = NegAnamnesisDetalle.listaDetallesAnamnesis(anamnesis.ANE_CODIGO);
                    listaCatalogo = NegCatalogos.RecuperarHcCatalogosPorTipo(Parametros.ReporteFormularios.codigoCatalogo_ExamenFisico);
                    for (int i = 0; i < listaDetalleEF.Count; i++)
                    {
                        detalle = listaDetalleEF.ElementAt(i);
                        for (int j = 0; j < listaCatalogo.Count; j++)
                        {
                            catalogo = listaCatalogo.ElementAt(j);
                            int codigo = (NegCatalogos.listaCatalogos().FirstOrDefault(c => c.EntityKey == detalle.HC_CATALOGOSReference.EntityKey).HCC_CODIGO);
                            if (catalogo.HCC_CODIGO == codigo)
                                cuadroClinico = cuadroClinico + "\n" + catalogo.HCC_NOMBRE + " :" + detalle.ADE_DESCRIPCION;
                        }
                    }
                    txt_cuadro.Text = anamnesis.ANE_PROBLEMA + "\t\t" + cuadroClinico;
                    //for (int i = 0; i < evolucionAnalisis.Rows.Count; i++)
                    //{
                    //    string[] soloAnalisis = { evolucionAnalisis.Rows[i][0].ToString() };
                    //    int encontrar = 0;
                    //    string analisis = "";
                    //    foreach (var s in soloAnalisis)
                    //    {
                    //        encontrar = s.IndexOf("NOTA DE EVOLUCIÓN: ANÁLISIS");
                    //        if (encontrar < 0)
                    //            continue;
                    //        analisis = s.Substring(encontrar + 0);
                    //    }
                    //    string[] analisisfinal = { analisis };
                    //    int encontrar2 = 0;
                    //    string analisis2 = "";
                    //    foreach (var s in analisisfinal)
                    //    {
                    //        encontrar2 = s.IndexOf("NOTA DE EVOLUCIÓN: PLAN");
                    //        if (encontrar2 < 0)
                    //            continue;
                    //        analisis2 = s.Substring(encontrar2 + 0);
                    //    }
                    //    //agregaciones Edgar 20201220
                    //    string[] AnalisisNuevo = { evolucionAnalisis.Rows[i][0].ToString() };
                    //    int buscar = 0;
                    //    string analisis3 = "";
                    //    foreach (var s in AnalisisNuevo)
                    //    {
                    //        buscar = s.IndexOf("NOTA POS OPERATORIA:");
                    //        if (buscar != 0)
                    //            continue;
                    //        analisis3 = s.Substring(buscar + 0);
                    //    }
                    //    //-----------------------------------------------------
                    //    if (analisis2 != "")
                    //    {
                    //        analisis = analisis.Remove(analisis.IndexOf("NOTA DE EVOLUCIÓN: PLAN") + 1);
                    //        analisis = analisis.TrimEnd('N');                            
                    //    }
                    //    if (analisis3 != "")
                    //    {
                    //        if (analisis == "")
                    //        {
                    //            analisis3 = analisis3.Trim();
                    //            txt_evolucion.Text += evolucionAnalisis.Rows[i][1].ToString() + "\r\n" + analisis3 + "\r\n";
                    //        }
                    //        else
                    //        {
                    //            analisis3 = analisis3.Trim();
                    //            txt_evolucion.Text += "\n" + evolucionAnalisis.Rows[i][1].ToString() + "\r\n" + analisis3 + "\r\n";
                    //        }

                    //    }
                    //    if(analisis != "")
                    //    {
                    //        analisis = analisis.Trim();
                    //        txt_evolucion.Text += evolucionAnalisis.Rows[i][1].ToString() + "\r\n" + analisis + "\r\n";
                    //    }
                    //}
                    txt_tratamiento.Text = anamnesis.ANE_PLAN_TRATAMIENTO;
                    List<HC_ANAMNESIS_DIAGNOSTICOS> diag = NegAnamnesisDetalle.recuperarDiagnosticosAnamnesis(anamnesis.ANE_CODIGO);
                    if (diag != null)
                    {
                        foreach (HC_ANAMNESIS_DIAGNOSTICOS diagnosticos in diag)
                        {
                            DataGridViewRow fila = new DataGridViewRow();
                            DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                            DataGridViewTextBoxCell textcell1 = new DataGridViewTextBoxCell();
                            DataGridViewTextBoxCell textcell2 = new DataGridViewTextBoxCell();
                            DataGridViewCheckBoxCell chkcell = new DataGridViewCheckBoxCell();
                            DataGridViewCheckBoxCell chkcell2 = new DataGridViewCheckBoxCell();
                            //textcell.Value = diagnosticos.CDA_CODIGO;
                            textcell1.Value = diagnosticos.CIE_CODIGO;
                            textcell2.Value = diagnosticos.CDA_DESCRIPCION;
                            if (diagnosticos.CDA_ESTADO.Value)
                            {
                                chkcell.Value = true;
                                chkcell2.Value = false;
                            }
                            else
                            {
                                chkcell2.Value = true;
                                chkcell.Value = false;
                            }
                            //fila.Cells.Add(textcell);
                            fila.Cells.Add(textcell2);
                            fila.Cells.Add(textcell1);
                            fila.Cells.Add(chkcell);
                            fila.Cells.Add(chkcell2);
                            dtg_DIngreso.Rows.Add(fila);
                        }
                    }
                }
                if (Protocolo != null)
                {
                    foreach (DataRow item in Protocolo.Rows)
                    {
                        if (txt_hallazgos.Text.Trim() == "")
                        {
                            txt_hallazgos.Text = item[1].ToString() + "\r\n" + item[0].ToString();
                        }
                        else
                        {
                            txt_hallazgos.Text += "\r\n\n" + item[1].ToString() + "\r\n" + item[0].ToString();
                        }
                    }
                }
                if (evolucionAnalisis != null)
                {
                    for (int i = 0; i < evolucionAnalisis.Rows.Count; i++)
                    {
                        string[] soloAnalisis = { evolucionAnalisis.Rows[i][0].ToString() };
                        int encontrar = 0;
                        string analisis = "";
                        foreach (var s in soloAnalisis)
                        {
                            encontrar = s.IndexOf("NOTA DE EVOLUCIÓN: ANÁLISIS");
                            if (encontrar < 0)
                                continue;
                            analisis = s.Substring(encontrar + 0);
                        }
                        //agregaciones Edgar 20201220
                        string[] AnalisisNuevo = { evolucionAnalisis.Rows[i][0].ToString() };
                        int buscar = 0;
                        string analisis3 = "";
                        foreach (var s in AnalisisNuevo)
                        {
                            buscar = s.IndexOf("NOTA POS OPERATORIA:");
                            if (buscar != 0)
                                continue;
                            analisis3 = s.Substring(buscar + 0);
                        }
                        //-----------------------------------------------------
                        string[] analisisfinal = { analisis };
                        int encontrar2 = 0;
                        string analisis2 = "";
                        foreach (var s in analisisfinal)
                        {
                            encontrar2 = s.IndexOf("NOTA DE EVOLUCIÓN: PLAN");
                            if (encontrar2 < 0)
                                continue;
                            analisis2 = s.Substring(encontrar2 + 0);
                        }
                        //if (analisis2 != "")
                        //{
                        //    analisis = analisis.Remove(analisis.IndexOf("NOTA DE EVOLUCIÓN: PLAN") + 1);
                        //    analisis = analisis.TrimEnd('N');
                        //}
                        if (analisis3 != "")
                        {
                            if (analisis == "")
                            {
                                analisis3 = analisis3.Trim();
                                txt_evolucion.Text += evolucionAnalisis.Rows[i][1].ToString() + "\r\n" + analisis3 + "\r\n";
                            }
                            else
                            {
                                analisis3 = analisis3.Trim();
                                txt_evolucion.Text += "\n" + evolucionAnalisis.Rows[i][1].ToString() + "\r\n" + analisis3 + "\r\n";
                            }

                        }
                        if (analisis != "")
                        {
                            analisis = analisis.Trim();
                            txt_evolucion.Text += evolucionAnalisis.Rows[i][1].ToString() + "\r\n" + analisis + "\r\n";
                        }
                    }
                }
                modo = "SAVE";
                List<ESPECIALIDADES_MEDICAS> especialidad = NegEspecialidades.ListaEspecialidades();
                string esp = especialidad.FirstOrDefault(m => m.EntityKey == medico.ESPECIALIDADES_MEDICASReference.EntityKey).ESP_NOMBRE;
                //dtg_medicos.Rows[0].Cells[2].Value = esp;
                //dtg_medicos.Rows[0].Cells[3].Value = medico.MED_CODIGO;
                dtg_medicos.Rows.Add(medico.MED_RUC, medico.MED_APELLIDO_PATERNO.Trim() + " " + medico.MED_APELLIDO_MATERNO.Trim()
                    + " " + medico.MED_NOMBRE1.Trim() + " " + medico.MED_NOMBRE2.Trim(), esp, medico.MED_RUC, "", medico.MED_CODIGO); //cambios Edgar 20210120 daba problemas al querer agregar uno nuevo en medicos tratantes
                //modo = "SAVE";
            }
            else
            {
                btnNuevo.Enabled = false;
                btnGuardar.Enabled = false;
                btnImprimir.Enabled = true;
                btnModificar.Enabled = true;
                txt_cuadro.Text = epicrisis.EPI_CUADRO_CLINICO;
                txt_evolucion.Text = epicrisis.EPI_EVOLUCION;
                txt_hallazgos.Text = epicrisis.EPI_HALLAZGOS;
                txt_tratamiento.Text = epicrisis.EPI_TRATAMIENTO;
                txt_condiciones.Text = epicrisis.EPI_CONDICIONES_EGRESO;
                txtRealizadoPor.Text = epicrisis.EPI_REALIZADO;
                List<TIPO_EGRESO> tipo = NegTipoEgreso.ListaTipoIngreso();
                cmb_tipoEgreso.DataSource = tipo;
                cmb_tipoEgreso.SelectedItem = tipo.FirstOrDefault(t => t.EntityKey == epicrisis.TIPO_EGRESOReference.EntityKey);
                dtp_fechaEpicrisis.Value = epicrisis.EPI_FECHA_EGRESO;
                cargarEpicrisisDiagnosticos(epicrisis.EPI_CODIGO);
                modo = "UPDATE";
            }
        }

        private void actualizarDiagnosticos(int codEpicrisis)
        {
            //INGRESOS
            List<HC_EPICRISIS_DIAGNOSTICO> diagnosticosIngreso = NegEpicrisis.recuperarDiagnosticosEpicrisisIngreso(codEpicrisis);
            if (diagnosticosIngreso != null)
            {
                int cont = 0;
                foreach (HC_EPICRISIS_DIAGNOSTICO diag in diagnosticosIngreso)
                {
                    if (diag.HED_TIPO.Equals("I"))
                    {
                        DataGridViewRow fila = dtg_DIngreso.Rows[cont];

                        if (fila.Cells[1].Value != null)
                            diag.CIE_CODIGO = fila.Cells[1].Value.ToString();
                        else
                            diag.CIE_CODIGO = "";
                        if ((bool)fila.Cells[2].Value)
                            diag.HED_ESTADO = true;
                        else
                            diag.HED_ESTADO = false;
                        diag.HED_DESCRIPCION = fila.Cells[0].Value.ToString();
                        diag.HC_EPICRISISReference.EntityKey = epicrisis.EntityKey;
                        diag.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                        diag.HED_TIPO = "I";
                        NegEpicrisis.ActualizarDiagnostico(diag);
                    }
                    cont++;
                }
            }

            //EGRESO
            List<HC_EPICRISIS_DIAGNOSTICO> diagnosticosEgreso = NegEpicrisis.recuperarDiagnosticosEpicrisisEgreso(codEpicrisis);
            if (diagnosticosIngreso != null)
            {
                int cont = 0;
                foreach (HC_EPICRISIS_DIAGNOSTICO diag in diagnosticosEgreso)
                {
                    if (diag.HED_TIPO.Equals("E"))
                    {
                        DataGridViewRow fila = dtg_DEgresos.Rows[cont];
                        if (fila.Cells[1].Value != null)
                            diag.CIE_CODIGO = fila.Cells[1].Value.ToString();
                        else
                            diag.CIE_CODIGO = "";
                        if ((bool)fila.Cells[2].Value)
                            diag.HED_ESTADO = true;
                        else
                            diag.HED_ESTADO = false;
                        diag.HED_DESCRIPCION = fila.Cells[0].Value.ToString();
                        diag.HC_EPICRISISReference.EntityKey = epicrisis.EntityKey;
                        diag.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                        diag.HED_TIPO = "E";
                        NegEpicrisis.ActualizarDiagnostico(diag);
                    }
                    cont++;
                }
            }

            //MEDICOS
            List<HC_EPICRISIS_MEDICOS> listaMedicos = NegEpicrisis.recuperarMedicosEpicrisis(codEpicrisis);
            if (listaMedicos.Count != 0)
            {
                int cont = 0;
                HC_EPICRISIS_MEDICOS med = new HC_EPICRISIS_MEDICOS();
                foreach (DataGridViewRow fila in dtg_medicos.Rows)
                {
                    if (fila.Cells[1].Value != null)
                    {
                        if (fila.Cells[0].Value != null)
                        {
                            med = listaMedicos.Where(m => m.MED_CODIGO == Convert.ToInt32(fila.Cells[5].Value.ToString())).
                                    FirstOrDefault();
                            if (med != null)
                            {
                                if (med.MED_CODIGO == Convert.ToInt32(fila.Cells[5].Value.ToString()))
                                {
                                    med.MED_NOMBRE = fila.Cells[1].Value.ToString();
                                    med.MED_CODIGO = Convert.ToInt32(fila.Cells[5].Value);
                                    med.MED_PERIODO_RESP = fila.Cells["resp_medico"].Value.ToString();
                                    NegEpicrisis.ActualizarMedicos(med);
                                }
                            }
                            else
                            {
                                med = new HC_EPICRISIS_MEDICOS();
                                med.EPM_CODIGO = NegEpicrisis.ultimoCodigoMedico() + 1;
                                med.HC_EPICRISISReference.EntityKey = epicrisis.EntityKey;
                                med.MED_NOMBRE = fila.Cells[1].Value.ToString();
                                med.MED_CODIGO = Convert.ToInt32(fila.Cells[5].Value);
                                med.MED_PERIODO_RESP = fila.Cells["resp_medico"].Value.ToString();
                                NegEpicrisis.crearEpicrisisMedicos(med);
                            }
                        }
                    }
                    cont++;
                }
                //foreach (HC_EPICRISIS_MEDICOS med in listaMedicos)
                //{
                //    DataGridViewRow filaM = dtg_medicos.Rows[cont];
                //    if (filaM.Cells[0].Value != null)
                //    {
                //        med.MED_NOMBRE = filaM.Cells[2].Value.ToString();
                //        med.MED_CODIGO = Convert.ToInt32(filaM.Cells[3].Value);
                //        med.MED_PERIODO_RESP = filaM.Cells[4].Value.ToString();
                //        NegEpicrisis.ActualizarMedicos(med);
                //    }else
                //    {
                //        med.EPM_CODIGO = NegEpicrisis.ultimoCodigoMedico()+1;
                //        med.HC_EPICRISISReference.EntityKey = epicrisis.EntityKey;
                //        med.MED_NOMBRE = filaM.Cells[2].Value.ToString();
                //        med.MED_CODIGO = Convert.ToInt32(filaM.Cells[3].Value);
                //        med.MED_PERIODO_RESP = filaM.Cells[4].Value.ToString();
                //        NegEpicrisis.crearEpicrisisMedicos(med);
                //    }

                //    cont++;}
            }
            else
            {
                GuardarMedicosEpicrisis();
            }


        }

        private void cargarEpicrisisDiagnosticos(int codEpicrisis)
        {
            dtg_DIngreso.DataSource = null;
            List<HC_EPICRISIS_DIAGNOSTICO> diagnosticosIngreso = NegEpicrisis.recuperarDiagnosticosEpicrisisIngreso(codEpicrisis);
            if (diagnosticosIngreso != null)
            {
                foreach (HC_EPICRISIS_DIAGNOSTICO diag in diagnosticosIngreso)
                {
                    DataGridViewRow fila = new DataGridViewRow();
                    DataGridViewTextBoxCell txtcell = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell txtcell2 = new DataGridViewTextBoxCell();
                    txtcell.Value = diag.HED_DESCRIPCION;
                    txtcell2.Value = diag.CIE_CODIGO;
                    fila.Cells.Add(txtcell);
                    fila.Cells.Add(txtcell2);
                    DataGridViewCheckBoxCell c1 = new DataGridViewCheckBoxCell();
                    DataGridViewCheckBoxCell c2 = new DataGridViewCheckBoxCell();
                    if (diag.HED_ESTADO.Value)
                    {
                        c1.Value = true;
                        c2.Value = false;
                    }
                    else
                    {
                        c1.Value = false;
                        c2.Value = true;
                    }
                    fila.Cells.Add(c1);
                    fila.Cells.Add(c2);
                    dtg_DIngreso.Rows.Add(fila);
                    index++;
                }
            }
            dtg_DEgresos.DataSource = null;
            List<HC_EPICRISIS_DIAGNOSTICO> diagnosticosEgreso = NegEpicrisis.recuperarDiagnosticosEpicrisisEgreso(codEpicrisis);
            if (diagnosticosEgreso != null)
            {
                foreach (HC_EPICRISIS_DIAGNOSTICO diag in diagnosticosEgreso)
                {
                    DataGridViewRow fila = new DataGridViewRow();
                    DataGridViewTextBoxCell txtcell = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell txtcell2 = new DataGridViewTextBoxCell();
                    txtcell.Value = diag.HED_DESCRIPCION;
                    txtcell2.Value = diag.CIE_CODIGO;
                    fila.Cells.Add(txtcell);
                    fila.Cells.Add(txtcell2);
                    DataGridViewCheckBoxCell c1 = new DataGridViewCheckBoxCell();
                    DataGridViewCheckBoxCell c2 = new DataGridViewCheckBoxCell();
                    if (diag.HED_ESTADO.Value)
                    {
                        c1.Value = true;
                        c2.Value = false;
                    }
                    else
                    {
                        c1.Value = false;
                        c2.Value = true;
                    }
                    fila.Cells.Add(c1);
                    fila.Cells.Add(c2);
                    dtg_DEgresos.Rows.Add(fila);
                    index2++;
                }
            }
            index3 = 0;
            dtg_medicos.DataSource = null;
            List<HC_EPICRISIS_MEDICOS> listaMedicos = NegEpicrisis.recuperarMedicosEpicrisis(epicrisis.EPI_CODIGO);
            if (listaMedicos != null)
            {
                foreach (HC_EPICRISIS_MEDICOS med in listaMedicos)
                {
                    DataGridViewRow filaMedico = new DataGridViewRow();
                    DataGridViewTextBoxCell txtcel0 = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell txtcell = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell txtcell2 = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell txtcell3 = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell txtcell4 = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell txtcell5 = new DataGridViewTextBoxCell();
                    txtcel0.Value = med.EPM_CODIGO;
                    txtcell.Value = med.MED_NOMBRE;
                    MEDICOS actual = NegMedicos.MedicoID((Int32)med.MED_CODIGO);
                    medico = NegMedicos.RecuperaMedicoId(Convert.ToInt32(med.MED_CODIGO));
                    txtcell2.Value = NegEspecialidades.ListaEspecialidades().FirstOrDefault(m => m.EntityKey == actual.ESPECIALIDADES_MEDICASReference.EntityKey).ESP_NOMBRE;
                    txtcell3.Value = medico.MED_RUC;
                    txtcell4.Value = med.MED_PERIODO_RESP;
                    txtcell5.Value = med.MED_CODIGO;
                    filaMedico.Cells.Add(txtcel0);
                    filaMedico.Cells.Add(txtcell);
                    filaMedico.Cells.Add(txtcell2);
                    filaMedico.Cells.Add(txtcell3);
                    filaMedico.Cells.Add(txtcell4);
                    filaMedico.Cells.Add(txtcell5);
                    dtg_medicos.Rows.Add(filaMedico);
                    index3++;
                }
            }
        }

        private void cargarMedico(int cod)
        {
            //medico = NegMedicos.RecuperaMedicoId(cod);  
            medico = NegMedicos.recuperarMedico(cod);
            txt_profesional.Text = medico.MED_APELLIDO_PATERNO.Trim() + " " +
                medico.MED_APELLIDO_MATERNO.Trim() + " " +
                medico.MED_NOMBRE1.Trim() + " " + medico.MED_NOMBRE2.Trim();
            lbl_medico.Text = txt_profesional.Text;
            med_codigo = medico.MED_RUC;

        }

        private void agregarMedico(MEDICOS medicoTratante)
        {
            string codmedAdmision = "";
            if ((medicoTratante != null))
            {
                //int fila = 0;
                //if (dtg_medicos.CurrentRow != null)
                //{
                //    fila = dtg_medicos.CurrentRow.Index;
                //}
                if (dtg_medicos.Rows.Count - 1 <= 3)
                {
                    if (dtg_medicos.Rows[0].Cells[0].Value != null)
                    {
                        codmedAdmision = dtg_medicos.Rows[0].Cells[0].Value.ToString(); //el codigo del medico agregado desde admision, sera el EPM_Codigo
                    }
                    else
                    {
                        codmedAdmision = "0";
                    }
                    //dtg_medicos.Rows[fila].Cells[1].Value = medicoTratante.MED_APELLIDO_PATERNO.Trim() + " " + medicoTratante.MED_APELLIDO_MATERNO.Trim()
                    //    + " " + medicoTratante.MED_NOMBRE1.Trim() + " " + medicoTratante.MED_NOMBRE2.Trim();
                    List<ESPECIALIDADES_MEDICAS> especialidad = NegEspecialidades.ListaEspecialidades();
                    string esp = especialidad.FirstOrDefault(m => m.EntityKey == medicoTratante.ESPECIALIDADES_MEDICASReference.EntityKey).ESP_NOMBRE;
                    //dtg_medicos.Rows[fila].Cells[2].Value = esp;
                    //dtg_medicos.Rows[fila].Cells[3].Value = medicoTratante.MED_CODIGO;
                    dtg_medicos.Rows.Add(codmedAdmision, medicoTratante.MED_APELLIDO_PATERNO.Trim() + " " + medicoTratante.MED_APELLIDO_MATERNO.Trim()
                        + " " + medicoTratante.MED_NOMBRE1.Trim() + " " + medicoTratante.MED_NOMBRE2.Trim(), esp, medicoTratante.MED_RUC, "", medicoTratante.MED_CODIGO);
                }
                else
                    MessageBox.Show("No se puede agregar mas de 4 medicos tratantes.", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private void cargarPaciente(int codPac)
        {
            paciente = NegPacientes.RecuperarPacienteID(codPac);

            if (paciente != null)
            {
                string passs = paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO + " " + paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2;
                lblPaciente.Text = passs.ToString();
                txt_pacHCL.Text = paciente.PAC_HISTORIA_CLINICA;
                txt_sexo.Text = paciente.PAC_GENERO;
            }
            else
            {
                txt_pacHCL.Text = string.Empty;
                lblPaciente.Text = string.Empty;
                txt_sexo.Text = string.Empty;
            }
            txtRealizadoPor.Text = His.Entidades.Clases.Sesion.nomUsuario;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!validarFormulario())
            {
                try
                {
                    if (modo == "SAVE")
                        epicrisis = new HC_EPICRISIS();
                    else
                        modo = "UPDATE";
                    epicrisis.EPI_CUADRO_CLINICO = txt_cuadro.Text;
                    epicrisis.EPI_EVOLUCION = txt_evolucion.Text;
                    epicrisis.EPI_HALLAZGOS = txt_hallazgos.Text;
                    epicrisis.EPI_TRATAMIENTO = txt_tratamiento.Text;
                    epicrisis.EPI_CONDICIONES_EGRESO = txt_condiciones.Text;
                    epicrisis.EPI_FECHA_CREACION = Convert.ToDateTime(txt_fecha.Text);
                    epicrisis.EPI_FECHA_EGRESO = dtp_fechaEpicrisis.Value;

                    //epicrisis.EPI_FECHA_MODIFICACION = DateTime.Now; ' para que al momento de ingresar una epicrisis la fecha de ingreso y modificacion sea las mismas y luego al modificar se muestra la fecha modificada/Giovanny Tapia / 18/09/2012
                    epicrisis.EPI_FECHA_MODIFICACION = Convert.ToDateTime(txt_fecha.Text + " " + txt_hora.Text); // para que al momento de ingresar una epicrisis la fecha de ingreso y modificacion sea las mismas y luego al modificar se muestra la fecha modificada/Giovanny Tapia / 18/09/2012

                    epicrisis.ATENCIONESReference.EntityKey = atencion.EntityKey;
                    epicrisis.PAC_CODIGO = paciente.PAC_CODIGO;
                    epicrisis.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                    epicrisis.EPI_REALIZADO = txtRealizadoPor.Text;
                    TIPO_EGRESO tip = (TIPO_EGRESO)cmb_tipoEgreso.SelectedItem;
                    epicrisis.TIPO_EGRESOReference.EntityKey = tip.EntityKey;

                    if (modo == "SAVE")
                    {
                        epicrisis.EPI_CODIGO = NegEpicrisis.ultimoCodigo() + 1;
                        NegEpicrisis.crearEpicrisis(epicrisis);
                        guardarDiagnosticos();
                        GuardarMedicosEpicrisis();
                        MessageBox.Show("Registro Guardado", "EPICRISIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        if (modo == "UPDATE")
                    {
                        NegEpicrisis.ActualizarEpicrisis(epicrisis);
                        actualizarDiagnosticos(epicrisis.EPI_CODIGO);
                        MessageBox.Show("REGISTRO ACTUALIZADO");
                        //cargarMedicosTratantes();
                    }
                    guadarDatosAtencion(atencion);
                    btnGuardar.Enabled = false;
                    btnImprimir.Enabled = true;
                    imprimirReporte("pdf");
                    btnImprimir.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Ingrese todos los campos", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guadarDatosAtencion(ATENCIONES atencion)
        {
            //atencion.ATE_FECHA_ALTA = dtp_fechaEpicrisis.Value;
            NegAtenciones.actualizarAtencion(atencion);
        }


        /// <summary>
        /// Guardar Datos de Diagnótico de Ingreso y Egreso
        /// </summary>
        private void guardarDiagnosticos()
        {
            //INGRESOS
            foreach (DataGridViewRow fila in dtg_DIngreso.Rows)
            {
                if (fila.Cells[1].RowIndex < dtg_DIngreso.Rows.Count - 1)
                {
                    HC_EPICRISIS_DIAGNOSTICO detalle = new HC_EPICRISIS_DIAGNOSTICO();
                    if (fila.Cells[1].Value != null)
                        detalle.CIE_CODIGO = fila.Cells[1].Value.ToString();
                    else
                        detalle.CIE_CODIGO = "";
                    if ((bool)fila.Cells[2].Value)
                        detalle.HED_ESTADO = true;
                    else
                        detalle.HED_ESTADO = false;
                    detalle.HED_DESCRIPCION = fila.Cells[0].Value.ToString();
                    detalle.HED_CODIGO = NegEpicrisis.ultimoCodigoDiagnostico() + 1;
                    detalle.HC_EPICRISISReference.EntityKey = epicrisis.EntityKey;
                    detalle.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                    detalle.HED_TIPO = "I";
                    NegEpicrisis.crearEpicrisisDiagnosticos(detalle);
                }
            }

            //EGRESOS
            foreach (DataGridViewRow fila in dtg_DEgresos.Rows)
            {
                if (fila.Cells[1].RowIndex < dtg_DEgresos.Rows.Count - 1)
                {
                    HC_EPICRISIS_DIAGNOSTICO detalle = new HC_EPICRISIS_DIAGNOSTICO();
                    if (fila.Cells[1].Value != null)
                        detalle.CIE_CODIGO = fila.Cells[1].Value.ToString();
                    else
                        detalle.CIE_CODIGO = "";
                    if ((bool)fila.Cells[2].Value)
                        detalle.HED_ESTADO = true;
                    else
                        detalle.HED_ESTADO = false;
                    detalle.HED_DESCRIPCION = fila.Cells[0].Value.ToString();
                    detalle.HED_CODIGO = NegEpicrisis.ultimoCodigoDiagnostico() + 1;
                    detalle.HC_EPICRISISReference.EntityKey = epicrisis.EntityKey;
                    //detalle.EPI_CODIGO = epicrisis.EPI_CODIGO;
                    detalle.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                    detalle.HED_TIPO = "E";
                    NegEpicrisis.crearEpicrisisDiagnosticos(detalle);
                }
            }
        }
        int index2 = 0;

        public void valida1()
        {
            if ((diagnostico != "") && (diagnostico != null))
            {
                if (index != dtg_DIngreso.Rows.Count)
                {
                    DataGridViewTextBoxCell txtcell = (DataGridViewTextBoxCell)this.dtg_DIngreso.Rows[index].Cells[0];
                    DataGridViewTextBoxCell txtcell2 = (DataGridViewTextBoxCell)this.dtg_DIngreso.Rows[index].Cells[1];
                    if (diagnostico != null)
                    {
                        txtcell.Value = diagnostico;
                        txtcell2.Value = codigoCIE;
                        diagnostico = "";
                    }
                    index++;

                }
                else
                {
                    MessageBox.Show("Marque CIE10 como presuntivo o definitivo", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }

        public void valida2()
        {
            if (index2 != dtg_DEgresos.Rows.Count)
            {
                if ((diagnostico2 != "") && (diagnostico2 != null))
                {
                    DataGridViewTextBoxCell txtcell = (DataGridViewTextBoxCell)this.dtg_DEgresos.Rows[index2].Cells[0];
                    DataGridViewTextBoxCell txtcell2 = (DataGridViewTextBoxCell)this.dtg_DEgresos.Rows[index2].Cells[1];
                    if (diagnostico2 != null)
                    {
                        txtcell.Value = diagnostico2;
                        txtcell2.Value = codigoCIE2;
                        diagnostico2 = "";
                    }
                    index2++;
                }
            }
            else
            {
                MessageBox.Show("Marque CIE10 como presuntivo o definitivo", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            //try
            //{


            //    //if ((medicoTratante != null))
            //    //{
            //    //    DataGridViewTextBoxCell txtcel = (DataGridViewTextBoxCell)this.dtg_medicos.Rows[index3].Cells[0];
            //    //    DataGridViewTextBoxCell txtcell = (DataGridViewTextBoxCell)this.dtg_medicos.Rows[index3].Cells[1];
            //    //    DataGridViewTextBoxCell txtcell2 = (DataGridViewTextBoxCell)this.dtg_medicos.Rows[index3].Cells[2];
            //    //    DataGridViewTextBoxCell txtcell3 = (DataGridViewTextBoxCell)this.dtg_medicos.Rows[index3].Cells[3];
            //    //    DataGridViewTextBoxCell txtcell4 = (DataGridViewTextBoxCell)this.dtg_medicos.Rows[index3].Cells[4];
            //    //    txtcel.Value = null;
            //    //    txtcell.Value = medicoTratante.MED_APELLIDO_PATERNO.Trim() + " " + medicoTratante.MED_APELLIDO_MATERNO.Trim()
            //    //        + " " + medicoTratante.MED_NOMBRE1.Trim() + " " + medicoTratante.MED_NOMBRE2.Trim();
            //    //    List<ESPECIALIDADES_MEDICAS> especialidad = NegEspecialidades.ListaEspecialidades();
            //    //    string esp = especialidad.FirstOrDefault(m => m.EntityKey == medicoTratante.ESPECIALIDADES_MEDICASReference.EntityKey).ESP_NOMBRE;
            //    //    txtcell2.Value = esp;
            //    //    txtcell3.Value = medicoTratante.MED_CODIGO;
            //    //    //txtcell4.Value = "1";
            //    //    medicoTratante = null;
            //    //    index3++;
            //    //}
            //}
            //catch
            //{
            //    if (aux == 1)
            //    {
            //        aux = 0;
            //        MessageBox.Show("Marque CIE10 como presuntivo o definitivo", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return;
            //    }
            //    else
            //        return;
            //}
        }

        private void GuardarMedicosEpicrisis()
        {
            //MEDICOS
            //if(!accion)
            //{
            //    HC_EPICRISIS_MEDICOS detalleM = new HC_EPICRISIS_MEDICOS();
            //    detalleM.EPM_CODIGO = NegEpicrisis.ultimoCodigoMedico() + 1;
            //    detalleM.HC_EPICRISISReference.EntityKey = epicrisis.EntityKey;
            //    medico = NegMedicos.RecuperaMedicoId(codigoMedico);
            //    detalleM.MED_CODIGO = medico.MED_CODIGO;
            //    detalleM.MED_NOMBRE = medico.MED_APELLIDO_PATERNO.Trim() + " " + medico.MED_APELLIDO_MATERNO.Trim() + " " + medico.MED_NOMBRE1.Trim() + " " + medico.MED_NOMBRE2.Trim();
            //    detalleM.MED_PERIODO_RESP = Convert.ToString((DateTime.Now.Day - Convert.ToDateTime(atencion.ATE_FECHA).Day));
            //    NegEpicrisis.crearEpicrisisMedicos(detalleM);
            //}
            foreach (DataGridViewRow fila in dtg_medicos.Rows)
            {
                if (fila.Cells[1].RowIndex < dtg_medicos.Rows.Count - 1)
                {
                    medicoTratante = NegMedicos.RecuperaMedicoId(Convert.ToInt32(fila.Cells[5].Value.ToString()));
                    if (medicoTratante != null)
                    {
                        HC_EPICRISIS_MEDICOS detalle = new HC_EPICRISIS_MEDICOS();
                        detalle.EPM_CODIGO = NegEpicrisis.ultimoCodigoMedico() + 1;
                        detalle.HC_EPICRISISReference.EntityKey = epicrisis.EntityKey;
                        detalle.MED_CODIGO = medicoTratante.MED_CODIGO;
                        detalle.MED_NOMBRE = medicoTratante.MED_APELLIDO_PATERNO.Trim() + " " + medico.MED_APELLIDO_MATERNO.Trim() + " " + medico.MED_NOMBRE1.Trim() + " " + medico.MED_NOMBRE2.Trim();
                        if (fila.Cells["resp_medico"].Value != null)
                            detalle.MED_PERIODO_RESP = fila.Cells["resp_medico"].Value.ToString();
                        else
                            detalle.MED_PERIODO_RESP = " ";
                        NegEpicrisis.crearEpicrisisMedicos(detalle);
                    }
                }
            }
        }

        private void dtg_DIngreso_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            index--;
        }

        private void dtg_DEgresos_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            index2--;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            imprimirReporte("reporte");
        }
        public string med_codigo, med_nombre;
        private void imprimirReporte(string accion)
        {
            try
            {
                ////recupero la informacion para llenar el informe Epicrisis 2
                //recupero la informacion para llenar el informe Epicrisis 2
                ReporteEpicrisis2 reporte = new ReporteEpicrisis2();
                NegCertificadoMedico m = new NegCertificadoMedico();
                reporte.forEstablec = Sesion.nomEmpresa;
                reporte.logo = m.path();
                reporte.forNombres = (paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2).Replace("'", "´");
                reporte.forApellidos = (paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO).Replace("'", "´");
                if (txt_sexo.Text == "M")
                    reporte.forSexo = "M";
                else
                    reporte.forSexo = "F";
                //reporte.forSexo = txt_sexo.Text;
                reporte.forHistoria = paciente.PAC_HISTORIA_CLINICA;
                reporte.forResumenCadro = txt_cuadro.Text;

                reporte.forEvolucion = txt_evolucion.Text;

                reporte.forHallazgo = txt_hallazgos.Text;

                reporte.ForResumenTra = txt_tratamiento.Text;
                string input = txt_tratamiento.Text;

                int size = input.Length;
                if (input.Length > 65534)
                {
                    string trat = input.Substring(0, 65534);
                    string trat1 = input.Substring(trat.Length, size - trat.Length);
                    reporte.ForResumenTra1 = trat1;
                }

                //reporte.ForResumenTra = txt_tratamiento.Text;
                //LLena los campos de Diagnóstico CIE10
                for (int i = 0; i < dtg_DIngreso.Rows.Count - 1; i++)
                {
                    if (i == 0)
                    {
                        reporte.ForDiagIngresoUnoDesc = (dtg_DIngreso.Rows[0].Cells[0].Value.ToString()).Replace("'", "´");
                        reporte.ForDiagIngresoUnoCie = dtg_DIngreso.Rows[0].Cells[1].Value.ToString();
                        if ((bool)dtg_DIngreso.Rows[i].Cells[2].Value)
                            reporte.ForDiagIngresoUnoPre = dtg_DIngreso.Rows[0].Cells[2].Value != null ? "X" : " ";
                        else
                            reporte.ForDiagIngresoUnoDef = dtg_DIngreso.Rows[0].Cells[3].Value != null ? "X" : " ";
                    }
                    if (i == 1)
                    {
                        reporte.ForDiagIngresoDosDesc = (dtg_DIngreso.Rows[i].Cells[0].Value.ToString()).Replace("'", "´");
                        reporte.ForDiagIngresoDosCie = dtg_DIngreso.Rows[i].Cells[1].Value.ToString();
                        if ((bool)dtg_DIngreso.Rows[i].Cells[2].Value)
                            reporte.ForDiagIngresoDosPre = dtg_DIngreso.Rows[i].Cells[2].Value != null ? "X" : " ";
                        else
                            reporte.ForDiagIngresoDosDef = dtg_DIngreso.Rows[i].Cells[3].Value != null ? "X" : " ";
                    }
                    if (i == 2)
                    {
                        reporte.ForDiagIngresoTresDesc = (dtg_DIngreso.Rows[i].Cells[0].Value.ToString()).Replace("'", "´");
                        reporte.ForDiagIngresoTresCie = dtg_DIngreso.Rows[i].Cells[1].Value.ToString();
                        if ((bool)dtg_DIngreso.Rows[i].Cells[2].Value)
                            reporte.ForDiagIngresoTresPre = dtg_DIngreso.Rows[i].Cells[2].Value != null ? "X" : " ";
                        else
                            reporte.ForDiagIngresoTresDef = dtg_DIngreso.Rows[i].Cells[3].Value != null ? "X" : " ";
                    }
                    if (i == 3)
                    {
                        reporte.ForDiagIngresoCuatroDesc = (dtg_DIngreso.Rows[i].Cells[0].Value.ToString()).Replace("'", "´");
                        reporte.ForDiagIngresoCuatroCie = dtg_DIngreso.Rows[i].Cells[1].Value.ToString();
                        if ((bool)dtg_DIngreso.Rows[i].Cells[2].Value)
                            reporte.ForDiagIngresoCuatroPre = dtg_DIngreso.Rows[i].Cells[2].Value != null ? "X" : " ";
                        else
                            reporte.ForDiagIngresoCuatroDef = dtg_DIngreso.Rows[i].Cells[3].Value != null ? "X" : " ";
                    }
                    if (i == 4)
                    {
                        reporte.ForDiagIngresoCincoDesc = (dtg_DIngreso.Rows[i].Cells[0].Value.ToString()).Replace("'", "´");
                        reporte.ForDiagIngresoCincoCie = dtg_DIngreso.Rows[i].Cells[1].Value.ToString();
                        if ((bool)dtg_DIngreso.Rows[i].Cells[2].Value)
                            reporte.ForDiagIngresoCincoPre = dtg_DIngreso.Rows[i].Cells[2].Value != null ? "X" : " ";
                        else
                            reporte.ForDiagIngresoCincoDef = dtg_DIngreso.Rows[i].Cells[3].Value != null ? "X" : " ";
                    }
                    if (i == 5)
                    {
                        reporte.ForDiagIngresoSeisDesc = (dtg_DIngreso.Rows[i].Cells[0].Value.ToString()).Replace("'", "´");
                        reporte.ForDiagIngresoSeisCie = dtg_DIngreso.Rows[i].Cells[1].Value.ToString();
                        if ((bool)dtg_DIngreso.Rows[i].Cells[2].Value)
                            reporte.ForDiagIngresoSeisPre = dtg_DIngreso.Rows[i].Cells[2].Value != null ? "X" : " ";
                        else
                            reporte.ForDiagIngresoSeisDef = dtg_DIngreso.Rows[i].Cells[3].Value != null ? "X" : " ";
                    }
                }
                for (int i = 0; i < dtg_DEgresos.Rows.Count - 1; i++)
                {
                    if (i == 0)
                    {
                        reporte.ForDiagEgresoUnoDesc = (dtg_DEgresos.Rows[0].Cells[0].Value.ToString()).Replace("'", "´");
                        reporte.ForDiagEgresoUnoCie = dtg_DEgresos.Rows[0].Cells[1].Value.ToString();
                        if ((bool)dtg_DEgresos.Rows[i].Cells[2].Value)
                            reporte.ForDiagEgresoUnoPre = dtg_DEgresos.Rows[0].Cells[2].Value != null ? "X" : " ";
                        else
                            reporte.ForDiagEgresoUnoDef = dtg_DEgresos.Rows[0].Cells[3].Value != null ? "X" : " ";
                    }
                    if (i == 1)
                    {
                        reporte.ForDiagEgresoDosDesc = (dtg_DEgresos.Rows[i].Cells[0].Value.ToString()).Replace("'", "´");
                        reporte.ForDiagEgresoDosCie = dtg_DEgresos.Rows[i].Cells[1].Value.ToString();
                        if ((bool)dtg_DEgresos.Rows[i].Cells[2].Value)
                            reporte.ForDiagEgresoDosPre = dtg_DEgresos.Rows[i].Cells[2].Value != null ? "X" : " ";
                        else
                            reporte.ForDiagEgresoDosDef = dtg_DEgresos.Rows[i].Cells[3].Value != null ? "X" : " ";
                    }
                    if (i == 2)
                    {
                        reporte.ForDiagEgresoTresDesc = (dtg_DEgresos.Rows[i].Cells[0].Value.ToString()).Replace("'", "´");
                        reporte.ForDiagEgresoTresCie = dtg_DEgresos.Rows[i].Cells[1].Value.ToString();
                        if ((bool)dtg_DEgresos.Rows[i].Cells[2].Value)
                            reporte.ForDiagEgresoTresPre = dtg_DEgresos.Rows[i].Cells[2].Value != null ? "X" : " ";
                        else
                            reporte.ForDiagEgresoTresDef = dtg_DEgresos.Rows[i].Cells[3].Value != null ? "X" : " ";
                    }
                    if (i == 3)
                    {
                        reporte.ForDiagEgresoCuatroDesc = (dtg_DEgresos.Rows[i].Cells[0].Value.ToString()).Replace("'", "´");
                        reporte.ForDiagEgresoCuatroCie = dtg_DEgresos.Rows[i].Cells[1].Value.ToString();
                        if ((bool)dtg_DEgresos.Rows[i].Cells[2].Value)
                            reporte.ForDiagEgresoCuatroPre = dtg_DEgresos.Rows[i].Cells[2].Value != null ? "X" : " ";
                        else
                            reporte.ForDiagEgresoCuatroDef = dtg_DEgresos.Rows[i].Cells[3].Value != null ? "X" : " ";
                    }
                    if (i == 4)
                    {
                        reporte.ForDiagEgresoCincoDesc = (dtg_DEgresos.Rows[i].Cells[0].Value.ToString()).Replace("'", "´");
                        reporte.ForDiagEgresoCincoCie = dtg_DEgresos.Rows[i].Cells[1].Value.ToString();
                        if ((bool)dtg_DEgresos.Rows[i].Cells[2].Value)
                            reporte.ForDiagEgresoCincoPre = dtg_DEgresos.Rows[i].Cells[2].Value != null ? "X" : " ";
                        else
                            reporte.ForDiagEgresoCincoDef = dtg_DEgresos.Rows[i].Cells[3].Value != null ? "X" : " ";
                    }
                    if (i == 5)
                    {
                        reporte.ForDiagEgresoSeisDesc = (dtg_DEgresos.Rows[i].Cells[0].Value.ToString()).Replace("'", "´");
                        reporte.ForDiagEgresoSeisCie = dtg_DEgresos.Rows[i].Cells[1].Value.ToString();
                        if ((bool)dtg_DEgresos.Rows[i].Cells[2].Value)
                            reporte.ForDiagEgresoSeisPre = dtg_DEgresos.Rows[i].Cells[2].Value != null ? "X" : " ";
                        else
                            reporte.ForDiagEgresoSeisDef = dtg_DEgresos.Rows[i].Cells[3].Value != null ? "X" : " ";
                    }
                }
                reporte.ForCondEgresoPron = txt_condiciones.Text;

                string medicoProfesional = ""; // almacena el numero EPM_CODIGO de la tabla HC_EPICRISIS_MEDICOS
                string codmedicoProfesional = "";

                for (int i = 0; i < dtg_medicos.Rows.Count - 1; i++)
                {
                    if (i == 0)
                    {
                        reporte.ForMedicoTratUnoNom = (dtg_medicos.Rows[i].Cells[1].Value.ToString()).Replace("'", "´");
                        reporte.ForMedicoTratUnoEspec = (dtg_medicos.Rows[i].Cells[2].Value.ToString()).Replace("'", "´");
                        if (dtg_medicos.Rows[i].Cells[3].Value != null)
                            reporte.ForMedicoTratUnoCodigo = dtg_medicos.Rows[i].Cells[3].Value.ToString();
                        else
                            reporte.ForMedicoTratUnoCodigo = dtg_medicos.Rows[i].Cells[3].Value.ToString();
                        if ((dtg_medicos.Rows[i].Cells["resp_medico"].Value != string.Empty) && (dtg_medicos.Rows[i].Cells["resp_medico"].Value != null))
                        {
                            reporte.ForMedicoTratUnoRespo = dtg_medicos.Rows[i].Cells["resp_medico"].Value.ToString();

                            if ((dtg_medicos.Rows[i].Cells[0].Value) != null) // Verifica que el campo EPM_CODIGO de la tabla HC_EPICRISIS_MEDICOS no sea NULL / Giovanny Tapia / 18/09/2012
                            {
                                medicoProfesional = dtg_medicos.Rows[i].Cells[1].Value.ToString();
                                if (dtg_medicos.Rows[i].Cells[3].Value != null)
                                    codmedicoProfesional = dtg_medicos.Rows[i].Cells[3].Value.ToString();
                                else
                                    codmedicoProfesional = dtg_medicos.Rows[i].Cells[3].Value.ToString();
                            }
                            else

                            {
                                medicoProfesional = NegEpicrisis.recuperarMedicosEpicrisisCodigo(epicrisis.EPI_CODIGO).EPM_CODIGO.ToString();
                            }
                        }
                        else
                            reporte.ForMedicoTratUnoRespo = " ";
                    }
                    if (i == 1)
                    {
                        reporte.ForMedicoTratDosNom = (dtg_medicos.Rows[i].Cells[1].Value.ToString()).Replace("'", "´");
                        reporte.ForMedicoTratDosEspec = (dtg_medicos.Rows[i].Cells[2].Value.ToString()).Replace("'", "´");
                        if (dtg_medicos.Rows[i].Cells[3].Value != null)
                            reporte.ForMedicoTratUnoCodigo = dtg_medicos.Rows[i].Cells[3].Value.ToString();
                        else
                            reporte.ForMedicoTratUnoCodigo = dtg_medicos.Rows[i].Cells[3].Value.ToString();
                        if ((dtg_medicos.Rows[i].Cells[4].Value.ToString() != string.Empty) && (dtg_medicos.Rows[i].Cells[4].Value.ToString() != null))
                            reporte.ForMedicoTratDosRespo = dtg_medicos.Rows[i].Cells["resp_medico"].Value.ToString();
                        else
                            reporte.ForMedicoTratDosRespo = " ";
                    }
                    if (i == 2)
                    {
                        reporte.ForMedicoTratTresNom = (dtg_medicos.Rows[i].Cells[1].Value.ToString()).Replace("'", "´");
                        reporte.ForMedicoTratTresEspec = (dtg_medicos.Rows[i].Cells[2].Value.ToString()).Replace("'", "´");
                        if (dtg_medicos.Rows[i].Cells[3].Value != null)
                            reporte.ForMedicoTratUnoCodigo = dtg_medicos.Rows[i].Cells[3].Value.ToString();
                        else
                            reporte.ForMedicoTratUnoCodigo = dtg_medicos.Rows[i].Cells[3].Value.ToString();
                        if ((dtg_medicos.Rows[i].Cells[4].Value.ToString() != string.Empty) && (dtg_medicos.Rows[i].Cells[4].Value.ToString() != null))
                            reporte.ForMedicoTratTresRespo = dtg_medicos.Rows[i].Cells["resp_medico"].Value.ToString();
                        else
                            reporte.ForMedicoTratTresRespo = " ";
                    }
                    if (i == 3)
                    {
                        reporte.ForMedicoTratCuatroNom = (dtg_medicos.Rows[i].Cells[1].Value.ToString()).Replace("'", "´");
                        reporte.ForMedicoTratCuatroEspec = (dtg_medicos.Rows[i].Cells[2].Value.ToString()).Replace("'", "´");
                        if (dtg_medicos.Rows[i].Cells[3].Value != null)
                            reporte.ForMedicoTratUnoCodigo = dtg_medicos.Rows[i].Cells[3].Value.ToString();
                        else
                            reporte.ForMedicoTratUnoCodigo = dtg_medicos.Rows[i].Cells[3].Value.ToString();
                        if ((dtg_medicos.Rows[i].Cells[4].Value.ToString() != string.Empty) && (dtg_medicos.Rows[i].Cells[4].Value.ToString() != null))
                            reporte.ForMedicoTratCuatroRespo = dtg_medicos.Rows[i].Cells["resp_medico"].Value.ToString();
                        else
                            reporte.ForMedicoTratCuatroRespo = " ";

                    }
                }
                List<TIPO_EGRESO> listaTipoEgreso = new List<TIPO_EGRESO>();
                listaTipoEgreso = NegTipoEgreso.ListaTipoIngreso();
                if (listaTipoEgreso.Count > 0)
                {
                    TIPO_EGRESO tipoEgreso = new TIPO_EGRESO();
                    tipoEgreso = (TIPO_EGRESO)cmb_tipoEgreso.SelectedItem;
                    for (int i = 0; i < listaTipoEgreso.Count; i++)
                    {
                        tipoEgreso = listaTipoEgreso.ElementAt(i);
                        if (tipoEgreso.TIE_CODIGO == ((TIPO_EGRESO)cmb_tipoEgreso.SelectedItem).TIE_CODIGO)
                        {
                            if (i == 0)
                                reporte.ForAlataDef = "X";
                            if (i == 1)
                                reporte.ForAlataTra = "X";
                            if (i == 2)
                                reporte.ForAsintomatico = "X";
                            if (i == 3)
                                reporte.ForDiscLeve = "X";
                            if (i == 4)
                                reporte.ForDisccModerada = "X";
                            if (i == 5)
                                reporte.ForDiscGrave = "X";
                            if (i == 6)
                                reporte.ForRetiroAut = "X";
                            if (i == 7)
                                reporte.ForRetiroNoAuto = "X";
                            if (i == 8)
                                reporte.ForDefuncionMenos = "X";
                            if (i == 9)
                                reporte.ForDefucnionMas = "X";
                        }
                    }
                }
                DateTime fechaUno = atencion.ATE_FECHA_INGRESO.Value.Date;
                DateTime fechaDos;
                if (atencion.ATE_FECHA_ALTA == null)
                    fechaDos = DateTime.Now.Date;
                else
                    fechaDos = atencion.ATE_FECHA_ALTA.Value.Date;
                TimeSpan difFechas = fechaDos - fechaUno;
                reporte.ForDiasEstancia = difFechas.Days + 1;
                //reporte.ForDiasIncapacidad = 0;
                //reporte.ForFecha = dtp_fechaEpicrisis.Value;
                //reporte.ForHora = dtp_fechaEpicrisis.Value;

                if (atencion.ATE_FECHA_ALTA == null)
                {
                    DateTime vacio = Convert.ToDateTime("01/01/0001 00:00:00");
                    reporte.ForFecha = vacio.Date; // Para mostrar la fecha de la epicrisis en el reporte / Giovanny Tapia / 18/09/2012
                    reporte.ForHora = vacio.Date; // Para mostrar la hora de la epicrisis en el reporte / Giovanny Tapia / 18/09/2012

                }
                else
                {
                    reporte.ForFecha = atencion.ATE_FECHA_ALTA.Value; // Para mostrar la fecha de la epicrisis en el reporte / Giovanny Tapia / 18/09/2012
                    reporte.ForHora = atencion.ATE_FECHA_ALTA.Value; // Para mostrar la hora de la epicrisis en el reporte / Giovanny Tapia / 18/09/2012

                }

                reporte.ForFecha = Convert.ToDateTime((reporte.ForHora).ToString("dd/MM/yyyy")); // Para dar formato a la fecha de la epicrisis en el reporte / Giovanny Tapia / 18/09/2012 
                reporte.ForHora = Convert.ToDateTime((reporte.ForHora).ToString(("HH:mm:ss"))); // Para dar formato a la hora de la epicrisis en el reporte / Giovanny Tapia / 18/09/2012

                //reporte.ForNomProfesional = ("Dr/a. " + medicoProfesional + "\n" + "Dr/a. " + NegUsuarios.RecuperaUsuario(epicrisis.ID_USUARIO).APELLIDOS + " " + NegUsuarios.RecuperaUsuario(epicrisis.ID_USUARIO).NOMBRES).Replace("'", "´");
                //reporte.ForCodProfesional = Convert.ToString( epicrisis.ID_USUARIO);

                // Segun lo solicitado por el doctor Amilcar Herrera en una Epicrisis modificada al imprimirse debe ir el nombre de la persona que modifica. / 18/09/2012 / Giovanny Tapia

                reporte.ForNomProfesional = ("Dr/a. " + txt_profesional.Text + "\n" + "Dr/a. " + epicrisis.EPI_REALIZADO.Replace("'", "´")); // Muestra el nombre de quien creo o modifico una epicrisis / 18/09/2012 / Giovanny Tapia
                //Cambios Edgar 20210120 peticion de pablo, se necesita ver el codigo de medico tratante no el que realiza la epicrisis
                reporte.ForCodProfesional = med_codigo;

                //reporte.ForCodProfesional = Convert.ToString(NegUsuarios.RecuperaUsuarioNombres(epicrisis.EPI_REALIZADO).ID_USUARIO); // Muestra el codigo de quien creo o modifico una epicrisis / 18/09/2012 / Giovanny Tapia


                //"Dr/a. " + NegUsuarios.RecuperaUsuario(anamnesis.ID_USUARIO).NOMBRES + " " + NegUsuarios.RecuperaUsuario(anamnesis.ID_USUARIO).APELLIDOS;
                reporte.ForNumHoja = 2;

                ReportesHistoriaClinica reporteEpicrisis2 = new ReportesHistoriaClinica();
                reporteEpicrisis2.ingresarEpicrisis2(reporte);

                frmReportes ventana = new frmReportes(1, "epicrisis1");
                ventana.Show();


                //DSPrueba ds = new DSPrueba();
                //DataRow dr;

                //dr = ds.Tables["Epicrisis"].NewRow();
                //dr["Tratamiento"] = txt_tratamiento.Text;

                //ds.Tables["Epicrisis"].Rows.Add(dr);

                //frmReportes x = new frmReportes(1, "prueba", ds);
                //x.Show();

            }
            catch (Exception err)
            { MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

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


        private void AgregarError(Control control)
        {
            error.SetError(control, "Campo Requerido");

        }

        private bool validarFormulario()
        {
            bool flag = false;
            if (txt_cuadro.Text.ToString() == string.Empty)
            {
                AgregarError(txt_cuadro);
                flag = true;
            }
            if (txt_evolucion.Text.ToString() == string.Empty)
            {
                AgregarError(txt_evolucion);
                flag = true;
            }
            if (txt_hallazgos.Text.ToString() == string.Empty)
            {
                AgregarError(txt_hallazgos);
                flag = true;
            }
            if (txt_tratamiento.Text.ToString() == string.Empty)
            {
                AgregarError(txt_tratamiento);
                flag = true;
            }
            if (dtg_DIngreso.Rows.Count == 1)
            {
                AgregarError(dtg_DIngreso);
                flag = true;
            }
            else
            {
                for (Int16 i = 0; i < dtg_DIngreso.Rows.Count - 1; i++)
                {
                    DataGridViewRow fila = dtg_DIngreso.Rows[i];
                    DataGridViewTextBoxCell caja = (DataGridViewTextBoxCell)this.dtg_DIngreso.Rows[fila.Index].Cells[0];
                    if (caja.Value == null)
                    {
                        AgregarError(dtg_DIngreso);
                        flag = true;
                    }
                }
            }
            if (dtg_DEgresos.Rows.Count == 1)
            {
                AgregarError(dtg_DEgresos);
                flag = true;
            }
            else
            {
                for (Int16 i = 0; i < dtg_DEgresos.Rows.Count - 1; i++)
                {
                    DataGridViewRow fila = dtg_DEgresos.Rows[i];
                    DataGridViewTextBoxCell caja = (DataGridViewTextBoxCell)this.dtg_DEgresos.Rows[fila.Index].Cells[0];
                    if (caja.Value == null)
                    {
                        AgregarError(dtg_DEgresos);
                        flag = true;
                    }
                }
            }
            if (txt_condiciones.Text.ToString() == string.Empty)
            {
                AgregarError(txt_condiciones);
                flag = true;
            }
            if (dtg_medicos.Rows.Count == 0)
            {
                AgregarError(dtg_medicos);
                flag = true;
            }
            if (txtRealizadoPor.Text == string.Empty)
            {
                AgregarError(txtRealizadoPor);
                flag = true;
            }
            return flag;
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void frm_Epicrisis_Load(object sender, EventArgs e)
        {
            // Traduzco los mensajes al espa¤ol en la ventana de correcci¢n ortogr fica
            Infragistics.Shared.ResourceCustomizer rc = new Infragistics.Shared.ResourceCustomizer();

            rc = Infragistics.Win.UltraWinSpellChecker.Resources.Customizer;
            rc.SetCustomizedString("LS_SpellCheckForm", "Ortograf¡a");
            rc.SetCustomizedString("LS_SpellCheckForm_btChange", "&Cambiar");
            rc.SetCustomizedString("LS_SpellCheckForm_btChangeAll", "Cam&biar Todas");
            rc.SetCustomizedString("LS_SpellCheckForm_btClose_1", "Cancelar");
            rc.SetCustomizedString("LS_SpellCheckForm_btClose_2", "Cerrar");
            rc.SetCustomizedString("LS_SpellCheckForm_btIgnoreAll", "Omitir toda&s");
            rc.SetCustomizedString("LS_SpellCheckForm_btIgnoreOnce_1", "Om&itir una vez");
            rc.SetCustomizedString("LS_SpellCheckForm_btIgnoreOnce_2", "&Reanudar");
            rc.SetCustomizedString("LS_SpellCheckForm_btAddToDictionary", "Ag&regar");
            rc.SetCustomizedString("LS_SpellCheckForm_btUndo", "&Deshacer");
            rc.SetCustomizedString("LS_SpellCheckForm_lbErrorsFound", "Se han encontrado errores");
            rc.SetCustomizedString("LS_SpellCheckForm_lbChangeTo", "Cambiar a:");
            rc.SetCustomizedString("LS_SpellCheckForm_lbSuggestions", "Sugerencias:");

            //cargo el diccionario
            //ultraSpellCheckerEpicrisis.Dictionary = Application.StartupPath + "\\Recursos\\es-spanish-v2-whole.dict";

            //Añado el panel con la informaciòn del paciente
            InfPaciente infPaciente = new InfPaciente(ate_codigop);
            panelInfPaciente.Controls.Add(infPaciente);
            //cambio las dimenciones de los paneles
            panelInfPaciente.Size = new Size(panelInfPaciente.Width, 110);
            //pantab1.Top = 125;
        }

        private void txt_cuadro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Tab) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_evolucion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Tab) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_hallazgos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Tab) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_tratamiento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Tab) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_condiciones_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Tab) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_cuadro_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F7)
                {
                    //ultraSpellCheckerEpicrisis.ShowSpellCheck(txt_cuadro);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void ultraTabPageControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txt_hallazgos_ValueChanged(object sender, EventArgs e)
        {

        }

        private void ultraSpellCheckerEpicrisis_SpellChecking(object sender, Infragistics.Win.UltraWinSpellChecker.SpellCheckingEventArgs e)
        {

        }

        private void txt_evolucion_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
            {
                //ultraSpellCheckerEpicrisis.ShowSpellCheck(txt_evolucion);
            }
        }

        private void txt_hallazgos_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
            {
                //ultraSpellCheckerEpicrisis.ShowSpellCheck(txt_hallazgos);
            }
        }

        private void txt_tratamiento_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
            {
                //ultraSpellCheckerEpicrisis.ShowSpellCheck(txt_tratamiento);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            //tabControl1.Enabled = true;
            panellResumenes.Enabled = true;
            panelDiagnosticos.Enabled = true;
            panelCondiciones.Enabled = true;
            panelEgreso.Enabled = true;
            btnNuevo.Enabled = false;
            btnGuardar.Enabled = true;
            btnImprimir.Enabled = true;
            btnModificar.Enabled = false;
            modo = "UPDATE";

            txtRealizadoPor.Text = His.Entidades.Clases.Sesion.nomUsuario;
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void dtg_medicos_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            //dtg_medicos.Focus();
            //int fila = dtg_medicos.CurrentRow.Index;
            //int codigoF = Convert.ToInt32(dtg_medicos.Rows[dtg_medicos.CurrentRow.Index].Cells[0].Value);
            //if (dtg_medicos.Rows[fila].Cells[1].Value.ToString() != null)
            //{
            //     NegEpicrisis.EliminarrMedicos(Convert.ToInt32(dtg_medicos.Rows[fila].Cells[0].Value.ToString()));
            //}else
            //{
            //    dtg_medicos.CurrentRow.Dispose();
            //}
        }

        private void dtg_medicos_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            try
            {
                //cambios Edgar 20210120 se comenta por error de que supuestamente se envia null 
                //int fila = dtg_medicos.CurrentRow.Index;
                //if (e.Row.Cells[0].Value.ToString() != "")
                //{
                //    NegEpicrisis.EliminarrMedicos(Convert.ToInt32(e.Row.Cells[0].Value.ToString()));
                //    dtg_medicos.CurrentCell.Dispose();
                //    MessageBox.Show("Registro eliminado exitosamente", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
                //else
                //    dtg_medicos.CurrentCell.Dispose();
            }
            catch (Exception err) { MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnMedico_Click(object sender, EventArgs e)
        {
            List<MEDICOS> medicos = NegMedicos.listaMedicos();
            frm_Ayudas frm = new frm_Ayudas(medicos);
            frm.bandCampo = true;
            frm.ShowDialog();
            if (frm.campoPadre2.Text != string.Empty)
            {
                codMedico = (frm.campoPadre2);
                medico = NegMedicos.RecuperaMedicoId(Convert.ToInt32(codMedico.Text));
                agregarMedico(medico);

            }
        }

        private void ultraTabPageControl2_Paint(object sender, PaintEventArgs e)
        {

        }
        int rowIndex;
        private void btnSubir5_Click(object sender, EventArgs e)
        {
            try
            {
                rowIndex = dtg_DIngreso.SelectedCells[0].OwningRow.Index;
                if (rowIndex == 0)
                {
                    MessageBox.Show("Item En Primer Lugar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                DataTable table = new DataTable();
                table.Columns.Add("");
                table.Columns.Add("");
                table.Columns.Add("");
                table.Columns.Add("");
                DataRow row = table.NewRow();
                row[0] = dtg_DIngreso.Rows[rowIndex].Cells[0].Value.ToString();
                row[1] = dtg_DIngreso.Rows[rowIndex].Cells[1].Value.ToString();
                row[2] = dtg_DIngreso.Rows[rowIndex].Cells[2].Value.ToString();
                row[3] = dtg_DIngreso.Rows[rowIndex].Cells[3].Value.ToString();

                dtg_DIngreso.Rows.RemoveAt(rowIndex);
                dtg_DIngreso.Rows.Insert(rowIndex - 1, row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString());
                dtg_DIngreso.ClearSelection();
                dtg_DIngreso.Rows[rowIndex - 1].Selected = true;
            }
            catch
            {
                MessageBox.Show("Item No Seleccionado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        private void btnSubir6_Click(object sender, EventArgs e)
        {
            try
            {
                rowIndex = dtg_DEgresos.SelectedCells[0].OwningRow.Index;
                if (rowIndex == 0)
                {
                    MessageBox.Show("Item En Primer Lugar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                DataTable table = new DataTable();
                table.Columns.Add("");
                table.Columns.Add("");
                table.Columns.Add("");
                table.Columns.Add("");
                DataRow row = table.NewRow();
                row[0] = dtg_DEgresos.Rows[rowIndex].Cells[0].Value.ToString();
                row[1] = dtg_DEgresos.Rows[rowIndex].Cells[1].Value.ToString();
                row[2] = dtg_DEgresos.Rows[rowIndex].Cells[2].Value.ToString();
                row[3] = dtg_DEgresos.Rows[rowIndex].Cells[3].Value.ToString();

                dtg_DEgresos.Rows.RemoveAt(rowIndex);
                dtg_DEgresos.Rows.Insert(rowIndex - 1, row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString());
                dtg_DEgresos.ClearSelection();
                dtg_DEgresos.Rows[rowIndex - 1].Selected = true;
            }
            catch
            {
                MessageBox.Show("Item No Seleccionado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBajar6_Click(object sender, EventArgs e)
        {
            try
            {
                rowIndex = dtg_DEgresos.SelectedCells[0].OwningRow.Index;
                if (rowIndex == dtg_DEgresos.Rows.Count - 2)
                {
                    MessageBox.Show("Item En Ultimo Lugar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                DataTable table = new DataTable();
                table.Columns.Add("");
                table.Columns.Add("");
                table.Columns.Add("");
                table.Columns.Add("");
                DataRow row = table.NewRow();
                row[0] = dtg_DEgresos.Rows[rowIndex].Cells[0].Value.ToString();
                row[1] = dtg_DEgresos.Rows[rowIndex].Cells[1].Value.ToString();
                row[2] = dtg_DEgresos.Rows[rowIndex].Cells[2].Value.ToString();
                row[3] = dtg_DEgresos.Rows[rowIndex].Cells[3].Value.ToString();

                dtg_DEgresos.Rows.RemoveAt(rowIndex);
                dtg_DEgresos.Rows.Insert(rowIndex + 1, row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString());
                dtg_DEgresos.ClearSelection();
                dtg_DEgresos.Rows[rowIndex + 1].Selected = true;
            }
            catch
            {
                MessageBox.Show("Item No Seleccionado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBajar5_Click(object sender, EventArgs e)
        {
            try
            {
                rowIndex = dtg_DIngreso.SelectedCells[0].OwningRow.Index;
                if (rowIndex == dtg_DIngreso.Rows.Count - 2)
                {
                    MessageBox.Show("Item En Ultimo Lugar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                DataTable table = new DataTable();
                table.Columns.Add("");
                table.Columns.Add("");
                table.Columns.Add("");
                table.Columns.Add("");
                DataRow row = table.NewRow();
                row[0] = dtg_DIngreso.Rows[rowIndex].Cells[0].Value.ToString();
                row[1] = dtg_DIngreso.Rows[rowIndex].Cells[1].Value.ToString();
                row[2] = dtg_DIngreso.Rows[rowIndex].Cells[2].Value.ToString();
                row[3] = dtg_DIngreso.Rows[rowIndex].Cells[3].Value.ToString();

                dtg_DIngreso.Rows.RemoveAt(rowIndex);
                dtg_DIngreso.Rows.Insert(rowIndex + 1, row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString());
                dtg_DIngreso.ClearSelection();
                dtg_DIngreso.Rows[rowIndex + 1].Selected = true;
            }
            catch
            {
                MessageBox.Show("Item No Seleccionado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnagregar_Click(object sender, EventArgs e)
        {
            error.Clear();
            if (dtpInicio.Value.Date > dtpFin.Value)
            {
                error.SetError(btnagregar, "La fecha de inicio no puede ser mayor a la fecha de fin.");
            }
            else
            {
                if (dtpInicio.Value > DateTime.Now || dtpFin.Value > DateTime.Now)
                {
                    error.SetError(btnagregar, "Las fechas no deben sobrepasan el dia actual.");
                }
                else
                {
                    if (dtg_medicos.Rows.Count - 1 > 0)
                    {
                        foreach (DataGridViewRow item in dtg_medicos.Rows)
                        {
                            if (item.Cells["resp_medico"].Value == null || item.Cells["resp_medico"].Value.ToString() == "")
                            {
                                if (item.Cells[0].Value != null)
                                {
                                    string fechainicio = dtpInicio.Value.ToShortDateString();
                                    fechainicio = fechainicio.Replace('/', '-');
                                    string fechafin = dtpFin.Value.ToShortDateString();
                                    fechafin = fechafin.Replace('/', '-');
                                    item.Cells["resp_medico"].Value = fechainicio + " / " + fechafin;
                                }
                            }
                        }
                    }
                }
            }
        }
        MEDICOS med = null;
        private void txt_profesional_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                List<MEDICOS> medicos = NegMedicos.listaMedicos();
                frm_Ayudas frm = new frm_Ayudas(medicos);
                frm.bandCampo = true;
                frm.ShowDialog();
                if (frm.campoPadre2.Text != string.Empty)
                {
                    codMedico = (frm.campoPadre2);
                    med = NegMedicos.RecuperaMedicoId(Convert.ToInt32(codMedico.Text));
                    agregarMedico1(med);
                }
            }
        }

        private void btnF1DiagIngreso_Click(object sender, EventArgs e)
        {
            frm_BusquedaCIE10 busqueda = new frm_BusquedaCIE10();
            busqueda.ShowDialog();
            //if()
            diagnostico = busqueda.resultado;
            codigoCIE = busqueda.codigo;
            valida1();
        }

        private void btnDiagEgreso_Click(object sender, EventArgs e)
        {
            frm_BusquedaCIE10 busqueda = new frm_BusquedaCIE10();
            busqueda.ShowDialog();
            diagnostico2 = busqueda.resultado;
            codigoCIE2 = busqueda.codigo;
            valida2();
        }

        private void agregarMedico1(MEDICOS medicoTratante)
        {
            if ((medicoTratante != null))
            {
                txt_profesional.Text = medicoTratante.MED_APELLIDO_PATERNO.Trim() + " " + medicoTratante.MED_APELLIDO_MATERNO.Trim()
                    + " " + medicoTratante.MED_NOMBRE1.Trim() + " " + medicoTratante.MED_NOMBRE2.Trim();
                if (medicoTratante.MED_RUC != null)
                    med_codigo = medicoTratante.MED_RUC;
                else
                    med_codigo = "0";
                //if (medicoTratante.MED_CODIGO_MEDICO != null)
                //    txt_CodMSPE.Text = medicoTratante.MED_CODIGO_MEDICO.ToString();
                //else
                //    txt_CodMSPE.Text = "0"; //no tiene codigo
            }

        }
    }
}
