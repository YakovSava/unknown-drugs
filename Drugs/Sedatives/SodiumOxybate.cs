using UnityEngine;
using UnknownDrugs.Core;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class SodiumOxybate : TimedEffectDrug
    {
        private const string CollapseEffectId = "sodiumoxybatecollapse";
        private const string HypoxiaEffectId = "sodiumoxybatehypoxia";
        private const float CollapseDoseMl = 30f;
        private const float CollapseSecondsPerExcessMl = 2f;
        private const float HypoxiaDoseMl = 120f;
        private const float HypoxiaSecondsPerExcessMl = 0.5f;
        private const float MaxSlowdown = -0.1f;

        public override string Id => "sodiumoxybate";

        public override string EffectId => "sodiumoxybate";

        public override DrugClass Class => DrugClass.Sedative;

        public override bool IsLethalForCans => true;

        public override Color Color => new Color(0.92f, 0.95f, 0.97f);

        public override float ValuePerLiter => 130f;

        protected override float SecondsPerHundredMl => 120f;

        public override DrugText Text { get; } = new DrugText(
            "Sodium oxybate",
            "Sodium salt of gamma-hydroxybutyric acid (GHB), synthesized in 1960 as an anesthetic aid. A small dose relaxes. A slightly larger one switches you off without warning.",
            "Оксибутират натрия",
            "Натриевая соль гамма-оксимасляной кислоты (ГОМК), синтезирована в 1960 году как вспомогательное средство для наркоза. Малая доза расслабляет, чуть большая внезапно выключает.");

        protected override void OnImmediate(float ml, Body body)
        {
            if (ml > CollapseDoseMl)
            {
                float collapse = (ml - CollapseDoseMl) * CollapseSecondsPerExcessMl;
                CoUtils.instance.DoTimedOp(CollapseEffectId, () => Collapse(body), collapse);
            }
            if (ml > HypoxiaDoseMl)
            {
                float hypoxia = (ml - HypoxiaDoseMl) * HypoxiaSecondsPerExcessMl;
                CoUtils.instance.DoTimedOp(HypoxiaEffectId, () => Suffocate(body), hypoxia);
            }
        }

        protected override void Tick(Body body)
        {
            body.happiness += 0.1f;
            if (body.stimulantMultiplier > MaxSlowdown)
            {
                body.stimulantMultiplier -= 0.02f;
            }
            foreach (Limb limb in body.limbs)
            {
                limb.pain = Mathf.Max(0f, limb.pain - 0.2f);
            }
        }

        private static void Collapse(Body body)
        {
            body.consciousness = Mathf.MoveTowards(body.consciousness, 0f, 8f);
        }

        private static void Suffocate(Body body)
        {
            body.bloodOxygen -= 1.5f;
        }
    }
}
