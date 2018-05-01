﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;

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
    class server
    {
        static bool active = true;
        static LinkedList<string> incommingMessages = new LinkedList<string>();
        static Dictionary<String, ReceiveThreadLaunchInfo> connectedClients = new Dictionary<string, ReceiveThreadLaunchInfo>();
        static Dungeon MudowRun = new Dungeon();

        static string databaseName = "database.database";



        class ReceiveThreadLaunchInfo
        {
            public ReceiveThreadLaunchInfo(int ID, Socket socket, Character newCharacter)
            {
                this.ID = ID;
                this.socket = socket;
                this.clientCharacter = newCharacter;
            }

            public int ID;
            public Socket socket;
            public Character clientCharacter;

        }

        static void clientReceiveThread(object obj)
        {
            ReceiveThreadLaunchInfo receiveInfo = obj as ReceiveThreadLaunchInfo;
            bool socketactive = true;

            while ((active == true) && (socketactive == true))
            {
                byte[] buffer = new byte[4094];

                try
                {
                    int result = receiveInfo.socket.Receive(buffer);


                    if (result > 0)
                    {
                        ASCIIEncoding encoder = new ASCIIEncoding();

                        lock (incommingMessages)
                        {
                            string message = encoder.GetString(buffer, 0, result);
                            receiveInfo.clientCharacter.playerRoom = MudowRun.Process(receiveInfo.clientCharacter, message, receiveInfo.socket);
                            MudowRun.RoomInfo(receiveInfo.clientCharacter, receiveInfo.socket);
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    socketactive = false;
                }




            }


        }

        static void acceptClientThread(object obj)
        {
            Socket s = obj as Socket;

            int ID = 0;

            while (active == true)
            {
                var newClientSocket = s.Accept();

                var myThread = new Thread(clientReceiveThread);

                string clientID = "" + ID;

                var newCharacter = new Character(clientID);

                var ThreadLaunchInfo = new ReceiveThreadLaunchInfo(ID, newClientSocket, newCharacter);

                ThreadLaunchInfo.clientCharacter.SetPlayerRoom(MudowRun.SetRoom(), ThreadLaunchInfo.socket);

                MudowRun.RoomInfo(ThreadLaunchInfo.clientCharacter, ThreadLaunchInfo.socket);

                connectedClients.Add(clientID, ThreadLaunchInfo);

                myThread.Start(ThreadLaunchInfo);

                ID++;
            }

        }


        static void Main(string[] args)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint ipLocal = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8221);

            s.Bind(ipLocal);
            s.Listen(4);

            sqliteConnection connection;
            sqliteCommand command;

            try
            {
                connection = new sqliteConnection("Data Source=" + databaseName + ";Version=3;FailIfMissing=True");

                if (connection != null)
                {
                    sqliteConnection.CreateFile(databaseName);

                    connection.Open();

                    command = new sqliteCommand("create table table_phonenumbers (name varchar(20), number int)", connection);

                    command.ExecuteNonQuery();

                    connection = new sqliteConnection("Data Source=" + databaseName + ";Version=3;FailIfMissing=True");

                }

                try
                {
                    Console.WriteLine("enter name");
                    var name = Console.ReadLine();

                    Console.WriteLine("enter number:");
                    var phoneNo = Console.ReadLine();

                    connection.Open();

                    command = new sqliteCommand("select * from  table_phonenumbers where name == '" + name + "'", connection);
                    var reader = command.ExecuteReader();

                    if (reader.HasRows == false)
                    {
                        try
                        {
                            var sql = "insert into " + "table_phonenumbers" + " (name, number) values";
                            sql += "('" + name + "'";
                            sql += ",";
                            sql += "'" + phoneNo + "'";
                            sql += ")";

                            command = new sqliteCommand(sql, connection);
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Failed to add: " + name + " : " + phoneNo + " to DB " + ex);
                        }
                    }

                    else
                    {
                        Console.WriteLine("User: " + name + " in DB already");
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Failed to add to DB " + ex);
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Failed to create a new Database: " + ex);
            }
           

            MudowRun.Init();

            Console.WriteLine("Waiting for client ...");

            var myThread = new Thread(acceptClientThread);
            myThread.Start(s);

            int itemsProcessed = 0;
            string tempID = "" + 0;


            while (true)
            {
                String labelToPrint = "";


                lock (incommingMessages)
                {
                    if (incommingMessages.First != null)
                    {
                        labelToPrint = incommingMessages.First.Value;
                        incommingMessages.RemoveFirst();

                        itemsProcessed++;
                    }
                }

                Thread.Sleep(1);

            }

            }

    //End of Program

    }

}
