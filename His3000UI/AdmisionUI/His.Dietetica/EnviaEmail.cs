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
using His.DatosReportes;
using His.Entidades.Clases;
using System.Globalization;
using His.Formulario;
using System.Net.Mail;
using System.IO;
using System.Net.Mime;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

namespace His.Dietetica
{
    public partial class EnviaEmail : Form
    {
        Int32 id, atencion;
        PACIENTES paciente = new PACIENTES();
        DataTable resultados = new DataTable();
        string nombrePaciente = "";
        public EnviaEmail(Int32 _id, Int32 _atencion)
        {
            InitializeComponent();
            id = _id;
            atencion = _atencion;
            paciente = NegPacientes.recuperarPacientePorAtencion(_atencion);
            resultados = NegPacientes.RecuperaResultadosImagen(_atencion);
            CargaInformacion();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {

            ImprimirReportex(id);

        }

        private void ImprimirReportex(int idImagenologia)
        {
            try
            {
                ReportesHistoriaClinica imagenL = new ReportesHistoriaClinica();
                imagenL.limpiarForm012();

                ReportesHistoriaClinica imagenL1 = new ReportesHistoriaClinica();
                imagenL1.limpiarForm012();

                NegCertificadoMedico c = new NegCertificadoMedico();


                DataTable est = NegImagen.getForm012Estudios(idImagenologia);
                string estudios = "";
                int aux = 0;
                foreach (DataRow row in est.Rows)
                {
                    if (aux > 0)
                        estudios += "   ,    ";
                    estudios += row["PRO_DESCRIPCION"].ToString();
                    aux++;
                }

                DataTable e = NegImagen.getForm012(idImagenologia);

                string[] x = new string[] {
                    Convert.ToString(Sesion.nomEmpresa),
                    Convert.ToString(e.Rows[0]["PARROQUIA"]),
                    Convert.ToString(e.Rows[0]["CANTON"]),
                    Convert.ToString(e.Rows[0]["PROVINCIA"]),
                    Convert.ToString(e.Rows[0]["PAC_HISTORIA_CLINICA"]),
                    Convert.ToString(e.Rows[0]["PAC_APELLIDO_PATERNO"]),
                    Convert.ToString(e.Rows[0]["PAC_APELLIDO_MATERNO"]),
                    Convert.ToString(e.Rows[0]["PAC_NOMBRE1"]),
                    Convert.ToString(e.Rows[0]["PAC_NOMBRE2"]),
                    Convert.ToString(e.Rows[0]["PAC_IDENTIFICACION"]),
                    (Convert.ToDateTime(e.Rows[0]["fecha_informe"])).ToString("yyyy-MM-dd"),
                    (Convert.ToDateTime(e.Rows[0]["fecha_informe"])).ToString("HH:mm:ss",CultureInfo.InvariantCulture) , //hora
                    Convert.ToString(e.Rows[0]["TIP_DESCRIPCION"]),
                    " 1 ", //sala
                    Convert.ToString(e.Rows[0]["hab_Numero"]),
                    Convert.ToString(e.Rows[0]["Medico_solicitante"]),
                    Convert.ToString(e.Rows[0]["P_Control"]),
                    Convert.ToString(e.Rows[0]["P_Normal"]),
                    Convert.ToString(e.Rows[0]["P_Urgente"]),
                    (Convert.ToDateTime(e.Rows[0]["fecha_entrega"])).ToString("yyyy-MM-dd"),
                    " ", //rubros
                    estudios,
                    Convert.ToString(e.Rows[0]["informe"]),
                    Convert.ToString(e.Rows[0]["DB_V"]),
                    Convert.ToString(e.Rows[0]["LF_V"]),
                    Convert.ToString(e.Rows[0]["PA_V"]),
                    Convert.ToString(e.Rows[0]["DB_EG"]),
                    Convert.ToString(e.Rows[0]["LF_EG"]),
                    Convert.ToString(e.Rows[0]["PA_EG"]),
                    Convert.ToString(e.Rows[0]["DB_P"]),
                    Convert.ToString(e.Rows[0]["LF_P"]),
                    Convert.ToString(e.Rows[0]["PA_P"]),
                    (Convert.ToString(e.Rows[0]["PLACENTA_F"])=="0"?" ":"X"),
                    (Convert.ToString(e.Rows[0]["PLACENTA_M"])=="0"?" ":"X"),
                    (Convert.ToString(e.Rows[0]["PLACENTA_P"])=="0"?" ":"X"),
                    (Convert.ToString(e.Rows[0]["MASCULINO"])=="0"?" ":"X"),
                    (Convert.ToString(e.Rows[0]["FEMENINO"]) == "0" ? " " : "X"),
                    (Convert.ToString(e.Rows[0]["MULTIPLE"]) == "0" ? " " : "X"),
                    Convert.ToString(e.Rows[0]["GRADO_MADUREZ"]),
                    (Convert.ToString(e.Rows[0]["ANTEVERSION"])=="0"?" ":"X"),
                    (Convert.ToString(e.Rows[0]["RETROVERSION"]) == "0" ? " " : "X"),
                    (Convert.ToString(e.Rows[0]["DIU"]) == "0" ? " " : "X"),
                    (Convert.ToString(e.Rows[0]["FIBROMA"]) == "0" ? " " : "X"),
                    (Convert.ToString(e.Rows[0]["MIOMA"]) == "0" ? " " : "X"),
                    (Convert.ToString(e.Rows[0]["AUSENTE"]) == "0" ? " " : "X"),
                    (Convert.ToString(e.Rows[0]["HIDROSALPIX"]) == "0" ? " " : "X"),
                    (Convert.ToString(e.Rows[0]["QUISTE"]) == "0" ? " " : "X"),
                    (Convert.ToString(e.Rows[0]["VACIA"]) == "0" ? " " : "X"),
                    (Convert.ToString(e.Rows[0]["OCUPADA"]) == "0" ? " " : "X"),
                    Convert.ToString(e.Rows[0]["SACO_DOUGLAS"]),
                    Convert.ToString(e.Rows[0]["recomendaciones"]),
                    Convert.ToString(e.Rows[0]["PLACAS_ENVIADAS"]),
                    Convert.ToString(e.Rows[0]["30X40"]),
                    Convert.ToString(e.Rows[0]["8X10"]),
                    Convert.ToString(e.Rows[0]["14X14"]),
                    Convert.ToString(e.Rows[0]["14X17"]),
                    Convert.ToString(e.Rows[0]["18X24"]),
                    Convert.ToString(e.Rows[0]["ODONT"]),
                    Convert.ToString(e.Rows[0]["DANADAS"]),
                    (Convert.ToString(e.Rows[0]["MEDIO_CONTRASTE"])=="0"?" ":"X"),
                    Convert.ToString(e.Rows[0]["Tecnologo"]),
                    Convert.ToString(e.Rows[0]["Radiologo"]),
                    Convert.ToString(c.path())
                };


                ReportesHistoriaClinica imagen = new ReportesHistoriaClinica();
                imagen.addImagenologiaInforme(x);

                //List<PedidoImagen_reporteDiagnosticos> ListDx = empaquetarReporteDx(idImagenologia);

                ReportesHistoriaClinica imagend = new ReportesHistoriaClinica();
                imagend.limpiarImagenologiaDiagnostico();

                DataTable diagnosticos = NegImagen.getForm012Dx(idImagenologia);
                foreach (DataRow row in diagnosticos.Rows)
                {
                    PedidoImagen_reporteDiagnosticos dx = new PedidoImagen_reporteDiagnosticos();
                    dx.diagnostico = row["CIE_DESCRIPCION"].ToString();
                    dx.CIE = row["CIE_CODIGO"].ToString();
                    dx.presuntivo = "1";
                    ReportesHistoriaClinica AUXX = new ReportesHistoriaClinica();
                    AUXX.ingresarImagenologiaDiagnostico(dx);
                }

                His.Formulario.frmReportes myreport = new frmReportes(1, "form012", true, txtDestinatarios.Text, txtCuerpo.Text, txtAsunto.Text, nombrePaciente+idImagenologia);
                myreport.Show();
                myreport.Close();
                MessageBox.Show("Informe enviado con EXITO!!! :)", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CargaInformacion()
        {
            string resultado = "";
            if (resultados.Rows.Count > 0)
            {
                for (int i = 0; i < resultados.Rows.Count; i++)
                {
                    resultado += string.Format("{0}.- {1}{2}", i + 1, resultados.Rows[i][0].ToString(), Environment.NewLine);
                }
            }
            txtAsunto.Text = "CLINICA PASTEUR – INFORME MEDICO.";
            txtDestinatarios.Text = paciente.PAC_EMAIL;
            string cuerpo = string.Format("Estimado paciente: {1} {2} {3} {4} {0}{0}La Clinica Pasteur por medio de la presente adjunta informes médicos de estudios realizados: {0}{5}{0}Saludos cordiales.", Environment.NewLine, paciente.PAC_APELLIDO_PATERNO, paciente.PAC_APELLIDO_MATERNO, paciente.PAC_NOMBRE1, paciente.PAC_NOMBRE2, resultado);
            txtCuerpo.Text = cuerpo;
            nombrePaciente = paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO + " " + paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2;
        }
    }
}
