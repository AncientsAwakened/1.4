using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Armor.Razewood
{
	[AutoloadEquip(EquipType.Head)]
    public class RazewoodHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Razewood Helmet");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 22;

            Item.defense = 1;

            Item.value = Item.sellPrice(copper: 40);
        }

        public override bool IsArmorSet(Item head, Item body, Item legs) => body.type == ModContent.ItemType<RazewoodChestplate>() && legs.type == ModContent.ItemType<RazewoodBoots>();

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "1 defense";
			player.statDefense += 1;
		}


		public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ModContent.ItemType<Placeables.Blocks.Razewood>(), 20).AddTile(TileID.WorkBenches).Register();
        }
    }
}
