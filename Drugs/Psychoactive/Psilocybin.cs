using UnityEngine;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class Psilocybin : PsychedelicDrug
    {
        private const float StartingSickness = 5f;
        private const float VomitChance = 0.3f;

        private static readonly PhraseSet TripPhrases = new PhraseSet(
            new[] { "Hahaha... the cave is laughing!", "Everything is so beautiful.", "I am the mushroom.", "Look at the patterns!" },
            new[] { "Ха-ха-ха... пещера смеётся!", "Всё такое красивое.", "Я и есть гриб.", "Смотрите, какие узоры!" });

        public override string Id => "psilocybin";

        public override string EffectId => "psilocybin";

        public override Color Color => new Color(0.75f, 0.6f, 0.4f);

        public override float ValuePerLiter => 110f;

        protected override float SecondsPerHundredMl => 180f;

        protected override float DistortionLevel => 0.8f;

        protected override float BadTripChance => 0.1f;

        protected override float PhraseChance => 0.05f;

        protected override PhraseSet Phrases => TripPhrases;

        public override DrugText Text { get; } = new DrugText(
            "Psilocybin",
            "Compound of certain mushrooms, isolated by Albert Hofmann in 1958. The stomach revolts first, then the colors arrive.",
            "Псилоцибин",
            "Вещество некоторых грибов, выделенное Альбертом Хофманом в 1958 году. Сначала бунтует желудок, потом приходят краски.");

        protected override void OnImmediate(float ml, Body body)
        {
            base.OnImmediate(ml, body);
            body.sicknessAmount += StartingSickness;
            if (Random.value < VomitChance)
            {
                body.vomiter.Vomit();
            }
        }

        protected override void OnEnd(Body body)
        {
            body.happiness -= 5f;
            body.sicknessAmount += 2f;
        }
    }
}
