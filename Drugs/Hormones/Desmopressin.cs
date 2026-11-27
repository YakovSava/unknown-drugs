using UnityEngine;
using UnknownDrugs.Core;
using UnknownDrugs.Effects;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class Desmopressin : TimedEffectDrug
    {
        private const float ThirstRetentionPerSecond = 0.04f;
        private const float BrainDamagePerHundredMl = 2f;

        public override string Id => "ddvap";

        public override string EffectId => "ddvap";

        public override Color Color => new Color(0.7f, 0.85f, 0.95f);

        public override float ValuePerLiter => 90f;

        public override float InjectionSickness => 0.15f;

        protected override float SecondsPerHundredMl => 600f;

        public override DrugText Text { get; } = new DrugText(
            "DDVAP (Desmopressin)",
            "A synthetic copy of vasopressin that tells the kidneys to hold on to water. The body saves every drop, then slowly drowns its own blood in sodium-poor fluid.",
            "ДДВАП (Десмопрессин)",
            "Синтетическая копия вазопрессина, которая приказывает почкам удерживать воду. Организм бережёт каждую каплю, а потом медленно разбавляет собственную кровь жидкостью, бедной натрием.");

        protected override void OnImmediate(float ml, Body body)
        {
            float hundreds = ml * 0.01f;
            float duration = SecondsPerHundredMl * hundreds;
            float brainDamage = Mathf.Clamp(BrainDamagePerHundredMl * hundreds, 1f, 3f);
            Hyponatremia.Apply(body, duration, brainDamage);
        }

        protected override void Tick(Body body)
        {
            body.thirst += ThirstRetentionPerSecond;
        }
    }
}
