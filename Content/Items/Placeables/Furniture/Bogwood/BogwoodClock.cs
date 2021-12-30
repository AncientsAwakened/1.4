using AAMod.Content.Items.Bases;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Placeables.Furniture.Bogwood
{
	public class BogwoodClock : TileItem<Tiles.Furniture.Bogwood.BogwoodClock>
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Bogwood Clock");

		public override void SafeSetDefaults()
		{
			Item.maxStack = 99;

			Item.width = 26;
			Item.height = 36;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Blocks.Bogwood>(), 10).AddIngredient(ItemID.Glass, 6).AddRecipeGroup("IronBar", 3).AddTile(TileID.Sawmill).Register();
		}
	}
}
