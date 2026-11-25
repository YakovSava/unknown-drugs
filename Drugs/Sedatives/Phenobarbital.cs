using Unity.VisualScripting;
using UnityEngine;
using UnknownDrugs.Core;
using UnknownDrugs.Effects;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class Phenobarbital : TimedEffectDrug
    {
        private const float SedationPerHundredMl = 450f;
        private const float MaxSlowdown = -0.15f;

        public override string Id => "phenobarbital";

        public override string EffectId => "phenobarbital";

        public override DrugClass Class => DrugClass.Sedative;

        public override Color Color => new Color(0.95f, 0.9f, 0.8f);

        public override float ValuePerLiter => 70f;

        protected override float SecondsPerHundredMl => 240f;

        public override DrugText Text { get; } = new DrugText(
            "Phenobarbital",
            "Barbiturate sold as Luminal since 1912. A heavy sleeping aid and a reliable anticonvulsant. Easy to take too much of.",
            "Фенобарбитал",
            "Барбитурат, который с 1912 года продаётся как Люминал. Тяжёлое снотворное и надёжное противосудорожное. Легко принять лишнего.");

        protected override void OnImmediate(float ml, Body body)
        {
            body.GetOrAddComponent<SleepingPills>().amount += SedationPerHundredMl * ml * 0.01f;
        }

        protected override void Tick(Body body)
        {
            if (body.stimulantMultiplier > MaxSlowdown)
            {
                body.stimulantMultiplier -= 0.02f;
            }
            CoUtils.instance.Stop(Convulsions.EffectId);
        }

        protected override void OnEnd(Body body)
        {
            body.sicknessAmount += 3f;
            body.energy -= 10f;
        }
    }
}
