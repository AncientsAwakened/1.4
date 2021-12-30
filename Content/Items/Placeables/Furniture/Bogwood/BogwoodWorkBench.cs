using AAMod.Content.Items.Bases;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Placeables.Furniture.Bogwood
{
	public class BogwoodWorkBench : TileItem<Tiles.Furniture.Bogwood.BogwoodWorkBench>
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Bogwood Work Bench");

		public override void SafeSetDefaults()
		{
			Item.maxStack = 99;

			Item.width = 32;
			Item.height = 16;
		}
		
		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Blocks.Bogwood>(), 10).Register();
		}
	}
}
