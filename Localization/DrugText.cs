namespace UnknownDrugs.Localization
{
    public sealed class DrugText
    {
        private readonly string englishName;
        private readonly string englishDescription;
        private readonly string russianName;
        private readonly string russianDescription;

        public DrugText(
            string englishName,
            string englishDescription,
            string russianName,
            string russianDescription)
        {
            this.englishName = englishName;
            this.englishDescription = englishDescription;
            this.russianName = russianName;
            this.russianDescription = russianDescription;
        }

        public string Name => GameLocale.IsRussian ? russianName : englishName;

        public string Description =>
            GameLocale.IsRussian ? russianDescription : englishDescription;
    }
}
