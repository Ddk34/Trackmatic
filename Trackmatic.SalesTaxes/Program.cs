using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackmatic.SalesTaxes
{
    class Program
    {
        static void Input1()
        {
            Input("1 book at 12.49",
                "1 music CD at 14.99",
                "1 chocolate bar at 0.85");
        }

        static void Input2()
        {
            Input("1 imported box of chocolates at 10.00",
                "1 imported bottle of perfume at 47.50");
        }

        static void Input3()
        {
            Input("1 imported bottle of perfume at 27.99",
                "1 bottle of perfume at 18.99",
                "1 packet of headache pills at 9.75",
                "1 box of imported chocolates at 11.25");
        }

        static void Input(params string[] items)
        {
            var receipt = new Receipt();

            receipt.AddRange(GoodsFactory.Parse(items));

            var view = new ReceiptView(receipt);
            view.Display(Console.WriteLine);
        }

        static void Main(string[] args)
        {
            Input1();
            Console.WriteLine();
            Input2();
            Console.WriteLine();
            Input3();
            Console.ReadLine();
        }
    }
}
