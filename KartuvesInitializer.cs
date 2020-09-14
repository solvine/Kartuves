using System.Data.Entity;

namespace KartuvesDatabase
{
    public class KartuvesInitializer : CreateDatabaseIfNotExists<KartuvesContext>
    {
        protected override void Seed(KartuvesContext context)
        {
            context.Daiktai.Add(new Daiktas { Pavadinimas = "LAIKRODIS" });
            context.Daiktai.Add(new Daiktas { Pavadinimas = "TUSINUKAS" });
            context.Daiktai.Add(new Daiktas { Pavadinimas = "TRINTUKAS" });
            context.Daiktai.Add(new Daiktas { Pavadinimas = "KUPRINE" });
            context.Daiktai.Add(new Daiktas { Pavadinimas = "STALAS" });
            context.Daiktai.Add(new Daiktas { Pavadinimas = "TELEVIZORIUS" });
            context.Daiktai.Add(new Daiktas { Pavadinimas = "KEDE" });
            context.Daiktai.Add(new Daiktas { Pavadinimas = "SOFA" });
            context.Daiktai.Add(new Daiktas { Pavadinimas = "LANGAS" });
            context.Daiktai.Add(new Daiktas { Pavadinimas = "DURYS" });

            context.Gyvunai.Add(new Gyvunas { Pavadinimas = "ZIRAFA" });
            context.Gyvunai.Add(new Gyvunas { Pavadinimas = "STRUTIS" });
            context.Gyvunai.Add(new Gyvunas { Pavadinimas = "SUO" });
            context.Gyvunai.Add(new Gyvunas { Pavadinimas = "KATE" });
            context.Gyvunai.Add(new Gyvunas { Pavadinimas = "KROKODILAS" });
            context.Gyvunai.Add(new Gyvunas { Pavadinimas = "BEGEMOTAS" });
            context.Gyvunai.Add(new Gyvunas { Pavadinimas = "TIGRAS" });
            context.Gyvunai.Add(new Gyvunas { Pavadinimas = "LIUTAS" });
            context.Gyvunai.Add(new Gyvunas { Pavadinimas = "ZUVIS" });
            context.Gyvunai.Add(new Gyvunas { Pavadinimas = "LEOPARDAS" });

            context.Miestai.Add(new Miestas {Pavadinimas = "VILNIUS" });
            context.Miestai.Add(new Miestas {Pavadinimas = "KAUNAS" });
            context.Miestai.Add(new Miestas {Pavadinimas = "SIAULIAI" });
            context.Miestai.Add(new Miestas {Pavadinimas = "PANEVEZYS" });
            context.Miestai.Add(new Miestas {Pavadinimas = "KLAIPEDA" });
            context.Miestai.Add(new Miestas {Pavadinimas = "TELSIAI" });
            context.Miestai.Add(new Miestas {Pavadinimas = "PLUNGE" });
            context.Miestai.Add(new Miestas {Pavadinimas = "PALANGA" });
            context.Miestai.Add(new Miestas {Pavadinimas = "KRETINGA" });
            context.Miestai.Add(new Miestas {Pavadinimas = "SILUTE" });

            context.Valstybes.Add(new Valstybe {Pavadinimas = "LIETUVA" });
            context.Valstybes.Add(new Valstybe {Pavadinimas = "LATVIJA" });
            context.Valstybes.Add(new Valstybe {Pavadinimas = "ESTIJA" });
            context.Valstybes.Add(new Valstybe {Pavadinimas = "SVEDIJA" });
            context.Valstybes.Add(new Valstybe {Pavadinimas = "NORVEGIJA" });
            context.Valstybes.Add(new Valstybe {Pavadinimas = "SUOMIJA" });
            context.Valstybes.Add(new Valstybe {Pavadinimas = "RUSIJA" });
            context.Valstybes.Add(new Valstybe {Pavadinimas = "LENKIJA" });
            context.Valstybes.Add(new Valstybe {Pavadinimas = "UKRAINA" });
            context.Valstybes.Add(new Valstybe {Pavadinimas = "BALTARUSIJA" });

            context.Vardai.Add(new Vardas {Pavadinimas = "GIEDRE" });
            context.Vardai.Add(new Vardas {Pavadinimas = "TOMAS" });
            context.Vardai.Add(new Vardas {Pavadinimas = "VIKTORAS" });
            context.Vardai.Add(new Vardas {Pavadinimas = "ARVYDA" });
            context.Vardai.Add(new Vardas {Pavadinimas = "SNIEGA" });
            context.Vardai.Add(new Vardas {Pavadinimas = "SAULE" });
            context.Vardai.Add(new Vardas {Pavadinimas = "RAIMONDAS" });
            context.Vardai.Add(new Vardas {Pavadinimas = "ROMUALDAS" });
            context.Vardai.Add(new Vardas {Pavadinimas = "VYTAUTAS" });
            context.Vardai.Add(new Vardas {Pavadinimas = "DOVILE" });

        }
    }
}
