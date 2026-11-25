using Unity.VisualScripting;
using UnityEngine;
using UnknownDrugs.Core;
using UnknownDrugs.Effects;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class Isopropanol : Drug
    {
        private const float Strength = 0.7f;
        private const float SicknessPerMl = 0.1f;
        private const float BrainDamagePerMl = 0.01f;
        private const float VomitChance = 0.3f;

        public override string Id => "isopropanol";

        public override Color Color => new Color(0.95f, 0.97f, 1f);

        public override float ValuePerLiter => 18f;

        public override bool Injectable => false;

        public override DrugText Text { get; } = new DrugText(
            "Isopropyl alcohol",
            "Rubbing alcohol for wiping wounds and instruments.",
            "Изопропиловый спирт",
            "Спирт для обработки ран и инструментов.");

        public override void Apply(float ml, Body body)
        {
            body.GetOrAddComponent<Intoxication>().Add(Intoxication.PointsFor(ml, Strength));
            body.sicknessAmount += ml * SicknessPerMl;
            body.brainHealth -= ml * BrainDamagePerMl;
            if (Random.value < VomitChance)
            {
                body.vomiter.Vomit();
            }
        }
    }
}
