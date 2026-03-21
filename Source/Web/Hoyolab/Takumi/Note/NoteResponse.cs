using StarRailDamage.Source.Core.LocalText.Marked;
using StarRailDamage.Source.Core.LocalText.Marked.Text;
using StarRailDamage.Source.Extension;
using StarRailDamage.Source.Model.Text;
using StarRailDamage.Source.Web.Response;
using System.Diagnostics.CodeAnalysis;

namespace StarRailDamage.Source.Web.Hoyolab.Takumi.Note
{
    public class NoteResponse : ResponseWrapper<NoteResponseWrapper>, IResponseAnalyzedBody<NoteAnalyzedBody>
    {
        private static readonly TextBinding MarkText = MarkedTextManage.Binding(nameof(MarkedText.HoyolabGameNoteStamina));

        public bool TryGetAnalyzedBody([NotNullWhen(true)] out NoteAnalyzedBody? analyedBody)
        {
            if (Content.IsNotNull())
            {
                return true.Configure(analyedBody = new(Content.CurrentStamina, Content.MaxStamina, DateTimeOffset.FromUnixTimeSeconds(Content.StaminaFullTs).ToLocalTime()));
            }
            return false.Configure(analyedBody = default);
        }

        public override string ToString()
        {
            if (TryGetAnalyzedBody(out NoteAnalyzedBody? Body))
            {
                TimeSpan Offset = Body.FullTime.Subtract(DateTimeOffset.Now);
                return MarkText.Format(Body.Current, Body.Maximum, (int)Offset.TotalHours, Offset.Minutes);
            }
            return MarkText.Text;
        }
    }
}