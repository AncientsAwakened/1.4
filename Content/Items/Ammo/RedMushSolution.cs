using AAMod.Content.Projectiles.Sprays;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Ammo
{
    public class RedMushSolution : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Red Mush Solution");
            Tooltip.SetDefault("Used by the Clentaminator" + "\nSpreads the Red Mushland");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
        }

        public override void SetDefaults()
        {
            Item.consumable = true;

            Item.width = 12;
            Item.height = 14;

            Item.maxStack = 999;
			Item.rare = ItemRarityID.Orange;

			Item.ammo = AmmoID.Solution;
            Item.shoot = ModContent.ProjectileType<RedMushSpray>() - ProjectileID.PureSpray;
        }
    }
}
