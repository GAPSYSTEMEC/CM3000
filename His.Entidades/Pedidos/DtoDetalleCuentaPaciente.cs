using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades.Pedidos
{
    public class DtoDetalleCuentaPaciente
    {
        //PED_CODIGO
        public Int64 INDICE { get; set; }
        //PEA_NOMBRE
        public string AREA { get; set; }
        //PEA_NOMBRE
        public string SUBAREA { get; set; }

        //PRO_CODIGO
        public DateTime FECHA { get; set; }
        public String CODIGO { get; set; }
        // PRO_DESCRIPCION
        public string DESCRIPCION { get; set; }
        //PDD_VALOR
        public Decimal VALOR { get; set; }
        //PDD_CANTIDAD
        public decimal CANTIDAD { get; set; }
        //PDD_TOTAL
        public Decimal TOTAL { get; set; }
        //PDD_IVA
        public Decimal IVA { get; set; }
        public Int64 RUBRO { get; set; }
        public string RUBRO_NOMBRE { get; set; }

        public string MEDICO_NOMBRE { get; set; }
        public int MEDICO_COD { get; set; }
        public string NumeroControl { get; set; }
        public double DESCUENTO { get; set; }
        public Int32 TipoMedico { get; set; }
        

    }
}
