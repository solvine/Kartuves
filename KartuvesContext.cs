using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KartuvesDatabase
{
    public class KartuvesContext : DbContext
    {
        public KartuvesContext() : base("KartuvesDB")
        {
            Database.SetInitializer(new KartuvesInitializer());
        }
        public DbSet<Daiktas> Daiktai { get; set; }
        public DbSet<Vardas> Vardai { get; set; }
        public DbSet<Gyvunas> Gyvunai { get; set; }
        public DbSet<Valstybe> Valstybes { get; set; }
        public DbSet<Miestas> Miestai { get; set; }
        public DbSet<Spejimas> Spejimai { get; set; }
    }
}

