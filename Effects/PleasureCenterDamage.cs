using CUCoreLib.Registries;
using Newtonsoft.Json;
using UnityEngine;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Effects
{
    [Saveable]
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class PleasureCenterDamage : MonoBehaviour
    {
        private const float OnsetSeconds = 60f;
        private const float BrainLossPerSecond = 0.2f;
        private const float RecoveryThreshold = 60f;
        private const float LockedHappiness = -150f;
        private const float MoodleInterval = 1.5f;
        private const string MoodleKey = "umptp.damage";

        [JsonProperty]
        private float elapsed;

        [JsonProperty]
        private bool active;

        [JsonProperty]
        private bool braingrowUsed;

        private Body body;
        private float nextMoodleTime;

        private void Awake()
        {
            body = GetComponent<Body>();
        }

        private void Update()
        {
            if (!active)
            {
                elapsed += Time.deltaTime;
                if (elapsed < OnsetSeconds)
                {
                    return;
                }
                active = true;
            }
            if (CoUtils.instance.DurationOf("braingrow") > 0f)
            {
                braingrowUsed = true;
            }
            body.brainHealth = Mathf.Max(
                0f,
                body.brainHealth - BrainLossPerSecond * Time.deltaTime);
            bool recovering = braingrowUsed && body.brainHealth >= RecoveryThreshold;
            if (recovering)
            {
                Destroy(this);
                return;
            }
            ShowMoodle();
        }

        private void LateUpdate()
        {
            if (!active)
            {
                return;
            }
            body.happiness = LockedHappiness;
            body.opiateHappiness = 0f;
            body.antidepressantHappiness = 0f;
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
                "braindamage",
                GameLocale.IsRussian ? "Отмирание мозга" : "Brain death",
                GameLocale.IsRussian
                    ? "Разум угасает. Обычные обезболивающие больше не помогают."
                    : "The mind is shutting down. Ordinary painkillers no longer help.",
                key: MoodleKey,
                holdSeconds: MoodleInterval);
        }
    }
}
