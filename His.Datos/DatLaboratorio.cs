using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;
using His.Entidades.General;

namespace His.Datos
{
    /// <summary>
    /// Recupera datos de laboratorio
    /// </summary>
    public class DatLaboratorio
    {
        /// <summary>
        /// 
        /// Método para recuperar por fechas 
        /// </summary>
        /// <param name="fechaIni"></param>
        /// <param name="fechaFin"></param>
        /// <returns></returns>
        public List<DtoLaboratorio> RecuperarPacientes(string  fechaIni, string fechaFin)
        {
            try
            {
                
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    if (fechaIni != null)
                    {
                        DateTime fechainicio = Convert.ToDateTime(fechaIni);
                        DateTime fechafinal = Convert.ToDateTime(fechaFin);
                       return (from l in contexto.LABORATORIOS
                                    where fechainicio <= l.FECHA && fechafinal >= l.FECHA
                                    orderby l.FECHA descending
                                    select new DtoLaboratorio
                                    {
                                        HISTORIA_CLINICA = l.HISTORIA_CLINICA,
                                        FECHA = l.FECHA.Value,
                                        APELLIDO = l.APELLIDO,
                                        NOMBRE = l.NOMBRE,
                                        NO_ORDEN = l.NO_ORDEN,
                                        AÑO_ORDEN = l.AÑO_ORDEN.Value,
                                        COD_EXAMEN = l.CODIGO_EXAMEN,
                                        SOAT = l.SOAT.Value,
                                        IESS = l.IESS.Value,
                                        NOM_EXA = l.EXAMEN,
                                        COD_TARIFA = l.COD_TARIFA.Value,
                                        NOM_TARIFA = l.NOM_TARIFA,
                                        TARIFA = l.TARIFA.Value,
                                        COD_IESS = l.COD_IESS.Value,
                                        TAR_IESS = l.TAR_IESS.Value,
                                        TAR_DIFERENCIA = l.TAR_DIFERENCIA.Value,
                                        CANTIDAD = 1,
                                        TOTAL = l.TAR_IESS.Value 
                                    }).ToList(); 
                    }
                    else
                    {
                        return  (from l in contexto.LABORATORIOS
                                    select new DtoLaboratorio
                                    {
                                        HISTORIA_CLINICA = l.HISTORIA_CLINICA,
                                        FECHA = l.FECHA.Value,
                                        APELLIDO = l.APELLIDO,
                                        NOMBRE = l.NOMBRE,
                                        NO_ORDEN = l.NO_ORDEN,
                                        AÑO_ORDEN = l.AÑO_ORDEN.Value,
                                        COD_EXAMEN = l.CODIGO_EXAMEN,
                                        SOAT = l.SOAT.Value,
                                        IESS = l.IESS.Value,
                                        NOM_EXA = l.EXAMEN,
                                        COD_TARIFA = l.COD_TARIFA.Value,
                                        NOM_TARIFA = l.NOM_TARIFA,
                                        TARIFA = l.TARIFA.Value,
                                        COD_IESS = l.COD_IESS.Value,
                                        TAR_IESS = l.TAR_IESS.Value,
                                        TAR_DIFERENCIA = l.TAR_DIFERENCIA.Value,
                                        CANTIDAD = 1,
                                        TOTAL = l.TARIFA.Value
                                    }).ToList();   
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public HC_LABORATORIO_CLINICO recuperarlaboratorioPorAtencion(int codAtencion)
        {
            HC_LABORATORIO_CLINICO laboratorio;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                laboratorio = (from d in contexto.HC_LABORATORIO_CLINICO
                               where d.ATENCIONES.ATE_CODIGO == codAtencion
                               select d).FirstOrDefault();
                return laboratorio;
            }
        }
    }
}
     