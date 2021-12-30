using AAMod.Content.Items.Bases;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Placeables.Furniture.Razewood
{
	public class RazewoodPlatform : TileItem<Tiles.Furniture.Razewood.RazewoodPlatform>
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Razewood Platform");

		public override void SafeSetDefaults()
		{
			Item.maxStack = 999;

			Item.width = 22;
			Item.height = 14;
		}

		public override void AddRecipes()
		{
			CreateRecipe(2).AddIngredient(ModContent.ItemType<Blocks.Razewood>()).Register();
		}
	}
}
