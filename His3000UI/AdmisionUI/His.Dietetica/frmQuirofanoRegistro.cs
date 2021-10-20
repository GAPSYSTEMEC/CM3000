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
using System.IO;

namespace His.Dietetica
{
    public partial class frmQuirofanoRegistro : Form
    {
        NegQuirofano Quirofano = new NegQuirofano();
        internal static string nom_paciente; //Variable que contiene el nombre del paciente.
        internal static string habitacion; //Variable que contiene la habitacion del paciente.
        internal static DateTime fecha_nacimiento; //Variable de contencion de fecha de nacimiento del paciente.
        internal static string cie_codigo; //Variable de contencion de codigo del procedimiento de la tabla cie10;
        internal static string cie_descripcion; //Variable de contencion del nombre del procedimiento, tabla cie10;
        internal static string cod_cirujano; //Variable que contiene el codigo del medico cirujano
        internal static string nom_cirujano; //Variable que contiene el nombre del medico cirujano
        internal static string cod_ayudante = ""; //Variable que contiene el codigo del medico(ayudante)
        internal static string nom_ayudante = ""; //Variable que contiene el nombre del medico(ayudante)
        internal static string cod_ayudantia = ""; //Variable que contiene el codigo del medico(ayudantia)
        internal static string nom_ayudantia = ""; //Variable que contiene el nombre del medico(ayudantia)
        internal static string cod_anestesiologo = ""; //Variable que contiene el codigo del anestesiologo
        internal static string nom_anestesiologo = ""; //Variable que contiene el nombredel anestesiologo
        internal static string cod_circulante = ""; //Variable que contiene el codigo del circulante
        internal static string nom_circulante; //Variable que contiene el nombre del circulante
        internal static string cod_instrumentista = ""; //Variable que contiene el codigo del instrumentista
        internal static string nom_instrumentista = ""; //Variable que contiene el nombre del instrumentista
        internal static string cod_patologo = ""; //Variable que contiene el codigo del patologo
        internal static string nom_patologo; //Vairiable que contiene el nombre del patologo
        private static bool valido; //Variable que permite el paso de guardar registro si este es verdadero, caso contrario no.
        private static string recuperacion; //variable que entrega en string el estado de recuperacion 1(si) y 0(no)
        private static string tip_ate; //Variable que contiene el estado de atencion 
        internal static string ate_codigo; //Variable que contiene el codigo de atencion del paciente
        internal static string pac_codigo; //Variable que contiene el codigo del paciente.
        private static string existe; //Valida si el usuario ya tiene registro, si es asi levanta la informacion, caso contrario debe ingresarla
        private bool Editar = false; //Modo Edidicion debe activarse unicamente cuando levante informacion
        public static string seguro, tratamiento, referido;
        public frmQuirofanoRegistro()
        {
            InitializeComponent();
        }

        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Esta Seguro de Salir sin Guardar?", "His3000", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
                == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void toolStripButtonEditar_Click(object sender, EventArgs e)
        {
            //Establecer lo que se va editar por el usuario
            toolStripButtonGuardar.Enabled = true;
            btnmodificar.Enabled = false;
            Desbloquear();
            Editar = true;
        }

        private void frmQuirofanoRegistro_Load(object sender, EventArgs e)
        {
            CargarPacienteExistente();
            if(Editar == true)
            {
                btnmodificar.Enabled = false;
            }
            else
            {
                btnmodificar.Enabled = true;
                toolStripButtonGuardar.Enabled = false;
            }
        }

