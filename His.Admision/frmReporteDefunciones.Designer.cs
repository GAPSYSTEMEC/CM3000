﻿namespace His.Admision
{
    partial class frmReporteDefunciones
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            this.ultraGridExcelExporter1 = new Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter(this.components);
            this.tools = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ugrdHistorial = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.dtpFiltroHasta = new System.Windows.Forms.DateTimePicker();
            this.lblHasta = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFiltroDesde = new System.Windows.Forms.DateTimePicker();
            this.toolStripButtonActualizar = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonBuscar = new System.Windows.Forms.ToolStripButton();
            this.btnRevertir = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSalir = new System.Windows.Forms.ToolStripButton();
            this.tools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ugrdHistorial)).BeginInit();
            this.SuspendLayout();
            // 
            // tools
            // 
            this.tools.AutoSize = false;
            this.tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonActualizar,
            this.toolStripButtonBuscar,
            this.btnRevertir,
            this.toolStripButton1,
            this.toolStripSeparator3,
            this.toolStripButtonSalir});
            this.tools.Location = new System.Drawing.Point(0, 0);
            this.tools.Name = "tools";
            this.tools.Size = new System.Drawing.Size(977, 45);
            this.tools.TabIndex = 14;
            this.tools.Text = "toolStrip1";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 45);
            // 
            // ugrdHistorial
            // 
            this.ugrdHistorial.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ugrdHistorial.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance1.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance1.BorderColor = System.Drawing.SystemColors.Window;
            this.ugrdHistorial.DisplayLayout.GroupByBox.Appearance = appearance1;
            appearance2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ugrdHistorial.DisplayLayout.GroupByBox.BandLabelAppearance = appearance2;
            this.ugrdHistorial.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance3.BackColor2 = System.Drawing.SystemColors.Control;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ugrdHistorial.DisplayLayout.GroupByBox.PromptAppearance = appearance3;
            this.ugrdHistorial.DisplayLayout.MaxColScrollRegions = 1;
            this.ugrdHistorial.DisplayLayout.MaxRowScrollRegions = 1;
            appearance4.BackColor = System.Drawing.SystemColors.Window;
            appearance4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ugrdHistorial.DisplayLayout.Override.ActiveCellAppearance = appearance4;
            appearance5.BackColor = System.Drawing.SystemColors.Highlight;
            appearance5.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.ugrdHistorial.DisplayLayout.Override.ActiveRowAppearance = appearance5;
            this.ugrdHistorial.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ugrdHistorial.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance6.BackColor = System.Drawing.SystemColors.Window;
            this.ugrdHistorial.DisplayLayout.Override.CardAreaAppearance = appearance6;
            appearance7.BorderColor = System.Drawing.Color.Silver;
            appearance7.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.ugrdHistorial.DisplayLayout.Override.CellAppearance = appearance7;
            this.ugrdHistorial.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.ugrdHistorial.DisplayLayout.Override.CellPadding = 0;
            appearance8.BackColor = System.Drawing.SystemColors.Control;
            appearance8.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance8.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance8.BorderColor = System.Drawing.SystemColors.Window;
            this.ugrdHistorial.DisplayLayout.Override.GroupByRowAppearance = appearance8;
            appearance9.TextHAlignAsString = "Left";
            this.ugrdHistorial.DisplayLayout.Override.HeaderAppearance = appearance9;
            this.ugrdHistorial.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.ugrdHistorial.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance10.BackColor = System.Drawing.SystemColors.Window;
            appearance10.BorderColor = System.Drawing.Color.Silver;
            this.ugrdHistorial.DisplayLayout.Override.RowAppearance = appearance10;
            this.ugrdHistorial.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance11.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ugrdHistorial.DisplayLayout.Override.TemplateAddRowAppearance = appearance11;
            this.ugrdHistorial.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ugrdHistorial.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ugrdHistorial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ugrdHistorial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ugrdHistorial.Location = new System.Drawing.Point(0, 45);
            this.ugrdHistorial.Name = "ugrdHistorial";
            this.ugrdHistorial.Size = new System.Drawing.Size(977, 223);
            this.ugrdHistorial.TabIndex = 264;
            this.ugrdHistorial.Text = "ultraGrid";
            this.ugrdHistorial.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ugrdHistorial_InitializeLayout);
            // 
            // dtpFiltroHasta
            // 
            this.dtpFiltroHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFiltroHasta.Location = new System.Drawing.Point(488, 18);
            this.dtpFiltroHasta.Name = "dtpFiltroHasta";
            this.dtpFiltroHasta.Size = new System.Drawing.Size(87, 20);
            this.dtpFiltroHasta.TabIndex = 266;
            // 
            // lblHasta
            // 
            this.lblHasta.AutoSize = true;
            this.lblHasta.Location = new System.Drawing.Point(444, 18);
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.Size = new System.Drawing.Size(38, 13);
            this.lblHasta.TabIndex = 268;
            this.lblHasta.Text = "Hasta:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(291, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 267;
            this.label1.Text = "Desde:";
            // 
            // dtpFiltroDesde
            // 
            this.dtpFiltroDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFiltroDesde.Location = new System.Drawing.Point(338, 19);
            this.dtpFiltroDesde.Name = "dtpFiltroDesde";
            this.dtpFiltroDesde.Size = new System.Drawing.Size(87, 20);
            this.dtpFiltroDesde.TabIndex = 265;
            this.dtpFiltroDesde.Value = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
            // 
            // toolStripButtonActualizar
            // 
            this.toolStripButtonActualizar.AutoSize = false;
            this.toolStripButtonActualizar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonActualizar.Image = global::His.Admision.Properties.Resources.HIS_REFRESH;
            this.toolStripButtonActualizar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonActualizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonActualizar.Name = "toolStripButtonActualizar";
            this.toolStripButtonActualizar.Size = new System.Drawing.Size(42, 42);
            this.toolStripButtonActualizar.Text = "toolStripButton1";
            this.toolStripButtonActualizar.ToolTipText = "Actualizar";
            this.toolStripButtonActualizar.Click += new System.EventHandler(this.toolStripButtonActualizar_Click);
            // 
            // toolStripButtonBuscar
            // 
            this.toolStripButtonBuscar.AutoSize = false;
            this.toolStripButtonBuscar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonBuscar.Image = global::His.Admision.Properties.Resources.HIS_EXPORT_TO_EXCEL;
            this.toolStripButtonBuscar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonBuscar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonBuscar.Name = "toolStripButtonBuscar";
            this.toolStripButtonBuscar.Size = new System.Drawing.Size(42, 42);
            this.toolStripButtonBuscar.Text = "Exportar";
            this.toolStripButtonBuscar.ToolTipText = "Exportar a excel";
            this.toolStripButtonBuscar.Click += new System.EventHandler(this.toolStripButtonBuscar_Click);
            // 
            // btnRevertir
            // 
            this.btnRevertir.AutoSize = false;
            this.btnRevertir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRevertir.Image = global::His.Admision.Properties.Resources.hospital_2_icon_icons_com_66067;
            this.btnRevertir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnRevertir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRevertir.Name = "btnRevertir";
            this.btnRevertir.Size = new System.Drawing.Size(42, 42);
            this.btnRevertir.Text = "Revertir ";
            this.btnRevertir.ToolTipText = "Revertir Estado";
            this.btnRevertir.Click += new System.EventHandler(this.btnRevertir_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.AutoSize = false;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::His.Admision.Properties.Resources.HIS_IMPRIMIR;
            this.toolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(42, 42);
            this.toolStripButton1.Text = "Exportar";
            this.toolStripButton1.ToolTipText = "Imprimir Reporte";
            this.toolStripButton1.Visible = false;
            // 
            // toolStripButtonSalir
            // 
            this.toolStripButtonSalir.AutoSize = false;
            this.toolStripButtonSalir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSalir.Image = global::His.Admision.Properties.Resources.HIS_SALIR;
            this.toolStripButtonSalir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSalir.Name = "toolStripButtonSalir";
            this.toolStripButtonSalir.Size = new System.Drawing.Size(42, 42);
            this.toolStripButtonSalir.Text = "toolStripButton1";
            this.toolStripButtonSalir.ToolTipText = "Salir";
            this.toolStripButtonSalir.Click += new System.EventHandler(this.toolStripButtonSalir_Click);
            // 
            // frmReporteDefunciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 268);
            this.Controls.Add(this.dtpFiltroHasta);
            this.Controls.Add(this.lblHasta);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpFiltroDesde);
            this.Controls.Add(this.ugrdHistorial);
            this.Controls.Add(this.tools);
            this.Name = "frmReporteDefunciones";
            this.ShowIcon = false;
            this.Text = "Reporte de defunciones";
            this.Load += new System.EventHandler(this.frmReporteDefunciones_Load);
            this.tools.ResumeLayout(false);
            this.tools.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ugrdHistorial)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter ultraGridExcelExporter1;
        private System.Windows.Forms.ToolStrip tools;
        private System.Windows.Forms.ToolStripButton toolStripButtonActualizar;
        private System.Windows.Forms.ToolStripButton toolStripButtonBuscar;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButtonSalir;
        private Infragistics.Win.UltraWinGrid.UltraGrid ugrdHistorial;
        private System.Windows.Forms.DateTimePicker dtpFiltroHasta;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFiltroDesde;
        private System.Windows.Forms.ToolStripButton btnRevertir;
    }
}