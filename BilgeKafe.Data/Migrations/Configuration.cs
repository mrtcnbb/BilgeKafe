namespace BilgeKafe.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BilgeKafe.Data.KafeVeri>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BilgeKafe.Data.KafeVeri context)
        {
            if (!context.Urunler.Any())
            {
                context.Urunler.Add(new Urun() { UrunAd = "Kola", BirimFİyat = 5.99m });
                context.Urunler.Add(new Urun() { UrunAd = "Çay", BirimFİyat = 4.50m });
            }
        }
    }
}
