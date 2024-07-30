namespace WebApiApp.ServiceContracts
{
    public interface ISwiftParser
    {
        List<string> ParseMessage(string message);
    }
}
