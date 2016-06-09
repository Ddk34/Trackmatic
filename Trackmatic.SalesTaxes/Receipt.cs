using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackmatic.SalesTaxes
{
    public class Receipt : IEnumerable<Goods>
    {

        private List<Goods> _goods;
        private ISalesTaxCalculator _taxCalculator;
        public decimal Total { get; private set; }
        public decimal TotalTax { get; private set; }

        public Receipt() :
            this(new SalesTaxCalculator())
        { }

        internal Receipt(ISalesTaxCalculator taxCalculator)
        {
            _taxCalculator = taxCalculator;
            _goods = new List<Goods>();
        }

        public void AddRange(IEnumerable<Goods> goods)
        {
            foreach (var good in goods)
                Add(good);
        }

        public void Add(Goods good)
        {
            _goods.Add(good);

            good.CalculateTax(_taxCalculator);

            Total += good.Price();
            TotalTax += good.Tax;
        }

        public IEnumerator<Goods> GetEnumerator()
        {
            return _goods.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _goods.GetEnumerator();
        }
    }
}
