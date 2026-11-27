using Unity.VisualScripting;
using UnityEngine;
using UnknownDrugs.Core;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class Trazodone : TimedEffectDrug
    {
        private const float OnsetSeconds = 35f;
        private const float Slowdown = 0.45f;
        private const float SedationPerHundredMl = 180f;
        private const float SicknessPerHundredMl = 2f;
        private const float WobbleShake = 1.5f;
        private const float EyeClose = 0.8f;
        private const float SedationFloor = 5f;

        public override string Id => "trazodone";

        public override string EffectId => "trazodone";

        public override DrugClass Class => DrugClass.Sedative;

        public override Color Color => new Color(0.85f, 0.82f, 0.72f);

        public override float ValuePerLiter => 60f;

        public override float InjectionSickness => 0.25f;

        protected override float SecondsPerHundredMl => 300f;

        public override DrugText Text { get; } = new DrugText(
            "Trazodone",
            "An antidepressant from 1960s Italy that sedates far more than it lifts the mood. After half a minute the floor starts to sway and sleep becomes very inviting.",
            "Тразодон",
            "Антидепрессант из Италии 1960-х, который успокаивает гораздо сильнее, чем поднимает настроение. Через полминуты пол начинает качаться, а сон становится очень заманчивым.");

        protected override void OnImmediate(float ml, Body body)
        {
            float hundreds = ml * 0.01f;
            body.GetOrAddComponent<SleepingPills>().amount += SedationPerHundredMl * hundreds;
            body.sicknessAmount += SicknessPerHundredMl * hundreds;
        }

        protected override void Tick(Body body)
        {
            float elapsed = CoUtils.instance.HighestDurationOf(EffectId)
                - CoUtils.instance.DurationOf(EffectId);
            if (elapsed < OnsetSeconds)
            {
                return;
            }
            SleepingPills pills = body.GetOrAddComponent<SleepingPills>();
            pills.amount = Mathf.Max(pills.amount, SedationFloor);
            body.temporarySlowdown = Mathf.Max(body.temporarySlowdown, Slowdown);
            body.miscShakeIntensity += WobbleShake;
            body.eyeCloseTime = Mathf.Max(body.eyeCloseTime, EyeClose);
        }

        protected override void OnEnd(Body body)
        {
            body.sicknessAmount += 1f;
        }
    }
}
