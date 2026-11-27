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
            "Psilocybe",
            "A brew made from Psilocybe fungus and Bio-Chem Fluid, far stronger than the mushroom alone. The stomach revolts first, then the colors arrive.",
            "Псилоцибе",
            "Настой из гриба псилоцибе и биохимической жидкости, гораздо сильнее самого гриба. Сначала бунтует желудок, потом приходят краски.");

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
