using System.Collections.Generic;
using CUCoreLib.Data;
using CUCoreLib.Registries;
using UnityEngine;

namespace UnknownDrugs.Items
{
    public static class ItemCatalog
    {
        private const string FungusTemplateId = "funguschunk";
        private const string ItemCategory = "custom";
        private const string ItemTags = "cangetwet";
        private const float SlotRotation = 0f;

        private static readonly IReadOnlyList<CustomItem> Items = new CustomItem[]
        {
            new PsilocybeFungus(),
            new AmanitaMuscaria()
        };

        public static IReadOnlyList<CustomItem> All => Items;

        public static bool TryRegister()
        {
            Sprite icon = LoadFungusIcon();
            if (icon == null)
            {
                return false;
            }
            foreach (CustomItem definition in Items)
            {
                Register(definition, icon);
            }
            return true;
        }

        private static Sprite LoadFungusIcon()
        {
            GameObject template = Resources.Load<GameObject>(FungusTemplateId);
            if (template == null)
            {
                return null;
            }
            SpriteRenderer renderer = template.GetComponent<SpriteRenderer>();
            return renderer == null ? null : renderer.sprite;
        }

        private static void Register(CustomItem definition, Sprite icon)
        {
            CustomItemInfo info = new CustomItemInfo
            {
                fullName = definition.Text.Name,
                description = definition.Text.Description,
                category = ItemCategory,
                slotRotation = SlotRotation,
                usable = true,
                usableOnLimb = false,
                destroyAtZeroCondition = true,
                combineable = true,
                weight = definition.Weight,
                scaleWeightWithCondition = true,
                value = definition.Value,
                tags = ItemTags,
                rec = new Recognition(definition.Recognition)
            };
            info.useAction = (Body body, Item item) => definition.Use(body, item);
            ItemRegistry.Register(definition.Id, info, icon, 0);
        }
    }
}
