using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Parametros
{
    public class ReporteFormularios
    {
        private static int codigoCatAFam = 3;
        private static int codigoCatOSistemas = 4;
        private static int codigoCatExamFisic = 5;

        public static int codigoCatalogo_AFamiliares
        {
            get { return codigoCatAFam; }
            set { codigoCatAFam = value; }
        }

        public static int codigoCatalogo_OSistemas
        {
            get { return codigoCatOSistemas; }
            set { codigoCatOSistemas = value; }
        }

        public static int codigoCatalogo_ExamenFisico
        {
            get { return codigoCatExamFisic; }
            set { codigoCatExamFisic = value; }
        }
    }
}
