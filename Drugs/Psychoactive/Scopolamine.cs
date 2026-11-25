using UnityEngine;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class Scopolamine : PsychedelicDrug
    {
        private const float ReversedControlsChance = 0.03f;
        private const float BlackoutChance = 0.03f;
        private const float BlackoutDepth = 25f;

        private static readonly PhraseSet DeliriumPhrases = new PhraseSet(
            new[] { "Who is standing behind me?", "Did I already say that?", "Where did I put my hands?", "Everyone is whispering." },
            new[] { "Кто там стоит за спиной?", "Я уже говорил это?", "Куда я дел свои руки?", "Все шепчутся." });

        public override string Id => "scopolamine";

        public override string EffectId => "scopolamine";

        public override Color Color => new Color(0.9f, 0.85f, 0.95f);

        public override float ValuePerLiter => 160f;

        protected override float SecondsPerHundredMl => 200f;

        protected override float DistortionLevel => 0.7f;

        protected override float BadTripChance => 0.3f;

        protected override float PhraseChance => 0.04f;

        protected override PhraseSet Phrases => DeliriumPhrases;

        public override DrugText Text { get; } = new DrugText(
            "Scopolamine",
            "Alkaloid of nightshade plants, used in the early 1900s for \"twilight sleep\" and rumored to loosen tongues. Dry mouth, confusion, and gaps in memory.",
            "Скополамин",
            "Алкалоид пасленовых, в начале 1900-х применялся для «сумеречного сна» и считался развязывающим языки. Сухость во рту, спутанность и провалы в памяти.");

        protected override void Tick(Body body)
        {
            base.Tick(body);
            body.thirst -= 0.15f;
            if (Random.value < ReversedControlsChance)
            {
                body.StartCoroutine("BrainControlReverse");
            }
            if (Random.value < BlackoutChance)
            {
                body.consciousness = Mathf.Max(0f, body.consciousness - BlackoutDepth);
            }
        }

        protected override void OnEnd(Body body)
        {
            body.energy -= 10f;
            body.sicknessAmount += 4f;
        }
    }
}
