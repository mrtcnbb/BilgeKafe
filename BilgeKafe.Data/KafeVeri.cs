﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeKafe.Data
{
    public class KafeVeri
    {
        public int MasaAdet { get; set; } = 20; // default olarak giriyoruz
        public List<Urun> Urunler { get; set; } = new List<Urun>();
        public List<Siparis> AktifSiparisler { get; set; } = new List<Siparis>();
        public List<Siparis> GecmisSiparisler { get; set; } = new List<Siparis>();
    }
}
