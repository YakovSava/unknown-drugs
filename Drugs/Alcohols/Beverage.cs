using Unity.VisualScripting;
using UnityEngine;
using UnknownDrugs.Core;
using UnknownDrugs.Effects;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public sealed class Beverage : Drug
    {
        private const float HydrationPerLiter = 70f;
        private const float BadBatchMultiplier = 10f;

        private readonly string id;
        private readonly Color color;
        private readonly float valuePerLiter;
        private readonly float strength;
        private readonly float methanolShare;
        private readonly float badBatchChance;
        private readonly bool lethalForCans;

        public Beverage(
            string id,
            Color color,
            float valuePerLiter,
            float strength,
            DrugText text,
            float methanolShare = 0f,
            float badBatchChance = 0f,
            bool lethalForCans = false)
        {
            this.id = id;
            this.color = color;
            this.valuePerLiter = valuePerLiter;
            this.strength = strength;
            this.methanolShare = methanolShare;
            this.badBatchChance = badBatchChance;
            this.lethalForCans = lethalForCans;
            Text = text;
        }

        public override string Id => id;

        public override Color Color => color;

        public override float ValuePerLiter => valuePerLiter;

        public override DrugText Text { get; }

        public override bool Injectable => false;

        public override bool IsLethalForCans => lethalForCans;

        public override void Apply(float ml, Body body)
        {
            body.GetOrAddComponent<Intoxication>().Add(Intoxication.PointsFor(ml, strength));
            body.Drink(ml * 0.001f * HydrationPerLiter * (1f - strength));
            float methanol = ml * MethanolShareOfBatch();
            if (methanol > 0f)
            {
                body.GetOrAddComponent<MethanolPoisoning>().Ingest(methanol);
            }
        }

        private float MethanolShareOfBatch()
        {
            bool badBatch = Random.value < badBatchChance;
            return badBatch ? methanolShare * BadBatchMultiplier : methanolShare;
        }
    }
}
