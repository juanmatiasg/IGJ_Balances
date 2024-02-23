using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Balances.Utilities
{
    public static class Validator
    {
      
        public static bool IsNumeric(string input)
        {
            // Expresión regular para verificar si la cadena contiene solo dígitos
            Regex numericRegex = new Regex(@"^\d+$", RegexOptions.Compiled);

            // Devuelve true si la cadena contiene solo dígitos, de lo contrario, false
            return numericRegex.IsMatch(input);
        }

        public static bool IsNumericDecimal(decimal input)
        {
            // Expresión regular para verificar si la cadena contiene solo dígitos
            Regex numericRegex = new Regex(@"^\d+$", RegexOptions.Compiled);

            // Devuelve true si la cadena contiene solo dígitos, de lo contrario, false
            return numericRegex.IsMatch(input.ToString());
        }


        public static bool IsEmail(string input)
        {
            // Expresión regular para verificar si la cadena es un correo electrónico válido y no es numérica
            Regex emailRegex = new Regex(@"^(?![0-9]+$)[^\s@]+@[^\s@]+\.[^\s@]+$", RegexOptions.Compiled);

            // Devuelve true si la cadena cumple con las condiciones, de lo contrario, false
            return emailRegex.IsMatch(input);
        }
    }
}
