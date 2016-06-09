using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackmatic.Rovers
{
    class Program
    {
        static void Main(string[] args)
        {
            var plateau = new Plateau(5, 5);

            var rover1 = new Rover(plateau, 1, 2, 'N');
            rover1.Move("LMLMLMLMM");

            var rover2 = new Rover(plateau, 3, 3, 'E');
            rover2.Move("MMRMMRMRRM");

            Console.WriteLine(rover1);
            Console.WriteLine(rover2);

            Console.ReadLine();
        }
    }
}
