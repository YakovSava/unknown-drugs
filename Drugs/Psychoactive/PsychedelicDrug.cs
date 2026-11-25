using UnityEngine;
using UnknownDrugs.Core;
using UnknownDrugs.Effects;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public abstract class PsychedelicDrug : TimedEffectDrug
    {
        private const float MoodSwingDown = 0.3f;
        private const float MoodSwingUp = 0.4f;
        private const float BadTripFear = 40f;

        protected abstract float DistortionLevel { get; }

        protected abstract float BadTripChance { get; }

        protected abstract float PhraseChance { get; }

        protected abstract PhraseSet Phrases { get; }

        protected override void OnImmediate(float ml, Body body)
        {
            if (Random.value < BadTripChance)
            {
                body.horrifiedLevel += BadTripFear;
            }
        }

        protected override void Tick(Body body)
        {
            ScreenDistortion.Push(body, DistortionLevel);
            body.happiness += Random.Range(-MoodSwingDown, MoodSwingUp);
            if (Random.value < PhraseChance)
            {
                body.talker.Talk(Phrases.Pick());
            }
        }
    }
}
