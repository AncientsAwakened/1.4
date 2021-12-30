using AAMod.Content.Items.Bases;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Placeables.Furniture.Bogwood
{
	public class BogwoodCandelabra : TileItem<Tiles.Furniture.Bogwood.BogwoodCandelabra>
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Bogwood Candelabra");

		public override void SafeSetDefaults()
		{
			Item.maxStack = 99;

			Item.width = 26;
			Item.height = 32;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Blocks.Bogwood>(), 5).AddIngredient(ItemID.Torch, 3).AddTile(TileID.WorkBenches).Register();
		}
	}
}
