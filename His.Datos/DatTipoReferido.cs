using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;

namespace His.Datos
{
    public class DatTipoReferido
    {
        public List<TIPO_REFERIDO> listaTipoReferido()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from r in contexto.TIPO_REFERIDO
                        orderby r.TIR_DESCRIPCION
                        select r).ToList();
            }
        }
    }
}
