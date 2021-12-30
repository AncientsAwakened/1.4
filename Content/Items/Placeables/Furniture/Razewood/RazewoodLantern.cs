using AAMod.Content.Items.Bases;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Placeables.Furniture.Razewood
{
	public class RazewoodLantern : TileItem<Tiles.Furniture.Razewood.RazewoodLantern>
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Razewood Lantern");

		public override void SafeSetDefaults()
		{
			Item.maxStack = 99;

			Item.width = 16;
			Item.height = 32;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Blocks.Razewood>(), 6).AddIngredient(ItemID.Torch).AddTile(TileID.WorkBenches).Register();
		}
	}
}
