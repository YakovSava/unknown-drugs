using UnityEngine;
using UnknownDrugs.Core;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class Kinetic : TimedEffectDrug
    {
        private const float MaxStimulantBonus = 0.25f;
        private const float KickChance = 0.12f;
        private const float KickSpeed = 25f;
        private const float KickShake = 60f;
        private const float CrashSlowdown = 0.5f;
        private const string BounceSound = "mushroombounce";

        public override string Id => "kinetic";

        public override string EffectId => "kinetic";

        public override DrugClass Class => DrugClass.Stimulant;

        public override Color Color => new Color(0.4f, 0.9f, 0.95f);

        public override float ValuePerLiter => 120f;

        protected override float SecondsPerHundredMl => 60f;

        public override DrugText Text { get; } = new DrugText(
            "Kinetic tincture",
            "Tincture of kinetic crystal. Your legs will not stay still until the moment they refuse to move at all.",
            "Кинетическая настойка",
            "Настойка на кинетическом кристалле. Ноги не могут устоять на месте, пока в какой-то момент не откажутся двигаться вовсе.");

        protected override void Tick(Body body)
        {
            body.stamina += 1.5f;
            if (body.stimulantMultiplier < MaxStimulantBonus)
            {
                body.stimulantMultiplier += 0.05f;
            }
            if (Random.value < KickChance)
            {
                Kick(body);
            }
        }

        protected override void OnEnd(Body body)
        {
            body.slowdownAmount = CrashSlowdown;
            body.stamina -= 40f;
            foreach (Limb limb in body.limbs)
            {
                limb.muscleHealth = Mathf.Max(0f, limb.muscleHealth - 5f);
            }
        }

        private static void Kick(Body body)
        {
            body.SetVelocity(Random.insideUnitCircle.normalized * KickSpeed);
            PlayerCamera.main.shaker.Shake(KickShake);
            Sound.Play(BounceSound, body.transform.position);
        }
    }
}
