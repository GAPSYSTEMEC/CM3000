﻿#pragma checksum "..\..\frmListaFormularios.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8CD28FFDC63703E6FD207E0A507D4A747ADDED07"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using Infragistics.Windows.DataPresenter;
using Microsoft.Windows.Controls;
using Microsoft.Windows.Controls.Primitives;
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
    /// frmListaFormularios
    /// </summary>
    public partial class frmListaFormularios : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\frmListaFormularios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAdd;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\frmListaFormularios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancelar;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\frmListaFormularios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Infragistics.Windows.DataPresenter.XamDataGrid grid;
        
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
            System.Uri resourceLocater = new System.Uri("/His.HabitacionesUI;component/frmlistaformularios.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\frmListaFormularios.xaml"
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
            
            #line 5 "..\..\frmListaFormularios.xaml"
            ((His.HabitacionesUI.frmListaFormularios)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnAdd = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\frmListaFormularios.xaml"
            this.btnAdd.Click += new System.Windows.RoutedEventHandler(this.btnAdd_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnCancelar = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\frmListaFormularios.xaml"
            this.btnCancelar.Click += new System.Windows.RoutedEventHandler(this.btnCancelar_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.grid = ((Infragistics.Windows.DataPresenter.XamDataGrid)(target));
            
            #line 16 "..\..\frmListaFormularios.xaml"
            this.grid.CellUpdated += new System.EventHandler<Infragistics.Windows.DataPresenter.Events.CellUpdatedEventArgs>(this.grid_CellUpdated);
            
            #line default
            #line hidden
            
            #line 16 "..\..\frmListaFormularios.xaml"
            this.grid.SelectedItemsChanging += new System.EventHandler<Infragistics.Windows.DataPresenter.Events.SelectedItemsChangingEventArgs>(this.grid_SelectedItemsChanging);
            
            #line default
            #line hidden
            
            #line 16 "..\..\frmListaFormularios.xaml"
            this.grid.SelectedItemsChanged += new System.EventHandler<Infragistics.Windows.DataPresenter.Events.SelectedItemsChangedEventArgs>(this.grid_SelectedItemsChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

