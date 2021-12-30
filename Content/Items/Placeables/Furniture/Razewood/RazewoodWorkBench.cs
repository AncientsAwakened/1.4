using AAMod.Content.Items.Bases;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Placeables.Furniture.Razewood
{
	public class RazewoodWorkBench : TileItem<Tiles.Furniture.Razewood.RazewoodWorkBench>
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Razewood Work Bench");

		public override void SafeSetDefaults()
		{
			Item.maxStack = 99;

			Item.width = 32;
			Item.height = 16;
		}
		
		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Blocks.Razewood>(), 10).Register();
		}
	}
}
