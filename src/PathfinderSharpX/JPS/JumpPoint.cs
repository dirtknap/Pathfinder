using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathfinderSharpX.Commons;

namespace PathfinderSharpX.JPS
{
    class JumpPoint : Point
    {
        public float Huristic { get; set; }


        public JumpPoint(int x, int y) : base(x, y)
        {
        }
    }
}
