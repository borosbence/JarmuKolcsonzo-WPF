using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace JarmuKolcsonzo.Validators
{
    public class RendszamValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value != null)
            {
                string valueToValidate = value as string;
                if (valueToValidate.Length == 7)
                {
                    return ValidationResult.ValidResult;
                }
            }
            return new ValidationResult(false, "A rendszám 7 karakterből állhat");
        }
    }
}
