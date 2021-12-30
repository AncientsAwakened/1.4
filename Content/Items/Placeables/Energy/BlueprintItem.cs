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
    class BlueprintItem : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blueprints");
			Tooltip.SetDefault("No Machine to Thrash Your own Ass Included");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<Blueprint>());
			Item.width = 32;
			Item.height = 32;
			Item.maxStack = 99;
			Item.rare = ItemRarityID.Green;
			Item.value = 0;
		}
		public override void AddRecipes()
		{
			CreateRecipe().AddIngredient(ItemID.TatteredCloth, 5).AddIngredient(ItemID.BlueDye).AddTile(TileID.Sawmill).Register();
		}
	}
}