        ATENCIONES ultimaAtencion = NegAtenciones.RecuperarAtencionID(Convert.ToInt64(ate_codigo));
        PACIENTES datosPaciente = NegPacientes.recuperarPacientePorAtencion(Convert.ToInt32(ate_codigo));
        public void CargarPacienteExistente()
        {
            existe = Quirofano.ExisteProcedimiento(ate_codigo, pac_codigo, cie_codigo);
            if (existe == "")
            {
                var now = DateTime.Now;
                var birthday = datosPaciente.PAC_FECHA_NACIMIENTO;
                var yearsOld = now - birthday;

                int years = Convert.ToInt32(((TimeSpan)(yearsOld)).TotalDays / 365.25);
                int months = Convert.ToInt32((((TimeSpan)(yearsOld)).TotalDays / 365.25) - years) * 12;
                Editar = true;
                DateTime Fecha = DateTime.Now;
                lblfecha.Text = Convert.ToString(Fecha.ToShortDateString());
                lblpaciente.Text = datosPaciente.PAC_APELLIDO_PATERNO + " " + datosPaciente.PAC_APELLIDO_MATERNO + " " + datosPaciente.PAC_NOMBRE1 + " " + datosPaciente.PAC_NOMBRE2;
                lblhabitacion.Text = habitacion;
                CargarHabitacionesQuirofano();
                lbledad.Text = years.ToString() + " años";
                lblprocedimiento.Text = cie_descripcion;
                txthorainicio.Text = Fecha.ToLongTimeString();
                CargarTipoAnestesia();
            }
            else
            {
                var now = DateTime.Now;
                var birthday = datosPaciente.PAC_FECHA_NACIMIENTO;
                var yearsOld = now - birthday;

                int years = Convert.ToInt32(((TimeSpan)(yearsOld)).TotalDays / 365.25);
                int months = Convert.ToInt32((((TimeSpan)(yearsOld)).TotalDays / 365.25) - years) * 12;
                //int months = (int)(((yearsOld.TotalDays / 365.25) - years) * 12);


                Editar = false;
                DataTable TablaRegistro = new DataTable();
                DateTime Fecha = DateTime.Now;
                bool recu;
                string ate;
                TablaRegistro = Quirofano.RegistroPaciente(ate_codigo, pac_codigo, cie_codigo);
                CargarHabitacionesQuirofano();
                CargarTipoAnestesia();
                Bloquear();
                foreach (DataRow item in TablaRegistro.Rows)
                {
                    cbHabQuirofano.SelectedValue = item[0].ToString();
                    txtcirujano.Text = Convert.ToString(item[1]);
                    cod_cirujano = item["CODCIRUJANO"].ToString();
                    txtayudante.Text = Convert.ToString(item[2]);
                    cod_ayudante = item["CODAYUDANTE"].ToString();
                    txtayudantia.Text = Convert.ToString(item[3]);
                    cod_ayudantia = item["CODAYUDANTIA"].ToString();
                    cbanestesia.SelectedValue = item[4].ToString();
                    recu = Convert.ToBoolean(item[5]);
                    if(recu == true)
                    {
                        ckbsi.Checked = true;
                        ckbno.Checked = false;
                    }
                    else
                    {
                        ckbno.Checked = true;
                        ckbsi.Checked = false;
                    }
                    txtanestesiologo.Text = Convert.ToString(item[6]);
                    cod_anestesiologo = item["CODANESTESIOLOGO"].ToString();
                    txthorainicio.Text = Convert.ToString(item[7]);
                    txtcirculante.Text = Convert.ToString(item[8]);
                    cod_circulante = item["CODCIRCULANTE"].ToString();
                    txtinstrumentista.Text = Convert.ToString(item[9]);
                    cod_instrumentista = item["CODINSTRUMENTISTA"].ToString();
                    txtpatologia.Text = Convert.ToString(item[10]);
                    cod_patologo = item["CODPATOLOGO"].ToString();
                    ate = Convert.ToString(item[11]);
                    if(ate == "PROGRAMADA")
                    {
                        ckbProgramada.Checked = true;
                    }
                    if(ate == "EMERGENCIA")
                    {
                        ckbEmergencia.Checked = true;
                    }
                    lblprocedimiento.Text = cie_descripcion;
                    lblpaciente.Text = datosPaciente.PAC_APELLIDO_PATERNO + " " + datosPaciente.PAC_APELLIDO_MATERNO + " " + datosPaciente.PAC_NOMBRE1 + " " + datosPaciente.PAC_NOMBRE2;
                    lblhabitacion.Text = habitacion;
                    lbledad.Text = years.ToString() + " años";
                    lblfecha.Text = item[13].ToString();
                }
            }
        }
        public void CargarTipoAnestesia()
        {
            cbanestesia.DataSource = Quirofano.MostrarAnestesia();
            cbanestesia.DisplayMember = "DESCRIPCION";
            cbanestesia.ValueMember = "CODIGO";
        }
        public void CargarHabitacionesQuirofano()
        {
            cbHabQuirofano.DataSource = Quirofano.QuirofanoHabitacion();
            cbHabQuirofano.DisplayMember = "HABITACION";
            cbHabQuirofano.ValueMember = "CODIGO";
        }

