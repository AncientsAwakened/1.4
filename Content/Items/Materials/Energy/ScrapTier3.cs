using AAMod.Common.Globals;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Materials.Energy
{
    public class ScrapTier3 : ModItem
    {
        public override string Texture => "AAMod/Content/Items/Materials/Energy/Scraps";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scraps");
            Tooltip.SetDefault("Use with Caution");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
        }
        public override void SetDefaults()
        {
            Item.maxStack = 1;
            Item.width = 20;
            Item.height = 22;
            Item.rare = ItemRarityID.Cyan;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine tooltip = new(Mod, "AAScraps: ScrapLevel", $"Tier: {Item.GetGlobalItem<AAGlobalItemModular>().ScrapLevel}") { overrideColor = Color.Yellow };
            tooltips.Add(tooltip);
        }
    }
}