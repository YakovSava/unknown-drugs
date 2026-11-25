using Unity.VisualScripting;
using UnityEngine;
using UnknownDrugs.Core;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class Desomorphine : Drug
    {
        private const float OpiateUnitsPerHundredMl = 60f;

        public override string Id => "desomorphine";

        public override Color Color => new Color(0.45f, 0.5f, 0.25f);

        public override float ValuePerLiter => 30f;

        public override float InjectionSickness => 1f;

        public override float InjectionGangreneChance => 0.75f;

        public override DrugText Text { get; } = new DrugText(
            "Desomorphine",
            "Opioid first synthesized in 1932. Strong and short. Backyard batches, nicknamed \"krokodil\", rot the flesh around the injection site.",
            "Дезоморфин",
            "Опиоид, впервые синтезированный в 1932 году. Сильный и короткий. Кустарные варианты, прозванные «крокодилом», гноят плоть вокруг места укола.");

        public override void Apply(float ml, Body body)
        {
            float hundreds = ml * 0.01f;
            body.GetOrAddComponent<Painkillers>().opiateAmount += OpiateUnitsPerHundredMl * hundreds;
        }
    }
}
