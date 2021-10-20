using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;

namespace His.Negocio
{
    public class NegModulo
    {
        public static List<MODULO> RecuperaModulos()
        {
            return new DatModulo().RecuperaModulos();
        }
    }
}
