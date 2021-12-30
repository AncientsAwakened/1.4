using AAMod.Content.Items.Bases;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Placeables.Furniture.Bogwood
{
	public class BogwoodLamp : TileItem<Tiles.Furniture.Bogwood.BogwoodLamp>
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Bogwood Lamp");

		public override void SafeSetDefaults()
		{
			Item.maxStack = 99;

			Item.width = 12;
			Item.height = 34;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Blocks.Bogwood>(), 3).AddIngredient(ItemID.Torch).AddTile(TileID.WorkBenches).Register();
		}
	}
}
