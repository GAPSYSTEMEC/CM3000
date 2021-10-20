using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using System.Data.SqlClient;
using Core.Entidades;
namespace His.Datos
{
    public class DatAccesoOpciones
    {
        public int RecuperaMaximoAccesoOpciones(Int16 modulo)
        {
            int maxim;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<ACCESO_OPCIONES> ac = new List<ACCESO_OPCIONES>();
                ac = contexto.ACCESO_OPCIONES.Where(i=>i.MODULO.ID_MODULO==modulo).ToList();
                if (ac.Count > 0)
                    maxim = contexto.ACCESO_OPCIONES.Include("MODULO").Where(i => i.MODULO.ID_MODULO == modulo).Max(emp => emp.ID_ACCESO);
                else
                    maxim = 0;
                return maxim;
            }
        }
        public List<DtoAccesoOpciones> RecuperaAccesoOpciones()
        {
            List<DtoAccesoOpciones> accesogrid = new List<DtoAccesoOpciones>();
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<ACCESO_OPCIONES> accesoopciones = new List<ACCESO_OPCIONES>();
                accesoopciones = contexto.ACCESO_OPCIONES.Include("MODULO").ToList();
                foreach (var acceso in accesoopciones)
                {
                    accesogrid.Add(new DtoAccesoOpciones()
                    {
                        ENTITYSETNAME = acceso.EntityKey.GetFullEntitySetName(),
                        ENTITYID = acceso.EntityKey.EntityKeyValues[0].Key,
                        ID_ACCESO= acceso.ID_ACCESO,
                        DESCRIPCION= acceso.DESCRIPCION,
                        ID_MODULO=acceso.MODULO.ID_MODULO,DESCRIPCIONMod= acceso.MODULO.DESCRIPCION
                        
                    });
                }
                return accesogrid;
            }
        }
        public void CrearAccesoOpciones(ACCESO_OPCIONES accOp)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Crear("ACCESO_OPCIONES", accOp);
            }
        }
        public void GrabarAccesoOpciones(ACCESO_OPCIONES accOpModificada, ACCESO_OPCIONES accOpOriginal)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {

                contexto.Grabar(accOpModificada, accOpOriginal);
            }
        }
        public void EliminarAccesoOpciones(ACCESO_OPCIONES accOp)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Eliminar(accOp);
            }
        }
        public List<ACCESO_OPCIONES> ListaAccesoOpciones()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.ACCESO_OPCIONES.ToList();
            }
        }
        /// <summary>
        /// Metodo que recupera el listado de accesos por perfil y modulo
        /// </summary>
        /// <param name="codigoPerfil">Codigo del perfil</param>
        /// <param name="codigoModulo">Codigo del modulo</param>
        /// <returns>Retorna ul listado de Acceso_opciones</returns>
        public List<ACCESO_OPCIONES> ListaAccesoOpcionesPorPerfil(Int16 codigoPerfil,Int16 codigoModulo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<ACCESO_OPCIONES> accesoOpcionesLista = (from p in contexto.PERFILES_ACCESOS
                                                             join a in contexto.ACCESO_OPCIONES on p.ID_ACCESO equals a.ID_ACCESO
                                                             where p.ID_PERFIL == codigoPerfil && a.MODULO.ID_MODULO==codigoModulo  
                                                             select a).ToList();
                return accesoOpcionesLista; 
            }
        }

        public bool ParametroBodega()
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            bool bodega = false;
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("SELECT PAD_ACTIVO FROM PARAMETROS_DETALLE WHERE PAD_CODIGO = 24", connection);
            command.CommandType = System.Data.CommandType.Text;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                bodega = Convert.ToBoolean(reader["PAD_ACTIVO"].ToString());
            }
            reader.Close();
            connection.Close();
            return bodega;
        }

    }
}
