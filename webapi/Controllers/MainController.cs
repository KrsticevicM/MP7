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

    [HttpPost(Name = "PostAd")]
    [Route("postAd")]
    public int PostAd([FromQuery] string insertJSON)
    {
        Dictionary<string, List<Object>> insertDictionary;

        try
        {
            insertDictionary = DatabaseFunctions.ConvertJsonToDictionary(insertJSON);
        }
        catch
        {
            Console.WriteLine("Incorrect JSON format!");
            return 400;
        }

        List<Object> names = insertDictionary["Names"];
        List<Object> values = insertDictionary["Values"];
        List<Object> row = new List<Object>();
        List<Object> insertRow = new List<Object>();

        Ad ad = new Ad();
        Pet pet = new Pet();
        hasColor hc = new hasColor();
        photoAd pa = new photoAd();

        row = (List<Object>)values[0];

        /*Insert into Ad*/
        try
        {
            foreach (var name in names)
            {
                if (ad.returnColumnTypes().ContainsKey(name.ToString()))
                {
                    int index = names.IndexOf(name.ToString());
                    insertRow.Add(row[index]);
                }
            }

            //DatabaseFunctions.insert(ad, insertRow);
            insertRow.RemoveAll(x => x != null);

            /* Insert into Pet */
            foreach (var name in names)
            {
                if (pet.returnColumnTypes().ContainsKey(name.ToString()))
                {
                    int index = names.IndexOf(name.ToString());
                    insertRow.Add(row[index]);
                }
            }

            //DatabaseFunctions.insert(pet, insertRow);
            insertRow.RemoveAll(x => x != null);

            /* Insert into hasColor */
            foreach (var name in names)
            {
                if (hc.returnColumnTypes().ContainsKey(name.ToString()))
                {
                    int index = names.IndexOf(name.ToString());
                    insertRow.Add(row[index]);
                }
            }

            //DatabaseFunctions.insert(hc, insertRow);
            insertRow.RemoveAll(x => x != null);

            /* Insert into photoAd */
            foreach (var name in names)
            {
                if (pa.returnColumnTypes().ContainsKey(name.ToString()))
                {
                    int index = names.IndexOf(name.ToString());
                    insertRow.Add(row[index]);
                }
            }

            //DatabaseFunctions.insert(pa, insertRow);
            insertRow.RemoveAll(x => x != null);
        }
        catch (Exception ex)
        {
            // Missing database cleanup - required Delete method.
            Console.WriteLine(ex.ToString());
            return 400;
        }

        return 200;
    }
}

