using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;
using System.Data;

namespace His.Negocio
{
    public class NegInterconsulta
    {
        public static void crearInterconsulta(HC_INTERCONSULTA nuevaInterconsulta)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.AddToHC_INTERCONSULTA(nuevaInterconsulta);
                contexto.SaveChanges();
            }
        }
        public static void crearInterconsultaDiagnosticos(HC_INTERCONSULTA_DIAGNOSTICO nuevoDiagnostico)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.AddToHC_INTERCONSULTA_DIAGNOSTICO(nuevoDiagnostico);
                contexto.SaveChanges();
            }
        }

        public static HC_INTERCONSULTA recuperarInterconsulta(int codAtencion) 
        {
            return new DatInterconsulta().recuperarInterconsulta(codAtencion);
        }

        public static HC_INTERCONSULTA Ultima(int ate_codigo, int hin_codigo)
        {
            return new DatInterconsulta().UltimarecuperarInterconsulta(ate_codigo, hin_codigo);
        }
        public static HC_INTERCONSULTA UltimoCodigoAtencion(int ate_codigo)
        {
            return new DatInterconsulta().UltimoCodigoAtencion(ate_codigo);
        }
        public static int ultimoCodigo()
        {
            return new DatInterconsulta().ultimoCodigo();
        }

        public static int ultimoCodigoDiagnostico()
        {
            return new DatInterconsulta().ultimoCodigoDiagnostico();
        }

        public static List<HC_INTERCONSULTA_DIAGNOSTICO> recuperarDiagnosticosIntercIng(int codInterc)
        {
            return new DatInterconsulta().recuperarDiagnosticosIntercIng(codInterc);
        }

        public static List<HC_INTERCONSULTA_DIAGNOSTICO> recuperarDiagnosticosIntercEgre(int codInterc)
        {
            return new DatInterconsulta().recuperarDiagnosticosIntercEgre(codInterc);
        }

        public static void actualizarInterconsulta(HC_INTERCONSULTA interconsulta)
        {
            new DatInterconsulta().actualizarInterconculta(interconsulta);
        }

        public static void actualizarDiagnostico(HC_INTERCONSULTA_DIAGNOSTICO diagnostico)
        {
            new DatInterconsulta().actualizarDiagnosticos(diagnostico);
        }

        public static void GuardarCamaInterconsulta(int hin_codigo, string cama, string medico, string med_codigo, string interconsu_id)
        {
            new DatInterconsulta().GuardarCamaInterconsulta(hin_codigo, cama, medico, med_codigo, interconsu_id);
        }
        public static DataTable RecuperarDatosInterconsulta(int ate_codigo)
        {
            return new DatInterconsulta().RecuperarDatosInterconsulta(ate_codigo);
        }
        public static bool EstadoInterconsulta(int hin_codigo)
        {
            return new DatInterconsulta().EstadoInterconsulta(hin_codigo);
        }
        public static void EditarEstado(int hin_codigo, bool valor)
        {
            new DatInterconsulta().EditarEstado(hin_codigo, valor);
        }
       public static DataTable RecoverDataInterconsulta(int hin_codigo)
        {
            return new DatInterconsulta().RecoverDataInterconsulta(hin_codigo);
        }
    }
}
