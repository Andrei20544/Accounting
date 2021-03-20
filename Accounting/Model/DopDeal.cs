using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Model
{
    class DopDeal
    {
        public string FIORieltor { get; set; }
        public double? SquareApartament { get; set; }
        public decimal? Price { get; set; }
        public int? Percent { get; set; }
        public int IDAP { get; set; }
        public int IDD { get; set; }
        public int IDR { get; set; }

        public DopDeal(string fIORieltor, double? squareApartament, decimal? price, int? percent, int idap, int idd, int idr)
        {
            FIORieltor = fIORieltor;
            SquareApartament = squareApartament;
            Price = price;
            Percent = percent;
            IDAP = idap;
            IDD = idd;
            IDR = idr;
        }
    }
}
