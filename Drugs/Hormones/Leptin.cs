using UnityEngine;
using UnknownDrugs.Core;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class Leptin : TimedEffectDrug
    {
        private const float HungerRestorePerSecond = 0.06f;
        private const float EnergyDrainPerSecond = 0.18f;
        private const float ImmediateHungerPerHundredMl = 12f;

        public override string Id => "leptin";

        public override string EffectId => "leptin";

        public override Color Color => new Color(0.95f, 0.9f, 0.7f);

        public override float ValuePerLiter => 75f;

        public override float InjectionSickness => 0.2f;

        protected override float SecondsPerHundredMl => 300f;

        public override DrugText Text { get; } = new DrugText(
            "Leptin",
            "The satiety hormone, discovered in 1994. Hunger disappears almost at once. So does your energy, and it does not come back until the hormone wears off.",
            "Лептин",
            "Гормон сытости, открытый в 1994 году. Голод исчезает почти сразу. Вместе с ним исчезает и энергия, и она не вернётся, пока гормон не перестанет действовать.");

        protected override void OnImmediate(float ml, Body body)
        {
            body.hunger += ImmediateHungerPerHundredMl * ml * 0.01f;
        }

        protected override void Tick(Body body)
        {
            body.hunger += HungerRestorePerSecond;
            body.energy -= EnergyDrainPerSecond;
        }
    }
}
