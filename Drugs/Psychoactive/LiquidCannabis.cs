using UnityEngine;
using UnknownDrugs.Core;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class LiquidCannabis : Drug
    {
        private const string RelaxEffectId = "liquidcannabis";
        private const string MunchiesEffectId = "liquidcannabismunchies";
        private const float RelaxSecondsPerHundredMl = 120f;
        private const float MunchiesToRelaxRatio = 1.5f;
        private const float MaxSlowdown = -0.12f;
        private const float HungerChance = 0.04f;

        private static readonly PhraseSet HungerPhrases = new PhraseSet(
            new[] { "I could eat a horse.", "Is there anything to eat?", "My stomach is a black hole." },
            new[] { "Я бы съел коня.", "Есть что-нибудь пожевать?", "Мой желудок как чёрная дыра." });

        public override string Id => "liquidcannabis";

        public override Color Color => new Color(0.45f, 0.7f, 0.2f);

        public override float ValuePerLiter => 40f;

        public override DrugText Text { get; } = new DrugText(
            "Liquid cannabis",
            "Cannabis extract. Relaxes the body and dulls minor pain, but afterwards you will want to eat everything in sight.",
            "Жидкий каннабис",
            "Экстракт каннабиса. Расслабляет тело и притупляет лёгкую боль, но потом захочется съесть всё, что видишь.");

        public override void Apply(float ml, Body body)
        {
            float hundreds = ml * 0.01f;
            CoUtils.instance.DoTimedOp(RelaxEffectId, () => Relax(body), RelaxSecondsPerHundredMl * hundreds);
        }

        private static void Relax(Body body)
        {
            body.happiness += 0.1f;
            body.adrenaline = Mathf.Max(0f, body.adrenaline - 1.5f);
            body.thirst -= 0.03f;
            if (body.stimulantMultiplier > MaxSlowdown)
            {
                body.stimulantMultiplier -= 0.02f;
            }
            foreach (Limb limb in body.limbs)
            {
                limb.pain = Mathf.Max(0f, limb.pain - 0.4f);
            }
            if (CoUtils.instance.DurationOf(RelaxEffectId) <= 1f)
            {
                StartMunchies(body);
            }
        }

        private static void StartMunchies(Body body)
        {
            float duration = CoUtils.instance.HighestDurationOf(RelaxEffectId) * MunchiesToRelaxRatio;
            CoUtils.instance.DoTimedOp(MunchiesEffectId, () => Munchies(body), duration);
        }

        private static void Munchies(Body body)
        {
            body.hunger -= 0.12f;
            if (Random.value < HungerChance)
            {
                body.talker.Talk(HungerPhrases.Pick());
            }
        }
    }
}
