using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Service.IO.FiledStream;
using System.IO;
using System.Text;

namespace StarRailDamage.Source.Service.IO.CommaSeparated
{
    public class CommaSeparatedWriter : FiledWriter
    {
        private bool _Separated = true;

        public CommaSeparatedWriter(string filePath) : base(filePath) { }

        public CommaSeparatedWriter(Stream stream) : base(stream) { }

        public CommaSeparatedWriter(StreamWriter writer) : base(writer) { }

        public void Write(string value)
        {
            _Writer?.Write(Escaped(value));
            _Separated = false;
        }

        public void WriteLine(params string[] values)
        {
            values.Foreach(Write);
            _Writer?.WriteLine();
            _Separated = true;
        }

        private string Escaped(string value)
        {
            return (_Separated ? string.Empty : ',') + Escape(value);
        }

        public static string Escape(string value)
        {
            StringBuilder StringBuilder = new();
            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] is ',' or '\n' or '\r' or '"')
                {
                    while (i < value.Length)
                    {
                        if (value[i] is '"')
                        {
                            StringBuilder.Append('"');
                        }
                        StringBuilder.Append(value[i++]);
                    }
                    return '"' + StringBuilder.ToString() + '"';
                }
                StringBuilder.Append(value[i]);
            }
            return StringBuilder.ToString();
        }
    }
}