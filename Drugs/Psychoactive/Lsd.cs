using UnityEngine;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class Lsd : PsychedelicDrug
    {
        private static readonly PhraseSet TripPhrases = new PhraseSet(
            new[] { "The walls are breathing.", "I can hear colors.", "Everything is connected!", "Why is the ceiling melting?" },
            new[] { "Стены дышат.", "Я слышу цвета.", "Всё связано со всем!", "Почему потолок плавится?" });

        public override string Id => "lsd";

        public override string EffectId => "lsd";

        public override Color Color => new Color(0.8f, 0.6f, 1f);

        public override float ValuePerLiter => 200f;

        protected override float SecondsPerHundredMl => 300f;

        protected override float DistortionLevel => 1f;

        protected override float BadTripChance => 0.2f;

        protected override float PhraseChance => 0.04f;

        protected override PhraseSet Phrases => TripPhrases;

        public override DrugText Text { get; } = new DrugText(
            "LSD",
            "Lysergic acid diethylamide, first synthesized in 1938 by Albert Hofmann. The world stops obeying its own rules for hours. The trip does not always go where you hoped.",
            "ЛСД",
            "Диэтиламид лизергиновой кислоты, впервые синтезирован в 1938 году Альбертом Хофманом. Мир на несколько часов перестаёт следовать своим правилам. Путешествие не всегда ведёт туда, куда хотелось.");

        protected override void OnEnd(Body body)
        {
            body.happiness -= 8f;
            body.energy -= 10f;
        }
    }
}
