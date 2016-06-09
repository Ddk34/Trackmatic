using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackmatic.SalesTaxes
{
    public class ReceiptView
    {
        private Receipt _receipt;

        public ReceiptView(Receipt receipt)
        {
            _receipt = receipt;
        }

        public void Display(Action<string> display)
        {
            foreach(var good in _receipt)
            {
                display($"{good.Quantity} {good.Description}: {good.Price().ToString("0.00")}");
            }
            display($"Sales Taxes: {_receipt.TotalTax.ToString("0.00")}");
            display($"Total: {_receipt.Total.ToString("0.00")}");
        }
    }
}
