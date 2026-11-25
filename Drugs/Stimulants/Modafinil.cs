using UnityEngine;
using UnknownDrugs.Core;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class Modafinil : TimedEffectDrug
    {
        private const float FullFocus = 100f;

        public override string Id => "modafinil";

        public override string EffectId => "modafinil";

        public override DrugClass Class => DrugClass.Stimulant;

        public override Color Color => new Color(0.85f, 0.9f, 1f);

        public override float ValuePerLiter => 90f;

        protected override float SecondsPerHundredMl => 300f;

        public override DrugText Text { get; } = new DrugText(
            "Modafinil",
            "Wakefulness-promoting drug from the 1990s. Calm focus and no jitters. The sleep it holds back is only postponed.",
            "Модафинил",
            "Средство для бодрствования, появившееся в 1990-х. Спокойный фокус без тряски. Сон, который он отгоняет, лишь откладывается.");

        protected override void Tick(Body body)
        {
            body.focusedLevel = FullFocus;
            body.stamina += 0.8f;
            body.energy += 0.2f;
            if (body.sleeping)
            {
                body.WakeUp();
            }
        }

        protected override void OnEnd(Body body)
        {
            body.energy -= 30f;
            body.consciousness = Mathf.Max(0f, body.consciousness - 15f);
            body.happiness -= 4f;
        }
    }
}
