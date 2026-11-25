using System.Collections.Generic;

namespace UnknownDrugs.Core
{
    public static class Antidotes
    {
        public static bool IsActive(DrugClass drugClass)
        {
            foreach (string id in EffectIdsOf(drugClass))
            {
                if (CoUtils.instance.DurationOf(id) > 0f)
                {
                    return true;
                }
            }
            return false;
        }

        public static void StopEffects(DrugClass drugClass)
        {
            foreach (string id in EffectIdsOf(drugClass))
            {
                CoUtils.instance.Stop(id);
            }
        }

        private static IEnumerable<string> EffectIdsOf(DrugClass drugClass)
        {
            foreach (Drug drug in DrugRegistry.All)
            {
                if (drug.Class != drugClass)
                {
                    continue;
                }
                foreach (string id in drug.ActiveEffectIds)
                {
                    yield return id;
                }
            }
        }
    }
}
