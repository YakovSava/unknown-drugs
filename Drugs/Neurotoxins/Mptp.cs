using Unity.VisualScripting;
using UnityEngine;
using UnknownDrugs.Core;
using UnknownDrugs.Effects;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class Mptp : Drug
    {
        public const string EffectId = "mptp";

        private const float EuphoriaSecondsPerHundredMl = 90f;
        private const float MinEuphoriaSeconds = 60f;
        private const float InitialEuphoriaPerHundredMl = 50f;
        private const float EuphoriaPerSecond = 1.5f;
        private const float EuphoriaDistortion = 0.35f;
        private const float EuphoriaPhraseChance = 0.04f;

        private static readonly PhraseSet EuphoriaPhrases = new PhraseSet(
            new[] { "Everything is perfect.", "I feel wonderful.", "This is the best I have ever felt.", "Nothing can go wrong now." },
            new[] { "Всё идеально.", "Я чувствую себя прекрасно.", "Лучше, чем когда-либо.", "Теперь ничто не может пойти не так." });

        public override string Id => "mptp";

        public override Color Color => new Color(0.55f, 0.65f, 0.45f);

        public override float ValuePerLiter => 300f;

        public override float InjectionSickness => 0.3f;

        public override DrugText Text { get; } = new DrugText(
            "MPTP",
            "???",
            "MPTP",
            "???");

        public override void Apply(float ml, Body body)
        {
            float hundreds = ml * 0.01f;
            body.happiness += InitialEuphoriaPerHundredMl * hundreds;
            body.GetOrAddComponent<PleasureCenterDamage>();
            float duration = Mathf.Max(MinEuphoriaSeconds, EuphoriaSecondsPerHundredMl * hundreds);
            CoUtils.instance.DoTimedOp(EffectId, () => FeelGood(body), duration);
        }

        private static void FeelGood(Body body)
        {
            body.happiness += EuphoriaPerSecond;
            ScreenDistortion.Push(body, EuphoriaDistortion);
            if (Random.value < EuphoriaPhraseChance)
            {
                body.talker.Talk(EuphoriaPhrases.Pick());
            }
        }
    }
}
