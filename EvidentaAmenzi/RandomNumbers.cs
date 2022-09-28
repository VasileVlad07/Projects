using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidentaAmenzi
{
    public static class RandomNumbers
    {
        static Random random = new Random();

        public static int GetNext()
        {
            return random.Next(1, 999);
        }
    }
}
