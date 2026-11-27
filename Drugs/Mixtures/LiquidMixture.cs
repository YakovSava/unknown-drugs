using System.Collections.Generic;
using UnknownDrugs.Core;

namespace UnknownDrugs.Drugs
{
    public abstract class LiquidMixture : Drug
    {
        public override bool IsCraftOnly => true;

        protected abstract IReadOnlyList<MixturePart> Parts { get; }

        public override void Apply(float ml, Body body)
        {
            foreach (MixturePart part in Parts)
            {
                if (!Liquids.Registry.TryGetValue(part.Id, out LiquidType liquid))
                {
                    continue;
                }
                liquid.onDrink?.Invoke(ml * part.Share, body);
            }
        }
    }
}
