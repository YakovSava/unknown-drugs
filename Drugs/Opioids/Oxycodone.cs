using Unity.VisualScripting;
using UnityEngine;
using UnknownDrugs.Core;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class Oxycodone : Drug
    {
        private const float OpiateUnitsPerHundredMl = 40f;
        private const float ToleranceUnitsPerHundredMl = 10f;
        private const float SicknessPerHundredMl = 4f;

        public override string Id => "oxycodone";

        public override Color Color => new Color(0.95f, 0.8f, 0.6f);

        public override float ValuePerLiter => 120f;

        public override DrugText Text { get; } = new DrugText(
            "Oxycodone",
            "Semi-synthetic opioid first made in 1916. Gentler than morphine, but tolerance builds quickly and the stomach protests.",
            "Оксикодон",
            "Полусинтетический опиоид, впервые полученный в 1916 году. Мягче морфина, но привыкание растёт быстро, а желудок протестует.");

        public override void Apply(float ml, Body body)
        {
            float hundreds = ml * 0.01f;
            Painkillers painkillers = body.GetOrAddComponent<Painkillers>();
            painkillers.opiateAmount += OpiateUnitsPerHundredMl * hundreds;
            painkillers.opiateTolerance += ToleranceUnitsPerHundredMl * hundreds;
            body.sicknessAmount += SicknessPerHundredMl * hundreds;
        }
    }
}
