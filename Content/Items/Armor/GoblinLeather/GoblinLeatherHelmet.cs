using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Armor.GoblinLeather
{
    [AutoloadEquip(EquipType.Head)]
    public class GoblinLeatherHelmet : ModItem
    {
        public override string Texture => AAMod.PlaceholderTexture;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Goblin Leather Helmet");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = Item.height = 22;

            Item.defense = 3;

            Item.value = Item.sellPrice(copper: 40);
			Item.rare = ItemRarityID.Green;
		}

        public override bool IsArmorSet(Item head, Item body, Item legs) => body.type == ModContent.ItemType<GoblinLeatherBreastplate>() && legs.type == ModContent.ItemType<GoblinLeatherGreaves>();

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "8 defense" + "\nKnockback immunity";
			player.statDefense += 8;
			player.noKnockback = true; 
		}

		public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ModContent.ItemType<Materials.ToughLeather>(), 15).AddTile(TileID.WorkBenches).Register();
        }
    }
}
