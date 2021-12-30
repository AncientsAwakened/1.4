using AAMod.Content.Items.Bases;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Placeables.Furniture.Razewood
{
	public class RazewoodTable : TileItem<Tiles.Furniture.Razewood.RazewoodTable>
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Razewood Table");

		public override void SafeSetDefaults()
		{
			Item.maxStack = 99;

			Item.width = 38;
			Item.height = 24;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Blocks.Razewood>(), 8).AddTile(TileID.WorkBenches).Register();
		}
	}
}
