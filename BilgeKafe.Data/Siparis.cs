using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeKafe.Data
{
    [Table("Siparisler")]
    public class Siparis
    {
        public int Id { get; set; }
        public int MasaNo { get; set; }
        public DateTime? AcilisZamani { get; set; } = DateTime.Now;
        public DateTime? KapanisZamani { get; set; }
        public SiparisDurum Durum { get; set; } = SiparisDurum.Aktif;
        public decimal OdeneneTutar { get; set; }
        public virtual ICollection<SiparisDetay> SiparisDetaylar { get; set; } = new HashSet<SiparisDetay>(); // burada new oluştutulmazsa aşağıdaki linq metodu hata verir, null olmamalı o yüzden.

        [NotMapped]
        public string ToplamTuatarTl => $"{ToplamTutar():n2}₺";

        //public decimal ToplamTutar() => SiparisDetaylar.Sum(sd => sd.Tutar());
        public decimal ToplamTutar()   //-----> yukarıdaki metodun uzun hali
        {
            return SiparisDetaylar.Sum(x => x.Tutar());

            decimal toplam = 0;
            foreach (SiparisDetay detay in SiparisDetaylar)
            {
                toplam += detay.Tutar();
            }
            return toplam;
        }
    }
}
