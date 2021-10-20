using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using His.Datos;

namespace His.Negocio
{
    public class NegCertificadoMedico
    {
        DatCertificadoMedico Certificado = new DatCertificadoMedico();
        public DataTable BuscarPaciente(string ate_codigo)
        {
            DataTable Tabla = new DataTable();
            Tabla = Certificado.BuscarPaciente(Convert.ToInt64(ate_codigo));
            return Tabla;
        }
        public DataTable Medico_Pacientes()
        {
            DataTable Tabla = new DataTable();
            Tabla = Certificado.Medico_Paciente();
            return Tabla;
        }
        public void InsertarCertificado(string ate_codigo, string med_codigo, string observacion, string reposo,
            string actividad, string contingencia, string tratamiento, string procedimiento, int ingreso, DateTime fechaCirugia)
        {
            Certificado.InsertarCertificado(Convert.ToInt32(ate_codigo), Convert.ToInt32(med_codigo), observacion, Convert.ToInt32(reposo),
                actividad, contingencia, tratamiento, procedimiento, ingreso, fechaCirugia);
        }
        public void InsertarCertificadoDetalle(string cie_codigo)
        {
            Certificado.InsertarCertificadoDetalle(cie_codigo);
        }
        public DataTable CargarDatosCertificado(string ate_codigo)
        {
            DataTable Tabla = new DataTable();
            Tabla = Certificado.CargarDatosCertificado(Convert.ToInt32(ate_codigo));
            return Tabla;
        }
        public DataTable CargarDatosCertificado_Detalle(Int64 cer_Codigo)
        {
            DataTable Tabla = new DataTable();
            Tabla = Certificado.CargarDatosCertificado_Detalle(cer_Codigo);
            return Tabla;
        }
        public string path()
        {
            string path = Certificado.PathImagen();
            return path;
        }

        public string pathPre()
        {
            string path = Certificado.PathImagenPre();
            return path;
        }
        public DataTable CargarCie10Hosp(string ate_codigo)
        {
            DataTable Tabla = Certificado.CargarCie10Hosp(Convert.ToInt64(ate_codigo));
            return Tabla;
        }
        public DataTable CargarCie10Emerg(string ate_codigo)
        {
            DataTable Tabla = Certificado.CargarCie10Emerg(Convert.ToInt64(ate_codigo));
            return Tabla;
        }
        public DataTable CargarCie10Consulta(string ate_codigo)
        {
            DataTable Tabla = Certificado.CargarCie10Consulta(Convert.ToInt64(ate_codigo));
            return Tabla;
        }
        public DataTable CargarHoras()
        {
            DataTable Tabla = Certificado.CargarHoras();
            return Tabla;
        }
        public DataTable CargarDias()
        {
            DataTable Tabla = Certificado.CargarDias();
            return Tabla;
        }
        public static DataTable CertificadosMedicos(DateTime fechainicio, DateTime fechafin, bool estado)
        {
            return new DatCertificadoMedico().CertificadosMedicos(fechainicio, fechafin, estado);
        }

        public static DataTable CertificadoXmedicos(DateTime fechainicio, DateTime fechafin, int codMedico, bool estado)
        {
            return new DatCertificadoMedico().CertificadoXmedicos(fechainicio, fechafin, codMedico, estado);
        }

        public static DataTable TiposContingencia()
        {
            return new DatCertificadoMedico().CargarTipoContingencia();
        }
        public static DataTable ReimpresionCertificado(int cer_codigo)
        {
            return new DatCertificadoMedico().ReimpresionCertificado(cer_codigo);
        }
        public static bool InhabilitaCertificado(string motivo, string medico, Int32 codigoCertificado)
        {
            return new DatCertificadoMedico().InhabilitaCertificado(motivo, medico, codigoCertificado);
        }
        public static DataTable VerificaEstado(Int64 ateCodigo)
        {
            return new DatCertificadoMedico().VerificaEstado(ateCodigo);
        }
    }
}
