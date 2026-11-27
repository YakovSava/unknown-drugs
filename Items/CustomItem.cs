using UnknownDrugs.Localization;

namespace UnknownDrugs.Items
{
    public abstract class CustomItem
    {
        public abstract string Id { get; }

        public abstract float Weight { get; }

        public abstract int Value { get; }

        public abstract int Recognition { get; }

        public abstract DrugText Text { get; }

        public abstract void Use(Body body, Item item);
    }
}
