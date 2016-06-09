using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackmatic.SalesTaxes
{
    public enum GoodsType
    {
        Book,
        Food,
        Medical,
        Other
    }

    public class Goods
    {
        public GoodsType Type { get; private set; }
        public decimal UnitPrice { get; private set; }
        public string Description { get; private set; }
        public decimal Tax { get; private set; }
        public int Quantity { get; set; }
        public bool Imported { get; set; }

        public Goods(GoodsType type, string description, decimal unitPrice)
        {
            Type = type;
            Description = description;
            UnitPrice = unitPrice;
        }

        public decimal Price()
        {
            return (UnitPrice * Quantity) + Tax;
        }

        public void CalculateTax(ISalesTaxCalculator calculator)
        {
            Tax = calculator.Calculate(this) * Quantity;
        }

        public void Combine(Goods good)
        {
            Quantity += good.Quantity;
        }

        public override string ToString()
        {
            return $"{Quantity} {Description} at {UnitPrice}";
        }

        public override int GetHashCode()
        {
            return Type.GetHashCode() ^ Description.GetHashCode() ^ UnitPrice.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Goods);
        }

        public bool Equals(Goods other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Type == other.Type &&
                Description == other.Description &&
                UnitPrice == other.UnitPrice &&
                Imported == other.Imported;
        }

    }
}
