
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;

#if TARGET_LINUX
using Mono.Data.Sqlite;
using sqliteConnection 	=Mono.Data.Sqlite.SqliteConnection;
using sqliteCommand 	=Mono.Data.Sqlite.SqliteCommand;
using sqliteDataReader	=Mono.Data.Sqlite.SqliteDataReader;
#endif

#if TARGET_WINDOWS
using System.Data.SQLite;
using sqliteConnection = System.Data.SQLite.SQLiteConnection;
using sqliteCommand = System.Data.SQLite.SQLiteCommand;
using sqliteDataReader = System.Data.SQLite.SQLiteDataReader;
#endif

namespace server
{
    public class Dungeon
    {        
       static string spawnRoom = "A1 Safe House";

        sqliteCommand command;


        public void Init(string database, sqliteConnection connection)
        {


        {
            var room = new Room("A1 Roof", "You find yourself on the roof of the apartment building. The view is pretty spectacular but it is not going help you complete your mission. ");
            room.south = "A1 Stairwell 4F";
            var sql = "insert into " + "table_dungeon" + " (name, description, north, south, east, west, up, down) values";
            sql += "('" + room.name + "'";
            sql += ",";
            sql += "'" + room.desc + "'";
            sql += ",";
            sql += "'" + room.north + "'";
            sql += ",";
            sql += "'" + room.south + "'";
            sql += ",";
            sql += "'" + room.east + "'";
            sql += ",";
            sql += "'" + room.west + "'";
            sql += ",";
            sql += "'" + room.up + "'";
            sql += ",";
            sql += "'" + room.down + "'";
            sql += ")";
            command = new sqliteCommand(sql, connection);
            command.ExecuteNonQuery();
         
        }

        {
            var room = new Room("A1 Stairwell 4F", "This is the fourth floor stairwell, it leads to the roof.");
            room.north = "A1 Roof";
            room.down = "A1 Stairwell 3F";
                var sql = "insert into " + "table_dungeon" + " (name, description, north, south, east, west, up, down) values";
                sql += "('" + room.name + "'";
                sql += ",";
                sql += "'" + room.desc + "'";
                sql += ",";
                sql += "'" + room.north + "'";
                sql += ",";
                sql += "'" + room.south + "'";
                sql += ",";
                sql += "'" + room.east + "'";
                sql += ",";
                sql += "'" + room.west + "'";
                sql += ",";
                sql += "'" + room.up + "'";
                sql += ",";
                sql += "'" + room.down + "'";
                sql += ")";
                command = new sqliteCommand(sql, connection);
                command.ExecuteNonQuery();
               
            }

        {
            var room = new Room("A1 Stairwell 3F", "This is the third floor stairwell, this is the floor for the safe house.");
            room.north = "A1 Hall 3F";
            room.up = "A1 Stairwell 4F";
            room.down = "A1 Stairwell 2F";
                var sql = "insert into " + "table_dungeon" + " (name, description, north, south, east, west, up, down) values";
                sql += "('" + room.name + "'";
                sql += ",";
                sql += "'" + room.desc + "'";
                sql += ",";
                sql += "'" + room.north + "'";
                sql += ",";
                sql += "'" + room.south + "'";
                sql += ",";
                sql += "'" + room.east + "'";
                sql += ",";
                sql += "'" + room.west + "'";
                sql += ",";
                sql += "'" + room.up + "'";
                sql += ",";
                sql += "'" + room.down + "'";
                sql += ")";
                command = new sqliteCommand(sql, connection);
                command.ExecuteNonQuery();
               
            }

        {
            var room = new Room("A1 Hall 3F", "This is the third floor hallway, it has rooms numbered from 20 - 30. The safe house is in room 23.");
            room.north = "A1 Safe House";
            room.south = "A1 Stairwell 3F";
                var sql = "insert into " + "table_dungeon" + " (name, description, north, south, east, west, up, down) values";
                sql += "('" + room.name + "'";
                sql += ",";
                sql += "'" + room.desc + "'";
                sql += ",";
                sql += "'" + room.north + "'";
                sql += ",";
                sql += "'" + room.south + "'";
                sql += ",";
                sql += "'" + room.east + "'";
                sql += ",";
                sql += "'" + room.west + "'";
                sql += ",";
                sql += "'" + room.up + "'";
                sql += ",";
                sql += "'" + room.down + "'";
                sql += ")";
                command = new sqliteCommand(sql, connection);
                command.ExecuteNonQuery();
                
            }

        {
            var room = new Room("A1 Safe House", "You awake in the safe house. You know your mission was to destroy the research data being kept at Renraku Corp R&D lab.");
            room.south = "A1 Hall 3F";
                var sql = "insert into " + "table_dungeon" + " (name, description, north, south, east, west, up, down) values";
                sql += "('" + room.name + "'";
                sql += ",";
                sql += "'" + room.desc + "'";
                sql += ",";
                sql += "'" + room.north + "'";
                sql += ",";
                sql += "'" + room.south + "'";
                sql += ",";
                sql += "'" + room.east + "'";
                sql += ",";
                sql += "'" + room.west + "'";
                sql += ",";
                sql += "'" + room.up + "'";
                sql += ",";
                sql += "'" + room.down + "'";
                sql += ")";
                command = new sqliteCommand(sql, connection);
                command.ExecuteNonQuery();
                
            }

        {
            var room = new Room("A1 Stairwell 2F", "This is the second floor stairwell.");
            room.north = "A1 Hall 2F";
            room.up = "A1 Stairwell 3F";
            room.down = "A1 Stairwell 1F";
                var sql = "insert into " + "table_dungeon" + " (name, description, north, south, east, west, up, down) values";
                sql += "('" + room.name + "'";
                sql += ",";
                sql += "'" + room.desc + "'";
                sql += ",";
                sql += "'" + room.north + "'";
                sql += ",";
                sql += "'" + room.south + "'";
                sql += ",";
                sql += "'" + room.east + "'";
                sql += ",";
                sql += "'" + room.west + "'";
                sql += ",";
                sql += "'" + room.up + "'";
                sql += ",";
                sql += "'" + room.down + "'";
                sql += ")";
                command = new sqliteCommand(sql, connection);
                command.ExecuteNonQuery();
                
            }

        {
            var room = new Room("A1 Hall 2F", "This is the second floor hallway, rooms numbered 1 - 19 are here. Nothing of interest though should probably get back to my mission.");
            room.south = "A1 Stairwell 2F";
                var sql = "insert into " + "table_dungeon" + " (name, description, north, south, east, west, up, down) values";
                sql += "('" + room.name + "'";
                sql += ",";
                sql += "'" + room.desc + "'";
                sql += ",";
                sql += "'" + room.north + "'";
                sql += ",";
                sql += "'" + room.south + "'";
                sql += ",";
                sql += "'" + room.east + "'";
                sql += ",";
                sql += "'" + room.west + "'";
                sql += ",";
                sql += "'" + room.up + "'";
                sql += ",";
                sql += "'" + room.down + "'";
                sql += ")";
                command = new sqliteCommand(sql, connection);
                command.ExecuteNonQuery();
                
            }

        {
            var room = new Room("A1 Stairwell 1F", "This is the ground floor stairwell.");
            room.north = "A1 Hall 1F";
            room.up = "A1 Stairwell 2F";
                var sql = "insert into " + "table_dungeon" + " (name, description, north, south, east, west, up, down) values";
                sql += "('" + room.name + "'";
                sql += ",";
                sql += "'" + room.desc + "'";
                sql += ",";
                sql += "'" + room.north + "'";
                sql += ",";
                sql += "'" + room.south + "'";
                sql += ",";
                sql += "'" + room.east + "'";
                sql += ",";
                sql += "'" + room.west + "'";
                sql += ",";
                sql += "'" + room.up + "'";
                sql += ",";
                sql += "'" + room.down + "'";
                sql += ")";
                command = new sqliteCommand(sql, connection);
                command.ExecuteNonQuery();
                
            }

        {
            var room = new Room("A1 Hall 1F", "This is the ground floor hallway, nothing but reception to be found here.");
            room.south = "A1 Stairwell 2F";
            room.north = "Outside South";
                var sql = "insert into " + "table_dungeon" + " (name, description, north, south, east, west, up, down) values";
                sql += "('" + room.name + "'";
                sql += ",";
                sql += "'" + room.desc + "'";
                sql += ",";
                sql += "'" + room.north + "'";
                sql += ",";
                sql += "'" + room.south + "'";
                sql += ",";
                sql += "'" + room.east + "'";
                sql += ",";
                sql += "'" + room.west + "'";
                sql += ",";
                sql += "'" + room.up + "'";
                sql += ",";
                sql += "'" + room.down + "'";
                sql += ")";
                command = new sqliteCommand(sql, connection);
                command.ExecuteNonQuery();
                
            }

        {
            var room = new Room("Outside South", "Finally I am outside, now to get to the R&D lab to destroy that data.");
            room.south = "A1 Stairwell 2F";
                var sql = "insert into " + "table_dungeon" + " (name, description, north, south, east, west, up, down) values";
                sql += "('" + room.name + "'";
                sql += ",";
                sql += "'" + room.desc + "'";
                sql += ",";
                sql += "'" + room.north + "'";
                sql += ",";
                sql += "'" + room.south + "'";
                sql += ",";
                sql += "'" + room.east + "'";
                sql += ",";
                sql += "'" + room.west + "'";
                sql += ",";
                sql += "'" + room.up + "'";
                sql += ",";
                sql += "'" + room.down + "'";
                sql += ")";
                command = new sqliteCommand(sql, connection);
                command.ExecuteNonQuery();
                
            }

            Console.WriteLine("Finished Rebuilding the Dungeon.");
        }

