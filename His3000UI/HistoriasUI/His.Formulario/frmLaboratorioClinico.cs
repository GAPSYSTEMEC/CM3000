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
using Infragistics.Win.UltraWinGrid;
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


namespace His.Formulario
{
    public partial class frmLaboratorioClinico : Form
    {
        #region Variables
        private int atencionId;             //codigo de la atencion del paciente
        private bool mostrarInfPaciente;    //si se mostrara el panel con la informacion del paciente
        ATENCIONES atencion = null;
        PACIENTES paciente = null;
        HC_EVOLUCION evolucion = null;
        HC_EVOLUCION_DETALLE ultimaNota = null;
        List<HC_PRESCRIPCIONES> listaPrescripciones = new List<HC_PRESCRIPCIONES>();
        List<DtoAtenciones> atenciones = new List<DtoAtenciones>();
        bool formulario = false;
        bool band = true;
        private int posicion;
        private int codigoAtencion;
        public int ate_codigo;
        private bool estadoGrid = false;
        private bool nuevo;

        List<PRODUCTO_ESTRUCTURA> listaProductoEstructura = new List<PRODUCTO_ESTRUCTURA>(); 

        #endregion


        public frmLaboratorioClinico()
        {
            InitializeComponent();
            inicializar();

            NegAtenciones atenciones = new NegAtenciones();
            string estado = atenciones.EstadoCuenta(Convert.ToString(codigoAtencion));
            if (estado != "1")
            {
                Bloquear();
            }
        }
        public void Bloquear()
        {
            btnNuevo.Enabled = true;
            btnGuardar.Enabled = false;
            btnEditar.Enabled = false;
            btnSalir.Enabled = false;
            btnImprimir.Enabled = false;
            ultraTabPageControl1.Enabled = false;
            ultraTabPageControl2.Enabled = false;
            ultraTabPageControl3.Enabled = false;
            ultraTabPageControl4.Enabled = false;
        }
        public frmLaboratorioClinico(int codigoAtencion, bool parMostrarInfPaciente)
        {
            atencion = NegAtenciones.AtencionID(codigoAtencion);
            ate_codigo = codigoAtencion;
            string mascara = string.Empty;

            for (int i = 0; i < GeneralPAR.TamNumAtencion; i++)
                mascara = mascara + "9";
            formulario = true;
            //atenciones = NegAtenciones.atencionesActivas();

            InitializeComponent();
            inicializarForma();
            deshabilitarCampos();
            cargarLaboratorio(atencion.ATE_CODIGO);
            //variables para obtener la inf del paciente
            atencionId = codigoAtencion;
            mostrarInfPaciente = parMostrarInfPaciente;
        }

        #region Cargar Datos

        private void habilitarCampos(bool nuevo, bool editar, bool guadar, bool cancelar, bool imprimir, bool salir)
        {
            btnGuardar.Enabled = guadar;
            btnCancelar.Enabled = cancelar;
            btnNuevo.Enabled = nuevo;
            btnEditar.Enabled = editar;
            btnSalir.Enabled = salir;
            btnImprimir.Enabled = imprimir;
        }

        private void deshabilitarCampos()
        {
            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
            btnNuevo.Enabled = true;
            btnEditar.Enabled = true;
            btnSalir.Enabled = true;

        }
        private void inicializar()
        {
            btnNuevo.Image = Recursos.Archivo.imgBtnAdd2;
            btnEditar.Image = Recursos.Archivo.imgBtnRestart;
            btnImprimir.Image = Recursos.Archivo.imgBtnGonePrint48;
            btnGuardar.Image = Recursos.Archivo.imgBtnGoneSave48;
            btnSalir.Image = Recursos.Archivo.imgBtnSalir32;
            btnCancelar.Image = Recursos.Archivo.imgCancelar1;
        }

        private void inicializarForma()
        {
            btnNuevo.Image = Recursos.Archivo.imgBtnAdd2;
            btnEditar.Image = Recursos.Archivo.imgBtnRestart;
            btnImprimir.Image = Recursos.Archivo.imgBtnGonePrint48;
            btnGuardar.Image = Recursos.Archivo.imgBtnGoneSave48;
            btnSalir.Image = Recursos.Archivo.imgBtnSalir32;
            btnCancelar.Image = Recursos.Archivo.imgCancelar1;
            //ultraPictureBox1.Image = Recursos.Archivo.F1;
            //ultraPictureBox1.Visible = false;


            ////txtNumAtencion.ReadOnly = true;
            ////txt_pacHCL.ReadOnly = true;
            ////txt_pacNombre.ReadOnly = true;
            //txtMedico.ReadOnly = true;
            ////txtSexo.ReadOnly = true;
            ////txtEdad.ReadOnly = true;

            ////txtNumAtencion.BackColor = Color.White;
            ////txt_pacHCL.BackColor = Color.White;
            ////txtSexo.BackColor = Color.White;
            ////txtEdad.BackColor = Color.White;
        }

