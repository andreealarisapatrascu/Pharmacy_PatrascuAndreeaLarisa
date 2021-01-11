using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PharmacyModel.Models;

namespace PharmacyModel.Data
{
    public class DbInitializer
    {
        public static void Initialize(PharmacyContext context)
        {
            context.Database.EnsureCreated();
            
            if (context.Categorii.Any())
            {
                return; // BD a fost creata anterior
            }
            
            var categorii = new Categorie[]
            {
                new Categorie{NumeCategorie="Durere"},
                new Categorie{NumeCategorie="Răceală şi gripă"},
                new Categorie{NumeCategorie="Cardiovascular"},
                new Categorie{NumeCategorie="Dermatologie"},
                new Categorie{NumeCategorie="Afecţiuni gastrointestinale"},
            };
            
            foreach (Categorie s in categorii)
            {
                context.Categorii.Add(s);
            }
            context.SaveChanges();
            
            var furnizori = new Furnizor[]
            {
                new Furnizor{FurnizorID=1,NumeFurnizor="BAYER SRL",Adresa="Şos Pipera nr 42, etajele: 1,16,17, România",Telefon="0215295900",Email="medical-info-ro@bayer.com"},
                new Furnizor{FurnizorID=2,NumeFurnizor="TERAPIA SA",Adresa="Str. Fabricii nr.124, România",Telefon="0264501500",Email="terapia@yahoo.com"},
                new Furnizor{FurnizorID=3,NumeFurnizor="ACTAVIS Group",Adresa="Reykjavíkurvegi 76-78, ICELAND",Telefon="3545503300",Email="actavis_group@yahoo.com"},
                new Furnizor{FurnizorID=4,NumeFurnizor="RECKITT BENCKISER România SRL",Adresa="Str. Grigore Alexandrescu, nr. 89-97, Et.5, Sector 1, România",Telefon="0215296700",Email="office-romania@rb.com"},
                new Furnizor{FurnizorID=5,NumeFurnizor="S.C. BIOFARM SA",Adresa="Str.Logofatul Tautu nr.99, sector 3, Romania",Telefon="0213010600",Email="office@biofarm.ro"},
            };

            foreach (Furnizor c in furnizori)
            {
                context.Furnizori.Add(c);
            }
            context.SaveChanges();

            var produse = new Produs[]
            {
                new Produs{CategorieID=1,FurnizorID=1,NumeMedicament="ASPIRIN",Doza="20 compr x 500 mg",Pret=0,DataExpirare=DateTime.Parse("2020-12-22")},
                new Produs{CategorieID=1,FurnizorID=3,NumeMedicament="ADAGIN",Doza="10 cps x 400 mg",Pret=0,DataExpirare=DateTime.Parse("2020-12-22")},
                new Produs{CategorieID=2,FurnizorID=4,NumeMedicament="NUROFEN",Doza="100 ml",Pret=0,DataExpirare=DateTime.Parse("2020-12-22")},
                new Produs{CategorieID=3,FurnizorID=2,NumeMedicament="ASPACARDIN",Doza="39mg/12mg x 30 tb",Pret=0,DataExpirare=DateTime.Parse("2020-12-22")},
                new Produs{CategorieID=5,FurnizorID=4,NumeMedicament="GAVISCON MENTOL",Doza="200 ml",Pret=0,DataExpirare=DateTime.Parse("2020-12-22")},
                new Produs{CategorieID=2,FurnizorID=5,NumeMedicament="BIXTONIM Xylo",Doza="10 ml",Pret=0,DataExpirare=DateTime.Parse("2020-12-22")},
                new Produs{CategorieID=2,FurnizorID=2,NumeMedicament="PARACETAMOL",Doza="10 cps X 400 mg",Pret=0,DataExpirare=DateTime.Parse("2020-12-22")},
            };
            
            foreach (Produs e in produse)
            {
                context.Produse.Add(e);
            }
            context.SaveChanges();

            var tipuri = new Tip[]
            {
                new Tip{TipMedicament="Comprimate"},
                new Tip{TipMedicament="Capsule"},
                new Tip{TipMedicament="Sirop"},
                new Tip{TipMedicament="Spray"},
                new Tip{TipMedicament="Picături"},
             };

            foreach (Tip p in tipuri)
            {
                context.Tipuri.Add(p);
            }
            context.SaveChanges();

            var formaproduse = new FormaProdus[]
            {
                new FormaProdus {ProdusID = produse.Single(c => c.NumeMedicament == "ASPIRIN" ).ProdusID,TipID = tipuri.Single(i => i.TipMedicament =="Comprimate").TipID},
                new FormaProdus {ProdusID = produse.Single(c => c.NumeMedicament == "ADAGIN" ).ProdusID,TipID = tipuri.Single(i => i.TipMedicament =="Capsule").TipID},
                new FormaProdus {ProdusID = produse.Single(c => c.NumeMedicament == "NUROFEN" ).ProdusID,TipID = tipuri.Single(i => i.TipMedicament =="Sirop").TipID},
                new FormaProdus {ProdusID = produse.Single(c => c.NumeMedicament == "ASPACARDIN" ).ProdusID,TipID = tipuri.Single(i => i.TipMedicament =="Comprimate").TipID},
                new FormaProdus {ProdusID = produse.Single(c => c.NumeMedicament == "GAVISCON MENTOL" ).ProdusID,TipID = tipuri.Single(i => i.TipMedicament =="Sirop").TipID},
                new FormaProdus {ProdusID = produse.Single(c => c.NumeMedicament == "BIXTONIM Xylo" ).ProdusID,TipID = tipuri.Single(i => i.TipMedicament =="Spray").TipID},
                new FormaProdus {ProdusID = produse.Single(c => c.NumeMedicament == "BIXTONIM Xylo" ).ProdusID,TipID = tipuri.Single(i => i.TipMedicament =="Picături").TipID},
            };

            foreach (FormaProdus pb in formaproduse)
            {
                context.FormaProduse.Add(pb);
            }
            context.SaveChanges();

        }
    }
}
