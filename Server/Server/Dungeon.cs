
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;

namespace server
{
    public class Dungeon
    {        
        Dictionary<String, Room> roomMap;

        Room spawnRoom;

        public void Init()
        {
            roomMap = new Dictionary<string, Room>();
            {
                var room = new Room("Room 0", "You are standing in the entrance hall\nAll adventures start here");
                room.north = "Room 1";
                roomMap.Add(room.name, room);
            }

            {
                var room = new Room("Room 1", "You are in room 1");
                room.south = "Room 0";
                room.west = "Room 3";
                room.east = "Room 2";
                roomMap.Add(room.name, room);
            }

            {
                var room = new Room("Room 2", "You are in room 2");
                room.north = "Room 4";
                roomMap.Add(room.name, room);
            }

            {
                var room = new Room("Room 3", "You are in room 3");
                room.east = "Room 1";
                roomMap.Add(room.name, room);
            }

            {
                var room = new Room("Room 4", "You are in room 4");
                room.south = "Room 2";
                room.west = "Room 5";
                roomMap.Add(room.name, room);
            }

            {
                var room = new Room("Room 5", "You are in room 5");
                room.south = "Room 1";
                room.east = "Room 4";
                roomMap.Add(room.name, room);
            }

            spawnRoom = roomMap["Room 0"];
        }

        public Room SetRoom()
        {
           return spawnRoom;
        }

        public Room Process(Room clientRoom, String key, Socket UserSocket)
        {
            ASCIIEncoding encoder = new ASCIIEncoding();


            string returnMessage = "";

            returnMessage += (clientRoom.desc);
            returnMessage += ("Exits");
            for (var i = 0; i < clientRoom.exits.Length; i++)
            {
                if (clientRoom.exits[i] != null)
                {
                    returnMessage += (Room.exitNames[i] + " ");
                }
            }

            returnMessage += ("\n> "); 

            var input = key.Split(' ');

            Console.WriteLine("Client in room " + clientRoom.name + " sent " + key);

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
                    Console.Clear();
                    Thread.Sleep(1000);
                    break;

                case "say":
                    returnMessage += ("You say ");
                    for (var i = 1; i < input.Length; i++)
                    {
                        returnMessage += (input[i] + " ");
                    }

                    Thread.Sleep(1000);
                    Console.Clear();
                    break;

                case "go":
                    // is arg[1] sensible?
                    if ((input[1].ToLower() == "north") && (clientRoom.north != null))
                    {
                        clientRoom = roomMap[clientRoom.north];
                    }
                    else
                    {
                        if ((input[1].ToLower() == "south") && (clientRoom.south != null))
                        {
                            clientRoom = roomMap[clientRoom.south];
                        }
                        else
                        {
                            if ((input[1].ToLower() == "east") && (clientRoom.east != null))
                            {
                                clientRoom = roomMap[clientRoom.east];
                            }
                            else
                            {
                                if ((input[1].ToLower() == "west") && (clientRoom.west != null))
                                {
                                    clientRoom = roomMap[clientRoom.west];
                                }
                                else
                                {
                                    if ((input[1].ToLower() == "up") && (clientRoom.up != null))
                                    {
                                        clientRoom = roomMap[clientRoom.up];
                                    }
                                    else
                                    {
                                        if ((input[1].ToLower() == "down") && (clientRoom.down != null))
                                        {
                                            clientRoom = roomMap[clientRoom.down];
                                        }
                                        else
                                        {
                                            //handle error
                                            returnMessage += ("\nERROR");
                                            returnMessage += ("\nCan not go " + input[1] + " from here");
                                            returnMessage += ("\nPress any key to continue");
                                        }
                                    }
                                }
                               
                            }
                        }
                    }
                    break;

                default:
                    //handle error
                    returnMessage += ("\nERROR");
                    returnMessage += ("\nCan not " + key);
                    returnMessage += ("\nPress any key to continue");
                    break;
            }
            byte[] sendbuffer = encoder.GetBytes(returnMessage);

            int bytesSent = UserSocket.Send(sendbuffer);

            return clientRoom;
        }
    }
}
