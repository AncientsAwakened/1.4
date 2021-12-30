using AAMod.Content.Items.Placeables.Blocks;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Weapons.Melee
{
    public class RazewoodSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Razewood Sword");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = false;
            Item.useTurn = true;

            Item.damage = 10;
            Item.knockBack = 4f;

            Item.width = Item.height = 32;

            Item.useTime = Item.useAnimation = 24;
            Item.useStyle = ItemUseStyleID.Swing;

            Item.UseSound = SoundID.Item1;

            Item.value = Item.sellPrice(copper: 14);
        }

        public override void AddRecipes()
            => CreateRecipe()
            .AddIngredient(ModContent.ItemType<Razewood>(), 7)
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}
