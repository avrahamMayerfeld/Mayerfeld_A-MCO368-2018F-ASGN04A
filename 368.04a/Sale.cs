using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _368._04a
{
    public class Sale
    {
        private string v1;
        private string v2;
        private double v3;
        private int v4;
        private string v5;
        private bool v6;

        public Sale(string v1, string v2, double v3, int v4, string v5, bool v6)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
            this.v4 = v4;
            this.v5 = v5;
            this.v6 = v6;
        }

        public String Item { get; set; }
        public String Customer { get; set; }
        public double PricePerItem { get; set; }
        public int Quantity { get; set; }
        public String Address { get; set; }
        public bool ExpeditedShipping { get; set; }
        


    }
}
