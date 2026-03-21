using StarRailDamage.Source.Core.LocalText.Fixed.Text;
using StarRailDamage.Source.Model.Text;
using System.Globalization;
using System.Resources;

namespace StarRailDamage.Source.Core.LocalText.Fixed
{
    public class FixedTextManage : LocalTextManage
    {
        public static readonly LocalTextManage TextManage = new FixedTextManage();

        protected override ResourceManager Manager => FixedText.ResourceManager;

        public override CultureInfo Culture
        {
            get => FixedText.Culture;
            set => FixedText.Culture = OnUICultureChanged(value);
        }

        public new static TextBinding Binding(string target) => TextManage.Binding(target);

        public new static string GetString(string target) => TextManage.GetString(target);
    }
}