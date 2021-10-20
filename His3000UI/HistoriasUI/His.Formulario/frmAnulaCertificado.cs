using His.Entidades;
using His.Entidades.Clases;
using His.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace His.Formulario
{
    public partial class frmAnulaCertificado : Form
    {
        internal static bool Eliminar;
        Int32 codigoCertificado = 0;
        MaskedTextBox codMedico;
        MEDICOS medico = null;
        public frmAnulaCertificado(Int32 codCertificado, string medico)
        {
            InitializeComponent();
            codigoCertificado = codCertificado;
            txtmedico.Text = medico;
        }

        private void btnBuscaMedico_Click(object sender, EventArgs e)
        {
            List<MEDICOS> medicos = NegMedicos.listaMedicos();
            frm_Ayudas x = new frm_Ayudas(medicos);
            x.ShowDialog();
            if (x.campoPadre.Text != string.Empty)
            {
                codMedico = (x.campoPadre2);
                string cod = x.campoPadre.Text;
                medico = NegMedicos.RecuperaMedicoId(Convert.ToInt32(cod));
                agregarMedico(medico);
            }
        }

        private void agregarMedico(MEDICOS medicoTratante)
        {
            if ((medicoTratante != null))
            {
                txtmedico.Text = medicoTratante.MED_APELLIDO_PATERNO.Trim() + " " + medicoTratante.MED_APELLIDO_MATERNO.Trim()
                    + " " + medicoTratante.MED_NOMBRE1.Trim() + " " + medicoTratante.MED_NOMBRE2.Trim();
            }
        }

        private void btnGuarda_Click(object sender, EventArgs e)
        {
            if (txtmedico.Text == "")
            {
                MessageBox.Show("Se necesita el médico que solicita la inhabilitación del certificado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMotivo.Text == "")
            {
                MessageBox.Show("Se necesita el motivo de inhabilitación del certificado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            frmLogin x = new frmLogin();
            x.ShowDialog();
            if (Eliminar == true)
            {
                NegCertificadoMedico.InhabilitaCertificado(txtMotivo.Text+ " Usuario que anula: " + Sesion.nomUsuario, txtmedico.Text, codigoCertificado);
                MessageBox.Show("Certificado Inhabilitado con exito.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("No tienes permitido Inhabilitar un certificado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancela_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
