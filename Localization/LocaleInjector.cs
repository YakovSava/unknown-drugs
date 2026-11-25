using HarmonyLib;
using UnknownDrugs.Core;

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
            foreach (Drug drug in DrugRegistry.All)
            {
                Locale.currentLang.other[drug.Id] = drug.Text.Name;
                Locale.currentLang.other[drug.Id + DescriptionSuffix] = drug.Text.Description;
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
