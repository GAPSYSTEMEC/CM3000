
namespace His.ConsultaExterna
{
    partial class frm_CitaMedica
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ultraGroupBoxPaciente = new Infragistics.Win.Misc.UltraGroupBox();
            this.txtcedula = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.txtapellido1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lbledad = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TablaMotivos = new System.Windows.Forms.DataGridView();
            this.menu = new System.Windows.Forms.ToolStrip();
            this.toolStripNuevo = new System.Windows.Forms.ToolStripSplitButton();
            this.btnNuevo = new System.Windows.Forms.ToolStripMenuItem();
            this.btnActualizar = new System.Windows.Forms.ToolStripButton();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.btnCerrar = new System.Windows.Forms.ToolStripButton();
            this.txtapellido2 = new System.Windows.Forms.TextBox();
            this.txtnombre1 = new System.Windows.Forms.TextBox();
            this.txtnombre2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Errores = new System.Windows.Forms.ErrorProvider(this.components);
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sintoma = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.seleccionar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtcedular = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtemail = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbn_m = new System.Windows.Forms.RadioButton();
            this.rbn_h = new System.Windows.Forms.RadioButton();
            this.label9 = new System.Windows.Forms.Label();
            this.dtp_fecnac = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.rbPasaporte = new System.Windows.Forms.RadioButton();
            this.rbCedula = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBoxPaciente)).BeginInit();
            this.ultraGroupBoxPaciente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TablaMotivos)).BeginInit();
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Errores)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ultraGroupBoxPaciente
            // 
            this.ultraGroupBoxPaciente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ultraGroupBoxPaciente.CaptionAlignment = Infragistics.Win.Misc.GroupBoxCaptionAlignment.Center;
            this.ultraGroupBoxPaciente.Controls.Add(this.label11);
            this.ultraGroupBoxPaciente.Controls.Add(this.rbPasaporte);
            this.ultraGroupBoxPaciente.Controls.Add(this.rbCedula);
            this.ultraGroupBoxPaciente.Controls.Add(this.btnBuscar);
            this.ultraGroupBoxPaciente.Controls.Add(this.txtcedula);
            this.ultraGroupBoxPaciente.Controls.Add(this.label1);
            this.ultraGroupBoxPaciente.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraGroupBoxPaciente.ForeColor = System.Drawing.Color.DimGray;
            this.ultraGroupBoxPaciente.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOutsideBorder;
            this.ultraGroupBoxPaciente.Location = new System.Drawing.Point(0, 40);
            this.ultraGroupBoxPaciente.Name = "ultraGroupBoxPaciente";
            this.ultraGroupBoxPaciente.Size = new System.Drawing.Size(1282, 67);
            this.ultraGroupBoxPaciente.TabIndex = 45;
            this.ultraGroupBoxPaciente.Text = "BUSCAR PACIENTE";
            this.ultraGroupBoxPaciente.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // txtcedula
            // 
            this.txtcedula.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtcedula.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtcedula.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcedula.Location = new System.Drawing.Point(93, 30);
            this.txtcedula.MaxLength = 15;
            this.txtcedula.Name = "txtcedula";
            this.txtcedula.Size = new System.Drawing.Size(226, 29);
            this.txtcedula.TabIndex = 0;
            this.txtcedula.Tag = "false";
            this.txtcedula.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtcedula_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 24);
            this.label1.TabIndex = 65;
            this.label1.Text = "Cédula: ";
            // 
            // ultraGroupBox1
            // 
            this.ultraGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ultraGroupBox1.CaptionAlignment = Infragistics.Win.Misc.GroupBoxCaptionAlignment.Center;
            this.ultraGroupBox1.Controls.Add(this.label10);
            this.ultraGroupBox1.Controls.Add(this.dtp_fecnac);
            this.ultraGroupBox1.Controls.Add(this.label9);
            this.ultraGroupBox1.Controls.Add(this.groupBox2);
            this.ultraGroupBox1.Controls.Add(this.txtemail);
            this.ultraGroupBox1.Controls.Add(this.label8);
            this.ultraGroupBox1.Controls.Add(this.txtcedular);
            this.ultraGroupBox1.Controls.Add(this.label7);
            this.ultraGroupBox1.Controls.Add(this.txtDireccion);
            this.ultraGroupBox1.Controls.Add(this.label6);
            this.ultraGroupBox1.Controls.Add(this.label4);
            this.ultraGroupBox1.Controls.Add(this.txtnombre2);
            this.ultraGroupBox1.Controls.Add(this.txtnombre1);
            this.ultraGroupBox1.Controls.Add(this.txtapellido2);
            this.ultraGroupBox1.Controls.Add(this.TablaMotivos);
            this.ultraGroupBox1.Controls.Add(this.label5);
            this.ultraGroupBox1.Controls.Add(this.lbledad);
            this.ultraGroupBox1.Controls.Add(this.label3);
            this.ultraGroupBox1.Controls.Add(this.txtapellido1);
            this.ultraGroupBox1.Controls.Add(this.label2);
            this.ultraGroupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraGroupBox1.ForeColor = System.Drawing.Color.DimGray;
            this.ultraGroupBox1.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOutsideBorder;
            this.ultraGroupBox1.Location = new System.Drawing.Point(0, 126);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(1282, 285);
            this.ultraGroupBox1.TabIndex = 46;
            this.ultraGroupBox1.Text = "DATOS DEL PACIENTE";
            this.ultraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // txtapellido1
            // 
            this.txtapellido1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtapellido1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtapellido1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtapellido1.Location = new System.Drawing.Point(106, 34);
            this.txtapellido1.MaxLength = 100;
            this.txtapellido1.Name = "txtapellido1";
            this.txtapellido1.Size = new System.Drawing.Size(163, 29);
            this.txtapellido1.TabIndex = 0;
            this.txtapellido1.Tag = "false";
            this.txtapellido1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtapellido1_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 24);
            this.label2.TabIndex = 65;
            this.label2.Text = "Apellidos:";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(325, 31);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(97, 29);
            this.btnBuscar.TabIndex = 66;
            this.btnBuscar.Text = "BUSCAR";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(906, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 24);
            this.label3.TabIndex = 66;
            this.label3.Text = "Edad: ";
            // 
            // lbledad
            // 
            this.lbledad.AutoSize = true;
            this.lbledad.BackColor = System.Drawing.Color.Transparent;
            this.lbledad.ForeColor = System.Drawing.Color.Black;
            this.lbledad.Location = new System.Drawing.Point(977, 37);
            this.lbledad.Name = "lbledad";
            this.lbledad.Size = new System.Drawing.Size(0, 24);
            this.lbledad.TabIndex = 67;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 183);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(175, 24);
            this.label5.TabIndex = 68;
            this.label5.Text = "Motivo de Consulta:";
            // 
            // TablaMotivos
            // 
            this.TablaMotivos.AllowUserToAddRows = false;
            this.TablaMotivos.AllowUserToDeleteRows = false;
            this.TablaMotivos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.TablaMotivos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.TablaMotivos.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.TablaMotivos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TablaMotivos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.TablaMotivos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TablaMotivos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codigo,
            this.sintoma,
            this.seleccionar});
            this.TablaMotivos.EnableHeadersVisualStyles = false;
            this.TablaMotivos.Location = new System.Drawing.Point(6, 220);
            this.TablaMotivos.Name = "TablaMotivos";
            this.TablaMotivos.RowHeadersVisible = false;
            this.TablaMotivos.Size = new System.Drawing.Size(875, 59);
            this.TablaMotivos.TabIndex = 69;
            // 
            // menu
            // 
            this.menu.AutoSize = false;
            this.menu.BackColor = System.Drawing.Color.WhiteSmoke;
            this.menu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripNuevo,
            this.btnActualizar,
            this.btnGuardar,
            this.btnCancelar,
            this.btnCerrar});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(1282, 37);
            this.menu.TabIndex = 47;
            this.menu.Text = "menu";
            // 
            // toolStripNuevo
            // 
            this.toolStripNuevo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo});
            this.toolStripNuevo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripNuevo.ForeColor = System.Drawing.Color.Black;
            this.toolStripNuevo.Image = global::His.ConsultaExterna.Properties.Resources.HIS_NUEVO;
            this.toolStripNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripNuevo.Name = "toolStripNuevo";
            this.toolStripNuevo.Size = new System.Drawing.Size(87, 34);
            this.toolStripNuevo.Text = "Nuevo";
            this.toolStripNuevo.Visible = false;
            // 
            // btnNuevo
            // 
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(116, 22);
            this.btnNuevo.Text = "Paciente";
            // 
            // btnActualizar
            // 
            this.btnActualizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizar.ForeColor = System.Drawing.Color.Black;
            this.btnActualizar.Image = global::His.ConsultaExterna.Properties.Resources.HIS_EDITAR;
            this.btnActualizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(86, 34);
            this.btnActualizar.Text = "Modificar";
            this.btnActualizar.Visible = false;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.Black;
            this.btnGuardar.Image = global::His.ConsultaExterna.Properties.Resources.HIS_GUARDAR;
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(81, 34);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.Black;
            this.btnCancelar.Image = global::His.ConsultaExterna.Properties.Resources.HIS_CANCELAR;
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(85, 34);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.ForeColor = System.Drawing.Color.Black;
            this.btnCerrar.Image = global::His.ConsultaExterna.Properties.Resources.HIS_SALIR;
            this.btnCerrar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(63, 34);
            this.btnCerrar.Text = "Salir";
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // txtapellido2
            // 
            this.txtapellido2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtapellido2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtapellido2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtapellido2.Location = new System.Drawing.Point(275, 34);
            this.txtapellido2.MaxLength = 100;
            this.txtapellido2.Name = "txtapellido2";
            this.txtapellido2.Size = new System.Drawing.Size(158, 29);
            this.txtapellido2.TabIndex = 71;
            this.txtapellido2.Tag = "false";
            this.txtapellido2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtapellido2_KeyDown);
            // 
            // txtnombre1
            // 
            this.txtnombre1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtnombre1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtnombre1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtnombre1.Location = new System.Drawing.Point(554, 34);
            this.txtnombre1.MaxLength = 100;
            this.txtnombre1.Name = "txtnombre1";
            this.txtnombre1.Size = new System.Drawing.Size(163, 29);
            this.txtnombre1.TabIndex = 72;
            this.txtnombre1.Tag = "false";
            this.txtnombre1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtnombre1_KeyDown);
            // 
            // txtnombre2
            // 
            this.txtnombre2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtnombre2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtnombre2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtnombre2.Location = new System.Drawing.Point(723, 34);
            this.txtnombre2.MaxLength = 100;
            this.txtnombre2.Name = "txtnombre2";
            this.txtnombre2.Size = new System.Drawing.Size(158, 29);
            this.txtnombre2.TabIndex = 73;
            this.txtnombre2.Tag = "false";
            this.txtnombre2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtnombre2_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(455, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 24);
            this.label4.TabIndex = 74;
            this.label4.Text = "Nombres:";
            // 
            // Errores
            // 
            this.Errores.ContainerControl = this;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Codigo";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Visible = false;
            this.dataGridViewTextBoxColumn1.Width = 76;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Sintomas";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 74;
            // 
            // codigo
            // 
            this.codigo.HeaderText = "Codigo";
            this.codigo.Name = "codigo";
            this.codigo.ReadOnly = true;
            this.codigo.Visible = false;
            this.codigo.Width = 76;
            // 
            // sintoma
            // 
            this.sintoma.HeaderText = "Sintomas";
            this.sintoma.Name = "sintoma";
            this.sintoma.ReadOnly = true;
            this.sintoma.Width = 111;
            // 
            // seleccionar
            // 
            this.seleccionar.HeaderText = "Seleccionar";
            this.seleccionar.Name = "seleccionar";
            this.seleccionar.Width = 115;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 89);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 24);
            this.label6.TabIndex = 75;
            this.label6.Text = "Dirección: ";
            // 
            // txtDireccion
            // 
            this.txtDireccion.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtDireccion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDireccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDireccion.Location = new System.Drawing.Point(112, 86);
            this.txtDireccion.MaxLength = 1000;
            this.txtDireccion.Multiline = true;
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(321, 91);
            this.txtDireccion.TabIndex = 76;
            this.txtDireccion.Tag = "false";
            this.txtDireccion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDireccion_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(455, 89);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 24);
            this.label7.TabIndex = 77;
            this.label7.Text = "Célular: ";
            // 
            // txtcedular
            // 
            this.txtcedular.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtcedular.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtcedular.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcedular.Location = new System.Drawing.Point(540, 86);
            this.txtcedular.MaxLength = 10;
            this.txtcedular.Name = "txtcedular";
            this.txtcedular.Size = new System.Drawing.Size(163, 29);
            this.txtcedular.TabIndex = 78;
            this.txtcedular.Tag = "false";
            this.txtcedular.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtcedular_KeyDown);
            this.txtcedular.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtcedular_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(455, 142);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 24);
            this.label8.TabIndex = 79;
            this.label8.Text = "Email:";
            // 
            // txtemail
            // 
            this.txtemail.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtemail.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtemail.Location = new System.Drawing.Point(523, 139);
            this.txtemail.MaxLength = 100;
            this.txtemail.Name = "txtemail";
            this.txtemail.Size = new System.Drawing.Size(358, 29);
            this.txtemail.TabIndex = 80;
            this.txtemail.Tag = "false";
            this.txtemail.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtemail_KeyDown);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.rbn_m);
            this.groupBox2.Controls.Add(this.rbn_h);
            this.groupBox2.Location = new System.Drawing.Point(808, 81);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(199, 42);
            this.groupBox2.TabIndex = 81;
            this.groupBox2.TabStop = false;
            // 
            // rbn_m
            // 
            this.rbn_m.AutoSize = true;
            this.rbn_m.ForeColor = System.Drawing.Color.Black;
            this.rbn_m.Location = new System.Drawing.Point(115, 8);
            this.rbn_m.Name = "rbn_m";
            this.rbn_m.Size = new System.Drawing.Size(76, 28);
            this.rbn_m.TabIndex = 17;
            this.rbn_m.Text = "Mujer";
            this.rbn_m.UseVisualStyleBackColor = true;
            this.rbn_m.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rbn_m_KeyDown);
            // 
            // rbn_h
            // 
            this.rbn_h.AutoSize = true;
            this.rbn_h.Checked = true;
            this.rbn_h.ForeColor = System.Drawing.Color.Black;
            this.rbn_h.Location = new System.Drawing.Point(6, 8);
            this.rbn_h.Name = "rbn_h";
            this.rbn_h.Size = new System.Drawing.Size(97, 28);
            this.rbn_h.TabIndex = 16;
            this.rbn_h.TabStop = true;
            this.rbn_h.Text = "Hombre";
            this.rbn_h.UseVisualStyleBackColor = true;
            this.rbn_h.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rbn_h_KeyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(723, 89);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 24);
            this.label9.TabIndex = 82;
            this.label9.Text = "Genero:";
            // 
            // dtp_fecnac
            // 
            this.dtp_fecnac.Checked = false;
            this.dtp_fecnac.CustomFormat = "yyyy/MM/dd";
            this.dtp_fecnac.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_fecnac.Location = new System.Drawing.Point(1044, 139);
            this.dtp_fecnac.Name = "dtp_fecnac";
            this.dtp_fecnac.Size = new System.Drawing.Size(139, 29);
            this.dtp_fecnac.TabIndex = 83;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(906, 142);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(132, 24);
            this.label10.TabIndex = 84;
            this.label10.Text = "F. Nacimiento:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Location = new System.Drawing.Point(490, 33);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(58, 24);
            this.label11.TabIndex = 301;
            this.label11.Text = "Tipo :";
            // 
            // rbPasaporte
            // 
            this.rbPasaporte.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.rbPasaporte.AutoSize = true;
            this.rbPasaporte.BackColor = System.Drawing.Color.Transparent;
            this.rbPasaporte.Location = new System.Drawing.Point(695, 32);
            this.rbPasaporte.Name = "rbPasaporte";
            this.rbPasaporte.Size = new System.Drawing.Size(112, 28);
            this.rbPasaporte.TabIndex = 300;
            this.rbPasaporte.TabStop = true;
            this.rbPasaporte.Text = "Pasaporte";
            this.rbPasaporte.UseVisualStyleBackColor = false;
            // 
            // rbCedula
            // 
            this.rbCedula.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.rbCedula.AutoSize = true;
            this.rbCedula.BackColor = System.Drawing.Color.Transparent;
            this.rbCedula.Checked = true;
            this.rbCedula.Location = new System.Drawing.Point(601, 32);
            this.rbCedula.Name = "rbCedula";
            this.rbCedula.Size = new System.Drawing.Size(88, 28);
            this.rbCedula.TabIndex = 299;
            this.rbCedula.TabStop = true;
            this.rbCedula.Text = "Cédula";
            this.rbCedula.UseVisualStyleBackColor = false;
            // 
            // frm_CitaMedica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1282, 423);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.ultraGroupBox1);
            this.Controls.Add(this.ultraGroupBoxPaciente);
            this.Name = "frm_CitaMedica";
            this.Text = "Cita Médica";
            this.Load += new System.EventHandler(this.frm_CitaMedica_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBoxPaciente)).EndInit();
            this.ultraGroupBoxPaciente.ResumeLayout(false);
            this.ultraGroupBoxPaciente.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            this.ultraGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TablaMotivos)).EndInit();
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Errores)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBoxPaciente;
        private System.Windows.Forms.TextBox txtcedula;
        private System.Windows.Forms.Label label1;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private System.Windows.Forms.TextBox txtapellido1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DataGridView TablaMotivos;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbledad;
        private System.Windows.Forms.Label label3;
        protected System.Windows.Forms.ToolStrip menu;
        private System.Windows.Forms.ToolStripSplitButton toolStripNuevo;
        private System.Windows.Forms.ToolStripMenuItem btnNuevo;
        protected System.Windows.Forms.ToolStripButton btnActualizar;
        protected System.Windows.Forms.ToolStripButton btnGuardar;
        protected System.Windows.Forms.ToolStripButton btnCancelar;
        protected System.Windows.Forms.ToolStripButton btnCerrar;
        private System.Windows.Forms.TextBox txtnombre2;
        private System.Windows.Forms.TextBox txtnombre1;
        private System.Windows.Forms.TextBox txtapellido2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ErrorProvider Errores;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn sintoma;
        private System.Windows.Forms.DataGridViewCheckBoxColumn seleccionar;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtcedular;
        private System.Windows.Forms.TextBox txtemail;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbn_m;
        private System.Windows.Forms.RadioButton rbn_h;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtp_fecnac;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.RadioButton rbPasaporte;
        private System.Windows.Forms.RadioButton rbCedula;
    }
}