        public string SetRoom()
        {
           return spawnRoom;
        }


        public void RoomInfo(Socket UserSocket, SQLiteConnection connection, Dictionary<Socket, Character> clientDictonary)
        {

            ASCIIEncoding encoder = new ASCIIEncoding();
            string returnMessage = "";

            Character character = clientDictonary[UserSocket];

            command = new sqliteCommand("select * from " + "table_characters" + " where name = " + "'" + character.name + "'", connection);

            var characterSearch = command.ExecuteReader();

            while(characterSearch.Read())
            {
                command = new sqliteCommand("select * from " + "table_dungeon" + " where name = " + "'" + characterSearch["room"] + "'", connection);
            }
            characterSearch.Close();

            var dungeonSearch = command.ExecuteReader();

            while (dungeonSearch.Read())
            {
               
                returnMessage += "-------------------------------";
                returnMessage += "\nName: " + dungeonSearch["name"];
                returnMessage += "\nDescription: " + dungeonSearch["description"];
                returnMessage += "\nNorth: " + dungeonSearch["North"];
                returnMessage += "\nSouth: " + dungeonSearch["South"];
                returnMessage += "\nEast: " + dungeonSearch["East"];
                returnMessage += "\nWest: " + dungeonSearch["West"];
                returnMessage += "\nUp: " + dungeonSearch["Up"];
                returnMessage += "\nDown: " + dungeonSearch["Down"];
                returnMessage += "\n-------------------------------";

            }

            connection.Close();

            byte[] sendbuffer = encoder.GetBytes(returnMessage);

            int bytesSent = UserSocket.Send(sendbuffer);


            //try
            //{
            //    Console.WriteLine("");
            //    command = new sqliteCommand("select * from " + "table_dungeon" + " order by name asc", connection);
            //    var reader = command.ExecuteReader();

            //    while (reader.Read())
            //    {
            //        Console.WriteLine("Name: " + reader["name"]);
            //    }

            //    reader.Close();
            //    Console.WriteLine("");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Failed to display DB" + ex);
            //}



        }

