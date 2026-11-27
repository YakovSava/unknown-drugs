using System.Collections.Generic;
using UnityEngine;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class Goofball : LiquidMixture
    {
        private static readonly MixturePart[] Mixture =
        {
            new MixturePart("pervitin", 0.5f),
            new MixturePart("heroin", 0.5f)
        };

        public override string Id => "goofball";

        public override Color Color => new Color(0.92f, 0.88f, 0.82f);

        public override float ValuePerLiter => 260f;

        public override float InjectionSickness => 0.3f;

        public override float InjectionGangreneChance => 0.1f;

        protected override IReadOnlyList<MixturePart> Parts => Mixture;

        public override DrugText Text { get; } = new DrugText(
            "Goofball",
            "Methamphetamine and heroin in one syringe. The stimulant hides how much the opioid is shutting down your breathing, which is exactly why people do not wake up from it.",
            "Гуфболл",
            "Метамфетамин и героин в одном шприце. Стимулятор скрывает, насколько опиоид уже угнетает дыхание, — именно поэтому с него иногда не просыпаются.");
    }
}
