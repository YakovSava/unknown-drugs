using Newtonsoft.Json;
using UnityEngine;

namespace UnknownDrugs.Effects
{
    [Saveable]
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class ArsenicLoad : ToxinLoad
    {
        private const float SymptomsStart = 10f;
        private const float SicknessPerSecond = 0.04f;
        private const float SkinDamagePerSecond = 0.02f;
        private const float BrainDamageStart = 60f;
        private const float BrainDamagePerSecond = 0.006f;
        private const float FullLoad = 100f;

        protected override float DecayPerSecond => 0.002f;

        protected override void ApplySymptoms(float deltaTime)
        {
            if (load < SymptomsStart)
            {
                return;
            }
            float severity = load / FullLoad;
            body.sicknessAmount += SicknessPerSecond * severity * deltaTime;
            foreach (Limb limb in body.limbs)
            {
                limb.skinHealth = Mathf.Max(0f, limb.skinHealth - SkinDamagePerSecond * severity * deltaTime);
            }
            if (load > BrainDamageStart)
            {
                body.brainHealth -= BrainDamagePerSecond * (load - BrainDamageStart) / (FullLoad - BrainDamageStart) * deltaTime;
            }
        }
    }

    [Saveable]
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class StrychnineLoad : ToxinLoad
    {
        private const float ConvulsionLoad = 15f;
        private const float ConvulsionChancePerLoad = 0.004f;
        private const float ParalysisLoad = 60f;
        private const float HypoxiaPerLoad = 0.05f;

        protected override float DecayPerSecond => 0.05f;

        protected override void ApplySymptoms(float deltaTime)
        {
            if (load > ConvulsionLoad && Random.value < (load - ConvulsionLoad) * ConvulsionChancePerLoad * deltaTime)
            {
                Convulsions.Trigger(body);
            }
            if (load > ParalysisLoad)
            {
                body.bloodOxygen -= (load - ParalysisLoad) * HypoxiaPerLoad * deltaTime;
            }
        }
    }
}
