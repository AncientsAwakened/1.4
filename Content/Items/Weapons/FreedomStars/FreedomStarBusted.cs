using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Weapons.FreedomStars
{
    public class FreedomStarBusted : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Broken Freedom Star");
            Tooltip.SetDefault("Needs a Repair");
        }
        public override void SetDefaults()
        {
            Item.maxStack = 1;
            Item.width = 22;
            Item.height = 24;
            Item.rare = ItemRarityID.Cyan;
        }
    }
}