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
                    string createKorisnikTableQuery = @"
                        CREATE TABLE IF NOT EXISTS Korisnik(
                        userID INTEGER PRIMARY KEY, 
                        ime TEXT,
                        prezime TEXT,
                        email TEXT UNIQUE,
                        telBroj INTEGER UNIQUE,
                        lozinka TEXT,
                        korIme TEXT UNIQUE,
                        nazSklon TEXT UNIQUE CHECK (ime IS NULL OR prezime IS NULL)
                     );";

                    string createKomunikacijaTableQuery = @"
                        CREATE TABLE IF NOT EXISTS Komunikacija(
                        porID INTEGER PRIMARY KEY,
                        lokPor TEXT,
                        slikaPor TEXT,
                        tekstPor TEXT,
                        oglasID INTEGER,
                        FOREIGN KEY(oglasID) REFERENCES Oglas(oglasID)
                     );";

                    string createLjubimacTableQuery = @"
                        CREATE TABLE IF NOT EXISTS Ljubimac(
                        petID INTEGER,
                        imeLjub TEXT,
                        vrsta TEXT,
                        boja TEXT,
                        starost TEXT,
                        datSatNest DATETIME,
                        lokacija TEXT,
                        tekstniOpis TEXT,
                        oglasID INTEGER,
                        PRIMARY KEY(petID),
                        FOREIGN KEY(oglasID) REFERENCES Oglas(oglasID)
                     );";

                    string createOglasTableQuery = @"
                        CREATE TABLE IF NOT EXISTS Oglas (
                        oglasID INTEGER PRIMARY KEY,
                        katOglas TEXT CHECK (katOglas IN ('u potrazi', 'sretno pronađen', 'nije pronađen', 'pronađen uz nesretne okolnosti'))
                     );";

                    string createpisePorTableQuery = @"
                        CREATE TABLE IF NOT EXISTS pisePor(
                        userID INTEGER,
                        porID INTEGER,
                        PRIMARY KEY(userID, porID),
                        FOREIGN KEY(userID) REFERENCES Korisnik(userID),
                        FOREIGN KEY (porID) REFERENCES Komunikacija(porID)
                     );";

                    string createslikeOglasTableQuery = @"
                        CREATE TABLE IF NOT EXISTS slikeOglas(
                        fotoID INTEGER,
                        userID INTEGER,
                        oglasID INTEGER,
                        petID INTEGER,
                        foto TEXT,
                        PRIMARY KEY( fotoID),
                        FOREIGN KEY (userID) REFERENCES Korisnik(userID),
                        FOREIGN KEY (oglasID) REFERENCES Oglas(oglasID),
                        FOREIGN KEY(petID) REFERENCES Ljubimac(petID)
                     );";

                    string createCountPhotosTrigger = @"
                    CREATE TRIGGER IF NOT EXISTS check_numOf_Photos BEFORE INSERT ON slikeOglas WHEN (
                        SELECT COUNT(*) FROM slikeOglas
                        WHERE userID = NEW.userID AND
                        oglasID = NEW.oglasID AND
                        petID= NEW.petID
                        ) >= 3 
                         BEGIN SELECT RAISE(ABORT,'Cannot insert more than three photos of missing pet'); END;
                    ";



                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = createKorisnikTableQuery;
                        command.ExecuteNonQuery();

                        command.CommandText = createKomunikacijaTableQuery;
                        command.ExecuteNonQuery();

                        command.CommandText = createOglasTableQuery;
                        command.ExecuteNonQuery();

                        command.CommandText = createpisePorTableQuery;
                        command.ExecuteNonQuery();

                        command.CommandText = createslikeOglasTableQuery;
                        command.ExecuteNonQuery();

                        command.CommandText = createLjubimacTableQuery;
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

  
