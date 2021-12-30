using AAMod.Content.Projectiles.Sprays;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Ammo
{
    public class InfernoSolution : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Inferno Solution");
            Tooltip.SetDefault("Used by the Clentaminator" + "\nSpreads the Inferno");

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
            Item.shoot = ModContent.ProjectileType<InfernoSpray>() - ProjectileID.PureSpray;
        }
    }
}
