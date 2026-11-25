using UnityEngine;
using UnknownDrugs.Core;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class Radithor : Drug
    {
        private const float RadiationPerHundredMl = 10f;
        private const float EnergyPerHundredMl = 5f;
        private const float HappinessPerHundredMl = 2f;

        public override string Id => "radithor";

        public override Color Color => new Color(0.7f, 1f, 0.8f);

        public override float ValuePerLiter => 70f;

        public override bool Injectable => false;

        public override bool IsLethalForCans => true;

        public override DrugText Text { get; } = new DrugText(
            "Radithor",
            "Radium water bottled in the 1920s as a tonic for energy and vitality.",
            "Радитор",
            "Радиевая вода, разливавшаяся в 1920-х как тоник для бодрости и жизненных сил.");

        public override void Apply(float ml, Body body)
        {
            float hundreds = ml * 0.01f;
            body.radiationSickness += RadiationPerHundredMl * hundreds;
            body.energy += EnergyPerHundredMl * hundreds;
            body.happiness += HappinessPerHundredMl * hundreds;
        }
    }
}
