using WebApiApp.ServiceContracts;

public class SwiftParser : ISwiftParser
{
    public List<string> ParseMessage(string message)
    {
        var contents = new List<string>();
        var parts = message.Split(new[] { $"{Environment.NewLine}." }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var part in parts)
        {
            var cleanedPart = part.Trim();
            if (cleanedPart.Length > 0)
            {
                contents.Add(cleanedPart);
            }
        }

        return contents;
    }
}
