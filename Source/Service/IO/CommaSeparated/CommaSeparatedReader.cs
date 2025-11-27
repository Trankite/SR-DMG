using Microsoft.VisualBasic.FileIO;
using StarRailDamage.Source.Service.IO.FileStream;
using System.Collections;
using System.IO;

namespace StarRailDamage.Source.Service.IO.CommaSeparated
{
    public class CommaSeparatedReader : FileReader, IEnumerable<string[]?>
    {
        public CommaSeparatedReader(string filePath) : base(filePath) { }

        public CommaSeparatedReader(Stream stream) : base(stream) { }

        public CommaSeparatedReader(StreamReader streamReader) : base(streamReader) { }

        private IEnumerator<string[]?> Enumerator()
        {
            ArgumentNullException.ThrowIfNull(_Reader);
            using TextFieldParser TextFieldParser = new(_Reader);
            TextFieldParser.TextFieldType = FieldType.Delimited;
            TextFieldParser.SetDelimiters(",");
            while (!TextFieldParser.EndOfData)
            {
                yield return TextFieldParser.ReadFields();
            }
        }

        public IEnumerator<string[]?> GetEnumerator() => Enumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}