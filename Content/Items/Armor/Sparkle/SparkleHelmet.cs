using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Armor.Sparkle
{
    [AutoloadEquip(EquipType.Head)]
    public class SparkleHelmet : ModItem
    {
		public override string Texture => AAMod.PlaceholderTexture;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sparkle Helmet");
			Tooltip.SetDefault("+1 max minions");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
        {
            Item.width = Item.height = 22;

            Item.defense = 1;

            Item.value = Item.sellPrice(copper: 40);
			Item.rare = ItemRarityID.Blue;
		}
		public override void UpdateEquip(Player player) => player.maxMinions++;

		public override bool IsArmorSet(Item head, Item body, Item legs) => body.type == ModContent.ItemType<SparkleBreastplate>() && legs.type == ModContent.ItemType<SparkleGreaves>();

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "1 defense" + "\n10% reduced mana usage";
			player.statDefense += 1;
			player.manaCost -= 0.1f;
		}

		public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ItemID.FallenStar, 15).AddTile(TileID.WorkBenches).Register();
        }
    }
}
