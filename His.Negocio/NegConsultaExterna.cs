﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using His.Datos;
using His.Entidades;

namespace His.Negocio
{
    public class NegConsultaExterna
    {
        public static DataTable RecuperaPaciente(Int64 Ate_Codigo)
        {
            return new DatConsultaExterna().RecuperaPaciente(Ate_Codigo);
        }

        public static DataTable RecuperaEspecialidades()
        {
            return new DatConsultaExterna().RecuperaEspecialidades();
        }
        
        public static DataTable RecuperaConsultorios()
        {
            return new DatConsultaExterna().RecuperaConsultorios();
        }

        public static DataTable RecuperaHorario(string tiempo, DateTime fechaCita, string consultorio)
        {
            return new DatConsultaExterna().RecuperaHorario(tiempo, fechaCita, consultorio);
        }

        public static DataTable BuscaPaciente(string cedula)
        {
            return new DatConsultaExterna().BuscaPaciente(cedula);
        }

        public static bool GuardaAgendamientoPaciente(string txtIdentificacion, string txtNombres, string txtApellidos, string txtEmail, string txtTelefono, string txtCelular, string txtDireccion, DateTime dtpFechaCita, string cmbEspecialidades, string lblMedico, string lblMailMed, string cmbConsultorios, string cmbHora, string txtMotivo, string txtNotas)
        {
            return new DatConsultaExterna().GuardaAgendamientoPaciente(txtIdentificacion, txtNombres, txtApellidos, txtEmail, txtTelefono, txtCelular, txtDireccion, dtpFechaCita, cmbEspecialidades, lblMedico, lblMailMed, cmbConsultorios, cmbHora, txtMotivo, txtNotas);
        }

        public static DataTable RecuperaNumAgenda()
        {
            return new DatConsultaExterna().RecuperaNumAgenda();
        }

        public static bool GuardaTriajeSignosVitales(string lblHistoria, Int64 lblAtencion, int nourgente, int urgente, int critico, int muerto, int alcohol, int drogas, int otros, string txtOtrasActual, string txtObserEnfer, decimal txt_PresionA1, decimal txt_PresionA2, decimal txt_FCardiaca, decimal txt_FResp, decimal txt_TBucal, decimal txt_TAxilar, decimal txt_SaturaO, decimal txt_PesoKG, decimal txt_Talla, decimal txtIMCorporal, decimal txt_PerimetroC, decimal txt_Glicemia, decimal txt_TotalG, int cmb_Motora, int cmb_Verbal, int cmb_Ocular, int txt_DiamPDV, string cmb_ReacPDValor, int txt_DiamPIV, string cmb_ReacPIValor, int txt_Gesta, int txt_Partos, int txt_Abortos, int txt_Cesareas, DateTime dtp_ultimaMenst1, decimal txt_SemanaG, int movFetal, int txt_FrecCF, int memRotas, string txt_Tiempo, int txt_AltU, int txt_Presentacion, int txt_Dilatacion, int txt_Borramiento, string txt_Plano, int pelvis, int sangrado, string txt_Contracciones, int urgente2)
        {
            return new DatConsultaExterna().GuardaTriajeSignosVitales(lblHistoria, lblAtencion, nourgente, urgente, critico, muerto, alcohol, drogas, otros, txtOtrasActual, txtObserEnfer, txt_PresionA1, txt_PresionA2, txt_FCardiaca, txt_FResp, txt_TBucal, txt_TAxilar, txt_SaturaO, txt_PesoKG, txt_Talla, txtIMCorporal, txt_PerimetroC, txt_Glicemia, txt_TotalG, cmb_Motora, cmb_Verbal, cmb_Ocular, txt_DiamPDV, cmb_ReacPDValor, txt_DiamPIV, cmb_ReacPIValor, txt_Gesta, txt_Partos, txt_Abortos, txt_Cesareas, dtp_ultimaMenst1, txt_SemanaG, movFetal, txt_FrecCF, memRotas, txt_Tiempo, txt_AltU, txt_Presentacion, txt_Dilatacion, txt_Borramiento, txt_Plano, pelvis, sangrado, txt_Contracciones, urgente2);
        }

