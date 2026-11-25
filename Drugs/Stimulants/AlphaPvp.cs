using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnknownDrugs.Core;
using UnknownDrugs.Effects;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class AlphaPvp : Drug
    {
        public const string EffectId = "alphapvp";

        private const float HighSecondsPerHundredMl = 40f;
        private const float CravingPerHundredMl = 50f;
        private const float CravingShortensHigh = 0.02f;
        private const float MaxStimulantBonus = 0.1f;
        private const float ShakeChance = 0.07f;
        private const float BabbleChance = 0.03f;
        private const float RagdollChance = 0.02f;
        private const float ReversedControlChance = 0.015f;

        public static readonly PhraseSet HighPhrases = new PhraseSet(
            new[] { "I can feel EVERYTHING!", "Why is everything so funny?!", "I'm unstoppable!", "Hehehe... hehe." },
            new[] { "Я чувствую ВСЁ!", "Почему всё такое смешное?!", "Меня не остановить!", "Хе-хе-хе... хе." });

        public static readonly PhraseSet CravingPhrases = new PhraseSet(
            new[] { "Just a little more...", "I need it. Right now.", "One more and I'm done. Promise." },
            new[] { "Ещё чуть-чуть...", "Мне нужно. Прямо сейчас.", "Ещё одну и всё. Обещаю." });

        public override string Id => "alphapvp";

        public override DrugClass Class => DrugClass.Stimulant;

        public override IReadOnlyList<string> ActiveEffectIds => new[] { EffectId };

        public override Color Color => new Color(0.9f, 0.55f, 0.95f);

        public override float ValuePerLiter => 70f;

        public override float InjectionSickness => 0.5f;

        public override DrugText Text { get; } = new DrugText(
            "Alpha-PVP",
            "Synthetic stimulant. Hooks from the very first dose. The high is brief and silly, the comedown lasts far longer.",
            "Альфа-PVP",
            "Синтетический стимулятор. Цепляет с первой дозы. Кайф короткий и глупый, а отходняк длится гораздо дольше.");

        public override void Apply(float ml, Body body)
        {
            float hundreds = ml * 0.01f;
            StimulantCraving craving = body.GetOrAddComponent<StimulantCraving>();
            float duration = HighSecondsPerHundredMl * hundreds / (1f + craving.Level * CravingShortensHigh);
            craving.Feed(CravingPerHundredMl * hundreds);
            CoUtils.instance.DoTimedOp(EffectId, () => High(body), duration);
        }

        private static void High(Body body)
        {
            body.stamina += 2f;
            body.adrenaline += 6f;
            body.happiness += 0.08f;
            if (body.stimulantMultiplier < MaxStimulantBonus)
            {
                body.stimulantMultiplier += 0.03f;
            }
            ActLikeAnIdiot(body);
            if (CoUtils.instance.DurationOf(EffectId) <= 1f)
            {
                Crash(body);
            }
        }

        private static void ActLikeAnIdiot(Body body)
        {
            if (Random.value < ShakeChance)
            {
                body.miscShakeIntensity += 2f;
            }
            if (Random.value < BabbleChance)
            {
                body.talker.Talk(HighPhrases.Pick());
            }
            if (Random.value < RagdollChance)
            {
                body.Ragdoll();
            }
            if (Random.value < ReversedControlChance)
            {
                body.StartCoroutine("BrainControlReverse");
            }
        }

        private static void Crash(Body body)
        {
            body.stamina -= 35f;
            body.energy -= 10f;
            body.happiness -= 6f;
        }
    }
}
