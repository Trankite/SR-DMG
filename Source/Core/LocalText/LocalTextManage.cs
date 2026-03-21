using StarRailDamage.Source.Core.LocalText.Fixed;
using StarRailDamage.Source.Core.LocalText.Marked;
using StarRailDamage.Source.Model.Text;
using System.Diagnostics;
using System.Globalization;
using System.Resources;

namespace StarRailDamage.Source.Core.LocalText
{
    public abstract class LocalTextManage
    {
        private static readonly LocalTextManage[] ManageTable;

        protected abstract ResourceManager Manager { get; }

        private readonly Dictionary<string, TextBinding> TextBindingTable = [];

        public abstract CultureInfo Culture { get; set; }

        public CultureInfo OnUICultureChanged(CultureInfo cultureInfo)
        {
            if (Culture != cultureInfo)
            {
                foreach (KeyValuePair<string, TextBinding> LocalText in TextBindingTable)
                {
                    LocalText.Value.Text = GetString(LocalText.Key);
                }
            }
            return cultureInfo;
        }

        [DebuggerStepThrough]
        public TextBinding Binding(string target)
        {
            return TextBindingTable.TryGetValue(target, out TextBinding? TextBinding) ? TextBinding : TextBindingTable[target] = new TextBinding(GetString(target));
        }

        public string GetString(string target)
        {
            return Manager.GetString(target, Culture) ?? $"Unknown LocalText:{target}";
        }

        public static void SetCultrue(CultureInfo cultureInfo)
        {
            CultureInfo.CurrentCulture = cultureInfo;
            foreach (LocalTextManage TextManage in ManageTable)
            {
                TextManage.Culture = cultureInfo;
            }
        }

        static LocalTextManage()
        {
            ManageTable = [FixedTextManage.TextManage, MarkedTextManage.TextManage];
        }
    }
}