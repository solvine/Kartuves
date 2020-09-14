using System.ComponentModel.DataAnnotations.Schema;

namespace KartuvesDatabase
{
    [Table("Daiktai")]
    public class Daiktas
    {
        public int DaiktasID { get; set; }
        public string Pavadinimas { get; set; }
        public int KiekSpeta { get; set; }
        public int KiekAtspeta { get; set; }
    }
}
