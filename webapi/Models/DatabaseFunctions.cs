using Newtonsoft.Json;
using System.Collections;
using System.Data.SQLite;
using System.Text.Json;

namespace MP7_progi.Models
{
    public class DatabaseFunctions
    {
        public enum joinType
        {
            Natural,
            Inner,
            Left,
            Right
        }

        public static readonly Dictionary<joinType, string> joinExpression = new()
        {
            {joinType.Natural, "NATURAL JOIN"},
            {joinType.Inner, "INNER JOIN"},
            {joinType.Left, "LEFT JOIN"},
            {joinType.Right, "RIGHT JOIN"}
        };

        private static readonly string connectionString = @"Data Source=MP7.db;Version=3;UTF8Encoding=True;";

        public static void InitializeDB()
        {
            if (!File.Exists(@"MP7.db"))
            {
                SQLiteConnection.CreateFile(@"MP7.db");

                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    //Kreiramo tablice
                    string createUserTableQuery = @"
                          CREATE TABLE IF NOT EXISTS User(
                          userID INT NOT NULL,
                          userName TEXT NOT NULL,
                          email TEXT NOT NULL,
                          phoneNum TEXT NOT NULL,
                          psw TEXT NOT NULL,
                          userType TEXT NOT NULL,
                          PRIMARY KEY (userID)
                     );";

                    string createRegularTableQuery = @"
                          CREATE TABLE IF NOT EXISTS Regular(
                            firstName TEXT NOT NULL,
                            lastName TEXT NOT NULL,
                            userID INT NOT NULL,
                            FOREIGN KEY (userID) REFERENCES User(userID)
                     );";

                    string createShelterTableQuery = @"
                          CREATE TABLE IF NOT EXISTS Shelter(
                            nameShelter TEXT NOT NULL,
                            userID INT NOT NULL,
                            FOREIGN KEY (userID) REFERENCES User(userID)
                    );";

                    string createCommunicationTableQuery = @"
                        CREATE TABLE IF NOT EXISTS Communication(
                        textID INT NOT NULL,
                        photoCom BLOB NOT NULL,
                        textCom TEXT NOT NULL,
                        locCom TEXT NOT NULL,
                        adID INT NOT NULL,
                        userID INT NOT NULL,
                        PRIMARY KEY (textID),
                        FOREIGN KEY (adID) REFERENCES Ad(adID),
                        FOREIGN KEY (userID) REFERENCES User(userID)
                    );";

                    string createPetTableQuery = @"
                        CREATE TABLE IF NOT EXISTS Pet(
                        petID INT NOT NULL,
                        namePet TEXT NOT NULL,
                        species TEXT NOT NULL,
                        age TEXT NOT NULL,
                        description TEXT NOT NULL,
                        adID INT NOT NULL,
                        PRIMARY KEY (petID),
                        FOREIGN KEY (adID) REFERENCES Ad(adID)
                    );";

                    string createAdTableQuery = @"
                       CREATE TABLE IF NOT EXISTS Ad (
                        adID INT NOT NULL,
                        catAd INT NOT NULL CHECK ((catAd IN ('u potrazi', 'sretno pronađen', 'nije pronađen', 'pronađen uz nesretne okolnosti', 'u skloništu'))),
                        userID INT NOT NULL,
                        dateHourMis TEXT NOT NULL,
                        location TEXT NOT NULL,
                        PRIMARY KEY (adID),
                        FOREIGN KEY (userID) REFERENCES User (userID)
                     );";



                    string createPhotoAdTableQuery = @"
                        CREATE TABLE IF NOT EXISTS PhotoAd(
                        photoID INT NOT NULL,
                        photo BLOB NOT NULL,
                        adID INT NOT NULL,
                        PRIMARY KEY (photoID),
                        FOREIGN KEY (adID) REFERENCES Ad(adID)
                    );";

                    string createColorPetTableQuery = @"
                        CREATE TABLE IF NOT EXISTS ColorPet(
                        color TEXT NOT NULL,
                        colorID INT NOT NULL,
                        PRIMARY KEY (colorID)
                    );";

                    string createhasTableQuery = @"
                        CREATE TABLE IF NOT EXISTS has(
                        petID INT NOT NULL,
                        colorID INT NOT NULL,
                        PRIMARY KEY (petID, colorID),
                        FOREIGN KEY (petID) REFERENCES Pet(petID),
                        FOREIGN KEY (colorID) REFERENCES ColorPet(colorID)
                    );";

                    string createCountPhotosTrigger = @"
                    CREATE TRIGGER IF NOT EXISTS check_numOf_Photos
                    BEFORE INSERT ON PhotoAd
                    WHEN (SELECT COUNT(*) FROM PhotoAd WHERE adID = NEW.adID) >= 3
                    BEGIN
                    SELECT RAISE (ABORT, 'Cannot insert more than three photos of missing pet'); END;
                    ";



                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = createUserTableQuery;
                        command.ExecuteNonQuery();

                        command.CommandText = createRegularTableQuery;
                        command.ExecuteNonQuery();

                        command.CommandText = createShelterTableQuery;
                        command.ExecuteNonQuery();

                        command.CommandText = createCommunicationTableQuery;
                        command.ExecuteNonQuery();

                        command.CommandText = createPetTableQuery;
                        command.ExecuteNonQuery();

                        command.CommandText = createAdTableQuery;
                        command.ExecuteNonQuery();

                        command.CommandText = createPhotoAdTableQuery;
                        command.ExecuteNonQuery();

                        command.CommandText = createColorPetTableQuery;
                        command.ExecuteNonQuery();

                        command.CommandText = createhasTableQuery;
                        command.ExecuteNonQuery();

                        command.CommandText = createCountPhotosTrigger;
                        command.ExecuteNonQuery();

                    }
                }
            }
        }

        /*
        read([Table], [List<Table>], [List<joinType>]) takes parameters for doint the SELECT SQL operation
        on the database.

        Params in:
            
            @ [Table]           - Takes the table to perform the operation on, REQ
            @ [List<Table>]     - Takes the list of tables which are going to be joined, OPT (NULL)
            @ [List<joinType>]  - Takes the list of join types for each of the tables, OPT (NULL)
            @ [Expression]      - Where expression, object-presented, OPT, (NULL)
            

        Params out:

            @ [Dictionary<string, List<Object>] - SQL operation result
                > Key                           - Defines if values are column names or data
                    > Possible keys:
                        @ "Names"
                        @ "Values"
                > Values                        - SQL operation result values, columns and data
                    > Values:
                        @ List<Object>          - Names (list of strings) - KEY: Names
                        @ List<List<Object>>    - Data table              - KEY: Values
                            @ List<Object>      - One row
         */
        public static Dictionary<string, List<Object>> read(Table table, List<Table>? joins, List<joinType>? jt, Expression? where)
        {
            Dictionary<string, List<Object>> queryResultData = new();
            Dictionary<string, string> mergedDataTypes = new();
            List<Object> tableAttributes = new();
            List<Object> tableRows = new();
            List<Object> row;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM " + table.returnTable();

                mergedDataTypes = mergedDataTypes
                                  .Concat(table.returnColumnTypes())
                                  .ToDictionary(pair => pair.Key, pair => pair.Value);

                if (joins != null)
                {
                    foreach (Table joinTables in joins)
                    {
                        mergedDataTypes = mergedDataTypes
                                          .Union(joinTables.returnColumnTypes())
                                          .ToDictionary(pair => pair.Key, pair => pair.Value);
                    }

                    for (int i = 0; i < joins.Count; i++)
                    {
                        query += " " + DatabaseFunctions.joinExpression[jt[i]] + " " + joins[i].returnTable();
                    }
                }

                if (where != null)
                {
                    query += (" WHERE " + where.returnExpression());
                }

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            tableAttributes.Add(reader.GetName(i));
                        }
                        while (reader.Read())
                        {
                            row = new List<Object>();

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                if (reader.IsDBNull(i))
                                {
                                    row.Add(null);
                                }
                                else
                                {
                                    if (mergedDataTypes[reader.GetName(i)] == "int")

                                    {
                                        row.Add(reader.GetValue(i));
                                    }
                                    else if (mergedDataTypes[reader.GetName(i)] == "string" || mergedDataTypes[reader.GetName(i)] == "DateTime")
                                    {
                                        row.Add(reader.GetString(i));
                                    }
                                }
                            }
                            tableRows.Add(row);
                        }
                    }
                }
                queryResultData.Add("Names", tableAttributes);
                queryResultData.Add("Values", tableRows);
            }
            return queryResultData;
        }

        public static string ConvertDictionaryToJson(Dictionary<string, List<object>> data)
        {
            if (!data.ContainsKey("Names") || !data.ContainsKey("Values"))
            {
                throw new ArgumentException("Invalid data structure. 'Names' and 'Values' keys are required.");
            }

            var names = ((List<object>)data["Names"]).ConvertAll(name => (string)name);

            if (data["Values"] is List<Object> values)
            {
                var rows = new List<Dictionary<string, object>>();
                foreach (List<Object> valueRow in values)
                {
                    var row = new Dictionary<string, object>();
                    for (int i = 0; i < names.Count; i++)
                    {
                        row[names[i]] = valueRow[i];
                    }
                    rows.Add(row);
                }

                var resultData = new Dictionary<string, object>
                {
                    { "Data", rows }
                };

                string jsonString = JsonConvert.SerializeObject(resultData, Formatting.Indented);

                return jsonString;
            }
            else
            {
                throw new ArgumentException("Invalid data type for 'Values'. List<List<object>> expected.");
            }
        }

        public static Dictionary<string, List<object>> ConvertJsonToDictionary(string jsonString)
        {
            // Deserialize JSON string to a dynamic object
            dynamic jsonData = JsonConvert.DeserializeObject(jsonString);

            // Extract data from dynamic object
            var resultDictionary = new Dictionary<string, List<object>>();
            var names = new List<object>();
            var values = new List<Object>();

            // Extract column names
            foreach (var property in jsonData.Data[0].Properties())
            {
                names.Add(property.Name);
            }

            // Extract data rows
            foreach (var dataRow in jsonData.Data)
            {
                var row = new List<object>();
                foreach (var property in dataRow.Properties())
                {
                    row.Add(property.Value.ToObject<object>());
                }
                values.Add(row);
            }

            // Populate the result dictionary
            resultDictionary["Names"] = names;
            resultDictionary["Values"] = values;

            return resultDictionary;
        }

        /*
        insert(Table, List<ArrayList> method for inserting rows into database

        Params in:

           @ [Table]            - Takes the table to perform the operation on, REQ
           @ [List<Object>]     - Single row that is to be added into table, REQ

        */

        public static int insert(Table table, List<Object> row)
        {
            //check if provided data types inside list match those in table
         
                for (int j = 0; j < table.returnColumnTypes().Count; j++)
                {
                    string suffix = "";
                    if (table.returnColumnTypes().ElementAt(j).Value == "int")
                    {
                        suffix += "32";
                    }
                try
                {
                    if (("system." + table.returnColumnTypes().ElementAt(j).Value + suffix) != row.ElementAt(j).GetType().ToString().ToLower())
                    {
                        throw new InvalidOperationException("ERROR: Tried to enter type that doesn't match that in the table")
                        {
                            Data =
                                {
                                 { "ExpectedType", "system." + table.returnColumnTypes().ElementAt(j).Value },
                                 { "ProvidedType", row.ElementAt(j).GetType().ToString().ToLower() + " (" + row.ElementAt(j) + ")" }
                                }
                        };
                    }                 
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Expected type: " + ex.Data["ExpectedType"]);
                    Console.WriteLine("Provided type: " + ex.Data["ProvidedType"]);

                    return 400; 
                }

            }

            Console.WriteLine("All provided data is matching type");

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                int changes = 0;

                    string query = "INSERT INTO " + table.returnTable();
                    query += "  VALUES(";
                    for (int j = 0; j < table.returnColumnTypes().Count; j++)
                    {

                        if (row.ElementAt(j).GetType().ToString() == "System.String")
                        {
                            query += "'" + row.ElementAt(j) + "'";
                        }
                        else
                        {
                            query += row.ElementAt(j);
                        }
                        if (j < table.returnColumnTypes().Count - 1)
                        {
                            query += ", ";
                        }
                    }
                    query += ");";

                    SQLiteCommand command = new SQLiteCommand(query, connection);

                    int check = command.ExecuteNonQuery();
                    if (check == 1)
                    {
                        changes++;
                    }
                    else
                    {
                        Console.WriteLine("ERROR: Row hasnt been inserted");
                        return 500;
                    }
                    query = "";


                Console.WriteLine("Row has been successfully added");
                return 200;
            }

        }

        /*

        checkLoginData(string username, string password) - method for checking whether user already has account

        Params in:

           @ [string]           - user username, REQ
           @ [string]           - user password, REQ

        Params out:

            @ [string]          - user ID
          
         */
        public static string checkLoginData(string username, string password)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT userID FROM User WHERE userName = @username AND psw = @password";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            object id = reader.GetValue(0);
                            Console.WriteLine("UserID: " + id.ToString());
                            return id.ToString();
                        }

                        Console.WriteLine("Wrong username or password entered");
                        return "";
                    }
                }
            }
        }

        /*

       getShelterData() - method for returning userID, nameShelter, email and phoneNum in that order for all existing shelters in JSON format


       Params out:

           @ [string]          - JSON file format of specified data

        */
        public static string getShelterData()
        {
           
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                string query = "SELECT userID, nameShelter, email, phoneNum FROM User NATURAL JOIN Shelter;";
                    List<Dictionary<string, object>> resultList = new List<Dictionary<string, object>>();

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Dictionary<string, object> row = new Dictionary<string, object>();

                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        string columnName = reader.GetName(i);
                                        object columnValue = reader.GetValue(i);
                                        row[columnName] = columnValue;
                                    }

                                    resultList.Add(row);
                                }

                                string jsonResult = System.Text.Json.JsonSerializer.Serialize(resultList, new JsonSerializerOptions
                                {
                                    WriteIndented = true // Optional: This makes the JSON more readable with indentation
                                });
                                Console.WriteLine(jsonResult);
                                return jsonResult;
                            }

                            return "";
                        }
                    }
                }
            }
        
            /*

            getNextAvailableID(Table) - method to get an ID for new User/Ad/Pet/...

            Params in:

               @ [Table]         - table from which ID is being requested, REQ

            Params out:

                @ [int]          - ID of new User/Ad/Pet/...

             */
            public static int getNextAvailableID(Table table)
        {
            Dictionary<string, List<Object>> ids = new Dictionary<string, List<Object>>();
            ids = read(table, null, null, null);

            List<Object> list = new List<Object>();
            List<Object> valuesList = new List<Object>();
            List<int> idList = new List<int>();

            bool check = ids.TryGetValue("Values", out valuesList);
            if (!check)
            {
                Console.WriteLine("Couldnt get values!");
                return -1;
            }

            for (int i = 0; i < valuesList.Count; i++)
            {
                list = (List<object>)valuesList.ElementAt(i);

                Object id = list.ElementAt(0);

                idList.Add((int)id);
                Console.WriteLine((int)id);
            }
            idList.Sort();

            for (int i = 0; i < idList.Count; i++)
            {
                if (idList.ElementAt(i) != i + 1)
                {
                    return (i + 1);
                }
            }

            return idList.Count + 1;
        }

        public static void databaseTester(Table table)
        {
            Dictionary<string, List<Object>>? tableOut;
            List<Object> attributes = new();
            List<Object> values = new();
            Expression exp = new Expression();

            exp.addElement(Pet.names.petID, Expression.OP.EQUAL);
            exp.addElement("24", Expression.OP.None);


            Console.WriteLine("Attempting read operation from the database...");

            try
            {
                tableOut = DatabaseFunctions.read(table, new List<Table> { new Pet(), new hasColor(), new ColorPet(), new photoAd() }, new List<joinType> { joinType.Natural, joinType.Natural, joinType.Natural, joinType.Natural }, null);
                Console.WriteLine(ConvertDictionaryToJson(tableOut));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            Expression where = new Expression();
            int userID = 24;

            where.addElement((Object)"userID", Expression.OP.EQUAL);
            where.addElement((Object)userID, Expression.OP.None);

            Dictionary<string, List<Object>> result = DatabaseFunctions.read(new User(), new List<Table> { new Regular() }, new List<DatabaseFunctions.joinType> { DatabaseFunctions.joinType.Natural }, where);
            Console.WriteLine(ConvertDictionaryToJson(result));


            
            result.TryGetValue("Names", out attributes);
            result.TryGetValue("Values", out values);

            Console.WriteLine("Performing test for database: " + table);

            foreach (string attrib in attributes.ToArray())
            {
                Console.Write(attrib + "\t\t");
            }

            foreach (List<Object> row in values)
            {
                Console.WriteLine();

                foreach (Object rowItem in row)
                {
                    Console.Write(rowItem + "\t\t");
                }
            }
            Console.WriteLine();
           
        }
    }
}