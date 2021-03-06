using AAMod.Content.Items.Bases;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Placeables.Furniture.Bogwood
{
	public class BogwoodSofa : TileItem<Tiles.Furniture.Bogwood.BogwoodSofa>
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Bogwood Sofa");

		public override void SafeSetDefaults()
		{
			Item.maxStack = 99;

			Item.width = 38;
			Item.height = 24;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Blocks.Bogwood>(), 5).AddIngredient(ItemID.Silk, 2).AddTile(TileID.Sawmill).Register();
		}
	}
}
