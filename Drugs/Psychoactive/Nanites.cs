using UnityEngine;
using UnknownDrugs.Core;
using UnknownDrugs.Effects;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class Nanites : Drug
    {
        private const string EffectId = "nanites";
        private const float DurationPerHundredMl = 60f;
        private const float SkinRepairPerSecond = 0.8f;
        private const float MuscleRepairPerSecond = 0.6f;
        private const float BleedingDecayPerSecond = 0.97f;
        private const float BrainDamagePerSecond = 0.05f;
        private const float SeizureChancePerSecond = 0.015f;
        private const float MaxHealth = 100f;

        public override string Id => "nanites";

        public override Color Color => new Color(0.7f, 0.75f, 0.85f);

        public override float ValuePerLiter => 400f;

        public override float InjectionSickness => 0.3f;

        public override DrugText Text { get; } = new DrugText(
            "Nanites",
            "A suspension of experimental repair machines. They knit skin and muscle and slow bleeding, but gnaw at the brain and sometimes cause seizures.",
            "Наниты",
            "Взвесь экспериментальных ремонтных машин. Сращивают кожу и мышцы и замедляют кровотечение, но разъедают мозг и порой вызывают припадки.");

        public override void Apply(float ml, Body body)
        {
            float hundreds = ml * 0.01f;
            CoUtils.instance.DoTimedOp(EffectId, () => Repair(body), DurationPerHundredMl * hundreds);
        }

        private static void Repair(Body body)
        {
            foreach (Limb limb in body.limbs)
            {
                limb.skinHealth = Mathf.Min(MaxHealth, limb.skinHealth + SkinRepairPerSecond);
                limb.muscleHealth = Mathf.Min(MaxHealth, limb.muscleHealth + MuscleRepairPerSecond);
                limb.bleedAmount *= BleedingDecayPerSecond;
            }
            body.brainHealth -= BrainDamagePerSecond;
            if (Random.value < SeizureChancePerSecond)
            {
                Convulsions.Trigger(body);
            }
        }
    }
}
