using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackmatic.SalesTaxes
{
    public interface ISalesTaxCalculator
    {
        Decimal Calculate(Goods good);
    }
    public class SalesTaxCalculator : ISalesTaxCalculator
    {
        private static readonly GoodsType[] _excludedFromSalesTax = new GoodsType[]
        {
            GoodsType.Book,
            GoodsType.Food,
            GoodsType.Medical
        };
        public Decimal Calculate(Goods good)
        {
            decimal tax = 0.0m;

            if (!_excludedFromSalesTax.Contains(good.Type))
                tax += 0.10m;

            if (good.Imported)
                tax += 0.05m;

            if (tax == 0)
                return 0;

            var unrounded = good.UnitPrice * tax;
            return Math.Ceiling(unrounded * 20) / 20;
        }
    }
}
