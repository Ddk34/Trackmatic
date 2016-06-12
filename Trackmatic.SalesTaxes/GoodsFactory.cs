using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Trackmatic.SalesTaxes
{
    public class GoodsFactory
    {
        private static readonly Regex _parse = new Regex(@"^(\d+) ([\w ]+) at (\d+\.\d{2})$");

        public static IEnumerable<Goods> Parse(IEnumerable<string> items)
        {
            foreach (var item in items)
                yield return Parse(item);
        }

        public static Goods Parse(string item)
        {
            var matches = _parse.Matches(item);

            if (matches.Count != 1)
                throw new Exception($"Invalid Goods line format: \"{item}\"");

            if (matches[0].Groups.Count != 4)
                throw new Exception($"Invalid Goods line format: \"{item}\"");

            int quantity = 0;
            if (!int.TryParse(matches[0].Groups[1].Value, out quantity))
                throw new Exception($"Invalid Goods line format: \"{item}\"");

            string description = matches[0].Groups[2].Value;

            decimal unitPrice = 0m;
            
            if (!decimal.TryParse(matches[0].Groups[3].Value, out unitPrice))
                throw new Exception($"Invalid Goods line format: \"{item}\"");

            return new Goods(GetType(description), description, unitPrice)
            {
                Quantity = quantity,
                Imported = description.Contains("imported")
            };
        }

        private static GoodsType GetType(string item)
        {
            if (item.Contains("book"))
                return GoodsType.Book;

            if (item.Contains("chocolate"))
                return GoodsType.Food;

            if (item.Contains("headache pills"))
                return GoodsType.Medical;

            return GoodsType.Other;
        }
    }
}
