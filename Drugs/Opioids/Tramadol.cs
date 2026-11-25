using Unity.VisualScripting;
using UnityEngine;
using UnknownDrugs.Core;
using UnknownDrugs.Effects;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class Tramadol : Drug
    {
        private const float OpiateUnitsPerHundredMl = 20f;
        private const float SafeDoseMl = 50f;
        private const float DoseRangeToCertainSeizureMl = 150f;

        public override string Id => "tramadol";

        public override Color Color => new Color(0.9f, 0.9f, 0.7f);

        public override float ValuePerLiter => 70f;

        public override DrugText Text { get; } = new DrugText(
            "Tramadol",
            "Synthetic opioid developed in Germany in 1962. Weak painkiller, but a large dose can cause a seizure.",
            "Трамадол",
            "Синтетический опиоид, разработанный в Германии в 1962 году. Слабое обезболивающее, но большая доза может вызвать припадок.");

        public override void Apply(float ml, Body body)
        {
            float hundreds = ml * 0.01f;
            body.GetOrAddComponent<Painkillers>().opiateAmount += OpiateUnitsPerHundredMl * hundreds;
            if (Random.value < SeizureChance(ml))
            {
                Convulsions.Trigger(body);
            }
        }

        private static float SeizureChance(float ml)
        {
            return Mathf.Clamp01((ml - SafeDoseMl) / DoseRangeToCertainSeizureMl);
        }
    }
}
