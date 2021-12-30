using AAMod.Content.Items.Bases;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Placeables.Furniture.Razewood
{
	public class RazewoodDresser : TileItem<Tiles.Furniture.Razewood.RazewoodDresser>
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Razewood Dresser");

		public override void SafeSetDefaults()
		{
			Item.maxStack = 99;

			Item.width = 38;
			Item.height = 22;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Blocks.Razewood>(), 16).AddTile(TileID.Sawmill).Register();
		}
	}
}
