using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Tools.Razewood
{
    public class RazewoodHammer : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Razewood Hammer");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.DamageType = DamageClass.Melee;

            Item.damage = 4;
            Item.knockBack = 5.5f;

            Item.width = Item.height = 32;

            Item.hammer = 25;

            Item.useTime = 24;
            Item.useAnimation = 34;
            Item.useStyle = ItemUseStyleID.Swing;

            Item.UseSound = SoundID.Item1;

            Item.value = Item.sellPrice(copper: 16);
        }

        public override void AddRecipes()
            => CreateRecipe()
            .AddIngredient(ModContent.ItemType<Placeables.Blocks.Razewood>(), 8)
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}
