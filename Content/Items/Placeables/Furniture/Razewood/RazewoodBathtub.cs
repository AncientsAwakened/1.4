using AAMod.Content.Items.Bases;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Placeables.Furniture.Razewood
{
	public class RazewoodBathtub : TileItem<Tiles.Furniture.Razewood.RazewoodBathtub>
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Razewood Bathtub");

		public override void SafeSetDefaults()
		{
			Item.maxStack = 99;

			Item.width = 34;
			Item.height = 18;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Blocks.Razewood>(), 14).AddTile(TileID.Sawmill).Register();
		}
	}
}
