using HarmonyLib;
using Unity.VisualScripting;
using UnityEngine;

namespace UnknownDrugs.Effects
{
    public sealed class ScreenDistortion : MonoBehaviour
    {
        private const float FadePerSecond = 0.25f;
        private const float Smoothing = 2f;
        private const float SilenceThreshold = 0.001f;

        private float target;
        private float current;

        public float Current => current;

        public static void Push(Body body, float amount)
        {
            ScreenDistortion distortion = body.GetOrAddComponent<ScreenDistortion>();
            distortion.target = Mathf.Max(distortion.target, amount);
        }

        private void Update()
        {
            target = Mathf.MoveTowards(target, 0f, FadePerSecond * Time.deltaTime);
            current = Mathf.Lerp(current, target, Smoothing * Time.deltaTime);
            if (current < SilenceThreshold && target <= 0f)
            {
                Destroy(this);
            }
        }
    }

    [HarmonyPatch(typeof(PlayerCamera), "HandleScreenShaders")]
    internal static class ScreenDistortionPatch
    {
        private const float AberrationPerLevel = 0.6f;
        private const float WarpPerLevel = 0.12f;
        private const float WarpSpeed = 0.7f;
        private const float BlurPerLevel = 0.5f;
        private const float SaturationPerLevel = 1.5f;

        private static void Postfix(PlayerCamera __instance)
        {
            if (!__instance.body.TryGetComponent(out ScreenDistortion distortion))
            {
                return;
            }
            float level = distortion.Current;
            float warp = Mathf.Sin(Time.time * WarpSpeed) * level * WarpPerLevel;
            Add(__instance.bonusPass, "_Intensity", level * AberrationPerLevel);
            Add(__instance.bonusPass, "_Distort", warp);
            Add(__instance.blurPass, "_BlurIntensity", level * BlurPerLevel);
            Add(__instance.mainScreenPass, "_SaturationMult", level * SaturationPerLevel);
        }

        private static void Add(Material material, string property, float delta)
        {
            if (material.HasProperty(property))
            {
                material.SetFloat(property, material.GetFloat(property) + delta);
            }
        }
    }
}
