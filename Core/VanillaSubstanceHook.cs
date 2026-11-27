using UnknownDrugs.Effects;

namespace UnknownDrugs.Core
{
    public static class VanillaSubstanceHook
    {
        private const float HeroinSeconds = 300f;
        private const float FentanylSeconds = 300f;
        private const float BleachSeconds = 120f;
        private const float AntidepressantSeconds = 600f;

        private static bool installed;

        public static void Install()
        {
            if (installed)
            {
                return;
            }
            installed = true;
            Wrap("heroin", HeroinSeconds);
            Wrap("fentanyl", FentanylSeconds);
            Wrap("bleach", BleachSeconds);
            Wrap("antidepressants", AntidepressantSeconds);
        }

        private static void Wrap(string id, float seconds)
        {
            if (!Liquids.Registry.TryGetValue(id, out LiquidType liquid))
            {
                return;
            }
            LiquidType.OnDrink drink = liquid.onDrink;
            liquid.onDrink = (ml, body) =>
            {
                drink?.Invoke(ml, body);
                SubstanceTracker.Mark(body, id, seconds);
            };
            LiquidType.OnHealthUse healthUse = liquid.onHealthUse;
            liquid.onHealthUse = (ml, limb) =>
            {
                healthUse?.Invoke(ml, limb);
                SubstanceTracker.Mark(limb.body, id, seconds);
            };
        }
    }
}
