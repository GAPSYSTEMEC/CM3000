#pragma checksum "..\..\login.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "92ECE3560F990ACF35BA010A950BAA935CC53F5BC68685F07D3FED9FB18774B5"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
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


namespace AccesoUI {
    
    
    /// <summary>
    /// login
    /// </summary>
    public partial class login : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 36 "..\..\login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid marco;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtUsuario;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAceptar;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSalir;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblUser;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblPwd;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox txtClave;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock lblEmpresa;
        
        #line default
        #line hidden
        
        
        #line 113 "..\..\login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image IconoPruebas;
        
        #line default
        #line hidden
        
        
        #line 114 "..\..\login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image cambioClave;
        
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
            System.Uri resourceLocater = new System.Uri("/AccesoUI;component/login.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\login.xaml"
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
            
            #line 5 "..\..\login.xaml"
            ((AccesoUI.login)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.marco = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.txtUsuario = ((System.Windows.Controls.TextBox)(target));
            
            #line 49 "..\..\login.xaml"
            this.txtUsuario.KeyDown += new System.Windows.Input.KeyEventHandler(this.txtUsuario_KeyDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnAceptar = ((System.Windows.Controls.Button)(target));
            
            #line 50 "..\..\login.xaml"
            this.btnAceptar.Click += new System.Windows.RoutedEventHandler(this.btnAceptar_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnSalir = ((System.Windows.Controls.Button)(target));
            
            #line 58 "..\..\login.xaml"
            this.btnSalir.Click += new System.Windows.RoutedEventHandler(this.btnSalir_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.lblUser = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.lblPwd = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.txtClave = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 77 "..\..\login.xaml"
            this.txtClave.KeyDown += new System.Windows.Input.KeyEventHandler(this.txtClave_KeyDown);
            
            #line default
            #line hidden
            return;
            case 9:
            this.lblEmpresa = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 10:
            this.IconoPruebas = ((System.Windows.Controls.Image)(target));
            return;
            case 11:
            this.cambioClave = ((System.Windows.Controls.Image)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

