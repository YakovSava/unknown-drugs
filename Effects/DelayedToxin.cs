using Newtonsoft.Json;
using UnityEngine;

namespace UnknownDrugs.Effects
{
    public abstract class DelayedToxin : ToxinLoad
    {
        private const float AbsorbFractionPerSecond = 0.02f;
        private const float EthanolShieldLevel = 5f;
        private const float ShieldedRateFactor = 0.1f;
        private const float PendingEpsilon = 0.01f;

        [JsonProperty]
        private float pending;

        protected abstract float ToxinPerMl { get; }

        protected override bool HasPendingWork => pending > PendingEpsilon;

        private bool EthanolPresent =>
            TryGetComponent(out Intoxication intoxication) && intoxication.Level >= EthanolShieldLevel;

        public void Ingest(float ml)
        {
            pending += ml;
        }

        protected override void Absorb(float deltaTime)
        {
            float rate = AbsorbFractionPerSecond * (EthanolPresent ? ShieldedRateFactor : 1f);
            float converted = pending * rate * deltaTime;
            pending -= converted;
            load += converted * ToxinPerMl;
        }
    }

    [Saveable]
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class MethanolPoisoning : DelayedToxin
    {
        private const float FirstEyeLoad = 30f;
        private const float SecondEyeLoad = 70f;
        private const float SicknessPerLoadSecond = 0.002f;
        private const float BrainDamageStart = 50f;
        private const float BrainDamagePerLoadSecond = 0.005f;
        private const float CollapseLoad = 120f;
        private const float CollapsePerLoadSecond = 0.1f;
        private const float DistortionPerLoad = 0.01f;

        [JsonProperty]
        private int eyesLost;

        protected override float DecayPerSecond => 0.004f;

        protected override float ToxinPerMl => 1.5f;

        protected override void ApplySymptoms(float deltaTime)
        {
            if (load <= 0f)
            {
                return;
            }
            body.sicknessAmount += load * SicknessPerLoadSecond * deltaTime;
            ScreenDistortion.Push(body, Mathf.Clamp01(load * DistortionPerLoad));
            LoseEyes();
            if (load > BrainDamageStart)
            {
                body.brainHealth -= (load - BrainDamageStart) * BrainDamagePerLoadSecond * deltaTime;
            }
            if (load > CollapseLoad)
            {
                body.consciousness = Mathf.Max(0f, body.consciousness - (load - CollapseLoad) * CollapsePerLoadSecond * deltaTime);
            }
        }

        private void LoseEyes()
        {
            if (eyesLost < 1 && load > FirstEyeLoad)
            {
                body.RemoveEye();
                eyesLost = 1;
            }
            if (eyesLost < 2 && load > SecondEyeLoad)
            {
                body.RemoveEye();
                eyesLost = 2;
            }
        }
    }

    [Saveable]
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class GlycolPoisoning : DelayedToxin
    {
        private const float SicknessPerLoadSecond = 0.003f;
        private const float ThirstPerLoadSecond = 0.0006f;
        private const float BrainDamageStart = 40f;
        private const float BrainDamagePerLoadSecond = 0.004f;
        private const float CollapseLoad = 100f;
        private const float CollapsePerLoadSecond = 0.1f;

        protected override float DecayPerSecond => 0.003f;

        protected override float ToxinPerMl => 1f;

        protected override void ApplySymptoms(float deltaTime)
        {
            if (load <= 0f)
            {
                return;
            }
            body.sicknessAmount += load * SicknessPerLoadSecond * deltaTime;
            body.thirst -= load * ThirstPerLoadSecond * deltaTime;
            if (load > BrainDamageStart)
            {
                body.brainHealth -= (load - BrainDamageStart) * BrainDamagePerLoadSecond * deltaTime;
            }
            if (load > CollapseLoad)
            {
                body.consciousness = Mathf.Max(0f, body.consciousness - (load - CollapseLoad) * CollapsePerLoadSecond * deltaTime);
            }
        }
    }
}
