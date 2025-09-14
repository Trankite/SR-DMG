using System.Globalization;
using System.Windows.Controls;

namespace SR_DMG.Source.UI.Rules
{
    public class IsEquation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return new ValidationResult(true, null);
        }
    }
}