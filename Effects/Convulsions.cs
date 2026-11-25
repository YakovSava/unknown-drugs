using UnityEngine;

namespace UnknownDrugs.Effects
{
    public static class Convulsions
    {
        public const string EffectId = "convulsions";
        private const float DurationSeconds = 6f;
        private const float ShakePerSecond = 6f;
        private const float ConsciousnessLossPerSecond = 8f;

        public static void Trigger(Body body)
        {
            CoUtils.instance.DoTimedOp(EffectId, () => Shake(body), DurationSeconds);
        }

        private static void Shake(Body body)
        {
            body.Ragdoll();
            body.miscShakeIntensity += ShakePerSecond;
            body.consciousness = Mathf.Max(0f, body.consciousness - ConsciousnessLossPerSecond);
        }
    }
}
