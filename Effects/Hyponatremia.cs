using CUCoreLib.Registries;
using Newtonsoft.Json;
using UnityEngine;
using Unity.VisualScripting;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Effects
{
    [Saveable]
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class Hyponatremia : MonoBehaviour
    {
        private const float OnsetSeconds = 60f;
        private const float SicknessPerSecond = 0.03f;
        private const float ThirstRetentionPerSecond = 0.04f;
        private const float HappinessDrainPerSecond = 0.01f;
        private const float BrainDamageSeconds = 120f;
        private const float VomitCheckSeconds = 40f;
        private const float VomitChance = 0.35f;
        private const float MoodleInterval = 1.5f;
        private const string MoodleKey = "uddvap.hyponatremia";

        [JsonProperty]
        private float remaining;

        [JsonProperty]
        private float silent;

        [JsonProperty]
        private float brainDamage;

        [JsonProperty]
        private float brainDamageLeft;
        private Body body;
        private float vomitTimer;
        private float nextMoodleTime;

        public static void Apply(Body body, float seconds, float brainDamage)
        {
            Hyponatremia effect = body.GetOrAddComponent<Hyponatremia>();
            bool wasActive = effect.remaining > 0f;
            effect.remaining = Mathf.Max(effect.remaining, seconds);
            effect.brainDamage = Mathf.Max(effect.brainDamage, brainDamage);
            if (!wasActive)
            {
                effect.silent = OnsetSeconds;
                effect.brainDamageLeft = brainDamage;
            }
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
            if (silent > 0f)
            {
                silent -= deltaTime;
                return;
            }
            ApplySymptoms(deltaTime);
            ShowMoodle();
        }

        private void ApplySymptoms(float deltaTime)
        {
            body.thirst += ThirstRetentionPerSecond * deltaTime;
            body.sicknessAmount += SicknessPerSecond * deltaTime;
            body.happiness -= HappinessDrainPerSecond * deltaTime;
            ApplyBrainDamage(deltaTime);
            RollVomit(deltaTime);
        }

        private void ApplyBrainDamage(float deltaTime)
        {
            if (brainDamageLeft <= 0f)
            {
                return;
            }
            float amount = Mathf.Min(
                brainDamageLeft,
                brainDamage / BrainDamageSeconds * deltaTime);
            body.brainHealth -= amount;
            brainDamageLeft -= amount;
        }

        private void RollVomit(float deltaTime)
        {
            vomitTimer -= deltaTime;
            if (vomitTimer > 0f)
            {
                return;
            }
            vomitTimer = VomitCheckSeconds;
            if (Random.value < VomitChance)
            {
                body.vomiter.Vomit();
            }
        }

        private void ShowMoodle()
        {
            if (Time.time < nextMoodleTime)
            {
                return;
            }
            nextMoodleTime = Time.time + MoodleInterval;
            MoodleRegistry.AddMoodle(
                2,
                "overhydrated",
                GameLocale.IsRussian ? "Гипонатриемия" : "Hyponatremia",
                GameLocale.IsRussian
                    ? "Мало натрия в крови. Тошнота, спутанность сознания и отёк мозга."
                    : "Low sodium in the blood. Nausea, confusion and swelling of the brain.",
                key: MoodleKey,
                holdSeconds: MoodleInterval);
        }
    }
}
