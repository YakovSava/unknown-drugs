using UnityEngine;
using UnknownDrugs.Core;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class Propranolol : Drug
    {
        private const float FibrillationReliefPerHundredMl = 5f;
        private const float PressureDropPerHundredMl = 15f;
        private const float StaminaLossPerHundredMl = 15f;

        public override string Id => "propranolol";

        public override Color Color => new Color(1f, 0.95f, 0.85f);

        public override float ValuePerLiter => 90f;

        public override DrugText Text { get; } = new DrugText(
            "Propranolol",
            "Beta blocker from the 1960s. Quiets a racing heart and trembling hands. Leaves you tired and light-headed.",
            "Пропранолол",
            "Бета-блокатор 1960-х годов. Успокаивает бешеное сердце и дрожь в руках. Оставляет усталость и лёгкое головокружение.");

        public override void Apply(float ml, Body body)
        {
            float hundreds = ml * 0.01f;
            Antidotes.StopEffects(DrugClass.Stimulant);
            body.adrenaline = 0f;
            body.miscShakeIntensity = 0f;
            body.stimulantMultiplier = Mathf.Min(body.stimulantMultiplier, 0f);
            body.fibrillationProgress = Mathf.Max(0f, body.fibrillationProgress - FibrillationReliefPerHundredMl * hundreds);
            body.bloodPressure -= PressureDropPerHundredMl * hundreds;
            body.stamina -= StaminaLossPerHundredMl * hundreds;
        }
    }
}
