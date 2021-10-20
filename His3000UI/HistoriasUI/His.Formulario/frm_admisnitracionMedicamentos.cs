using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using His.Entidades.Clases;
using His.Entidades;

namespace His.Formulario
{
    public partial class frm_admisnitracionMedicamentos : Form
    {
        int pocision;
        string Xatecodigo;
        string Xnumeroatencion;
        TextBox codMedicamento = new TextBox();
        TextBox cantidad = new TextBox();
        int PrimerIngreso = 0;
        private static ATENCIONES atencion = null;
        private static PACIENTES pcte = null;
        public frm_admisnitracionMedicamentos()
        {
            InitializeComponent();

        }
        public frm_admisnitracionMedicamentos(string numero_atencion)
        {
            InitializeComponent();
            ATENCIONES x = NegAtenciones.RecuperarAtencionPorNumero(numero_atencion);

            Xnumeroatencion = numero_atencion;
            Xatecodigo = x.ATE_CODIGO.ToString();
            atencion = NegAtenciones.AtencionID(Convert.ToInt32(Xatecodigo));
            pcte = NegPacientes.recuperarPacientePorAtencion(Convert.ToInt32(Xatecodigo));
            LlenaCombos();
            DataTable paciente = new DataTable();
            paciente = NegFormulariosHCU.Paciente(atencion.ATE_NUMERO_ATENCION);
            lblPaciente.Text = paciente.Rows[0][0].ToString();
            lblCodPaciente.Text = paciente.Rows[0][1].ToString();
            CargaDatos();
            PrimerIngreso = 1;

        }
        public void CargaDatos()
        {
            DataTable cargaGrid = new DataTable();
            bool admin = false;
            bool noadmin = false;
            cargaGrid = NegFormulariosHCU.RecuperaKardex(atencion.ATE_CODIGO.ToString());
            if (cargaGrid != null)
            {
                //CAMBIOS EDGAR 20210217 
                dtgCabeceraKardex.Rows.Clear();

                for (int i = 0; i < cargaGrid.Rows.Count; i++)
                {
                    if (cargaGrid.Rows[i][15].ToString() == "True")
                    {
                        admin = true;
                    }
                    else
                    {
                        admin = false;
                    }
                    if (cargaGrid.Rows[i][14].ToString() == "True")
                    {
                        noadmin = true;
                    }
                    else
                    {
                        noadmin = false;
                    }

                    dtgCabeceraKardex.Rows.Add(cargaGrid.Rows[i][3].ToString(), cargaGrid.Rows[i][4].ToString(),
                        cargaGrid.Rows[i][0].ToString(), cargaGrid.Rows[i][7].ToString(), cargaGrid.Rows[i][5].ToString(),
                        "", cargaGrid.Rows[i][6].ToString(), cargaGrid.Rows[i][9].ToString(),
                        cargaGrid.Rows[i][8].ToString(), admin, noadmin, cargaGrid.Rows[i][13].ToString(),
                        cargaGrid.Rows[i][0].ToString()); //Agrego uno a uno

                    //dtgCabeceraKardex.Rows.Add();
                    //dtgCabeceraKardex.Rows[i].Cells[0].Value = cargaGrid.Rows[i][3].ToString();
                    //dtgCabeceraKardex.Rows[i].Cells[1].Value = cargaGrid.Rows[i][4].ToString();
                    //dtgCabeceraKardex.Rows[i].Cells[3].Value = cargaGrid.Rows[i][7].ToString();
                    //dtgCabeceraKardex.Rows[i].Cells[4].Value = cargaGrid.Rows[i][5].ToString();
                    //dtgCabeceraKardex.Rows[i].Cells[6].Value = cargaGrid.Rows[i][6].ToString();
                    //dtgCabeceraKardex.Rows[i].Cells[7].Value = cargaGrid.Rows[i][9].ToString();
                    //dtgCabeceraKardex.Rows[i].Cells[8].Value = cargaGrid.Rows[i][8].ToString();
                    //if (cargaGrid.Rows[i][15].ToString() == "True")
                    //{
                    //    dtgCabeceraKardex.Rows[i].Cells[9].Value = true;
                    //    dtgCabeceraKardex.Rows[i].ReadOnly = true;
                    //    dtgCabeceraKardex.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                    //}
                    //if (cargaGrid.Rows[i][14].ToString() == "True")
                    //{
                    //    dtgCabeceraKardex.Rows[i].Cells[10].Value = true;
                    //    dtgCabeceraKardex.Rows[i].ReadOnly = true;
                    //    dtgCabeceraKardex.Rows[i].DefaultCellStyle.BackColor = Color.LightPink;
                    //}
                    //dtgCabeceraKardex.Rows[i].Cells[11].Value = cargaGrid.Rows[i][13].ToString();
                    //dtgCabeceraKardex.Rows[i].Cells[12].Value = cargaGrid.Rows[i][0].ToString();
                }


                //Cambios Edgar 20210217
                int valor = 0;
                foreach (DataGridViewRow item in dtgCabeceraKardex.Rows) //pintar las filas con check activos
                {
                    if (Convert.ToBoolean(item.Cells[9].Value) == true)
                    {
                        dtgCabeceraKardex.Rows[valor].ReadOnly = true;
                        dtgCabeceraKardex.Rows[valor].DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                    if (Convert.ToBoolean(item.Cells[10].Value) == true)
                    {
                        dtgCabeceraKardex.Rows[valor].ReadOnly = true;
                        dtgCabeceraKardex.Rows[valor].DefaultCellStyle.BackColor = Color.LightPink;
                    }
                    valor++;
                }
            }

        }

        public void LlenaCombos()
        {

            DataTable via = new DataTable();
            DataTable frecuencia = new DataTable();

            via = NegFormulariosHCU.LlenaCombos("VIA");
            frecuencia = NegFormulariosHCU.LlenaCombos("FRECUENCIA");
            cmbVia.DataSource = via;
            cmbVia.DisplayMember = "Detalle";
            cmbVia.ValueMember = "ID_VIA";

            cmbFrecuencia.DataSource = frecuencia;
            cmbFrecuencia.DisplayMember = "Detalle";
            cmbFrecuencia.ValueMember = "ID_FRECUENCIA";

            lblFecha.Text = Convert.ToString(DateTime.Now);
            lblHora.Text = Convert.ToString(DateTime.Now.Hour + ":" + DateTime.Now.Minute);
            lblUsuario.Text = Sesion.nomUsuario.ToString();

        }

        private void btnMedicamento_Click(object sender, EventArgs e)
        {
            int check = 0;
            if (checkBox1.Checked)
                check = 1;
            frm_AyudaKardex kardex = new frm_AyudaKardex(atencion.ATE_CODIGO.ToString(), 1, check);
            //kardex.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;


            kardex.ShowDialog();
            lblMedicamento.Text = kardex.medicamento;
            codMedicamento.Text = kardex.cue_codigo;
            cantidad.Text = kardex.cantidad;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                //lblMedicamento.ReadOnly = false;
                lblMedicamento.Text = "";
                //btnMedicamento.Enabled = false;
            }
            else
            {
                lblMedicamento.ReadOnly = true;
                lblMedicamento.Text = "";
                btnMedicamento.Enabled = true;
            }
        }

