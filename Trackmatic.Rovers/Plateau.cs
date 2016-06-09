using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackmatic.Rovers
{
    public class Plateau
    {
        private int _x, _y;

        public Plateau(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public bool CanMove(int x, int y)
        {
            return (x >= 0 && x <= _x) && (y >= 0 && y <= _y);
        }
    }
}
