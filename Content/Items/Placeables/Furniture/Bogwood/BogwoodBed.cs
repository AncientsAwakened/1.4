using AAMod.Content.Items.Bases;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Placeables.Furniture.Bogwood
{
	public class BogwoodBed : TileItem<Tiles.Furniture.Bogwood.BogwoodBed>
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Bogwood Bed");

		public override void SafeSetDefaults()
		{
			Item.maxStack = 99;

			Item.width = 34;
			Item.height = 14;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Blocks.Bogwood>(), 15).AddIngredient(ItemID.Silk, 5).AddTile(TileID.Sawmill).Register();
		}
	}
}
