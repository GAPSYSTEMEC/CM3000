﻿namespace His.Farmacia
{
    partial class frm_AyudaProductos
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
            Infragistics.Win.UltraWinScrollBar.ScrollBarLook scrollBarLook1 = new Infragistics.Win.UltraWinScrollBar.ScrollBarLook();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            this.panel = new Infragistics.Win.Misc.UltraPanel();
            this.grid = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.btnActualizar = new Infragistics.Win.Misc.UltraButton();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.cbCriterio = new System.Windows.Forms.ComboBox();
            this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
            this.cb_numFilas = new System.Windows.Forms.ComboBox();
            this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
            this.txt_busqNomCom = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraFormManager1 = new Infragistics.Win.UltraWinForm.UltraFormManager(this.components);
            this._frm_Ayudas_UltraFormManager_Dock_Area_Top = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._frm_Ayudas_UltraFormManager_Dock_Area_Bottom = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._frm_Ayudas_UltraFormManager_Dock_Area_Left = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._frm_Ayudas_UltraFormManager_Dock_Area_Right = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this.panel.ClientArea.SuspendLayout();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_busqNomCom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraFormManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            appearance1.BackColor = System.Drawing.Color.AliceBlue;
            appearance1.BackColor2 = System.Drawing.Color.GhostWhite;
            this.panel.Appearance = appearance1;
            // 
            // panel.ClientArea
            // 
            this.panel.ClientArea.Controls.Add(this.grid);
            this.panel.ClientArea.Controls.Add(this.btnActualizar);
            this.panel.ClientArea.Controls.Add(this.ultraGroupBox1);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(4, 23);
            this.panel.Name = "panel";
            scrollBarLook1.ViewStyle = Infragistics.Win.UltraWinScrollBar.ScrollBarViewStyle.Office2007;
            this.panel.ScrollBarLook = scrollBarLook1;
            this.panel.Size = new System.Drawing.Size(639, 301);
            this.panel.TabIndex = 0;
            // 
            // grid
            // 
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            appearance7.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.grid.DisplayLayout.Appearance = appearance7;
            this.grid.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.grid.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance4.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance4.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance4.BorderColor = System.Drawing.SystemColors.Window;
            this.grid.DisplayLayout.GroupByBox.Appearance = appearance4;
            appearance5.ForeColor = System.Drawing.SystemColors.GrayText;
            this.grid.DisplayLayout.GroupByBox.BandLabelAppearance = appearance5;
            this.grid.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance6.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance6.BackColor2 = System.Drawing.SystemColors.Control;
            appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance6.ForeColor = System.Drawing.SystemColors.GrayText;
            this.grid.DisplayLayout.GroupByBox.PromptAppearance = appearance6;
            this.grid.DisplayLayout.MaxColScrollRegions = 1;
            this.grid.DisplayLayout.MaxRowScrollRegions = 1;
            appearance15.BackColor = System.Drawing.SystemColors.Window;
            appearance15.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grid.DisplayLayout.Override.ActiveCellAppearance = appearance15;
            appearance10.BackColor = System.Drawing.SystemColors.Highlight;
            appearance10.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.grid.DisplayLayout.Override.ActiveRowAppearance = appearance10;
            this.grid.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.grid.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance9.BackColor = System.Drawing.SystemColors.Window;
            this.grid.DisplayLayout.Override.CardAreaAppearance = appearance9;
            appearance8.BorderColor = System.Drawing.Color.Silver;
            appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.grid.DisplayLayout.Override.CellAppearance = appearance8;
            this.grid.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            this.grid.DisplayLayout.Override.CellPadding = 0;
            appearance12.BackColor = System.Drawing.SystemColors.Control;
            appearance12.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance12.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance12.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance12.BorderColor = System.Drawing.SystemColors.Window;
            this.grid.DisplayLayout.Override.GroupByRowAppearance = appearance12;
            appearance14.TextHAlignAsString = "Left";
            this.grid.DisplayLayout.Override.HeaderAppearance = appearance14;
            this.grid.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.grid.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance13.BackColor = System.Drawing.SystemColors.Window;
            appearance13.BorderColor = System.Drawing.Color.Silver;
            this.grid.DisplayLayout.Override.RowAppearance = appearance13;
            this.grid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance11.BackColor = System.Drawing.SystemColors.ControlLight;
            this.grid.DisplayLayout.Override.TemplateAddRowAppearance = appearance11;
            this.grid.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grid.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grid.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.Horizontal;
            this.grid.Location = new System.Drawing.Point(3, 63);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(633, 235);
            this.grid.TabIndex = 2;
            this.grid.Text = "ultraGrid1";
            this.grid.DoubleClickRow += new Infragistics.Win.UltraWinGrid.DoubleClickRowEventHandler(this.ultraGrid1_DoubleClickRow);
            this.grid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ultraGrid1_KeyDown);
            this.grid.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.grid_InitializeLayout);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            appearance3.BackColor = System.Drawing.Color.DimGray;
            appearance3.BackColor2 = System.Drawing.Color.LightGray;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.btnActualizar.Appearance = appearance3;
            this.btnActualizar.Location = new System.Drawing.Point(598, 32);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(32, 28);
            this.btnActualizar.TabIndex = 3;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // ultraGroupBox1
            // 
            this.ultraGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ultraGroupBox1.Controls.Add(this.cbCriterio);
            this.ultraGroupBox1.Controls.Add(this.ultraLabel4);
            this.ultraGroupBox1.Controls.Add(this.cb_numFilas);
            this.ultraGroupBox1.Controls.Add(this.ultraLabel2);
            this.ultraGroupBox1.Controls.Add(this.txt_busqNomCom);
            this.ultraGroupBox1.Controls.Add(this.ultraLabel1);
            this.ultraGroupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            appearance2.FontData.BoldAsString = "True";
            this.ultraGroupBox1.HeaderAppearance = appearance2;
            this.ultraGroupBox1.Location = new System.Drawing.Point(12, 4);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(576, 56);
            this.ultraGroupBox1.TabIndex = 0;
            this.ultraGroupBox1.Text = "Búsqueda";
            // 
            // cbCriterio
            // 
            this.cbCriterio.FormattingEnabled = true;
            this.cbCriterio.Location = new System.Drawing.Point(84, 32);
            this.cbCriterio.Name = "cbCriterio";
            this.cbCriterio.Size = new System.Drawing.Size(140, 21);
            this.cbCriterio.TabIndex = 0;
            this.cbCriterio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cb_numFilas_KeyPress);
            this.cbCriterio.SelectedValueChanged += new System.EventHandler(this.cbCriterio_SelectedValueChanged);
            // 
            // ultraLabel4
            // 
            this.ultraLabel4.Location = new System.Drawing.Point(6, 18);
            this.ultraLabel4.Name = "ultraLabel4";
            this.ultraLabel4.Size = new System.Drawing.Size(46, 12);
            this.ultraLabel4.TabIndex = 7;
            this.ultraLabel4.Text = "N° Filas";
            // 
            // cb_numFilas
            // 
            this.cb_numFilas.FormattingEnabled = true;
            this.cb_numFilas.Location = new System.Drawing.Point(6, 32);
            this.cb_numFilas.Name = "cb_numFilas";
            this.cb_numFilas.Size = new System.Drawing.Size(72, 21);
            this.cb_numFilas.TabIndex = 4;
            this.cb_numFilas.TabStop = false;
            this.cb_numFilas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cb_numFilas_KeyPress);
            this.cb_numFilas.SelectedValueChanged += new System.EventHandler(this.cb_numFilas_SelectedValueChanged);
            // 
            // ultraLabel2
            // 
            this.ultraLabel2.Location = new System.Drawing.Point(230, 18);
            this.ultraLabel2.Name = "ultraLabel2";
            this.ultraLabel2.Size = new System.Drawing.Size(104, 12);
            this.ultraLabel2.TabIndex = 3;
            this.ultraLabel2.Text = "Buscar";
            // 
            // txt_busqNomCom
            // 
            this.txt_busqNomCom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_busqNomCom.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_busqNomCom.Location = new System.Drawing.Point(230, 32);
            this.txt_busqNomCom.Name = "txt_busqNomCom";
            this.txt_busqNomCom.Size = new System.Drawing.Size(340, 21);
            this.txt_busqNomCom.TabIndex = 1;
            this.txt_busqNomCom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_busqNom_KeyPress);
            // 
            // ultraLabel1
            // 
            this.ultraLabel1.Location = new System.Drawing.Point(86, 18);
            this.ultraLabel1.Name = "ultraLabel1";
            this.ultraLabel1.Size = new System.Drawing.Size(90, 12);
            this.ultraLabel1.TabIndex = 1;
            this.ultraLabel1.Text = "Criterio";
            // 
            // ultraFormManager1
            // 
            this.ultraFormManager1.Form = this;
            this.ultraFormManager1.FormStyleSettings.Style = Infragistics.Win.UltraWinForm.UltraFormStyle.Office2010;
            // 
            // _frm_Ayudas_UltraFormManager_Dock_Area_Top
            // 
            this._frm_Ayudas_UltraFormManager_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frm_Ayudas_UltraFormManager_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._frm_Ayudas_UltraFormManager_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Top;
            this._frm_Ayudas_UltraFormManager_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frm_Ayudas_UltraFormManager_Dock_Area_Top.FormManager = this.ultraFormManager1;
            this._frm_Ayudas_UltraFormManager_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
            this._frm_Ayudas_UltraFormManager_Dock_Area_Top.Name = "_frm_Ayudas_UltraFormManager_Dock_Area_Top";
            this._frm_Ayudas_UltraFormManager_Dock_Area_Top.Size = new System.Drawing.Size(647, 23);
            // 
            // _frm_Ayudas_UltraFormManager_Dock_Area_Bottom
            // 
            this._frm_Ayudas_UltraFormManager_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frm_Ayudas_UltraFormManager_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._frm_Ayudas_UltraFormManager_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Bottom;
            this._frm_Ayudas_UltraFormManager_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frm_Ayudas_UltraFormManager_Dock_Area_Bottom.FormManager = this.ultraFormManager1;
            this._frm_Ayudas_UltraFormManager_Dock_Area_Bottom.InitialResizeAreaExtent = 4;
            this._frm_Ayudas_UltraFormManager_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 324);
            this._frm_Ayudas_UltraFormManager_Dock_Area_Bottom.Name = "_frm_Ayudas_UltraFormManager_Dock_Area_Bottom";
            this._frm_Ayudas_UltraFormManager_Dock_Area_Bottom.Size = new System.Drawing.Size(647, 4);
            // 
            // _frm_Ayudas_UltraFormManager_Dock_Area_Left
            // 
            this._frm_Ayudas_UltraFormManager_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frm_Ayudas_UltraFormManager_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._frm_Ayudas_UltraFormManager_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Left;
            this._frm_Ayudas_UltraFormManager_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frm_Ayudas_UltraFormManager_Dock_Area_Left.FormManager = this.ultraFormManager1;
            this._frm_Ayudas_UltraFormManager_Dock_Area_Left.InitialResizeAreaExtent = 4;
            this._frm_Ayudas_UltraFormManager_Dock_Area_Left.Location = new System.Drawing.Point(0, 23);
            this._frm_Ayudas_UltraFormManager_Dock_Area_Left.Name = "_frm_Ayudas_UltraFormManager_Dock_Area_Left";
            this._frm_Ayudas_UltraFormManager_Dock_Area_Left.Size = new System.Drawing.Size(4, 301);
            // 
            // _frm_Ayudas_UltraFormManager_Dock_Area_Right
            // 
            this._frm_Ayudas_UltraFormManager_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frm_Ayudas_UltraFormManager_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._frm_Ayudas_UltraFormManager_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Right;
            this._frm_Ayudas_UltraFormManager_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frm_Ayudas_UltraFormManager_Dock_Area_Right.FormManager = this.ultraFormManager1;
            this._frm_Ayudas_UltraFormManager_Dock_Area_Right.InitialResizeAreaExtent = 4;
            this._frm_Ayudas_UltraFormManager_Dock_Area_Right.Location = new System.Drawing.Point(643, 23);
            this._frm_Ayudas_UltraFormManager_Dock_Area_Right.Name = "_frm_Ayudas_UltraFormManager_Dock_Area_Right";
            this._frm_Ayudas_UltraFormManager_Dock_Area_Right.Size = new System.Drawing.Size(4, 301);
            // 
            // frm_AyudaProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 328);
            this.Controls.Add(this.panel);
            this.Controls.Add(this._frm_Ayudas_UltraFormManager_Dock_Area_Left);
            this.Controls.Add(this._frm_Ayudas_UltraFormManager_Dock_Area_Right);
            this.Controls.Add(this._frm_Ayudas_UltraFormManager_Dock_Area_Top);
            this.Controls.Add(this._frm_Ayudas_UltraFormManager_Dock_Area_Bottom);
            this.Name = "frm_AyudaProductos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ayuda";
            this.Load += new System.EventHandler(this.frm_AyudaPacientes_Load);
            this.panel.ClientArea.ResumeLayout(false);
            this.panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            this.ultraGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_busqNomCom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraFormManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraPanel panel;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private Infragistics.Win.Misc.UltraLabel ultraLabel1;
        private Infragistics.Win.Misc.UltraLabel ultraLabel2;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txt_busqNomCom;
        private Infragistics.Win.Misc.UltraLabel ultraLabel4;
        private System.Windows.Forms.ComboBox cb_numFilas;
        private Infragistics.Win.UltraWinForm.UltraFormManager ultraFormManager1;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frm_Ayudas_UltraFormManager_Dock_Area_Left;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frm_Ayudas_UltraFormManager_Dock_Area_Right;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frm_Ayudas_UltraFormManager_Dock_Area_Top;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frm_Ayudas_UltraFormManager_Dock_Area_Bottom;
        private Infragistics.Win.Misc.UltraButton btnActualizar;
        private Infragistics.Win.UltraWinGrid.UltraGrid grid;
        private System.Windows.Forms.ComboBox cbCriterio;
    }
}