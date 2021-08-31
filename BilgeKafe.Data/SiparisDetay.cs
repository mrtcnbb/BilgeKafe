using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeKafe.Data
{
    public class SiparisDetay
    {
        // buradaki özellikler null olarak kalabilir, işleyiş açısından sorun yaşanmaz.

        public string UrunAd { get; set; }
        public decimal BirimFİyat { get; set; }
        public int Adet { get; set; }

        //public string TutarTL => $"{Tutar():n2}₺"; ----> altındakinin aynısı (gösterim farkı)
        public string TutarTL { get { return $"{Tutar():n2}₺"; } }

        public decimal Tutar()
        {
            return Adet * BirimFİyat;
        }
    }
}
