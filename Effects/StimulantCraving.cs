using Newtonsoft.Json;
using UnityEngine;
using UnknownDrugs.Drugs;

namespace UnknownDrugs.Effects
{
    [Saveable]
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class StimulantCraving : MonoBehaviour
    {
        private const float MaxLevel = 100f;
        private const float DecayPerSecond = 0.1f;
        private const float HappinessDrainPerSecond = 0.15f;
        private const float SicknessPerSecond = 0.02f;
        private const float EnergyDrainPerSecond = 0.03f;
        private const float MinPhraseDelay = 25f;
        private const float MaxPhraseDelay = 45f;

        [JsonProperty]
        private float level;

        private Body body;
        private float nextPhraseTime;

        public float Level => level;

        public void Feed(float amount)
        {
            level = Mathf.Min(MaxLevel, level + amount);
        }

        private void Awake()
        {
            body = GetComponent<Body>();
        }

        private void Update()
        {
            if (CoUtils.instance.DurationOf(AlphaPvp.EffectId) > 0f)
            {
                return;
            }
            level = Mathf.Max(0f, level - DecayPerSecond * Time.deltaTime);
            if (level <= 0f)
            {
                Destroy(this);
                return;
            }
            ApplyWithdrawal(level / MaxLevel * Time.deltaTime);
            SpeakWhenDue();
        }

        private void ApplyWithdrawal(float scaledSeconds)
        {
            body.happiness -= HappinessDrainPerSecond * scaledSeconds;
            body.sicknessAmount += SicknessPerSecond * scaledSeconds;
            body.energy -= EnergyDrainPerSecond * scaledSeconds;
        }

        private void SpeakWhenDue()
        {
            if (Time.time < nextPhraseTime)
            {
                return;
            }
            nextPhraseTime = Time.time + Random.Range(MinPhraseDelay, MaxPhraseDelay);
            body.talker.Talk(AlphaPvp.CravingPhrases.Pick());
        }
    }
}
