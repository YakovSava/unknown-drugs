using UnityEngine;

namespace UnknownDrugs.Effects
{
    public static class Gangrene
    {
        private const float InitialInfection = 5f;
        private const float TissueDamage = 30f;

        public static void Roll(Limb limb, float chance)
        {
            if (Random.value < chance)
            {
                Cause(limb);
            }
        }

        private static void Cause(Limb limb)
        {
            limb.infected = true;
            limb.infectionAmount = Mathf.Max(limb.infectionAmount, InitialInfection);
            limb.skinHealth = Mathf.Max(0f, limb.skinHealth - TissueDamage);
            limb.muscleHealth = Mathf.Max(0f, limb.muscleHealth - TissueDamage);
        }
    }
}
