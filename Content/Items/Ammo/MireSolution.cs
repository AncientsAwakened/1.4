using AAMod.Content.Projectiles.Sprays;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Ammo
{
    public class MireSolution : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mire Solution");
            Tooltip.SetDefault("Used by the Clentaminator" + "\nSpreads the Mire");

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
            Item.shoot = ModContent.ProjectileType<MireSpray>() - ProjectileID.PureSpray;
        }
    }
}
