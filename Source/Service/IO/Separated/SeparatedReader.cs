using Microsoft.VisualBasic.FileIO;
using StarRailDamage.Source.Service.IO.FiledStream;
using System.Collections;
using System.IO;

namespace StarRailDamage.Source.Service.IO.Separated
{
    public class SeparatedReader : FiledReader, IEnumerable<string[]?>
    {
        public SeparatedReader(string filePath) : base(filePath) { }

        public SeparatedReader(Stream stream) : base(stream) { }

        public SeparatedReader(StreamReader streamReader) : base(streamReader) { }

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