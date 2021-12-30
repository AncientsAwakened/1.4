using AAMod.Content.Items.Bases;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Placeables.Furniture.Bogwood
{
	public class BogwoodPlatform : TileItem<Tiles.Furniture.Bogwood.BogwoodPlatform>
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Bogwood Platform");

		public override void SafeSetDefaults()
		{
			Item.maxStack = 999;

			Item.width = 22;
			Item.height = 14;
		}

		public override void AddRecipes()
		{
			CreateRecipe(2).AddIngredient(ModContent.ItemType<Blocks.Bogwood>()).Register();
		}
	}
}
