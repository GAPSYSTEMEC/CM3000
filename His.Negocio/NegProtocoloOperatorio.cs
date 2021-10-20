using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;
using His.Entidades;
using System.Data;

namespace His.Negocio
{
    public class NegProtocoloOperatorio
    {
        /// <summary>
        /// Método que recupera el último Codido de la tabla HC_PROTOCOLO_OPERATORIO de Base de Datos
        /// </summary>
        /// <returns>Código HC_PROTOCOLO_OPERATORIO</returns>
        public static int ultimoCodigo()
        {
            return new DatProtocoloOperatorio().ultimoCodigo();
        }

        /// <summary>
        /// Método que permite Crear un Elemeto en la tabla HC_PROTOCOLO_OPERATORIO de la Base de Datos
        /// </summary>
        /// <param name="protocolo">Recibe como parametro un Objeto HC_PROTOCOLO_OPERATORIO</param>
        public static void crearProtocolo(HC_PROTOCOLO_OPERATORIO protocolo)
        {
            new DatProtocoloOperatorio().crearProtocolo(protocolo);
        }

        public static void GuardarHoraAnestesia(int prot_codigo, string hora)
        {
            new DatProtocoloOperatorio().GuardarHoraAnestesia(prot_codigo, hora);
        }
        public static string RecuperarHoraAnestesia(int prot_codigo, int ate_codigo)
        {
            return new DatProtocoloOperatorio().RecupararHoraAnestesia(prot_codigo, ate_codigo);
        }
        /// <summary>
        /// Método que permite actualizar los datos en la tabla HC_PROTOCOLO_OPERATORIO 
        /// según el Protocolo Operatorio Modificado
        /// </summary>
        /// <param name="protocolo">Recibe como parametro un Objeto HC_PROTOCOLO_OPERATORIO</param>
        public static void actualizarProtocolo(HC_PROTOCOLO_OPERATORIO protocolo)
        {
            new DatProtocoloOperatorio().actualizarProtocolo(protocolo);
        }

        /// <summary>
        /// Método que permite recuperar Protocolo Operatorio según la atención
        /// </summary>
        /// <param name="codAtencion,CodigoProtocolo">Recibe como parametro el Código de Atención</param>
        /// <returns>Retorna un objeto HC_PROTOCOLO_OPERATORIO</returns>
        public static HC_PROTOCOLO_OPERATORIO recuperarProtocolo(int codAtencion, int CodigoProtocolo) // Método que permite recuperar Protocolo Operatorio según la atención Y EL NUMERO DE FORMULARIO / gIOVANNY tAPIA / 21/09/2012
        {
            return new DatProtocoloOperatorio().recuperarProtocolo(codAtencion, CodigoProtocolo);
        }

        public static HC_PROTOCOLO_OPERATORIO recuperarProtocolo(int codAtencion) // Método que permite recuperar Protocolo Operatorio según la atención Y EL NUMERO DE FORMULARIO / gIOVANNY tAPIA / 21/09/2012
        {
            return new DatProtocoloOperatorio().recuperarProtocolo(codAtencion);
        }  
        public static DataTable  ProtocoloEpicrisis(Int64 ate_codigo)
        {
            return new DatProtocoloOperatorio().ProtocolosEpicrisis(ate_codigo);
        }

    }
}
