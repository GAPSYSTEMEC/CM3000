using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms.VisualStyles;
using System.Windows.Forms;
using His.Entidades;
using His.Entidades.Reportes;
using His.Negocio;
using Recursos;
using GeneralApp.ControlesWinForms;
using His.Parametros;
using His.General;
using His.Entidades.Clases;
using His.DatosReportes;


namespace His.Formulario
{
    public partial class frm_Anemnesis : Form
    {
        #region variables
        private int atencionId;             //codigo de la atencion del paciente
        private bool mostrarInfPaciente;    //si se mostrara el panel con la informacion del paciente
        public int codigoAtencion;
        string diagnostico = String.Empty;
        string codigoCIE = string.Empty;
        int codigoMedico = 0;
        PACIENTES paciente = null;
        ATENCIONES atencion = null;
        MEDICOS medico = null;
        HC_ANAMNESIS anamnesis = new HC_ANAMNESIS();
        string modo = "SAVE";
        #endregion

        public frm_Anemnesis()
        {
            InitializeComponent();
            inicializar(0);
            mostrarInfPaciente = false;

        }
        public frm_Anemnesis(int codigoAtencion, bool parMostrarInfPaciente)
        {
            //cuando es anemnesis aqui se debe agregar el bloqueo si es diferente de 1 en la atencion
            InitializeComponent();
            
            inicializar(codigoAtencion);
            atencionId = codigoAtencion;
            mostrarInfPaciente = parMostrarInfPaciente;

            NegAtenciones atenciones = new NegAtenciones(); //Edgar 20201126
            string estado = atenciones.EstadoCuenta(Convert.ToString(codigoAtencion));
            if(estado != "1")
            {
                Bloquear();
            }
            else
            {
                //no hace nada
            } //Edgar 20201126

            //Cambios Edgar 20210303  Requerimientos de la pasteur por Alex.
            if(Sesion.codDepartamento == 6)
            {
                btnNuevo.Enabled = false;
                btnModificar.Enabled = false;
                btnGuardar.Enabled = false;
                btnImprimir.Enabled = false;
            }
        }
        public void HabilitarBotones(bool nuevo, bool guardar, bool modificar, bool imprimir)
        {
            btnNuevo.Enabled = nuevo;
            btnGuardar.Enabled = guardar;
            btnImprimir.Enabled = imprimir;
            btnModificar.Enabled = modificar;

        }
        public void Bloquear()
        {
            //btnImprimir.Enabled = false;
            btnModificar.Enabled = false;
            //grpUnoDos.Enabled = false;
            //grpTresCuatro.Enabled = false;
            //grpCincoSeis.Enabled = false;
            //grpSieteOcho.Enabled = false;
            //grpNueve.Enabled = false;
            btnNuevo.Enabled = false;
            btnGuardar.Enabled = false;
        }
        private void inicializar(int codigoAtencion)
        {
            if (codigoAtencion != 0)
            {
                cargarAtencion(codigoAtencion);

            }

            //valido perimetro cefalico
            if (paciente.PAC_FECHA_NACIMIENTO != null)
            {
                //if (Funciones.CalcularEdad((DateTime)paciente.PAC_FECHA_NACIMIENTO) > 3)
                //{
                //    txt_perimetro.Enabled = false;
                //    txt_tempAxilar.Enabled = false;
                //}
                //else
                //{
                //    txt_perimetro.Enabled = true;
                //    txt_tempAxilar.Enabled = true;
                //}
                //Cambios Edgar 20210118
            }
            btnNuevo.Image = Archivo.imgBtnAdd2;
            btnGuardar.Image = Archivo.imgBtnGoneSave48;
            btnImprimir.Image = Archivo.imgBtnGonePrint48;
            btnSalir.Image = Archivo.imgBtnGoneExit48;
            btnModificar.Image = Archivo.btnEditar16;
            //this.BackgroundImage = Archivo.fondoA1x1024x73;
            cargarAntecedentesFamiliares();
            cargarAntecedentesPersonales();
            cargarOrganos();
            cargarExamen();
            if (anamnesis != null)
            {
                if (anamnesis.ANE_FECHA.ToString() == null)
                    cargarHora();
            }
            else
                cargarHora();
            pictureBox1.Image = Archivo.F1_16x16;

        }

        #region Eventos

        //private void checkBox9_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (grp_mujer.Enabled == true)
        //        grp_mujer.Enabled = false;
        //    else
        //        grp_mujer.Enabled = true;
        //}          


        private void textBox20_KeyPress(object sender, KeyPressEventArgs e)
        {
            soloNumeros(e);
        }

        private void textBox21_KeyPress(object sender, KeyPressEventArgs e)
        {
            soloNumeros(e);
        }

        private void textBox22_KeyPress(object sender, KeyPressEventArgs e)
        {
            soloNumeros(e);
        }

        private void textBox23_KeyPress(object sender, KeyPressEventArgs e)
        {
            soloNumeros(e);
        }

        //private void checkBox9_CheckedChanged_1(object sender, EventArgs e)
        //{
        //    if (grp_mujer.Enabled == true)
        //        grp_mujer.Enabled = false;
        //    else
        //        grp_mujer.Enabled = true;

        //}

        protected void chk_evidenciaP_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;
            DataGridViewRow row = (DataGridViewRow)checkbox.Container;
            //GridViewRow row = (GridViewRow)checkbox.NamingContainer;
            //Todo lo que queramos hacer aquí
        }
       
        /// <summary>
        /// Método modificado en txtcell.ReadOnly = false;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtg_organos_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if(dtg_organos.CurrentRow.Cells[1].Value != null)
            {
                if (e.ColumnIndex == this.dtg_organos.Columns[2].Index)
                {
                    DataGridViewCheckBoxCell chkCell = (DataGridViewCheckBoxCell)this.dtg_organos.Rows[e.RowIndex].Cells[2];
                    if (chkCell.Value == null)
                        chkCell.Value = false;
                    else
                        chkCell.Value = true;

                    DataGridViewTextBoxCell txtcell = (DataGridViewTextBoxCell)this.dtg_organos.Rows[e.RowIndex].Cells[4];
                    txtcell.ReadOnly = false;
                    DataGridViewCheckBoxCell chkCell2 = (DataGridViewCheckBoxCell)this.dtg_organos.Rows[e.RowIndex].Cells[3];
                    chkCell2.Value = false;
                }
                else
                {
                    if (e.ColumnIndex == this.dtg_organos.Columns[3].Index)
                    {
                        DataGridViewCheckBoxCell chkCell = (DataGridViewCheckBoxCell)this.dtg_organos.Rows[e.RowIndex].Cells[3];
                        if (chkCell.Value == null)
                            chkCell.Value = false;
                        else
                            chkCell.Value = true;

                        DataGridViewTextBoxCell txtcell = (DataGridViewTextBoxCell)this.dtg_organos.Rows[e.RowIndex].Cells[4];
                        txtcell.ReadOnly = false;
                        //txtcell.Value = "";
                        DataGridViewCheckBoxCell chkCell2 = (DataGridViewCheckBoxCell)this.dtg_organos.Rows[e.RowIndex].Cells[2];
                        chkCell2.Value = false;
                    }

                }
            }
            else
            {
                MessageBox.Show("Debe elegir ORGANO/SISTEMA para continuar...", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //dtg_organos.Rows.RemoveAt(dtg_organos.CurrentRow.Index);
            }
        }

