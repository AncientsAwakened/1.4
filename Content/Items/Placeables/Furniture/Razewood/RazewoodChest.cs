using AAMod.Content.Items.Bases;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Placeables.Furniture.Razewood
{
	public class RazewoodChest : TileItem<Tiles.Furniture.Razewood.RazewoodChest>
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Razewood Chest");

		public override void SafeSetDefaults()
		{
			Item.maxStack = 99;

			Item.width = 32;
			Item.height = 24;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Blocks.Razewood>(), 8).AddRecipeGroup("IronBar", 2).AddTile(TileID.WorkBenches).Register();
		}
	}
}
