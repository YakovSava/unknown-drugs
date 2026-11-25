using Unity.VisualScripting;
using UnityEngine;
using UnknownDrugs.Core;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class Oxylitin : Drug
    {
        private const string OxylineId = "oxyline";
        private const int TorsoIndex = 1;
        private const float OpiateUnitsPerHundredMl = 35f;

        public override string Id => "oxylitin";

        public override Color Color => new Color(0.55f, 1f, 0.6f);

        public override float ValuePerLiter => 150f;

        public override DrugText Text { get; } = new DrugText(
            "Oxylitin",
            "A makeshift blend of oxyline and opium. Dulls pain while keeping the lungs working. The breathing stimulant offsets the opiate's depression.",
            "Оксилитин",
            "Кустарная смесь оксилина и опиума. Заглушает боль, но не даёт лёгким сдаться. Стимулятор дыхания гасит угнетение от опиата.");

        public override void Apply(float ml, Body body)
        {
            Liquids.Registry[OxylineId].onHealthUse(ml, body.limbs[TorsoIndex]);
            float hundreds = ml * 0.01f;
            body.GetOrAddComponent<Painkillers>().opiateAmount += OpiateUnitsPerHundredMl * hundreds;
        }
    }
}
