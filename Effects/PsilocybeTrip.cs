using Newtonsoft.Json;
using UnityEngine;
using Unity.VisualScripting;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Effects
{
    [Saveable]
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class PsilocybeTrip : MonoBehaviour
    {
        private const float DurationSeconds = 150f;
        private const float HappinessPerSecond = 0.08f;
        private const float HungerPerSecond = 0.015f;
        private const float Distortion = 0.3f;
        private const float EndCrash = 4f;
        private const float MinPhraseDelay = 25f;
        private const float MaxPhraseDelay = 45f;

        private static readonly PhraseSet TripPhrases = new PhraseSet(
            new[] { "The walls are soft.", "I can taste the light.", "Everything is growing.", "The cave is humming." },
            new[] { "Стены мягкие.", "Я чувствую вкус света.", "Всё растёт.", "Пещера гудит." });

        [JsonProperty]
        private float remaining;

        private Body body;
        private float nextPhraseTime;

        public static void Start(Body body)
        {
            PsilocybeTrip effect = body.GetOrAddComponent<PsilocybeTrip>();
            effect.remaining = Mathf.Max(effect.remaining, DurationSeconds);
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
            body.happiness += HappinessPerSecond * deltaTime;
            body.hunger += HungerPerSecond * deltaTime;
            ScreenDistortion.Push(body, Distortion);
            SpeakWhenDue();
            if (remaining <= 0f)
            {
                body.happiness -= EndCrash;
                Destroy(this);
            }
        }

        private void SpeakWhenDue()
        {
            if (Time.time < nextPhraseTime)
            {
                return;
            }
            nextPhraseTime = Time.time + Random.Range(MinPhraseDelay, MaxPhraseDelay);
            body.talker.Talk(TripPhrases.Pick());
        }
    }
}
