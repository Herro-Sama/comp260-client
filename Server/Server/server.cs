﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace server
{
    class server
    {
        static bool active = true;
        static LinkedList<string> incommingMessages = new LinkedList<string>();
        static Dictionary<String, ReceiveThreadLaunchInfo> connectedClients = new Dictionary<string, ReceiveThreadLaunchInfo>();
        static Dungeon MudowRun = new Dungeon();


        class ReceiveThreadLaunchInfo
        {
            public ReceiveThreadLaunchInfo(int ID, Socket socket, Room room)
            {
                this.ID = ID;
                this.socket = socket;
                this.room = room;
            }

            public int ID;
            public Socket socket;
            public Room room;

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
                            receiveInfo.room = MudowRun.Process(receiveInfo.room, message, receiveInfo.socket);
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

                var ThreadLaunchInfo = new ReceiveThreadLaunchInfo(ID, newClientSocket, MudowRun.SetRoom());

                string clientID = "" + ID;

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

            MudowRun.Init();

            Console.WriteLine("Waiting for client ...");

            var myThread = new Thread(acceptClientThread);
            myThread.Start(s);

            //int tick = 0;
            int itemsProcessed = 0;
            string tempID = "" + 0;


            while (true)
            {
                String labelToPrint = "";

                //if (connectedClients[tempID].room != null)
                //{

                //    var ClientUpdate = MudowRun.Process(connectedClients[tempID].room, labelToPrint);


                //}

                lock (incommingMessages)
                {
                    if (incommingMessages.First != null)
                    {
                        labelToPrint = incommingMessages.First.Value;
                        incommingMessages.RemoveFirst();

                        itemsProcessed++;
                    }
                }

                //if (labelToPrint != "")
                //{
                //    Console.WriteLine(tick + ": " + itemsProcessed + " " + labelToPrint);
                //}

                //tick++;

                Thread.Sleep(1);

            }

            }

    //End of Program

    }

}
