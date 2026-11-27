using UnityEngine;
using UnknownDrugs.Effects;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Items
{
    public sealed class PsilocybeFungus : CustomItem
    {
        public const string ItemId = "psilocybefungus";

        private const float FoodValue = 8f;
        private const float FatValue = 0.1f;
        private const float HappinessGain = 8f;
        private const float HungerGain = 12f;
        private const float SicknessGain = 2f;
        private const float VomitChance = 0.15f;

        public override string Id => ItemId;

        public override float Weight => 0.4f;

        public override int Value => 6;

        public override int Recognition => 5;

        public override DrugText Text { get; } = new DrugText(
            "Psilocybe Fungus",
            "A piece of fungus with a faint blue bruise and a familiar shape. Eating it makes the world softer and the stomach uncertain.",
            "Псилоцибе",
            "Кусок гриба с едва заметной синевой и знакомой формой. Если его съесть, мир станет мягче, а желудок — подозрительнее.");

        public override void Use(Body body, Item item)
        {
            item.condition -= 1f;
            Sound.Play("eatFlesh", body.transform.position);
            body.Eat(FoodValue, FatValue, 0);
            body.happiness += HappinessGain;
            body.hunger += HungerGain;
            body.sicknessAmount += SicknessGain;
            if (Random.value < VomitChance)
            {
                body.vomiter.Vomit();
            }
            PsilocybeTrip.Start(body);
        }
    }
}
