using Unity.VisualScripting;
using UnityEngine;
using UnknownDrugs.Core;
using UnknownDrugs.Effects;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class Methanol : Drug
    {
        private const float BuzzStrength = 0.1f;

        public override string Id => "methanol";

        public override Color Color => new Color(0.96f, 0.98f, 1f);

        public override float ValuePerLiter => 10f;

        public override bool Injectable => false;

        public override bool IsLethalForCans => true;

        public override DrugText Text { get; } = new DrugText(
            "Methanol",
            "Industrial solvent. Clear, with a faint smell of alcohol.",
            "Метанол",
            "Промышленный растворитель. Прозрачный, с лёгким запахом спирта.");

        public override void Apply(float ml, Body body)
        {
            body.GetOrAddComponent<Intoxication>().Add(Intoxication.PointsFor(ml, BuzzStrength));
            body.GetOrAddComponent<MethanolPoisoning>().Ingest(ml);
        }
    }
}
