using System.Collections.Generic;
using UnityEngine;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class Tranq : LiquidMixture
    {
        private static readonly MixturePart[] Mixture =
        {
            new MixturePart("fentanyl", 0.1f),
            new MixturePart("xylazine", 0.9f)
        };

        public override string Id => "tranq";

        public override Color Color => new Color(0.8f, 0.78f, 0.62f);

        public override float ValuePerLiter => 800f;

        public override float InjectionSickness => 0.4f;

        public override float InjectionGangreneChance => 0.4f;

        protected override IReadOnlyList<MixturePart> Parts => Mixture;

        public override DrugText Text { get; } = new DrugText(
            "Tranq",
            "Fentanyl cut with veterinary xylazine. Naloxone may wake the opioid part, but the tranquilizer stays in the blood while the skin around the injection begins to die.",
            "Транк",
            "Фентанил, разбавленный ветеринарным ксилазином. Налоксон может разбудить опиоидную часть, но транквилизатор остаётся в крови, пока кожа вокруг укола начинает отмирать.");
    }
}
