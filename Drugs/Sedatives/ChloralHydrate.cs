using Unity.VisualScripting;
using UnityEngine;
using UnknownDrugs.Core;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class ChloralHydrate : TimedEffectDrug
    {
        private const float SedationPerHundredMl = 350f;
        private const float SicknessPerHundredMl = 6f;
        private const float VomitChance = 0.3f;

        public override string Id => "chloralhydrate";

        public override string EffectId => "chloralhydrate";

        public override DrugClass Class => DrugClass.Sedative;

        public override Color Color => new Color(0.9f, 0.9f, 0.75f);

        public override float ValuePerLiter => 40f;

        protected override float SecondsPerHundredMl => 150f;

        public override DrugText Text { get; } = new DrugText(
            "Chloral hydrate",
            "Hypnotic used since 1869, the original knockout drops. Sleep comes slowly and the stomach pays for it.",
            "Хлоралгидрат",
            "Снотворное, применяемое с 1869 года, те самые «капли для нокаута». Сон приходит медленно, а расплачивается желудок.");

        protected override void OnImmediate(float ml, Body body)
        {
            float hundreds = ml * 0.01f;
            body.GetOrAddComponent<SleepingPills>().amount += SedationPerHundredMl * hundreds;
            body.sicknessAmount += SicknessPerHundredMl * hundreds;
            if (Random.value < VomitChance)
            {
                body.vomiter.Vomit();
            }
        }

        protected override void Tick(Body body)
        {
            body.consciousness = Mathf.Max(0f, body.consciousness - 0.2f);
        }

        protected override void OnEnd(Body body)
        {
            body.sicknessAmount += 4f;
        }
    }
}
