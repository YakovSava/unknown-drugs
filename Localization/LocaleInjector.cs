using HarmonyLib;
using UnknownDrugs.Core;
using UnknownDrugs.Items;

namespace UnknownDrugs.Localization
{
    public static class LocaleInjector
    {
        private const string DescriptionSuffix = "dsc";

        public static void Inject()
        {
            if (Locale.currentLang?.other == null)
            {
                return;
            }
            InjectLiquids();
            InjectItems();
        }

        private static void InjectLiquids()
        {
            foreach (Drug drug in DrugRegistry.All)
            {
                Locale.currentLang.other[drug.Id] = drug.Text.Name;
                Locale.currentLang.other[drug.Id + DescriptionSuffix] = drug.Text.Description;
            }
        }

        private static void InjectItems()
        {
            if (Locale.currentLang.main == null)
            {
                return;
            }
            foreach (CustomItem item in ItemCatalog.All)
            {
                Locale.currentLang.main[item.Id] = item.Text.Name;
                Locale.currentLang.main[item.Id + DescriptionSuffix] = item.Text.Description;
                if (Item.GlobalItems != null
                    && Item.GlobalItems.TryGetValue(item.Id, out ItemInfo info))
                {
                    info.fullName = item.Text.Name;
                    info.description = item.Text.Description;
                }
            }
        }
    }

    [HarmonyPatch(typeof(Locale), nameof(Locale.LoadLanguage))]
    internal static class LoadLanguagePatch
    {
        private static void Postfix()
        {
            LocaleInjector.Inject();
        }
    }
}
