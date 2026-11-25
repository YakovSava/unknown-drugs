using UnityEngine;
using UnknownDrugs.Core;
using UnknownDrugs.Effects;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class Flumazenil : Drug
    {
        private const float SedationReversalPerHundredMl = 400f;
        private const float WithdrawalSeizureChance = 0.4f;

        public override string Id => "flumazenil";

        public override Color Color => new Color(0.85f, 1f, 0.9f);

        public override float ValuePerLiter => 600f;

        public override DrugText Text { get; } = new DrugText(
            "Flumazenil",
            "Antidote for benzodiazepines. Wakes the patient quickly, but in someone who has been on them for a while it can trigger a seizure.",
            "Флумазенил",
            "Антидот бензодиазепинов. Быстро приводит в чувство, но у тех, кто принимал их долго, может спровоцировать припадок.");

        public override void Apply(float ml, Body body)
        {
            bool wasSedated = Antidotes.IsActive(DrugClass.Benzodiazepine);
            Antidotes.StopEffects(DrugClass.Benzodiazepine);
            if (body.TryGetComponent(out SleepingPills pills))
            {
                pills.amount = Mathf.Max(0f, pills.amount - SedationReversalPerHundredMl * ml * 0.01f);
            }
            if (wasSedated && Random.value < WithdrawalSeizureChance)
            {
                Convulsions.Trigger(body);
            }
        }
    }
}
