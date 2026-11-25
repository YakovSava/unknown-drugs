using Newtonsoft.Json;
using UnityEngine;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Effects
{
    [Saveable]
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class Intoxication : MonoBehaviour
    {
        private const float PointsPerMlOfPureAlcohol = 0.25f;
        private const float MaxLevel = 120f;
        private const float DecayPerSecond = 0.03f;
        private const float TipsyLevel = 8f;
        private const float DrunkLevel = 25f;
        private const float SickLevel = 50f;
        private const float PoisoningLevel = 85f;
        private const float SlowdownPerLevel = 0.004f;
        private const float MaxSlowdown = 0.3f;
        private const float CheerPerSecond = 0.05f;
        private const float StumbleChancePerLevel = 0.002f;
        private const float VomitChancePerLevel = 0.002f;
        private const float BlackoutPerLevel = 0.1f;
        private const float HypoxiaPerLevel = 0.1f;
        private const float MinPhraseDelay = 20f;
        private const float MaxPhraseDelay = 40f;
        private const float DistortionPerLevel = 0.01f;

        private static readonly PhraseSet SlurPhrases = new PhraseSet(
            new[] { "I'm fine. Totally fine.", "Whoa... the floor moved.", "Love you guys...", "Hic!" },
            new[] { "Я в порядке. Совершенно в порядке.", "Ух... пол двинулся.", "Я вас всех люблю...", "Ик!" });

        [JsonProperty]
        private float level;

        private Body body;
        private float nextPhraseTime;

        public float Level => level;

        public static float PointsFor(float ml, float strength)
        {
            return ml * strength * PointsPerMlOfPureAlcohol;
        }

        public void Add(float points)
        {
            level = Mathf.Min(MaxLevel, level + points);
        }

        private void Awake()
        {
            body = GetComponent<Body>();
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;
            level -= DecayPerSecond * deltaTime;
            if (level <= 0f)
            {
                Destroy(this);
                return;
            }
            ScreenDistortion.Push(body, Mathf.Clamp01(level * DistortionPerLevel));
            if (level >= TipsyLevel)
            {
                Loosen(deltaTime);
            }
            if (level >= DrunkLevel)
            {
                Stagger(deltaTime);
            }
            if (level >= SickLevel)
            {
                Sicken(deltaTime);
            }
            if (level >= PoisoningLevel)
            {
                body.bloodOxygen -= (level - PoisoningLevel) * HypoxiaPerLevel * deltaTime;
            }
        }

        private void Loosen(float deltaTime)
        {
            float slowdown = Mathf.Min(MaxSlowdown, level * SlowdownPerLevel);
            body.stimulantMultiplier = Mathf.Min(body.stimulantMultiplier, -slowdown);
            body.happiness += CheerPerSecond * deltaTime;
        }

        private void Stagger(float deltaTime)
        {
            if (Random.value < (level - DrunkLevel) * StumbleChancePerLevel * deltaTime)
            {
                body.Ragdoll();
            }
            if (Time.time >= nextPhraseTime)
            {
                nextPhraseTime = Time.time + Random.Range(MinPhraseDelay, MaxPhraseDelay);
                body.talker.Talk(SlurPhrases.Pick());
            }
        }

        private void Sicken(float deltaTime)
        {
            float excess = level - SickLevel;
            if (Random.value < excess * VomitChancePerLevel * deltaTime)
            {
                body.vomiter.Vomit();
            }
            body.consciousness = Mathf.Max(0f, body.consciousness - excess * BlackoutPerLevel * deltaTime);
        }
    }
}