        private void cargarLaboratorio(int codigoAtencion)
        {
            try
            {

                
                atencion = NegAtenciones.RecuperarAtencionID(codigoAtencion);

                if (atencion != null)
                {
                    paciente = NegPacientes.recuperarPacientePorAtencion(atencion.ATE_CODIGO);

                    evolucion = NegEvolucion.recuperarEvolucionPorAtencion(atencion.ATE_CODIGO);

                    habilitarCampos(false, true, false, false, true, true);
                }
                else
                {
                    //txt_pacNombre.Text = string.Empty;
                    //txt_pacHCL.Text = string.Empty;
                    paciente = null;
                    habilitarCampos(true, false, false, false, false, true);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        #endregion


#region

        private void limpiarCamposGrid()
        {
            for (Int16 i = 0; i < gridExamenes.Rows.Count; i++)
            {
                //for (Int16 j = 0; j < gridExamenes.Rows[i].ChildBands[0].Rows.Count; j++)
                //{
                //    gridExamenes.Rows[i].ChildBands[0].Rows[j].Cells["columnaCheck"].Value = false;
                //    gridExamenes.Rows[i].ChildBands[0].Rows[j].Cells["columnaPrecio"].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                //    gridExamenes.Rows[i].ChildBands[0].Rows[j].Cells["columnaPorcentaje"].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                //    gridExamenes.Rows[i].ChildBands[0].Rows[j].Cells["columnaPrecio"].Value = null;
                //    gridExamenes.Rows[i].ChildBands[0].Rows[j].Cells["columnaPorcentaje"].Value = null;
                //}
            }
        }

#endregion

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLaboratorioClinico_Load(object sender, EventArgs e)
        {
            mostrarInfPaciente = true;
            if (mostrarInfPaciente == true)
            {
                //Añado el panel con la informaciòn del paciente
                InfPaciente infPaciente = new InfPaciente(ate_codigo);//Cambios Edgar 20210310 no funcionaba
                panelInfPaciente.Controls.Add(infPaciente);
                //cambio las dimenciones de los paneles
                panelInfPaciente.Size = new Size(panelInfPaciente.Width, 110);
                //pantab1.Top = 125;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                gridExamenes.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.True;
                limpiarCamposGrid();
                nuevo = true;
                List<DtoLaboratorioEstructura> listaProEstruc = NegProducto.RecuperarProductoSub(Convert.ToInt16(411));

                //var query = (from p in listaProEstruc
                //             group p by p.AREA into grupo
                //             select new { grupo.Key, detalle = grupo }).ToList();
               
                
                //List<PRODUCTO> listaProductos = NegProducto.RecuperarProductoSubDiv(His.Parametros.CuentasPacientes.CodigoLaboratorioClinico);
                gridExamenes.DataSource = listaProEstruc;
                    habilitarCampos(false, true, false, false,true,true);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridExamenes_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            //if (estadoGrid == false)
            //{
            //    gridExamenes.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            //    gridExamenes.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            //    gridExamenes.DisplayLayout.Bands[2].Hidden = true;

            //    //Añado la columna check
            //    gridExamenes.DisplayLayout.Bands[1].Columns.Add("columnaCheck", "");
            //    gridExamenes.DisplayLayout.Bands[1].Columns["columnaCheck"].DataType = typeof(bool);
            //    gridExamenes.DisplayLayout.Bands[1].Columns["columnaCheck"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;

            //    gridExamenes.DisplayLayout.Bands[1].Columns.Add("columnaPrecio", "");
            //    gridExamenes.DisplayLayout.Bands[1].Columns["columnaPrecio"].DataType = typeof(Decimal);
            //    gridExamenes.DisplayLayout.Bands[1].Columns["columnaPrecio"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CurrencyNonNegative;
            //    gridExamenes.DisplayLayout.Bands[1].Columns["columnaPrecio"].Header.Caption = "Precio";

            //    //gridExamenes.DisplayLayout.Bands[1].Columns.Add("columnaPorcentaje", "");
            //    //gridExamenes.DisplayLayout.Bands[1].Columns["columnaPorcentaje"].DataType = typeof(Decimal);
            //    //gridExamenes.DisplayLayout.Bands[1].Columns["columnaPorcentaje"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CurrencyNonNegative;
            //    //gridExamenes.DisplayLayout.Bands[1].Columns["columnaPorcentaje"].Header.Caption = "Porcentaje";

            //    gridExamenes.DisplayLayout.Bands[0].Override.CellAppearance.BackColor = Color.LightCyan;
            //    gridExamenes.DisplayLayout.Bands[0].Override.CellAppearance.BackColor2 = Color.Azure;
            //    gridExamenes.DisplayLayout.Bands[0].Override.CellAppearance.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;

            //    estadoGrid = true;
            //}
            //gridExamenes.DisplayLayout.Bands[0].Columns["PRO_CODIGO"].Hidden = true;
            //gridExamenes.DisplayLayout.Bands[1].Columns["PRO_DESCRIPCION"].Hidden = true;
            //gridExamenes.DisplayLayout.Bands[1].Columns["PRO"].Hidden = true;
            ////
            //gridExamenes.DisplayLayout.Bands[0].Columns["CCT_NOMBRE"].Header.Caption = "Tipo Costo";
            //gridExamenes.DisplayLayout.Bands[1].Columns["CAC_NOMBRE"].Header.Caption = "Descripcion";

            ////
            //gridExamenes.DisplayLayout.Bands[1].Columns["columnaCheck"].Header.VisiblePosition = 2;
            //gridExamenes.DisplayLayout.Bands[1].Columns["CAC_NOMBRE"].Header.VisiblePosition = 1;
            //gridExamenes.DisplayLayout.Bands[1].Columns["columnaPrecio"].Header.VisiblePosition = 3;
            //gridExamenes.DisplayLayout.Bands[1].Columns["columnaPorcentaje"].Header.VisiblePosition = 4;

            ////
            //gridExamenes.DisplayLayout.Bands[1].Columns["columnaCheck"].Width = 30;
            //gridExamenes.DisplayLayout.Bands[1].Columns["columnaPrecio"].Width = 70;
            //gridExamenes.DisplayLayout.Bands[1].Columns["columnaPorcentaje"].Width = 70;
            //gridExamenes.DisplayLayout.Bands[1].Columns["CAC_NOMBRE"].Width = 380;

            ////
            //gridExamenes.DisplayLayout.Bands[0].Columns["CCT_NOMBRE"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ////gridPconvenios.DisplayLayout.Bands[1].Columns["columnaPrecio"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ////gridPconvenios.DisplayLayout.Bands[1].Columns["columnaPorcentaje"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            //gridExamenes.DisplayLayout.Bands[1].Columns["CAC_NOMBRE"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            //for (Int16 i = 0; i < gridExamenes.Rows.Count; i++)
            //{
            //    for (Int16 j = 0; j < gridExamenes.Rows[i].ChildBands[0].Rows.Count; j++)
            //    {
            //        gridExamenes.Rows[i].ChildBands[0].Rows[j].Cells["columnaPrecio"].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            //        gridExamenes.Rows[i].ChildBands[0].Rows[j].Cells["columnaPorcentaje"].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            //    }
            //}
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }
        MaskedTextBox codMedico;
        MEDICOS med = null;
        private void btnAyudaMedicoS_Click(object sender, EventArgs e)
        {
            List<MEDICOS> medicos = NegMedicos.listaMedicos();
            frm_Ayudas frm = new frm_Ayudas(medicos);
            frm.bandCampo = true;
            frm.ShowDialog();
            if (frm.campoPadre2.Text != string.Empty)
            {
                codMedico = (frm.campoPadre2);
                med = NegMedicos.RecuperaMedicoId(Convert.ToInt32(codMedico.Text));
                agregarMedico(med);
            }
        }
        private void agregarMedico(MEDICOS medicoTratante)
        {
            if ((medicoTratante != null))
            {
                txtSolicitante.Text = medicoTratante.MED_APELLIDO_PATERNO.Trim() + " " + medicoTratante.MED_APELLIDO_MATERNO.Trim()
                    + " " + medicoTratante.MED_NOMBRE1.Trim() + " " + medicoTratante.MED_NOMBRE2.Trim();
                //if (medicoTratante.MED_CODIGO_MEDICO != null)
                //    txt_CodMSPE.Text = medicoTratante.MED_CODIGO_MEDICO.ToString();
                //else
                //    txt_CodMSPE.Text = "0"; //no tiene codigo
            }

        }

        private void btnSalir_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