        public void Process(Character clientCharacter, String key, Socket UserSocket, Dictionary<Socket, Character> clientDictonary, SQLiteConnection connection)
        {
            ASCIIEncoding encoder = new ASCIIEncoding();

            connection.Open();

            byte[] sendbuffer;

            int bytesSent;

            string returnMessage = "";

            var input = key.Split(' ');

            command = new sqliteCommand("select * from " + "table_characters" + " where name = " + "'" + clientCharacter.name + "'", connection);

            var characterSearch = command.ExecuteReader();

            while (characterSearch.Read())
            {


                switch (input[0].ToLower())
                {
                    case "help":
                        Console.Clear();
                        returnMessage += ("\nCommands are ....");
                        returnMessage += ("\nhelp - for this screen");
                        returnMessage += ("\nlook - to look around");
                        returnMessage += ("\ngo [north | south | east | west | up | down]  - to travel between locations");
                        returnMessage += ("\nPress any key to continue");
                        Console.ReadKey(true);
                        break;

                    case "look":
                        //loop straight back
                        Thread.Sleep(1000);
                        break;

                    case "say":

                        returnMessage += clientCharacter.name + " said ";

                        for (var i = 1; i < input.Length; i++)
                        {
                            returnMessage += (input[i] + " ");
                        }

                        command = new sqliteCommand("select * from " + "table_characters" + " where Room = " + "'" + characterSearch["room"] + "'", connection);


                        var sameRoomSearch = command.ExecuteReader();

                        while (sameRoomSearch.Read())
                        {
                            if (sameRoomSearch["name"] != UserSocket)
                            {
                                sendbuffer = encoder.GetBytes(returnMessage);
                                // bytesSent = player.Send(sendbuffer);
                            }
                        }
                        Thread.Sleep(1000);

                        break;

                    case "go":

                        command = new sqliteCommand("select * from " + "table_dungeon" + " where name = " + "'" + characterSearch["room"] + "'", connection);

                        var dungeonSearch = command.ExecuteReader();

                        while (dungeonSearch.Read())
                        {


                            // is arg[1] sensible?
                            if ((input[1].ToLower() != null))
                            {
                                command = new sqliteCommand("update table_characters set room = " + "'" + dungeonSearch[input[1].ToLower()] + "'" + " where name = " + "'" + characterSearch["name"] + "'", connection);
                                command.ExecuteNonQuery();
                            }

                            else
                            {
                                //handle error
                                returnMessage += ("\nERROR");
                                returnMessage += ("\nCan not go " + input[1] + " from here");
                                returnMessage += ("\nPress any key to continue");
                            }
                                            
                        }
                        dungeonSearch.Close();
                        characterSearch.Close();
                        break;
                }

                //default:
                    //handle error
                    returnMessage += ("\nERROR");
                    returnMessage += ("\nCan not " + key);
                    returnMessage += ("\nPress any key to continue");
                    break;
            }

           sendbuffer = encoder.GetBytes(returnMessage);

           bytesSent = UserSocket.Send(sendbuffer);

        }
    }
}
