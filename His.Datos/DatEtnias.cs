using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;

namespace His.Datos
{
    public class DatEtnias
    {
        public List<ETNIA> ListaEtnias()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from e in contexto.ETNIA
                        orderby e.E_NOMBRE
                        select e).ToList();
            }
        }
        public ETNIA RecuperarEtniaID(int codigoEtnia)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from e in contexto.ETNIA
                        where e.E_CODIGO == codigoEtnia
                        select e).FirstOrDefault();
            }
        }
    }
}
