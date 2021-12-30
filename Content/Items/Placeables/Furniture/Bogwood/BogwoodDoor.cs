using AAMod.Content.Items.Bases;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Placeables.Furniture.Bogwood
{
	public class BogwoodDoor : TileItem<Tiles.Furniture.Bogwood.BogwoodDoorClosed>
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Bogwood Door");

		public override void SafeSetDefaults()
		{
			Item.maxStack = 99;

			Item.width = 20;
			Item.height = 34;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Blocks.Bogwood>(), 6).AddTile(TileID.WorkBenches).Register();
		}
	}
}
