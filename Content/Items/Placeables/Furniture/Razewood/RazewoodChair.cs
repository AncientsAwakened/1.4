using AAMod.Content.Items.Bases;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Placeables.Furniture.Razewood
{
	public class RazewoodChair : TileItem<Tiles.Furniture.Razewood.RazewoodChair>
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Razewood Chair");

		public override void SafeSetDefaults()
		{
			Item.maxStack = 99;

			Item.width = 16;
			Item.height = 32;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Blocks.Razewood>(), 4).AddTile(TileID.WorkBenches).Register();
		}
	}
}
