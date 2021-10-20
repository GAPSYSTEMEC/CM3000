using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using His.Entidades;
using His.Negocio;

namespace His.Formulario
{
    public partial class frm_AyudaGeneral : Form
    {
        public string resultado = "";
        public string codigo = "";

        public bool tarifario = false;
        public bool quirofano = false;
        public frm_AyudaGeneral()
        {
            InitializeComponent();

            UltraGridDatos.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            UltraGridDatos.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            //Inicializo las opciones del timer por defecto
            timerBusqueda.Interval = 1500;

            txtBuscar.Focus();
        }

        private void UltraGridDatos_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            try
            {

                //UltraGridDatos.DisplayLayout.Bands[0].ColHeadersVisible = false;

                UltraGridDatos.DisplayLayout.Bands[0].Columns["CODIGO"].Width = 80;
                UltraGridDatos.DisplayLayout.Bands[0].Columns["DESCRIPCION"].Width = 450;
                //ulgdbListadoCIE.DisplayLayout.Bands[1].Columns["CIE_IDPADRE"].Hidden = true;


                UltraGridDatos.DisplayLayout.Bands[0].Columns["CODIGO"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                UltraGridDatos.DisplayLayout.Bands[0].Columns["DESCRIPCION"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                //ulgdbListadoCIE.DisplayLayout.Bands[1].Columns["CIE_CODIGO"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                //ulgdbListadoCIE.DisplayLayout.Bands[1].Columns["CIE_DESCRIPCION"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

                //ulgdbListadoCIE.DisplayLayout.Bands[1].Columns["CIE_DESCRIPCION"].CellMultiLine = Infragistics.Win.DefaultableBoolean.True;

                UltraGridDatos.DisplayLayout.Bands[0].Override.CellAppearance.BackColor = Color.LightCyan;
                UltraGridDatos.DisplayLayout.Bands[0].Override.CellAppearance.BackColor2 = Color.Azure;
                UltraGridDatos.DisplayLayout.Bands[0].Override.CellAppearance.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;

                //e.Layout.Rows.ExpandAllCards();
                //e.Row.ExpansionIndicator = Infragistics.Win.UltraWinGrid.ShowExpansionIndicator.Never;
                //e.Row.Expanded = true;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void UltraGridDatos_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                resultado = UltraGridDatos.ActiveRow.Cells[1].Text;
                codigo = UltraGridDatos.ActiveRow.Cells[0].Text;
                this.Close();
            }
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (chkAutoBusqueda.Checked == true)
            {
                if (!timerBusqueda.Enabled)
                {
                    timerBusqueda.Start();
                }
            }
        }

        private void chkAutoBusqueda_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoBusqueda.Checked)
            {
                timerBusqueda.Enabled = true;
            }
            else
            {
                timerBusqueda.Stop();
                timerBusqueda.Enabled = false;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (tarifario == true)
                Tarifarios();
            else if (quirofano == true)
                Quirofano();
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            if (timerBusqueda.Enabled)
            {
                timerBusqueda.Stop();
            }
        }

        private void UltraGridDatos_DoubleClickCell(object sender, Infragistics.Win.UltraWinGrid.DoubleClickCellEventArgs e)
        {
            if (UltraGridDatos.ActiveRow.Index > -1)
            {
                resultado = UltraGridDatos.ActiveRow.Cells[1].Value.ToString();
                codigo = UltraGridDatos.ActiveRow.Cells[0].Value.ToString();
            }
            this.Close();
        }

        private void frm_AyudaGeneral_Load(object sender, EventArgs e)
        {
            if (tarifario == true)
                Tarifarios();
            else if (quirofano == true)
                Quirofano();
        }

        public void Quirofano()
        {
            try
            {
                DataTable Quirofano = NegQuirofano.ProcedimientosCirugia(txtBuscar.Text, rdbPorCodigo.Checked, rdbPorDescripcion.Checked);
                UltraGridDatos.DataSource = Quirofano;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Tarifarios()
        {
            try
            {
                DataTable Tarifarios = NegTarifario.ListaTarifario(txtBuscar.Text, rdbPorCodigo.Checked, rdbPorDescripcion.Checked);

                UltraGridDatos.DataSource = Tarifarios;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void timerBusqueda_Tick(object sender, EventArgs e)
        {
            try
            {
                btnBuscar_Click(null, null);
                timerBusqueda.Stop();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
