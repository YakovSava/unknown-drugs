using Unity.VisualScripting;
using UnityEngine;
using UnknownDrugs.Core;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class Carfentanil : Drug
    {
        private const float OpiateUnitsPerMl = 1500f;

        public override string Id => "carfentanil";

        public override Color Color => new Color(0.78f, 0.93f, 0.98f);

        public override float ValuePerLiter => 4000f;

        public override bool IsLethalForCans => true;

        public override DrugText Text { get; } = new DrugText(
            "Carfentanil",
            "Opioid made in 1974 to sedate elephants. Roughly ten thousand times stronger than morphine. A single milliliter is enough to stop your breathing.",
            "Карфентанил",
            "Опиоид, созданный в 1974 году для усыпления слонов. Примерно в десять тысяч раз сильнее морфина. Одного миллилитра достаточно, чтобы остановить дыхание.");

        public override void Apply(float ml, Body body)
        {
            body.GetOrAddComponent<Painkillers>().opiateAmount += ml * OpiateUnitsPerMl;
        }
    }
}
