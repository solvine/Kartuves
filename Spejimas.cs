using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KartuvesDatabase
{
    [Table("Spejimai")]
    public class Spejimas
    {
        public int SpejimasId { get; set; }
        public string ZaidejoVardas { get; set; }

        public string SpetasZodis { get; set; }
        public int KiekKartuSpejo { get; set; }

        public string Spejimai { get; set; } // kokias raides, zodzius spejo
        public bool ArAtspejo { get; set; }

        public DateTime ZaidimoData { get; set; }

    }
}
