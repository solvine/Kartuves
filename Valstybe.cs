using System.ComponentModel.DataAnnotations.Schema;

namespace KartuvesDatabase
{
    [Table("Valstybes")]
    public class Valstybe
    {
        public int ValstybeID { get; set; }
        public string Pavadinimas { get; set; }
        public int KiekSpeta { get; set; }
        public int KiekAtspeta { get; set; }
    }
}