        private void btncirujano_Click(object sender, EventArgs e)
        {
            frmQuirofanoAyudaRegistro.medico = true;
            frmQuirofanoAyudaRegistro x = new frmQuirofanoAyudaRegistro();
            x.Show();
            x.FormClosed += X_FormClosed;
        }

        private void X_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(frmQuirofanoAyudaRegistro.medico == true)
            {
                frmQuirofanoAyudaRegistro.medico = false;
                txtcirujano.Text = nom_cirujano;
            }
            if(frmQuirofanoAyudaRegistro.ayudante == true)
            {
                frmQuirofanoAyudaRegistro.ayudante = false;
                txtayudante.Text = nom_ayudante;
            }
            if(frmQuirofanoAyudaRegistro.ayudantia == true)
            {
                frmQuirofanoAyudaRegistro.ayudantia = false;
                txtayudantia.Text = nom_ayudantia;
            }
            if(frmQuirofanoAyudaRegistro.anestesiologo == true)
            {
                frmQuirofanoAyudaRegistro.anestesiologo = false;
                txtanestesiologo.Text = nom_anestesiologo;
            }
            if(frmQuirofanoAyudaRegistro.circulante == true)
            {
                frmQuirofanoAyudaRegistro.circulante = false;
                txtcirculante.Text = nom_circulante;
            }
            if(frmQuirofanoAyudaRegistro.instrumentista == true)
            {
                frmQuirofanoAyudaRegistro.instrumentista = false;
                txtinstrumentista.Text = nom_instrumentista;
            }
            if(frmQuirofanoAyudaRegistro.patologo == true)
            {
                frmQuirofanoAyudaRegistro.patologo = false;
                txtpatologia.Text = nom_patologo;
            }
        }

        private void btnayudante_Click(object sender, EventArgs e)
        {
            frmQuirofanoAyudaRegistro.ayudante = true;
            frmQuirofanoAyudaRegistro x = new frmQuirofanoAyudaRegistro();
            x.Show();
            x.FormClosed += X_FormClosed;
        }

        private void btnayudantia_Click(object sender, EventArgs e)
        {
            frmQuirofanoAyudaRegistro.ayudantia = true;
            frmQuirofanoAyudaRegistro x = new frmQuirofanoAyudaRegistro();
            x.Show();
            x.FormClosed += X_FormClosed;
        }

        private void btnanestesiologo_Click(object sender, EventArgs e)
        {
            frmQuirofanoAyudaRegistro.anestesiologo = true;
            frmQuirofanoAyudaRegistro x = new frmQuirofanoAyudaRegistro();
            x.Show();
            x.FormClosed += X_FormClosed;
        }

        private void btncirculante_Click(object sender, EventArgs e)
        {
            frmQuirofanoAyudaRegistro.circulante = true;
            frmQuirofanoAyudaRegistro x = new frmQuirofanoAyudaRegistro();
            x.Show();
            x.FormClosed += X_FormClosed;
        }

        private void btninstrumentista_Click(object sender, EventArgs e)
        {
            frmQuirofanoAyudaRegistro.instrumentista = true;
            frmQuirofanoAyudaRegistro x = new frmQuirofanoAyudaRegistro();
            x.Show();
            x.FormClosed += X_FormClosed;
        }

        private void ckbProgramada_CheckedChanged(object sender, EventArgs e)
        {
            if(ckbProgramada.Checked == true)
            {
                ckbEmergencia.Enabled = false;
                tip_ate = "PROGRAMADA";
            }
            else
            {
                ckbEmergencia.Enabled = true;
            }
        }

