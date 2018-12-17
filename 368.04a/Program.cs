using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _368._04a
{
    class Program
    {
        public static Sale[] Sales = new Sale[] { };

        static void Main(string[] args)
        {
            //query
            var ppiG10 = from csale in Sales where csale.PricePerItem > 10.0 select csale;
            var quant1 = from csale in Sales
                         where csale.Quantity == 1
                         orderby csale.PricePerItem descending
                         select csale;
            var teaFalse = from csale in Sales
                           where csale.Item.Equals("Tea") && !csale.ExpeditedShipping
                           select csale;
            

            var totalAdress = from csale in Sales
                              where (csale.PricePerItem * csale.Quantity) > 100.00
                              select csale.Address;
            var newSale = from csale in Sales
                          where csale.Customer.ToLower().Contains("LLC".ToLower())
                          let n = new
                          {
                              Item = csale.Item,
                              TotalPrice = csale.PricePerItem * csale.Quantity,
                              Adress = csale.ExpeditedShipping ? csale.Address + " EXPEDITE" : csale.Address
                          }
                          orderby n.TotalPrice
                          select n;








            //Extension
            var ppiG102 = Sales.Where(s => s.PricePerItem > 10.0);
            var quant12 = Sales.Where(s => s.Quantity == 1).OrderByDescending(s => s.PricePerItem);
            var teaFalse2 = Sales.Where(s => s.Item.Equals("Tea") && !s.ExpeditedShipping);
            var totalAdress2 = Sales.Where(s => (s.Quantity * s.PricePerItem) > 100.00)
                .Select(s => s.Address);
            var newSale2 = Sales.Where(s => s.Customer.ToLower().Contains("LLC".ToLower())).Select(s => new
            {
                Item = s.Item,
                TotalPrice = s.PricePerItem * s.Quantity,
                Adress = s.ExpeditedShipping ? s.Address + " EXPEDITE" : s.Address
            }).OrderBy(s2 => s2.TotalPrice);

        }
    }
}
