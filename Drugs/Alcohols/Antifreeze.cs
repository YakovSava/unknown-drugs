using Unity.VisualScripting;
using UnityEngine;
using UnknownDrugs.Core;
using UnknownDrugs.Effects;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class Antifreeze : Drug
    {
        private const float BuzzStrength = 0.1f;

        public override string Id => "antifreeze";

        public override Color Color => new Color(0.3f, 0.9f, 0.5f);

        public override float ValuePerLiter => 12f;

        public override bool Injectable => false;

        public override bool IsLethalForCans => true;

        public override DrugText Text { get; } = new DrugText(
            "Antifreeze",
            "Engine coolant with a faintly sweet taste.",
            "Антифриз",
            "Охлаждающая жидкость для двигателей со слегка сладким вкусом.");

        public override void Apply(float ml, Body body)
        {
            body.GetOrAddComponent<Intoxication>().Add(Intoxication.PointsFor(ml, BuzzStrength));
            body.GetOrAddComponent<GlycolPoisoning>().Ingest(ml);
        }
    }
}
