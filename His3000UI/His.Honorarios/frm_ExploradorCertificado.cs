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
using His.Entidades;
using His.Formulario;
using His.Entidades.Clases;
using System.Globalization;

namespace His.Honorarios
{
    public partial class frm_ExploradorCertificado : Form
    {
        public frm_ExploradorCertificado()
        {
            InitializeComponent();
            //Primero obtenemos el día actual
            DateTime date = DateTime.Now;

            //Asi obtenemos el primer dia del mes actual
            DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);
            dtpFiltroDesde.Value = oPrimerDiaDelMes;
            dtpFiltroHasta.Value = DateTime.Now;
        }

        public void CargarCertificados()
        {
            try
            {
                if(dtpFiltroDesde.Value > dtpFiltroHasta.Value)
                {
                    errorProvider1.SetError(grpFechas, "La fecha desde no puede ser mayor a fecha hasta.");
                }
                else
                {
                    UltraGridCertificados.DataSource = null;
                    if (chkAnulados.Checked)
                    {
                        Reportes(false);
                        btnimprimir.Enabled = false;
                        btnanular.Enabled = false;
                    }
                    else
                    {
                        Reportes(true);
                        btnimprimir.Enabled = true;
                        btnanular.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo ocurrio al cargar certificados del mes actual.\r\nMas detalles: " +ex.Message , "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Reportes(bool estado)
        {
            if (dtpFiltroDesde.Value.Date < DateTime.Now.Date)
            {
                dtpFiltroHasta.Value = dtpFiltroHasta.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                //UltraGridCertificados.DataSource = NegCertificadoMedico.CertificadosMedicos(dtpFiltroDesde.Value.Date, dtpFiltroHasta.Value);
                if (Sesion.codDepartamento == 1 || Sesion.codDepartamento == 5 || Sesion.codDepartamento == 7 || Sesion.codDepartamento == 10)
                    UltraGridCertificados.DataSource = NegCertificadoMedico.CertificadosMedicos(dtpFiltroDesde.Value.Date, dtpFiltroHasta.Value, estado);
                else
                    UltraGridCertificados.DataSource = NegCertificadoMedico.CertificadoXmedicos(dtpFiltroDesde.Value.Date, dtpFiltroHasta.Value, Sesion.codMedico, estado);
            }
            else
            {
                //DataTable usu = new DataTable();
                //usu = NegUsuarios.ConsultaUsuarioDep(Sesion.codDepartamento);
                if (Sesion.codDepartamento == 1 || Sesion.codDepartamento == 5 || Sesion.codDepartamento == 7 || Sesion.codDepartamento == 10)
                    UltraGridCertificados.DataSource = NegCertificadoMedico.CertificadosMedicos(dtpFiltroDesde.Value.Date, dtpFiltroHasta.Value, estado);
                else
                    UltraGridCertificados.DataSource = NegCertificadoMedico.CertificadoXmedicos(dtpFiltroDesde.Value.Date, dtpFiltroHasta.Value, Sesion.codMedico, estado);
            }
        }
        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                if (UltraGridCertificados.Rows.Count > 0)
                {
                    string PathExcel = FindSavePath();
                    if (PathExcel != null)
                    {
                        if (UltraGridCertificados.CanFocus == true)
                            this.ultraGridExcelExporter1.Export(UltraGridCertificados, PathExcel);
                        MessageBox.Show("Se termino de exportar el grid en el archivo " + PathExcel);
                    }
                }
                else
                    MessageBox.Show("No hay registros para exportar.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); 
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            finally
            { this.Cursor = Cursors.Default; }
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarCertificados();
        }

        private void frm_ExploradorCertificado_Load(object sender, EventArgs e)
        {
            CargarCertificados();
        }

        private void UltraGridCertificados_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            try
            {
                UltraGridBand bandUno = UltraGridCertificados.DisplayLayout.Bands[0];

                UltraGridCertificados.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
                //grid.DisplayLayout.Override.Allow

                UltraGridCertificados.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                UltraGridCertificados.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                UltraGridCertificados.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

                bandUno.Override.CellClickAction = CellClickAction.RowSelect;
                bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

                UltraGridCertificados.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
                UltraGridCertificados.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
                UltraGridCertificados.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;


                //Caracteristicas de Filtro en la grilla
                UltraGridCertificados.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                UltraGridCertificados.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                UltraGridCertificados.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                UltraGridCertificados.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
                UltraGridCertificados.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
                //
                UltraGridCertificados.DisplayLayout.UseFixedHeaders = true;

                //Dimension los registros
                UltraGridCertificados.DisplayLayout.Bands[0].Columns[3].Width = 300;
                UltraGridCertificados.DisplayLayout.Bands[0].Columns[4].Width = 300;
                UltraGridCertificados.DisplayLayout.Bands[0].Columns[6].Width = 500;

                //agrandamiento de filas 

                ////Ocultar columnas, que son fundamentales al levantar informacion
                //UltraGridPacientes.DisplayLayout.Bands[0].Columns[3].Hidden = false;
                //UltraGridPacientes.DisplayLayout.Bands[0].Columns[5].Hidden = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        NegCertificadoMedico Certificado = new NegCertificadoMedico();
        private void btnimprimir_Click(object sender, EventArgs e)
        {

            if(UltraGridCertificados.Selected.Rows.Count == 1)
            {
                UltraGridRow fila = this.UltraGridCertificados.ActiveRow;
                DataTable Tabla = new DataTable();
                Tabla = NegCertificadoMedico.ReimpresionCertificado(Convert.ToInt32(fila.Cells["NRO CERTIFICADO"].Value.ToString()));

                ATENCIONES atencion = new ATENCIONES();
                atencion = NegAtenciones.RecuperarAtencionPorNumero(fila.Cells["NRO ATENCION"].Value.ToString());

                DataTable reporteDatos = new DataTable();
                DataTable detalleReporteDatos = new DataTable();
                reporteDatos = Certificado.CargarDatosCertificado(atencion.ATE_CODIGO.ToString());
                detalleReporteDatos = Certificado.CargarDatosCertificado_Detalle(Convert.ToInt64(reporteDatos.Rows[0][9].ToString()));

                Certificado_Medico CM = new Certificado_Medico();

                PACIENTES_DATOS_ADICIONALES pacien = new PACIENTES_DATOS_ADICIONALES();
                PACIENTES pacienteActual = new PACIENTES();
                pacienteActual = NegPacientes.recuperarPacientePorAtencion(Convert.ToInt32(atencion.ATE_CODIGO));

                pacien = NegPacienteDatosAdicionales.RecuperarDatosAdicionalesPaciente(pacienteActual.PAC_CODIGO);

                EMPRESA empresa = new EMPRESA();
                empresa = NegEmpresa.RecuperaEmpresa();

                string FechadeIngreso, FechadeAlta, dias_reposo;

                MEDICOS medico = NegMedicos.RecuperaMedicoId(Convert.ToInt32(Tabla.Rows[0]["MED_CODIGO"].ToString()));

                if (Convert.ToBoolean(Tabla.Rows[0]["CER_ESTADO"].ToString()) == true)
                {
                    if (atencion.ATE_FECHA_ALTA != null)
                    {
                        if (Tabla.Rows[0]["CER_TIPO_INGRESO"].ToString() == "1") //ES EMERGENCIA SIN FECHA DE ALTA
                        {
                            DataRow drCertificado;
                            foreach (DataRow item in detalleReporteDatos.Rows)
                            {
                                drCertificado = CM.Tables["EMERGENCIA"].NewRow();
                                drCertificado["Paciente"] = reporteDatos.Rows[0][0].ToString();
                                drCertificado["Identificacion"] = reporteDatos.Rows[0][1].ToString();
                                FechadeIngreso = Fecha_En_Palabra(atencion.ATE_FECHA_INGRESO.ToString());
                                drCertificado["FechaIngreso"] = FechadeIngreso;
                                drCertificado["Cie_Codigo"] = item["CIE_CODIGO"];
                                drCertificado["Cie_Descripcion"] = item["CIE_DESCRIPCION"];
                                drCertificado["Observacion"] = reporteDatos.Rows[0][10].ToString() + "\r\nACTIVIDAD LABORAL: " +
                                  Tabla.Rows[0]["CER_ACTIVIDAD_LABORAL"].ToString() + "\r\nTIPO CONTINGENCIA: " + Tabla.Rows[0]["CER_CONTINGENCIA"].ToString();
                                dias_reposo = Dia_En_Palabras(reporteDatos.Rows[0][5].ToString());
                                drCertificado["Dias_Reposo"] = reporteDatos.Rows[0][5].ToString() + " (" + dias_reposo + ")";
                                DateTime FechaHoy =atencion.ATE_FECHA_ALTA.Value;
                                FechadeAlta = Fecha_En_Palabra(FechaHoy.ToShortDateString());
                                FechaHoy = FechaHoy.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString()) - 1);
                                drCertificado["FechaAlta"] = FechadeAlta;
                                drCertificado["Dias_FinReposo"] = FechaHoy.ToString("dd") + " (" + Dia_En_Palabras(FechaHoy.ToString("dd")) + ")" + " de " + FechaHoy.ToString("MMMM") + " del " + FechaHoy.ToString("yyyy");
                                drCertificado["Num_Certificado"] = reporteDatos.Rows[0][9].ToString();
                                drCertificado["Empresa"] = reporteDatos.Rows[0][11].ToString();
                                drCertificado["Direccion_Empresa"] = reporteDatos.Rows[0][12].ToString();
                                drCertificado["Telefono_Empresa"] = reporteDatos.Rows[0][13].ToString();
                                drCertificado["emailEmpresa"] = empresa.EMP_EMAIL;

                                // else
                                // {
                                drCertificado["Nombre_Medico"] = medico.MED_APELLIDO_PATERNO + ' ' + medico.MED_APELLIDO_MATERNO + ' ' + medico.MED_NOMBRE1 + ' ' + medico.MED_NOMBRE2;
                                drCertificado["Email_Medico"] = medico.MED_EMAIL;
                                drCertificado["Identificacion_Medico"] = medico.MED_RUC;
                                drCertificado["telefonoMedico"] = medico.MED_TELEFONO_CASA;
                                //}
                                drCertificado["Tipo"] = "emergencia";
                                drCertificado["PathImagen"] = Certificado.path();
                                CM.Tables["EMERGENCIA"].Rows.Add(drCertificado);
                            }
                            His.Formulario.frmReportes reporte = new His.Formulario.frmReportes(1, "CertificadoEA", CM);
                            reporte.Show();
                        }
                        else if (Tabla.Rows[0]["CER_TIPO_INGRESO"].ToString() == "2")//ES HOSPITALIZACION SIN FECHA DE ALTA
                        {
                            if (Tabla.Rows[0]["CER_TRATAMIENTO"].ToString() == "CLINICO")
                            {
                                DataRow drCertificado;
                                foreach (DataRow item in detalleReporteDatos.Rows)
                                {

                                    drCertificado = CM.Tables["HOSPITALARIO"].NewRow();
                                    drCertificado["Paciente"] = reporteDatos.Rows[0][0].ToString();
                                    drCertificado["Identificacion"] = reporteDatos.Rows[0][1].ToString();
                                    drCertificado["HC"] = reporteDatos.Rows[0][2].ToString();
                                    FechadeIngreso = Fecha_En_Palabra(reporteDatos.Rows[0][3].ToString());
                                    drCertificado["FechaIngreso"] = FechadeIngreso;
                                    dias_reposo = Dia_En_Palabras(reporteDatos.Rows[0][5].ToString());
                                    drCertificado["Dias_Reposo"] = drCertificado["Dias_Reposo"] = reporteDatos.Rows[0][5].ToString() + " (" + dias_reposo + ")";
                                    if (reporteDatos.Rows[0][4].ToString() == "")
                                    {
                                        FechadeAlta = Fecha_En_Palabra(atencion.ATE_FECHA_ALTA.ToString());
                                        drCertificado["FechaAlta"] = FechadeAlta;
                                        DateTime Fecha = Convert.ToDateTime(atencion.ATE_FECHA_ALTA.ToString());
                                        string resultado = Fecha_En_Palabra(Convert.ToString(Fecha.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString()) - 1)));
                                        drCertificado["Dias_FinReposo"] = resultado;
                                    }
                                    else
                                    {
                                        FechadeAlta = Fecha_En_Palabra(reporteDatos.Rows[0][4].ToString());
                                        drCertificado["FechaAlta"] = FechadeAlta;
                                        DateTime Fecha = Convert.ToDateTime(reporteDatos.Rows[0][4].ToString());
                                        string resultado = Fecha_En_Palabra(Convert.ToString(Fecha.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString()) - 1)));
                                        drCertificado["Dias_FinReposo"] = resultado;
                                    }

                                    drCertificado["Nombre_Medico"] = medico.MED_APELLIDO_PATERNO + ' ' + medico.MED_APELLIDO_MATERNO + ' ' + medico.MED_NOMBRE1 + ' ' + medico.MED_NOMBRE2;
                                    drCertificado["Email_Medico"] = medico.MED_EMAIL;
                                    drCertificado["Identificacion_Medico"] = medico.MED_RUC;
                                    drCertificado["telefonoMedico"] = medico.MED_TELEFONO_CASA;

                                    drCertificado["Empresa"] = reporteDatos.Rows[0][11].ToString();
                                    drCertificado["Direccion_Empresa"] = reporteDatos.Rows[0][12].ToString();
                                    drCertificado["Telefono_Empresa"] = reporteDatos.Rows[0][13].ToString();
                                    drCertificado["emailEmpresa"] = empresa.EMP_EMAIL;
                                    drCertificado["Num_Certificado"] = reporteDatos.Rows[0][9].ToString();

                                    drCertificado["Observacion"] = reporteDatos.Rows[0][10].ToString() + "\r\nACTIVIDAD LABORAL: " +
                                    Tabla.Rows[0]["CER_ACTIVIDAD_LABORAL"].ToString() + "\r\nTIPO CONTINGENCIA: " + Tabla.Rows[0]["CER_CONTINGENCIA"].ToString();

                                    drCertificado["Cie_Codigo"] = item["CIE_CODIGO"];
                                    drCertificado["Cie_Descripcion"] = item["CIE_DESCRIPCION"];
                                    drCertificado["PathImagen"] = Certificado.path();
                                    drCertificado["Tratamiento"] = "TIPO DE TRATAMIENTO: " + Tabla.Rows[0]["CER_TRATAMIENTO"].ToString();
                                    drCertificado["FechaTratamiento"] = "";
                                    drCertificado["Procedimiento"] = "";
                                    CM.Tables["HOSPITALARIO"].Rows.Add(drCertificado);
                                }
                                His.Formulario.frmReportes myreport = new His.Formulario.frmReportes(1, "CertificadoHA", CM);
                                myreport.Show();
                            }
                            else
                            {
                                DataRow drCertificado;
                                foreach (DataRow item in detalleReporteDatos.Rows)
                                {
                                    drCertificado = CM.Tables["HOSPITALARIO"].NewRow();
                                    drCertificado["Paciente"] = reporteDatos.Rows[0][0].ToString();
                                    drCertificado["Identificacion"] = reporteDatos.Rows[0][1].ToString();
                                    drCertificado["HC"] = reporteDatos.Rows[0][2].ToString();
                                    FechadeIngreso = Fecha_En_Palabra(reporteDatos.Rows[0][3].ToString());
                                    drCertificado["FechaIngreso"] = FechadeIngreso;
                                    dias_reposo = Dia_En_Palabras(reporteDatos.Rows[0][5].ToString());
                                    drCertificado["Dias_Reposo"] = drCertificado["Dias_Reposo"] = reporteDatos.Rows[0][5].ToString() + " (" + dias_reposo + ")";
                                    if (reporteDatos.Rows[0][4].ToString() == "")
                                    {
                                        FechadeAlta = Fecha_En_Palabra(atencion.ATE_FECHA_ALTA.ToString());
                                        drCertificado["FechaAlta"] = FechadeAlta;
                                        DateTime Fecha = Convert.ToDateTime(atencion.ATE_FECHA_ALTA.ToString());
                                        string resultado = Fecha_En_Palabra(Convert.ToString(Fecha.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString()) - 1)));
                                        drCertificado["Dias_FinReposo"] = resultado;
                                    }
                                    else
                                    {
                                        FechadeAlta = Fecha_En_Palabra(reporteDatos.Rows[0][4].ToString());
                                        drCertificado["FechaAlta"] = FechadeAlta;
                                        DateTime Fecha = Convert.ToDateTime(reporteDatos.Rows[0][4].ToString());
                                        string resultado = Fecha_En_Palabra(Convert.ToString(Fecha.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString()) - 1)));
                                        drCertificado["Dias_FinReposo"] = resultado;
                                    }

                                    drCertificado["Nombre_Medico"] = medico.MED_APELLIDO_PATERNO + ' ' + medico.MED_APELLIDO_MATERNO + ' ' + medico.MED_NOMBRE1 + ' ' + medico.MED_NOMBRE2;
                                    drCertificado["Email_Medico"] = medico.MED_EMAIL;
                                    drCertificado["Identificacion_Medico"] = medico.MED_RUC;
                                    drCertificado["telefonoMedico"] = medico.MED_TELEFONO_CASA;

                                    drCertificado["Empresa"] = reporteDatos.Rows[0][11].ToString();
                                    drCertificado["Direccion_Empresa"] = reporteDatos.Rows[0][12].ToString();
                                    drCertificado["Telefono_Empresa"] = reporteDatos.Rows[0][13].ToString();
                                    drCertificado["emailEmpresa"] = empresa.EMP_EMAIL;
                                    drCertificado["Num_Certificado"] = reporteDatos.Rows[0][9].ToString();

                                    drCertificado["Observacion"] = reporteDatos.Rows[0][10].ToString() + "\r\nACTIVIDAD LABORAL: " +
                                       Tabla.Rows[0]["CER_ACTIVIDAD_LABORAL"].ToString() + "\r\nTIPO CONTINGENCIA: " + Tabla.Rows[0]["CER_CONTINGENCIA"].ToString();

                                    drCertificado["Cie_Codigo"] = item["CIE_CODIGO"];
                                    drCertificado["Cie_Descripcion"] = item["CIE_DESCRIPCION"];
                                    drCertificado["PathImagen"] = Certificado.path();
                                    drCertificado["Tratamiento"] = "TIPO DE TRATAMIENTO: " + Tabla.Rows[0]["CER_TRATAMIENTO"].ToString();
                                    drCertificado["FechaTratamiento"] = "FECHA: " + Tabla.Rows[0]["CER_FECHA_CIRUGIA"].ToString();
                                    drCertificado["Procedimiento"] = "PROCEDIMIENTO: " + Tabla.Rows[0]["CER_PROCEDIMIENTO"].ToString();
                                    CM.Tables["HOSPITALARIO"].Rows.Add(drCertificado);
       
                                }
                                His.Formulario.frmReportes myreport = new His.Formulario.frmReportes(1, "CertificadoHA", CM);                                
                                myreport.Show();
                                
                            }
                        }
                        else if (Tabla.Rows[0]["CER_TIPO_INGRESO"].ToString() == "4") //ES CONSULTA EXTERNA SIN FECHA DE ALTA
                        {
                            DataRow drCertificado;
                            foreach (DataRow item in detalleReporteDatos.Rows)
                            {
                                drCertificado = CM.Tables["EMERGENCIA"].NewRow();
                                drCertificado["Paciente"] = reporteDatos.Rows[0][0].ToString();
                                drCertificado["Identificacion"] = reporteDatos.Rows[0][1].ToString();
                                FechadeIngreso = Fecha_En_Palabra(reporteDatos.Rows[0][3].ToString());
                                drCertificado["FechaIngreso"] = FechadeIngreso;
                                drCertificado["Cie_Codigo"] = item["CIE_CODIGO"];
                                drCertificado["Cie_Descripcion"] = item["CIE_DESCRIPCION"];
                                DateTime FechaHoy = DateTime.Now;
                                FechadeAlta = FechaHoy.ToString("dd") + " de " + FechaHoy.ToString("MMMM") + " del " + FechaHoy.ToString("yyyy");
                                drCertificado["FechaAlta"] = FechadeAlta;
                                drCertificado["Observacion"] = reporteDatos.Rows[0][10].ToString() + "\r\n\r\nACTIVIDAD LABORAL: " +
                                   Tabla.Rows[0]["CER_ACTIVIDAD_LABORAL"].ToString() + "\r\nTIPO CONTINGENCIA: " + Tabla.Rows[0]["CER_CONTINGENCIA"].ToString();

                                dias_reposo = Dia_En_Palabras(reporteDatos.Rows[0][5].ToString());
                                drCertificado["Dias_Reposo"] = reporteDatos.Rows[0][5].ToString() + " (" + dias_reposo + ")";
                                FechadeAlta = Fecha_En_Palabra(FechaHoy.ToShortDateString());
                                drCertificado["FechaAlta"] = FechadeAlta;
                                FechaHoy = FechaHoy.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString()) - 1);
                                drCertificado["Dias_FinReposo"] = FechaHoy.ToString("dd") + " (" + Dia_En_Palabras(FechaHoy.ToString("dd")) + ")" + " de " + FechaHoy.ToString("MMMM") + " del " + FechaHoy.ToString("yyyy");
                                drCertificado["Num_Certificado"] = reporteDatos.Rows[0][9].ToString();
                                drCertificado["Empresa"] = reporteDatos.Rows[0][11].ToString();
                                drCertificado["Direccion_Empresa"] = reporteDatos.Rows[0][12].ToString();
                                drCertificado["Telefono_Empresa"] = reporteDatos.Rows[0][13].ToString();
                                drCertificado["emailEmpresa"] = empresa.EMP_EMAIL;

                                drCertificado["Nombre_Medico"] = medico.MED_APELLIDO_PATERNO + ' ' + medico.MED_APELLIDO_MATERNO + ' ' + medico.MED_NOMBRE1 + ' ' + medico.MED_NOMBRE2;
                                drCertificado["Email_Medico"] = medico.MED_EMAIL;
                                drCertificado["Identificacion_Medico"] = medico.MED_RUC;
                                drCertificado["telefonoMedico"] = medico.MED_TELEFONO_CASA;

                                drCertificado["Tipo"] = "consulta externa";
                                drCertificado["PathImagen"] = Certificado.path();
                                CM.Tables["EMERGENCIA"].Rows.Add(drCertificado);
                            }
                            His.Formulario.frmReportes reporte = new His.Formulario.frmReportes(1, "CertificadoEA", CM);
                            reporte.Show();
                        }
                    }
                    else
                    {
                        if (Tabla.Rows[0]["CER_TIPO_INGRESO"].ToString() == "1") //ES EMERGENCIA SIN FECHA DE ALTA
                        {
                            DataRow drCertificado;
                            foreach (DataRow item in detalleReporteDatos.Rows)
                            {
                                drCertificado = CM.Tables["EMERGENCIA"].NewRow();
                                drCertificado["Paciente"] = reporteDatos.Rows[0][0].ToString();
                                drCertificado["Identificacion"] = reporteDatos.Rows[0][1].ToString();
                                FechadeIngreso = Fecha_En_Palabra(atencion.ATE_FECHA_INGRESO.ToString());
                                drCertificado["FechaIngreso"] = FechadeIngreso;
                                drCertificado["Cie_Codigo"] = item["CIE_CODIGO"];
                                drCertificado["Cie_Descripcion"] = item["CIE_DESCRIPCION"];
                                drCertificado["Observacion"] = reporteDatos.Rows[0][10].ToString() + "\r\nACTIVIDAD LABORAL: " +
                                  Tabla.Rows[0]["CER_ACTIVIDAD_LABORAL"].ToString() + "\r\nTIPO CONTINGENCIA: " + Tabla.Rows[0]["CER_CONTINGENCIA"].ToString();
                                dias_reposo = Dia_En_Palabras(reporteDatos.Rows[0][5].ToString());
                                drCertificado["Dias_Reposo"] = reporteDatos.Rows[0][5].ToString() + " (" + dias_reposo + ")";
                                DateTime FechaHoy = DateTime.Now;
                                FechadeAlta = Fecha_En_Palabra(FechaHoy.ToShortDateString());
                                FechaHoy = FechaHoy.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString()) - 1);
                                drCertificado["FechaAlta"] = FechadeAlta;
                                drCertificado["Dias_FinReposo"] = FechaHoy.ToString("dd") + " (" + Dia_En_Palabras(FechaHoy.ToString("dd")) + ")" + " de " + FechaHoy.ToString("MMMM") + " del " + FechaHoy.ToString("yyyy");
                                drCertificado["Num_Certificado"] = reporteDatos.Rows[0][9].ToString();
                                drCertificado["Empresa"] = reporteDatos.Rows[0][11].ToString();
                                drCertificado["Direccion_Empresa"] = reporteDatos.Rows[0][12].ToString();
                                drCertificado["Telefono_Empresa"] = reporteDatos.Rows[0][13].ToString();
                                drCertificado["emailEmpresa"] = empresa.EMP_EMAIL;

                                // else
                                // {
                                drCertificado["Nombre_Medico"] = medico.MED_APELLIDO_PATERNO + ' ' + medico.MED_APELLIDO_MATERNO + ' ' + medico.MED_NOMBRE1 + ' ' + medico.MED_NOMBRE2;
                                drCertificado["Email_Medico"] = medico.MED_EMAIL;
                                drCertificado["Identificacion_Medico"] = medico.MED_RUC;
                                drCertificado["telefonoMedico"] = medico.MED_TELEFONO_CASA;
                                //}
                                drCertificado["Tipo"] = "emergencia";
                                drCertificado["PathImagen"] = Certificado.path();
                                CM.Tables["EMERGENCIA"].Rows.Add(drCertificado);
                            }
                            His.Formulario.frmReportes reporte = new His.Formulario.frmReportes(1, "CertificadoEA", CM);
                            reporte.Show();
                        }
                        else if (Tabla.Rows[0]["CER_TIPO_INGRESO"].ToString() == "2")//ES HOSPITALIZACION SIN FECHA DE ALTA
                        {
                            if (Tabla.Rows[0]["CER_TRATAMIENTO"].ToString() == "CLINICO")
                            {
                                DataRow drCertificado;
                                foreach (DataRow item in detalleReporteDatos.Rows)
                                {
                                    drCertificado = CM.Tables["HOSPITALARIO"].NewRow();
                                    drCertificado["Paciente"] = reporteDatos.Rows[0][0].ToString();
                                    drCertificado["Identificacion"] = reporteDatos.Rows[0][1].ToString();
                                    drCertificado["HC"] = reporteDatos.Rows[0][2].ToString();
                                    FechadeIngreso = Fecha_En_Palabra(reporteDatos.Rows[0][3].ToString());
                                    drCertificado["FechaIngreso"] = FechadeIngreso;
                                    drCertificado["Cie_Codigo"] = item["CIE_CODIGO"];
                                    drCertificado["Cie_Descripcion"] = item["CIE_DESCRIPCION"];

                                    drCertificado["Nombre_Medico"] = medico.MED_APELLIDO_PATERNO + ' ' + medico.MED_APELLIDO_MATERNO + ' ' + medico.MED_NOMBRE1 + ' ' + medico.MED_NOMBRE2;
                                    drCertificado["Email_Medico"] = medico.MED_EMAIL;
                                    drCertificado["Identificacion_Medico"] = medico.MED_RUC;
                                    drCertificado["telefonoMedico"] = medico.MED_TELEFONO_CASA;

                                    drCertificado["Empresa"] = reporteDatos.Rows[0][11].ToString();
                                    drCertificado["Direccion_Empresa"] = reporteDatos.Rows[0][12].ToString();
                                    drCertificado["Telefono_Empresa"] = reporteDatos.Rows[0][13].ToString();
                                    drCertificado["emailEmpresa"] = empresa.EMP_EMAIL;
                                    drCertificado["Num_Certificado"] = reporteDatos.Rows[0][9].ToString();

                                    drCertificado["Observacion"] = reporteDatos.Rows[0][10].ToString() + "\r\n\r\nACTIVIDAD LABORAL: " +
                                        Tabla.Rows[0]["CER_ACTIVIDAD_LABORAL"].ToString() + "\r\nTIPO CONTINGENCIA: " + Tabla.Rows[0]["CER_CONTINGENCIA"].ToString();

                                    drCertificado["PathImagen"] = Certificado.path();
                                    drCertificado["Tratamiento"] = "TIPO DE TRATAMIENTO: " + Tabla.Rows[0]["CER_TRATAMIENTO"].ToString();
                                    drCertificado["FechaTratamiento"] = "";
                                    drCertificado["Procedimiento"] = "";
                                    CM.Tables["HOSPITALARIO"].Rows.Add(drCertificado);
                                }
                                His.Formulario.frmReportes reporte = new His.Formulario.frmReportes(1, "CertificadoHSA", CM);
                                reporte.Show();
                            }
                            else
                            {
                                DataRow drCertificado;
                                foreach (DataRow item in detalleReporteDatos.Rows)
                                {
                                    drCertificado = CM.Tables["HOSPITALARIO"].NewRow();
                                    drCertificado["Paciente"] = reporteDatos.Rows[0][0].ToString();
                                    drCertificado["Identificacion"] = reporteDatos.Rows[0][1].ToString();
                                    drCertificado["HC"] = reporteDatos.Rows[0][2].ToString();
                                    FechadeIngreso = Fecha_En_Palabra(reporteDatos.Rows[0][3].ToString());
                                    drCertificado["FechaIngreso"] = FechadeIngreso;
                                    drCertificado["Cie_Codigo"] = item["CIE_CODIGO"];
                                    drCertificado["Cie_Descripcion"] = item["CIE_DESCRIPCION"];

                                    drCertificado["Nombre_Medico"] = medico.MED_APELLIDO_PATERNO + ' ' + medico.MED_APELLIDO_MATERNO + ' ' + medico.MED_NOMBRE1 + ' ' + medico.MED_NOMBRE2;
                                    drCertificado["Email_Medico"] = medico.MED_EMAIL;
                                    drCertificado["Identificacion_Medico"] = medico.MED_RUC;
                                    drCertificado["telefonoMedico"] = medico.MED_TELEFONO_CASA;

                                    drCertificado["Empresa"] = reporteDatos.Rows[0][11].ToString();
                                    drCertificado["Direccion_Empresa"] = reporteDatos.Rows[0][12].ToString();
                                    drCertificado["Telefono_Empresa"] = reporteDatos.Rows[0][13].ToString();
                                    drCertificado["emailEmpresa"] = empresa.EMP_EMAIL;
                                    drCertificado["Num_Certificado"] = reporteDatos.Rows[0][9].ToString();

                                    drCertificado["Observacion"] = reporteDatos.Rows[0][10].ToString() + "\r\n\r\nACTIVIDAD LABORAL: " +
                                       Tabla.Rows[0]["CER_ACTIVIDAD_LABORAL"].ToString() + "\r\nTIPO CONTINGENCIA: " + Tabla.Rows[0]["CER_CONTINGENCIA"].ToString();

                                    drCertificado["PathImagen"] = Certificado.path();
                                    drCertificado["Tratamiento"] = "TIPO DE TRATAMIENTO: " + Tabla.Rows[0]["CER_TRATAMIENTO"].ToString();
                                    drCertificado["FechaTratamiento"] = "FECHA: " + Tabla.Rows[0]["CER_FECHA_CIRUGIA"].ToString();
                                    drCertificado["Procedimiento"] = "PROCEDIMIENTO: " + Tabla.Rows[0]["CER_PROCEDIMIENTO"].ToString();
                                    CM.Tables["HOSPITALARIO"].Rows.Add(drCertificado);
                                }
                                His.Formulario.frmReportes reporte = new His.Formulario.frmReportes(1, "CertificadoHSA", CM);
                                reporte.Show();
                            }
                        }
                        else if (Tabla.Rows[0]["CER_TIPO_INGRESO"].ToString() == "4") //ES CONSULTA EXTERNA SIN FECHA DE ALTA
                        {
                            DataRow drCertificado;
                            foreach (DataRow item in detalleReporteDatos.Rows)
                            {
                                drCertificado = CM.Tables["EMERGENCIA"].NewRow();
                                drCertificado["Paciente"] = reporteDatos.Rows[0][0].ToString();
                                drCertificado["Identificacion"] = reporteDatos.Rows[0][1].ToString();
                                FechadeIngreso = Fecha_En_Palabra(reporteDatos.Rows[0][3].ToString());
                                drCertificado["FechaIngreso"] = FechadeIngreso;
                                drCertificado["Cie_Codigo"] = item["CIE_CODIGO"];
                                drCertificado["Cie_Descripcion"] = item["CIE_DESCRIPCION"];
                                DateTime FechaHoy = DateTime.Now;
                                FechadeAlta = FechaHoy.ToString("dd") + " de " + FechaHoy.ToString("MMMM") + " del " + FechaHoy.ToString("yyyy");
                                drCertificado["FechaAlta"] = FechadeAlta;
                                drCertificado["Observacion"] = reporteDatos.Rows[0][10].ToString() + "\r\n\r\nACTIVIDAD LABORAL: " +
                                   Tabla.Rows[0]["CER_ACTIVIDAD_LABORAL"].ToString() + "\r\nTIPO CONTINGENCIA: " + Tabla.Rows[0]["CER_CONTINGENCIA"].ToString();

                                dias_reposo = Dia_En_Palabras(reporteDatos.Rows[0][5].ToString());
                                drCertificado["Dias_Reposo"] = reporteDatos.Rows[0][5].ToString() + " (" + dias_reposo + ")";
                                FechadeAlta = Fecha_En_Palabra(FechaHoy.ToShortDateString());
                                drCertificado["FechaAlta"] = FechadeAlta;
                                FechaHoy = FechaHoy.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString()) - 1);
                                drCertificado["Dias_FinReposo"] = FechaHoy.ToString("dd") + " (" + Dia_En_Palabras(FechaHoy.ToString("dd")) + ")" + " de " + FechaHoy.ToString("MMMM") + " del " + FechaHoy.ToString("yyyy");
                                drCertificado["Num_Certificado"] = reporteDatos.Rows[0][9].ToString();
                                drCertificado["Empresa"] = reporteDatos.Rows[0][11].ToString();
                                drCertificado["Direccion_Empresa"] = reporteDatos.Rows[0][12].ToString();
                                drCertificado["Telefono_Empresa"] = reporteDatos.Rows[0][13].ToString();
                                drCertificado["emailEmpresa"] = empresa.EMP_EMAIL;

                                drCertificado["Nombre_Medico"] = medico.MED_APELLIDO_PATERNO + ' ' + medico.MED_APELLIDO_MATERNO + ' ' + medico.MED_NOMBRE1 + ' ' + medico.MED_NOMBRE2;
                                drCertificado["Email_Medico"] = medico.MED_EMAIL;
                                drCertificado["Identificacion_Medico"] = medico.MED_RUC;
                                drCertificado["telefonoMedico"] = medico.MED_TELEFONO_CASA;

                                drCertificado["Tipo"] = "consulta externa";
                                drCertificado["PathImagen"] = Certificado.path();
                                CM.Tables["EMERGENCIA"].Rows.Add(drCertificado);
                            }
                            His.Formulario.frmReportes reporte = new His.Formulario.frmReportes(1, "CertificadoEA", CM);
                            reporte.Show();
                        }
                    }
                }
                else
                    MessageBox.Show("Los certificados anulados no pueden imprimirse.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                
            }
            else
            {
                MessageBox.Show("Debe elegir un certificado para continuar.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public string Fecha_En_Palabra(string fecha)
        {
            string fechaprueba;
            CultureInfo cul = new CultureInfo("es");
            DateTime FI = Convert.ToDateTime(fecha,cul);
            if (Convert.ToInt32(FI.ToString("dd")) == 1)
            {
                fechaprueba = "1 (UNO) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("hh:mm",cul);
                return fechaprueba;
            }
            if (Convert.ToInt32(FI.ToString("dd")) == 2)
            {
                fechaprueba = "2 (DOS) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("hh:mm");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 3)
            {
                fechaprueba = "3 (TRES) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("hh:mm");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 4)
            {
                fechaprueba = "4 (CUATRO) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("hh:mm");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 5)
            {
                fechaprueba = "5 (CINCO) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("hh:mm");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 6)
            {
                fechaprueba = "6 (SEIS) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("hh:mm");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 7)
            {
                fechaprueba = "7 (SIETE) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("hh:mm");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 8)
            {
                fechaprueba = "8 (OCHO) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("hh:mm");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 9)
            {
                fechaprueba = "9 (NUEVE) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("hh:mm");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 10)
            {
                fechaprueba = "10 (DIEZ) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("hh:mm");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 11)
            {
                fechaprueba = "11 (ONCE) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("hh:mm");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 12)
            {
                fechaprueba = "12 (DOCE) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("hh:mm");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 13)
            {
                fechaprueba = "13 (TRECE) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("hh:mm");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 14)
            {
                fechaprueba = "14 (CATORCE) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("hh:mm");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 15)
            {
                fechaprueba = "15 (QUINCE) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("hh:mm");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 16)
            {
                fechaprueba = "16 (DIECISEIS) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("hh:mm");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 17)
            {
                fechaprueba = "17 (DIECISIETE) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("hh:mm");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 18)
            {
                fechaprueba = "18 (DIECIOCHO) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("hh:mm");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 19)
            {
                fechaprueba = "19 (DIECINUEVE) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("hh:mm");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 20)
            {
                fechaprueba = "20 (VEINTE) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("hh:mm");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 21)
            {
                fechaprueba = "21 (VEINTIUNO) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("hh:mm");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 22)
            {
                fechaprueba = "22 (VEINTIDOS) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("hh:mm");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 23)
            {
                fechaprueba = "23 (VEINTITRES) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("hh:mm");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 24)
            {
                fechaprueba = "24 (VEINTICUATRO) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("hh:mm");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 25)
            {
                fechaprueba = "25 (VEINTICINCO) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("hh:mm");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 26)
            {
                fechaprueba = "26 (VEINTISEIS) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("hh:mm");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 28)
            {
                fechaprueba = "28 (VEINTIOCHO) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("hh:mm");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 29)
            {
                fechaprueba = "29 (VEINTINUEVE) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("hh:mm");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 30)
            {
                fechaprueba = "30 (TREINTA) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("hh:mm");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 31)
            {
                fechaprueba = "31 (TREINTA Y UNO) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("hh:mm");
                return fechaprueba;
            }
            else
            {
                return "";
            }
        }

        public string Dia_En_Palabras(string dia)
        {
            string prueba;
            string num2Text = ""; int value = Convert.ToInt32(dia);
            if (value == 0) num2Text = "CERO";
            else if (value == 1) num2Text = "UN";
            else if (value == 2) num2Text = "DOS";
            else if (value == 3) num2Text = "TRES";
            else if (value == 4) num2Text = "CUATRO";
            else if (value == 5) num2Text = "CINCO";
            else if (value == 6) num2Text = "SEIS";
            else if (value == 7) num2Text = "SIETE";
            else if (value == 8) num2Text = "OCHO";
            else if (value == 9) num2Text = "NUEVE";
            else if (value == 10) num2Text = "DIEZ";
            else if (value == 11) num2Text = "ONCE";
            else if (value == 12) num2Text = "DOCE";
            else if (value == 13) num2Text = "TRECE";
            else if (value == 14) num2Text = "CATORCE";
            else if (value == 15) num2Text = "QUINCE";
            else if (value < 20) num2Text = "DIECI" + Dia_En_Palabras(Convert.ToString(value - 10));
            else if (value == 20) num2Text = "VEINTE";
            else if (value < 30) num2Text = "VEINTI" + Dia_En_Palabras(Convert.ToString(value - 20));
            else if (value == 30) num2Text = "TREINTA";
            else if (value < 40) num2Text = "TREINTA Y " + Dia_En_Palabras(Convert.ToString(value - 30));
            else if (value == 40) num2Text = "CUARENTA";
            else if (value < 50) num2Text = "CUARENTA Y " + Dia_En_Palabras(Convert.ToString(value - 40));
            else if (value == 50) num2Text = "CINCUENTA";
            else if (value == 60) num2Text = "SESENTA";
            else if (value == 70) num2Text = "SETENTA";
            else if (value == 80) num2Text = "OCHENTA";
            else if (value == 90) num2Text = "NOVENTA";
            return num2Text;

        }

        private void btnanular_Click(object sender, EventArgs e)
        {            
            if(MessageBox.Show("Para inhabilitar un certificado necesita la autorización de Dirección Médica","HIS3000",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Information)==DialogResult.Yes)
            {
                UltraGridRow fila = this.UltraGridCertificados.ActiveRow;
                frmAnulaCertificado frm = new frmAnulaCertificado(Convert.ToInt32(fila.Cells["NRO CERTIFICADO"].Value.ToString()), Convert.ToString(fila.Cells["MEDICO"].Value.ToString()));
                frm.ShowDialog();
                CargarCertificados();
            }
        }

        private void chkAnulados_CheckedChanged(object sender, EventArgs e)
        {
            CargarCertificados();
        }
    }
}
