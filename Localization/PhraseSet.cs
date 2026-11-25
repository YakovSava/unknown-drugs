using UnityEngine;

namespace UnknownDrugs.Localization
{
    public sealed class PhraseSet
    {
        private readonly string[] english;
        private readonly string[] russian;

        public PhraseSet(string[] english, string[] russian)
        {
            this.english = english;
            this.russian = russian;
        }

        public string Pick()
        {
            string[] phrases = GameLocale.IsRussian ? russian : english;
            return phrases[Random.Range(0, phrases.Length)];
        }
    }
}
