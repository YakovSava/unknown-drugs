using UnityEngine;
using UnknownDrugs.Core;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class Mephedrone : TimedEffectDrug
    {
        private const float MaxStimulantBonus = 0.15f;
        private const float ShakeChance = 0.08f;

        public override string Id => "mephedrone";

        public override string EffectId => "mephedrone";

        public override DrugClass Class => DrugClass.Stimulant;

        public override Color Color => new Color(0.96f, 0.93f, 0.75f);

        public override float ValuePerLiter => 60f;

        public override float InjectionSickness => 0.5f;

        protected override float SecondsPerHundredMl => 75f;

        public override DrugText Text { get; } = new DrugText(
            "Mephedrone",
            "Stimulant first synthesized in 1929. Short bright rush, then a hard crash. Raises body temperature.",
            "Мефедрон",
            "Стимулятор, впервые синтезированный в 1929 году. Короткий яркий прилив и тяжёлый откат. Повышает температуру тела.");

        protected override void Tick(Body body)
        {
            body.adrenaline += 4f;
            body.stamina += 1.5f;
            body.happiness += 0.12f;
            body.temperature += 0.015f;
            if (body.stimulantMultiplier < MaxStimulantBonus)
            {
                body.stimulantMultiplier += 0.03f;
            }
            if (Random.value < ShakeChance)
            {
                body.miscShakeIntensity += 1.5f;
            }
        }

        protected override void OnEnd(Body body)
        {
            body.happiness -= 8f;
            body.energy -= 10f;
            body.sicknessAmount += 4f;
        }
    }
}
