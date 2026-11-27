using HarmonyLib;

namespace UnknownDrugs.Items
{
    [HarmonyPatch(typeof(BuildingEntity), "Start")]
    internal static class MushroomDropPatch
    {
        private const string FungusId = "funguschunk";
        private const float PsilocybeChance = 0.05f;
        private const float AmanitaChance = 0.01f;
        private const float FullCondition = 1f;

        private static void Postfix(BuildingEntity __instance)
        {
            if (!DropsFungus(__instance))
            {
                return;
            }
            AddDrop(__instance, PsilocybeFungus.ItemId, PsilocybeChance);
            AddDrop(__instance, AmanitaMuscaria.ItemId, AmanitaChance);
        }

        private static bool DropsFungus(BuildingEntity building)
        {
            return Contains(building.itemsDropOnDestroy, FungusId)
                || Contains(building.alwaysDrop, FungusId);
        }

        private static void AddDrop(BuildingEntity building, string id, float chance)
        {
            ItemDrop[] current = building.itemsDropOnDestroy;
            if (Contains(current, id))
            {
                return;
            }
            ItemDrop drop = new ItemDrop
            {
                id = id,
                chance = chance,
                conditionMin = FullCondition,
                conditionMax = FullCondition
            };
            if (current == null)
            {
                building.itemsDropOnDestroy = new ItemDrop[] { drop };
                return;
            }
            ItemDrop[] updated = new ItemDrop[current.Length + 1];
            for (int i = 0; i < current.Length; i++)
            {
                updated[i] = current[i];
            }
            updated[current.Length] = drop;
            building.itemsDropOnDestroy = updated;
        }

        private static bool Contains(ItemDrop[] drops, string id)
        {
            if (drops == null)
            {
                return false;
            }
            foreach (ItemDrop drop in drops)
            {
                if (drop != null && drop.id == id)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
