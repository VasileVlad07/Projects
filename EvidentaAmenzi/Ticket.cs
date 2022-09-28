using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace EvidentaAmenzi
{
    [Serializable]
    public enum Category
    {
        Bicycle,
        MopedMotorcycle,
        Car,
        Truck,
        Tractor
    }

    [Serializable]
    public class Ticket
    {
        public Category category;
        private uint fine;
        private int agentId;
        private string name;


        public Ticket(int _agentId, string _name, Category _category, uint _fine)
        {
            agentId = _agentId;
            name = _name;
            category = _category;
            fine = _fine;
        }

        public uint Fine
        {
            get { return fine; }
            set { fine = value; }
        }

        public int AgentID
        {
            get { return agentId; }
            set { agentId = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

    }
}
