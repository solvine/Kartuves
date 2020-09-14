using System.ComponentModel.DataAnnotations.Schema;

namespace KartuvesDatabase
{
    [Table("Miestai")]
    public class Miestas
    {
        public int MiestasID { get; set; }
        public string Pavadinimas { get; set; }

        public int KiekSpeta { get; set; }
        public int KiekAtspeta { get; set; }

    }
}
