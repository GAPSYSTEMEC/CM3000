﻿#pragma checksum "..\..\frmMoverPaciente.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "99C7295BD76D2E7E1CEC196533AD27B74E486CD6"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using Infragistics.Windows.Editors;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace His.HabitacionesUI {
    
    
    /// <summary>
    /// frmMoverPaciente
    /// </summary>
    public partial class frmMoverPaciente : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\frmMoverPaciente.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal His.HabitacionesUI.frmMoverPaciente Window;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\frmMoverPaciente.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid LayoutRoot;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\frmMoverPaciente.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Infragistics.Windows.Editors.XamComboEditor xamCboPiso;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\frmMoverPaciente.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Infragistics.Windows.Editors.XamComboEditor xamCboHabitaciones;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\frmMoverPaciente.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label blbPiso;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\frmMoverPaciente.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblNumHabitacion;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\frmMoverPaciente.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblObservaciones;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\frmMoverPaciente.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAceptar;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\frmMoverPaciente.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancelar;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\frmMoverPaciente.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtObservacion;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/His.HabitacionesUI;component/frmmoverpaciente.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\frmMoverPaciente.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Window = ((His.HabitacionesUI.frmMoverPaciente)(target));
            
            #line 8 "..\..\frmMoverPaciente.xaml"
            this.Window.Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.LayoutRoot = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.xamCboPiso = ((Infragistics.Windows.Editors.XamComboEditor)(target));
            
            #line 18 "..\..\frmMoverPaciente.xaml"
            this.xamCboPiso.SelectedItemChanged += new System.Windows.RoutedPropertyChangedEventHandler<object>(this.xamCboPiso_SelectedItemChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.xamCboHabitaciones = ((Infragistics.Windows.Editors.XamComboEditor)(target));
            return;
            case 5:
            this.blbPiso = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.lblNumHabitacion = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.lblObservaciones = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.btnAceptar = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\frmMoverPaciente.xaml"
            this.btnAceptar.Click += new System.Windows.RoutedEventHandler(this.btnAceptar_Click_1);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btnCancelar = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\frmMoverPaciente.xaml"
            this.btnCancelar.Click += new System.Windows.RoutedEventHandler(this.btnCancelar_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.txtObservacion = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

