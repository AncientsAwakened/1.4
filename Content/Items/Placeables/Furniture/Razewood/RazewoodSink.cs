using AAMod.Content.Items.Bases;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Placeables.Furniture.Razewood
{
	public class RazewoodSink : TileItem<Tiles.Furniture.Razewood.RazewoodSink>
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Razewood Sink");

		public override void SafeSetDefaults()
		{
			Item.maxStack = 99;

			Item.width = 28;
			Item.height = 32;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Blocks.Razewood>(), 6).AddIngredient(ItemID.WaterBucket).AddTile(TileID.WorkBenches).Register();
		}
	}
}
