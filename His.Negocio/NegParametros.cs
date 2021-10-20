using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;
using His.Entidades;
using System.Data;

namespace His.Negocio
{
    public class NegParametros
    {
        public static List<DtoParametros> RecuperaParametros(Int16 codigopar)
        {
            return new DatParametros().RecuperaParametros(codigopar);
        }

        public static DataTable RecuepraHorasyLitros(int hora)
        {
            return new DatParametros().RecuepraHorasyLitros(hora);
        }

        public static bool EditarEvolucion()
        {
            return new DatParametros().EDITAEVOLUCION();
        }
        public static bool ParametroAdmisionDesactivacion()
        {
            return new DatParametros().ParametroAdmisionDesactivacion();
        }
        public static double ParametroIva()
        {
            return new DatParametros().ParametroIva();
        }

        public static  bool ParametroDevolucionBienes()
        {
            return new DatParametros().ParametroDevolucionBienes();
        }
        public static double ParametroBodegaQuirofano()
        {
            return new DatParametros().ParametroBodegaQuirofano();
        }
    }
}
