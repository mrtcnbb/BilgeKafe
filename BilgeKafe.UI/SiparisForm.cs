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
        public event EventHandler<MasaTasindiEvenArgs> MasaTasindi;

        private readonly KafeVeri db;
        private readonly Siparis siparis;
        private readonly BindingList<SiparisDetay> blSiparisDetaylar;

        public SiparisForm(KafeVeri db, Siparis siparis)
        {
            this.db = db;
            this.siparis = siparis;
            blSiparisDetaylar = new BindingList<SiparisDetay>(siparis.SiparisDetaylar.ToList());
            blSiparisDetaylar.ListChanged += BlSiparisDetaylar_ListChanged;
            InitializeComponent();
            dgvSiparisDetaylari.AutoGenerateColumns = false; // otomatik sütun oluşturmayı kapattık
            dgvSiparisDetaylari.DataSource = blSiparisDetaylar;
            UrunleriListele();
            MasaNoyuGuncelle();
            MasaNolariListele();
            blSiparisDetaylar.ResetBindings();
            

        }

        private void MasaNolariListele()
        {
            cboMasaNo.DataSource = Enumerable
                .Range(1, 20)
                .Where(i => !db.Siparisler
                .Any(s => s.MasaNo == i && s.Durum == SiparisDurum.Aktif))
                .ToList();
        }

        // bindinglist üzerinde değişiklik yapıldığında 
        private void BlSiparisDetaylar_ListChanged(object sender, ListChangedEventArgs e)
        {
            OdemeTutariniGuncelle();
        }

        private void OdemeTutariniGuncelle()
        {
            lblOdemeTutari.Text = siparis.ToplamTuatarTl;
        }

        private void UrunleriListele()
        {
            cboUrun.DataSource = db.Urunler.ToList();
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
                Adet = adet,
                Urun = urun
                
            };
            siparis.SiparisDetaylar.Add(sd);
            db.SaveChanges();
            blSiparisDetaylar.Add(sd);
        }

        private void btnAnasayfa_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOdemeAl_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show($"{siparis.ToplamTuatarTl} tutarı tahsil edildiyse sipariş kapatılacaktır. Onaylıyor musunuz?", "Ödeme Onayı", MessageBoxButtons.YesNo,MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (dr == DialogResult.Yes)
            {
                SiparisiKapat(SiparisDurum.Odendi);
            }
        }
        private void btnSiparisIptal_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show($"Sipariş iptal edilecektir. Onaylıyot musunuz?", "İptal Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

            if (dr == DialogResult.Yes)
            {
                SiparisiKapat(SiparisDurum.Iptal);
            }
        }

        private void SiparisiKapat(SiparisDurum durum)
        {
            siparis.OdeneneTutar = durum == SiparisDurum.Odendi ? siparis.ToplamTutar() : 0;
            siparis.Durum = durum;
            siparis.KapanisZamani = DateTime.Now;
            db.SaveChanges();
            Close();
        }

        private void btnMasaTasi_Click(object sender, EventArgs e)
        {
            int eskiMasaNo = siparis.MasaNo;
            int yeniMasaNo = (int)cboMasaNo.SelectedItem;
            siparis.MasaNo = yeniMasaNo;
            db.SaveChanges();
            MasaNoyuGuncelle();
            MasaNolariListele();

            MasaTasindiEvenArgs args = new MasaTasindiEvenArgs()
            {
                EskiMasaNo = eskiMasaNo,
                YeniMasaNo = yeniMasaNo
            };


            if (MasaTasindi != null)
            {
                MasaTasindi(this, args);
            }
        }

        private void dgvSiparisDetaylari_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            SiparisDetay sd = (SiparisDetay)e.Row.DataBoundItem;
            db.SiparisDetaylar.Remove(sd);
            db.SaveChanges();
        }
    }
}
