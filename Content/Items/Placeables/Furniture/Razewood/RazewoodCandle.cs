using AAMod.Content.Items.Bases;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Placeables.Furniture.Razewood
{
	public class RazewoodCandle : TileItem<Tiles.Furniture.Razewood.RazewoodCandle>
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Razewood Candle");

		public override void SafeSetDefaults()
		{
			Item.maxStack = 99;

			Item.width = 16;
			Item.height = 18;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Blocks.Razewood>(), 4).AddIngredient(ItemID.Torch).AddTile(TileID.WorkBenches).Register();
		}
	}
}
