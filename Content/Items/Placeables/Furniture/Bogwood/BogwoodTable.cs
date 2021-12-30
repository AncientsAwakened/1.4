using AAMod.Content.Items.Bases;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Placeables.Furniture.Bogwood
{
	public class BogwoodTable : TileItem<Tiles.Furniture.Bogwood.BogwoodTable>
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Bogwood Table");

		public override void SafeSetDefaults()
		{
			Item.maxStack = 99;

			Item.width = 38;
			Item.height = 14;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Blocks.Bogwood>(), 8).AddTile(TileID.WorkBenches).Register();
		}
	}
}
