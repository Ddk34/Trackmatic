using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackmatic.Rovers
{
    public class Orientation
    {
        public enum Compass
        {
            N = 0,
            E = 1,
            S = 2,
            W = 3
        }

        private int _orientation;

        public Compass Facing
        {
            get
            {
                return (Compass)_orientation;
            }
        }

        public Orientation(char orientation)
        {
            _orientation = (int)Enum.Parse(typeof(Compass), orientation.ToString());
        }

        public Orientation(Compass orientation)
        {
            _orientation = (int)orientation;
        }

        public Compass Turn(char move)
        {
            switch(move)
            {
                case 'L':
                    _orientation -= 1;
                    break;
                case 'R':
                    _orientation += 1;
                    break;
                default:
                    throw new InvalidOperationException($"Unknown move: {move}");
            }

            if (_orientation < 0)
                _orientation += 4;

            if (_orientation > 3)
                _orientation -= 4;

            return Facing;
        }

        public override string ToString()
        {
            return Facing.ToString();
        }

        public static implicit operator Orientation(char orientation)
        {
            return new Orientation(orientation);
        }

        public static implicit operator Orientation(Compass orientation)
        {
            return new Orientation(orientation);
        }
    }
}
