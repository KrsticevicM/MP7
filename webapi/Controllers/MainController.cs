using Microsoft.AspNetCore.Mvc;
using MP7_progi.Models;
using System.Text.Json;
using Newtonsoft.Json;
using System.Data.Entity;

namespace webapi.Controllers;

[ApiController]
[Route("main/[controller]")]
public class MainController : ControllerBase
{
    private readonly ILogger<MainController> _logger;

    public MainController(ILogger<MainController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetFrontPageData")]
    public string GetFrontPageData()
    {
        Dictionary<string, List<Object>> data = DatabaseFunctions.read(new Ad(),
                                                               new List<Table> { new Pet(), new ColorPet(), new hasColor(), new photoAd() },
                                                               new List<DatabaseFunctions.joinType> {
                                                                   DatabaseFunctions.joinType.Natural,
                                                                   DatabaseFunctions.joinType.Natural,
                                                                   DatabaseFunctions.joinType.Natural,
                                                                   DatabaseFunctions.joinType.Natural
                                                               }, null);

        return DatabaseFunctions.ConvertDictionaryToJson(data);
    }

    [HttpGet(Name = "Login")]

    public string Login([FromQuery] string usrname, [FromQuery] string password)
    {
        return DatabaseFunctions.checkLoginData(usrname, password);
    }
}
