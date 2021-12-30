using AAMod.Content.Items.Bases;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Placeables.Furniture.Bogwood
{
	public class BogwoodBathtub : TileItem<Tiles.Furniture.Bogwood.BogwoodBathtub>
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Bogwood Bathtub");

		public override void SafeSetDefaults()
		{
			Item.maxStack = 99;

			Item.width = 34;
			Item.height = 16;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Blocks.Bogwood>(), 14).AddTile(TileID.Sawmill).Register();
		}
	}
}
