using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace EvidentaAmenzi
{
    [Serializable]
    public class Agent
    { 
        private string name;
        private int code;  

        public Agent(int _code, string _name)
        {
            code = _code;
            name = _name;
        }

        public int Code
        {
            get { return code; }
            set { code = value; }   
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

    }
}
