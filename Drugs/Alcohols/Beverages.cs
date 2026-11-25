using System.Collections.Generic;
using UnityEngine;
using UnknownDrugs.Core;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Drugs
{
    public static class Beverages
    {
        public static IEnumerable<Drug> Create()
        {
            yield return new Beverage(
                "vodka",
                new Color(0.93f, 0.97f, 1f),
                25f,
                0.4f,
                new DrugText(
                    "Vodka",
                    "Distilled grain spirit, forty percent. Warms from the inside.",
                    "Водка",
                    "Перегнанный зерновой спирт, сорок градусов. Согревает изнутри."));
            yield return new Beverage(
                "wine",
                new Color(0.55f, 0.1f, 0.2f),
                20f,
                0.12f,
                new DrugText(
                    "Wine",
                    "Fermented grape juice. Old bottles keep better than their labels.",
                    "Вино",
                    "Забродивший виноградный сок. Старые бутылки сохраняются лучше своих этикеток."));
            yield return new Beverage(
                "beer",
                new Color(0.9f, 0.7f, 0.2f),
                10f,
                0.05f,
                new DrugText(
                    "Beer",
                    "Weak, bitter and nearly liquid bread.",
                    "Пиво",
                    "Слабое, горькое и почти жидкий хлеб."));
            yield return new Beverage(
                "cognac",
                new Color(0.7f, 0.4f, 0.1f),
                45f,
                0.4f,
                new DrugText(
                    "Cognac",
                    "Grape brandy aged in oak barrels.",
                    "Коньяк",
                    "Виноградный бренди, выдержанный в дубовых бочках."));
            yield return new Beverage(
                "moonshine",
                new Color(0.95f, 0.95f, 0.85f),
                8f,
                0.45f,
                new DrugText(
                    "Moonshine",
                    "Home-distilled spirit of uncertain recipe. Everyone's uncle swears by it.",
                    "Самогон",
                    "Домашний перегон неизвестного рецепта. Чей-нибудь дядя им клянётся."),
                methanolShare: 0.01f,
                badBatchChance: 0.1f);
            yield return new Beverage(
                "grandpastincture",
                new Color(0.35f, 0.2f, 0.08f),
                15f,
                0.5f,
                new DrugText(
                    "Grandpa's tincture",
                    "Dark herbal tincture from an unlabeled jar. The recipe stayed in the family.",
                    "Дедова настойка",
                    "Тёмная травяная настойка из банки без этикетки. Рецепт остался в семье."),
                methanolShare: 0.2f,
                lethalForCans: true);
        }
    }
}
