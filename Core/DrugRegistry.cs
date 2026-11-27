using System.Collections.Generic;
using System.Linq;
using UnknownDrugs.Drugs;
using UnknownDrugs.Effects;

namespace UnknownDrugs.Core
{
    public static class DrugRegistry
    {
        private static readonly IReadOnlyList<Drug> Drugs = CreateAll();

        public static IReadOnlyList<Drug> All => Drugs;

        public static void Register()
        {
            foreach (Drug drug in Drugs)
            {
                Liquids.Registry[drug.Id] = CreateLiquidType(drug);
                if ((drug.IsLethalForCans || drug.IsCraftOnly)
                    && !Liquids.DangerList.Contains(drug.Id))
                {
                    Liquids.DangerList.Add(drug.Id);
                }
            }
            VanillaSubstanceHook.Install();
            VanillaAlcoholHook.Install();
        }

        private static IReadOnlyList<Drug> CreateAll()
        {
            Drug[] drugs =
            {
                new Methadone(),
                new Mephedrone(),
                new AlphaPvp(),
                new Carfentanil(),
                new Oxylitin(),
                new LiquidCannabis(),
                new Desomorphine(),
                new Oxycodone(),
                new Codeine(),
                new Tramadol(),
                new Buprenorphine(),
                new Nanites(),
                new Pervitin(),
                new Cocaine(),
                new Modafinil(),
                new Nicotine(),
                new Kinetic(),
                new Diazepam(),
                new Phenobarbital(),
                new ChloralHydrate(),
                new SodiumOxybate(),
                new Flumazenil(),
                new Propranolol(),
                new Ketamine(),
                new Scopolamine(),
                new Lsd(),
                new Psilocybin(),
                new Methanol(),
                new Isopropanol(),
                new Antifreeze(),
                new FowlersSolution(),
                new StrychnineTonic(),
                new Radithor(),
                new Trazodone(),
                new Desmopressin(),
                new Leptin(),
                new VasopressinLeptin(),
                new Mptp(),
                new Xylazine(),
                new Goofball(),
                new Tranq(),
                new Krokodil(),
                new Speedball()
            };
            return drugs.Concat(Beverages.Create()).ToList();
        }

        private static LiquidType CreateLiquidType(Drug drug)
        {
            return new LiquidType
            {
                localeName = drug.Id,
                color = drug.Color,
                valuePerLiter = drug.ValuePerLiter,
                injectable = drug.Injectable,
                injectionSickness = drug.InjectionSickness,
                onDrink = (float ml, Body body) =>
                {
                    drug.Apply(ml, body);
                    SubstanceTracker.Mark(body, drug.Id, drug.InteractionSeconds(ml));
                },
                onHealthUse = (float ml, Limb limb) =>
                {
                    drug.Apply(ml, limb.body);
                    SubstanceTracker.Mark(limb.body, drug.Id, drug.InteractionSeconds(ml));
                    Gangrene.Roll(limb, drug.InjectionGangreneChance);
                }
            };
        }
    }
}
