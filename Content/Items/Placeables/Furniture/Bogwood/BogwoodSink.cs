using AAMod.Content.Items.Bases;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Placeables.Furniture.Bogwood
{
	public class BogwoodSink : TileItem<Tiles.Furniture.Bogwood.BogwoodSink>
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Bogwood Sink");

		public override void SafeSetDefaults()
		{
			Item.maxStack = 99;

			Item.width = 32;
			Item.height = 32;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Blocks.Bogwood>(), 6).AddIngredient(ItemID.WaterBucket).AddTile(TileID.WorkBenches).Register();
		}
	}
}
