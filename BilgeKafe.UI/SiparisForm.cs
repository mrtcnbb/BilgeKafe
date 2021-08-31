using BilgeKafe.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BilgeKafe.UI
{
    public partial class SiparisForm : Form
    {
        private readonly KafeVeri db;
        private readonly Siparis siparis;
        private readonly BindingList<SiparisDetay> blSiparisDetaylar;

        public SiparisForm(KafeVeri db, Siparis siparis)
        {
            this.db = db;
            this.siparis = siparis;
            blSiparisDetaylar = new BindingList<SiparisDetay>(siparis.SiparisDetaylar);
            InitializeComponent();
            dgvSiparisDetaylari.DataSource = blSiparisDetaylar;
            UrunleriListeler();
            MasaNoyuGuncelle();

        }

        private void UrunleriListeler()
        {
            cboUrun.DataSource = db.Urunler;
        }

        private void MasaNoyuGuncelle()
        {
            Text = $"Masa {siparis.MasaNo} (Açılış zamanı: {siparis.AcilisZamani}";
            lblMasaNo.Text = $"{siparis.MasaNo:00}";
        }

        private void btnDetayEkle_Click(object sender, EventArgs e)
        {
            Urun urun = (Urun)cboUrun.SelectedItem;
            int adet = (int)nudAdet.Value;

            if (urun == null)
            {
                MessageBox.Show("Önce bir ürün seçmelisiniz.");
                return;
            }

            SiparisDetay sd = new SiparisDetay()
            {
                UrunAd = urun.UrunAd,
                BirimFİyat = urun.BirimFİyat,
                Adet = adet
            };
            blSiparisDetaylar.Add(sd);
            // todo: ödeme tutarını güncelle
            
        }
    }
}
