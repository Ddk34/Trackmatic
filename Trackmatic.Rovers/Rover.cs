using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackmatic.Rovers
{
    public class Rover
    {
        private Plateau _plateau;
        private Orientation _orientation;
        private int _x, _y;
        
        public Rover(Plateau plateau, int x, int y, Orientation orientation)
        {
            _plateau = plateau;
            _x = x;
            _y = y;
            _orientation = orientation;
        }

        private void Move()
        {
            int tempX = _x, tempY = _y;

            switch (_orientation.Facing)
            {
                case Orientation.Compass.N:
                    tempY += 1;
                    break;
                case Orientation.Compass.S:
                    tempY -= 1;
                    break;
                case Orientation.Compass.W:
                    tempX -= 1;
                    break;
                case Orientation.Compass.E:
                    tempX += 1;
                    break;
            }

            if (!_plateau.CanMove(tempX, tempY))
                throw new InvalidOperationException($"Cant move to points {tempX},{tempY} as out of bounds");

            _x = tempX;
            _y = tempY;
        }

        public void Move(string moves)
        {
            foreach (var move in moves)
                Move(move);
        }

        public void Move(char move)
        {
            switch(move)
            {
                case 'M':
                    Move();
                    break;
                case 'L':
                    _orientation.Turn(move);
                    break;
                case 'R':
                    _orientation.Turn(move);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown move: {move}");
            }
        }

        public override string ToString()
        {
            return $"{_x} {_y} {_orientation.Facing}";
        }
    }
}