        private void ckbEmergencia_CheckedChanged(object sender, EventArgs e)
        {
            if(ckbEmergencia.Checked == true)
            {
                ckbProgramada.Enabled = false;
                tip_ate = "EMERGENCIA";
            }
            else
            {
                ckbProgramada.Enabled = true;
            }
        }

        private void btnpatologia_Click(object sender, EventArgs e)
        {
            frmQuirofanoAyudaRegistro.patologo = true;
            frmQuirofanoAyudaRegistro x = new frmQuirofanoAyudaRegistro();
            x.Show();
            x.FormClosed += X_FormClosed;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(ckbsi.Checked == true)
            {
                ckbno.Enabled = false;
                recuperacion = "1";
            }
            else
            {
                ckbno.Enabled = true;
            }
        }

        private void ckbno_CheckedChanged(object sender, EventArgs e)
        {
            if(ckbno.Checked == true)
            {
                ckbsi.Enabled = false;
                recuperacion = "0";
            }
            else
            {
                ckbsi.Enabled = true;
            }
        }

        private void txthorafinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNumber(e, false);
        }
        private static void OnlyNumber(KeyPressEventArgs e, bool isdecimal)
        {
            String aceptados = null;
            if (!isdecimal)
            {
                aceptados = "0123456789:" + Convert.ToChar(8);
            }
            if (aceptados.Contains("" + e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txthorainicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNumber(e, false);
        }

        private void HoraFin_Leave(object sender, EventArgs e)
        {
            TimeSpan Duracion;
            DateTime horainicio = Convert.ToDateTime(txthorainicio.Text);
            DateTime horafin = Convert.ToDateTime(HoraFin.Value);
            Duracion = horafin.Subtract(horainicio).Duration();
            txtduracion.Text = Convert.ToString(Duracion);
        }

        private void HoraFin_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                TimeSpan Duracion;
                DateTime horainicio = Convert.ToDateTime(txthorainicio.Text);
                DateTime horafin = Convert.ToDateTime(HoraFin.Value);
                Duracion = horafin.Subtract(horainicio).Duration();
                txtduracion.Text = Convert.ToString(Duracion);
            }
        }

        private void toolStripButtonGuardar_Click(object sender, EventArgs e)
        {
            ValidarVacio();
            DateTime fecha = DateTime.Now;

            ATENCIONES atencionActual = new ATENCIONES();
            PACIENTES pacienteActual = new PACIENTES();

            atencionActual = NegAtenciones.RecuperarAtencionID(Convert.ToInt64(ate_codigo));
            pacienteActual = NegPacientes.recuperarPacientePorAtencion(Convert.ToInt32(ate_codigo));
            if (valido == true)
            {
                try
                {
                    if (cie_codigo != null)
                    {
                        Quirofano.PedidoPaciente(cie_codigo, datosPaciente.PAC_CODIGO.ToString(), ate_codigo, Convert.ToString(fecha));
                        Quirofano.AgregarRegistro(Convert.ToString(cbHabQuirofano.SelectedValue), cod_cirujano, cod_ayudante, cod_ayudantia, Convert.ToString(cbanestesia.SelectedValue),
                      recuperacion, cod_anestesiologo, txthorainicio.Text, HoraFin.Text, txtduracion.Text, cod_circulante, cod_instrumentista, cod_patologo, tip_ate, ate_codigo, datosPaciente.PAC_CODIGO.ToString(), cie_codigo);
                        this.Close();
                        MessageBox.Show("¡Los Datos ha sigo Agregados Correctamente!", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //UltraGridRow Fila = UltraGridPacientes.ActiveRow;

                        //Envio datos a otro formulario pedido paciente
                        frmQuirofanoPedidoPaciente.medico = atencionActual.MEDICOS.MED_APELLIDO_PATERNO + " " +
                        atencionActual.MEDICOS.MED_APELLIDO_MATERNO + " " + atencionActual.MEDICOS.MED_NOMBRE1 + " " + atencionActual.MEDICOS.MED_NOMBRE2;
                        frmQuirofanoPedidoPaciente.seguro = seguro;
                        frmQuirofanoPedidoPaciente.tipo = tratamiento;
                        frmQuirofanoPedidoPaciente.genero = pacienteActual.PAC_GENERO;
                        frmQuirofanoPedidoPaciente.referido = referido;
                        frmQuirofanoPedidoPaciente.nombrepaciente = lblpaciente.Text;
                        frmQuirofanoPedidoPaciente.ate_codigo = ate_codigo;
                        frmQuirofanoPedidoPaciente.pac_codigo = datosPaciente.PAC_CODIGO.ToString();

                        //Abrir el formulario
                        frmQuirofanoPedidoPaciente x = new frmQuirofanoPedidoPaciente();
                        x.Show();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("¡Algo Ocurrio al Agregar los Datos al Paciente!\r\nMás detalle: " +ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
        public void ValidarVacio()
        {
            valido = true;
            if (txtcirujano.Text == "")
            {
                errorProvider1.SetError(txtcirujano, "¡Campo Obligatorio!");
                valido = false;
            }
            //if (txtayudante.Text == "")
            //{
            //    errorProvider1.SetError(txtayudante, "¡Campo Obligatorio!");
            //    valido = false;
            //    return;
            //}
            //if (txtayudantia.Text == "")
            //{
            //    errorProvider1.SetError(txtayudantia, "¡Campo Obligatorio!");
            //    valido = false;
            //    return;
            //}
            if (txtanestesiologo.Text == "")
            {
                errorProvider1.SetError(txtanestesiologo, "¡Campo Obligatorio!");
                valido = false;
            }
            //if (txtcirculante.Text == "")
            //{
            //    errorProvider1.SetError(txtcirculante, "¡Campo Obligatorio!");
            //    valido = false;
            //    return;
            //}
            if (txtinstrumentista.Text == "")
            {
                errorProvider1.SetError(txtinstrumentista, "¡Campo Obligatorio!");
                valido = false;
            }
            //if (txtpatologia.Text == "")
            //{
            //    errorProvider1.SetError(txtpatologia, "¡Campo Obligatorio!");
            //    valido = false;
            //    return;
            //}
            if (ckbEmergencia.Checked == false && ckbProgramada.Checked == false)
            {
                errorProvider1.SetError(lblatencion, "¡Campo Obligatorio!");
                valido = false;
            }
            if (ckbsi.Checked == false && ckbno.Checked == false)
            {
                errorProvider1.SetError(lblrecuperacion, "¡Campo Obligatorio!");
                valido = false;
            }
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("¿Está seguro de eliminar este procedimiento? \r\nUna vez eliminado no podra recuperar la información." 
               + "\r\n¿Desea continuar..?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                NegQuirofano Quir = new NegQuirofano();
                string cerrado = Quir.ProcedimientoCerrado(ate_codigo, pac_codigo, cie_codigo);
                if(cerrado == "")
                {
                    NegQuirofano.EliminarRegistro(Convert.ToInt64(ate_codigo), Convert.ToInt32(cie_codigo));
                    MessageBox.Show("Datos eliminados correctamente", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se puede eliminar un registro que ya ha sido cerrado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        public void Bloquear()
        {
            btncirujano.Enabled = false;
            btnayudante.Enabled = false;
            btnayudantia.Enabled = false;
            cbHabQuirofano.Enabled = false;
            cbanestesia.Enabled = false;
            ckbsi.Enabled = false;
            ckbno.Enabled = false;
            btnanestesiologo.Enabled = false;
            btncirculante.Enabled = false;
            btninstrumentista.Enabled = false;
            btnpatologia.Enabled = false;
            ckbProgramada.Enabled = false;
            ckbEmergencia.Enabled = false;
        }
        public void Desbloquear()
        {
            btncirujano.Enabled = true;
            btnayudante.Enabled = true;
            btnayudantia.Enabled = true;
            cbHabQuirofano.Enabled = true;
            cbanestesia.Enabled = true;
            ckbsi.Enabled = true;
            ckbno.Enabled = true;
            btnanestesiologo.Enabled = true;
            btncirculante.Enabled = true;
            btninstrumentista.Enabled = true;
            btnpatologia.Enabled = true;
            ckbProgramada.Enabled = true;
            ckbEmergencia.Enabled = true;
        }
    }
}
