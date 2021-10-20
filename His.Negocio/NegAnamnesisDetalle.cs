using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;

namespace His.Negocio
{
    public class NegAnamnesisDetalle
    {
        public static void crearAnamnesisDetalle(HC_ANAMNESIS_DETALLE nuevaAnamnesis)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.AddToHC_ANAMNESIS_DETALLE(nuevaAnamnesis);
                contexto.SaveChanges();
            }
        }

        public static void eliminarAnamnesisDetalle(int codigoDetalleAnamnesis)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                HC_ANAMNESIS_DETALLE anamDetalle = contexto.HC_ANAMNESIS_DETALLE.FirstOrDefault(h => h.ADE_CODIGO == codigoDetalleAnamnesis);
                contexto.DeleteObject(anamDetalle);
                contexto.SaveChanges();
            }
        }

        public static void eliminarDiagnosticoDetalle(Int64 codigoDiagnosticoDetalle)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                HC_ANAMNESIS_DIAGNOSTICOS diagDetalle = contexto.HC_ANAMNESIS_DIAGNOSTICOS.FirstOrDefault(h => h.CDA_CODIGO == codigoDiagnosticoDetalle);
                contexto.DeleteObject(diagDetalle);
                contexto.SaveChanges();
            }
        }

        public static void crearAnamnesisConsultas(HC_ANAMNESIS_MOTIVOS_CONSULTA nuevaConsulta)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.AddToHC_ANAMNESIS_MOTIVOS_CONSULTA(nuevaConsulta);
                contexto.SaveChanges();
            }
        }

        public static void crearAnamnesisDiagnosticos(HC_ANAMNESIS_DIAGNOSTICOS nuevoDiagnostico)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    int codigo;
                    if (contexto.HC_ANAMNESIS_DIAGNOSTICOS.Count()>0)
                        codigo = contexto.HC_ANAMNESIS_DIAGNOSTICOS.Max(h=>h.CDA_CODIGO)+1;
                    else
                        codigo = 1;

                    nuevoDiagnostico.CDA_CODIGO = codigo;
                    contexto.AddToHC_ANAMNESIS_DIAGNOSTICOS(nuevoDiagnostico);
                    contexto.SaveChanges();
                }
            }
            catch (Exception err) { throw err; }
        }

        public static void actualizarAnamnesisDiagnosticos(HC_ANAMNESIS_DIAGNOSTICOS detalle)
        {
            new DatAnamnesisDetalle().actualizarAnamnesisDiagnostico(detalle);
        }

        public static void actualizarMotivosConsulta(HC_ANAMNESIS_MOTIVOS_CONSULTA motivos)
        {
            new DatAnamnesisDetalle().actualizarMotivosConsulta(motivos);
        }

        public static List<HC_ANAMNESIS_DETALLE> listaDetallesAnamnesis(int codAnamnesis)
        {
            return new DatAnamnesisDetalle().listaDetallesAnamnesis(codAnamnesis);
        }

        public static void actualizarDetalle(HC_ANAMNESIS_DETALLE detalle)
        {
            new DatAnamnesisDetalle().actualizarDetalle(detalle);
        }

        public static List<HC_ANAMNESIS_MOTIVOS_CONSULTA> listaMotivosConsulta(int codAnamnesis)
        {
            return new DatAnamnesisDetalle().listaMotivosConsulta(codAnamnesis);
        }

        public static HC_ANAMNESIS_DETALLE ultimaAnamnesis(int codAnamnesis)
        {
            return new DatAnamnesisDetalle().ultimaAnamnesis(codAnamnesis);
        }

        public static int ultimoCodigo()
        {
            return new DatAnamnesisDetalle().ultimoCodigo();
        }

        public static int ultimoCodigoADiagnostico()
        {
            return new DatAnamnesisDetalle().ultimoCodigoADiagnostico();
        }

        public static  HC_ANAMNESIS_DIAGNOSTICOS buscarDiagnostico(string codigo)
        {
            return new DatAnamnesisDetalle().buscarDiagnostico(codigo);
        }

        public static List<HC_ANAMNESIS_MOTIVOS_CONSULTA> motivosConsultaPorId(int codAnamnesis)
        {
            return new DatAnamnesisDetalle().motivosConsultaPorId(codAnamnesis);
        }

        public static List<HC_ANAMNESIS_DIAGNOSTICOS> recuperarDiagnosticosAnamnesis(int codAnamnesis)
        {
            return new DatAnamnesisDetalle().recuperarDiagnosticosAnamnesis(codAnamnesis);
        }
        public static HC_ANAMNESIS_DIAGNOSTICOS recuperarDiagnosticosAnamnesisCodigo()
        {
            return new DatAnamnesisDetalle().recuperarDiagnosticosAnamnesisCodigo();
        }
    }
}
