using Unity.VisualScripting;
using UnityEngine;
using UnknownDrugs.Core;
using UnknownDrugs.Effects;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class FowlersSolution : Drug
    {
        private const float EnergyPerHundredMl = 3f;
        private const float HappinessPerHundredMl = 1.5f;
        private const float ArsenicPerMl = 0.4f;

        public override string Id => "fowlerssolution";

        public override Color Color => new Color(0.95f, 0.95f, 0.8f);

        public override float ValuePerLiter => 60f;

        public override bool Injectable => false;

        public override bool IsLethalForCans => true;

        public override DrugText Text { get; } = new DrugText(
            "Fowler's solution",
            "Tonic prescribed since 1786 for weakness, fevers and skin complaints. Taken by the spoonful.",
            "Раствор Фаулера",
            "Тоник, который с 1786 года прописывали при слабости, лихорадке и кожных недугах. Принимается по ложке.");

        public override void Apply(float ml, Body body)
        {
            float hundreds = ml * 0.01f;
            body.energy += EnergyPerHundredMl * hundreds;
            body.happiness += HappinessPerHundredMl * hundreds;
            body.GetOrAddComponent<ArsenicLoad>().Add(ml * ArsenicPerMl);
        }
    }
}
