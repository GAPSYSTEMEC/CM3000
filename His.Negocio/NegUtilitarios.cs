using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using His.Entidades;
using His.Datos;

namespace His.Negocio
{
    public class NegUtilitarios
    {
        public static void OnlyNumber(KeyPressEventArgs e, bool isdecimal)
        {
            String aceptados = null;
            if (!isdecimal)
            {
                aceptados = "0123456789" + Convert.ToChar(8);
            }
            if (aceptados.Contains("" + e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        public static void OnlyNumberDecimal(KeyPressEventArgs e, bool isdecimal)
        {
            String aceptados = null;
            if (!isdecimal)
            {
                aceptados = "0123456789." + Convert.ToChar(8);
            }
            if (aceptados.Contains("" + e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        public static void OnlyHora(KeyPressEventArgs e, bool isdecimal)
        {
            String aceptados = null;
            if (!isdecimal)
            {
                aceptados = "0123456789:" + Convert.ToChar(8);
            }
            if (aceptados.Contains("" + e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        public static bool ValidaTemperatura( decimal valor )
        {
            if (valor >= 0 && valor <= 45)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Temperatura no puede ser mayo de 45°", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        public static bool ValidaPrecion1(double valor)
        {
            if (valor >= 0 && valor <= 300)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Presión 1 no puede ser mayoR de 300", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        public static bool ValidaPrecion2(double valor)
        {
            if (valor >= 0 && valor <= 250)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Presión 2 no puede ser mayor de 250", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        public static bool validadorEmail(string correo)
        {
            bool ok = false;
            try
            {
                String expresion;
                expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
                if (Regex.IsMatch(correo, expresion))
                {
                    if (Regex.Replace(correo, expresion, String.Empty).Length == 0)
                    {
                        ok = true;
                    }
                    else
                    {
                        ok = false;
                    }
                }
                else
                {
                    ok = false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return ok;
        }

        public static List<DtoParametros> RecuperaInformacionCorreo()
        {
            return new DatParametros().RecuperaParametros(20);
        }

        public static string RutaLogo(string tipo)
        {
            return new DatParametros().RutaLogo(tipo);
        }
    }
}
