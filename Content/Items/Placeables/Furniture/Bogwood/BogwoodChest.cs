using AAMod.Content.Items.Bases;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Placeables.Furniture.Bogwood
{
	public class BogwoodChest : TileItem<Tiles.Furniture.Bogwood.BogwoodChest>
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Bogwood Chest");

		public override void SafeSetDefaults()
		{
			Item.maxStack = 99;

			Item.width = 32;
			Item.height = 28;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Blocks.Bogwood>(), 8).AddRecipeGroup("IronBar", 2).AddTile(TileID.WorkBenches).Register();
		}
	}
}
