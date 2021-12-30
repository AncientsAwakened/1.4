using AAMod.Content.Items.Bases;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Placeables.Furniture.Bogwood
{
	public class BogwoodChandelier : TileItem<Tiles.Furniture.Bogwood.BogwoodChandelier>
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Bogwood Chandelier");

		public override void SafeSetDefaults()
		{
			Item.maxStack = 99;

			Item.width = 34;
			Item.height = 34;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Blocks.Bogwood>(), 4).AddIngredient(ItemID.Torch, 4).AddIngredient(ItemID.Chain).AddTile(TileID.WorkBenches).Register();
		}
	}
}
