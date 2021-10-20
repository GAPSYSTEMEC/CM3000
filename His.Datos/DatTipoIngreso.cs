using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;

namespace His.Datos
{
    public class DatTipoIngreso
    {
        public List<TIPO_INGRESO> ListaTipoIngreso()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from t in contexto.TIPO_INGRESO
                        orderby t.TIP_DESCRIPCION
                        select t).ToList();
            }
        }

        public List<TIPO_INGRESO> ListaTipoIngresoNombre(String Filtro)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from t in contexto.TIPO_INGRESO
                        where t.TIP_DESCRIPCION.Contains(Filtro) 
                        orderby t.TIP_CODIGO
                        select t).ToList();
            }
        }

        public TIPO_INGRESO TipoPorId(Int16 codigo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from h in contexto.TIPO_INGRESO
                        where h.TIP_CODIGO == codigo
                        select h).FirstOrDefault();
            }
        }
    }
}
