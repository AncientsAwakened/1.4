using AAMod.Content.Items.Bases;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Placeables.Furniture.Razewood
{
	public class RazewoodSofa : TileItem<Tiles.Furniture.Razewood.RazewoodSofa>
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Razewood Sofa");

		public override void SafeSetDefaults()
		{
			Item.maxStack = 99;

			Item.width = 38;
			Item.height = 22;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Blocks.Razewood>(), 5).AddIngredient(ItemID.Silk, 2).AddTile(TileID.Sawmill).Register();
		}
	}
}
