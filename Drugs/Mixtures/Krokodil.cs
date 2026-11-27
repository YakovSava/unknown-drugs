using System.Collections.Generic;
using UnityEngine;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class Krokodil : LiquidMixture
    {
        private static readonly MixturePart[] Mixture =
        {
            new MixturePart("desomorphine", 0.5f),
            new MixturePart("bleach", 0.5f)
        };

        public override string Id => "krokodil";

        public override Color Color => new Color(0.55f, 0.6f, 0.35f);

        public override float ValuePerLiter => 40f;

        public override float InjectionSickness => 1f;

        public override float InjectionGangreneChance => 0.9f;

        protected override IReadOnlyList<MixturePart> Parts => Mixture;

        public override DrugText Text { get; } = new DrugText(
            "Krokodil",
            "Desomorphine mixed with bleach. The injection site dies almost at once, clots travel through the blood, and sepsis follows the necrosis.",
            "Крокодил",
            "Дезоморфин, смешанный с отбеливателем. Место укола умирает почти сразу, по крови идут тромбы, а за некрозом приходит сепсис.");
    }
}
