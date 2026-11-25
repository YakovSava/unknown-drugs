using UnityEngine;
using UnknownDrugs.Core;
using UnknownDrugs.Effects;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class Ketamine : TimedEffectDrug
    {
        private const float DistortionLevel = 0.5f;
        private const float PainRelief = 1.2f;
        private const float EmergenceFear = 20f;
        private const float EmergenceVomitChance = 0.3f;

        public override string Id => "ketamine";

        public override string EffectId => "ketamine";

        public override Color Color => new Color(0.95f, 0.95f, 1f);

        public override float ValuePerLiter => 180f;

        protected override float SecondsPerHundredMl => 90f;

        public override DrugText Text { get; } = new DrugText(
            "Ketamine",
            "Dissociative anesthetic synthesized in 1962 and widely used on battlefields. Strong pain relief that leaves breathing alone, while the body goes limp and the mind drifts elsewhere.",
            "Кетамин",
            "Диссоциативный анестетик, синтезированный в 1962 году и широко применявшийся на полях сражений. Сильное обезболивание, не трогающее дыхание, пока тело обмякает, а разум уплывает.");

        protected override void Tick(Body body)
        {
            body.Ragdoll();
            ScreenDistortion.Push(body, DistortionLevel);
            body.happiness += 0.1f;
            foreach (Limb limb in body.limbs)
            {
                limb.pain = Mathf.Max(0f, limb.pain - PainRelief);
            }
        }

        protected override void OnEnd(Body body)
        {
            body.horrifiedLevel += EmergenceFear;
            body.sicknessAmount += 5f;
            body.happiness -= 5f;
            if (Random.value < EmergenceVomitChance)
            {
                body.vomiter.Vomit();
            }
        }
    }
}
