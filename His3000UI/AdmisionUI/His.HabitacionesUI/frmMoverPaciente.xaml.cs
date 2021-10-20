using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FeserWard.Controls;
using His.Entidades;
using His.Negocio;
using His.Entidades.Clases;
using His.Parametros;

namespace His.HabitacionesUI
{
	/// <summary>
	/// Interaction logic for frmMoverPaciente.xaml
	/// </summary>
	public partial class frmMoverPaciente : Window
	{

        bool estado;
        HABITACIONES parHabitacion;
        ATENCIONES parAtencion;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
		public frmMoverPaciente(HABITACIONES habitacion,ATENCIONES atencion)
		{
			this.InitializeComponent();
            parAtencion = atencion;
            parHabitacion = habitacion;
            estado = false;
            
		}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cargarCombos();
        }
        private void cargarCombos()
        {
            try
            {
                xamCboPiso.ItemsSource =  NegHabitaciones.listaNivelesPiso();
                xamCboPiso.DisplayMemberPath = "NIV_NOMBRE";
                xamCboPiso.SelectedIndex = 0;
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);   
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void xamCboPiso_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (xamCboPiso.SelectedItem != null)
                {
                    NIVEL_PISO item = (NIVEL_PISO)xamCboPiso.SelectedItem;
                    List<HABITACIONES> listaHabitaciones = NegHabitaciones.listaHabitaciones(item.NIV_CODIGO,Parametros.AdmisionParametros.getEstadoHabitacionDisponible());
                    xamCboHabitaciones.ItemsSource = listaHabitaciones;
                    xamCboHabitaciones.DisplayMemberPath = "hab_Numero";
                    xamCboHabitaciones.SelectedIndex = 0;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);   
            }

        }

        public bool getEstado()
        {
            return estado;
        }

        private void btnAceptar_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultado = MessageBox.Show("Desea guardar los cambios", "Alerta", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultado == MessageBoxResult.Yes)
            {
                try
                {
                    HABITACIONES habitacionSelecionada = (HABITACIONES)xamCboHabitaciones.SelectedItem;
                    //recupero el detalle actual
                    HABITACIONES_DETALLE habitacionDetalleOld = NegHabitaciones.RecuperarDetalleHabitacion(parAtencion);
                    habitacionDetalleOld.HAD_FECHA_DISPONIBILIDAD = DateTime.Now;
                    habitacionDetalleOld.HAD_OBSERVACION = "(cambio habitación origen) " + txtObservacion.Text;
                    NegHabitaciones.ActualizarDetallehabitacion(habitacionDetalleOld);
                    //creo el nuevo detalle
                    HABITACIONES_DETALLE habitacionDetalle = new HABITACIONES_DETALLE();
                    habitacionDetalle.HAD_CODIGO = NegHabitaciones.RecuperaMaximoDetalleHabitacion() + 1;
                    habitacionDetalle.ATE_CODIGO = parAtencion.ATE_CODIGO;
                    habitacionDetalle.HABITACIONESReference.EntityKey = habitacionSelecionada.EntityKey;
                    habitacionDetalle.HAD_ESTADO = Convert.ToString(parHabitacion.HABITACIONES_ESTADO.HES_CODIGO);
                    habitacionDetalle.ID_USUARIO = Sesion.codUsuario;
                    habitacionDetalle.HAD_FECHA_INGRESO = DateTime.Now;
                    habitacionDetalle.HAD_OBSERVACION = "cambio habitación destino";
                    habitacionDetalle.HAD_REGISTRO_ANTERIOR = (short)habitacionDetalleOld.HAD_CODIGO;
                    NegHabitaciones.CrearHabitacionDetalle(habitacionDetalle);
                   //crear habitaciones detalle
                    HABITACIONES_HISTORIAL habitacionHistorial = new HABITACIONES_HISTORIAL();
                    habitacionHistorial.HAH_CODIGO = NegHabitacionesHistorial.RecuperaMaximoHabitacionHistorial();
                    habitacionHistorial.ATE_CODIGO = parAtencion.ATE_CODIGO;
                  
                    habitacionHistorial.ID_USUARIO = Entidades.Clases.Sesion.codUsuario;
                    habitacionHistorial.HAH_FECHA_INGRESO = DateTime.Now;
                    habitacionHistorial.HAD_OBSERVACION = "Se mueve de  habitacion";
                    habitacionHistorial.HAH_REGISTRO_ANTERIOR = (short)habitacionDetalleOld.HAD_CODIGO;
                    habitacionHistorial.HAH_ESTADO = Convert.ToInt16(parHabitacion.HABITACIONES_ESTADO.HES_CODIGO);
                  
                    
                    //actualizo la atencion
                    parAtencion.HABITACIONESReference.EntityKey = habitacionSelecionada.EntityKey;
                    NegAtenciones.EditarAtencionAdmision(parAtencion,1);
                    //actualizo el estado de la habitacion
                    parHabitacion.HABITACIONES_ESTADOReference.EntityKey = NegHabitaciones.RecuperarEstadoHabitacion(AdmisionParametros.getEstadoHabitacionDisponible()).EntityKey;
                    NegHabitaciones.CambiarEstadoHabitacion(parHabitacion);
                    habitacionSelecionada.HABITACIONES_ESTADOReference.EntityKey = NegHabitaciones.RecuperarEstadoHabitacion(AdmisionParametros.getEstadoHabitacionOcupado()).EntityKey;
                    habitacionHistorial.HAB_CODIGO = (habitacionSelecionada.hab_Codigo);
                    NegHabitaciones.CambiarEstadoHabitacion(habitacionSelecionada);
                    estado = true;
                    NegHabitacionesHistorial.CrearHabitacionHistorial(habitacionHistorial);
                    
                    this.Close();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);   
                }
            }
        }
	}
	
}