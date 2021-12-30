using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.Linq;

namespace AAMod.Content.Items.Consumables
{
    public class HoneycombChunk : ModItem
	{
		public override string Texture => AAMod.PlaceholderTexture;
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Gives the Honey buff.");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 20;
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 26;
			Item.useStyle = ItemUseStyleID.EatFood;
			Item.useAnimation = 15;
			Item.useTime = 15;
			Item.useTurn = true;
			Item.UseSound = SoundID.Item3;
			Item.maxStack = 30;
			Item.consumable = true;
			Item.rare = ItemRarityID.Orange;
			Item.value = Item.buyPrice(gold: 1);

			Item.buffType = BuffID.Honey;
			Item.buffTime = 1800;

			Item.healLife = 25;
			Item.potion = true;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipLine line = tooltips.FirstOrDefault(x => x.mod == "Terraria" && x.Name == "HealLife");
		}
	}
}