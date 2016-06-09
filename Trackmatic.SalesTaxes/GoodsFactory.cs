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
        public static IEnumerable<Goods> Parse(IEnumerable<string> items)
        {
            foreach (var item in items)
                yield return Parse(item);
        }

        public static Goods Parse(string item)
        {
            var firstSpace = item.IndexOf(' ');
            var quantity = Convert.ToInt32(item.Substring(0, firstSpace));

            var _at_ = item.IndexOf(" at ");
            firstSpace++;
            var description = item.Substring(firstSpace, _at_ - firstSpace);

            _at_ += " at ".Length;
            var unitPrice = Convert.ToDecimal(item.Substring(_at_, item.Length - _at_));

            return new Goods(GetType(item), description, unitPrice)
            {
                Quantity = quantity,
                Imported = item.Contains("imported")
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
