using AAMod.Content.Items.Bases;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Placeables.Furniture.Bogwood
{
	public class BogwoodPiano : TileItem<Tiles.Furniture.Bogwood.BogwoodPiano>
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Bogwood Piano");

		public override void SafeSetDefaults()
		{
			Item.maxStack = 99;

			Item.width = 38;
			Item.height = 24;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Blocks.Bogwood>(), 15).AddIngredient(ItemID.Bone, 4).AddIngredient(ItemID.Book).AddTile(TileID.Sawmill).Register();
		}
	}
}
