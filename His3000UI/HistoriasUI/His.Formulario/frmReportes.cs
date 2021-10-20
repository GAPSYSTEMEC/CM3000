using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using His.Negocio;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using His.Entidades;
using System.IO;

namespace His.Formulario
{
    public partial class frmReportes : Form
    {
        public frmReportes()
        {
            InitializeComponent();
        }

        public bool _envia;
        public int parametro;
        public string reporte;
        public string campo1;
        public DataSet Datos;
        public DataSet Datos1;
        public DataSet Datos2;
        string para = "";
        string mensaje = "";
        string asunto = "";
        string nombrePac = "";
        //ReportDocument rpt = new ReportDocument();
        public frmReportes(int parParametro, string reporte)
        {
            this.parametro = parParametro;
            this.reporte = reporte;
            InitializeComponent();

        }

        public frmReportes(int parParametro, string reporte, bool enviaEmail, string _para, string _mensaje, string _asunto, string nombre)
        {
            this.parametro = parParametro;
            this.reporte = reporte;
            InitializeComponent();
            _envia = enviaEmail;
            para = _para;
            mensaje = _mensaje;
            asunto = _asunto;
            nombrePac = nombre;
        }

        public frmReportes(int parParamentro, string reporte, DataSet datos)
        {
            this.parametro = parParamentro;
            this.reporte = reporte;
            this.Datos = datos;
            InitializeComponent();
        }
        public frmReportes(int parParametro, string reporte, DataSet Cabecera, DataSet Cuerpo, DataSet Detalle)
        {
            this.parametro = parParametro;
            this.reporte = reporte;
            this.Datos = Cabecera;
            this.Datos1 = Cuerpo;
            this.Datos2 = Detalle;
            InitializeComponent();
        }
        private void cargarReporte()
        {
            try
            {
                #region ADMISION
                if (reporte == "admision")
                {
                    ReportDocument cryRpt = new ReportDocument();

                    cryRpt.Load(Application.StartupPath + "\\Reportes\\Admision\\rPrimeraAtencion2.rpt");

                    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                    ConnectionInfo crConnectionInfo = new ConnectionInfo();
                    Tables CrTables = null;

                    string campo1 = Convert.ToString(parametro);
                    cryRpt.SetParameterValue("cPaciente", campo1);
                    cryRpt.SetParameterValue("@codigoAtencion", campo1);
                    cryRpt.SetParameterValue("@codigoAtencion1", campo1);
                    cryRpt.SetParameterValue("codPaciente3", campo1);

                    crConnectionInfo.ServerName = Core.Datos.Sesion.nombreServidor;
                    crConnectionInfo.DatabaseName = Core.Datos.Sesion.nombreBaseDatos;
                    crConnectionInfo.UserID = Core.Datos.Sesion.usrBaseDatos;
                    crConnectionInfo.Password = Core.Datos.Sesion.pwdBaseDatos;

                    CrTables = cryRpt.Database.Tables;
                    foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                    {
                        crtableLogoninfo = CrTable.LogOnInfo;
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                        CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    }
                    crystalReportViewer1.ReportSource = cryRpt;
                    crystalReportViewer1.RefreshReport();
                    crystalReportViewer1.PrintReport();
                }
                #endregion

                #region EPICRISIS 1 - 2

                else if (reporte == "epicrisis1")
                {
                    ReportDocument reporteEpicrisis = new ReportDocument();
                    reporteEpicrisis.FileName = Application.StartupPath + "\\Reportes\\HistoriasClinicas\\epicrisis.rpt";
                    //crystalReportViewer1.ReportSource = reporteAnamnesis;
                    TableLogOnInfo logInCon = new TableLogOnInfo();
                    logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                    foreach (Table tb in reporteEpicrisis.Database.Tables)
                    {
                        tb.ApplyLogOnInfo(logInCon);
                    }
                    reporteEpicrisis.Refresh();
                    crystalReportViewer1.ReportSource = reporteEpicrisis;
                    crystalReportViewer1.RefreshReport();

                }
                else
                    if (reporte == "epicrisis2")
                {
                    try
                    {
                        ReportDocument reporteEpicrisis = new ReportDocument();
                        reporteEpicrisis.FileName = Application.StartupPath + "\\Reportes\\HistoriasClinicas\\epicrisis2.rpt";
                        //crystalReportViewer1.ReportSource = reporteAnamnesis;
                        TableLogOnInfo logInCon = new TableLogOnInfo();
                        logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                        foreach (Table tb in reporteEpicrisis.Database.Tables)
                        {
                            tb.ApplyLogOnInfo(logInCon);
                        }
                        reporteEpicrisis.Refresh();
                        crystalReportViewer1.ReportSource = reporteEpicrisis;
                        crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception err)
                    { throw err; }

                }

                #endregion

                #region EVOLUCION
                else if (reporte == "evolucion")
                {
                    try
                    {
                        NotasIndividualesMedicos myreport = new NotasIndividualesMedicos();
                        myreport.Refresh();
                        myreport.SetDataSource(Datos);
                        crystalReportViewer1.ReportSource = myreport;
                        crystalReportViewer1.RefreshReport();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                #endregion
                #region QuirofanoProductos
                else if (reporte == "QuirofanoProductos")
                {
                    try
                    {
                        ControlProductosQuirofano myreport = new ControlProductosQuirofano();
                        myreport.Refresh();
                        myreport.SetDataSource(Datos);
                        crystalReportViewer1.ReportSource = myreport;
                        crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                #endregion
                #region PedidoQuirofano
                else if (reporte == "PedidoQuirofano")
                {
                    try
                    {
                        QuirofanoPedidoImpresion myreport = new QuirofanoPedidoImpresion();
                        myreport.Refresh();
                        myreport.SetDataSource(Datos);
                        crystalReportViewer1.ReportSource = myreport;
                        crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                #endregion

                #region RecetaMedica
                else if (reporte == "Receta")
                {
                    try
                    {
                        ReporteReceta myreport = new ReporteReceta();
                        myreport.Refresh();
                        myreport.SetDataSource(Datos);
                        crystalReportViewer1.ReportSource = myreport;
                        crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                #endregion
                #region CertificadoMedicoHospitalarioAlta
                else if (reporte == "CertificadoHA")
                {
                    try
                    {
                        CertificadoHospitalarioAlta myreport = new CertificadoHospitalarioAlta();
                        myreport.Refresh();
                        myreport.SetDataSource(Datos);
                        crystalReportViewer1.ReportSource = myreport;
                        crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                #endregion
                #region CertificadoMedicoEmergenciaAlta
                else if (reporte == "CertificadoEA")
                {
                    try
                    {
                        CertificadoEmergenciaAlta myreport = new CertificadoEmergenciaAlta();
                        myreport.Refresh();
                        myreport.SetDataSource(Datos);
                        crystalReportViewer1.ReportSource = myreport;
                        crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                #endregion
                #region CertificadoHospitalarioSinAlta
                else if (reporte == "CertificadoHSA")
                {
                    try
                    {
                        CertificadoHospitalarioSinAlta myreport = new CertificadoHospitalarioSinAlta();
                        myreport.Refresh();
                        myreport.SetDataSource(Datos);
                        crystalReportViewer1.ReportSource = myreport;
                        crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                #endregion
                #region CIERREDIETA
                else if (reporte == "CierreDietetica")
                {
                    try
                    {
                        rptCierreTurnoDietetica myreport = new rptCierreTurnoDietetica();
                        myreport.Refresh();
                        myreport.SetDataSource(Datos);
                        crystalReportViewer1.ReportSource = myreport;
                        crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                }
                #endregion
                #region NotasIndividualesEvolucionMedicos
                else if (reporte == "NotasIndividualesEvolucionMedicos")
                {
                    try
                    {
                        ReportDocument reporteAnamnesis = new ReportDocument();
                        reporteAnamnesis.FileName = Application.StartupPath + "\\Reportes\\HistoriasClinicas\\rEvolucionNotas.rpt";
                        //crystalReportViewer1.ReportSource = reporteAnamnesis;
                        TableLogOnInfo logInCon = new TableLogOnInfo();
                        logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                        foreach (Table tb in reporteAnamnesis.Database.Tables)
                        {
                            tb.ApplyLogOnInfo(logInCon);
                        }
                        reporteAnamnesis.Refresh();
                        crystalReportViewer1.ReportSource = reporteAnamnesis;
                        crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception err)
                    { throw err; }

                }
                #endregion
                #region IMAGENResumen



                else if (reporte == "imagenologiaResumen")
                {
                    try
                    {
                        ReportDocument reporteAnamnesis = new ReportDocument();
                        reporteAnamnesis.FileName = Application.StartupPath + "\\Reportes\\HistoriasClinicas\\rptImagenResumen.rpt";
                        TableLogOnInfo logInCon = new TableLogOnInfo();
                        logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                        foreach (Table tb in reporteAnamnesis.Database.Tables)
                        {
                            tb.ApplyLogOnInfo(logInCon);
                        }
                        reporteAnamnesis.Refresh();
                        crystalReportViewer1.ReportSource = reporteAnamnesis;
                        crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception err)
                    { throw err; }

                }
                #endregion
                #region IMAGEN
                else if (reporte == "imagenologia")
                {
                    try
                    {
                        ReportDocument reporteAnamnesis = new ReportDocument();
                        reporteAnamnesis.FileName = Application.StartupPath + "\\Reportes\\HistoriasClinicas\\rptImagen.rpt";
                        TableLogOnInfo logInCon = new TableLogOnInfo();
                        logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                        foreach (Table tb in reporteAnamnesis.Database.Tables)
                        {
                            tb.ApplyLogOnInfo(logInCon);
                        }
                        reporteAnamnesis.Refresh();
                        crystalReportViewer1.ReportSource = reporteAnamnesis;
                        crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception err)
                    { throw err; }

                }
                #endregion

                #region PRECIO CONVENIO
                else if (reporte == "PreciosConvenio")
                {
                    try
                    {
                        ReportDocument reporteAnamnesis = new ReportDocument();
                        reporteAnamnesis.FileName = Application.StartupPath + "\\Reportes\\Mantenimiento\\rptPreciosConvenios.rpt";
                        TableLogOnInfo logInCon = new TableLogOnInfo();
                        logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                        foreach (Table tb in reporteAnamnesis.Database.Tables)
                        {
                            tb.ApplyLogOnInfo(logInCon);
                        }
                        reporteAnamnesis.Refresh();
                        crystalReportViewer1.ReportSource = reporteAnamnesis;
                        crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception err)
                    { throw err; }

                }
                #endregion

                #region Form012
                else if (reporte == "form012")
                {
                    try
                    {
                        ReportDocument reporteAnamnesis = new ReportDocument();
                        reporteAnamnesis.FileName = Application.StartupPath + "\\Reportes\\HistoriasClinicas\\rptForm012.rpt";
                        TableLogOnInfo logInCon = new TableLogOnInfo();
                        logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                        foreach (Table tb in reporteAnamnesis.Database.Tables)
                        {
                            tb.ApplyLogOnInfo(logInCon);
                        }
                        reporteAnamnesis.Refresh();
                        crystalReportViewer1.ReportSource = reporteAnamnesis;
                        crystalReportViewer1.RefreshReport();
                        if (_envia)
                        {
                            List<DtoParametros> Informacion = new List<DtoParametros>();
                            Informacion = NegUtilitarios.RecuperaInformacionCorreo();
                            string[] datos = new string[10];
                            foreach (var item in Informacion)
                            {
                                datos = item.PAD_VALOR.Split(';');
                            }
                            ExportOptions CrExportOptions;
                            DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                            PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                            CrDiskFileDestinationOptions.DiskFileName = datos[0].ToString() + nombrePac + ".pdf";
                            CrExportOptions = reporteAnamnesis.ExportOptions;
                            {
                                CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                                CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                                CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                                CrExportOptions.FormatOptions = CrFormatTypeOptions;
                            }
                            reporteAnamnesis.Export();
                            reporteAnamnesis.Close();
                            SmtpClient smtp = new SmtpClient();
                            MailMessage mail = new MailMessage();
                            Attachment anexar = new Attachment(datos[0].ToString() + nombrePac + ".pdf");
                            mail.Attachments.Add(anexar);
                            smtp.Host = datos[1].ToString();
                            smtp.Port = Convert.ToInt16(datos[2].ToString());
                            smtp.EnableSsl = true;
                            smtp.UseDefaultCredentials = false;
                            smtp.Credentials = new System.Net.NetworkCredential(datos[3].ToString(), datos[4].ToString());
                            mail.From = new MailAddress(datos[3].ToString());
                            mail.To.Add(new MailAddress(para));
                            mail.Subject = asunto;
                            mail.Body = mensaje;

                            smtp.Send(mail);

                            //File.Delete(datos[0].ToString() + nombrePac + ".pdf");
                            this.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                }
                #endregion

                #region REFERENCIA
                else if (reporte == "Referencia")
                {
                    try
                    {
                        ReportDocument reporteAnamnesis = new ReportDocument();
                        reporteAnamnesis.FileName = Application.StartupPath + "\\Reportes\\HistoriasClinicas\\rptReferencia.rpt";
                        TableLogOnInfo logInCon = new TableLogOnInfo();
                        logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                        foreach (Table tb in reporteAnamnesis.Database.Tables)
                        {
                            tb.ApplyLogOnInfo(logInCon);
                        }
                        reporteAnamnesis.Refresh();
                        crystalReportViewer1.ReportSource = reporteAnamnesis;
                        crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception err)
                    { throw err; }

                }
                #endregion

                #region TEMP DIETETICA
                else if (reporte == "rptTempDietetica")
                {
                    try
                    {
                        ReportDocument reporteAnamnesis = new ReportDocument();
                        reporteAnamnesis.FileName = Application.StartupPath + "\\Reportes\\HistoriasClinicas\\rptTempDietetica.rpt";
                        TableLogOnInfo logInCon = new TableLogOnInfo();
                        logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                        foreach (Table tb in reporteAnamnesis.Database.Tables)
                        {
                            tb.ApplyLogOnInfo(logInCon);
                        }
                        reporteAnamnesis.Refresh();
                        crystalReportViewer1.ReportSource = reporteAnamnesis;
                        crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception err)
                    { throw err; }

                }
                #endregion

                #region CONTRAREFERENCIA
                else if (reporte == "Contrareferencia")
                {
                    try
                    {
                        ReportDocument reporteAnamnesis = new ReportDocument();
                        reporteAnamnesis.FileName = Application.StartupPath + "\\Reportes\\HistoriasClinicas\\rptContrareferencia.rpt";
                        TableLogOnInfo logInCon = new TableLogOnInfo();
                        logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                        foreach (Table tb in reporteAnamnesis.Database.Tables)
                        {
                            tb.ApplyLogOnInfo(logInCon);
                        }
                        reporteAnamnesis.Refresh();
                        crystalReportViewer1.ReportSource = reporteAnamnesis;
                        crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception err)
                    { throw err; }

                }
                #endregion

                #region vendedres comison
                else if (reporte == "Vendedores")
                {
                    try
                    {
                        ReportDocument reporteAnamnesis = new ReportDocument();
                        reporteAnamnesis.FileName = Application.StartupPath + "\\Reportes\\Honorarios\\rptVendedoresFacturas.rpt";
                        TableLogOnInfo logInCon = new TableLogOnInfo();
                        logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                        foreach (Table tb in reporteAnamnesis.Database.Tables)
                        {
                            tb.ApplyLogOnInfo(logInCon);
                        }
                        reporteAnamnesis.Refresh();
                        crystalReportViewer1.ReportSource = reporteAnamnesis;
                        crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception err)
                    { throw err; }

                }
                #endregion

                #region ADMISIONEGRESO
                else if (reporte == "admisionE")
                {
                    try
                    {
                        ReportDocument reporteAnamnesis = new ReportDocument();
                        reporteAnamnesis.FileName = Application.StartupPath + "\\Reportes\\Admision\\rPrimeraAtencion3.rpt";
                        //crystalReportViewer1.ReportSource = reporteAnamnesis;
                        TableLogOnInfo logInCon = new TableLogOnInfo();
                        logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                        foreach (Table tb in reporteAnamnesis.Database.Tables)
                        {
                            tb.ApplyLogOnInfo(logInCon);
                        }
                        reporteAnamnesis.Refresh();
                        crystalReportViewer1.ReportSource = reporteAnamnesis;
                        crystalReportViewer1.RefreshReport();
                        crystalReportViewer1.PrintReport();
                    }
                    catch (Exception err)
                    { throw err; }
                }
                #endregion

                #region INTERCONSULTA
                else if (reporte == "ExamenSangre")
                {
                    try
                    {
                        ReportDocument reporteAnamnesis = new ReportDocument();
                        reporteAnamnesis.FileName = Application.StartupPath + "\\Reportes\\HistoriasClinicas\\RPT_PEDIDO_SANGRE_detalle.rpt";
                        //crystalReportViewer1.ReportSource = reporteAnamnesis;
                        TableLogOnInfo logInCon = new TableLogOnInfo();
                        logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                        foreach (Table tb in reporteAnamnesis.Database.Tables)
                        {
                            tb.ApplyLogOnInfo(logInCon);
                        }
                        reporteAnamnesis.Refresh();
                        crystalReportViewer1.ReportSource = reporteAnamnesis;
                        crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception ex)
                    { throw ex; }
                }
                else if (reporte == "interconsulta")
                {
                    try
                    {
                        ReportDocument reporteAnamnesis = new ReportDocument();
                        reporteAnamnesis.FileName = Application.StartupPath + "\\Reportes\\HistoriasClinicas\\interconsulta.rpt";
                        //crystalReportViewer1.ReportSource = reporteAnamnesis;
                        TableLogOnInfo logInCon = new TableLogOnInfo();
                        logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                        foreach (Table tb in reporteAnamnesis.Database.Tables)
                        {
                            tb.ApplyLogOnInfo(logInCon);
                        }
                        reporteAnamnesis.Refresh();
                        crystalReportViewer1.ReportSource = reporteAnamnesis;
                        crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else if (reporte == "interconsulta2")
                {
                    try
                    {
                        ReportDocument reporteAnamnesis = new ReportDocument();
                        reporteAnamnesis.FileName = Application.StartupPath + "\\Reportes\\HistoriasClinicas\\interconsulta2.rpt";
                        //crystalReportViewer1.ReportSource = reporteAnamnesis;
                        TableLogOnInfo logInCon = new TableLogOnInfo();
                        logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                        foreach (Table tb in reporteAnamnesis.Database.Tables)
                        {
                            tb.ApplyLogOnInfo(logInCon);
                        }
                        reporteAnamnesis.Refresh();
                        crystalReportViewer1.ReportSource = reporteAnamnesis;
                        crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception ex)
                    { throw ex; }
                }
                #endregion

                #region ANAMNESIS
                else if (reporte == "anamnesis")
                {
                    try
                    {
                        ReportDocument reporteAnamnesis = new ReportDocument();
                        reporteAnamnesis.FileName = Application.StartupPath + "\\Reportes\\HistoriasClinicas\\historiaClinica.rpt";
                        //crystalReportViewer1.ReportSource = reporteAnamnesis;
                        TableLogOnInfo logInCon = new TableLogOnInfo();
                        logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                        foreach (Table tb in reporteAnamnesis.Database.Tables)
                        {
                            tb.ApplyLogOnInfo(logInCon);
                        }
                        reporteAnamnesis.Refresh();
                        crystalReportViewer1.ReportSource = reporteAnamnesis;
                        crystalReportViewer1.RefreshReport();

                    }
                    catch (Exception err)
                    { throw err; }
                }
                #endregion

                #region FORMULARIO 8
                else if (reporte == "Hoja 008")
                {
                    try
                    {
                        ReportDocument reporteEmergencia = new ReportDocument();
                        reporteEmergencia.FileName = Application.StartupPath + "\\Reportes\\HistoriasClinicas\\Formulario8.rpt";
                        //Database s = new Database;     
                        //reporteEmergencia.Database = ""; 
                        TableLogOnInfo logInCon = new TableLogOnInfo();
                        logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                        foreach (Table tb in reporteEmergencia.Database.Tables)
                        {
                            tb.ApplyLogOnInfo(logInCon);
                        }
                        reporteEmergencia.Refresh();
                        crystalReportViewer1.ReportSource = reporteEmergencia;
                        crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception err)
                    { throw err; }
                }
                #endregion

                //Hoja 008E
                #region FORMULARIO 008E
                else if (reporte == "Hoja 008E")
                {
                    try
                    {
                        ReportDocument reporteEmergencia = new ReportDocument();
                        reporteEmergencia.FileName = Application.StartupPath + "\\Reportes\\HistoriasClinicas\\Formulario008E.rpt";
                        //Database s = new Database;     
                        //reporteEmergencia.Database = ""; 
                        TableLogOnInfo logInCon = new TableLogOnInfo();
                        logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                        foreach (Table tb in reporteEmergencia.Database.Tables)
                        {
                            tb.ApplyLogOnInfo(logInCon);
                        }
                        reporteEmergencia.Refresh();
                        crystalReportViewer1.ReportSource = reporteEmergencia;
                        crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception err)
                    { throw err; }
                }
                #endregion


                #region PROTOCOLO
                else if (reporte == "protocolo")
                {
                    try
                    {
                        ReportDocument reporteProtocolo = new ReportDocument();
                        reporteProtocolo.FileName = Application.StartupPath + "\\Reportes\\HistoriasClinicas\\ProtocoloOperatorio.rpt";
                        //crystalReportViewer1.ReportSource = reporteAnamnesis;
                        TableLogOnInfo logInCon = new TableLogOnInfo();
                        logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                        foreach (Table tb in reporteProtocolo.Database.Tables)
                        {
                            tb.ApplyLogOnInfo(logInCon);
                        }
                        reporteProtocolo.Refresh();
                        crystalReportViewer1.ReportSource = reporteProtocolo;
                        crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception err)
                    { throw err; }
                }
                #endregion

                #region FACTURA
                else if (reporte == "Factura")
                {
                    try
                    {
                        //ReportDocument reporteFactura = new ReportDocument();
                        //reporteFactura.FileName = Application.StartupPath + "\\Reportes\\Facturas\\Factura.rpt";
                        ////Database s = new Database;     
                        ////reporteEmergencia.Database = ""; 
                        //TableLogOnInfo logInCon = new TableLogOnInfo();
                        //logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                        //foreach (Table tb in reporteFactura.Database.Tables)
                        //{
                        //    tb.ApplyLogOnInfo(logInCon);
                        //}
                        //reporteFactura.Refresh();
                        //crystalReportViewer1.ReportSource = reporteFactura;
                        //crystalReportViewer1.RefreshReport();




                    }
                    catch (Exception err)
                    { throw err; }
                }
                #endregion

                #region CERTIFICADO MEDICO
                else if (reporte == "certificado")
                {
                    try
                    {
                        ReportDocument CertifivadoMedico = new ReportDocument();
                        CertifivadoMedico.FileName = Application.StartupPath + "\\Reportes\\Admision\\CertifivadoMedico.rpt";
                        //crystalReportViewer1.ReportSource = reporteAnamnesis;
                        TableLogOnInfo logInCon = new TableLogOnInfo();
                        logInCon.ConnectionInfo.DatabaseName = Application.StartupPath + "\\Reportes\\His3000Reportes.mdb";

                        foreach (Table tb in CertifivadoMedico.Database.Tables)
                        {
                            tb.ApplyLogOnInfo(logInCon);
                        }
                        CertifivadoMedico.Refresh();
                        crystalReportViewer1.ReportSource = CertifivadoMedico;
                        crystalReportViewer1.RefreshReport();

                    }
                    catch (Exception err)
                    { throw err; }
                }
                #endregion

                #region PRE-FACTURA RED

                else if(reporte== "PrefacturaRed")
                {
                    DataTable reporteDatos = new DataTable();
                    NegCertificadoMedico C = new NegCertificadoMedico();
                    PrefacturaAuditoria pre = new PrefacturaAuditoria();

                    try
                    {
                        reporteDatos = NegFormulariosHCU.RecuperaPrefacturaDatos(Convert.ToInt32(parametro));
                        if (reporteDatos.Rows.Count > 0)
                        {
                            for (int i = 0; i < reporteDatos.Rows.Count; i++)
                            {

                            DataRow drKardex;
                            drKardex = pre.Tables["DatosPrefactura"].NewRow();
                            drKardex["Remitente"] = reporteDatos.Rows[i][0].ToString();
                            drKardex["Prestador"] = reporteDatos.Rows[i][1].ToString();
                            drKardex["TipoSeguro"] = reporteDatos.Rows[i][2].ToString();
                            drKardex["Cedula"] = reporteDatos.Rows[i][4].ToString();
                            drKardex["FechaNac"] = reporteDatos.Rows[i][5].ToString();
                            drKardex["Titular"] = reporteDatos.Rows[i][3].ToString();
                            drKardex["CedulaTitular"] = reporteDatos.Rows[i][4].ToString();
                            drKardex["HC"] = reporteDatos.Rows[i][6].ToString();
                            drKardex["FechaIngreso"] = reporteDatos.Rows[i][7].ToString();
                            drKardex["FechaEgreso"] = reporteDatos.Rows[i][8].ToString();
                            drKardex["Factura"] = reporteDatos.Rows[i][10].ToString();
                            drKardex["Total"] = reporteDatos.Rows[i][9].ToString();
                            drKardex["CieIngreso"] = reporteDatos.Rows[i][11].ToString();
                            drKardex["PathImagen"] = C.path();
                            drKardex["Paciente"] = reporteDatos.Rows[i][3].ToString();                            
                            pre.Tables["DatosPrefactura"].Rows.Add(drKardex);
                            }
                        }
                        reporteDatos = null;
                        reporteDatos = NegFormulariosHCU.RecuperaPrefacturaRubros(Convert.ToInt32(parametro));
                        if (reporteDatos.Rows.Count > 0)
                        {
                            for (int i = 0; i < reporteDatos.Rows.Count; i++)
                            {

                                DataRow drKardex;
                                drKardex = pre.Tables["Rubros"].NewRow();
                                drKardex["Fecha"] = reporteDatos.Rows[i][0].ToString();
                                drKardex["Procedimiento"] = reporteDatos.Rows[i][1].ToString();
                                drKardex["Codigo"] = reporteDatos.Rows[i][2].ToString();
                                drKardex["Nivel"] = reporteDatos.Rows[i][3].ToString();
                                drKardex["Detalle"] = reporteDatos.Rows[i][4].ToString();
                                drKardex["ProcAnes"] = reporteDatos.Rows[i][5].ToString();
                                drKardex["Cantidad"] = reporteDatos.Rows[i][6].ToString();
                                drKardex["ValorUnitario"] = reporteDatos.Rows[i][7].ToString();
                                drKardex["ValorTotal"] = reporteDatos.Rows[i][8].ToString();
                                
                                pre.Tables["Rubros"].Rows.Add(drKardex);
                            }
                            Cry_PrefacturaRed myreport = new Cry_PrefacturaRed();
                            myreport.Refresh();
                            myreport.SetDataSource(pre);
                            crystalReportViewer1.ReportSource = myreport;
                            crystalReportViewer1.RefreshReport();
                        }
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                }

                #endregion

                #region KARDEX MEDICAMENTO

                else if (reporte == "KardexMedicamento")
                {
                    DataTable reporteDatos = new DataTable();
                    NegCertificadoMedico C = new NegCertificadoMedico();
                    reporteDatos = NegFormulariosHCU.ReporteDatos(campo1);
                    KardexMedicinas kardex = new KardexMedicinas();

                    for (int i = 0; i < reporteDatos.Rows.Count; i++)
                    {
                        DataRow drKardex;
                        drKardex = kardex.Tables["KARDEXMEDICAMENTOS"].NewRow();
                        drKardex["Usuario"] = reporteDatos.Rows[i][0].ToString();
                        drKardex["Departamento"] = reporteDatos.Rows[i][1].ToString();
                        drKardex["AteCodigo"] = reporteDatos.Rows[i][2].ToString();
                        drKardex["CueCodigo"] = reporteDatos.Rows[i][3].ToString();
                        drKardex["Presentacion"] = reporteDatos.Rows[i][4].ToString();
                        drKardex["Dosis"] = reporteDatos.Rows[i][5].ToString();
                        drKardex["Frecuencia"] = reporteDatos.Rows[i][6].ToString();
                        drKardex["Via"] = reporteDatos.Rows[i][7].ToString();
                        drKardex["Hora"] = reporteDatos.Rows[i][8].ToString();
                        drKardex["FechaAdministracion"] = reporteDatos.Rows[i][9].ToString();
                        drKardex["MedicamentoEventual"] = reporteDatos.Rows[i][10].ToString();
                        drKardex["MedicamentoPropio"] = reporteDatos.Rows[i][11].ToString();
                        drKardex["Paciente"] = reporteDatos.Rows[i][12].ToString();
                        drKardex["Hclinica"] = reporteDatos.Rows[i][13].ToString();
                        drKardex["Logo"] = C.path();
                        //drKardex["Otro"] = reporteDatos.Rows[i][14].ToString();
                        kardex.Tables["KARDEXMEDICAMENTOS"].Rows.Add(drKardex);

                    }
                    //Cambios Edgar 20210128
                    KardexMedicamentos myreport = new KardexMedicamentos();
                    myreport.Refresh();
                    myreport.SetDataSource(kardex);
                    crystalReportViewer1.ReportSource = myreport;
                    crystalReportViewer1.RefreshReport();
                }

                #endregion

                //#region KARDEX INSUMOS

                //else if (reporte == "KardexInsumos")
                //{
                //    DataTable reporteDatos = new DataTable();
                //    NegCertificadoMedico C = new NegCertificadoMedico();
                //    reporteDatos = NegFormulariosHCU.ReporteDatosInsumos(campo1);
                //    //His.Formulario.KardexInsumos kardex = new KardexInsumos();

                //    for (int i = 0; i < reporteDatos.Rows.Count; i++)
                //    {
                //        DataRow drKardex;
                //        drKardex = kardex.Tables["KARDEXINSUMOS"].NewRow();
                //        drKardex["Usuario"] = reporteDatos.Rows[i][0].ToString();
                //        drKardex["Departamento"] = reporteDatos.Rows[i][1].ToString();
                //        drKardex["AteCodigo"] = reporteDatos.Rows[i][2].ToString();
                //        drKardex["CueCodigo"] = reporteDatos.Rows[i][3].ToString();
                //        drKardex["Presentacion"] = reporteDatos.Rows[i][4].ToString();                        
                //        drKardex["Hora"] = reporteDatos.Rows[i][5].ToString();
                //        drKardex["FechaAdministracion"] = reporteDatos.Rows[i][6].ToString();                        
                //        drKardex["MedicamentoPropio"] = reporteDatos.Rows[i][7].ToString();
                //        drKardex["Paciente"] = reporteDatos.Rows[i][8].ToString();
                //        drKardex["Hclinica"] = reporteDatos.Rows[i][9].ToString();
                //        drKardex["Logo"] = C.path();
                //        //drKardex["Otro"] = reporteDatos.Rows[i][14].ToString();
                //        kardex.Tables["KARDEXINSUMOS"].Rows.Add(drKardex);

                //    }
                //    //Cambios Edgar 20210128
                //    KardexInsumosReporte myreport = new KardexInsumosReporte();
                //    myreport.Refresh();
                //    myreport.SetDataSource(kardex);
                //    crystalReportViewer1.ReportSource = myreport;
                //    crystalReportViewer1.RefreshReport();
                //}

                //#endregion

                #region CIERRE ADMISIONES
                else if (reporte == "CierreAdmision")
                {
                    try
                    {
                        CierredeTurno myreport = new CierredeTurno();
                        myreport.Refresh();
                        myreport.SetDataSource(Datos);
                        crystalReportViewer1.ReportSource = myreport;
                        crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                #endregion
                #region FORM.021 EVOLUCION ENFERMERIA
                else if (reporte == "FORM021")
                {
                    try
                    {
                        RPTevolucionEnfermeria myreport = new RPTevolucionEnfermeria();
                        myreport.Refresh();
                        myreport.SetDataSource(Datos);
                        crystalReportViewer1.ReportSource = myreport;
                        crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                #endregion
                #region FORM.022 KARDEX MEDICAMENTOS
                else if (reporte == "FORM022")
                {
                    try
                    {
                        //rptFORM022 myreport = new rptFORM022();
                        CrystalReport1 myreport = new CrystalReport1();
                        myreport.Refresh();
                        myreport.SetDataSource(Datos);
                        crystalReportViewer1.ReportSource = myreport;
                        crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                #endregion
                #region FORM.022 KARDEX INSUMOS
                else if (reporte == "FORM022i")
                {
                    try
                    {
                        //rptFORM022 myreport = new rptFORM022();
                        Form022i myreport = new Form022i();
                        myreport.Refresh();
                        myreport.SetDataSource(Datos);
                        crystalReportViewer1.ReportSource = myreport;
                        crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                #endregion
                #region Reporte de Edades
                else if (reporte == "ReporteEdades")
                {
                    try
                    {
                        ReporteEdadesAtenciones myreport = new ReporteEdadesAtenciones();
                        myreport.Refresh();
                        myreport.SetDataSource(Datos);
                        crystalReportViewer1.ReportSource = myreport;
                        crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                #endregion
                #region ConsultaExterna
                else if (reporte == "ConsultaExterna")
                {
                    try
                    {

                        HCU_Form002MSPrpt myreport = new HCU_Form002MSPrpt();
                        myreport.Refresh();
                        myreport.SetDataSource(Datos);
                        crystalReportViewer1.ReportSource = myreport;
                        crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                #endregion
                #region Ticket Dieta
                else if (reporte == "TicketDieta")
                {
                    try
                    {
                        Ticket_Dieta myreport = new Ticket_Dieta();
                        myreport.Refresh();
                        myreport.SetDataSource(Datos);
                        crystalReportViewer1.ReportSource = myreport;
                        crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                #endregion

                else if (reporte == "Consentimiento")
                {
                    try
                    {
                        form024_Consentimiento_Informado myreport = new form024_Consentimiento_Informado();
                        myreport.Refresh();
                        myreport.SetDataSource(Datos);
                        crystalReportViewer1.ReportSource = myreport;
                        crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else if (reporte == "Auditoria")
                {
                    try
                    {
                        CryAuditoriaCambios myreport = new CryAuditoriaCambios();
                        myreport.Refresh();
                        myreport.SetDataSource(Datos);
                        crystalReportViewer1.ReportSource = myreport;
                        crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else if (reporte == "Admision")
                {
                    try
                    {
                        Form001 myreport = new Form001();
                        //myreport.Refresh();
                        myreport.SetDataSource(Datos);
                        myreport.Subreports[0].SetDataSource(Datos1);
                        myreport.Subreports[1].SetDataSource(Datos2);
                        crystalReportViewer1.ReportSource = myreport;
                        crystalReportViewer1.RefreshReport();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else if (reporte == "TarifarioHonorario")
                {
                    try
                    {
                        HonorarioTarifario myreport = new HonorarioTarifario();
                        myreport.Refresh();
                        myreport.SetDataSource(Datos);
                        crystalReportViewer1.ReportSource = myreport;
                        crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else if (reporte == "HonorarioReporte")
                {
                    try
                    {
                        rptSubHonorario myreport = new rptSubHonorario();
                        myreport.Refresh();
                        myreport.SetDataSource(Datos);
                        crystalReportViewer1.ReportSource = myreport;
                        crystalReportViewer1.RefreshReport();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                #region Brazzalete
                else if (reporte == "Brazzalete")
                {
                    try
                    {
                        //rptFORM022 myreport = new rptFORM022();
                        Brazzalete myreport = new Brazzalete();
                        myreport.Refresh();
                        myreport.SetDataSource(Datos);
                        crystalReportViewer1.ReportSource = myreport;
                        crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                #endregion
                #region DETALLEITEM
                else if (reporte == "DetalleItem")
                {
                    try
                    {
                        rpt_DetalleItem myreport = new rpt_DetalleItem();
                        myreport.Refresh();
                        myreport.SetDataSource(Datos);
                        crystalReportViewer1.ReportSource = myreport;
                        crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                #endregion


            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmReportes_Load(object sender, EventArgs e)
        {
            cargarReporte();
            //crystalReportViewer1.RefreshReport();            
        }



        private void frmReportes_FormClosed(object sender, FormClosedEventArgs e)
        {
            crystalReportViewer1.Dispose();
        }

    }
}
