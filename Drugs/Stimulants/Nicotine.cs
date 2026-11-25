using UnityEngine;
using UnknownDrugs.Core;
using UnknownDrugs.Effects;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class Nicotine : TimedEffectDrug
    {
        private const float HarmfulDoseMl = 10f;
        private const float ConvulsiveDoseMl = 20f;
        private const float FibrillationDoseMl = 40f;
        private const float SicknessPerMl = 0.6f;
        private const float ShakeChance = 0.05f;

        public override string Id => "nicotine";

        public override string EffectId => "nicotine";

        public override DrugClass Class => DrugClass.Stimulant;

        public override bool IsLethalForCans => true;

        public override bool Injectable => false;

        public override Color Color => new Color(0.9f, 0.85f, 0.6f);

        public override float ValuePerLiter => 35f;

        protected override float SecondsPerHundredMl => 40f;

        public override DrugText Text { get; } = new DrugText(
            "Liquid nicotine",
            "Concentrated nicotine solution. A few drops steady the nerves.",
            "Жидкий никотин",
            "Концентрированный раствор никотина. Пара капель успокаивает нервы.");

        protected override void OnImmediate(float ml, Body body)
        {
            if (ml <= HarmfulDoseMl)
            {
                return;
            }
            body.sicknessAmount += ml * SicknessPerMl;
            body.vomiter.Vomit();
            if (ml > ConvulsiveDoseMl)
            {
                Convulsions.Trigger(body);
            }
            if (ml > FibrillationDoseMl)
            {
                body.TryStartFibrillation(forced: true);
            }
        }

        protected override void Tick(Body body)
        {
            body.adrenaline += 2f;
            body.happiness += 0.05f;
            body.stamina += 0.3f;
            if (Random.value < ShakeChance)
            {
                body.miscShakeIntensity += 1f;
            }
        }

        protected override void OnEnd(Body body)
        {
            body.happiness -= 3f;
        }
    }
}