        public static bool EditarTriajeSignosVitales(string lblHistoria, Int64 lblAtencion, int nourgente, int urgente, int critico, int muerto, int alcohol, int drogas, int otros, string txtOtrasActual, string txtObserEnfer, decimal txt_PresionA1, decimal txt_PresionA2, decimal txt_FCardiaca, decimal txt_FResp, decimal txt_TBucal, decimal txt_TAxilar, decimal txt_SaturaO, decimal txt_PesoKG, decimal txt_Talla, decimal txtIMCorporal, decimal txt_PerimetroC, decimal txt_Glicemia, decimal txt_TotalG, int cmb_Motora, int cmb_Verbal, int cmb_Ocular, int txt_DiamPDV, string cmb_ReacPDValor, int txt_DiamPIV, string cmb_ReacPIValor, int txt_Gesta, int txt_Partos, int txt_Abortos, int txt_Cesareas, DateTime dtp_ultimaMenst1, decimal txt_SemanaG, int movFetal, int txt_FrecCF, int memRotas, string txt_Tiempo, int txt_AltU, int txt_Presentacion, int txt_Dilatacion, int txt_Borramiento, string txt_Plano, int pelvis, int sangrado, string txt_Contracciones, int urgente2)
        {
            return new DatConsultaExterna().EditarTriajeSignosVitales(lblHistoria, lblAtencion, nourgente, urgente, critico, muerto, alcohol, drogas, otros, txtOtrasActual, txtObserEnfer, txt_PresionA1, txt_PresionA2, txt_FCardiaca, txt_FResp, txt_TBucal, txt_TAxilar, txt_SaturaO, txt_PesoKG, txt_Talla, txtIMCorporal, txt_PerimetroC, txt_Glicemia, txt_TotalG, cmb_Motora, cmb_Verbal, cmb_Ocular, txt_DiamPDV, cmb_ReacPDValor, txt_DiamPIV, cmb_ReacPIValor, txt_Gesta, txt_Partos, txt_Abortos, txt_Cesareas, dtp_ultimaMenst1, txt_SemanaG, movFetal, txt_FrecCF, memRotas, txt_Tiempo, txt_AltU, txt_Presentacion, txt_Dilatacion, txt_Borramiento, txt_Plano, pelvis, sangrado, txt_Contracciones, urgente2);
        }
        public static DataTable RecuperaTriaje(Int64 lblAteCodigo)
        {
            return new DatConsultaExterna().RecuperaTriaje(lblAteCodigo);
        }

        public static DataTable RecuperaSignos(Int64 lblAteCodigo)
        {
            return new DatConsultaExterna().RecuperaSignos(lblAteCodigo);
        }

        public static DataTable RecuperaObstetrica(Int64 lblAteCodigo)
        {
            return new DatConsultaExterna().RecuperaObstetrica(lblAteCodigo);
        }

        public static void GuardaDatos002(DtoForm002 datos)
        {
            new DatConsultaExterna().GuardaDatos002(datos);
        }
        public static DataTable ExistePaciente(Int64 atecodigo)
        {
            return new DatConsultaExterna().PacienteExiste(atecodigo);
        }
        public static DataTable PacienteConsultaExterna(Int64 ateCodigo)
        {
            return new DatConsultaExterna().PacientesConsultaExterna(ateCodigo);
        }
        public static DataTable DatosPaciente(int id)
        {
            return new DatConsultaExterna().PacienteConsultaExterna(id);
        }
        public static Int64 RecuperarId()
        {
            return new DatConsultaExterna().RecuperarId();
        }
        public static void GuardarPrescripcion(Int64 id_form002, int id_usuario, string usuario, string indicacion, string farmaco, DateTime fecha_admin, bool administrado)
        {
            new DatConsultaExterna().GuardarPrescripciones(id_form002, id_usuario, usuario, indicacion, farmaco, fecha_admin, administrado);
        }
        public static DataTable getConsultaExterna(int atecodigo)
        {
            return new DatConsultaExterna().getConsultasExternas(atecodigo);
        }
        public static DataTable VerSintomas()
        {
            return new DatConsultaExterna().VerSintomas();
        }
        public static void GuardarAtencionSintomas(Int64 ate_codigo, Int64 sc_codigo)
        {
            new DatConsultaExterna().GuardaAtencionSintomas(ate_codigo, sc_codigo);
        }
    }
}
