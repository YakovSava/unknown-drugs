using Newtonsoft.Json;
using UnityEngine;
using Unity.VisualScripting;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Effects
{
    [Saveable]
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class AmanitaTrip : MonoBehaviour
    {
        private const float DurationSeconds = 900f;
        private const float HappinessPerSecond = 0.35f;
        private const float HungerPerSecond = 0.01f;
        private const float BrainLossPerSecond = 0.04f;
        private const float Distortion = 0.55f;
        private const float EndCrash = 25f;
        private const float WithdrawalSeconds = 900f;
        private const float WithdrawalSeverity = 1.5f;
        private const float MinPhraseDelay = 25f;
        private const float MaxPhraseDelay = 45f;

        private static readonly PhraseSet TripPhrases = new PhraseSet(
            new[] { "Everything is hilarious!", "The ceiling is dancing.", "I am enormous.", "Colors have sounds." },
            new[] { "Всё уморительно!", "Потолок танцует.", "Я огромный.", "У цветов есть звук." });

        [JsonProperty]
        private float remaining;

        private Body body;
        private float nextPhraseTime;

        public static void Start(Body body)
        {
            AmanitaTrip effect = body.GetOrAddComponent<AmanitaTrip>();
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
            body.brainHealth -= BrainLossPerSecond * deltaTime;
            ScreenDistortion.Push(body, Distortion);
            SpeakWhenDue();
            if (remaining <= 0f)
            {
                Finish();
            }
        }

        private void Finish()
        {
            body.happiness -= EndCrash;
            PsychedelicWithdrawal.Start(
                body,
                WithdrawalSeconds,
                WithdrawalSeverity,
                WithdrawalFlavor.Amanita);
            Destroy(this);
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
