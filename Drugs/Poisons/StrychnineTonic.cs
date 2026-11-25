using Unity.VisualScripting;
using UnityEngine;
using UnknownDrugs.Core;
using UnknownDrugs.Effects;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class StrychnineTonic : TimedEffectDrug
    {
        private const float StrychninePerMl = 0.5f;
        private const float MaxStimulantBonus = 0.15f;

        public override string Id => "strychninetonic";

        public override string EffectId => "strychninetonic";

        public override DrugClass Class => DrugClass.Stimulant;

        public override Color Color => new Color(0.85f, 0.75f, 0.5f);

        public override float ValuePerLiter => 55f;

        public override bool Injectable => false;

        public override bool IsLethalForCans => true;

        protected override float SecondsPerHundredMl => 60f;

        public override DrugText Text { get; } = new DrugText(
            "Nerve tonic",
            "Strengthening tonic for the nerves and tired muscles, popular with athletes of the early 1900s.",
            "Нервный тоник",
            "Укрепляющее средство для нервов и усталых мышц, популярное среди атлетов начала 1900-х.");

        protected override void OnImmediate(float ml, Body body)
        {
            body.GetOrAddComponent<StrychnineLoad>().Add(ml * StrychninePerMl);
        }

        protected override void Tick(Body body)
        {
            body.stamina += 2f;
            body.adrenaline += 2f;
            if (body.stimulantMultiplier < MaxStimulantBonus)
            {
                body.stimulantMultiplier += 0.03f;
            }
        }
    }
}
