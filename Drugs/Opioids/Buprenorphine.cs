using Unity.VisualScripting;
using UnityEngine;
using UnknownDrugs.Core;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class Buprenorphine : Drug
    {
        private const float EffectCeiling = 45f;
        private const float OpiateUnitsPerHundredMl = 30f;
        private const float WithdrawalShareOfExcess = 0.3f;

        public override string Id => "buprenorphine";

        public override Color Color => new Color(0.75f, 0.85f, 0.95f);

        public override float ValuePerLiter => 110f;

        public override DrugText Text { get; } = new DrugText(
            "Buprenorphine",
            "Partial opioid agonist from the late 1960s. Its effect has a ceiling, so it is hard to overdose on. It also pushes stronger opioids out and can throw you into sudden withdrawal.",
            "Бупренорфин",
            "Частичный агонист опиоидных рецепторов, конец 1960-х. У его действия есть потолок, поэтому передозировать его трудно. Вытесняет более сильные опиоиды и может резко бросить в абстиненцию.");

        public override void Apply(float ml, Body body)
        {
            float hundreds = ml * 0.01f;
            Painkillers painkillers = body.GetOrAddComponent<Painkillers>();
            if (painkillers.opiateAmount > EffectCeiling)
            {
                DisplaceStrongerOpiates(painkillers, hundreds);
                return;
            }
            painkillers.opiateAmount = Mathf.Min(
                EffectCeiling,
                painkillers.opiateAmount + OpiateUnitsPerHundredMl * hundreds);
        }

        private static void DisplaceStrongerOpiates(Painkillers painkillers, float hundreds)
        {
            float excess = painkillers.opiateAmount - EffectCeiling;
            painkillers.opiateTolerance += excess * WithdrawalShareOfExcess;
            painkillers.opiateAmount = Mathf.Lerp(
                painkillers.opiateAmount,
                EffectCeiling,
                Mathf.Clamp01(hundreds));
        }
    }
}
