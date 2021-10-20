using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using His.Negocio;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using System.IO;

namespace His.Dietetica
{
    public partial class frm_QuirofanoExploradorNew : Form
    {
        NegQuirofano Quirofano = new NegQuirofano();
        public bool modotablet = false;
        public frm_QuirofanoExploradorNew()
        {
            InitializeComponent();
        }

        private void toolStripActualizar_Click(object sender, EventArgs e)
        {
            CargarQuirofanoPacientes();
        }
        public void CargarQuirofanoPacientes()
        {
            try
            {
                UltraGridPacientes.DataSource = Quirofano.QuirofanoPacientes();
                RedimencionarTabla();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        #region Dimesiones de Grid
        public void RedimencionarTabla()
        {
            try
            {
                UltraGridBand bandUno = UltraGridPacientes.DisplayLayout.Bands[0];

                UltraGridPacientes.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
                //grid.DisplayLayout.Override.Allow

                UltraGridPacientes.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                UltraGridPacientes.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                UltraGridPacientes.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

                bandUno.Override.CellClickAction = CellClickAction.RowSelect;
                bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

                UltraGridPacientes.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
                UltraGridPacientes.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
                UltraGridPacientes.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

                UltraGridPacientes.DisplayLayout.Override.DefaultRowHeight = 20; //Para el modo tablet

                //Caracteristicas de Filtro en la grilla
                UltraGridPacientes.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                UltraGridPacientes.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                UltraGridPacientes.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                UltraGridPacientes.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
                UltraGridPacientes.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
                //
                UltraGridPacientes.DisplayLayout.UseFixedHeaders = true;

                //Dimension los registros
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[0].Width = 60;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[1].Width = 60;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[2].Width = 300;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[3].Width = 100;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[4].Width = 80;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[5].Width = 140;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[6].Width = 400;

                //agrandamiento de filas 

                //Ocultar columnas, que son fundamentales al levantar informacion
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[3].Hidden = false;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[5].Hidden = false;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[6].Hidden = true;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[7].Hidden = true;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[8].Hidden = false;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[9].Hidden = true;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[10].Hidden = true;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[11].Hidden = true;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[12].Hidden = true;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[13].Hidden = true;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[14].Hidden = true;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[15].Hidden = true;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[16].Hidden = true;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[14].Hidden = true;
                //TablaProductos.DisplayLayout.Bands[0].Columns[12].Hidden = true;
                //TablaProductos.DisplayLayout.Bands[0].Columns[13].Hidden = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void RedimencionarTablet()
        {
            try
            {
                UltraGridBand bandUno = UltraGridPacientes.DisplayLayout.Bands[0];

                UltraGridPacientes.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
                //grid.DisplayLayout.Override.Allow

                UltraGridPacientes.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                UltraGridPacientes.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                UltraGridPacientes.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

                bandUno.Override.CellClickAction = CellClickAction.RowSelect;
                bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

                UltraGridPacientes.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
                UltraGridPacientes.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
                UltraGridPacientes.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

                //Agrandar las filas
                UltraGridPacientes.DisplayLayout.Override.DefaultRowHeight = 40; //Para el modo tablet

                //Caracteristicas de Filtro en la grilla
                UltraGridPacientes.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                UltraGridPacientes.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                UltraGridPacientes.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                UltraGridPacientes.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
                UltraGridPacientes.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
                //
                UltraGridPacientes.DisplayLayout.UseFixedHeaders = true;

                //Dimension los registros
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[0].Width = 70;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[1].Width = 60;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[2].Width = 300;
                //UltraGridPacientes.DisplayLayout.Bands[0].Columns[3].Width = 100;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[4].Width = 80;
                //UltraGridPacientes.DisplayLayout.Bands[0].Columns[5].Width = 140;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[6].Width = 400;

                //agrandamiento de filas 

                //Ocultar columnas, que son fundamentales al levantar informacion
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[3].Hidden = true;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[5].Hidden = true;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[7].Hidden = true;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[8].Hidden = false;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[9].Hidden = true;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[10].Hidden = true;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[11].Hidden = true;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[12].Hidden = true;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[13].Hidden = true;
                //UltraGridPacientes.DisplayLayout.Bands[0].Columns[14].Hidden = true;

                //TablaProductos.DisplayLayout.Bands[0].Columns[12].Hidden = true;
                //TablaProductos.DisplayLayout.Bands[0].Columns[13].Hidden = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        private void toolStripButtonTablet_Click(object sender, EventArgs e)
        {

            if (modotablet == false)
            {
                UltraGridPacientes.DataSource = Quirofano.QuirofanoPacientes();
                RedimencionarTablet();
                UltraGridPacientes.Focus();
                frmQuirofanoPedidoPaciente.modotablet = true;
                modotablet = true;
            }
            else
            {
                if (MessageBox.Show("¿Desea Salir de Modo Tablet?", "His3000", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                    == DialogResult.Yes)
                {
                    UltraGridPacientes.DataSource = Quirofano.QuirofanoPacientes();
                    RedimencionarTabla();
                    UltraGridPacientes.Focus();
                    frmQuirofanoPedidoPaciente.modotablet = false;
                    modotablet = false;
                }
            }
        }

        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButtonBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string PathExcel = FindSavePath();
                if (PathExcel != null)
                {
                    this.ultraGridExcelExporter1.Export(UltraGridPacientes, PathExcel);
                    MessageBox.Show("Se termino de exportar el grid en el archivo " + PathExcel);
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            finally
            { this.Cursor = Cursors.Default; }
        }

        #region Funciones y Constructores
        private String FindSavePath()
        {
            Stream myStream;
            string myFilepath = null;
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "excel files (*.xlsx)|*.xlsx";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                    {
                        myFilepath = saveFileDialog1.FileName;
                        myStream.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return myFilepath;
        }
        #endregion

        private void UltraGridPacientes_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void UltraGridPacientes_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            UltraGridBand band = this.UltraGridPacientes.DisplayLayout.Bands[0];
            foreach (UltraGridRow item in band.GetRowEnumerator(GridRowType.DataRow))
            {
                if (Convert.ToInt32(item.Cells[14].Value.ToString()) == 0 && Convert.ToInt32(item.Cells[15].Value.ToString()) == 0)
                {
                    item.Appearance.BackColor = Color.White; //No tiene procedimientos
                }
                if (Convert.ToInt32(item.Cells[14].Value.ToString()) > 1 && Convert.ToInt32(item.Cells[15].Value.ToString()) > 0)
                {
                    item.Appearance.BackColor = Color.FromArgb(255, 197, 201); // tiene mas de un procedimiento y uno o mas procedimientos cerrados
                }
                if (Convert.ToInt32(item.Cells[15].Value.ToString()) == Convert.ToInt32(item.Cells[16].Value.ToString()) && Convert.ToInt32(item.Cells[16].Value.ToString()) != 0)
                {
                    item.Appearance.BackColor = Color.FromArgb(255, 85, 137); //Tiene todos los procedimientos cerrados
                    item.Appearance.ForeColor = Color.White;
                }
                if (Convert.ToInt32(item.Cells[14].Value.ToString()) == 1 && Convert.ToInt32(item.Cells[15].Value.ToString()) == 0)
                {
                    item.Appearance.BackColor = Color.FromArgb(255, 210, 72); //Solo tiene un procedimiento
                }
                if (Convert.ToInt32(item.Cells[14].Value.ToString()) > 1 && Convert.ToInt32(item.Cells[15].Value.ToString()) == 0)
                {
                    item.Appearance.BackColor = Color.FromArgb(176, 245, 164); //Tiene mas de un solo procedimiento
                }
            }
        }

        private void UltraGridPacientes_MouseClick(object sender, MouseEventArgs e)
        {

            //if(e.Button == MouseButtons.Right)
            //{


            //}
            if (UltraGridPacientes.Selected.Rows.Count == 1)
            {
                UltraGridRow Fila = UltraGridPacientes.ActiveRow;
                //paciente = Fila.Cells[2].Value.ToString();
                //cie_descripcion = Fila.Cells[6].Value.ToString();
                string ate_codigo = Fila.Cells[7].Value.ToString();
                string pac_codigo = Fila.Cells[6].Value.ToString();
                //cie_codigo = Fila.Cells[7].Value.ToString();
                if (e.Button == MouseButtons.Right)
                {
                    Point mousepoint = new Point(e.X, e.Y);
                    contextMenuStrip1.Show(UltraGridPacientes, mousepoint);
                }
            }
        }

        private void nuevoProcedimientoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UltraGridRow Fila = UltraGridPacientes.ActiveRow;
            if (UltraGridPacientes.Selected.Rows.Count == 1)
            {
                frmayudaProcedimiento.ate_codigo = Fila.Cells[7].Value.ToString();
                frmayudaProcedimiento.pedidopaciente = true;
                frmayudaProcedimiento x = new frmayudaProcedimiento();
                x.Show();

                //CargarQuirofanoPacientes();
            }
            else
            {
                MessageBox.Show("Debe elegir un paciente para continuar...", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void frm_QuirofanoExploradorNew_Load(object sender, EventArgs e)
        {
            CargarQuirofanoPacientes();
        }

        private void verProcedimientosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UltraGridRow Fila = UltraGridPacientes.ActiveRow;
            if (UltraGridPacientes.Selected.Rows.Count == 1)
            {
                frmQuirofanoPedidoPaciente.ate_codigo = Fila.Cells[7].Value.ToString(); 
                frmQuirofanoPedidoPaciente.pac_codigo = Fila.Cells[6].Value.ToString();
                frmQuirofanoPedidoPaciente x = new frmQuirofanoPedidoPaciente();
                x.Show();
            }
            else
                MessageBox.Show("Debe eñegir un paciente para continuar...", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void toolStripButtonActualizar_Click(object sender, EventArgs e)
        {
        }
    }
}
