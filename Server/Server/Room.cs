using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    public class Room
    {
        public Room(String name, String desc)
        {
            this.desc = desc;
            this.name = name;
        }

        public String north
        {
            get { return exits[0]; }
            set { exits[0] = value; }
        }

        public String south
        {
            get { return exits[1]; }
            set { exits[1] = value; }
        }

        public String east
        {
            get { return exits[2]; }
            set { exits[2] = value; }
        }
        public String west
        {
            get { return exits[3]; }
            set { exits[3] = value; }
        }

        public String up
        {
            get { return exits[4]; }
            set { exits[4] = value; }
        }

        public String down
        {
            get { return exits[5]; }
            set { exits[5] = value; }
        }


        public String name = "";
        public String desc = "";
        public String[] exits = new String[6];
        public static String[] exitNames = { "NORTH", "SOUTH", "EAST", "WEST", "UP", "DOWN" };
    }

}
