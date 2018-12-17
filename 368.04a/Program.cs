using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _368._04a
{
    class Program
    {

        static Sale sale1 = new Sale("Coffee", "HyperLLC", 3.50, 300, "123 nowhere ave", true);
        static Sale sale2 = new Sale("Coffee", "WinnersLTD", 3.50, 300, "123 anywhere ave", true);
        static Sale sale3 = new Sale("Coffee", "WinnersLTD", 3.50, 400, "123 anywhere ave", false);
        static Sale sale4 = new Sale("Vodka", "HyperLLC", 30, 30, "123 nowhere ave", true);
        static Sale sale5 = new Sale("Vodka", "WinnersLTD", 30, 30, "123 anywhere ave", true);
        static Sale sale6 = new Sale("Vodka", "WinnersLTD", 30, 40, "123 anywhere ave", false);

        public static Sale[] Sales = new Sale[] {sale1, sale2, sale3, sale4, sale5, sale6 };

        public static Double TotalProfit(Sale[] s, Func<Sale, bool> fsb, Func<Sale, double> fsd, Action<Sale, double> asd,
                Action<Sale> asa)
        {
            double tp = 0;
            foreach (Sale sale in s) {
                if (fsb(sale))
                {
                    tp += fsd(sale);
                    asd(sale, fsd(sale));
                }
                else
                    asa(sale);
            }
            return tp;
        }

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


            ////STEP 2////////////////
            Console.WriteLine(TotalProfit(Sales, s => s.Item.Equals("Coffee"), s => s.PricePerItem * s.Quantity * .80,
                (s, s2) => Console.WriteLine($"Coffee item for {s}, total profit: {s2}"), s => Console.WriteLine("Non-Coffee item, Skipping")));

            Console.WriteLine(TotalProfit(Sales, s => s.Quantity > 1, s => s.ExpeditedShipping ? s.PricePerItem * s.Quantity + 20 :
                  s.PricePerItem * s.Quantity,
                (s, s2) =>
                {
                    if (s.ExpeditedShipping)
                        Console.WriteLine($"Expedited Shipping sale of {s} - Extra $20 profit");
                    else
                        Console.WriteLine();
                }, s => System.IO.File.WriteAllText(@"C:\Users\Public\TestFolder\WriteText.txt", $"Single Order Item: {s.Item}")));

        }
    }
}
