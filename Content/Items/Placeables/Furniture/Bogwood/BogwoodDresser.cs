using AAMod.Content.Items.Bases;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Placeables.Furniture.Bogwood
{
	public class BogwoodDresser : TileItem<Tiles.Furniture.Bogwood.BogwoodDresser>
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Bogwood Dresser");

		public override void SafeSetDefaults()
		{
			Item.maxStack = 99;

			Item.width = 38;
			Item.height = 24;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Blocks.Bogwood>(), 16).AddTile(TileID.Sawmill).Register();
		}
	}
}
