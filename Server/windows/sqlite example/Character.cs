using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

using System.Security.Cryptography;

namespace server
{
    public class Character
    {
        ASCIIEncoding encoder = new ASCIIEncoding();

        public String name = "";
        public string playerRoom;

        public Character(String name)
        {
            this.name = name;

            
        }

        public void SetPlayerRoom(string SpawnRoom, Socket ClientSocket)
        {
            this.playerRoom = SpawnRoom;
        }

        public bool PlayerLoginDetails(int userState, string userMessage, Socket ClientSocket, SQLiteConnection connection)
        {
            ASCIIEncoding encoder = new ASCIIEncoding();

            byte[] sendbuffer = null;

            int bytesSent = 0;


            // Check where the user is with the login process.
            if (userState == 0)
            {
                

                sendbuffer = GenerateSalt();
                try
                {
                    bytesSent = ClientSocket.Send(sendbuffer);

                    userState++;
                }

                catch
                {
                    Console.WriteLine("Failed to send stuff.");
                }
                return true;
            }

            // Waiting to recieve the encrypted username and password.
            if (userState == 1)
            {

                var messageParts = userMessage.Split(' ');

                var sql = "insert into " + "table_users" + " (name, password, salt) values";
                sql += "('" + messageParts[0] + "'";
                sql += ",";
                sql += "'" + messageParts[1] + "'";
                sql += ",";
                sql += "'" + messageParts[2] + "'";
                sql += ")";
                SQLiteCommand command = new sqliteCommand(sql, connection);

                try
                {
                    command.ExecuteNonQuery();
                    userState++;
                }
                catch
                {
                    Console.WriteLine("Failed to perform simple addition but still did it anyway.");
                }

                return true;
            }

            // If the user needs to create a new character, or if they already have one.
            if (userState == 2)
            {




                return true;
            }

            // If they have a new account get them to send a name for their character.
            if (userState == 3)
            {

                return true;
            }


            return false;
        }

        public byte[] GenerateSalt()
        {
            var rngCSP = new RNGCryptoServiceProvider();

            byte[] salt = new byte[16];

            rngCSP.GetBytes(salt);

            return salt;

        }


    }

}
