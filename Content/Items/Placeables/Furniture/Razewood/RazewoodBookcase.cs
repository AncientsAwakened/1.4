using AAMod.Content.Items.Bases;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Placeables.Furniture.Razewood
{
	public class RazewoodBookcase : TileItem<Tiles.Furniture.Razewood.RazewoodBookcase>
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Razewood Bookcase");

		public override void SafeSetDefaults()
		{
			Item.maxStack = 99;

			Item.width = 26;
			Item.height = 34;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Blocks.Razewood>(), 20).AddIngredient(ItemID.Book, 10).AddTile(TileID.Sawmill).Register();
		}
	}
}
