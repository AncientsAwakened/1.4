using AAMod.Content.Items.Materials;
using AAMod.Content.Tiles.Energy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Placeables.Energy
{
	class GrindstoneItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Grindstone");
			Tooltip.SetDefault("Gives the player scraps from unwanted treasure bags");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<Grindstone>());
			Item.width = 32;
			Item.height = 32;
			Item.maxStack = 99;
			Item.rare = ItemRarityID.Blue;
			Item.value = 0;
		}
		public override void AddRecipes()
		{
			CreateRecipe().AddIngredient(ItemID.WorkBench).AddIngredient(ItemID.StoneBlock, 20).AddTile(TileID.WorkBenches).Register();
		}
	}
}