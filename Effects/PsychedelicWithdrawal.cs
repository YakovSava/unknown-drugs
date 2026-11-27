using Newtonsoft.Json;
using UnityEngine;
using Unity.VisualScripting;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Effects
{
    public enum WithdrawalFlavor
    {
        Lsd,
        Amanita
    }

    [Saveable]
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class PsychedelicWithdrawal : MonoBehaviour
    {
        private const float HappinessDrainPerSecond = 0.12f;
        private const float EnergyDrainPerSecond = 0.035f;
        private const float SicknessPerSecond = 0.02f;
        private const float DistortionPerSeverity = 0.12f;
        private const float FearPerEpisode = 12f;
        private const float MinFearDelay = 20f;
        private const float MaxFearDelay = 40f;
        private const float MinPhraseDelay = 25f;
        private const float MaxPhraseDelay = 50f;

        private static readonly PhraseSet LsdPhrases = new PhraseSet(
            new[] { "Not again... not again.", "My thoughts are crawling.", "Everything feels wrong.", "I need it back." },
            new[] { "Только не снова... только не снова.", "Мои мысли ползут.", "Всё кажется неправильным.", "Мне нужно это вернуть." });

        private static readonly PhraseSet AmanitaPhrases = new PhraseSet(
            new[] { "It was so bright...", "My head is empty.", "Why is everything so quiet?", "Bring the colors back." },
            new[] { "Там было так ярко...", "В голове пусто.", "Почему всё так тихо?", "Верните краски." });

        [JsonProperty]
        private float remaining;

        [JsonProperty]
        private float severity;

        [JsonProperty]
        private int flavor;

        private Body body;
        private float nextFearTime;
        private float nextPhraseTime;

        public static void Start(
            Body body,
            float seconds,
            float severity,
            WithdrawalFlavor flavor)
        {
            PsychedelicWithdrawal effect = body.GetOrAddComponent<PsychedelicWithdrawal>();
            effect.remaining = Mathf.Max(effect.remaining, seconds);
            effect.severity = Mathf.Max(effect.severity, severity);
            effect.flavor = (int)flavor;
        }

        private void Awake()
        {
            body = GetComponent<Body>();
        }

        private void Update()
        {
            if (remaining <= 0f)
            {
                Destroy(this);
                return;
            }
            float deltaTime = Time.deltaTime;
            remaining -= deltaTime;
            body.happiness -= HappinessDrainPerSecond * severity * deltaTime;
            body.energy -= EnergyDrainPerSecond * severity * deltaTime;
            body.sicknessAmount += SicknessPerSecond * severity * deltaTime;
            ScreenDistortion.Push(body, DistortionPerSeverity * severity);
            RollFear();
            SpeakWhenDue();
        }

        private void RollFear()
        {
            if (Time.time < nextFearTime)
            {
                return;
            }
            nextFearTime = Time.time + Random.Range(MinFearDelay, MaxFearDelay);
            body.horrifiedLevel += FearPerEpisode * severity;
        }

        private void SpeakWhenDue()
        {
            if (Time.time < nextPhraseTime)
            {
                return;
            }
            nextPhraseTime = Time.time + Random.Range(MinPhraseDelay, MaxPhraseDelay);
            PhraseSet phrases = flavor == (int)WithdrawalFlavor.Amanita
                ? AmanitaPhrases
                : LsdPhrases;
            body.talker.Talk(phrases.Pick());
        }
    }
}
