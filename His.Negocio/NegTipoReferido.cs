using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;

namespace His.Negocio
{
    public class NegTipoReferido
    {
        public static List<TIPO_REFERIDO> listaTipoReferido()
        {
            return new DatTipoReferido().listaTipoReferido();
        }
    }
}
