using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ButtonMatrix
{
    public class State
    {
        public int I { get; set; }
        public int J { get; set; }
        public int K { get; set; }
        public Color C { get; set; }

        public State(int i, int j, int k, Color c)
        {
            I = i;
            J = j;
            K = k;
            C = c;
        }
    }
}
