using Microsoft.VisualBasic.FileIO;
using StarRailDamage.Source.Service.IO.FiledStream;
using System.Collections;
using System.IO;

namespace StarRailDamage.Source.Service.IO.CommaSeparated
{
    public class CommaSeparatedReader : FiledReader, IEnumerable<string[]?>
    {
        public CommaSeparatedReader(string filePath) : base(filePath) { }

        public CommaSeparatedReader(Stream stream) : base(stream) { }

        public CommaSeparatedReader(StreamReader streamReader) : base(streamReader) { }

        public IEnumerator<string[]?> GetEnumerator()
        {
            ArgumentNullException.ThrowIfNull(Reader);
            using TextFieldParser TextFieldParser = new(Reader);
            TextFieldParser.TextFieldType = FieldType.Delimited;
            TextFieldParser.SetDelimiters(",");
            while (!TextFieldParser.EndOfData)
            {
                yield return TextFieldParser.ReadFields();
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}