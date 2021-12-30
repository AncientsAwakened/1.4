using AAMod.Content.Items.Placeables.Blocks;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Weapons.Ranged
{
    public class RazewoodBow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Razewood Bow");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.noMelee = true;

            Item.DamageType = DamageClass.Ranged;
            Item.damage = 10;
            Item.knockBack = 1f;

            Item.width = 24;
            Item.height = 42;

            Item.useTime = Item.useAnimation = 28;
            Item.useStyle = ItemUseStyleID.Shoot;

            Item.shootSpeed = 7f;
            Item.shoot = ProjectileID.WoodenArrowFriendly;

            Item.useAmmo = AmmoID.Arrow;
            Item.UseSound = SoundID.Item5;

            Item.value = Item.sellPrice(copper: 20);
        }

        public override void AddRecipes()
            => CreateRecipe()
            .AddIngredient(ModContent.ItemType<Razewood>(), 10)
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}
