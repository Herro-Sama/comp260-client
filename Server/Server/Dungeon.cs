
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
                var room = new Room("A1 Roof", "You find yourself on the roof of the apartment building. \n The view is pretty spectacular but it won't help you complete your mission.\n ");
                room.south = "A1 Stairwell 4F";
                roomMap.Add(room.name, room);
            }

            {
                var room = new Room("A1 Stairwell 4F", "This is the fourth floor stairwell, it leads to the roof.");
                room.north = "A1 Roof";
                room.down = "A1 Stairwell 3F";
                roomMap.Add(room.name, room);
            }

            {
                var room = new Room("A1 Stairwell 3F", "This is the third floor stairwell, this is the floor for the safe house.");
                room.north = "A1 Hall 3F";
                room.up = "A1 Stairwell 4F";
                room.down = "A1 Stairwell 2F";
                roomMap.Add(room.name, room);
            }

            {
                var room = new Room("A1 Hall 3F", "This is the third floor hallway, it has rooms numbered from 20 - 30. The safe house is in room 23.");
                room.north = "A1 Safe House";
                room.south = "A1 Stairwell 3F";
                roomMap.Add(room.name, room);
            }

            {
                var room = new Room("A1 Safe House", "You awake in the safe house. \nYou know your mission was to destroy the research data being kept at Renraku Corp R&D lab.");
                room.south = "A1 Hall 3F";
                roomMap.Add(room.name, room);
            }

            {
                var room = new Room("A1 Stairwell 2F", "This is the second floor stairwell.");
                room.north = "A1 Hall 2F";
                room.up = "A1 Stairwell 3F";
                room.down = "A1 Stairwell 1F";
                roomMap.Add(room.name, room);
            }

            {
                var room = new Room("A1 Hall 2F", "This is the second floor hallway, rooms numbered 1 - 19 are here. Nothing of interest though should probably get back to my mission.");
                room.south = "A1 Stairwell 2F";
                roomMap.Add(room.name, room);
            }

            {
                var room = new Room("A1 Stairwell 1F", "This is the ground floor stairwell.");
                room.north = "A1 Hall 1F";
                room.up = "A1 Stairwell 2F";
                roomMap.Add(room.name, room);
            }

            {
                var room = new Room("A1 Hall 1F", "This is the ground floor hallway, nothing but reception to be found here.");
                room.south = "A1 Stairwell 2F";
                room.north = "Outside South";
                roomMap.Add(room.name, room);
            }

            {
                var room = new Room("Outside South", "Finally I'm outside, now to get to the R&D lab to destroy that data.");
                room.south = "A1 Stairwell 2F";
                roomMap.Add(room.name, room);
            }



            spawnRoom = roomMap["A1 Safe House"];
        }

        public Room SetRoom()
        {
           return spawnRoom;
        }

        public void RoomInfo(Character clientCharacter, Socket UserSocket)
        {
            ASCIIEncoding encoder = new ASCIIEncoding();
            string returnMessage = "";

            returnMessage += (clientCharacter.playerRoom.desc);
            returnMessage += ("Exits");
            for (var i = 0; i < clientCharacter.playerRoom.exits.Length; i++)
            {
                if (clientCharacter.playerRoom.exits[i] != null)
                {
                    returnMessage += (Room.exitNames[i] + " ");
                }
            }

            byte[] sendbuffer = encoder.GetBytes(returnMessage);

            int bytesSent = UserSocket.Send(sendbuffer);

        }

        public Room Process(Character clientCharacter, String key, Socket UserSocket)
        {
            ASCIIEncoding encoder = new ASCIIEncoding();

            byte[] sendbuffer;

            int bytesSent;

            string returnMessage = "";

            var input = key.Split(' ');

            Console.WriteLine(clientCharacter.name + " just left the following room " + clientCharacter.playerRoom.name);

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

                    foreach (Socket player in roomMap[clientCharacter.playerRoom.name].playersInRoom)
                    {
                        if (player != UserSocket)
                        {
                            sendbuffer = encoder.GetBytes(returnMessage);
                            bytesSent = player.Send(sendbuffer);
                        }
                    }
                    Thread.Sleep(1000);

                    break;

                case "go":
                    // is arg[1] sensible?
                    if ((input[1].ToLower() == "north") && (clientCharacter.playerRoom.north != null))
                    {
                        roomMap[clientCharacter.playerRoom.name].playersInRoom.Remove(UserSocket);
                        clientCharacter.playerRoom = roomMap[clientCharacter.playerRoom.north];
                        roomMap[clientCharacter.playerRoom.name].playersInRoom.Add(UserSocket);
                    }
                    else
                    {
                        if ((input[1].ToLower() == "south") && (clientCharacter.playerRoom.south != null))
                        {
                            roomMap[clientCharacter.playerRoom.name].playersInRoom.Remove(UserSocket);
                            clientCharacter.playerRoom = roomMap[clientCharacter.playerRoom.south];
                            roomMap[clientCharacter.playerRoom.name].addplayer(UserSocket);
                        }
                        else
                        {
                            if ((input[1].ToLower() == "east") && (clientCharacter.playerRoom.east != null))
                            {
                                roomMap[clientCharacter.playerRoom.name].playersInRoom.Remove(UserSocket);
                                clientCharacter.playerRoom = roomMap[clientCharacter.playerRoom.east];
                                roomMap[clientCharacter.playerRoom.name].addplayer(UserSocket);
                            }
                            else
                            {
                                if ((input[1].ToLower() == "west") && (clientCharacter.playerRoom.west != null))
                                {
                                    roomMap[clientCharacter.playerRoom.name].playersInRoom.Remove(UserSocket);
                                    clientCharacter.playerRoom = roomMap[clientCharacter.playerRoom.west];
                                    roomMap[clientCharacter.playerRoom.name].addplayer(UserSocket);
                                }
                                else
                                {
                                    if ((input[1].ToLower() == "up") && (clientCharacter.playerRoom.up != null))
                                    {
                                        roomMap[clientCharacter.playerRoom.name].playersInRoom.Remove(UserSocket);
                                        clientCharacter.playerRoom = roomMap[clientCharacter.playerRoom.up];
                                        roomMap[clientCharacter.playerRoom.name].addplayer(UserSocket);
                                    }
                                    else
                                    {
                                        if ((input[1].ToLower() == "down") && (clientCharacter.playerRoom.down != null))
                                        {
                                            roomMap[clientCharacter.playerRoom.name].playersInRoom.Remove(UserSocket);
                                            clientCharacter.playerRoom = roomMap[clientCharacter.playerRoom.down];
                                            roomMap[clientCharacter.playerRoom.name].addplayer(UserSocket);
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

            int playercounter = roomMap[clientCharacter.playerRoom.name].playersInRoom.Count();
            playercounter -= 1;

            returnMessage += "There are " + playercounter + " other people in this room";

           sendbuffer = encoder.GetBytes(returnMessage);

           bytesSent = UserSocket.Send(sendbuffer);

            return clientCharacter.playerRoom;
        }
    }
}
