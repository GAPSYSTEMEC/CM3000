using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using Infragistics.Win.UltraWinGrid;
using His.Formulario;
using His.Entidades.Clases;
using System.Threading;

namespace His.Dietetica
{
    
    public partial class frmQuirofanoProductosDetalle : Form
    {
        NegQuirofano Quirofano = new NegQuirofano();
        NegCertificadoMedico Certificado = new NegCertificadoMedico();
        public DataTable TablaProductosReposicion = new DataTable();
        public DataTable TablaProductoPacientes = new DataTable();
        public frmQuirofanoProductosDetalle()
        {
            InitializeComponent();
        }

        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmQuirofanoProductosDetalle_Load(object sender, EventArgs e)
        {
            dtpDesde.Value = Convert.ToDateTime(String.Format("{0:g}", (DateTime.Now.Year).ToString() + "/" + DateTime.Now.Month + "/01"));
            dtpHasta.Value = DateTime.Now;
            try
            {
                cmbUsuarios.DataSource = NegQuirofano.UsuariosReposicion();
                cmbUsuarios.ValueMember = "CODIGO";
                cmbUsuarios.DisplayMember = "USUARIO";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo ocurrio al cargar los usuarios de reposicion: " + ex.Message, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            if (dtpDesde.Value < dtpHasta.Value)
            {
                try
                {
                    dtpHasta.Value = dtpHasta.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                    UltraGridProductos.DataSource = Quirofano.RecuperarProductosUsados(dtpDesde.Value, dtpHasta.Value, Convert.ToInt32(cmbUsuarios.SelectedValue.ToString()));
                    TablaProductosReposicion = NegQuirofano.RecuperoReposicion(dtpDesde.Value, dtpHasta.Value, Convert.ToInt32(cmbUsuarios.SelectedValue.ToString()));
                    TablaProductoPacientes = Quirofano.RecuperarProductosUsados(dtpDesde.Value, dtpHasta.Value, Convert.ToInt32(cmbUsuarios.SelectedValue.ToString()));
                    ultraGrid1.DataSource = NegQuirofano.DetalleExportar(dtpDesde.Value, dtpHasta.Value); //hay que preguntar si es valido que totalice todo sin tener en cuenta el usuario
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
                MessageBox.Show("Fecha \"Desde\" no puede mayor a fecha \"Hasta\"", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnexcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (UltraGridProductos.Rows.Count > 0)
                {
                    string PathExcel = FindSavePath();
                    if (PathExcel != null)
                    {
                        this.ultraGridExcelExporter1.Export(UltraGridProductos, PathExcel);
                        MessageBox.Show("Se termino de exportar el grid en el archivo " + PathExcel);
                    }
                }
                else
                {
                    MessageBox.Show("No tiene Registros para Exportar.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
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

        private void btnimprimir_Click(object sender, EventArgs e)
        {
            if(UltraGridProductos.Rows.Count > 0)
            {
                try
                {
                    string path = Certificado.path();
                    QuirofanoProductos QP = new QuirofanoProductos();
                    DataRow dr;
                    foreach (var item in UltraGridProductos.Rows)
                    {
                        dr = QP.Tables["Quirofano_Productos"].NewRow();
                        dr["Codigo"] = item.Cells[0].Value.ToString();
                        dr["Producto"] = item.Cells[1].Value.ToString();
                        dr["Cantidad"] = item.Cells[2].Value.ToString();
                        dr["Logo"] = path;
                        dr["Usuario"] = His.Entidades.Clases.Sesion.nomUsuario;
                        QP.Tables["Quirofano_Productos"].Rows.Add(dr);
                    }
                    frmReportes Reporte = new frmReportes(1, "QuirofanoProductos", QP);
                    Reporte.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }   
            }
        }

        private void UltraGridProductos_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            try
            {
                UltraGridBand bandUno = UltraGridProductos.DisplayLayout.Bands[0];

                UltraGridProductos.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
                //grid.DisplayLayout.Override.Allow

                UltraGridProductos.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                UltraGridProductos.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                UltraGridProductos.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

                bandUno.Override.CellClickAction = CellClickAction.RowSelect;
                bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

                UltraGridProductos.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
                UltraGridProductos.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
                UltraGridProductos.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

                UltraGridProductos.DisplayLayout.Override.DefaultRowHeight = 20; //Para el modo tablet

                //Caracteristicas de Filtro en la grilla
                UltraGridProductos.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                UltraGridProductos.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                UltraGridProductos.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                UltraGridProductos.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
                UltraGridProductos.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
                //
                UltraGridProductos.DisplayLayout.UseFixedHeaders = true;

                //Dimension los registros
                UltraGridProductos.DisplayLayout.Bands[0].Columns[0].Width = 50;
                UltraGridProductos.DisplayLayout.Bands[0].Columns[1].Width = 200;
                UltraGridProductos.DisplayLayout.Bands[0].Columns[2].Width = 50;
                UltraGridProductos.DisplayLayout.Bands[0].Columns[3].Width = 80;
                UltraGridProductos.DisplayLayout.Bands[0].Columns[4].Width = 80;
                UltraGridProductos.DisplayLayout.Bands[0].Columns[5].Width = 80;
                UltraGridProductos.DisplayLayout.Bands[0].Columns[8].Width = 400;

                //agrandamiento de filas 

                //Ocultar columnas, que son fundamentales al levantar informacion
                UltraGridProductos.DisplayLayout.Bands[0].Columns["ATE_CODIGO"].Hidden = false;
                UltraGridProductos.DisplayLayout.Bands[0].Columns["ATE_CODIGO"].Hidden = false;

                //UltraGridBand bandUno = UltraGridProductos.DisplayLayout.Bands[0];

                //UltraGridProductos.DisplayLayout.ViewStyle = ViewStyle.SingleBand;

                //bandUno.Override.CellClickAction = CellClickAction.RowSelect;
                //bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

                ////Caracteristicas de Filtro en la grilla
                ////ultraGridFormasPago.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                ////ultraGridFormasPago.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                ////ultraGridFormasPago.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                ////ultraGridFormasPago.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.RowAndCell;
                ////ultraGridFormasPago.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;

                //UltraGridProductos.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
                //UltraGridProductos.DisplayLayout.GroupByBox.Hidden = true;
                //UltraGridProductos.DisplayLayout.Bands[0].SortedColumns.Add("PACIENTE - PROCEDIMIENTO", false, true);
                //UltraGridProductos.DisplayLayout.Bands[0].Columns["PRODUCTO"].Width = 300;

                //bandUno.Columns["PRODUCTO"].Hidden = false;
                //bandUno.Columns["CODIGO"].Hidden = false;

                ////OCULTAR COLUMNAS 
                //bandUno.Columns["ATE_CODIGO"].Hidden = true;
                //bandUno.Columns["PCI_CODIGO"].Hidden = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public DateTime fechaReposicion;
        private void btnreposicion_Click(object sender, EventArgs e)
        {
            Error.Clear();
            if (txtobservacion.Text.Trim() != "")
            {
                if (MessageBox.Show("¿Está seguro de realizar la reposición?", "HIS3000", MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    fechaReposicion = DateTime.Now;
                    //SE GENERA LA REPOSICION AL SIC3000
                    GenerarReposicion();
                    this.Close();
                }
            }
            else
                Error.SetError(txtobservacion, "CAMPO OBLIGATORIO");
        }

        public void GenerarReposicion()
        {
            if(TablaProductosReposicion.Rows.Count > 0)
            {
                try
                {
                    Int64 numdoc = NegQuirofano.NumeroControl();
                    if (numdoc == 0)
                    {
                        Thread.Sleep(2000); //Espero 2 segundos
                        GenerarReposicion();
                    }
                    else
                    {
                        NegQuirofano.OcuparNumControl();
                        //numdoc += 1; //Se le suma uno
                        NegQuirofano.LiberarNumControl(); //Tomado el numdoc se libera el numero y se le suma 1 para que otro pueda seguir con el proceso
                        NegQuirofano.CreaPedidoReposicion(numdoc, fechaReposicion.Date, fechaReposicion.ToString("HH:mm"), 12, 10, txtobservacion.Text, 'A', Sesion.codUsuario);
                        int linea = 0;


                        foreach (DataRow item in TablaProductosReposicion.Rows)
                        {
                            linea += 1;
                            NegQuirofano.DetalleReposicion(item["CODIGO"].ToString(),
                                item["PRODUCTO"].ToString(), Convert.ToDouble(item["CANTIDAD"].ToString()), linea, numdoc);
                        }
                        MessageBox.Show("Reposición enviada con éxito", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);


                        foreach (DataRow item in TablaProductoPacientes.Rows)
                        {
                            if (item["F. REPOSI"].ToString() == "")
                            {
                                try
                                {
                                    //GUARDO LA FECHA Y EL NUMDOC DE LA REPOSICION  CON EL NUMERO DE ATENCION Y EL TIPO DE PROCEDIMIENTO.
                                    NegQuirofano.FechaReposicion(fechaReposicion, Convert.ToInt32(item["PCI_CODIGO"].ToString()),
                                        Convert.ToInt64(item["ATE_CODIGO"].ToString()), numdoc);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Algo ocurrio al generar la reposición." + ex.Message, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Algo ocurrio al crear el detalle de la reposición.\r\nMás detalle: " + ex.Message,
                        "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No tiene productos que deban hacerse la reposición.", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UltraGridProductos_InitializeLayout_1(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                UltraGridBand bandUno = UltraGridProductos.DisplayLayout.Bands[0];

                UltraGridProductos.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
                //grid.DisplayLayout.Override.Allow

                UltraGridProductos.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                UltraGridProductos.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                UltraGridProductos.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

                bandUno.Override.CellClickAction = CellClickAction.RowSelect;
                bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

                UltraGridProductos.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
                UltraGridProductos.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
                UltraGridProductos.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

                UltraGridProductos.DisplayLayout.Override.DefaultRowHeight = 20; //Para el modo tablet

                //Caracteristicas de Filtro en la grilla
                UltraGridProductos.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                UltraGridProductos.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                UltraGridProductos.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                UltraGridProductos.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
                UltraGridProductos.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
                //
                UltraGridProductos.DisplayLayout.UseFixedHeaders = true;

                //Dimension los registros
                UltraGridProductos.DisplayLayout.Bands[0].Columns[0].Width = 50;
                UltraGridProductos.DisplayLayout.Bands[0].Columns[1].Width = 200;
                UltraGridProductos.DisplayLayout.Bands[0].Columns[2].Width = 60;
                UltraGridProductos.DisplayLayout.Bands[0].Columns[3].Width = 80;
                UltraGridProductos.DisplayLayout.Bands[0].Columns[4].Width = 80;
                UltraGridProductos.DisplayLayout.Bands[0].Columns[5].Width = 80;
                UltraGridProductos.DisplayLayout.Bands[0].Columns[8].Width = 600;

                //agrandamiento de filas 

                //Ocultar columnas, que son fundamentales al levantar informacion
                UltraGridProductos.DisplayLayout.Bands[0].Columns[6].Hidden = true;
                UltraGridProductos.DisplayLayout.Bands[0].Columns[7].Hidden = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ultraGrid1_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = ultraGrid1.DisplayLayout.Bands[0];

            ultraGrid1.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
            //grid.DisplayLayout.Override.Allow

            ultraGrid1.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
            ultraGrid1.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            ultraGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

            bandUno.Override.CellClickAction = CellClickAction.RowSelect;
            bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

            ultraGrid1.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
            ultraGrid1.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
            ultraGrid1.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

            UltraGridProductos.DisplayLayout.Override.DefaultRowHeight = 20; //Para el modo tablet

            //Caracteristicas de Filtro en la grilla
            ultraGrid1.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            ultraGrid1.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            ultraGrid1.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            ultraGrid1.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            ultraGrid1.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
            //
            ultraGrid1.DisplayLayout.UseFixedHeaders = true;

            //Dimension los registros
            ultraGrid1.DisplayLayout.Bands[0].Columns[0].Width = 50;
            ultraGrid1.DisplayLayout.Bands[0].Columns[1].Width = 500;
            ultraGrid1.DisplayLayout.Bands[0].Columns[2].Width = 60;
        }

        private void detallePorItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (UltraGridProductos.Rows.Count > 0)
                {
                    string PathExcel = FindSavePath();
                    if (PathExcel != null)
                    {
                        this.ultraGridExcelExporter1.Export(UltraGridProductos, PathExcel);
                        MessageBox.Show("Se termino de exportar el grid en el archivo " + PathExcel);
                    }
                }
                else
                {
                    MessageBox.Show("No tiene Registros para Exportar.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void detalleTotalizadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (ultraGrid1.Rows.Count > 0)
                {
                    string PathExcel = FindSavePath();
                    if (PathExcel != null)
                    {
                        this.ultraGridExcelExporter1.Export(ultraGrid1, PathExcel);
                        MessageBox.Show("Se termino de exportar el grid en el archivo " + PathExcel);
                    }
                }
                else
                {
                    MessageBox.Show("No tiene Registros para Exportar.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
