using AAMod.Content.Items.Bases;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Placeables.Furniture.Razewood
{
	public class RazewoodBed : TileItem<Tiles.Furniture.Razewood.RazewoodBed>
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Razewood Bed");

		public override void SafeSetDefaults()
		{
			Item.maxStack = 99;

			Item.width = 34;
			Item.height = 14;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Blocks.Razewood>(), 15).AddIngredient(ItemID.Silk, 5).AddTile(TileID.Sawmill).Register();
		}
	}
}
