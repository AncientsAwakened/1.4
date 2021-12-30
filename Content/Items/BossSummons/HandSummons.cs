using AAMod.Content.NPCs.Bosses.Grips;
using AAMod.Content.Projectiles.Typeless;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.BossSummons
{
    class HandSummons : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chaotic Extract");
			Tooltip.SetDefault("100% pure chaos biome natural oils and minerals extract. now comes with 99% more demonic sentient hands!");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
			ItemID.Sets.SortingPriorityBossSpawns[Type] = 12;
		}
		public override void SetDefaults()
		{
			Item.width = 44;
			Item.height = 54;
			Item.maxStack = 20;
			Item.value = 100;
			Item.rare = ItemRarityID.Blue;
			Item.useAnimation = 30;
			Item.useTime = 30;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.noUseGraphic = true;
			Item.shoot = ModContent.ProjectileType<CocktailSummon>();
			Item.shootSpeed = 6f;
		}
		public override void AddRecipes()
		{
			CreateRecipe().AddIngredient(ItemID.Bottle, 1).AddIngredient(ItemID.Deathweed, 3)/*.AddIngredient(ModContent.ItemType<MireItemIdea>(), 5)*//*.AddIngredient(ModContent.ItemType<InfernoItemIdea>(), 5)*/.AddTile(TileID.DemonAltar).Register();
		}
	}
}
