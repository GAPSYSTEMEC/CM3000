using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using His.Entidades.Pedidos;
using His.Negocio;
using Infragistics.Win.UltraWinGrid;
using Recursos;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using Core.Datos;
using His.Parametros;
using System.IO;
using System.Diagnostics;

namespace CuentaPaciente
{
    public partial class frmArchivoPlanoIess : Form
    {
        List<Int32> _ListaAtenciones = new List<Int32>();
        DataTable dtArchivoPlano = new DataTable();
        DataTable dtArchivoPlano1 = new DataTable();

        public frmArchivoPlanoIess(List<Int32> ListaAtenciones)
        {
            _ListaAtenciones = ListaAtenciones;
            InitializeComponent();
        }

        private void frmArchivoPlanoIess_Load(object sender, EventArgs e)
        {
            Int32 Contador = 0;

            foreach (Int32 Atencion in _ListaAtenciones)
            {
                if (Contador == 0)
                {
                    dtArchivoPlano = NegCuentasPacientes.GeneraArchivoPlano(Atencion.ToString());
                }
                else
                {
                    dtArchivoPlano1 = NegCuentasPacientes.GeneraArchivoPlano(Atencion.ToString());
                    dtArchivoPlano.Merge(dtArchivoPlano1);
                }
                Contador++;
            }
            dgvDatosCuentas.DataSource = dtArchivoPlano;
        }

        private void toolStripbtnNuevo_Click(object sender, EventArgs e)
        {
            int control = 0;
            try
            {
                CreateExcel(FindSavePath());
                var x2 = (from r in dtArchivoPlano.AsEnumerable()
                          select r["Identificador del prestador que  ingresa al sistema web"]).Distinct().ToList();

                foreach (var name2 in x2)
                {

                    //if (3 != Convert.ToInt16(name2))
                    //{
                    //    control++;

                    //}
                }

                if (control == 0)
                {
                    var x1 = (from r in dtArchivoPlano.AsEnumerable()
                              select r["Identificador del prestador que  ingresa al sistema web"]).Distinct().ToList();
                    foreach (var name1 in x1)
                    {
                        // NegAseguradoras.actualizarEstadoFactura(Convert.ToString(name1), 5);

                    }
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { this.Cursor = Cursors.Default; }
        }

        private void CreateExcel(String myFilepath)
        {
            try
            {
                if (myFilepath != null)
                {

                    this.ultraGridExcelExporter1.Export(dgvDatosCuentas, myFilepath);

                    MessageBox.Show("Se termino de exportar el grid en el archivo " + myFilepath);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private String FindSavePath()
        {
            Stream myStream;
            string myFilepath = null;
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "excel files (*.xls)|*.xls";
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

    }
}