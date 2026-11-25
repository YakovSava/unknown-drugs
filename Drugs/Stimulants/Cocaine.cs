using UnityEngine;
using UnknownDrugs.Core;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class Cocaine : TimedEffectDrug
    {
        private const float SafeDoseMl = 40f;
        private const float DoseRangeToCertainFibrillationMl = 120f;
        private const float MaxStimulantBonus = 0.12f;

        public override string Id => "cocaine";

        public override string EffectId => "cocaine";

        public override DrugClass Class => DrugClass.Stimulant;

        public override Color Color => new Color(0.97f, 0.97f, 0.97f);

        public override float ValuePerLiter => 150f;

        public override float InjectionSickness => 0.3f;

        protected override float SecondsPerHundredMl => 60f;

        public override DrugText Text { get; } = new DrugText(
            "Cocaine solution",
            "In 1884 Karl Koller used it as the first local anesthetic for eye surgery. Short euphoria, numbed pain, and a heart that does not like big doses.",
            "Раствор кокаина",
            "В 1884 году Карл Коллер применил его как первый местный анестетик при операции на глазу. Короткая эйфория, приглушённая боль и сердце, которое не любит больших доз.");

        protected override void OnImmediate(float ml, Body body)
        {
            float chance = Mathf.Clamp01((ml - SafeDoseMl) / DoseRangeToCertainFibrillationMl);
            if (Random.value < chance)
            {
                body.TryStartFibrillation();
            }
        }

        protected override void Tick(Body body)
        {
            body.adrenaline += 5f;
            body.stamina += 2f;
            body.happiness += 0.15f;
            if (body.stimulantMultiplier < MaxStimulantBonus)
            {
                body.stimulantMultiplier += 0.03f;
            }
            foreach (Limb limb in body.limbs)
            {
                limb.pain = Mathf.Max(0f, limb.pain - 0.5f);
            }
        }

        protected override void OnEnd(Body body)
        {
            body.happiness -= 8f;
            body.energy -= 10f;
        }
    }
}
