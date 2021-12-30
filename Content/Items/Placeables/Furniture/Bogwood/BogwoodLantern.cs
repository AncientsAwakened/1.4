using AAMod.Content.Items.Bases;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Placeables.Furniture.Bogwood
{
	public class BogwoodLantern : TileItem<Tiles.Furniture.Bogwood.BogwoodLantern>
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Bogwood Lantern");

		public override void SafeSetDefaults()
		{
			Item.maxStack = 99;

			Item.width = 16;
			Item.height = 32;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Blocks.Bogwood>(), 6).AddIngredient(ItemID.Torch).AddTile(TileID.WorkBenches).Register();
		}
	}
}
