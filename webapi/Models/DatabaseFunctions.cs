using System.Data.SQLite;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace MP7_progi.Models
{
    public class DatabaseFunctions
    {
        private static string connectionString = @"Data Source=MP7.db;Version=3;";

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

        //Generalizirana funkcija citanja koja bi trebala uzeti u obzir sve nama potrebne upite
        //**funkcionalnost jos nije testirana posto nema podataka u bazi**
        public static Dictionary<string, List<Table>> read(string table, string? where, string? orderBy, int? numOfCol)
        {
            Dictionary<string, List<Table>> dict = new Dictionary<string,List<Table>>();


            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
     

                string query = "SELECT * FROM " + table;
                if (where != null)
                    query += " WHERE " + where;
                if(orderBy != null)
                    query += " ORDER BY " + orderBy;
                if (numOfCol != null)
                    query += " LIMIT " + numOfCol;

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
           
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
     
                            for (int j = 0; j < reader.StepCount; j++)
                            {
                                string columnName = reader.GetName(j);
                                Console.WriteLine(columnName);
                                List<Table> column_list = new List<Table>();
                              
                                column_list.Add((Table)reader.GetValue(j));
                                
                                dict.Add(columnName, column_list);
                            }                             
                    }               
                }

            }
            return dict;
        }

        public static Dictionary<string, List<Table>> read(string table, string? where, string? orderBy)
        {
            Dictionary<string, List<Table>> dict = new Dictionary<string, List<Table>>();


            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();


                string query = "SELECT * FROM " + table;
                if (where != null)
                    query += " WHERE " + where;
                if (orderBy != null)
                    query += " ORDER BY " + orderBy;

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {

                        for (int j = 0; j < reader.StepCount; j++)
                        {
                            string columnName = reader.GetName(j);
                            Console.WriteLine(columnName);
                            List<Table> column_list = new List<Table>();

                            column_list.Add((Table)reader.GetValue(j));

                            dict.Add(columnName, column_list);
                        }
                    }
                }

            }
            return dict;
        }

        public static Dictionary<string, List<Table>> read(string table, string? where)
        {
            Dictionary<string, List<Table>> dict = new Dictionary<string, List<Table>>();


            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();


                string query = "SELECT * FROM " + table;
                if (where != null)
                    query += " WHERE " + where;

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {

                        for (int j = 0; j < reader.StepCount; j++)
                        {
                            string columnName = reader.GetName(j);
                            Console.WriteLine(columnName);
                            List<Table> column_list = new List<Table>();

                            column_list.Add((Table)reader.GetValue(j));

                            dict.Add(columnName, column_list);
                        }
                    }
                }

            }
            return dict;
        }

        public static Dictionary<string, List<Table>> read(string table)
        {
            Dictionary<string, List<Table>> dict = new Dictionary<string, List<Table>>();


            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();


                string query = "SELECT * FROM " + table;

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {

                        for (int j = 0; j < reader.StepCount; j++)
                        {
                            string columnName = reader.GetName(j);
                            Console.WriteLine(columnName);
                            List<Table> column_list = new List<Table>();

                            column_list.Add((Table)reader.GetValue(j));

                            dict.Add(columnName, column_list);
                        }
                    }
                }

            }
            return dict;
        }
        /*public static void readKorisnik(string email, string psw)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                //Console.WriteLine("succesful");

                string query = "SELECT * FROM Korisnik WHERE email = @email AND lozinka = @psw";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@psw", psw);


                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        //Console.WriteLine("succesful2");
                        while (reader.Read())
                        {
                            Console.WriteLine($"UserID: {reader["userID"]}, UserName: {reader["ime"]}, Email: {reader["email"]}");
                        }
                        //Console.WriteLine("succesful3");
                    }
                }


            }

        }

        public static void readOglas(string kategorija)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                //Console.WriteLine("succesful");

                string query = "SELECT * FROM Oglas NATURAL JOIN Ljubimac WHERE katOglas = @kat";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@kat", kategorija);


                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        //Console.WriteLine("succesful2");
                        while (reader.Read())
                        {
                            Console.WriteLine($"OglasID: {reader["oglasID"]}, Ime ljubimca: {reader["imeLjub"]}, Boja: {reader["boja"]}");
                        }
                        //Console.WriteLine("succesful3");
                    }
                }


            }



        }

        public static void readSlike(int oglasID)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                //Console.WriteLine("succesful");

                string query = "SELECT * FROM Oglas NATURAL JOIN slikeOglas WHERE oglasID = @oglasID";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@oglasID", oglasID);


                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        //Console.WriteLine("succesful2");
                        while (reader.Read())
                        {
                            Console.WriteLine($"OglasID: {reader["oglasID"]}, fotoID: {reader["fotoID"]}, foto: {reader["foto"]}");
                        }
                        //Console.WriteLine("succesful3");
                    }
                }


            }

        }*/
    }

}

  
