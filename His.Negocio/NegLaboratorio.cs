using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;
using His.Entidades.General;  

namespace His.Negocio
{
   public class NegLaboratorio
    {
       public static List<DtoLaboratorio> RecuperarPacientes(string fechaIni, string fechaFin)
        {
            return new DatLaboratorio().RecuperarPacientes(fechaIni, fechaFin);
        }

       public static HC_LABORATORIO_CLINICO recuperarlaboratorioPorAtencion(int codAtencion)
       {
           return new DatLaboratorio().recuperarlaboratorioPorAtencion(codAtencion);
       }
        //public static List<Vista_Laboratorio > RecuperarPacientesFecha(DateTime fechaIni, DateTime fechaFin)
        //{
        //    try
        //    {
        //        return new DatLaboratorio().RecuperarPacientesFecha(fechaIni, fechaFin);
        //    }
        //    catch (Exception err) { throw err; }
        //}
    }
}
