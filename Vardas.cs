using System.ComponentModel.DataAnnotations.Schema;

namespace KartuvesDatabase
{
    [Table("Vardai")]
    public class Vardas
    {
        public int VardasID { get; set; }
        public string Pavadinimas { get; set; }
        public int KiekSpeta { get; set; }
        public int KiekAtspeta { get; set; }
    }
}
