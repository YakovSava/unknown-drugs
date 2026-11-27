using Unity.VisualScripting;
using UnityEngine;
using UnknownDrugs.Core;
using UnknownDrugs.Effects;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class Methadone : Drug
    {
        private const float OpiateUnitsPerHundredMl = 90f;

        public override string Id => "methadone";

        public override Color Color => new Color(0.35f, 0.85f, 0.45f);

        public override float ValuePerLiter => 90f;

        public override float InjectionSickness => 0.3f;

        public override float InteractionSeconds(float ml) => 900f;

        public override DrugText Text { get; } = new DrugText(
            "Methadone",
            "Synthetic opioid created in Germany in the late 1930s. Acts slowly and for a long time, doses pile up on top of each other. Wait before taking more.",
            "Метадон",
            "Синтетический опиоид, созданный в Германии в конце 1930-х. Действует медленно и долго, дозы накапливаются. Не торопитесь принимать ещё.");

        public override void Apply(float ml, Body body)
        {
            float hundreds = ml * 0.01f;
            body.GetOrAddComponent<MethadoneDepot>().Add(OpiateUnitsPerHundredMl * hundreds);
        }
    }
}
