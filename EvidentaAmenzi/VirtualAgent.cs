using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace EvidentaAmenzi
{
    [Serializable]
    public class VirtualAgent
    {
        public Category category;
        private uint fine;

        public VirtualAgent(Category _category, uint _fine)
        {
            category = _category;
            fine = _fine;
        }

        public uint Fine
        {
            get { return fine; }
            set { fine = value; }
        }

    }
}
