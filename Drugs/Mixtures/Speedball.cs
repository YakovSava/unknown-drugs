using System.Collections.Generic;
using UnityEngine;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class Speedball : LiquidMixture
    {
        private static readonly MixturePart[] Mixture =
        {
            new MixturePart("cocaine", 0.5f),
            new MixturePart("desomorphine", 0.5f)
        };

        public override string Id => "speedball";

        public override Color Color => new Color(0.96f, 0.94f, 0.9f);

        public override float ValuePerLiter => 260f;

        public override float InjectionSickness => 0.25f;

        public override float InjectionGangreneChance => 0.2f;

        protected override IReadOnlyList<MixturePart> Parts => Mixture;

        public override DrugText Text { get; } = new DrugText(
            "Speedball",
            "Cocaine and a short-acting opioid together. The stimulant masks the overdose until the heart or the breathing gives out first.",
            "Спидболл",
            "Кокаин и короткодействующий опиоид вместе. Стимулятор маскирует передозировку, пока первым не сдастся сердце или дыхание.");
    }
}
