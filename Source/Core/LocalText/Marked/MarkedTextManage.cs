using StarRailDamage.Source.Core.LocalText.Marked.Text;
using StarRailDamage.Source.Model.Text;
using System.Globalization;
using System.Resources;

namespace StarRailDamage.Source.Core.LocalText.Marked
{
    public class MarkedTextManage : LocalTextManage
    {
        public static readonly LocalTextManage TextManage = new MarkedTextManage();

        protected override ResourceManager Manager => MarkedText.ResourceManager;

        public override CultureInfo Culture
        {
            get => MarkedText.Culture;
            set => MarkedText.Culture = OnUICultureChanged(value);
        }

        public new static TextBinding Binding(string target) => TextManage.Binding(target);

        public new static string GetString(string target) => TextManage.GetString(target);
    }
}