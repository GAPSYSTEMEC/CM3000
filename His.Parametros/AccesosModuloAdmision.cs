using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Parametros
{
    public static class AccesosModuloAdmision
    {
        #region Variables

        private static bool exploradorPacientes = false;
        private static bool formulariosHCU = false;
        private static bool admision = false;
        private static bool mantenimiento = false;
        private static bool infMorbimortalidad = false;
        private static bool detalleAtencion = false;
        private static bool formulariosAtencion = false;
        private static bool agregarFormulariosAtencion = false;
        private static bool microfilmsAtencion = false;
        private static bool historiaAtencion = false;
        private static bool ingresarFormulario = false;
        private static bool editarFormulario = false;
        private static bool eliminarFormulario = false;
        private static bool ingresarPaciente = false;
        private static bool ingresarAtencion = false;
        private static bool editarPaciente = false;
        private static bool eliminarPaciente = false;
        private static bool admisionEmergencia = false;

        #endregion

        #region Metodos get y set

        public static bool ExploradorPacientes
        {
            get
            {
                return exploradorPacientes;
            }
            set
            {
                exploradorPacientes = value;
            }
        }

        public static bool FormulariosHCU
        {
            get
            {
                return formulariosHCU;
            }
            set
            {
                formulariosHCU = value;
            }
        }

        public static bool Admision
        {
            get
            {
                return admision;
            }
            set
            {
                admision = value;
            }
        }

        public static bool Mantenimiento
        {
            get
            {
                return mantenimiento;
            }
            set
            {
                mantenimiento = value;
            }
        }

        public static bool InfMorbimortalidad
        {
            get
            {
                return infMorbimortalidad;
            }
            set
            {
                infMorbimortalidad = value;
            }

        }

        public static bool DetalleAtencion
        {
            get
            {
                return detalleAtencion;
            }
            set
            {
                detalleAtencion = value;
            }

        }

        public static bool FormulariosAtencion
        {
            get
            {
                return formulariosAtencion;
            }
            set
            {
                formulariosAtencion = value;
            }

        }

        public static bool AgregarFormulariosAtencion
        {
            get
            {
                return agregarFormulariosAtencion;
            }
            set
            {
                agregarFormulariosAtencion = value;
            }

        }

        public static bool MicrofilmsAtencion
        {
            get
            {
                return microfilmsAtencion;
            }
            set
            {
                microfilmsAtencion = value;
            }
        }

        public static bool HistoriaAtencion
        {
            get
            {
                return historiaAtencion;
            }
            set
            {
                historiaAtencion = value;
            }
        }

        public static bool IngresarFormulario
        {
            get
            {
                return ingresarFormulario;
            }
            set
            {
                ingresarFormulario = value;
            }
        }

        public static bool EditarFormulario
        {
            get
            {
                return editarFormulario;
            }
            set
            {
                editarFormulario = value;
            }
        }

        public static bool EliminarFormulario
        {
            get
            {
                return eliminarFormulario;
            }
            set
            {
                eliminarFormulario = value;
            }
        }

        public static bool IngresarPaciente
        {
            get
            {
                return ingresarPaciente;
            }
            set
            {
                ingresarPaciente = value;
            }
        }

        public static bool IngresarAtencion
        {
            get
            {
                return ingresarAtencion;
            }
            set
            {
                ingresarAtencion = value;
            }
        }

        public static bool EditarPaciente
        {
            get
            {
                return editarPaciente;
            }
            set
            {
                editarPaciente = value;
            }
        }

        public static bool EliminarPaciente
        {
            get
            {
                return eliminarPaciente;
            }
            set
            {
                eliminarPaciente = value;
            }
        }

        public static bool AdmisionEmergencia
        {
            get { return admisionEmergencia; }
            set { admisionEmergencia = value; }
        }

        #endregion
    }
}
