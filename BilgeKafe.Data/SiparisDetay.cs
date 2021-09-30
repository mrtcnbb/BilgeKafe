using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeKafe.Data
{
    [Table("SiparisDetaylar")]
    public class SiparisDetay
    {
        // buradaki özellikler null olarak kalabilir, işleyiş açısından sorun yaşanmaz.

        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string UrunAd { get; set; }
        public decimal BirimFİyat { get; set; }
        public int Adet { get; set; }

        //public string TutarTL => $"{Tutar():n2}₺"; ----> altındakinin aynısı (gösterim farkı)
        [NotMapped]
        public string TutarTL { get { return $"{Tutar():n2}₺"; } }
        public int UrunId { get; set; }
        public virtual Urun Urun { get; set; }

        public int SiparisId { get; set; }
        public virtual Siparis Siparis { get; set; }

        public decimal Tutar()
        {
            return Adet * BirimFİyat;
        }
    }
}
