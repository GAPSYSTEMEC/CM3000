using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Parametros
{
    public class AccesosModuloHonorarios
    {
        #region Variables       

        //archivo
	        private static bool medicos=false;
	        private static bool especialidades=false;
	        private static bool tipo_documento=false;
	        private static bool comision_clinica_referido=false;
	        private static bool tipo_retencion=false;
	        private static bool tipo_honorario=false;

        //procesos_diarios
	        private static bool ingreso_facturas=false;
	        private static bool honorarios_por_medico=false;

        private static bool explorador = false;

        //notas_retenciones
	        private static bool emision_retenciones=false;

            //notas
		        private static bool notas_credito=false;
		        private static bool notas_debito=false;
		        private static bool notasd_valores_no_cubiertos=false;
		        private static bool notasd_comisiones=false;

        //private static bool correos
	        private static bool nuevoCorreo=false;
	        private static bool opcionesCorreo=false;

        //reportes
	        private static bool r_medicos=false;
	        private static bool r_notas=false;
	        private static bool r_retenciones=false;
	        private static bool r_contables=false;

        //balances
	        private static bool honorarios_pend_pago=false;
	        private static bool honorarios_pend_cancelar=false;
	        private static bool honorarios_cancelados=false;
	        private static bool balance_gerencial=false;

        ////estadisticas
        //    private static bool honorarios_por_medico=false;
        //    private static bool honorarios_por_especialidad=false;

        ////transferencias
        //    private static bool a_contabilidad=false;
        //    private static bool migracion_sistema_actual=false;
        //    private static bool t_medicos=false;
        //    private static bool t_pacientes=false;
        //    private static bool actualizacion_tarifarios = false;
        

        #endregion

        #region Metodos get y set

        public static bool Medicos
        {
            get
            {
                return medicos;
            }
            set
            {
                medicos= value;
            }
        }

        public static bool Especialidades
        {
            get
            {
                return especialidades;
            }
            set
            {
                especialidades= value;
            }
        }

        public static bool Tipo_documento
        {
            get
            {
                return tipo_documento;
            }
            set
            {
                tipo_documento = value;
            }
        }

        public static bool ComisionesClinicaReferido
        {
            get
            {
                return comision_clinica_referido;
            }
            set
            {
                comision_clinica_referido = value;
            }
        }

        public static bool TipoRetencion
        {
            get
            {
                return tipo_retencion;
            }
            set
            {
                tipo_retencion = value;
            }

        }

        public static bool TipoHonorario
        {
            get
            {
                return tipo_honorario;
            }
            set
            {
                tipo_honorario = value;
            }

        }

        public static bool IngresoFacturas
        {
            get
            {
                return ingreso_facturas;
            }
            set
            {
                ingreso_facturas = value;
            }

        }

        public static bool HonorariosPorMedico
        {
            get
            {
                return honorarios_por_medico;
            }
            set
            {
                honorarios_por_medico = value;
            }
        }

        public static bool Explorador
        {
            get
            {
                return explorador;
            }
            set
            {
                explorador = value;
            }
        }

        public static bool EmisionRetenciones
        {
            get
            {
                return emision_retenciones;
            }
            set
            {
                emision_retenciones = value;
            }
        }

        public static bool NotasCredito
        {
            get
            {
                return notas_credito;
            }
            set
            {
                notas_credito = value;
            }
        }

        public static bool NotasDebito
        {
            get
            {
                return notas_debito;
            }
            set
            {
                notas_debito = value;
            }
        }

        public static bool NotasValoresNoCubiertos
        {
            get
            {
                return notasd_valores_no_cubiertos;
            }
            set
            {
                notasd_valores_no_cubiertos = value;
            }
        }

        public static bool NotasComisiones
        {
            get
            {
                return notasd_comisiones;
            }
            set
            {
                notasd_comisiones = value;
            }
        }

        public static bool NuevoCorreo
        {
            get
            {
                return nuevoCorreo;
            }
            set
            {
                nuevoCorreo = value;
            }
        }

        public static bool OpcionesCorreo
        {
            get
            {
                return opcionesCorreo;
            }
            set
            {
                opcionesCorreo = value;
            }
        }

        public static bool ReporteMedicos
        {
            get
            {
                return r_medicos;
            }
            set
            {
                r_medicos = value;
            }
        }

        public static bool ReporteNotas
        {
            get
            {
                return r_notas;
            }
            set
            {
                r_notas = value;
            }
        }

        public static bool ReporteRetenciones
        {
            get
            {
                return r_retenciones;
            }
            set
            {
                r_retenciones = value;
            }
        }

        public static bool ReporteContable
        {
            get
            {
                return r_contables;
            }
            set
            {
                r_contables = value;
            }
        }

        public static bool HonorariosPendientesPago
        {
            get
            {
                return honorarios_pend_pago;
            }
            set
            {
                honorarios_pend_pago = value;
            }
        }

        public static bool HonorariosPendientesCancelar
        {
            get
            {
                return honorarios_pend_cancelar;
            }
            set
            {
                honorarios_pend_cancelar = value;
            }
        }

        public static bool HonorariosCancelados
        {
            get
            {
                return honorarios_cancelados;
            }
            set
            {
                honorarios_cancelados = value;
            }
        }

        public static bool BalanceGerencial
        {
            get
            {
                return balance_gerencial;
            }
            set
            {
                balance_gerencial = value;
            }
        }

        #endregion
    }
}
