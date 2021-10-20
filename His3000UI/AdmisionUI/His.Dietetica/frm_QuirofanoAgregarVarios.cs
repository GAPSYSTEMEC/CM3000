using His.Entidades;
using His.Entidades.Clases;
using His.Negocio;
using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace His.Dietetica
{
    public partial class frm_QuirofanoAgregarVarios : Form
    {
        public string ate_codigo = "";
        public string pac_codigo = "";
        public string pci_codigo = "";
        NegQuirofano Quirofano = new NegQuirofano();

        ATENCIONES ultimaAtencion = new ATENCIONES();
        PACIENTES datosPacientes = new PACIENTES();
        public frm_QuirofanoAgregarVarios()
        {
            InitializeComponent();
        }

        private void frm_QuirofanoAgregarVarios_Load(object sender, EventArgs e)
        {
            CargarProductosSic();
            ultimaAtencion = NegAtenciones.RecuperarAtencionID(Convert.ToInt64(ate_codigo));
            datosPacientes = NegPacientes.recuperarPacientePorAtencion(Convert.ToInt32(ate_codigo));

        }

        public void CargarProductosSic()
        {
            UltraGridProductos.DataSource = NegQuirofano.ProductosSic();
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void txtOrden_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
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
                UltraGridProductos.DisplayLayout.Bands[0].Columns[0].Width = 60;
                UltraGridProductos.DisplayLayout.Bands[0].Columns[1].Width = 400;
                UltraGridProductos.DisplayLayout.Bands[0].Columns[2].Width = 100;
                UltraGridProductos.DisplayLayout.Bands[0].Columns[3].Width = 80;

                //agrandamiento de filas 

                //Ocultar columnas, que son fundamentales al levantar informacion
                UltraGridProductos.DisplayLayout.Bands[0].Columns[4].Hidden = true;
                //UltraGridProductos.DisplayLayout.Bands[0].Columns[5].Hidden = true;
                //UltraGridProductos.DisplayLayout.Bands[0].Columns[6].Hidden = true;
                //UltraGridProductos.DisplayLayout.Bands[0].Columns[7].Hidden = true;
                //UltraGridProductos.DisplayLayout.Bands[0].Columns[8].Hidden = true;
                //UltraGridProductos.DisplayLayout.Bands[0].Columns[9].Hidden = true;
                //UltraGridProductos.DisplayLayout.Bands[0].Columns[10].Hidden = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UltraGridProductos_KeyDown(object sender, KeyEventArgs e)
        {
            UltraGridRow Fila = UltraGridProductos.ActiveRow;
            if (e.KeyCode == Keys.Enter)
            {
                if (UltraGridProductos.Selected.Rows.Count == 1)
                {
                    txtCantidad.Text = "1";
                    txtCantidad.Focus();
                }
                else
                {
                    MessageBox.Show("Debe elegir un producto para continuar...", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        int contador = 0;
        public void AñadeProducto()
        {

            UltraGridRow Fila = UltraGridProductos.ActiveRow;
            string codpro = "";
            string descripcion;
            if (UltraGridProductos.Selected.Rows.Count == 1)
            {
                codpro = Fila.Cells["CODIGO"].Value.ToString();
                if (txtCantidad.Text.Trim() != "")
                {
                    bool existe = false;


                    if (Convert.ToInt32(Fila.Cells["STOCK"].Value.ToString()) != 0 && Convert.ToInt32(Fila.Cells["STOCK"].Value.ToString()) >= Convert.ToInt32(txtCantidad.Text))
                    {
                        DataTable TablaProcedimientos = Quirofano.PacienteProcedimiento(pci_codigo, ultimaAtencion.ATE_CODIGO.ToString());

                        if(TablaPedidos.Rows.Count > 0)
                        {
                            foreach (DataGridViewRow item in TablaPedidos.Rows)
                            {

                                if (item.Cells["codpro"].Value.ToString() == codpro)
                                {
                                    MessageBox.Show("El producto ya está agregado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    existe = true;
                                    break;
                                }
                                else
                                {
                                    foreach (DataRow item1 in TablaProcedimientos.Rows)
                                    {
                                        if (item1["Codigo"].ToString() == item.Cells["codpro"].Value.ToString())
                                        {
                                            MessageBox.Show("El producto ya está agregado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            existe = true;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (DataRow item in TablaProcedimientos.Rows)
                            {
                                if (item["Codigo"].ToString() == codpro)
                                {
                                    MessageBox.Show("El producto ya está agregado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    existe = true;
                                    break;
                                }
                            }
                        }
                        if (!existe)
                        {
                            int orden = 0;
                            if (contador == 0)
                            {
                                orden = NegQuirofano.ultimoOrden(Convert.ToInt32(pci_codigo), ultimaAtencion.ATE_CODIGO);
                                contador = orden + 1;
                            }
                            codpro = Fila.Cells[0].Value.ToString();
                            descripcion = Fila.Cells[1].Value.ToString();
                            //valor = Fila.Cells[10].Value.ToString();
                            TablaPedidos.Rows.Add(codpro, descripcion, txtCantidad.Text, contador.ToString());
                            txtCantidad.Text = "";
                            txtOrden.Text = "";
                            errorProvider1.Clear();
                            contador++;
                        }
                    }
                    else
                    {
                        MessageBox.Show("¡No hay suficiente Stock!", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtCantidad.Text = "1";
                    }
                    //if (txtOrden.Text != "")
                    //{
                        
                    //}
                    //else
                    //    errorProvider1.SetError(txtCantidad, "Debe agregar el orden, no puede ser 0");
                }
                else
                    errorProvider1.SetError(txtCantidad, "Debe agregar la cantidad, no puede ser 0");
            }
            else
            {
                MessageBox.Show("¡No se ha Elegido Producto ha Agregar!", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AñadeProducto();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            CargarProductosSic();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if(TablaPedidos.Rows.Count > 0)
            {
                foreach (DataGridViewRow item in TablaPedidos.Rows)
                {
                    try
                    {
                        Quirofano.AgregarProcedimientoPaciente(item.Cells["orden"].Value.ToString(), pci_codigo, item.Cells["codpro"].Value.ToString(), item.Cells["cant"].Value.ToString(), datosPacientes.PAC_CODIGO.ToString(), ultimaAtencion.ATE_CODIGO.ToString(), null, Sesion.nomUsuario, 0);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Algo ocurrio al guardar los productos" + ex.Message, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                MessageBox.Show("Producto(s) agregados correctamente", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("No tiene producto para guardar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtCantidad_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if(txtCantidad.Text.Trim() != "")
                {
                    if (Convert.ToInt32(txtCantidad.Text.Trim()) > 0)
                    {
                        AñadeProducto();
                    }
                    else
                        txtCantidad.Text = "1";
                }
                else
                    txtCantidad.Text = "1";
            }
        }

        private void txtOrden_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtOrden.Text.Trim() != "")
                {
                    if (Convert.ToInt32(txtOrden.Text.Trim()) > 0)
                    {
                        AñadeProducto();
                    }
                    else
                        txtOrden.Text = "1";
                }
                else
                    txtOrden.Text = "1";
            }
        }

        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int temp = 0;
            temp = NegQuirofano.ultimoOrden(Convert.ToInt32(pci_codigo), ultimaAtencion.ATE_CODIGO) + 1;

            int val = 0;
            foreach (DataGridViewRow item in TablaPedidos.Rows)
            {
                TablaPedidos.Rows[val].Cells["Orden"].Value = temp;
                temp++;
                val++;
            }
            contador = temp;
        }
    }
}
