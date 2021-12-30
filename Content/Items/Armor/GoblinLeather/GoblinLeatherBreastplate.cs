using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Armor.GoblinLeather
{
    [AutoloadEquip(EquipType.Body)]
    public class GoblinLeatherBreastplate : ModItem
    {
        public override string Texture => AAMod.PlaceholderTexture;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Goblin Leather Breastplate");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 20;

            Item.defense = 5;

            Item.value = Item.sellPrice(copper: 60);
			Item.rare = ItemRarityID.Green;
		}

        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ModContent.ItemType<Materials.ToughLeather>(), 20).AddTile(TileID.WorkBenches).Register();
        }
    }
}
