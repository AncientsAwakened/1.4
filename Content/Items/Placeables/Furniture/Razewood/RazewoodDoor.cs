using AAMod.Content.Items.Bases;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Placeables.Furniture.Razewood
{
	public class RazewoodDoor : TileItem<Tiles.Furniture.Razewood.RazewoodDoorClosed>
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Razewood Door");

		public override void SafeSetDefaults()
		{
			Item.maxStack = 99;

			Item.width = 20;
			Item.height = 34;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<Blocks.Razewood>(), 6).AddTile(TileID.WorkBenches).Register();
		}
	}
}
