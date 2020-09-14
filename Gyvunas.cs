using System.ComponentModel.DataAnnotations.Schema;

namespace KartuvesDatabase
{
    [Table("Gyvunai")]
    public class Gyvunas
    {
        public int GyvunasID { get; set; }
        public string Pavadinimas { get; set; }
        public int KiekSpeta { get; set; }
        public int KiekAtspeta { get; set; }
    }
}
