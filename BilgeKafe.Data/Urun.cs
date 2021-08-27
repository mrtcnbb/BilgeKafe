using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeKafe.Data
{
    public class Urun
    {
        public string UrunAd { get; set; }
        public decimal BirimFİyat { get; set; }

        public override string ToString()
        {
            return $"{UrunAd} - {BirimFİyat:n2}₺";
        }
    }
}