        private void dataGridView1_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            try
            {
                if(dtg_examenFisico.CurrentRow.Cells[1].Value != null)
                {
                    if (e.ColumnIndex == this.dtg_examenFisico.Columns[2].Index)
                    {
                        DataGridViewCheckBoxCell chkCell = (DataGridViewCheckBoxCell)this.dtg_examenFisico.Rows[e.RowIndex].Cells[2];
                        if (chkCell.Value == null)
                            chkCell.Value = false;
                        else
                            chkCell.Value = true;

                        DataGridViewTextBoxCell txtcell = (DataGridViewTextBoxCell)this.dtg_examenFisico.Rows[e.RowIndex].Cells[4];
                        txtcell.ReadOnly = false;
                        DataGridViewCheckBoxCell chkCell2 = (DataGridViewCheckBoxCell)this.dtg_examenFisico.Rows[e.RowIndex].Cells[3];
                        chkCell2.Value = false;
                    }
                    else
                    {
                        if (e.ColumnIndex == this.dtg_examenFisico.Columns[3].Index)
                        {
                            DataGridViewCheckBoxCell chkCell = (DataGridViewCheckBoxCell)this.dtg_examenFisico.Rows[e.RowIndex].Cells[3];
                            if (chkCell.Value == null)
                                chkCell.Value = false;
                            else
                                chkCell.Value = true;

                            DataGridViewTextBoxCell txtcell = (DataGridViewTextBoxCell)this.dtg_examenFisico.Rows[e.RowIndex].Cells[4];
                            txtcell.ReadOnly = false;
                            //txtcell.Value = "";
                            DataGridViewCheckBoxCell chkCell2 = (DataGridViewCheckBoxCell)this.dtg_examenFisico.Rows[e.RowIndex].Cells[2];
                            chkCell2.Value = false;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No se ha elegido un examen.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                frm_BusquedaCIE10 busqueda = new frm_BusquedaCIE10();
                busqueda.ShowDialog();
                //if (busqueda.codigo != null)
                //{
                //    DataGridViewRow fila = dtg_diagnosticos.CurrentRow;
                //    fila.Cells[1].Value = busqueda.codigo;
                //    fila.Cells[2].Value = busqueda.resultado;
                //}
                if (busqueda.codigo != null)
                {
                    dtg_diagnosticos.Rows.Add(null, busqueda.codigo, busqueda.resultado, false, false);
                    //DataGridViewRow fila = dtg_diagnosticos.CurrentRow;
                    //fila.Cells[1].Value = busqueda.codigo;
                    //fila.Cells[2].Value = busqueda.resultado;
                    //fila.Cells[3].Value = false;
                    //fila.Cells[4].Value = false;
                }

                dtg_diagnosticos.Focus();
            }
            if (e.KeyCode == Keys.Delete)
            {
                if (dtg_diagnosticos.CurrentRow != null)
                {
                    if (dtg_diagnosticos.CurrentRow.Cells["codigoDia"].Value != null)
                    {
                        Int64 codigoDetDiag = Convert.ToInt64(dtg_diagnosticos.CurrentRow.Cells["codigoDia"].Value);
                        NegAnamnesisDetalle.eliminarDiagnosticoDetalle(codigoDetDiag);
                        dtg_diagnosticos.Rows.Remove(dtg_diagnosticos.CurrentRow);
                        MessageBox.Show("registro eliminado exitosamente", "Inf", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        dtg_diagnosticos.Rows.Remove(dtg_diagnosticos.CurrentRow);
                        MessageBox.Show("registro eliminado exitosamente", "Inf", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        #endregion

        #region Funciones
        private void cargarAtencion(int codAtencion)
        {
            try
            {
                atencion = NegAtenciones.RecuperarAtencionID(codAtencion);
                paciente = NegPacientes.RecuperarPacienteID(atencion.PACIENTES.PAC_CODIGO);
                //cargarAntecedentesFamiliares();
                //cargarAntecedentesPersonales();
                cargarAnamnesis();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo cargar los datos de la atencion, error: " + ex.Message, "error");
            }
        }

        private void cargarAnamnesis()
        {
            try
            {
                anamnesis = NegAnamnesis.recuperarAnamnesisPorAtencion(atencion.ATE_CODIGO);
                //valido si el paciente es hombre o mujer
                if (paciente.PAC_GENERO.Equals("M"))
                    grp_mujer.Enabled = false;
                else
                    grp_mujer.Enabled = true;

                if (anamnesis == null)
                {
                    modo = "SAVE";
                    HabilitarBotones(true, false, false, false);
                    activarFormulario(false);
                    txt_profesional.Text = Sesion.nomUsuario;
                }
                else
                {
                    modo = "UPDATE";
                    
                    if (anamnesis.ID_USUARIO == His.Entidades.Clases.Sesion.codUsuario)
                        HabilitarBotones(false, false, true, true);
                    else
                        HabilitarBotones(false, false, false, true);
                    activarFormulario(false);
                    txt_problema.Text = anamnesis.ANE_PROBLEMA;
                    txt_presionA.Text = anamnesis.ANE_PRESION_A.ToString();
                    txt_presionB.Text = anamnesis.ANE_PRESION_B.ToString();
                    txt_frecCardiaca.Text = anamnesis.ANE_FREC_CARDIACA;
                    txt_frecRespiratoria.Text = anamnesis.ANE_FREC_RESPIRATORIA;
                    txt_tempBucal.Text = anamnesis.ANE_TEMP_BUCAL.ToString();
                    txt_tempAxilar.Text = anamnesis.ANE_TEMP_AXILAR.ToString();
                    txt_peso.Text = anamnesis.ANE_PESO.ToString();
                    txt_talla.Text = anamnesis.ANE_TALLA.ToString();
                    txt_perimetro.Text = anamnesis.ANE_PERIMETRO.ToString();
                    txt_tratamiento.Text = anamnesis.ANE_PLAN_TRATAMIENTO;
                    txt_fecha.Text = anamnesis.ANE_FECHA.Value.ToString("dd/MM/yyyy");
                    txt_hora.Text = anamnesis.ANE_FECHA.Value.ToString("HH:mm");
                    cargarMotivosConsulta(anamnesis.ANE_CODIGO);
                    cargarDetalles(anamnesis.ANE_CODIGO);
                    cargarDiagnosticos();
                    cargarDA(anamnesis.ANE_CODIGO);
                    //if (grp_mujer.Enabled == true)
                        cargarDatosMujer();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void cargarDA(int ANE_codigo)
        {
            

            DtoAnamnesis_DA DA = null;
            DA = NegAnamnesis.getDA(ANE_codigo);

            if (DA != null)
            {
                txt_profesional.Text = DA.MEDICO;
            }
            else
            {
                txt_profesional.Text = Sesion.nomUsuario;
            }
        }


        private void cargarDiagnosticos()
        {
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
                    textcell.Value = diagnosticos.CDA_CODIGO;
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
                    fila.Cells.Add(textcell);
                    fila.Cells.Add(textcell1);
                    fila.Cells.Add(textcell2);
                    fila.Cells.Add(chkcell);
                    fila.Cells.Add(chkcell2);
                    dtg_diagnosticos.Rows.Add(fila);
                }
            }
        }

        private void cargarDetalles(int codAnamnesis)
        {
            List<HC_ANAMNESIS_DETALLE> det = NegAnamnesisDetalle.listaDetallesAnamnesis(codAnamnesis);
            if (det != null)
            {
                foreach (HC_ANAMNESIS_DETALLE detalle in det)
                {
                    DataGridViewRow fila = new DataGridViewRow();
                    DataGridViewComboBoxCell cmbcell = new DataGridViewComboBoxCell();
                    DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                    codigoCell.Value = detalle.ADE_CODIGO;
                    textcell.Value = detalle.ADE_DESCRIPCION;

                    switch (NegCatalogos.listaTipoCatalogos().FirstOrDefault(t => t.EntityKey == NegCatalogos.RecuperarCatalogoPorID(NegCatalogos.listaCatalogos().FirstOrDefault(i => i.EntityKey == detalle.HC_CATALOGOSReference.EntityKey).HCC_CODIGO).First().HC_CATALOGOS_TIPOReference.EntityKey).HCT_CODIGO)
                    {
                        case 2:
                            //ANTECEDENTES PERSONALES
                            cmbcell.DataSource = NegCatalogos.RecuperarCatalogoPorID(NegCatalogos.listaCatalogos().FirstOrDefault(i => i.EntityKey == detalle.HC_CATALOGOSReference.EntityKey).HCC_CODIGO);
                            cmbcell.DisplayMember = "HCC_NOMBRE";
                            cmbcell.ValueMember = "HCC_NOMBRE";
                            cmbcell.Value = NegCatalogos.RecuperarCatalogoPorID(NegCatalogos.listaCatalogos().FirstOrDefault(i => i.EntityKey == detalle.HC_CATALOGOSReference.EntityKey).HCC_CODIGO).First().HCC_NOMBRE;
                            fila.Cells.Add(codigoCell);
                            fila.Cells.Add(cmbcell);
                            fila.Cells.Add(textcell);
                            dtg_antec_personales.Rows.Add(fila);
                            break;

                        case 3:
                            //ANTECEDENTES FAMILIARES
                            cmbcell.DataSource = NegCatalogos.RecuperarCatalogoPorID(NegCatalogos.listaCatalogos().FirstOrDefault(i => i.EntityKey == detalle.HC_CATALOGOSReference.EntityKey).HCC_CODIGO);
                            cmbcell.DisplayMember = "HCC_NOMBRE";
                            cmbcell.ValueMember = "HCC_NOMBRE";
                            cmbcell.Value = NegCatalogos.RecuperarCatalogoPorID(NegCatalogos.listaCatalogos().FirstOrDefault(i => i.EntityKey == detalle.HC_CATALOGOSReference.EntityKey).HCC_CODIGO).First().HCC_NOMBRE;
                            fila.Cells.Add(codigoCell);
                            fila.Cells.Add(cmbcell);
                            fila.Cells.Add(textcell);
                            dtg_antec_familiares.Rows.Add(fila);
                            break;

                        case 4:
                            //ORGANOS Y SISTEMAS
                            cmbcell.DataSource = NegCatalogos.RecuperarCatalogoPorID(NegCatalogos.listaCatalogos().FirstOrDefault(i => i.EntityKey == detalle.HC_CATALOGOSReference.EntityKey).HCC_CODIGO);
                            cmbcell.DisplayMember = "HCC_NOMBRE";
                            cmbcell.ValueMember = "HCC_NOMBRE";
                            cmbcell.Value = NegCatalogos.RecuperarCatalogoPorID(NegCatalogos.listaCatalogos().FirstOrDefault(i => i.EntityKey == detalle.HC_CATALOGOSReference.EntityKey).HCC_CODIGO).First().HCC_NOMBRE;
                            DataGridViewCheckBoxCell chk1 = new DataGridViewCheckBoxCell();
                            DataGridViewCheckBoxCell chk2 = new DataGridViewCheckBoxCell();
                            if (detalle.ADE_TIPO.Trim().Equals("CP"))
                            {
                                chk1.Value = true;
                                chk2.Value = false;
                            }
                            else
                            {
                                chk2.Value = true;
                                chk1.Value = false;
                            }
                            fila.Cells.Add(codigoCell);
                            fila.Cells.Add(cmbcell);
                            fila.Cells.Add(chk1);
                            fila.Cells.Add(chk2);
                            fila.Cells.Add(textcell);
                            dtg_organos.Rows.Add(fila);
                            break;

                        case 5:
                            //EXAMEN FISICO
                            chk1 = new DataGridViewCheckBoxCell();
                            chk2 = new DataGridViewCheckBoxCell();
                            cmbcell.DataSource = NegCatalogos.RecuperarCatalogoPorID(NegCatalogos.listaCatalogos().FirstOrDefault(i => i.EntityKey == detalle.HC_CATALOGOSReference.EntityKey).HCC_CODIGO);
                            cmbcell.DisplayMember = "HCC_NOMBRE";
                            cmbcell.ValueMember = "HCC_NOMBRE";
                            cmbcell.Value = NegCatalogos.RecuperarCatalogoPorID(NegCatalogos.listaCatalogos().FirstOrDefault(i => i.EntityKey == detalle.HC_CATALOGOSReference.EntityKey).HCC_CODIGO).First().HCC_NOMBRE;
                            if (detalle.ADE_TIPO.Trim().Equals("CP"))
                            {
                                chk1.Value = true;
                                chk2.Value = false;
                            }
                            else
                            {
                                chk2.Value = true;
                                chk1.Value = false;
                            }
                            fila.Cells.Add(codigoCell);
                            fila.Cells.Add(cmbcell);
                            fila.Cells.Add(chk1);
                            fila.Cells.Add(chk2);
                            fila.Cells.Add(textcell);
                            dtg_examenFisico.Rows.Add(fila);
                            break;
                    }
                }
            }
        }

        //Modificacion Orden del case 1 / txt_motivo4.Text a case 1 / txt_motivo1.Text

        private void cargarMotivosConsulta(int codAnamnesis)
        {
            List<HC_ANAMNESIS_MOTIVOS_CONSULTA> amc = NegAnamnesisDetalle.motivosConsultaPorId(codAnamnesis);
            if (amc != null)
            {
                for (int i = 0; i < amc.Count; i++ ) 
                {
                    HC_ANAMNESIS_MOTIVOS_CONSULTA motivo = new HC_ANAMNESIS_MOTIVOS_CONSULTA();
                    motivo = amc.ElementAt(i);
                    if (motivo.MOC_TIPO == "1")             
                        txt_motivo1.Text = motivo.MOC_DESCRIPCION;              
                    else
                    {
                        if (motivo.MOC_TIPO == "2")                        
                            txt_motivo2.Text = motivo.MOC_DESCRIPCION;                        
                        else 
                        {
                            if (motivo.MOC_TIPO == "3")                        
                                txt_motivo3.Text = motivo.MOC_DESCRIPCION;                         
                            else                             
                                if (motivo.MOC_TIPO == "4")                            
                                    txt_motivo4.Text = motivo.MOC_DESCRIPCION;                            
                          
                        }
                    }
                }
            }            
        }

        private void cargarDatosMujer()
        {
            try
            {
                HC_ANAMNESIS_ANTEC_MUJER datosMujer = NegAnamnesis.recuperarAnamnesisDatosMujer(anamnesis.ANE_CODIGO);
                if (datosMujer != null)
                {
                    chk_biopsia.Checked = (bool)datosMujer.AMU_BIOPSIA;
                    txt_ciclos.Text = datosMujer.AMU_CICLOS.ToString();
                    chk_colcoscopia.Checked = (bool)datosMujer.AMU_COLPOSCOPIA;
                    if (Convert.ToString(datosMujer.AMU_FUM.Value) != "01/01/1900 00:00:00")
                    {
                        dtp_ultimaMenst.Checked = true;
                        dtp_ultimaMenst.Value = datosMujer.AMU_FUC.Value;
                    }
                    else
                        dtp_ultimaMenst.Checked = false;

                    //dtp_ultimaMenst.Value = (DateTime)datosMujer.AMU_FUM;
                    chk_mamografia.Checked = (bool)datosMujer.AMU_MAMOGRAFIA;
                    txt_menarquia.Text = datosMujer.AMU_MENARQUIA.ToString();
                    txt_menopausia.Text = datosMujer.AMU_MENOPAUSIA.ToString();
                    txt_prevencion.Text = datosMujer.AMU_MET_PREVENCION;
                    if (datosMujer.AMU_GESTA.ToString() == "0")
                    {
                        txt_abortos.Enabled = false;
                        txt_hijosvivos.Enabled = false;
                        txt_partos.Enabled = false;
                        txt_cesareas.Enabled = false;
                        dtp_ultimoParto.Enabled = false;
                    }
                    else
                    {
                        txt_gesta.Text = datosMujer.AMU_GESTA.ToString();
                        txt_abortos.Text = datosMujer.AMU_ABORTOS.ToString();
                        txt_hijosvivos.Text = datosMujer.AMU_HIJOSVIVOS.ToString();
                        txt_partos.Text = datosMujer.AMU_PARTOS.ToString();
                        txt_cesareas.Text = datosMujer.AMU_CESAREAS.ToString();
                        dtp_ultimoParto.Value = (DateTime)datosMujer.AMU_FUP;
                    }
                    //aaam.AMU_FUC = dtp_ultimaCito.Checked ==true?dtp_ultimaCito.Value: Convert.ToDateTime("01/01/1900");                    
                    if (Convert.ToString(datosMujer.AMU_FUC.Value) != "01/01/1900 00:00:00")
                    {
                        dtp_ultimaCito.Checked = true;
                        dtp_ultimaCito.Value = datosMujer.AMU_FUC.Value;
                    }
                    else                     
                        dtp_ultimaCito.Checked = false;
                    
                    chk_terapia.Checked = (bool)datosMujer.AMU_TERAPIAHORMONAL;
                    chk_vidasexual.Checked = (bool)datosMujer.AMU_VIDASEXUAL;
                }
                else 
                {
                    txt_gesta.Text = "0";                    
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }


        private void cargarHora()
        {
            DateTime dt = new DateTime();
            txt_fecha.Text = System.DateTime.Now.ToString("yyyy/MM/dd");
            txt_hora.Text = System.DateTime.Now.ToString("HH:mm:ss");

        }


        private void cargarExamen()
        {
            try
            {
                List<DtoCatalogos> exa = NegCatalogos.RecuperarCatalogosPorTipo(5);
                tipo_examen.DataSource = exa;
                tipo_examen.DisplayMember = "HCC_NOMBRE";
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }

        private void cargarOrganos()
        {
            try
            {
                List<DtoCatalogos> org = NegCatalogos.RecuperarCatalogosPorTipo(4);
                tipo_organo.DataSource = org;
                tipo_organo.DisplayMember = "HCC_NOMBRE";
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }

        private void cargarAntecedentesPersonales()
        {
            try
            {
                List<DtoCatalogos> ant1 = NegCatalogos.RecuperarCatalogosPorTipo(2);
                Tipo.DataSource = ant1;
                Tipo.DisplayMember = "HCC_NOMBRE";
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }

        private void cargarAntecedentesFamiliares()
        {
            try
            {
                List<DtoCatalogos> ant1 = NegCatalogos.RecuperarCatalogosPorTipo(3);
                tipoAntFam.DataSource = ant1;
                tipoAntFam.DisplayMember = "HCC_NOMBRE";
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }


        private void soloNumeros(KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == true)
            { }
            else if (e.KeyChar == 46)
            { }
            else if (e.KeyChar == 44)
            { }
            else if (e.KeyChar == '\b')
            { }
            else
            { e.Handled = true; }
        }


        #endregion


        int index = 0;


        private void limpiarCampos()
        {
            foreach (Control c in this.pantab1.Controls)
                foreach (Control control in c.Controls)
                    foreach (Control cajas in control.Controls)
                        if (cajas.Name.Substring(0, 3).Equals("txt"))
                            cajas.Text = "";
                        else
                            if (cajas.Name.Substring(0, 3).Equals("dtg"))
                            {
                                //DataGridView dtg = (DataGridView)cajas;
                                //for (int i = 0; i < dtg.Rows.Count; i++)
                                //    dtg.Rows[i].Cells.Remove(dtg.Rows[i].Cells[i]);
                            }
                            else
                                if (cajas.Name.Substring(0, 3).Equals("grp"))
                                    foreach (Control txt in cajas.Controls)
                                        if (txt.Name.Substring(0, 3).Equals("txt"))
                                            txt.Text = "";
            cargarHora();
            //añado 0 por defecto en los campos numericos
            txt_presionA.Text = "0";
            txt_presionB.Text = "0";
            txt_frecCardiaca.Text = "0";
            txt_frecRespiratoria.Text = "0";
            txt_tempBucal.Text = "0";
            txt_peso.Text = "0";
            txt_talla.Text = "0";
            txtIndiceMasaCorporal.Text = "0";
            txt_perimetro.Text = "0";
            txt_tempAxilar.Text = "0";
            txt_menarquia.Text = "0";
            txt_menopausia.Text = "0";
            txt_ciclos.Text = "0";
            txt_gesta.Text = "0";
            txt_partos.Text = "0";
            txt_abortos.Text = "0";
            txt_cesareas.Text = "0";
            txt_hijosvivos.Text = "0";
            txt_profesional.Text = Sesion.nomUsuario;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!controlarCampos())
                {
                    if (!controlarCamposExtensos())
                    {
                        guardarAnamnesis();
                        MessageBox.Show("Registro Guardado", "ANAMNESIS", MessageBoxButtons.OK, MessageBoxIcon.Information);                    
                        btnGuardar.Enabled = true;
                        //btnNuevo.Enabled = true;
                        btnImprimir.Enabled = true;
                        //cargarAnamnesis();
                        //imprimirReporte("pdf");
                        imprimirReporte("reporte");
                    }
                    else
                    MessageBox.Show("Campos Extensos");
                }
                else
                    MessageBox.Show("Faltan campos por llenar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool controlarCampos()
        {
            error.Clear();
            bool flag = false;
            if (txt_motivo1.Text == string.Empty)
            {
                AgregarError(txt_motivo1);
                flag = true;
            }
            //valido que se ingrese antecedentes personales
            //if (dtg_antec_personales.Rows.Count == 1)
            //{
            //    AgregarError(dtg_antec_personales);
            //    flag = true;
            //}
            //valido que se ingrese antecedentes familiares
            //if (dtg_antec_familiares.Rows.Count == 1)
            //{
            //    AgregarError(dtg_antec_familiares);
            //    flag = true;
            //}
            
            if (txt_problema.Text == string.Empty)
            {
                AgregarError(txt_problema);
                flag = true;
            }
            //revision de organos y sistemas
            //if (dtg_organos.Rows.Count == 1)
            //{
            //    AgregarError(dtg_organos);
            //    flag = true;
            //}
            if (Convert.ToDecimal(txt_presionA.Text) <= 0 )
            {
                AgregarError(txt_presionA);
                flag = true;
            }
            if (Convert.ToDecimal(txt_presionB.Text) <= 0)
            {
                AgregarError(txt_presionB);
                flag = true;
            }
            if (Convert.ToDecimal(txt_frecCardiaca.Text)<= 0)
            {
                AgregarError(txt_frecCardiaca);
                flag = true;
            }
            if (Convert.ToDecimal(txt_frecRespiratoria.Text) <= 0)
            {
                AgregarError(txt_frecRespiratoria);
                flag = true;
            }

            if (Convert.ToDecimal(txt_tempBucal.Text) <= 0)
            {
                AgregarError(txt_tempBucal);
                flag = true;
            }
            if (Convert.ToDecimal(txt_tempAxilar.Text) < 0)
            {
                AgregarError(txt_tempAxilar);
                flag = true;
            }
            if (Convert.ToDecimal(txt_perimetro.Text) < 0)
            {
                AgregarError(txt_perimetro);
                flag = true;
            }
            if (Convert.ToDecimal(txt_talla.Text) <= 0)
            {
                AgregarError(txt_talla);
                flag = true;
            }
            if (Convert.ToDecimal(txt_peso.Text) <= 0)
            {
                AgregarError(txt_peso);
                flag = true;
            }
            //valido que ingrese examen fisico
            //if (dtg_examenFisico.Rows.Count == 1)
            //{
            //    AgregarError(dtg_examenFisico);
            //    flag = true;
            //}
            //valido que tenga diagnostico
            if (dtg_diagnosticos.Rows.Count == 1)
            {
                AgregarError(dtg_diagnosticos);
                flag = true;
            }
            dtg_diagnosticos.Refresh();
            for (int i = 0; i < dtg_diagnosticos.Rows.Count - 1; i++)
            {
                if (dtg_diagnosticos.Rows[i].Cells[3].Value == null && dtg_diagnosticos.Rows[i].Cells[4].Value == null)
                {
                    AgregarError(dtg_diagnosticos);
                    flag = true;
                }
                else
                {
                    if ((bool)dtg_diagnosticos.Rows[i].Cells[3].Value == true)
                    {
                        dtg_diagnosticos.Rows[i].Cells[4].Value = false;
                    }
                    else
                    {
                        dtg_diagnosticos.Rows[i].Cells[3].Value = false;
                    }
                }
            }

            dtg_antec_personales.Refresh();

            if(dtg_antec_personales.Rows.Count > 1)
            {
                for (int i = 0; i < dtg_antec_personales.Rows.Count -1; i++)
                {
                    if (dtg_antec_personales.Rows[i].Cells[2].Value == null)
                    {
                        error.SetError(dtg_antec_personales, "Debe agregar el detalle");
                        flag = true;
                    }

                }
            }

            if (dtg_antec_familiares.Rows.Count > 1)
            {
                for (int i = 0; i < dtg_antec_familiares.Rows.Count - 1; i++)
                {
                    if (dtg_antec_familiares.Rows[i].Cells[2].Value == null)
                    {
                        error.SetError(dtg_antec_familiares, "Debe agregar el detalle.");
                        flag = true;
                    }

                }
            }

            if (dtg_examenFisico.Rows.Count > 1)
            {
                for (int i = 0; i < dtg_examenFisico.Rows.Count - 1; i++)
                {
                    if (dtg_examenFisico.Rows[i].Cells[4].Value == null)
                    {
                        error.SetError(dtg_examenFisico, "Debe agregar el detalle.");
                        flag = true;
                    }

                }
            }


            if (txt_tratamiento.Text == string.Empty)
            {
                AgregarError(txt_tratamiento);
                flag = true;
            }
            return flag;
        }

        private bool controlarCamposExtensos()
        {
            bool flag = false;
            if (txt_problema.TextLength <=1240)
            {
                int cont = 0;
                foreach (string s in txt_problema.Text.Split('\n'))
                    cont++;
                if (cont > 14)
                {
                    AgregarError(txt_problema);
                    flag = true;
                }
            }else
            {
                AgregarError(txt_problema);
                flag = true;
            }
            return flag;
        }

        private void guardarAnamnesis()
        {
            try
            {
                if (anamnesis == null)
                    anamnesis = new HC_ANAMNESIS();

                anamnesis.PACIENTESReference.EntityKey = paciente.EntityKey;
                anamnesis.ATENCIONESReference.EntityKey = atencion.EntityKey;
                anamnesis.ANE_PROBLEMA = txt_problema.Text;
                anamnesis.ANE_FREC_CARDIACA = txt_frecCardiaca.Text;
                anamnesis.ANE_FREC_RESPIRATORIA = txt_frecRespiratoria.Text;
                anamnesis.ANE_TEMP_BUCAL = Convert.ToDecimal(txt_tempBucal.Text);
                anamnesis.ANE_TEMP_AXILAR = Convert.ToDecimal(txt_tempAxilar.Text);
                anamnesis.ANE_PESO = Convert.ToDecimal(txt_peso.Text);
                anamnesis.ANE_TALLA = Convert.ToDecimal(txt_talla.Text);
                if (txt_perimetro.Text != "")
                    anamnesis.ANE_PERIMETRO = Convert.ToDecimal(txt_perimetro.Text);
                anamnesis.ANE_PLAN_TRATAMIENTO = txt_tratamiento.Text;
                anamnesis.ANE_FECHA = Convert.ToDateTime(DateTime.Now);
                anamnesis.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                anamnesis.ANE_PRESION_A = Convert.ToInt32(txt_presionA.Text);
                anamnesis.ANE_PRESION_B = Convert.ToInt32(txt_presionB.Text);

                if (modo == "SAVE")
                {
                    anamnesis.ANE_CODIGO = NegAnamnesis.ultimoCodigo() + 1;
                    NegAnamnesis.crearAnamnesis(anamnesis);
                    guardarMotivosConsulta();
                    guardarDetalles();
                    
                }
                else
                    if (modo == "UPDATE")
                    {
                        NegAnamnesis.actualizarAnamnesis(anamnesis);
                        //actualizarDetalles(anamnesis.ANE_CODIGO);
                        actualizarMotivosConsulta(anamnesis.ANE_CODIGO);
                        guardarDetalles();

                    }
                guardarDatosAdicionales(anamnesis.ANE_CODIGO, modo);
                btnGuardar.Enabled = false;
                btnImprimir.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
            }
        }

        private void guardarDatosAdicionales(int ane_codigo, string modo)
        {
            DtoAnamnesis_DA x = new DtoAnamnesis_DA();
            x.ANE_CODIGO = ane_codigo;
            x.MEDICO = txt_profesional.Text.Trim();
            NegAnamnesis.saveDA(x,modo);
        }




        private void actualizarMotivosConsulta(int codAnamnesis)
        {
            List<HC_ANAMNESIS_MOTIVOS_CONSULTA> mot = NegAnamnesisDetalle.listaMotivosConsulta(codAnamnesis);

            if (mot.Count > 0)
            {
                for (int i = 0; i < mot.Count; i++)
                {
                    HC_ANAMNESIS_MOTIVOS_CONSULTA motivo = new HC_ANAMNESIS_MOTIVOS_CONSULTA();
                    motivo = mot.ElementAt(i);
                    if (motivo.MOC_TIPO == "1")
                        motivo.MOC_DESCRIPCION = txt_motivo1.Text.Trim();
                    if (motivo.MOC_TIPO == "2")
                        motivo.MOC_DESCRIPCION = txt_motivo2.Text.Trim();
                    if (motivo.MOC_TIPO == "3")
                        motivo.MOC_DESCRIPCION = txt_motivo3.Text.Trim();
                    if (motivo.MOC_TIPO == "4")
                        motivo.MOC_DESCRIPCION = txt_motivo4.Text.Trim();
                    motivo.HC_ANAMNESISReference.EntityKey = anamnesis.EntityKey;
                    NegAnamnesisDetalle.actualizarMotivosConsulta(motivo);
                }
            }
            else 
            {
                guardarMotivosConsulta();
            }
        }

        private void actualizarDetalles(int codAnamnesis)
        {
            try
            {
                //List<HC_ANAMNESIS_DETALLE> det = NegAnamnesisDetalle.listaDetallesAnamnesis(codAnamnesis);



                //        var catalogo = NegCatalogos.listaTipoCatalogos().FirstOrDefault(t => t.EntityKey == NegCatalogos.RecuperarCatalogoPorID(NegCatalogos.listaCatalogos().FirstOrDefault(i => i.EntityKey == detalle.HC_CATALOGOSReference.EntityKey).HCC_CODIGO).First().HC_CATALOGOS_TIPOReference.EntityKey).HCT_CODIGO;
                //        switch (NegCatalogos.listaTipoCatalogos().FirstOrDefault(t => t.EntityKey == NegCatalogos.RecuperarCatalogoPorID(NegCatalogos.listaCatalogos().FirstOrDefault(i => i.EntityKey == detalle.HC_CATALOGOSReference.EntityKey).HCC_CODIGO).First().HC_CATALOGOS_TIPOReference.EntityKey).HCT_CODIGO)
                //        {
                //            case 2:
                //                //ANTECEDENTES PERSONALES
                //                fila = dtg_antec_personales.Rows[cont];
                //                if (fila.Cells[1].Value != null)
                //                    detalle.ADE_DESCRIPCION = fila.Cells[1].Value.ToString();
                //                else
                //                    detalle.ADE_DESCRIPCION = "";
                //                detalle.HC_ANAMNESISReference.EntityKey = anamnesis.EntityKey;
                //                hc = NegCatalogos.RecuperarCatalogoPorNombre(fila.Cells[0].Value.ToString());
                //                detalle.HC_CATALOGOSReference.EntityKey = hc.EntityKey;
                //                detalle.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                //                NegAnamnesisDetalle.actualizarDetalle(detalle);
                //                cont++;
                //                break;

                //            case 3:
                //                //ANTECEDENTES FAMILIARES
                //                fila = dtg_antec_familiares.Rows[cont2];
                //                if (fila.Cells[1].Value != null)
                //                    detalle.ADE_DESCRIPCION = fila.Cells[1].Value.ToString();
                //                else
                //                    detalle.ADE_DESCRIPCION = "";
                //                detalle.HC_ANAMNESISReference.EntityKey = anamnesis.EntityKey;
                //                hc = NegCatalogos.RecuperarCatalogoPorNombre(fila.Cells[0].Value.ToString());
                //                detalle.HC_CATALOGOSReference.EntityKey = hc.EntityKey;
                //                detalle.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                //                NegAnamnesisDetalle.actualizarDetalle(detalle);
                //                cont2++;
                //                break;

                //            case 4:
                //                //ORGANOS Y SISTEMAS
                //                fila = dtg_organos.Rows[cont3];
                //                if (fila.Cells[1].Value != null)
                //                    detalle.ADE_DESCRIPCION = fila.Cells[1].Value.ToString();
                //                else
                //                    detalle.ADE_DESCRIPCION = "";
                //                detalle.HC_ANAMNESISReference.EntityKey = anamnesis.EntityKey;
                //                hc = NegCatalogos.RecuperarCatalogoPorNombre(fila.Cells[0].Value.ToString());
                //                detalle.HC_CATALOGOSReference.EntityKey = hc.EntityKey;
                //                detalle.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                //                NegAnamnesisDetalle.actualizarDetalle(detalle);
                //                cont3++;
                //                break;

                //            case 5:
                //                //EXAMEN FISICO
                //                fila = dtg_examenFisico.Rows[cont4];
                //                if (fila.Cells[1].Value != null)
                //                    detalle.ADE_DESCRIPCION = fila.Cells[1].Value.ToString();
                //                else
                //                    detalle.ADE_DESCRIPCION = "";
                //                detalle.HC_ANAMNESISReference.EntityKey = anamnesis.EntityKey;
                //                hc = NegCatalogos.RecuperarCatalogoPorNombre(fila.Cells[0].Value.ToString());
                //                detalle.HC_CATALOGOSReference.EntityKey = hc.EntityKey;
                //                detalle.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                //                NegAnamnesisDetalle.actualizarDetalle(detalle);
                //                cont4++;
                //                break;
                //        }

                //    }
                //}
                //List<HC_ANAMNESIS_DETALLE> det = NegAnamnesisDetalle.listaDetallesAnamnesis(codAnamnesis);
                //if (det != null)
                //{
                //    int cont = 0;
                //    int cont2 = 0;
                //    int cont3 = 0;
                //    int cont4 = 0;
                //    DataGridViewRow fila;
                //    HC_CATALOGOS hc;

                //    foreach (HC_ANAMNESIS_DETALLE detalle in det)
                //    {
                //        var catalogo = NegCatalogos.listaTipoCatalogos().FirstOrDefault(t => t.EntityKey == NegCatalogos.RecuperarCatalogoPorID(NegCatalogos.listaCatalogos().FirstOrDefault(i => i.EntityKey == detalle.HC_CATALOGOSReference.EntityKey).HCC_CODIGO).First().HC_CATALOGOS_TIPOReference.EntityKey).HCT_CODIGO;
                //        switch (NegCatalogos.listaTipoCatalogos().FirstOrDefault(t => t.EntityKey == NegCatalogos.RecuperarCatalogoPorID(NegCatalogos.listaCatalogos().FirstOrDefault(i => i.EntityKey == detalle.HC_CATALOGOSReference.EntityKey).HCC_CODIGO).First().HC_CATALOGOS_TIPOReference.EntityKey).HCT_CODIGO)
                //        {
                //            case 2:
                //                //ANTECEDENTES PERSONALES
                //                fila = dtg_antec_personales.Rows[cont];
                //                if (fila.Cells[1].Value != null)
                //                    detalle.ADE_DESCRIPCION = fila.Cells[1].Value.ToString();
                //                else
                //                    detalle.ADE_DESCRIPCION = "";
                //                detalle.HC_ANAMNESISReference.EntityKey = anamnesis.EntityKey;
                //                hc = NegCatalogos.RecuperarCatalogoPorNombre(fila.Cells[0].Value.ToString());
                //                detalle.HC_CATALOGOSReference.EntityKey = hc.EntityKey;
                //                detalle.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                //                NegAnamnesisDetalle.actualizarDetalle(detalle);
                //                cont++;
                //                break;

                //            case 3:
                //                //ANTECEDENTES FAMILIARES
                //                fila = dtg_antec_familiares.Rows[cont2];
                //                if (fila.Cells[1].Value != null)
                //                    detalle.ADE_DESCRIPCION = fila.Cells[1].Value.ToString();
                //                else
                //                    detalle.ADE_DESCRIPCION = "";
                //                detalle.HC_ANAMNESISReference.EntityKey = anamnesis.EntityKey;
                //                hc = NegCatalogos.RecuperarCatalogoPorNombre(fila.Cells[0].Value.ToString());
                //                detalle.HC_CATALOGOSReference.EntityKey = hc.EntityKey;
                //                detalle.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                //                NegAnamnesisDetalle.actualizarDetalle(detalle);
                //                cont2++;
                //                break;

                //            case 4:
                //                //ORGANOS Y SISTEMAS
                //                fila = dtg_organos.Rows[cont3];
                //                if (fila.Cells[1].Value != null)
                //                    detalle.ADE_DESCRIPCION = fila.Cells[1].Value.ToString();
                //                else
                //                    detalle.ADE_DESCRIPCION = "";
                //                detalle.HC_ANAMNESISReference.EntityKey = anamnesis.EntityKey;
                //                hc = NegCatalogos.RecuperarCatalogoPorNombre(fila.Cells[0].Value.ToString());
                //                detalle.HC_CATALOGOSReference.EntityKey = hc.EntityKey;
                //                detalle.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                //                NegAnamnesisDetalle.actualizarDetalle(detalle);
                //                cont3++;
                //                break;

                //            case 5:
                //                //EXAMEN FISICO
                //                fila = dtg_examenFisico.Rows[cont4];
                //                if (fila.Cells[1].Value != null)
                //                    detalle.ADE_DESCRIPCION = fila.Cells[1].Value.ToString();
                //                else
                //                    detalle.ADE_DESCRIPCION = "";
                //                detalle.HC_ANAMNESISReference.EntityKey = anamnesis.EntityKey;
                //                hc = NegCatalogos.RecuperarCatalogoPorNombre(fila.Cells[0].Value.ToString());
                //                detalle.HC_CATALOGOSReference.EntityKey = hc.EntityKey;
                //                detalle.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                //                NegAnamnesisDetalle.actualizarDetalle(detalle);
                //                cont4++;
                //                break;
                //        }

                //    }
                //}
            }
            catch (Exception err)
            { MessageBox.Show(err.Message); }
        }

        private void guardarDetalles()
        {
            //ANTECEDENTES PERSONALES
            try
            {
                foreach (DataGridViewRow fila in dtg_antec_personales.Rows)
                {
                    if (fila != null)
                    {
                        if (fila.Cells[1].Value != null)
                        {
                            HC_ANAMNESIS_DETALLE detalle = new HC_ANAMNESIS_DETALLE();
                            if (fila.Cells[2].Value != null)
                                detalle.ADE_DESCRIPCION = fila.Cells[2].Value.ToString();
                            else
                                detalle.ADE_DESCRIPCION = "";
                            detalle.HC_ANAMNESISReference.EntityKey = anamnesis.EntityKey;
                            HC_CATALOGOS hc = NegCatalogos.RecuperarCatalogoPorNombre(fila.Cells[1].Value.ToString());
                            detalle.HC_CATALOGOSReference.EntityKey = hc.EntityKey;
                            detalle.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                            if (fila.Cells[0].Value != null)
                            {
                                detalle.ADE_CODIGO = Convert.ToInt32(fila.Cells[0].Value);
                                NegAnamnesisDetalle.actualizarDetalle(detalle);
                            }
                            else
                            {
                                detalle.ADE_CODIGO = NegAnamnesisDetalle.ultimoCodigo() + 1;
                                NegAnamnesisDetalle.crearAnamnesisDetalle(detalle);
                            }
                        }
                    }
                }

                //ANTECEDENTES FAMILIARES
                foreach (DataGridViewRow fila in dtg_antec_familiares.Rows)
                {
                    if (fila != null)
                    {
                        if (fila.Cells[1].Value != null)
                        {
                            HC_ANAMNESIS_DETALLE detalle = new HC_ANAMNESIS_DETALLE();
                            if (fila.Cells[2].Value != null)
                                detalle.ADE_DESCRIPCION = fila.Cells[2].Value.ToString();
                            else
                                detalle.ADE_DESCRIPCION = "";                            
                            detalle.HC_ANAMNESISReference.EntityKey = anamnesis.EntityKey;
                            HC_CATALOGOS hc = NegCatalogos.RecuperarCatalogoPorNombre(fila.Cells[1].Value.ToString());
                            detalle.HC_CATALOGOSReference.EntityKey = hc.EntityKey;
                            detalle.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                            if (fila.Cells[0].Value != null)
                            {
                                detalle.ADE_CODIGO = Convert.ToInt32(fila.Cells[0].Value);
                                NegAnamnesisDetalle.actualizarDetalle(detalle);
                            }
                            else
                            {
                                detalle.ADE_CODIGO = NegAnamnesisDetalle.ultimoCodigo() + 1;
                                NegAnamnesisDetalle.crearAnamnesisDetalle(detalle);
                            }
                        }
                    }
                }
                //ORGANOS
                foreach (DataGridViewRow fila in dtg_organos.Rows)
                {
                    if (fila != null)
                    {
                        if (fila.Cells[1].Value != null)
                        {
                            HC_ANAMNESIS_DETALLE detalle = new HC_ANAMNESIS_DETALLE();
                            if (fila.Cells[4].Value != null)
                                detalle.ADE_DESCRIPCION = fila.Cells[4].Value.ToString();
                            else
                                detalle.ADE_DESCRIPCION = "";                             
                            detalle.HC_ANAMNESISReference.EntityKey = anamnesis.EntityKey;
                            HC_CATALOGOS hc = NegCatalogos.RecuperarCatalogoPorNombre(fila.Cells[1].Value.ToString());
                            detalle.HC_CATALOGOSReference.EntityKey = hc.EntityKey;
                            detalle.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                            if (Convert.ToBoolean(fila.Cells[2].Value))
                                detalle.ADE_TIPO = "CP";
                            else
                                detalle.ADE_TIPO = "SP";
                            //detalle.ADE_ESTADO 
                            if (fila.Cells[0].Value != null)
                            {
                                detalle.ADE_CODIGO = Convert.ToInt32(fila.Cells[0].Value);
                                NegAnamnesisDetalle.actualizarDetalle(detalle);
                            }
                            else
                            {
                                detalle.ADE_CODIGO = NegAnamnesisDetalle.ultimoCodigo() + 1;
                                NegAnamnesisDetalle.crearAnamnesisDetalle(detalle);
                            }
                        }
                    }
                }
                //EXAMEN FISICO
                foreach (DataGridViewRow fila in dtg_examenFisico.Rows)
                {

                    if (fila != null)
                    {
                        if (fila.Cells[2].Value != null)
                        {
                            HC_ANAMNESIS_DETALLE detalle = new HC_ANAMNESIS_DETALLE();
                            if (fila.Cells[4].Value != null)
                                detalle.ADE_DESCRIPCION = fila.Cells[4].Value.ToString();
                            else
                                detalle.ADE_DESCRIPCION = "";
                            detalle.HC_ANAMNESISReference.EntityKey = anamnesis.EntityKey;
                            HC_CATALOGOS hc = NegCatalogos.RecuperarCatalogoPorNombre(fila.Cells[1].Value.ToString());
                            detalle.HC_CATALOGOSReference.EntityKey = hc.EntityKey;
                            detalle.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                            if (Convert.ToBoolean(fila.Cells[2].Value))
                                detalle.ADE_TIPO = "CP";
                            else
                                detalle.ADE_TIPO = "SP";
                            if (fila.Cells[0].Value != null)
                            {
                                detalle.ADE_CODIGO = Convert.ToInt32(fila.Cells[0].Value);
                                NegAnamnesisDetalle.actualizarDetalle(detalle);
                            }
                            else
                            {
                                detalle.ADE_CODIGO = NegAnamnesisDetalle.ultimoCodigo() + 1;
                                NegAnamnesisDetalle.crearAnamnesisDetalle(detalle);
                            }
                        }

                    }
                }
                //DIAGNOSTICOss
                foreach (DataGridViewRow fila in dtg_diagnosticos.Rows)
                {
                    if (fila != null)
                    {
                        if (fila.Cells[1].Value != null)
                        {
                            HC_ANAMNESIS_DIAGNOSTICOS detalle = new HC_ANAMNESIS_DIAGNOSTICOS();
                            detalle.CIE_CODIGO = fila.Cells[1].Value.ToString();
                            if (Convert.ToBoolean(fila.Cells[3].Value))
                                detalle.CDA_ESTADO = true;
                            else
                                detalle.CDA_ESTADO = false;
                            detalle.HC_ANAMNESISReference.EntityKey = anamnesis.EntityKey;
                            detalle.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                            detalle.CDA_DESCRIPCION = fila.Cells[2].Value.ToString();
                            if (fila.Cells[0].Value != null)
                            {
                                HC_ANAMNESIS_DIAGNOSTICOS objcont = NegAnamnesisDetalle.recuperarDiagnosticosAnamnesisCodigo();
                                detalle.CDA_CODIGO = Convert.ToInt32(fila.Cells[0].Value.ToString());
                                NegAnamnesisDetalle.actualizarAnamnesisDiagnosticos(detalle);
                                //NegAnamnesisDetalle.crearAnamnesisDiagnosticos(detalle);
                            }
                            else
                                NegAnamnesisDetalle.crearAnamnesisDiagnosticos(detalle);
                        }
                    }
                }
                //guardarMotivosConsulta();
                if (grp_mujer.Enabled)
                    guardarDatosMujer();
            }
            catch (Exception err) { MessageBox.Show("error", err.Message, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void guardarMotivosConsulta()
        {
            HC_ANAMNESIS_MOTIVOS_CONSULTA amc = new HC_ANAMNESIS_MOTIVOS_CONSULTA();
            if (txt_motivo1.Text != "")
            {
                amc = new HC_ANAMNESIS_MOTIVOS_CONSULTA();
                amc.MOC_CODIGO = NegAnamnesis.ultimoCodigoMotivoConsuta() + 1;
                amc.MOC_DESCRIPCION = txt_motivo1.Text.Trim();
                amc.HC_ANAMNESISReference.EntityKey = anamnesis.EntityKey;
                amc.MOC_TIPO = "1";
                NegAnamnesisDetalle.crearAnamnesisConsultas(amc);
            }
            if (txt_motivo2.Text != "")
            {
                amc = new HC_ANAMNESIS_MOTIVOS_CONSULTA();
                amc.MOC_CODIGO = NegAnamnesis.ultimoCodigoMotivoConsuta() + 1;
                amc.MOC_DESCRIPCION = txt_motivo2.Text.Trim();
                amc.HC_ANAMNESISReference.EntityKey = anamnesis.EntityKey;
                amc.MOC_TIPO = "2";
                NegAnamnesisDetalle.crearAnamnesisConsultas(amc);
            }
            if (txt_motivo3.Text != "")
            {
                amc = new HC_ANAMNESIS_MOTIVOS_CONSULTA();
                amc.MOC_CODIGO = NegAnamnesis.ultimoCodigoMotivoConsuta() + 1;
                amc.MOC_DESCRIPCION = txt_motivo3.Text.Trim();
                amc.HC_ANAMNESISReference.EntityKey = anamnesis.EntityKey;
                amc.MOC_TIPO = "3";
                NegAnamnesisDetalle.crearAnamnesisConsultas(amc);
            }
            if (txt_motivo4.Text != "")
            {
                amc = new HC_ANAMNESIS_MOTIVOS_CONSULTA();
                amc.MOC_CODIGO = NegAnamnesis.ultimoCodigoMotivoConsuta() + 1;
                amc.MOC_DESCRIPCION = txt_motivo4.Text.Trim();
                amc.HC_ANAMNESISReference.EntityKey = anamnesis.EntityKey;
                amc.MOC_TIPO = "4";
                NegAnamnesisDetalle.crearAnamnesisConsultas(amc);
            }
        }


        private void guardarDatosMujer()
        {
            try
            {
                bool accion = false;
                HC_ANAMNESIS_ANTEC_MUJER aaam = NegAnamnesis.recuperarAnamnesisDatosMujer(anamnesis.ANE_CODIGO);
                if (aaam == null)
                {
                    accion = true;
                    aaam = new HC_ANAMNESIS_ANTEC_MUJER();
                    aaam.AMU_CODIGO = NegAnamnesis.ultimoCodigoMujer() + 1;
                }
                aaam.AMU_BIOPSIA = chk_biopsia.Checked;            
                aaam.AMU_CICLOS = Convert.ToInt32(txt_ciclos.Text);
                aaam.AMU_COLPOSCOPIA = chk_colcoscopia.Checked;
                aaam.AMU_FUM = dtp_ultimaMenst.Checked == true ? dtp_ultimaMenst.Value : Convert.ToDateTime("01/01/1900");                
                aaam.AMU_MAMOGRAFIA = chk_mamografia.Checked;
                aaam.AMU_MENARQUIA = Convert.ToInt32(txt_menarquia.Text);
                aaam.AMU_MENOPAUSIA = Convert.ToInt32(txt_menopausia.Text);
                aaam.AMU_MET_PREVENCION = txt_prevencion.Text;                
                aaam.AMU_TERAPIAHORMONAL = chk_terapia.Checked;
                aaam.AMU_VIDASEXUAL = chk_vidasexual.Checked;
                aaam.HC_ANAMNESISReference.EntityKey = anamnesis.EntityKey;
                aaam.AMU_ABORTOS = Convert.ToInt32(txt_abortos.Text);
                aaam.AMU_CESAREAS = Convert.ToInt32(txt_cesareas.Text);
                aaam.AMU_GESTA = Convert.ToInt32(txt_gesta.Text);
                aaam.AMU_HIJOSVIVOS = Convert.ToInt32(txt_hijosvivos.Text);
                aaam.AMU_PARTOS = Convert.ToInt32(txt_partos.Text);
                aaam.AMU_FUP = dtp_ultimoParto.Value;
                aaam.AMU_FUC = dtp_ultimaCito.Checked ==true?dtp_ultimaCito.Value: Convert.ToDateTime("01/01/1900");
                if (accion == true)
                {                    
                    NegAnamnesis.crearAntecedentesMujer(aaam);
                }
                else
                {
                    NegAnamnesis.actualizarAnamnesisDatosMujer(aaam);
                }
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }

        //private void guardarDatosMujer()
        //{
        //    try
        //    {
        //        HC_ANAMNESIS_ANTEC_MUJER aaam = new HC_ANAMNESIS_ANTEC_MUJER();
        //        aaam.AMU_ABORTOS = Convert.ToInt32(txt_abortos.Text);
        //        aaam.AMU_BIOPSIA = chk_biopsia.Checked;
        //        aaam.AMU_CESAREAS = Convert.ToInt32(txt_cesareas.Text);
        //        aaam.AMU_CICLOS = Convert.ToInt32(txt_ciclos.Text);
        //        aaam.AMU_CODIGO = NegAnamnesis.ultimoCodigoMujer() + 1;
        //        aaam.AMU_COLPOSCOPIA = chk_colcoscopia.Checked;
        //        aaam.AMU_FUC = dtp_ultimaCito.Value;
        //        aaam.AMU_FUM = dtp_ultimaMenst.Value;
        //        aaam.AMU_FUP = dtp_ultimoParto.Value;
        //        aaam.AMU_GESTA = Convert.ToInt32(txt_gesta.Text);
        //        aaam.AMU_HIJOSVIVOS = Convert.ToInt32(txt_hijosvivos.Text);
        //        aaam.AMU_MAMOGRAFIA = chk_mamografia.Checked;
        //        aaam.AMU_MENARQUIA = Convert.ToInt32(txt_menarquia.Text);
        //        aaam.AMU_MENOPAUSIA = Convert.ToInt32(txt_menopausia.Text);
        //        aaam.AMU_MET_PREVENCION = txt_prevencion.Text;
        //        aaam.AMU_PARTOS = Convert.ToInt32(txt_partos.Text);
        //        aaam.AMU_TERAPIAHORMONAL = chk_terapia.Checked;
        //        aaam.AMU_VIDASEXUAL = chk_vidasexual.Checked;
        //        aaam.HC_ANAMNESISReference.EntityKey = anamnesis.EntityKey;
        //        NegAnamnesis.crearAntecedentesMujer(aaam);
        //    }
        //    catch (Exception err) { MessageBox.Show(err.Message); }
        //}

        private void dataGridView1_CellContentClick_1(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == this.dtg_examenFisico.Columns[2].Index)
            //{
            //    DataGridViewCheckBoxCell chkCell = (DataGridViewCheckBoxCell)this.dtg_diagnosticos.Rows[e.RowIndex].Cells[2];
            //    if (chkCell.Value == null)
            //        chkCell.Value = false;
            //    else
            //        chkCell.Value = true;


            //    DataGridViewCheckBoxCell chkCell2 = (DataGridViewCheckBoxCell)this.dtg_diagnosticos.Rows[e.RowIndex].Cells[3];
            //    chkCell2.Value = false;
            //}
            //else
            //{
            //    if (e.ColumnIndex == this.dtg_diagnosticos.Columns[3].Index)
            //    {
            //        DataGridViewCheckBoxCell chkCell = (DataGridViewCheckBoxCell)this.dtg_diagnosticos.Rows[e.RowIndex].Cells[3];
            //        if (chkCell.Value == null)
            //            chkCell.Value = false;
            //        else
            //            chkCell.Value = true;

            //        DataGridViewCheckBoxCell chkCell2 = (DataGridViewCheckBoxCell)this.dtg_diagnosticos.Rows[e.RowIndex].Cells[2];
            //        chkCell2.Value = false;
            //    }
            //    else
            //        if (e.ColumnIndex == this.dtg_diagnosticos.Columns[0].Index)
            //        {

            //        }

            //}

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void AgregarError(Control control)
        {
            error.SetError(control, "Campo Requerido");

        }

        private void AgregarErrorTexto(Control control)
        {
            error.SetError(control, "Campo Demasiado Extenso");

        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            HabilitarBotones(false, true, false, false);
            activarFormulario(true);
            limpiarCampos();
        }
        private void activarFormulario(bool estado)
        {
            grpUnoDos.Enabled = estado;
            grpTresCuatro.Enabled = estado;
            grpCincoSeis.Enabled = estado;
            grpSieteOcho.Enabled = estado;
            grpNueve.Enabled = estado;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            //btnNuevo.Enabled = false;
            btnGuardar.Enabled = true;
            btnImprimir.Enabled = true;
            btnModificar.Enabled = false;
            activarFormulario(true);
            modo = "UPDATE";
        }

        private void frm_Anemnesis_Load(object sender, EventArgs e)
        {
            try
            {
                //Añado el panel con la informaciòn del paciente
                InfPaciente infPaciente = new InfPaciente(atencionId);
                panelInfPaciente.Controls.Add(infPaciente);
                //cambio las dimenciones de los paneles
                panelInfPaciente.Size = new Size(panelInfPaciente.Width, 110);
                pantab1.Top = 125;
                //cargar tamaño por defecto de la vista
                this.Height = this.Height + 110;
                //if (mostrarInfPaciente == true)
                //{

                //}
                //añade a los controles textbox el evento de keypress
                foreach (Control control in pantab1.Controls)
                {
                    if (control.Controls.Count > 0)
                        recorerControles(control);
                    else
                    {
                        if (control is TextBox)
                        {
                            control.KeyPress += new KeyPressEventHandler(keypressed);
                        }
                    }
                }
                
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void recorerControles(Control parControl)
        {
            try
            {
                foreach (Control control in parControl.Controls)
                {
                    if (control.Controls.Count > 0)
                        recorerControles(control);
                    else
                    {
                        if (control is TextBox)
                        {
                            control.KeyPress += new KeyPressEventHandler(keypressed);
                        }
                    }

                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void keypressed(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }

        }

        private void txt_talla_Leave(object sender, EventArgs e)
        {

        }

        private void txt_talla_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Decimal indiceMasaCorporal = 0;
                Decimal talla = Convert.ToDecimal(txt_talla.Text);
                if (talla > 0)
                    indiceMasaCorporal = Convert.ToDecimal(txt_peso.Text) / (talla * talla);
                txtIndiceMasaCorporal.Text = Decimal.Round(indiceMasaCorporal, 2).ToString();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void txt_tempBucal_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            imprimirReporte("reporte");
        }

        private void imprimirReporte(string accion)
        {
            try
            {
                //recupero la informacion para el informe
                ReporteAnamnesis reporte = new ReporteAnamnesis();
                reporte.path = NegUtilitarios.RutaLogo("General");
                reporte.ForEstablecimiento = Sesion.nomEmpresa;
                reporte.ForNombres = (paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2).Replace("'", "´");
                reporte.ForApellidos = (paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO).Replace("'", "´");
                reporte.ForSexo = paciente.PAC_GENERO;
                reporte.ForNumeroHoja = 1;
                reporte.ForNumeroHistoria = Convert.ToString(paciente.PAC_HISTORIA_CLINICA);
                reporte.ForMcA = (this.txt_motivo1.Text).Replace("'", "´");
                reporte.ForMcB = (txt_motivo2.Text).Replace("'", "´");
                reporte.ForMcC = (this.txt_motivo3.Text).Replace("'", "´");
                reporte.ForMcD = (txt_motivo4.Text).Replace("'", "´");
                reporte.ForApMenarquia = txt_menarquia.Text;
                reporte.ForApMenopausia = this.txt_menopausia.Text;
                reporte.ForApCiclos = txt_ciclos.Text;
                reporte.ForApVidaSexual = chk_vidasexual.Checked == true ? "X" : "-";
                reporte.ForApGesta = txt_gesta.Text == "0" ? "-" : txt_gesta.Text;
                reporte.ForApPartos = txt_partos.Text == "0" ? "-" : txt_partos.Text;
                reporte.ForApAbortos = txt_abortos.Text == "0" ? "-" : txt_abortos.Text;
                reporte.ForApCesareaas = txt_cesareas.Text == "0" ? "-" : txt_cesareas.Text;
                reporte.ForApHijosVivos = txt_hijosvivos.Text == "0" ? "-" : txt_hijosvivos.Text;
                if (paciente.PAC_GENERO == "M")
                {
                    reporte.ForApFum = "--------"; reporte.ForApFup = "--------"; reporte.ForApFuc = "--------";
                }
                else
                {
                    reporte.ForApFum = dtp_ultimaMenst.Checked == true ? Convert.ToString(dtp_ultimaMenst.Value) : "--------";
                    reporte.ForApFup = dtp_ultimoParto.Enabled == true ? Convert.ToString(dtp_ultimoParto.Value) : "--------";
                    reporte.ForApFuc = dtp_ultimaCito.Checked == true ? Convert.ToString(dtp_ultimaCito.Value) : "--------";
                }
                //(Convert.ToString(dtp_ultimoParto.Enabled == true));   

                //Convert.ToString(dtp_ultimaMenst.Value);                    

                //Convert.ToString(dtp_ultimaCito.Value);                    


                reporte.ForApBiopsia = chk_biopsia.Checked == true ? "X" : "-";
                reporte.ForApMetodoPlanifiFamiliar = txt_prevencion.Text;
                reporte.ForApTerapiaHormonal = chk_terapia.Checked == true ? "X" : "-";
                reporte.ForApColposCopia = chk_colcoscopia.Checked == true ? "X" : "-";
                reporte.ForApMamografia = chk_mamografia.Checked == true ? "X" : "-";

                //LLena los campos de Antecedente Personales
                string antecedentesPersonales = " ";
                for (int i = 0; i < dtg_antec_personales.Rows.Count -1; i++)
                {
                    if (dtg_antec_personales.Rows[i].Cells[1].Value != null)
                    {
                        antecedentesPersonales += dtg_antec_personales.Rows[i].Cells[1].Value.ToString() + " - " + dtg_antec_personales.Rows[i].Cells[2].Value.ToString() + "\t\t";
                    }
                }
               
                reporte.ForApDescripcion = (antecedentesPersonales).Replace("'", "´");
                //LLena los campos de Antecedente Familiares
                string antecedentesFamiliares = " ";
                reporte.ForAfCardiopatia = "-";
                reporte.ForAfDiabetes = "-";
                reporte.ForAfEnfvasculares = "-";
                reporte.ForAfHipertension = "-";
                reporte.ForAfCancer = "-";
                reporte.ForAfTuberculosis = "-";
                reporte.ForAfEnfmental = "-";
                reporte.ForAfEnfinfecciosa = "-";
                reporte.ForAfMalinfor = "-";
                reporte.ForAfOtro = "-";

                HC_CATALOGOS cat = new HC_CATALOGOS();
                List<HC_CATALOGOS> listaCatalogo = new List<HC_CATALOGOS>();
                listaCatalogo = NegCatalogos.RecuperarHcCatalogosPorTipo(Parametros.ReporteFormularios.codigoCatalogo_AFamiliares);
                foreach (DataGridViewRow item in dtg_antec_familiares.Rows)
                {
                    if (item.Cells[1].Value != null)
                    {
                        for (int i = 0; i < listaCatalogo.Count; i++)
                        {
                            cat = listaCatalogo.ElementAt(i);
                            if (cat.HCC_NOMBRE == item.Cells[1].Value.ToString())
                            {
                                if (i == 0)
                                    reporte.ForAfCardiopatia = i == 0 ? "X" : "";
                                if (i == 1)
                                    reporte.ForAfDiabetes = i == 1 ? "X" : "";
                                if (i == 2)
                                    reporte.ForAfEnfvasculares = i == 2 ? "X" : "";
                                if (i == 3)
                                    reporte.ForAfHipertension = i == 3 ? "X" : "";
                                if (i == 4)
                                    reporte.ForAfCancer = i == 4 ? "X" : "";
                                if (i == 5)
                                    reporte.ForAfTuberculosis = i == 5 ? "X" : "";
                                if (i == 6)
                                    reporte.ForAfEnfmental = i == 6 ? "X" : "";
                                if (i == 7)
                                    reporte.ForAfEnfinfecciosa = i == 7 ? "X" : "";
                                if (i == 8)
                                    reporte.ForAfMalinfor = i == 8 ? "X" : "";
                                if (i == 9)
                                    reporte.ForAfOtro = i == 9 ? "X" : "";
                                i = listaCatalogo.Count;
                            }
                        }
                    }
                }

                foreach (DataGridViewRow item in dtg_antec_familiares.Rows)
                {
                    if (item.Cells[1].Value != null)
                        antecedentesFamiliares += item.Cells[1].Value.ToString() + " - " + item.Cells[2].Value.ToString() + "\t\t";
                }
                //LLena los campos de Organos
                reporte.ForRaosSporganismoSentidos = "X";
                reporte.ForRaosSpprespiratorio = "X";
                reporte.ForRaosSpcardioVascular = "X";
                reporte.ForRaosSpdigestivo = "X";
                reporte.ForRaosSpgenital = "X";
                reporte.ForRaosSpurinario = "X";
                reporte.ForRaosSpmusculoEsqueletivo = "X";
                reporte.ForRaosSpendocrino = "X";
                reporte.ForRaosSphemoLinfatico = "X";
                reporte.ForRaosSpnervioso = "X";
                reporte.ForAfDescripcion = (antecedentesFamiliares).Replace("'", "´");
                reporte.ForEnfProAct = (txt_problema.Text).Replace("'", "´");
                //string cadenaR = txt_problema.Text;
                //int cont = 0;
                //foreach (string s in cadenaR.Split('\n'))
                //{
                //    cont++;
                //    if (cont < 15)
                //        reporte..ForEnfProAct = reporte..ForEnfProAct + "\n" + s;
                //}

                listaCatalogo = NegCatalogos.RecuperarHcCatalogosPorTipo(Parametros.ReporteFormularios.codigoCatalogo_OSistemas);
                foreach (DataGridViewRow item in dtg_organos.Rows)
                {
                    if (item.Cells[0].Value != null)
                    {
                        for (int i = 0; i < listaCatalogo.Count; i++)
                        {
                            cat = listaCatalogo.ElementAt(i);
                            if (cat.HCC_NOMBRE == item.Cells[1].Value.ToString())
                            {
                                if (i == 0)
                                {
                                    if ((bool)item.Cells[2].Value)
                                    {
                                        reporte.ForRaosCporganismoSentidos = i == 0 ? "X" : " ";
                                        reporte.ForRaosSporganismoSentidos = " ";
                                    }
                                    else
                                        reporte.ForRaosSporganismoSentidos = i == 0 ? "X" : " ";
                                }
                                if (i == 1)
                                {
                                    if ((bool)item.Cells[2].Value)
                                    {
                                        reporte.ForRaosCpprespiratorio = i == 1 ? "X" : " ";
                                        reporte.ForRaosSpprespiratorio = " ";
                                    }
                                    else
                                        reporte.ForRaosSpprespiratorio = i == 1 ? "X" : " ";
                                }
                                if (i == 2)
                                {
                                    if ((bool)item.Cells[2].Value)
                                    {
                                        reporte.ForRaosCpcardioVascular = i == 2 ? "X" : " ";
                                        reporte.ForRaosSpcardioVascular = " ";
                                    }
                                    else
                                        reporte.ForRaosSpcardioVascular = i == 2 ? "X" : " ";
                                }
                                if (i == 3)
                                {
                                    if ((bool)item.Cells[2].Value)
                                    {
                                        reporte.ForRaosCpdigestivo = i == 3 ? "X" : " ";
                                        reporte.ForRaosSpdigestivo = " ";
                                    }

                                    else
                                        reporte.ForRaosSpdigestivo = i == 3 ? "X" : " ";
                                }
                                if (i == 4)
                                {
                                    if ((bool)item.Cells[2].Value)
                                    {
                                        reporte.ForRaosCpgenital = i == 4 ? "X" : " ";
                                        reporte.ForRaosSpgenital = " ";
                                    }
                                    else
                                        reporte.ForRaosSpgenital = i == 4 ? "X" : " ";
                                }
                                if (i == 5)
                                {
                                    if ((bool)item.Cells[2].Value)
                                    {
                                        reporte.ForRaosCpurinario = i == 5 ? "X" : " ";
                                        reporte.ForRaosSpurinario = " ";
                                    }
                                    else
                                        reporte.ForRaosSpurinario = i == 5 ? "X" : " ";
                                }
                                if (i == 6)
                                {
                                    if ((bool)item.Cells[2].Value)
                                    {
                                        reporte.ForRaosCpmusculoEsqueletivo = i == 6 ? "X" : " ";
                                        reporte.ForRaosSpmusculoEsqueletivo = " ";
                                    }
                                    else
                                        reporte.ForRaosSpmusculoEsqueletivo = i == 6 ? "X" : " ";
                                }
                                if (i == 7)
                                {
                                    if ((bool)item.Cells[2].Value)
                                    {
                                        reporte.ForRaosCpendocrino = i == 7 ? "X" : " ";
                                        reporte.ForRaosSpendocrino = " ";
                                    }
                                    else
                                        reporte.ForRaosSpendocrino = i == 7 ? "X" : " ";
                                }
                                if (i == 8)
                                {
                                    if ((bool)item.Cells[2].Value)
                                    {
                                        reporte.ForRaosCphemoLinfatico = i == 8 ? "X" : " ";
                                        reporte.ForRaosSphemoLinfatico = " ";
                                    }
                                    else
                                        reporte.ForRaosSphemoLinfatico = i == 8 ? "X" : " ";
                                }
                                if (i == 9)
                                {
                                    if ((bool)item.Cells[2].Value)
                                    {
                                        reporte.ForRaosCpnervioso = i == 9 ? "X" : " ";
                                        reporte.ForRaosSpnervioso = " ";
                                    }
                                    else
                                        reporte.ForRaosSpnervioso = i == 9 ? "X" : " ";
                                }
                                i = listaCatalogo.Count;
                            }
                        }
                    }
                }
                string revisionActualOrganos = " ";
                foreach (DataGridViewRow item in dtg_organos.Rows)
                {
                    if (item.Cells[2].Value == null)
                        item.Cells[2].Value = "";
                    if (item.Cells[1].Value != null)
                        revisionActualOrganos += item.Cells[1].Value.ToString() + " - " + item.Cells[4].Value.ToString() + "\t\t";
                }
                reporte.ForRaosDescripcion = (revisionActualOrganos).Replace("'", "´");
                reporte.ForSvmPresionArterialuno = txt_presionA.Text != string.Empty ? Convert.ToInt32(txt_presionA.Text) : 0;
                reporte.ForSvmPresionArterialdos = txt_presionB.Text != string.Empty ? Convert.ToInt32(txt_presionB.Text) : 0;
                reporte.ForSvmFrecuenciaCardiaca = txt_frecCardiaca.Text != string.Empty ? Convert.ToInt32(txt_frecCardiaca.Text) : 0;
                reporte.ForSvmFrecuenciaRespiratoria = txt_frecRespiratoria.Text != string.Empty ? Convert.ToInt32(txt_frecRespiratoria.Text) : 0;
                reporte.ForSvmTempRbucal = txt_tempBucal.Text != string.Empty ? Convert.ToDouble(txt_tempBucal.Text) : 0;
                reporte.ForSvmTempRaxilar = txt_tempAxilar.Text != string.Empty ? Convert.ToDouble(txt_tempAxilar.Text) : 0;
                reporte.ForSvmPeso = txt_peso.Text != string.Empty ? Convert.ToDouble(txt_peso.Text) : 0;
                reporte.ForSvmTalla = txt_talla.Text != string.Empty ? Convert.ToDouble(txt_talla.Text) : 0;
                reporte.ForSvmPerimetroCefalico = txt_perimetro.Text != string.Empty ? Convert.ToDouble(txt_perimetro.Text) : 0;

                //LLena los campos de Examen Físico
                reporte.ForExamenFisicoPielSpfaneras = "X";
                reporte.ForExamenFisicoSpcabeza = "X";
                reporte.ForExamenFisicoSpojos = "X";
                reporte.ForExamenFisicoSpoidos = "X";
                reporte.ForExamenFisicoSpnariz = "X";
                reporte.ForExamenFisicoSpboca = "X";
                reporte.ForExamenFisicoOroSpfaringe = "X";
                reporte.ForExamenFisicoSpcuello = "X";
                reporte.ForExamenFisicoSpaxilasMamas = "X";
                reporte.ForExamenFisicoSptorax = "X";
                reporte.ForExamenFisicoSpabdomen = "X";
                reporte.ForExamenFisicoSpcolVer = "X";
                reporte.ForExamenFisicoSpingPerine = "X";
                reporte.ForExamenFisicoSpmiembSuper = "X";
                reporte.ForExamenFisicoSpmiembInf = "X";
                reporte.ForExamenFisicoSporgSentidos = "X";
                reporte.ForExamenFisicoSprespiratorio = "X";
                reporte.ForExamenFisicoSpcardioVasc = "X";
                reporte.ForExamenFisicoSpdigestivo = "X";
                reporte.ForExamenFisicoSpgenital = "X";
                reporte.ForExamenFisicoSpurinario = "X";
                reporte.ForExamenFisicoSpmuscEsquel = "X";
                reporte.ForExamenFisicoSpendocrino = "X";
                reporte.ForExamenFisicoSphemoLinfat = "X";
                reporte.ForExamenFisicoSpneurologico = "X";
                //
                listaCatalogo = NegCatalogos.RecuperarHcCatalogosPorTipo(Parametros.ReporteFormularios.codigoCatalogo_ExamenFisico);
                foreach (DataGridViewRow item in dtg_examenFisico.Rows)
                {
                    if (item.Cells[1].Value != null)
                    {
                        for (int i = 0; i < listaCatalogo.Count; i++)
                        {
                            cat = listaCatalogo.ElementAt(i);
                            if (cat.HCC_NOMBRE == item.Cells[1].Value.ToString())
                            {
                                if (i == 0)
                                {
                                    if ((bool)item.Cells[2].Value)
                                    {
                                        reporte.ForExamenFisicoPielCpfaneras = i == 0 ? "X" : " ";
                                        reporte.ForExamenFisicoPielSpfaneras = " ";
                                    }
                                    else
                                        reporte.ForExamenFisicoPielSpfaneras = i == 0 ? "X" : " ";
                                }
                                if (i == 1)
                                {
                                    if ((bool)item.Cells[2].Value)
                                    {
                                        reporte.ForExamenFisicoCpcabeza = i == 1 ? "X" : " ";
                                        reporte.ForExamenFisicoSpcabeza = " ";
                                    }
                                    else
                                        reporte.ForExamenFisicoSpcabeza = i == 1 ? "X" : " ";
                                }
                                if (i == 2)
                                {
                                    if ((bool)item.Cells[2].Value)
                                    {
                                        reporte.ForExamenFisicoCpojos = i == 2 ? "X" : " ";
                                        reporte.ForExamenFisicoSpojos = " ";
                                    }
                                    else
                                        reporte.ForExamenFisicoSpojos = i == 2 ? "X" : " ";
                                }
                                if (i == 3)
                                {
                                    if ((bool)item.Cells[2].Value)
                                    {
                                        reporte.ForExamenFisicoCpoidos = i == 3 ? "X" : " ";
                                        reporte.ForExamenFisicoSpoidos = " ";
                                    }
                                    else
                                        reporte.ForExamenFisicoSpoidos = i == 3 ? "X" : " ";
                                }
                                if (i == 4)
                                {
                                    if ((bool)item.Cells[2].Value)
                                    {
                                        reporte.ForExamenFisicoCpnariz = i == 4 ? "X" : " ";
                                        reporte.ForExamenFisicoSpnariz = " ";
                                    }
                                    else
                                        reporte.ForExamenFisicoSpnariz = i == 4 ? "X" : " ";
                                }
                                if (i == 5)
                                {
                                    if ((bool)item.Cells[2].Value)
                                    {
                                        reporte.ForExamenFisicoCpboca = i == 5 ? "X" : " ";
                                        reporte.ForExamenFisicoSpboca = " ";
                                    }
                                    else
                                        reporte.ForExamenFisicoSpboca = i == 5 ? "X" : " ";
                                }
                                if (i == 6)
                                {
                                    if ((bool)item.Cells[2].Value)
                                    {
                                        reporte.ForExamenFisicoOroCpfaringe = i == 6 ? "X" : " ";
                                        reporte.ForExamenFisicoOroSpfaringe = " ";
                                    }
                                    else
                                        reporte.ForExamenFisicoOroSpfaringe = i == 6 ? "X" : " ";
                                }
                                if (i == 7)
                                {
                                    if ((bool)item.Cells[2].Value)
                                    {
                                        reporte.ForExamenFisicoCpcuello = i == 7 ? "X" : " ";
                                        reporte.ForExamenFisicoSpcuello = " ";
                                    }
                                    else
                                        reporte.ForExamenFisicoSpcuello = i == 7 ? "X" : " ";
                                }
                                if (i == 8)
                                {
                                    if ((bool)item.Cells[2].Value)
                                    {
                                        reporte.ForExamenFisicoCpaxilasMamas = i == 8 ? "X" : " ";
                                        reporte.ForExamenFisicoSpaxilasMamas = " ";
                                    }
                                    else
                                        reporte.ForExamenFisicoSpaxilasMamas = i == 8 ? "X" : " ";
                                }
                                if (i == 9)
                                {
                                    if ((bool)item.Cells[2].Value)
                                    {
                                        reporte.ForExamenFisicoCptorax = i == 9 ? "X" : " ";
                                        reporte.ForExamenFisicoSptorax = " ";
                                    }
                                    else
                                        reporte.ForExamenFisicoSptorax = i == 9 ? "X" : " ";
                                }

                                if (i == 10)
                                {
                                    if ((bool)item.Cells[2].Value)
                                    {
                                        reporte.ForExamenFisicoCpabdomen = i == 10 ? "X" : " ";
                                        reporte.ForExamenFisicoSpabdomen = " ";
                                    }
                                    else
                                        reporte.ForExamenFisicoSpabdomen = i == 10 ? "X" : " ";
                                }
                                if (i == 11)
                                {
                                    if ((bool)item.Cells[2].Value)
                                    {
                                        reporte.ForExamenFisicoCpcolVer = i == 11 ? "X" : " ";
                                        reporte.ForExamenFisicoSpcolVer = " ";
                                    }
                                    else
                                        reporte.ForExamenFisicoSpcolVer = i == 11 ? "X" : " ";
                                }
                                if (i == 12)
                                {
                                    if ((bool)item.Cells[2].Value)
                                    {
                                        reporte.ForExamenFisicoCpingPerine = i == 12 ? "X" : " ";
                                        reporte.ForExamenFisicoSpingPerine = " ";
                                    }
                                    else
                                        reporte.ForExamenFisicoSpingPerine = i == 12 ? "X" : " ";
                                }
                                if (i == 13)
                                {
                                    if ((bool)item.Cells[2].Value)
                                    {
                                        reporte.ForExamenFisicoCpmiembSuper = i == 13 ? "X" : " ";
                                        reporte.ForExamenFisicoSpmiembSuper = " ";
                                    }
                                    else
                                        reporte.ForExamenFisicoSpmiembSuper = i == 13 ? "X" : " ";
                                }
                                if (i == 14)
                                {
                                    if ((bool)item.Cells[2].Value)
                                    {
                                        reporte.ForExamenFisicoCpmiembInf = i == 14 ? "X" : " ";
                                        reporte.ForExamenFisicoSpmiembInf = " ";
                                    }
                                    else
                                        reporte.ForExamenFisicoSpmiembInf = i == 14 ? "X" : " ";
                                }
                                if (i == 15)
                                {
                                    if ((bool)item.Cells[2].Value)
                                    {
                                        reporte.ForExamenFisicoCporgSentidos = i == 15 ? "X" : " ";
                                        reporte.ForExamenFisicoSporgSentidos = " ";
                                    }
                                    else
                                        reporte.ForExamenFisicoSporgSentidos = i == 15 ? "X" : " ";
                                }
                                if (i == 16)
                                {
                                    if ((bool)item.Cells[2].Value)
                                    {
                                        reporte.ForExamenFisicoCprespiratorio = i == 16 ? "X" : " ";
                                        reporte.ForExamenFisicoSprespiratorio = " ";
                                    }
                                    else
                                        reporte.ForExamenFisicoSprespiratorio = i == 16 ? "X" : " ";
                                }
                                if (i == 17)
                                {
                                    if ((bool)item.Cells[2].Value)
                                    {
                                        reporte.ForExamenFisicoCpcardioVasc = i == 17 ? "X" : " ";
                                        reporte.ForExamenFisicoSpcardioVasc = " ";
                                    }
                                    else
                                        reporte.ForExamenFisicoSpcardioVasc = i == 17 ? "X" : " ";
                                }
                                if (i == 18)
                                {
                                    if ((bool)item.Cells[2].Value)
                                    {
                                        reporte.ForExamenFisicoCpdigestivo = i == 18 ? "X" : " ";
                                        reporte.ForExamenFisicoSpdigestivo = " ";
                                    }
                                    else
                                        reporte.ForExamenFisicoSpdigestivo = i == 18 ? "X" : " ";
                                }
                                if (i == 19)
                                {
                                    if ((bool)item.Cells[2].Value)
                                    {
                                        reporte.ForExamenFisicoCpgenital = i == 19 ? "X" : " ";
                                        reporte.ForExamenFisicoSpgenital = " ";
                                    }
                                    else
                                        reporte.ForExamenFisicoSpgenital = i == 19 ? "X" : " ";
                                }

                                if (i == 20)
                                {
                                    if ((bool)item.Cells[2].Value)
                                    {
                                        reporte.ForExamenFisicoCpurinario = i == 20 ? "X" : " ";
                                        reporte.ForExamenFisicoSpurinario = " ";
                                    }
                                    else
                                        reporte.ForExamenFisicoSpurinario = i == 20 ? "X" : " ";
                                }
                                if (i == 21)
                                {
                                    if ((bool)item.Cells[2].Value)
                                    {
                                        reporte.ForExamenFisicoCpmuscEsquel = i == 21 ? "X" : " ";
                                        reporte.ForExamenFisicoSpmuscEsquel = " ";
                                    }
                                    else
                                        reporte.ForExamenFisicoSpmuscEsquel = i == 21 ? "X" : " ";
                                }
                                if (i == 22)
                                {
                                    if ((bool)item.Cells[2].Value)
                                    {
                                        reporte.ForExamenFisicoCpendocrino = i == 22 ? "X" : " ";
                                        reporte.ForExamenFisicoSpendocrino = " ";
                                    }
                                    else
                                        reporte.ForExamenFisicoSpendocrino = i == 22 ? "X" : " ";
                                }
                                if (i == 23)
                                {
                                    if ((bool)item.Cells[2].Value)
                                    {
                                        reporte.ForExamenFisicoCphemoLinfat = i == 23 ? "X" : " ";
                                        reporte.ForExamenFisicoSphemoLinfat = " ";
                                    }
                                    else
                                        reporte.ForExamenFisicoSphemoLinfat = i == 23 ? "X" : " ";
                                }
                                if (i == 24)
                                {
                                    if ((bool)item.Cells[2].Value)
                                    {
                                        reporte.ForExamenFisicoCpneurologico = i == 24 ? "X" : " ";
                                        reporte.ForExamenFisicoSpneurologico = " ";
                                    }
                                    else
                                        reporte.ForExamenFisicoSpneurologico = i == 24 ? "X" : " ";
                                }

                                i = listaCatalogo.Count;
                            }
                        }
                    }
                }
                string examenFisicoDescripcion = " ";
                foreach (DataGridViewRow item in dtg_examenFisico.Rows)
                {
                    if (item.Cells[1].Value != null)
                        examenFisicoDescripcion += item.Cells[1].Value.ToString() + " - " + item.Cells[4].Value.ToString() + "\t\t";
                }
                reporte.ForExamenFisicoDescripcion = (examenFisicoDescripcion + "\n" + "IMC : " + txtIndiceMasaCorporal.Text).Replace("'", "´");
                //LLena los campos de Diagnóstico CIE10
                dtg_diagnosticos.Refresh();
                for (int i = 0; i < dtg_diagnosticos.Rows.Count - 1; i++)
                {
                    if (i == 0)
                    {
                        reporte.ForDiagnosticoCieUno = dtg_diagnosticos.Rows[0].Cells[1].Value.ToString();
                        reporte.ForDiagnosticoCieUnoDesc = (dtg_diagnosticos.Rows[0].Cells[2].Value.ToString()).Replace("'", "´");
                        if ((bool)dtg_diagnosticos.Rows[i].Cells[3].Value) 
                            reporte.ForDiagnosticoCieUnoPre = dtg_diagnosticos.Rows[0].Cells[3].Value != null ? "X" : " ";
                        else
                            reporte.ForDiagnosticoCieUnoDef = dtg_diagnosticos.Rows[0].Cells[4].Value != null ? "X" : " ";
                    }
                    if (i == 1)
                    {
                        reporte.ForDiagnosticoCieDos = dtg_diagnosticos.Rows[1].Cells[1].Value.ToString();
                        reporte.ForDiagnosticoCieDosDesc = (dtg_diagnosticos.Rows[1].Cells[2].Value.ToString()).Replace("'", "´");
                        if ((bool)dtg_diagnosticos.Rows[i].Cells[3].Value)
                            reporte.ForDiagnosticoCieDosPre = dtg_diagnosticos.Rows[1].Cells[3].Value != null ? "X" : " ";
                        else
                            reporte.ForDiagnosticoCieDosDef = dtg_diagnosticos.Rows[1].Cells[4].Value != null ? "X" : " ";
                    }
                    if (i == 2)
                    {
                        reporte.ForDiagnosticoCieTres = dtg_diagnosticos.Rows[2].Cells[1].Value.ToString();
                        reporte.ForDiagnosticoCieTresDesc = (dtg_diagnosticos.Rows[2].Cells[2].Value.ToString()).Replace("'", "´");
                        if ((bool)dtg_diagnosticos.Rows[i].Cells[3].Value)
                            reporte.ForDiagnosticoCieTresPre = dtg_diagnosticos.Rows[2].Cells[3].Value != null ? "X" : " ";
                        else
                            reporte.ForDiagnosticoCieTresDef = dtg_diagnosticos.Rows[2].Cells[4].Value != null ? "X" : " ";
                    }
                    if (i == 3)
                    {
                        reporte.ForDiagnosticoCieCuatro = dtg_diagnosticos.Rows[3].Cells[1].Value.ToString();
                        reporte.ForDiagnosticoCieCuatroDesc = (dtg_diagnosticos.Rows[3].Cells[2].Value.ToString()).Replace("'", "´");
                        if ((bool)dtg_diagnosticos.Rows[i].Cells[3].Value)
                            reporte.ForDiagnosticoCieCuatroPre = dtg_diagnosticos.Rows[3].Cells[3].Value != null ? "X" : " ";
                        else
                            reporte.ForDiagnosticoCieCuatroDef = dtg_diagnosticos.Rows[3].Cells[4].Value != null ? "X" : " ";
                    }
                    if (i == 4)
                    {
                        reporte.ForDiagnosticoCieCinco = dtg_diagnosticos.Rows[4].Cells[1].Value.ToString();
                        reporte.ForDiagnosticoCieCincoDec = (dtg_diagnosticos.Rows[4].Cells[2].Value.ToString()).Replace("'", "´");
                        if ((bool)dtg_diagnosticos.Rows[i].Cells[3].Value)
                            reporte.ForDiagnosticoCieCincoPre = dtg_diagnosticos.Rows[4].Cells[3].Value != null ? "X" : " ";
                        else
                            reporte.ForDiagnosticoCieCincoDef = dtg_diagnosticos.Rows[4].Cells[4].Value != null ? "X" : " ";
                    }
                    if (i == 5)
                    {
                        reporte.ForDiagnosticoCieSeis = dtg_diagnosticos.Rows[5].Cells[1].Value.ToString();
                        reporte.ForDiagnosticoCieSeisDesc = (dtg_diagnosticos.Rows[5].Cells[2].Value.ToString()).Replace("'", "´");
                        if ((bool)dtg_diagnosticos.Rows[i].Cells[3].Value)
                            reporte.ForDiagnosticoCieSeisPre = dtg_diagnosticos.Rows[5].Cells[3].Value != null ? "X" : " ";
                        else
                            reporte.ForDiagnosticoCieSeisDef = dtg_diagnosticos.Rows[5].Cells[4].Value != null ? "X" : " ";
                    }
                }
                reporte.ForPlanesTratamiento = (txt_tratamiento.Text).Replace("'", "´");
                reporte.ForFecha = txt_fecha.Text; // Convert.ToString(anamnesis.ANE_FECHA.ToString().Substring(0, 10));
                reporte.ForHora = txt_hora.Text;//Convert.ToString(anamnesis.ANE_FECHA.ToString().Substring(11, 5));
                reporte.ForNombreProf = ("Dr/a. " + NegUsuarios.RecuperaUsuario(anamnesis.ID_USUARIO).NOMBRES + " " + NegUsuarios.RecuperaUsuario(anamnesis.ID_USUARIO).APELLIDOS).Replace("'", "´");
                USUARIOS objUsuario = new USUARIOS();
                objUsuario = NegUsuarios.RecuperaUsuario(anamnesis.ID_USUARIO);
                reporte.ForCodProf = objUsuario.IDENTIFICACION;
                //reporte.ForCodProf = Convert.ToString(anamnesis.ID_USUARIO);
                reporte.ForHoja = "1";
                ReportesHistoriaClinica reporteAnamnesis = new ReportesHistoriaClinica();
                reporteAnamnesis.ingresarAnamnesis(reporte);
                frmReportes ventana = new frmReportes(1, "anamnesis");
                //ventana.Show();
                if(accion.Equals("reporte"))
                    ventana.Show();
                else
                {
                    CrearCarpetas_Srvidor("anamnesis");
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                    //pdf.campo2 = nombreContrato;
                    //pdf.campo3 = numPoliza;
                    //pdf.campo4 = nomAseg;
                    //pdf.campo5 = montoAseg;
                    //pdf.campo6 = telfAseg;
                    //pdf.campo7 = numContrato;
                    //pdf.campo8 = nomEmp;
                    //pdf.campo9 = telfEmp;
                    //pdf.campo10 = montoEmp;
                    pdf.generar();

                
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void dtg_antec_personales_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        if (dtg_antec_personales.CurrentRow != null)
                        {
                            if (dtg_antec_personales.CurrentRow.Cells["codigoAntPer"].Value != null)
                            {
                                Int32 codigoDetAnam = Convert.ToInt32(dtg_antec_personales.CurrentRow.Cells["codigoAntPer"].Value);
                                NegAnamnesisDetalle.eliminarAnamnesisDetalle(codigoDetAnam);
                                dtg_antec_personales.Rows.Remove(dtg_antec_personales.CurrentRow);
                                MessageBox.Show("registro eliminado exitosamente", "Inf", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception err) { MessageBox.Show(err.Message, "err", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dtg_antec_familiares_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        if (dtg_antec_familiares.CurrentRow != null)
                        {
                            if (dtg_antec_familiares.CurrentRow.Cells["codigoAntFam"].Value != null)
                            {
                                Int32 codigoDetAnam = Convert.ToInt32(dtg_antec_familiares.CurrentRow.Cells["codigoAntFam"].Value);
                                NegAnamnesisDetalle.eliminarAnamnesisDetalle(codigoDetAnam);
                                dtg_antec_familiares.Rows.Remove(dtg_antec_familiares.CurrentRow);
                                MessageBox.Show("registro eliminado exitosamente", "Inf", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception err) { MessageBox.Show(err.Message, "err", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dtg_organos_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        if (dtg_organos.CurrentRow != null)
                        {
                            if (dtg_organos.CurrentRow.Cells["codigoOrganos"].Value != null)
                            {
                                Int32 codigoDetAnam = Convert.ToInt32(dtg_organos.CurrentRow.Cells["codigoOrganos"].Value);
                                NegAnamnesisDetalle.eliminarAnamnesisDetalle(codigoDetAnam);
                                dtg_organos.Rows.Remove(dtg_organos.CurrentRow);
                                MessageBox.Show("registro eliminado exitosamente", "Inf", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception err) { MessageBox.Show(err.Message, "err", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dtg_examenFisico_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        if (dtg_examenFisico.CurrentRow != null)
                        {
                            if (dtg_examenFisico.CurrentRow.Cells["codigoExamen"].Value != null)
                            {
                                Int32 codigoDetAnam = Convert.ToInt32(dtg_examenFisico.CurrentRow.Cells["codigoExamen"].Value);
                                NegAnamnesisDetalle.eliminarAnamnesisDetalle(codigoDetAnam);
                                dtg_examenFisico.Rows.Remove(dtg_examenFisico.CurrentRow);
                                MessageBox.Show("registro eliminado exitosamente", "Inf", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception err) { MessageBox.Show(err.Message, "err", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

          

        private void txt_gesta_TextChanged(object sender, EventArgs e)
        {
            if (txt_gesta.Text != "" || txt_gesta.Text == "0")
            {
                bool estado;
                if (Convert.ToByte(txt_gesta.Text) > 0)
                    estado = true;
                else
                    estado = false;
                txt_partos.Enabled = estado;
                txt_abortos.Enabled = estado;
                txt_cesareas.Enabled = estado;
                txt_hijosvivos.Enabled = estado;
                dtp_ultimoParto.Enabled = estado;                
            }
        }

        private void txt_problema_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frm_BusquedaCIE10 busqueda = new frm_BusquedaCIE10();
            busqueda.ShowDialog();
            if (busqueda.codigo != null)
            {
                dtg_diagnosticos.Rows.Add(null,busqueda.codigo, busqueda.resultado, false, false);
                //DataGridViewRow fila = dtg_diagnosticos.CurrentRow;
                //fila.Cells[1].Value = busqueda.codigo;
                //fila.Cells[2].Value = busqueda.resultado;
                //fila.Cells[3].Value = false;
                //fila.Cells[4].Value = false;
            }
            dtg_diagnosticos.Focus();
        }

        private void txt_tempBucal_Leave(object sender, EventArgs e)
        {
            if(Convert.ToDouble(txt_tempBucal.Text) > 0 )
            {
                txt_tempAxilar.Text = (Convert.ToDouble(txt_tempBucal.Text) - 0.5).ToString();
            }
            else
            {
                txt_tempBucal.Focus();
            }
        }

        private void txt_tempAxilar_Leave(object sender, EventArgs e)
        {
            if(Convert.ToDouble(txt_tempAxilar.Text) > 0)
            {
                txt_tempBucal.Text = (Convert.ToDouble(txt_tempAxilar.Text) + 0.5).ToString();
            }
            else
            {
                txt_tempAxilar.Focus();
            }
        }

        private void txt_presionB_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (Convert.ToInt32(txt_presionA.Text) < Convert.ToInt32(txt_presionB.Text))
                    txt_presionB.Text = "0";
            }
        }

        private void dtg_diagnosticos_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (dtg_diagnosticos.CurrentRow.Cells[1].Value != null)
            {
                if (e.ColumnIndex == this.dtg_diagnosticos.Columns[3].Index)
                {
                    DataGridViewCheckBoxCell chkpres = (DataGridViewCheckBoxCell)this.dtg_diagnosticos.Rows[e.RowIndex].Cells[3];
                    if (chkpres.Value == null)
                        chkpres.Value = false;
                    else
                        chkpres.Value = true;

                    DataGridViewCheckBoxCell chkdef = (DataGridViewCheckBoxCell)this.dtg_diagnosticos.Rows[e.RowIndex].Cells[4];
                    chkdef.Value = false;
                }
                else
                {
                    if (e.ColumnIndex == this.dtg_diagnosticos.Columns[4].Index)
                    {
                        DataGridViewCheckBoxCell chkdef = (DataGridViewCheckBoxCell)this.dtg_diagnosticos.Rows[e.RowIndex].Cells[4];
                        if (chkdef.Value == null)
                            chkdef.Value = false;
                        else
                            chkdef.Value = true;

                        DataGridViewCheckBoxCell chkpres = (DataGridViewCheckBoxCell)this.dtg_diagnosticos.Rows[e.RowIndex].Cells[3];
                        chkpres.Value = false;
                    }
                }
            }
            else
            {
                MessageBox.Show("No se ha agregado diagnostico en esta fila.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtg_diagnosticos.Rows.RemoveAt(dtg_diagnosticos.CurrentRow.Index);
            }
        }
    }
}


