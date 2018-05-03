using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

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
            get { return north; }
            set { north = value; }
        }

        public String south
        {
            get { return north; }
            set { north = value; }
        }

        public String east
        {
            get { return east; }
            set { east = value; }
        }
        public String west
        {
            get { return east; }
            set { east = value; }
        }

        public String up
        {
            get { return up; }
            set { up = value; }
        }

        public String down
        {
            get { return down; }
            set { down = value; }
        }

        public String name = "";
        public String desc = "";
               
    }

}
