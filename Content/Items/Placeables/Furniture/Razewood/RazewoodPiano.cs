using AAMod.Content.Items.Bases;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Placeables.Furniture.Razewood
{
	public class RazewoodPiano : TileItem<Tiles.Furniture.Razewood.RazewoodPiano>
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Razewood Piano");

		public override void SafeSetDefaults()
		{
			Item.maxStack = 99;

			Item.width = 38;
			Item.height = 24;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Blocks.Razewood>(), 15).AddIngredient(ItemID.Bone, 4).AddIngredient(ItemID.Book).AddTile(TileID.Sawmill).Register();
		}
	}
}
