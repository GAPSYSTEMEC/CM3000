using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;
using System.Data.SqlClient;
using System.Data;

namespace His.Datos
{
     public class DatNumeroControl
    {
        public int RecuperaMaximoNumeroControl()
        {
            int maxim;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<NUMERO_CONTROL> numerocontrol = contexto.NUMERO_CONTROL.ToList();
                if (numerocontrol.Count > 0)
                    maxim = contexto.NUMERO_CONTROL.Max(emp => emp.CODCON);
                else
                    maxim = 0;
                return maxim;
            }
        }
        public NUMERO_CONTROL RecuperaNumeroControlID(int codigoNumControl)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from n in contexto.NUMERO_CONTROL
                        where n.CODCON == codigoNumControl
                        select n).FirstOrDefault();
            }
        }
        public List<NUMERO_CONTROL> RecuperaNumeroControl()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.NUMERO_CONTROL.ToList();
            }
        }

        public DataTable RecuperaNumeroControlPablo()
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataSet Dts;
            BaseContextoDatos obj = new BaseContextoDatos();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Sqlcmd = new SqlCommand("Sp_RecuperaUltimoControl", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");

            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Dts.Tables["tabla"];
            //using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            //{
            //    return contexto.NUMERO_CONTROL.ToList();
            //}
        }


        public void CrearNumeroControl(NUMERO_CONTROL numerocontrol)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Crear("NUMERO_CONTROL", numerocontrol);
            }
        }
        public void GrabarNumeroControl(NUMERO_CONTROL numerocontrolModificada, NUMERO_CONTROL numerocontrolOriginal)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Grabar(numerocontrolModificada, numerocontrolOriginal);
            }
        }
        public void EliminarNumeroControl(NUMERO_CONTROL numerocontrol)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Eliminar(numerocontrol);
            }
        }
    }
}
