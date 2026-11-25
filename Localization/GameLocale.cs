using System;

namespace UnknownDrugs.Localization
{
    public static class GameLocale
    {
        private const string RussianPrefix = "RU";

        public static bool IsRussian =>
            Locale.currentLangName != null
            && Locale.currentLangName.StartsWith(RussianPrefix, StringComparison.OrdinalIgnoreCase);
    }
}
