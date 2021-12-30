using AAMod.Content.Items.Bases;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Placeables.Furniture.Razewood
{
	public class RazewoodClock : TileItem<Tiles.Furniture.Razewood.RazewoodClock>
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Razewood Clock");

		public override void SafeSetDefaults()
		{
			Item.maxStack = 99;

			Item.width = 18;
			Item.height = 40;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Blocks.Razewood>(), 10).AddIngredient(ItemID.Glass, 6).AddRecipeGroup("IronBar", 3).AddTile(TileID.Sawmill).Register();
		}
	}
}
