using AAMod.Common.Globals;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Materials.Energy
{
    public class VoidOverclockUnit : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Overclock Unit");
            Tooltip.SetDefault("The surge still remains...");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 11));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.maxStack = 1;
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Cyan;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine tooltip = new(Mod, "AAOverclock: EnergyBar", $"Potential: {Item.GetGlobalItem<AAGlobalItemModular>().OverclockValue}") { overrideColor = Color.Red };
            tooltips.Add(tooltip);
        }
    }
}