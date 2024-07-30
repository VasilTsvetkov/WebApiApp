using Microsoft.AspNetCore.Mvc;
using WebApiApp.ServiceContracts;

/// <summary>
/// Controller for processing SWIFT MT799 messages.
/// </summary>
[ApiController]
[Route("[controller]")]
public class SwiftController : ControllerBase
{
    private readonly ISwiftParser _swiftParser;
    private readonly Database _database;
    private readonly ILogger<SwiftController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="SwiftController"/> class.
    /// </summary>
    /// <param name="swiftParser">The parser for SWIFT messages.</param>
    /// <param name="database">The database for storing messages.</param>
    /// <param name="logger">The logger for recording information.</param>
    public SwiftController(ISwiftParser swiftParser, Database database, ILogger<SwiftController> logger)
    {
        _swiftParser = swiftParser;
        _database = database;
        _logger = logger;
    }

    /// <summary>
    /// Uploads a SWIFT MT799 message file and processes it.
    /// </summary>
    /// <param name="file">The file containing the SWIFT MT799 message. The file name should be 'example_mt799.txt'.</param>
    /// <returns>Result of processing the file.</returns>
    /// <response code="200">The file was processed successfully.</response>
    /// <response code="400">There was an issue with the file upload (e.g., file is empty or has an invalid name).</response>
    [Route("[action]")]
    [HttpPost]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        if (file == null || file.FileName == null)
        {
            return BadRequest("No file uploaded.");
        }

        if (file.Length == 0)
        {
            return BadRequest("Uploaded file is empty.");
        }

        if (file.FileName != "example_mt799.txt")
        {
            return BadRequest("Invalid file name. Expected 'example_mt799.txt'.");
        }

        using (var reader = new StreamReader(file.OpenReadStream()))
        {
            var content = await reader.ReadToEndAsync();
            var fields = _swiftParser.ParseMessage(content);
            _database.SaveSwiftMessage(fields);
        }

        _logger.LogInformation("File processed successfully.");
        return Ok("File processed successfully.");
    }
}