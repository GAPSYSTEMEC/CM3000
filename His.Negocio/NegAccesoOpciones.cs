using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;
using His.Entidades;
using His.Parametros; 

namespace His.Negocio
{
    public class NegAccesoOpciones
    {
        public static int RecuperaMaximoAccesoOpciones(Int16 modulo)
        {
            return new DatAccesoOpciones().RecuperaMaximoAccesoOpciones(modulo);
        }
        public static List<DtoAccesoOpciones> RecuperaAccesoOpciones()
        {
            return new DatAccesoOpciones().RecuperaAccesoOpciones();
        }
        public static void CrearAccesoOpciones(ACCESO_OPCIONES accOp)
        {
            new DatAccesoOpciones().CrearAccesoOpciones(accOp);
        }
        public static void GrabarAccesoOpciones(ACCESO_OPCIONES accOpModificada, ACCESO_OPCIONES accOpOriginal)
        {
            new DatAccesoOpciones().GrabarAccesoOpciones(accOpModificada, accOpOriginal);
        }
        public static void EliminarAccesoOpciones(ACCESO_OPCIONES accOp)
        {
            new DatAccesoOpciones().EliminarAccesoOpciones(accOp);
        }
        public static List<ACCESO_OPCIONES> ListaAccesoOpciones()
        {
            return new DatAccesoOpciones().ListaAccesoOpciones();
        }
        /// <summary>
        /// Metodo que asigana todas las opciones de acceso que tiene un perfil
        /// </summary>
        /// <param name="codigoPerfil">Codigo del Perfil</param>
        public static void asignarAccesosPorPerfil(Int16 codigoPerfil)
        {
            try
            {
                //Recupero la lista de modulos 
                List<MODULO> listaModulos = new DatModulo().RecuperaModulos();
                foreach (var modulo in listaModulos)
                {
                    //Recupero la lista de accesos que tendra el perfil para un modulo en especial
                    List<ACCESO_OPCIONES> listaAccesoOpciones = new DatAccesoOpciones().ListaAccesoOpcionesPorPerfil(codigoPerfil, modulo.ID_MODULO);
                    //Asigna las opciones de acceso para el Modulo de Mantenimiento
                    if (modulo.ID_MODULO == 1)
                    {

                    }
                    else if (modulo.ID_MODULO == 2)
                    {
                        foreach (var acceso in listaAccesoOpciones)
                        {
                            switch (acceso.ID_ACCESO)
                            {
                                //MENU
                                //Acceso Administracion de Medicos
                                case 2001:
                                    AccesosModuloHonorarios.Medicos = true;
                                    break;
                                //Acceso Administracion de Especialidades
                                case 2002:
                                    AccesosModuloHonorarios.Especialidades = true;
                                    break;
                                //Acceso Tipo de Documento
                                case 2003:
                                    AccesosModuloHonorarios.Tipo_documento = true;
                                    break;
                                //Acceso Comisiones
                                case 2004:
                                    AccesosModuloHonorarios.ComisionesClinicaReferido = true;
                                    break;
                                //Acceso Tipo Retenciones
                                case 2005:
                                    AccesosModuloHonorarios.TipoRetencion = true;
                                    break;
                                //Acceso Tipo Honorario
                                case 2006:
                                    AccesosModuloHonorarios.TipoHonorario = true;
                                    break;
                                //Acceso Ingreso de facturas
                                case 2008:
                                    AccesosModuloHonorarios.IngresoFacturas = true;
                                    break;
                                //Acceso Consulta Honorarios por Medico
                                case 2009:
                                    AccesosModuloHonorarios.HonorariosPorMedico = true;
                                    break;
                                //Acceso Explorador
                                case 2010:
                                    AccesosModuloHonorarios.Explorador = true;
                                    break;
                                //Acceso Emision Retenciones
                                case 2011:
                                    AccesosModuloHonorarios.EmisionRetenciones = true;
                                    break;
                                //Acceso Notas de Credito
                                case 2012:
                                    AccesosModuloHonorarios.NotasCredito = true;
                                    break;
                                //Acceso Notas de Debito
                                case 2013:
                                    AccesosModuloHonorarios.NotasDebito = true;
                                    break;
                                //Acceso Notas Valores no cubiertos
                                case 2014:
                                    AccesosModuloHonorarios.NotasValoresNoCubiertos = true;
                                    break;
                                //Acceso Notas Debito Comisiones
                                case 2015:
                                    AccesosModuloHonorarios.NotasComisiones = true;
                                    break;
                                //Acceso Nuevo Correo
                                case 2016:
                                    AccesosModuloHonorarios.NuevoCorreo = true;
                                    break;
                                //Acceso Opciones Correo
                                case 2017:
                                    AccesosModuloHonorarios.OpcionesCorreo = true;
                                    break;
                                //Acceso Reporte Medicos
                                case 2018:
                                    AccesosModuloHonorarios.ReporteMedicos = true;
                                    break;
                                //Acceso Reporte Notas
                                case 2019:
                                    AccesosModuloHonorarios.ReporteNotas = true;
                                    break;
                                //Acceso Reporte Retenciones
                                case 2020:
                                    AccesosModuloHonorarios.ReporteRetenciones = true;
                                    break;
                                //Acceso Reporte Contable
                                case 2021:
                                    AccesosModuloHonorarios.ReporteContable = true;
                                    break;
                                //Acceso Honorarios pendientes de pago
                                case 2022:
                                    AccesosModuloHonorarios.HonorariosPendientesPago = true;
                                    break;
                                //Acceso Honorarios pendientes por cancelar
                                case 2023:
                                    AccesosModuloHonorarios.HonorariosPendientesCancelar = true;
                                    break;
                                //Acceso Honorarios cancelados
                                case 2024:
                                    AccesosModuloHonorarios.HonorariosCancelados = true;
                                    break;
                                //Acceso Balance Gerencial
                                case 2025:
                                    AccesosModuloHonorarios.BalanceGerencial = true;
                                    break;
                                default:
                                    break;
                            }

                        }
                    }
                    //Asigna las opciones de acceso para el Modulo de Acceso
                    else if (modulo.ID_MODULO == 5)
                    {
                        foreach (var acceso in listaAccesoOpciones)
                        {
                            switch (acceso.ID_ACCESO)
                            {
                                //MENU
                                //Acceso Modulo de Mantenimiento
                                case 1:
                                    AccesosModuloAcceso.setModuloMantenimiento(true);
                                    break;
                                //Acceso Modulo de Honorarios
                                case 2:
                                    AccesosModuloAcceso.setModuloHonorarios(true);
                                    break;
                                //Acceso Modulo de Tarifario
                                case 3:
                                    AccesosModuloAcceso.setModuloTarifario(true);
                                    break;
                                //Acceso Modulo de Admision
                                case 4:
                                    AccesosModuloAcceso.setModuloAdmision(true);
                                    break;
                                //Acceso Modulo de Habitaciones
                                case 5:
                                    AccesosModuloAcceso.setModuloHabitaciones(true);
                                    break;
                                case 6:
                                    AccesosModuloAcceso.setModuloEmergencias(true);
                                    break;
                                case 7:
                                    AccesosModuloAcceso.setModuloPedidos(true);
                                    break;

                                case 8:
                                    AccesosModuloAcceso.setModuloCuentaPaciente(true);
                                    break;
                                case 9:
                                    AccesosModuloAcceso.ModuloPedidosEspeciales = true;
                                    break;
                                case 10:
                                    AccesosModuloAcceso.ModuloMedicos = true;
                                    break;
                                case 11:
                                    AccesosModuloAcceso.ModuloConsultaExterna = true;
                                    break;
                                case 15:
                                    AccesosModuloAcceso.ModuloEmergencia = true;
                                    break;
                                default:
                                    break;
                            }

                        }
                    }
                    else if (modulo.ID_MODULO == 3)
                    {
                        foreach (var acceso in listaAccesoOpciones)
                        {
                            switch (acceso.ID_ACCESO)
                            {
                                //MENU
                                //Acceso Explorador de Pacientes
                                case 301:
                                    AccesosModuloAdmision.ExploradorPacientes = true;
                                    break;
                                //Acceso Formularios HCU
                                case 302:
                                    AccesosModuloAdmision.FormulariosHCU = true;
                                    break;
                                //Acceso Admision
                                case 303:
                                    AccesosModuloAdmision.Admision = true;
                                    break;
                                //Acceso Mantenimiento
                                case 304:
                                    AccesosModuloAdmision.Mantenimiento = true;
                                    break;
                                //Acceso Informacion Morbimortalidad
                                case 305:
                                    AccesosModuloAdmision.InfMorbimortalidad = true;
                                    break;
                                //Acceso Detalle Atencion
                                case 306:
                                    AccesosModuloAdmision.DetalleAtencion = true;
                                    break;
                                //Acceso Formularios de la Atencion
                                case 307:
                                    AccesosModuloAdmision.FormulariosAtencion = true;
                                    break;
                                //Añadir Formularios en la Atencion
                                case 308:
                                    AccesosModuloAdmision.AgregarFormulariosAtencion = true;
                                    break;
                                //Acceso a microfilms de la atencion 
                                case 309:
                                    AccesosModuloAdmision.MicrofilmsAtencion = true;
                                    break;
                                //Acceso historia de la atencion 
                                case 310:
                                    AccesosModuloAdmision.HistoriaAtencion = true;
                                    break;
                                //Ingreso Formularios HCU 
                                case 311:
                                    AccesosModuloAdmision.IngresarFormulario = true;
                                    break;
                                //Editar Formularios HCU 
                                case 312:
                                    AccesosModuloAdmision.EditarFormulario = true;
                                    break;
                                //Eliminar Formularios HCU 
                                case 313:
                                    AccesosModuloAdmision.EliminarFormulario = true;
                                    break;
                                //Ingresar Paciente
                                case 314:
                                    AccesosModuloAdmision.IngresarPaciente = true;
                                    break;
                                //Ingresar Atencion 
                                case 315:
                                    AccesosModuloAdmision.IngresarAtencion = true;
                                    break;
                                //Editar paciente/atencion 
                                case 316:
                                    AccesosModuloAdmision.EditarPaciente = true;
                                    break;
                                //Eliminar paciente/atencion
                                case 317:
                                    AccesosModuloAdmision.EliminarPaciente = true;
                                    break;
                                //Ingreso Admision Emergencia
                                case 319:
                                    AccesosModuloAdmision.AdmisionEmergencia = true;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else if (modulo.ID_MODULO == 6)
                    {
                        foreach (var acceso in listaAccesoOpciones)
                        {
                            switch (acceso.ID_ACCESO)
                            {
                                //MENU
                                //A 
                                case 601:
                                    AccesosModuloTarifario.ConsultaTipoEmpresa = true;
                                    break;
                                //A
                                case 602:
                                    AccesosModuloTarifario.ConsultaAseguradorasEmpresas = true;
                                    break;
                                //A
                                case 603:
                                    AccesosModuloTarifario.ConsultaConveniosProcedimientos = true;
                                    break;
                                //A
                                case 604:
                                    AccesosModuloTarifario.ConsultaTarifario = true;
                                    break;
                                //A
                                case 605:
                                    AccesosModuloTarifario.ConsultaProcedimientos = true;
                                    break;
                                //A
                                case 606:
                                    AccesosModuloTarifario.ConsultaCreacionHonorarios = true;
                                    break;
                                //A
                                case 607:
                                    AccesosModuloTarifario.ConsultaReportes = true;
                                    break;
                                //A
                                case 608:
                                    AccesosModuloTarifario.ConsultaEstructuraEspecialidades = true;
                                    break;
                                //A
                                case 609:
                                    AccesosModuloTarifario.TipoEmpresaCRUD = true;
                                    break;
                                //A
                                case 610:
                                    AccesosModuloTarifario.AseguradorasEmpresasCRUD = true;
                                    break;
                                //I
                                case 611:
                                    AccesosModuloTarifario.ConveniosCRUD = true;
                                    break;
                                //E
                                case 612:
                                    AccesosModuloTarifario.TarifariosCRUD = true;
                                    break;
                                //E
                                case 613:
                                    AccesosModuloTarifario.ProcedimientosCRUD = true;
                                    break;
                                //I
                                case 614:
                                    AccesosModuloTarifario.CreacionHonorarios = true;
                                    break;

                                case 615:
                                    AccesosModuloTarifario.EstructuraEspecialidades = true;
                                    break;
                                case 171:
                                    AccesosModuloTarifario.CreacionTipoCatologo = true;
                                    break;
                                case 172:
                                    AccesosModuloTarifario.ModificacionTipoCatologo = true;
                                    break;
                                case 173:
                                    AccesosModuloTarifario.EliminacionTipoCatologo = true;
                                    break;
                                case 174:
                                    AccesosModuloTarifario.CreacionCatologo = true;
                                    break;
                                case 175:
                                    AccesosModuloTarifario.ModificacionCatologo = true;
                                    break;
                                case 176:
                                    AccesosModuloTarifario.EliminacionCatologo = true;
                                    break;
                                case 177:
                                    AccesosModuloTarifario.CreacionConvenio = true;
                                    break;
                                case 178:
                                    AccesosModuloTarifario.ModificacionConvenio = true;
                                    break;
                                case 179:
                                    AccesosModuloTarifario.EliminacionConvenio = true;
                                    break;
                                case 180:
                                    AccesosModuloTarifario.CreacionPrecioConvenio = true;
                                    break;
                                case 181:
                                    AccesosModuloTarifario.ModificacionPrecioConvenio = true;
                                    break;
                                case 182:
                                    AccesosModuloTarifario.EliminacionPrecioConvenio = true;
                                    break;

                                default:
                                    break;
                            }


                        }
                    }
                    else if (modulo.ID_MODULO == 11)
                    {
                        foreach (var acceso in listaAccesoOpciones)
                        {
                            switch (acceso.ID_ACCESO)
                            {
                                case 11:
                                    AccesosModuloAcceso.ModuloConsultaExterna = true;
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }
        public static bool ParametroBodega()
        {
            return new DatAccesoOpciones().ParametroBodega();
        }
    }
}
