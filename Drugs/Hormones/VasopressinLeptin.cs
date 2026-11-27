using UnityEngine;
using UnknownDrugs.Core;
using UnknownDrugs.Effects;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class VasopressinLeptin : TimedEffectDrug
    {
        private const float HungerRestorePerSecond = 0.05f;
        private const float ThirstRestorePerSecond = 0.055f;
        private const float EnergyDrainPerSecond = 0.12f;
        private const float BrainDamage = 2f;

        public override string Id => "vasopressinleptin";

        public override string EffectId => "vasopressinleptin";

        public override Color Color => new Color(0.8f, 0.75f, 0.95f);

        public override float ValuePerLiter => 160f;

        public override float InjectionSickness => 0.25f;

        protected override float SecondsPerHundredMl => 500f;

        public override DrugText Text { get; } = new DrugText(
            "Vasopressin-Leptin",
            "A synthetic chimera of vasopressin and leptin that no living body produces. Hunger and thirst fade, water stays inside, and the blood grows dangerously thin of sodium. Fatigue arrives at once.",
            "Вазопрессин-лептин",
            "Синтетическая химера вазопрессина и лептина, которую не производит ни один живой организм. Голод и жажда тускнеют, вода остаётся внутри, а кровь опасно теряет натрий. Усталость приходит сразу.");

        protected override void OnImmediate(float ml, Body body)
        {
            float duration = SecondsPerHundredMl * ml * 0.01f;
            Hyponatremia.Apply(body, duration, BrainDamage);
            body.energy -= 5f;
        }

        protected override void Tick(Body body)
        {
            body.hunger += HungerRestorePerSecond;
            body.thirst += ThirstRestorePerSecond;
            body.energy -= EnergyDrainPerSecond;
        }
    }
}
