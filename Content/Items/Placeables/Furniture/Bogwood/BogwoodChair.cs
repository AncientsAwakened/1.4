using AAMod.Content.Items.Bases;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Placeables.Furniture.Bogwood
{
	public class BogwoodChair : TileItem<Tiles.Furniture.Bogwood.BogwoodChair>
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Bogwood Chair");

		public override void SafeSetDefaults()
		{
			Item.maxStack = 99;

			Item.width = 16;
			Item.height = 18;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Blocks.Bogwood>(), 4).AddTile(TileID.WorkBenches).Register();
		}
	}
}
