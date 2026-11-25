using UnityEngine;
using UnknownDrugs.Core;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class Pervitin : TimedEffectDrug
    {
        private const float MaxStimulantBonus = 0.2f;
        private const float ShakeChance = 0.05f;

        public override string Id => "pervitin";

        public override string EffectId => "pervitin";

        public override DrugClass Class => DrugClass.Stimulant;

        public override Color Color => new Color(0.95f, 0.95f, 0.9f);

        public override float ValuePerLiter => 80f;

        public override float InjectionSickness => 0.4f;

        protected override float SecondsPerHundredMl => 240f;

        public override DrugText Text { get; } = new DrugText(
            "Pervitin",
            "Methamphetamine sold by Temmler from 1937 and handed out to soldiers by the million. Hours without sleep or hunger, paid back with interest.",
            "Первитин",
            "Метамфетамин, который фирма Temmler продавала с 1937 года и выдавала солдатам миллионами. Часы без сна и голода, за которые придётся платить с процентами.");

        protected override void Tick(Body body)
        {
            body.stamina += 2f;
            body.adrenaline += 3f;
            body.energy += 0.4f;
            body.hunger += 0.05f;
            body.happiness += 0.1f;
            if (body.sleeping)
            {
                body.WakeUp();
            }
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
            body.energy -= 45f;
            body.consciousness = Mathf.Max(0f, body.consciousness - 40f);
            body.happiness -= 12f;
            body.hunger -= 20f;
            body.sicknessAmount += 6f;
        }
    }
}
