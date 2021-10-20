using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Parametros
{
    public class AccesosModuloTarifario
    {
        #region Variables

        private static bool consultaTipoEmpresa = false;
        private static bool consultaAseguradorasEmpresas = false;
        private static bool consultaConveniosProcedimientos = false;
        private static bool consultaTarifario = false;
        private static bool consultaProcedimientos = false;
        private static bool consultaCreacionHonorarios = false;
        private static bool consultaReportes = false;
        private static bool tipoEmpresaCRUD = false;
        private static bool aseguradorasEmpresasCRUD = false;
        private static bool conveniosCRUD = false;
        private static bool tarifariosCRUD = false;
        private static bool procedimientosCRUD = false;
        private static bool creacionHonorarios = false;
        private static bool consultaHonorariosRealizados = false;
        private static bool consultaEstructuraEspecialidades = false;
        private static bool estructuraEspecialidades = false;

        private static bool creacionTipoCatologo = false;     
        private static bool modificacionTipoCatologo = false;
        private static bool eliminacionTipoCatologo = false;

        private static bool creacionCatologo = false;
        private static bool modificacionCatologo = false;       
        private static bool eliminacionCatologo = false;

        private static bool creacionConvenio = false;       
        private static bool modificacionConvenio = false;       
        private static bool eliminacionConvenio = false;       

        private static bool creacionPrecioConvenio = false;      
        private static bool modificacionPrecioConvenio = false;       
        private static bool eliminacionPrecioConvenio = false;

       


       
     

        #endregion

        #region Metodos get y set
        public static bool CreacionConvenio
        {
            get { return AccesosModuloTarifario.creacionConvenio; }
            set { AccesosModuloTarifario.creacionConvenio = value; }
        }
        public static bool ModificacionConvenio
        {
            get { return AccesosModuloTarifario.modificacionConvenio; }
            set { AccesosModuloTarifario.modificacionConvenio = value; }
        }
        public static bool EliminacionConvenio
        {
            get { return AccesosModuloTarifario.eliminacionConvenio; }
            set { AccesosModuloTarifario.eliminacionConvenio = value; }
        }

        public static bool ModificacionPrecioConvenio
        {
            get { return AccesosModuloTarifario.modificacionPrecioConvenio; }
            set { AccesosModuloTarifario.modificacionPrecioConvenio = value; }
        }        
        public static bool CreacionPrecioConvenio
        {
            get { return AccesosModuloTarifario.creacionPrecioConvenio; }
            set { AccesosModuloTarifario.creacionPrecioConvenio = value; }
        }      
        public static bool EliminacionPrecioConvenio
        {
            get { return AccesosModuloTarifario.eliminacionPrecioConvenio; }
            set { AccesosModuloTarifario.eliminacionPrecioConvenio = value; }
        }

        public static bool EliminacionCatologo
        {
            get { return AccesosModuloTarifario.eliminacionCatologo; }
            set { AccesosModuloTarifario.eliminacionCatologo = value; }
        }
        public static bool ModificacionCatologo
        {
            get { return AccesosModuloTarifario.modificacionCatologo; }
            set { AccesosModuloTarifario.modificacionCatologo = value; }
        }
        public static bool CreacionCatologo
        {
            get { return AccesosModuloTarifario.creacionCatologo; }
            set { AccesosModuloTarifario.creacionCatologo = value; }
        }

        public static bool EliminacionTipoCatologo
        {
            get { return AccesosModuloTarifario.eliminacionTipoCatologo; }
            set { AccesosModuloTarifario.eliminacionTipoCatologo = value; }
        }
        public static bool CreacionTipoCatologo
        {
            get { return AccesosModuloTarifario.creacionTipoCatologo; }
            set { AccesosModuloTarifario.creacionTipoCatologo = value; }
        }
        public static bool ModificacionTipoCatologo
        {
            get { return AccesosModuloTarifario.modificacionTipoCatologo; }
            set { AccesosModuloTarifario.modificacionTipoCatologo = value; }
        }

        public static bool ConsultaTipoEmpresa
        {
            get
            {
                return consultaTipoEmpresa;
            }
            set
            {
                consultaTipoEmpresa = value;
            }
        }

        public static bool ConsultaAseguradorasEmpresas
        {
            get
            {
                return consultaAseguradorasEmpresas;
            }
            set
            {
                consultaAseguradorasEmpresas = value;
            }
        }

        public static bool ConsultaConveniosProcedimientos
        {
            get
            {
                return consultaConveniosProcedimientos;
            }
            set
            {
                consultaConveniosProcedimientos = value;
            }
        }

        public static bool ConsultaTarifario
        {
            get
            {
                return consultaTarifario;
            }
            set
            {
                consultaTarifario = value;
            }
        }

        public static bool ConsultaProcedimientos
        {
            get
            {
                return consultaProcedimientos;
            }
            set
            {
                consultaProcedimientos = value;
            }

        }

        public static bool ConsultaCreacionHonorarios
        {
            get
            {
                return consultaCreacionHonorarios;
            }
            set
            {
                consultaCreacionHonorarios = value;
            }

        }

        public static bool ConsultaReportes
        {
            get
            {
                return consultaReportes;
            }
            set
            {
                consultaReportes = value;
            }
        }

        public static bool TipoEmpresaCRUD
        {
            get
            {
                return tipoEmpresaCRUD;
            }
            set
            {
                tipoEmpresaCRUD = value;
            }

        }

        public static bool AseguradorasEmpresasCRUD
        {
            get
            {
                return aseguradorasEmpresasCRUD;
            }
            set
            {
                aseguradorasEmpresasCRUD = value;
            }
        }

        public static bool ConveniosCRUD
        {
            get
            {
                return conveniosCRUD;
            }
            set
            {
                conveniosCRUD = value;
            }
        }

        public static bool TarifariosCRUD
        {
            get
            {
                return tarifariosCRUD;
            }
            set
            {
                tarifariosCRUD = value;
            }
        }

        public static bool ProcedimientosCRUD
        {
            get
            {
                return procedimientosCRUD;
            }
            set
            {
                procedimientosCRUD = value;
            }
        }

        public static bool CreacionHonorarios
        {
            get
            {
                return creacionHonorarios;
            }
            set
            {
                creacionHonorarios = value;
            }
        }

        public static bool ConsultaHonorariosRealizados
        {
            get
            {
                return consultaHonorariosRealizados;
            }
            set
            {
                consultaHonorariosRealizados = value;
            }
        }
        public static bool ConsultaEstructuraEspecialidades
        {
            get
            {
                return consultaEstructuraEspecialidades;
            }
            set
            {
                consultaEstructuraEspecialidades = value;
            }
        }

        public static bool EstructuraEspecialidades
        {
            get
            {
                return estructuraEspecialidades;
            }
            set
            {
                estructuraEspecialidades = value;
            }
        }

        #endregion
    }
}
