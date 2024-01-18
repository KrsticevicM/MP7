using Microsoft.AspNetCore.Mvc;
using MP7_progi.Models;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Linq.Expressions;
using Expression = MP7_progi.Models.Expression;

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
        if (userID == "")
        {
            return "Wrong password or username entered";
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
    public string postAd([FromBody] string insertJSON)
    {
        //Parsing json data
        dynamic data = JObject.Parse(insertJSON);
        Console.WriteLine(data);

        string catAd = data.Data[0].catAd;
        int userID = data.Data[0].userID;
        string location = data.Data[0].location;
        string dateString = data.Data[0].dateHourMis;

        DateTime dateHourMis = DateTime.ParseExact(dateString, "dd.MM.yyyy. HH:mm", CultureInfo.InvariantCulture);

        string lat = data.Data[0].lat;
        string lon = data.Data[0].lon;


        string namePet = data.Data[0].namePet;
        string species = data.Data[0].species;
        string age = data.Data[0].age;
        string description = data.Data[0].description;


        string colors = data.Data[0].color;
        string[] colorList = colors.Split(',');


        string images = data.Data[0].img;
        string[] imagesList = images.Split(',');


        //dodajemo oglas u bazu
        List<Object> newAdRow = new List<Object>();
        int newAdId = DatabaseFunctions.getNextAvailableID(new Ad());

        newAdRow.Add(newAdId);
        newAdRow.Add(catAd);
        newAdRow.Add(userID);
        newAdRow.Add(location);
        newAdRow.Add(dateHourMis);
        newAdRow.Add(lat);
        newAdRow.Add(lon);

        int code;
        try
        {
            code = DatabaseFunctions.insert(new Ad(), newAdRow);

        }
        catch (Exception e)
        {
            // Handle the exception
            Console.WriteLine("Error during insert operation: " + e.Message);

        }

        //dodajemo ljubimca u bazu

        List<Object> newPetRow = new List<Object>();
        int newPetId = DatabaseFunctions.getNextAvailableID(new Pet());

        newPetRow.Add(newPetId);
        newPetRow.Add(namePet);
        newPetRow.Add(species);
        newPetRow.Add(age);
        newPetRow.Add(description);
        newPetRow.Add(newAdId);

        int code1;
        try
        {
            code1 = DatabaseFunctions.insert(new Pet(), newPetRow);

        }
        catch (Exception e)
        {
            // Handle the exception
            Console.WriteLine("Error during insert operation: " + e.Message);

        }
        
        //dodavanje boje
        foreach (string color in colorList)
        {
            Console.WriteLine(color);
            List<Object> newhasRow = new List<Object>();

            Dictionary<string, List<Object>> res = new Dictionary<string, List<Object>>();
            Expression where = new Expression();
            where.addElement("color", Expression.OP.EQUAL);
            string c = "'" + color + "'";
            where.addElement(c, Expression.OP.None);

            res = DatabaseFunctions.read(new ColorPet(), null, null, where);
            string resJSON = DatabaseFunctions.ConvertDictionaryToJson(res);

            dynamic colorPetData = JObject.Parse(resJSON);
            int colorID = colorPetData.Data[0].colorID;

            newhasRow.Add(newPetId);
            newhasRow.Add(colorID);

            int code2;
            try
            {
                code2 = DatabaseFunctions.insert(new hasColor(), newhasRow);

            }
            catch (Exception e)
            {
                // Handle the exception
                Console.WriteLine("Error during insert operation: " + e.Message);

            }
        }

        //dodavanje slika
        foreach (string img in imagesList)
        {
            List<Object> newPhotoRow = new List<Object>();
            int newPhotoId = DatabaseFunctions.getNextAvailableID(new photoAd());

            newPhotoRow.Add(newPhotoId);
            newPhotoRow.Add(img);
            newPhotoRow.Add(newAdId);
 
            int code3;
            try
            {
                code3 = DatabaseFunctions.insert(new photoAd(), newPhotoRow);

            }
            catch (Exception e)
            {
                // Handle the exception
                Console.WriteLine("Error during insert operation: " + e.Message);

            }
        }



        return "";
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

    [HttpPost(Name = "DeleteAd")]
    [Route("deleteAd")]
    public int DeleteAd([FromBody] string adID)
    {
        Expression set = new Expression();
        Expression where = new Expression();

        int affected = 0;

        Console.WriteLine("INFO: Attempting to set Ad to status: deleted");

        set.addElement("catAd", Expression.OP.EQUAL);
        set.addElement("'obrisano'", Expression.OP.None);

        where.addElement("adID", Expression.OP.EQUAL);
        where.addElement(Int32.Parse(adID), Expression.OP.None);

        affected = DatabaseFunctions.update(new Ad(), set, where);

        if(affected == 0)
        {
            Console.WriteLine("WARNING: No changes made!");
            return 199;
        }
        else
        {
            Console.WriteLine("Success!");
            return 200;
        }
    }

    [HttpPost(Name = "ModifyAd")]
    [Route("modifyAd")]
    public int ModifyAd([FromBody] string modifyJSON)
    {
        Dictionary<string, List<Object>> modifyDictionary = new Dictionary<string, List<object>>();

        try
        {
            modifyDictionary = DatabaseFunctions.ConvertJsonToDictionary(modifyJSON);
        }
        catch
        {
            Console.WriteLine("Incorrect JSON format!");
            return 400;
        }

        List<Object> names = modifyDictionary["Names"];
        List<Object> values = modifyDictionary["Values"];
        List<Object> row = new List<Object>();

        names[names.IndexOf("img")] = "photo";

        row = (List<Object>) values[0];

        Ad ad = new Ad();
        Pet pet = new Pet();
        hasColor hc = new hasColor();
        photoAd pa = new photoAd();

        List<Table> table = new List<Table>();
        List<Expression> set_list = new List<Expression>();
        List<Expression> where_list = new List<Expression>();
        
        table.Add(ad);
        table.Add(pet);
        table.Add(hc);
        table.Add(pa);

        int i = 0;

        Console.WriteLine("INFO: Attempting database update...");

        try
        {
            foreach(Table t in table)
            {
                where_list.Add(new Expression());
                set_list.Add(new Expression()); 

                foreach (string name in names)
                {
                    if (t.returnColumnTypes().ContainsKey(name) || (t.returnTable() == "has" && name == "color"))
                    {
                        Object value = row[names.IndexOf(name)];
                        if (name.Contains("ID"))
                        {
                            if((name == "adID" && t.returnTable() == "Ad") ||
                               (name == "petID" && t.returnTable() == "Pet"))
                            {
                                where_list[i].addElement(name, Expression.OP.EQUAL);
                                where_list[i].addElement(value.ToString(), Expression.OP.None);
                            } 
                            else if (t.returnTable() == "has" || t.returnTable() == "photoAd")
                            {
                                where_list[i].addElement(value.ToString(), Expression.OP.None);
                            }
                        }
                        else
                        {
                            if(t.returnTable() == "photoAd" || t.returnTable() == "has")
                            {
                                set_list[i].addElement(value.ToString(), Expression.OP.None);
                            }
                            else
                            {
                                if (set_list[i].returnExpression() != "") set_list[i].addElement(", ", Expression.OP.None);
                                set_list[i].addElement(name, Expression.OP.EQUAL);

                                if (value is string)
                                {
                                    set_list[i].addElement("'" + value.ToString() + "'", Expression.OP.None);
                                }
                                else
                                {
                                    set_list[i].addElement("'" + value.ToString() + "'", Expression.OP.None);
                                }
                            }
                        }
                    }
                }
                i++;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERROR: Parsing list to UPDATE/INSERT expression failed!");
            Console.WriteLine(ex.Message);
            return 400;
        }

        try
        {
            for(i = 0; i < where_list.Count; i++)
            {

                if (table[i].returnTable() == "photoAd") 
                {
                    modifyAdUpdatePhoto(set_list[i], where_list[i]);
                } 
                else if (table[i].returnTable() == "has")
                {
                    modifyAdUpdateColor(set_list[i], where_list[i]);
                }
                else
                {
                    DatabaseFunctions.update(table[i], set_list[i], where_list[i]);
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine("ERROR: Updating database failed!");
            Console.WriteLine(ex.Message);
            return 400;
        }

        Console.WriteLine("INFO: Success!");

        return 200;
    }

    void modifyAdUpdatePhoto(Expression set, Expression where)
    {
        List<string> photos = new List<string>();
        List<Object> insert = new List<Object>();
        Expression delEx = new Expression();

        /* Delete existing photos */
        delEx.addElement("adID", Expression.OP.EQUAL);
        delEx.addElement(where.returnExpression(), Expression.OP.None);

        DatabaseFunctions.delete(new photoAd(), delEx);

        foreach(string photo in set.returnExpression().Split(','))
        {
            photos.Add(photo);
        }

        int photoID;

        foreach(string photo in photos)
        {
            photoID = DatabaseFunctions.getNextAvailableID(new photoAd());

            insert.Add(photoID);
            insert.Add(photo.Trim());
            insert.Add(Int32.Parse(where.returnExpression().Trim()));

            DatabaseFunctions.insert(new photoAd(), insert);

            insert.Clear();
        }

    }

    void modifyAdUpdateColor(Expression set, Expression where)
    {
        List<string> colors = new List<string>();
        List<Object> insert = new List<Object>();
        Expression delEx = new Expression();

        Dictionary<string, int> colorDic = new Dictionary<string, int>()
        {
            { "crna", 1 },
            { "smeða", 2 },
            { "siva", 3 },
            { "žuta", 4 },
            { "zelena", 5 },
            { "crvena", 6 },
            { "naranèasta", 7 },
            { "ljubièasta", 8 },
            { "plava", 9 },
            { "bijela", 10 },
            { "šarena", 11 }
        };

        /* Delete existing colors */
        delEx.addElement("petID", Expression.OP.EQUAL);
        delEx.addElement(where.returnExpression(), Expression.OP.None);

        DatabaseFunctions.delete(new hasColor(), delEx);

        foreach (string color in set.returnExpression().Split(','))
        {
            colors.Add(color.Trim());
        }

        foreach (string color in colors)
        {
            insert.Add(Int32.Parse(where.returnExpression().Trim()));
            insert.Add(colorDic[color]);

            DatabaseFunctions.insert(new hasColor(), insert);

            insert.Clear();
        }
    }
}
