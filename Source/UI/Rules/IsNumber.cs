using SR_DMG.Source.Employ;
using System.Globalization;
using System.Windows.Controls;

namespace SR_DMG.Source.UI.Rules
{
    public class IsNumber : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (float.TryParse(value.ToString(), out _))
            {
                return ValidationResult.ValidResult;
            }
            return new ValidationResult(false, Simple.Tip_Input_Not_Number);
        }
    }
}