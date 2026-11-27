using Unity.VisualScripting;
using UnityEngine;
using UnknownDrugs.Core;
using UnknownDrugs.Effects;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class Codeine : Drug
    {
        private const float OpiateUnitsPerHundredMl = 45f;

        public override string Id => "codeine";

        public override Color Color => new Color(0.85f, 0.7f, 0.5f);

        public override float ValuePerLiter => 50f;

        public override float InteractionSeconds(float ml) => 600f;

        public override DrugText Text { get; } = new DrugText(
            "Codeine",
            "Natural opium alkaloid, isolated in 1832. The body converts part of it into morphine, so the relief arrives with a delay.",
            "Кодеин",
            "Природный алкалоид опия, выделенный в 1832 году. Часть его организм превращает в морфин, поэтому облегчение приходит с задержкой.");

        public override void Apply(float ml, Body body)
        {
            float hundreds = ml * 0.01f;
            body.GetOrAddComponent<CodeineDepot>().Add(OpiateUnitsPerHundredMl * hundreds);
        }
    }
}
