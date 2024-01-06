using Microsoft.AspNetCore.Mvc;
using MP7_progi.Models;
using Newtonsoft.Json.Linq;

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
                                                               new List<Table> { new Pet(), new ColorPet(), new hasColor(), new photoAd(), new User() },
                                                               new List<DatabaseFunctions.joinType> {
                                                                   DatabaseFunctions.joinType.Natural,
                                                                   DatabaseFunctions.joinType.Natural,
                                                                   DatabaseFunctions.joinType.Natural,
                                                                   DatabaseFunctions.joinType.Natural,
                                                                   DatabaseFunctions.joinType.Natural,
                                                               }, null);

        return DatabaseFunctions.ConvertDictionaryToJson(data);
    }

    [HttpGet(Name = "Login")]
    [Route("login")]
    public string Login([FromQuery] string usrname, [FromQuery] string password)
    {
        Dictionary<string, List<Object>> result = new Dictionary<string, List<Object>>();
        Expression where = new Expression();
        string userID;

        try
        {
            userID = DatabaseFunctions.checkLoginData(usrname, password);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return "";
        }

        where.addElement((Object)"userID", Expression.OP.EQUAL);
        where.addElement((Object)userID, Expression.OP.None);
        result = DatabaseFunctions.read(new User(), new List<Table> { new Regular() }, new List<DatabaseFunctions.joinType> { DatabaseFunctions.joinType.Natural, DatabaseFunctions.joinType.Natural }, where);

        if (result["Values"].Count != 0)
        {

            Console.WriteLine(result.Values.Count);
            return DatabaseFunctions.ConvertDictionaryToJson(result);
        }

        result = DatabaseFunctions.read(new User(), new List<Table> { new Shelter() }, new List<DatabaseFunctions.joinType> { DatabaseFunctions.joinType.Natural, DatabaseFunctions.joinType.Natural }, where);
        return DatabaseFunctions.ConvertDictionaryToJson(result);
    }


    [HttpGet(Name = "ShelterData")]
    [Route("shelter_data")]
    public string getShelterData()
    {
        return DatabaseFunctions.getShelterData();
    }

    [HttpGet(Name = "CommentData")]
    [Route("comment_data")]
    public string getCommentData(int adID)
    {
        return DatabaseFunctions.getCommentData(adID);
    }

    [HttpPost(Name = "RegisterUser")]
    [Route("register_user")]
    public int RegisterUser([FromQuery] string usrname, [FromQuery] string password, [FromQuery] string email, [FromQuery] string phoneNum, [FromQuery] string name, [FromQuery] string surname)
    {
        List<Object> userRow = new List<Object>();
        int newId = DatabaseFunctions.getNextAvailableID(new User());
        userRow.Add(newId);
        userRow.Add(usrname);
        userRow.Add(email);
        userRow.Add(phoneNum);
        userRow.Add(password);
        userRow.Add("regular");


        int code1 = DatabaseFunctions.insert(new User(), userRow);

        List<Object> regularRow = new List<Object>();
        regularRow.Add(newId);
        regularRow.Add(name);
        regularRow.Add(surname);

        int code2 = DatabaseFunctions.insert(new Regular(), regularRow);

        if (code1 == 200 && code2 == 200)
        {
            return code1;
        }
        else if (code1 != 200)
        {
            return code1;
        }
        return code2;
    }

    [HttpPost(Name = "RegisterShelter")]
    [Route("register_shelter")]
    public int RegisterShelter([FromQuery] string usrname, [FromQuery] string password, [FromQuery] string email, [FromQuery] string phoneNum, [FromQuery] string shelterName)
    {

        List<Object> userRow = new List<Object>();
        int newId = DatabaseFunctions.getNextAvailableID(new User());
        userRow.Add(newId);
        userRow.Add(usrname);
        userRow.Add(email);
        userRow.Add(phoneNum);
        userRow.Add(password);
        userRow.Add("shelter");

        int code1 = DatabaseFunctions.insert(new User(), userRow);

        List<Object> shelterRow = new List<Object>();
        shelterRow.Add(newId);
        shelterRow.Add(shelterName);

        int code2 = DatabaseFunctions.insert(new Shelter(), shelterRow);

        if (code1 == 200 && code2 == 200)
        {
            return code1;
        }
        else if (code1 != 200)
        {
            return code1;
        }
        return code2;
    }

    [HttpPost(Name = "postComment")]
    [Route("post_comment")]
    public string postComment([FromBody] string parameters)
    {
        Console.WriteLine(parameters);
        return DatabaseFunctions.postComment(parameters);
    }

    [HttpPost(Name = "searchAd")]
    [Route("search_ad")]
    public string searchAd([FromBody] string searchParameters)
    {
        Console.WriteLine(searchParameters);
        return DatabaseFunctions.searchAd(searchParameters);
    }

    [HttpPost(Name = "PostAd")]
    [Route("postAd")]
    public int PostAd([FromBody] string insertJSON)
    {
        /* Convert the received JSON string to a specified format dictionary */
        Dictionary<string, List<Object>> insertDictionary;

        Console.WriteLine("Attempt converson...");
        try
        {

            insertDictionary = DatabaseFunctions.ConvertJsonToDictionary(insertJSON);
        }
        catch
        {
            Console.WriteLine("Incorrect JSON format!");
            return 400;
        }

        /* Split the received data dictionary into attribute names and values lists */
        List<Object> names = insertDictionary["Names"];
        List<Object> values = insertDictionary["Values"];
        List<Object> row = new List<Object>();

        /* Frontend sent 'img' but correct is 'photo' */
        names[names.IndexOf("img")] = "photo";

        foreach (string name in names)
        {
            Console.WriteLine(name);
        }

        /* Set order specific index dictionaries for interfacing the Insert method */
        /* These indexes correspond to the indexes of attributes in the tables */
        Dictionary<string, int> adIndex = new Dictionary<string, int>()
        {
            { "adID", 0 },
            { "catAd", 1 },
            { "userID", 2 },
            { "location", 3 },
            { "dateHourMis", 4 },
            { "lat", 5 },
            { "lon", 6 }
        };

        Dictionary<string, int> petIndex = new Dictionary<string, int>()
        {
            { "petID", 0 },
            { "namePet", 1 },
            { "species", 2 },
            { "age", 3 },
            { "description", 4 },
            { "adID", 5 }
        };

        Dictionary<string, int> hasColorIndex = new Dictionary<string, int>()
        {
            { "petID", 0 },
            { "colorID", 1 }
        };

        Dictionary<string, int> photoAdIndex = new Dictionary<string, int>()
        {
            { "photoID", 0 },
            { "photo", 1 },
            { "adID", 2 }
        };

        Dictionary<string, int> colorPet = new Dictionary<string, int>()
        {
            { "crna", 1 },
            { "smeda", 2 },
            { "siva", 3 },
            { "zuta", 4 },
            { "zelena", 5 },
            { "crvena", 6 },
            { "narancasta", 7 },
            { "ljubicasta", 8 },
            { "plava", 9 },
            { "bijela", 10 },
            { "šarena", 11 }
        };

        /* Set table objects for interfacing the Insert method */
        /* Holds table names */
        Ad ad = new Ad();
        Pet pet = new Pet();
        hasColor hc = new hasColor();
        photoAd pa = new photoAd();

        int adID;
        int petID;
        int photoID;
        int userID;

        /* Inserting only one row */
        row = (List<Object>)values[0];

        List<Object> insertRowAd = new List<Object>(new Object[7]);
        List<Object> insertRowPet = new List<Object>(new Object[6]);
        List<Object> insertRowHC = new List<Object>(new Object[2]);
        List<Object> insertRowPA = new List<Object>(new Object[3]);

        try
        {
            /* Insert into Ad */
            foreach (string name in names)
            {
                if (ad.returnColumnTypes().ContainsKey(name))
                {
                    int index;
                    if ((index = names.IndexOf(name)) != -1)

                        insertRowAd[adIndex[name]] = row[index];

                    else throw new Exception("Insert Ad: Index of " + name + " not found!");
                }
            }

            adID = DatabaseFunctions.getNextAvailableID(ad);
            userID = Int32.Parse(insertRowAd[2].ToString());
            
            insertRowAd[0] = adID;
            insertRowAd[5] = "N/A";
            insertRowAd[6] = "N/A";

            /* Force parse to Int32 from Int64 */
            /* and to DateTime from string for compatibility with Insert method */
            insertRowAd[2] = System.Int32.Parse(insertRowAd[2].ToString());
            insertRowAd[4] = (DateTime)System.DateTime.Parse(insertRowAd[4].ToString());

            /* Insert into Pet */
            foreach (string name in names)
            {
                if (pet.returnColumnTypes().ContainsKey(name.ToString()))
                {
                    int index;
                    if ((index = names.IndexOf(name)) != -1)

                        insertRowPet[petIndex[name]] = row[index];

                    else throw new Exception("Insert Pet: Index of " + name + " not found!");
                }
            }

            petID = DatabaseFunctions.getNextAvailableID(pet);

            insertRowPet[0] = petID;
            insertRowPet[5] = adID;

            /* Insert into hasColor */
            foreach (string name in names)
            {
                if (hc.returnColumnTypes().ContainsKey(name.ToString()))
                {
                    int index;
                    if ((index = names.IndexOf(name)) != -1)

                        insertRowHC[hasColorIndex[name]] = row[index];

                    else throw new Exception("Insert hasColor: Index of " + name + " not found!");
                }
            }
            insertRowHC[0] = petID;
            insertRowHC[1] = colorPet[row[names.IndexOf("color")].ToString()];

            /* Insert into photoAd */
            foreach (string name in names)
            {
                if (pa.returnColumnTypes().ContainsKey(name.ToString()))
                {
                    int index;
                    if ((index = names.IndexOf(name)) != -1)

                        insertRowPA[photoAdIndex[name]] = row[index];

                    else throw new Exception("Insert photoAd: Index of " + name + " not found!");
                }
            }

            photoID = DatabaseFunctions.getNextAvailableID(pa);

            insertRowPA[0] = photoID;
            insertRowPA[2] = adID;
        }
        catch (Exception ex)
        {
            /* Fail to organise collected data */
            Console.WriteLine(ex.ToString());
            return 400;
        }


        /* Log new entry insertion attempt */
        Console.WriteLine("-------------------------------------------------------");
        Console.WriteLine("For userID: " + userID.ToString() + " attempt POST AD: ");
        Console.WriteLine("-------------------------------------------------------");
        logNewInsertion(ad, insertRowAd);
        logNewInsertion(pet,insertRowPet);
        logNewInsertion(hc, insertRowHC);
        logNewInsertion(pa, insertRowPA);
        Console.WriteLine("-------------------------------------------------------");
        Console.WriteLine("Result:");

        try { DatabaseFunctions.insert(ad, insertRowAd); }
        catch (Exception ex) { Console.WriteLine(ex.ToString()); return recovery(); }

        try { DatabaseFunctions.insert(pet, insertRowPet); }
        catch (Exception ex) { Console.WriteLine(ex.ToString()); return recovery(); }

        try { DatabaseFunctions.insert(hc, insertRowHC); }
        catch (Exception ex) { Console.WriteLine(ex.ToString()); return recovery(); }

        try { DatabaseFunctions.insert(pa, insertRowPA); }
        catch (Exception ex) { Console.WriteLine(ex.ToString()); return recovery(); }

        return 200;
    }

    /* Insertion failure mechanism - possible automation in the future */
    int recovery()
    {
        Console.WriteLine("WARNING: Error during database insertion, " +
            "please check the logs above and manually delete garbage records!");
        return 400;
    }

    /* Method for logging new insertion into the database */
    void logNewInsertion(Table table, List<Object> list)
    {
        Console.WriteLine("Table: " + table.returnTable());

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] != null)
            {
                if (!(table.returnTable() == "photoAd" && i == 1))
                    Console.Write(list[i].ToString() + "\t");
                else
                    Console.Write(". . .\t");
            }
            else
            {
                Console.Write("NULL\t");
            }
        }

        Console.WriteLine();
    }

}