        public bool Valida()
        {
            if (lblMedicamento.Text != "")
            {
                if (txtDosisUnitaria.Text == "")
                {
                    MessageBox.Show("Ingrese la dosis del medicamento", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
                if (chbCantidadManual.Checked && txtCantidadManual.Text == "")
                {
                    MessageBox.Show("Ingrese el numero de cantidades que se va administrar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
                return true;
            }
            else
            {
                MessageBox.Show("Ingrese Medicamento", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            if (Valida())
            {
                bool ok = false;
                IngresaKardex objIngresa = new IngresaKardex();
                objIngresa.presentacion = lblMedicamento.Text;
                objIngresa.via = cmbVia.Text;
                objIngresa.frecuencia = cmbFrecuencia.Text;
                objIngresa.dosis = txtDosisUnitaria.Text;

                objIngresa.hora = Convert.ToDateTime(DateTime.Now.ToString("hh:mm"));
                objIngresa.fecha = (DateTime.Now);
                objIngresa.ate_codigo = atencion.ATE_NUMERO_ATENCION;
                if (checkBox2.Checked)
                    objIngresa.eventual = true;
                else
                    objIngresa.eventual = false;

                if (checkBox1.Checked)
                {
                    objIngresa.id_kardex = 0;
                    objIngresa.medPropio = true;
                }
                else
                {
                    objIngresa.id_kardex = Convert.ToInt64(codMedicamento.Text);//este es el codigo cue_cuenta para poder verificar que medicamento ya fue puesto en kardex                
                    objIngresa.medPropio = false;
                }
                int hora = 0;
                DateTime fecha = DateTime.Now.Date;
                DateTime xfecha = DateTime.Now.Date;
                int cant = 0;
                if (chbCantidadManual.Checked)
                {
                    cant = Convert.ToInt16(txtCantidadManual.Text);
                }
                else
                {
                    cant = Convert.ToInt16(cantidad.Text);
                }
                for (int i = 0; i < cant; i++)
                {
                    if (i == 0)
                    {
                        hora = DateTime.Now.Hour;
                    }
                    else
                    {
                        string frace = cmbFrecuencia.Text;
                        string[] spli = frace.Split(' ');
                        if (spli.Length > 1)
                        {
                            hora += Convert.ToInt16(spli[1]);
                        }
                        if (hora > 24)
                        {
                            hora -= 24;
                            fecha = DateTime.Today.AddDays(1);
                            //string mananatDate = manana.ToString("yyyy-MM-dd");
                            xfecha = xfecha.AddDays(1);

                        }
                    }
                    objIngresa.hora = Convert.ToDateTime(hora + ":00");
                    objIngresa.fecha = xfecha;
                    ok = NegFormulariosHCU.IngresaKardex(objIngresa, Sesion.codUsuario);
                    if (ok)
                    {

                        dtgCabeceraKardex.Rows.Add(new string[]{
                        Convert.ToString(codMedicamento.Text),
                        Convert.ToString(lblMedicamento.Text),
                        Convert.ToString(cmbVia.SelectedIndex+1),
                        Convert.ToString(cmbVia.Text),
                        Convert.ToString(txtDosisUnitaria.Text),
                        Convert.ToString(cmbFrecuencia.SelectedIndex+1),
                        Convert.ToString(cmbFrecuencia.Text),
                        Convert.ToString(fecha),
                        Convert.ToString(hora)
                        });
                    }
                    else
                    {
                        MessageBox.Show("Kardex No Se Actualizo", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                }
                lblCodMedicamento.Text = "";
                codMedicamento.Text = "";
                txtDosisUnitaria.Text = "";
                lblMedicamento.Text = "";
                PrimerIngreso = 0;
                CargaDatos();
                PrimerIngreso = 1;
                chbCantidadManual.Checked = false;
                txtCantidadManual.Text = "";
                txtCantidadManual.Visible = false;
                lblCantidad.Visible = false;
                MessageBox.Show("Kardex Actualizado Con Exito", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("Si Continua Perdera La Información Sin Cargar", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            this.Close();

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

            if (dtgCabeceraKardex.Rows.Count > 0)
            {
                DataTable xDatos = NegDietetica.getDataTable("KardexMedicamentos", Xatecodigo);


                DSkardexmedicamentos ds = new DSkardexmedicamentos();


                DataTable xFechas = NegDietetica.getDataTable("Form022_Fechas", Xatecodigo);
                DataTable xMedicamentos = NegDietetica.getDataTable("Form022_Medicamentos", Xatecodigo);

                foreach (DataRow imed in xMedicamentos.Rows)
                {
                    foreach (DataRow ifec in xFechas.Rows)
                    {
                        string[] values = new string[] {
                        ifec["FechaAdministración"].ToString(),
                        imed["Presentacion"].ToString(),
                        imed["dosis"].ToString(),
                        imed["via"].ToString(),
                        imed["Frecuencia"].ToString()
                        };

                        DataTable xDosis = NegDietetica.getDataTable("Form022_Registros", Xatecodigo, "0", values);

                        string xUDosis = "";
                        int count = 0;
                        foreach (DataRow item in xDosis.Rows)
                        {
                            Console.WriteLine(item["NoAdministrado"].ToString());
                            if (item["NoAdministrado"].ToString() == "True")
                            {
                                xUDosis += "(" + item["hora"].ToString().Substring(0, 2) + ") "
                                + item["nombres"].ToString().Substring(0, 1)
                                + item["apellidos"].ToString().Substring(0, 1) + " "
                                + item["DEP_NOMBRE"].ToString().Substring(0, 3) + " ["
                                + item["Observacion"].ToString().Trim() + "]"
                                + Environment.NewLine;
                            }
                            else
                            {
                                xUDosis += item["hora"].ToString().Substring(0, 2)
                                + "    " + item["nombres"].ToString().Substring(0, 1)
                                + item["apellidos"].ToString().Substring(0, 1) + "    "
                                + item["DEP_NOMBRE"].ToString().Substring(0, 3) + Environment.NewLine;
                            }

                            count++;
                        }
                        for (int i = count; i < 8; i++)
                        {
                            xUDosis += "." + Environment.NewLine;
                        }
                        NegCertificadoMedico neg = new NegCertificadoMedico();
                        DataRow dr = ds.Tables["dtKardexMED"].NewRow();
                        //dr["EMP_NOMBRE"] = row["EMP_NOMBRE"].ToString();
                        dr["PAC_NOMBRE1"] = pcte.PAC_NOMBRE1;
                        dr["PAC_NOMBRE2"] = pcte.PAC_NOMBRE2;
                        dr["PAC_APELLIDO_PATERNO"] = pcte.PAC_APELLIDO_PATERNO;
                        dr["PAC_APELLIDO_MATERNO"] = pcte.PAC_APELLIDO_MATERNO;
                        dr["PAC_GENERO"] = pcte.PAC_GENERO;
                        dr["PAC_HISTORIA_CLINICA"] = pcte.PAC_HISTORIA_CLINICA;
                        dr["MEDICAMENTO"] = imed["Presentacion"].ToString() + "," + imed["via"].ToString() + "," + imed["dosis"].ToString() + "," + imed["Frecuencia"].ToString();
                        dr["DIAyMES"] = ifec["FechaAdministración"].ToString().Substring(0, 5);
                        dr["EMP_NOMBRE"] = Sesion.nomEmpresa;
                        dr["Logo"] = neg.path();

                        dr["HORA"] = xUDosis;
                        //dr["INI"] = (row["NOMBRES"].ToString()).Substring(0,1) + (row["APELLIDOS"].ToString()).Substring(0, 1);
                        //dr["FUNCION"] = (row["DEP_NOMBRE"].ToString()).Substring(0, 3);
                        // string strdosis = string.Format("{1} {2} {3}", Environment.NewLine, row["Hora"].ToString(), (row["NOMBRES"].ToString()).Substring(0, 1) + (row["APELLIDOS"].ToString()).Substring(0, 1), (row["DEP_NOMBRE"].ToString()).Substring(0, 3));
                        //  dr["HORA"] = row["Hora"].ToString();

                        ds.Tables["dtKardexMED"].Rows.Add(dr);

                    }
                }
                His.Formulario.frmReportes myreport = new His.Formulario.frmReportes(1, "FORM022", ds);
                myreport.Show();
            }
            else
                MessageBox.Show("No hay datos para generar el reporte", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            
            ///********************************************///
            //frmReportes rep = new frmReportes();
            //rep.campo1 = ateCodigo;
            //rep.reporte = "KardexMedicamento";
            //rep.ShowDialog();
            //rep.Dispose();
        }

        private void frm_admisnitracionMedicamentos_Load(object sender, EventArgs e)
        {
            //foreach (DataGridViewColumn c in dtgCabeceraKardex.Columns)
            //{
            //    if (c.Name != "horaAdministracion")
            //        c.ReadOnly = true;
            //    //if (c.Name != "noAdministrado")
            //    //    c.ReadOnly = true;
            //    //if (c.Name != "Administrado")
            //    //    c.ReadOnly = true;
            //    //if (c.Name != "observacion")
            //    //    c.ReadOnly = true;
            //}
        }

        private void dtgCabeceraKardex_SelectionChanged(object sender, EventArgs e)
        {
            //dtgCabeceraKardex.CurrentCell = dtgCabeceraKardex.CurrentRow.Cells["horaAdministracion"];
            //dtgCabeceraKardex.BeginEdit(true);

        }

        private void dtgCabeceraKardex_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            if (dgv.CurrentCell.ColumnIndex == 11)
            {
                TextBox tb = ((TextBox)e.Control);
                tb.CharacterCasing = CharacterCasing.Upper;
            }
            if (dgv.CurrentCell.ColumnIndex == 8)
            {

                TextBox txt = e.Control as TextBox;
                if (txt != null)
                {
                    txt.KeyPress += new KeyPressEventHandler(dtgCabeceraKardex_KeyPress);
                }
            }
        }

        private void dtgCabeceraKardex_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            pocision = dtgCabeceraKardex.CurrentRow.Index;

            //dtgCabeceraKardex[1,pocision].
        }

        private void dtgCabeceraKardex_KeyPress(object sender, KeyPressEventArgs e)
        {

            //if (dtgCabeceraKardex.CurrentCell.ColumnIndex == 8)
            //{
            //    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            //    {
            //        e.Handled = true;
            //    }
            //}
        }

        private void dtgCabeceraKardex_CellValueChanged(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {

            dtgCabeceraKardex.CommitEdit(DataGridViewDataErrorContexts.Commit);

            if (PrimerIngreso == 1)
                try
                {
                    bool IsCheck = false;
                    bool IsCheck2 = false;
                    bool actualizado = false;
                    int hora;
                    #region Administrado

                    if (e.ColumnIndex == 9)
                    {

                        IsCheck = dtgCabeceraKardex.Rows.OfType<DataGridViewRow>().Any(x => Convert.ToBoolean(x.Cells["administrado"].Value));


                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dtgCabeceraKardex.CurrentRow.Cells["administrado"];
                        dtgCabeceraKardex.BeginEdit(true);
                        //if (chk.Value == null || (int)chk.Value == 0)
                        //{
                        //    chk.Value = 1;
                        //}
                        //else
                        //{
                        //    chk.Value = 0;
                        //}
                        //dtgCabeceraKardex.EndEdit();

                        if (IsCheck)
                        {
                            if (MessageBox.Show("Si Continua Se Almacenara el Medicamento como Administrado", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes && IsCheck)
                            {
                                dtgCabeceraKardex.Rows[pocision].ReadOnly = true;
                                actualizado = NegFormulariosHCU.ActualizaMedicamento(Convert.ToDateTime(dtgCabeceraKardex.Rows[pocision].Cells[8].Value), IsCheck, "", Convert.ToInt64(dtgCabeceraKardex.Rows[pocision].Cells[12].Value));
                                if (actualizado)
                                    MessageBox.Show("Medicamento Actualizado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                else
                                {
                                    MessageBox.Show("Medicamento No Actualizado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    dtgCabeceraKardex.Rows[pocision].Cells[9].Value = false;
                                }
                            }
                            else
                            {
                                //dtgCabeceraKardex.Rows[pocision].Cells["administrado"].Value = false;
                                chk.Value = 0;
                                dtgCabeceraKardex.EndEdit();
                            }

                        }
                    }

                    #endregion




                    #region No Administrado


                    if (e.ColumnIndex == 10)
                    {

                        //IsCheck2 = dtgCabeceraKardex.Rows.OfType<DataGridViewRow>().Any(x => Convert.ToBoolean(x.Cells[10].Value));
                        IsCheck2 = Convert.ToBoolean(dtgCabeceraKardex.Rows[pocision].Cells[10].Value);

                        //Cambios Edgar 20210323
                        DataGridViewCheckBoxCell chk2 = (DataGridViewCheckBoxCell)dtgCabeceraKardex.CurrentRow.Cells["noAdministrado"];
                        dtgCabeceraKardex.BeginEdit(true);

                        if (IsCheck2)
                        {
                            if (dtgCabeceraKardex.Rows[pocision].Cells[11].Value != null && dtgCabeceraKardex.Rows[pocision].Cells[11].Value.ToString() != "")
                            {
                                if (MessageBox.Show("Si Continua Se Almacenara el Medicamento como No Administrado", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                {
                                    dtgCabeceraKardex.Rows[pocision].ReadOnly = true;
                                    actualizado = NegFormulariosHCU.ActualizaMedicamento(Convert.ToDateTime(dtgCabeceraKardex.Rows[pocision].Cells[8].Value), IsCheck2, dtgCabeceraKardex.Rows[pocision].Cells[11].Value.ToString(), Convert.ToInt64(dtgCabeceraKardex.Rows[pocision].Cells[12].Value));
                                    if (actualizado)
                                        MessageBox.Show("Medicamento Actualizado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    else
                                    {
                                        MessageBox.Show("Medicamento No Actualizado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        dtgCabeceraKardex.Rows[pocision].Cells[10].Value = false;
                                    }
                                }
                            }
                            else
                            {
                                chk2.Value = 0;
                                dtgCabeceraKardex.EndEdit();
                                //dtgCabeceraKardex.Rows[pocision].Cells[10].Value = false;
                                MessageBox.Show("Si Va Marcar Medicamento Como No Administrado Debe Incluir La Razon En El Campo Observación", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                        }

                    }
                    #endregion
                    //Cambios Edgar 20210217
                    int valor = 0;
                    foreach (DataGridViewRow item in dtgCabeceraKardex.Rows) //pintar las filas con check activos
                    {
                        if (Convert.ToBoolean(item.Cells[9].Value) == true)
                        {
                            dtgCabeceraKardex.Rows[valor].ReadOnly = true;
                            dtgCabeceraKardex.Rows[valor].DefaultCellStyle.BackColor = Color.LightGreen;
                        }
                        if (Convert.ToBoolean(item.Cells[10].Value) == true)
                        {
                            dtgCabeceraKardex.Rows[valor].ReadOnly = true;
                            dtgCabeceraKardex.Rows[valor].DefaultCellStyle.BackColor = Color.LightPink;
                        }
                        valor++;
                    }
                }

                catch (Exception ex)
                { Console.WriteLine(ex); }
        }

        private void dtgCabeceraKardex_KeyDown(object sender, KeyEventArgs e)
        {
            //if(dtgCabeceraKardex.SelectedRows.Count > 0)
            //{
            //    if(e.KeyCode == Keys.Delete)
            //    {
            //        string producto = dtgCabeceraKardex.CurrentRow.Cells[1].Value.ToString();
            //        int id_kardexmedicamento = Convert.ToInt32(dtgCabeceraKardex.CurrentRow.Cells[2].Value.ToString());
            //        if(MessageBox.Show("¿Está seguro de eliminar: " + producto + "?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation
            //            ) == DialogResult.Yes)
            //        {
            //            //Elimina el producto
            //            try
            //            {
            //                NegFormulariosHCU.EliminarProdKardexMed(id_kardexmedicamento);
            //                MessageBox.Show("El producto se ha eliminado correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                CargaDatos();
            //            }
            //            catch (Exception ex)
            //            {
            //                MessageBox.Show(ex.Message);
            //            }
            //        }
            //    }

        }

        private void dtgCabeceraKardex_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            dtgCabeceraKardex.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dtgCabeceraKardex_CellEndEdit(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            #region fecha

            if (e.ColumnIndex == 8)
            {
                try
                {
                    string xhora = dtgCabeceraKardex.CurrentRow.Cells[8].Value.ToString().Substring(0, 2) + ":" + dtgCabeceraKardex.Rows[pocision].Cells[8].Value.ToString().Substring(2, 2);
                    string xid = dtgCabeceraKardex.CurrentRow.Cells[12].Value.ToString();
                    string[] x = new string[]
                    {
                                xid,xhora
                    };
                    NegDietetica.setROW("HoraKardexMedicamentos", x, xid);


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            #endregion
        }

        private void chbCantidadManual_CheckedChanged(object sender, EventArgs e)
        {
            if (lblMedicamento.Text != "" && Convert.ToInt16(cantidad.Text) <= 1)
            {
                if (chbCantidadManual.Checked)
                {
                    lblCantidad.Visible = true;
                    txtCantidadManual.Visible = true;
                    txtCantidadManual.Focus();
                }
                else
                {
                    lblCantidad.Visible = false;
                    txtCantidadManual.Visible = false;
                    txtCantidadManual.Text = "";
                }
            }
            else
            {
                if (cantidad.Text != "")
                {
                    if (Convert.ToInt16(cantidad.Text) > 1)
                    {
                        MessageBox.Show("Producto no se puede fraccionar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                chbCantidadManual.Checked = false;
            }
        }

        private void txtCantidadManual_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }
    }
}
