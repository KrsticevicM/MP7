using System.Data.SQLite;

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

        public static readonly Dictionary<joinType, string> joinExpression = new ()
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
                          userName INT NOT NULL,
                          email INT NOT NULL,
                          phoneNum INT NOT NULL,
                          psw INT NOT NULL,
                          PRIMARY KEY (userID)
                     );";

                    string createRegularTableQuery = @"
                          CREATE TABLE IF NOT EXISTS Regular(
                            firstName INT NOT NULL,
                            lastName INT NOT NULL,
                            userID INT NOT NULL,
                            FOREIGN KEY (userID) REFERENCES User(userID)
                     );";

                    string createShelterTableQuery = @"
                          CREATE TABLE IF NOT EXISTS Shelter(
                            nameShelter INT NOT NULL,
                            userID INT NOT NULL,
                            FOREIGN KEY (userID) REFERENCES User(userID)
                    );";

                    string createTypeOfUserTableQuery = @"
                          CREATE TABLE IF NOT EXISTS TypeOfUser(
                          userType INT NOT NULL,
                          userID INT NOT NULL,
                          FOREIGN KEY (userID) REFERENCES User(userID)
                    );";

                    string createCommunicationTableQuery = @"
                        CREATE TABLE IF NOT EXISTS Communication(
                        textID INT NOT NULL,
                        photoCom INT NOT NULL,
                        textCom INT NOT NULL,
                        locCom INT NOT NULL,
                        adID INT NOT NULL,
                        userID INT NOT NULL,
                        PRIMARY KEY (textID),
                        FOREIGN KEY (adID) REFERENCES Ad(adID),
                        FOREIGN KEY (userID) REFERENCES User(userID)
                    );";

                    string createPetTableQuery = @"
                        CREATE TABLE IF NOT EXISTS Pet(
                        petID INT NOT NULL,
                        namePet INT NOT NULL,
                        dateHourMis INT NOT NULL,
                        location INT NOT NULL,
                        species INT NOT NULL,
                        age INT NOT NULL,
                        description INT NOT NULL,
                        adID INT NOT NULL,
                        PRIMARY KEY (petID),
                        FOREIGN KEY (adID) REFERENCES Ad(adID)
                    );";

                    string createAdTableQuery = @"
                       CREATE TABLE IF NOT EXISTS Ad (
                        adID INT NOT NULL,
                        catAd INT NOT NULL CHECK ((catAd IN ('u potrazi', 'sretno pronađen', 'nije pronađen', 'pronađen uz nesretne okolnosti', 'u skloništu'))),
                        userID INT NOT NULL,
                        PRIMARY KEY (adID),
                        FOREIGN KEY (userID) REFERENCES User (userID)
                     );";



                    string createPhotoAdTableQuery = @"
                        CREATE TABLE IF NOT EXISTS PhotoAd(
                        photoID INT NOT NULL,
                        photo INT NOT NULL,
                        adID INT NOT NULL,
                        PRIMARY KEY (photoID),
                        FOREIGN KEY (adID) REFERENCES Ad(adID)
                    );";

                    string createColorPetTableQuery = @"
                        CREATE TABLE IF NOT EXISTS ColorPet(
                        color INT NOT NULL,
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

                        command.CommandText = createTypeOfUserTableQuery;
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
        public static Dictionary<string, List<Object>> read(Table table, List<Table>? joins, List<joinType>? jt)
        {
            Dictionary<string, List<Object>> queryResultData = new ();
            Dictionary<string, string> mergedDataTypes = new ();
            List<Object> tableAttributes = new ();
            List<Object> tableRows = new ();
            List<Object> row;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM " + table.returnTable();

<<<<<<< HEAD
                mergedDataTypes = mergedDataTypes
                                  .Concat(table.returnColumnTypes())
                                  .ToDictionary(pair => pair.Key, pair => pair.Value);
=======
                mergedDataTypes = mergedDataTypes.Concat(table.returnColumnTypes()).ToDictionary(pair => pair.Key, pair => pair.Value);
>>>>>>> 26d8c33eb18ac8fadc3022088a250064f3466f78

                if (joins != null)
                {
                    foreach (Table joinTables in joins)
                    {
<<<<<<< HEAD
                        mergedDataTypes = mergedDataTypes
                                          .Union(joinTables.returnColumnTypes())
                                          .ToDictionary(pair => pair.Key, pair => pair.Value);
                    }

                    for (int i = 0; i < joins.Count; i++)
=======
                        mergedDataTypes = mergedDataTypes.Union(joinTables.returnColumnTypes()).ToDictionary(pair => pair.Key, pair => pair.Value);
                    }
                    
                    for(int i = 0; i < joins.Count; i++)
>>>>>>> 26d8c33eb18ac8fadc3022088a250064f3466f78
                    {
                        query += " " + DatabaseFunctions.joinExpression[jt[i]] + " " + joins[i].returnTable();
                    }
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
<<<<<<< HEAD
                                    if (mergedDataTypes[reader.GetName(i)] == "int")
=======
                                    if (mergedDataTypes[reader.GetName(i)] != "string")
>>>>>>> 26d8c33eb18ac8fadc3022088a250064f3466f78
                                    {
                                        row.Add(reader.GetValue(i));
                                    }
                                    else if (mergedDataTypes[reader.GetName(i)] == "string")
                                    {
                                        row.Add(reader.GetString(i));
                                    }
                                    else if (mergedDataTypes[reader.GetName(i)] == "DateTime")
                                    {
                                        row.Add(reader.GetDateTime(i).ToString());
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
        public static void databaseTester(Table table)
        {
            Dictionary<string, List<Object>>? tableOut;
            List<Object> attributes = new ();
            List<Object> values = new ();

            Console.WriteLine("Attempting read operation from the database...");

            try
            {
                tableOut = DatabaseFunctions.read(table, new List<Table> { new Pet() }, new List<joinType> { joinType.Natural });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            tableOut.TryGetValue("Names", out attributes);
            tableOut.TryGetValue("Values", out values);

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