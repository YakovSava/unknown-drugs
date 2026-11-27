using Unity.VisualScripting;
using UnknownDrugs.Effects;

namespace UnknownDrugs.Core
{
    public static class VanillaAlcoholHook
    {
        private const string AlcoholId = "alcohol";
        private const float AssumedStrength = 0.7f;

        public static void Install()
        {
            if (!Liquids.Registry.TryGetValue(AlcoholId, out LiquidType alcohol))
            {
                return;
            }
            LiquidType.OnDrink original = alcohol.onDrink;
            alcohol.onDrink = (float ml, Body body) =>
            {
                original(ml, body);
                body.GetOrAddComponent<Intoxication>().Add(Intoxication.PointsFor(ml, AssumedStrength));
                SubstanceTracker.Mark(body, AlcoholId, 300f);
            };
        }
    }
}
