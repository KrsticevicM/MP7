using Microsoft.AspNetCore.Mvc;
using MP7_progi.Models;
using System.Text.Json;
using Newtonsoft.Json;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Collections;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class MainController : ControllerBase
{
    private readonly ILogger<MainController> _logger;

    public MainController(ILogger<MainController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetFrontPageData")]
    [Route("frontpagedata")]
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
    [Route("login")]
    public string Login([FromQuery] string usrname, [FromQuery] string password)
    {
        return DatabaseFunctions.checkLoginData(usrname, password);
    }
    
    [HttpPost(Name = "Register")]
    [Route("register")]
    public int Register([FromQuery] string usrname,[FromQuery] string password, [FromQuery] string email, [FromQuery] string phoneNum, [FromQuery] string name, [FromQuery] string surname)
    {
        ArrayList userRow = new ArrayList();

        userRow.Add(DatabaseFunctions.getNextAvailableID(new User()));
        userRow.Add(usrname);
        userRow.Add(password);
        userRow.Add(email);
        userRow.Add(phoneNum);

        List<ArrayList> userData = new List<ArrayList>();
        userData.Add(userRow); 
        int code1 = DatabaseFunctions.insert(new User(), userData);

        ArrayList regularRow = new ArrayList();
        regularRow.Add(DatabaseFunctions.getNextAvailableID(new Regular()));
        regularRow.Add(name);
        regularRow.Add(surname);

        List<ArrayList> regularData = new List<ArrayList>();
        regularData.Add(regularRow);

        int code2 = DatabaseFunctions.insert(new Regular(), regularData);
        
        if(code1 == 200 && code2 == 200)
        {
            return code1;
        }
        else if(code1 != 200)
        {
            return code1;
        }
        return code2;
    }
}
