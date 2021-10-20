using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;
using System.Data;

namespace His.Negocio
{
    public class NegFormulariosHCU
    {
        public static void Crear(FORMULARIOS_HCU formulario)
        {
            new DatFormulariosHCU().Crear(formulario);
        }

        public static List<FORMULARIOS_HCU> RecuperarFormulariosHCU()
        {
            return new DatFormulariosHCU().RecuperarFormulariosHCU();
        }

        public static FORMULARIOS_HCU RecuperarFormularioID(int codigo)
        {
            return new DatFormulariosHCU().RecupararFormularioID(codigo);
        }
        public static int ultimoCodigoFormularios()
        {
            return new DatFormulariosHCU().ultimoCodigoFormulario();
        }
        public static void Editar(FORMULARIOS_HCU formulario)
        {
            new DatFormulariosHCU().Editar(formulario);
        }
        public static void Eliminar(FORMULARIOS_HCU formulario)
        {
            new DatFormulariosHCU().Eliminar(formulario);
        }
        public static List<KeyValuePair<int, string>> RecuperarFormulariosLista()
        {
            return new DatFormulariosHCU().RecuperarFormulariosLista();
        }

        public static DataTable LlenaCombos(string tipo)
        {
            return new DatFormulariosHCU().LlenaCombos(tipo);
        }

        public static DataTable Paciente(string ate_codigo)
        {
            return new DatFormulariosHCU().Paciente(ate_codigo);
        }

        public static List<KardexEnfermeriaMEdicamentos> RecuperaMedicamentos(string ate_codigo, int rubro, int check)
        {
            return new DatFormulariosHCU().RecuperaMedicamentos(ate_codigo, rubro, check);
        }

        public static bool IngresaKardex(IngresaKardex ingresa, int usuario)
        {
            return new DatFormulariosHCU().IngresaKardex(ingresa, usuario);
        }

        public static bool IngresaKardexInsumo(IngresaKardex ingresa, int usuario)
        {
            return new DatFormulariosHCU().IngresaKardexInsumo(ingresa, usuario);
        }

        public static DataTable ReporteDatos(string ate_codigo)
        {
            return new DatFormulariosHCU().ReporteDatos(ate_codigo);
        }

        public static DataTable ReporteDatosInsumos(string ate_codigo)
        {
            return new DatFormulariosHCU().ReporteDatosInsumos(ate_codigo);
        }

        public static DataTable RecuperaKardex(string ate_codigo)
        {
            return new DatFormulariosHCU().RecuperaKardex(ate_codigo);
        }

        public static bool ActualizaMedicamento(DateTime hora, bool check, string observacion, Int64 codigo)
        {
            return new DatFormulariosHCU().ActualizaMedicamento(hora, check,observacion, codigo);
        }
        public static void EliminarProdKardexMed(int codigo)
        {
            new DatFormulariosHCU().EliminarProductoKardexM(codigo);
        }

        public static DataTable RecuperaPrefacturaRubros(Int32 ateCodigo)
        {
            return new DatFormulariosHCU().RecuperaPrefacturaRubros(ateCodigo);
        }

        public static DataTable RecuperaPrefacturaDatos(Int32 ateCodigo)
        {
            return new DatFormulariosHCU().RecuperaPrefacturaDatos(ateCodigo);
        }
    }
}
