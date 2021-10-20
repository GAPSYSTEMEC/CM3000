using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using His.Entidades;

namespace His.Formulario
{
    public partial class frm_AyudaKardex : Form
    {
        public string medicamento = "";
        public string cue_codigo = "";
        public string cantidad = "";
        List<KardexEnfermeriaMEdicamentos> Lista = new List<KardexEnfermeriaMEdicamentos>();

        public frm_AyudaKardex(string ate_codigo, int rubro, int check)
        {
            InitializeComponent();
            Lista = NegFormulariosHCU.RecuperaMedicamentos(ate_codigo, rubro, check);
            dtgAyudaKardex.DataSource = Lista;
            //grid.DataSource = Lista;
            dtgAyudaKardex.Columns[0].Width = 300;
            dtgAyudaKardex.Columns[1].Width = 40;
            dtgAyudaKardex.Columns[2].Width = 40;
            textBox1.Focus();
        }        

        private void dtgAyudaKardex_CellDoubleClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            medicamento = dtgAyudaKardex.Rows[e.RowIndex].Cells[0].Value.ToString();
            cue_codigo = dtgAyudaKardex.Rows[e.RowIndex].Cells[1].Value.ToString();
            cantidad = dtgAyudaKardex.Rows[e.RowIndex].Cells[2].Value.ToString();
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            var q = from x in Lista
                    where x.Producto.Contains(textBox1.Text.Trim())
                    select x;
            dtgAyudaKardex.DataSource = q.ToList();
            //grid.DataSource = q.ToList();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // e.Handled = e.KeyChar == Convert.ToChar(Keys.Space);
            if ((int)e.KeyChar == (int)Keys.Escape)
                this.Close();
            if ((int)e.KeyChar == (int)Keys.Enter)
                dtgAyudaKardex.Focus();
        }

        private void dtgAyudaKardex_KeyPress(object sender, KeyPressEventArgs e)
        {
            DataGridViewRow Item = null;
            Item = dtgAyudaKardex.CurrentRow;

            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                //medicamento.Text = Item.Cells[0].Value.ToString();
                //cue_codigo.Text = Item.Cells[1].Value.ToString();
                //cantidad.Text = Item.Cells[2].Value.ToString();
                this.Close();

                
            }
            if ((int)e.KeyChar == (int)Keys.Escape)
                this.Close();
        }

        private void dtgAyudaKardex_CellEnter(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            //if ((int)e.KeyChar == (int)Keys.Enter)
            //{

            
            //    medicamento.Text = dtgAyudaKardex.CurrentRow.Cells[0].Value.ToString();
            //    cue_codigo.Text = dtgAyudaKardex.CurrentRow.Cells[1].Value.ToString();
            //    cantidad.Text = dtgAyudaKardex.CurrentRow.Cells[2].Value.ToString();
            //    //this.Close();

            //}
        }

        private void grid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {

        }

        private void frm_AyudaKardex_Load(object sender, EventArgs e)
        {

        }

        private void dtgAyudaKardex_CurrentCellChanged(object sender, EventArgs e)
        {
            DataGridViewRow Item = null;
            Item = dtgAyudaKardex.CurrentRow;
            try
            {
                Console.WriteLine(Item.Cells[0].Value.ToString() + "  " + Item.Cells[1].Value.ToString() + "  " + Item.Cells[2].Value.ToString());
                medicamento = Item.Cells[0].Value.ToString();
                cue_codigo = Item.Cells[1].Value.ToString();
                cantidad = Item.Cells[2].Value.ToString();
            }
            catch (Exception)
            {

                //throw;
            }
            
        }

        private void dtgAyudaKardex_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataGridViewRow row = ((DataGridView)sender).CurrentRow;
                string valorPr = Convert.ToString(row.Cells[0].Value);
                e.Handled = true;
            }
        }
    }
}
