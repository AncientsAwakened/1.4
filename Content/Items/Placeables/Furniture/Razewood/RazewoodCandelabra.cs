using AAMod.Content.Items.Bases;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Placeables.Furniture.Razewood
{
	public class RazewoodCandelabra : TileItem<Tiles.Furniture.Razewood.RazewoodCandelabra>
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Razewood Candelabra");

		public override void SafeSetDefaults()
		{
			Item.maxStack = 99;

			Item.width = 26;
			Item.height = 32;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Blocks.Razewood>(), 5).AddIngredient(ItemID.Torch, 3).AddTile(TileID.WorkBenches).Register();
		}
	}
}
