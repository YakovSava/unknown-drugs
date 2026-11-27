using Unity.VisualScripting;
using UnityEngine;
using UnknownDrugs.Core;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class Xylazine : TimedEffectDrug
    {
        private const float SedationPerHundredMl = 140f;
        private const float Slowdown = 0.35f;
        private const float MaxRespiratoryRate = 70f;
        private const float HeartRateDropPerSecond = 1.2f;
        private const float VomitChance = 0.002f;

        public override string Id => "xylazine";

        public override string EffectId => "xylazine";

        public override DrugClass Class => DrugClass.Sedative;

        public override Color Color => new Color(0.85f, 0.8f, 0.55f);

        public override float ValuePerLiter => 80f;

        public override float InjectionSickness => 0.25f;

        protected override float SecondsPerHundredMl => 600f;

        public override DrugText Text { get; } = new DrugText(
            "Xylazine",
            "Veterinary tranquilizer that slows the heart and breathing. It was never meant for people, and it leaves the skin around repeated injections dying.",
            "Ксилазин",
            "Ветеринарный транквилизатор, замедляющий сердце и дыхание. Он никогда не предназначался людям и оставляет кожу вокруг повторных инъекций отмирающей.");

        protected override void OnImmediate(float ml, Body body)
        {
            body.GetOrAddComponent<SleepingPills>().amount += SedationPerHundredMl * ml * 0.01f;
        }

        protected override void Tick(Body body)
        {
            body.temporarySlowdown = Mathf.Max(body.temporarySlowdown, Slowdown);
            body.respiratoryRate = Mathf.Min(body.respiratoryRate, MaxRespiratoryRate);
            body.heartRate = Mathf.Max(0f, body.heartRate - HeartRateDropPerSecond);
            if (Random.value < VomitChance)
            {
                body.vomiter.Vomit();
            }
        }
    }
}
