using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;


namespace His.Datos
{
    public class DatCIE10
    {
        public Int16 RecuperaMaximoCIE10()
        {
            return 0;
        }
        public CIE10 RecuperarCIE10(string codigoCIE10)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return (from g in contexto.CIE10
                            where g.CIE_CODIGO == codigoCIE10
                            select g).FirstOrDefault();
                }
            }
            catch (Exception err)
            {
                throw err;
            }           
        }
    }
}
