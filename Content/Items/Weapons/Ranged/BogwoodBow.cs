using AAMod.Content.Items.Placeables.Blocks;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Weapons.Ranged
{
    public class BogwoodBow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bogwood Bow");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.noMelee = true;

            Item.DamageType = DamageClass.Ranged;
            Item.damage = 8;
            Item.knockBack = 0.5f;

            Item.width = 24;
            Item.height = 42;

            Item.useTime = Item.useAnimation = 26;
            Item.useStyle = ItemUseStyleID.Shoot;

            Item.shootSpeed = 7.5f;
            Item.shoot = ProjectileID.WoodenArrowFriendly;

            Item.useAmmo = AmmoID.Arrow;
            Item.UseSound = SoundID.Item5;

            Item.value = Item.sellPrice(copper: 20);
        }

        public override void AddRecipes()
            => CreateRecipe()
            .AddIngredient(ModContent.ItemType<Bogwood>(), 10)
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}
