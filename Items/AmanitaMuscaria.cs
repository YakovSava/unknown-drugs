using UnityEngine;
using UnknownDrugs.Effects;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Items
{
    public sealed class AmanitaMuscaria : CustomItem
    {
        public const string ItemId = "amanitamuscaria";

        private const float FoodValue = 5f;
        private const float FatValue = 0f;
        private const float HappinessGain = 30f;
        private const float BrainDamage = 5f;
        private const float SicknessGain = 5f;
        private const float VomitChance = 0.35f;

        public override string Id => ItemId;

        public override float Weight => 0.4f;

        public override int Value => 12;

        public override int Recognition => 4;

        public override DrugText Text { get; } = new DrugText(
            "Amanita muscaria",
            "A mushroom with a red cap and white flecks. It looks exactly like the other fungus, which is either reassuring or not.",
            "Мухомор",
            "Гриб с красной шляпкой и белыми крапинками. Выглядит в точности как остальная грибная масса — это либо успокаивает, либо нет.");

        public override void Use(Body body, Item item)
        {
            item.condition -= 1f;
            Sound.Play("eatFlesh", body.transform.position);
            body.Eat(FoodValue, FatValue, 0);
            body.happiness += HappinessGain;
            body.brainHealth -= BrainDamage;
            body.sicknessAmount += SicknessGain;
            if (Random.value < VomitChance)
            {
                body.vomiter.Vomit();
            }
            AmanitaTrip.Start(body);
        }
    }
}
