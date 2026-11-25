using Unity.VisualScripting;
using UnityEngine;
using UnknownDrugs.Core;
using UnknownDrugs.Effects;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class Diazepam : TimedEffectDrug
    {
        private const float SedationPerHundredMl = 250f;
        private const float MaxSlowdown = -0.1f;
        private const float ReboundFear = 25f;
        private const float ReboundSeizureChance = 0.15f;

        public override string Id => "diazepam";

        public override string EffectId => "diazepam";

        public override DrugClass Class => DrugClass.Benzodiazepine;

        public override Color Color => new Color(0.75f, 0.9f, 1f);

        public override float ValuePerLiter => 100f;

        protected override float SecondsPerHundredMl => 150f;

        public override DrugText Text { get; } = new DrugText(
            "Diazepam",
            "Benzodiazepine sold as Valium since 1963. Eases fear, relaxes muscles, stops seizures. Stacks dangerously with opioids and sleep aids.",
            "Диазепам",
            "Бензодиазепин, который с 1963 года продаётся как Валиум. Снимает страх, расслабляет мышцы, останавливает припадки. Опасно складывается с опиоидами и снотворным.");

        protected override void OnImmediate(float ml, Body body)
        {
            body.GetOrAddComponent<SleepingPills>().amount += SedationPerHundredMl * ml * 0.01f;
        }

        protected override void Tick(Body body)
        {
            body.horrifiedLevel = 0f;
            body.traumaAmount = Mathf.Max(0f, body.traumaAmount - 0.3f);
            if (body.stimulantMultiplier > MaxSlowdown)
            {
                body.stimulantMultiplier -= 0.02f;
            }
            CoUtils.instance.Stop(Convulsions.EffectId);
        }

        protected override void OnEnd(Body body)
        {
            body.horrifiedLevel += ReboundFear;
            body.happiness -= 5f;
            if (Random.value < ReboundSeizureChance)
            {
                Convulsions.Trigger(body);
            }
        }
    }
}
