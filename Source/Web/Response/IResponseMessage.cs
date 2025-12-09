namespace StarRailDamage.Source.Web.Response
{
    public interface IResponseMessage
    {
        int ReturnCode { get; set; }

        string Message { get; set; }
    }
}