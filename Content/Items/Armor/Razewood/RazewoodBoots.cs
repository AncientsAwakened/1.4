using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Armor.Razewood
{
	[AutoloadEquip(EquipType.Legs)]
    public class RazewoodBoots : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Razewood Boots");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 16;

            Item.defense = 1;

            Item.value = Item.sellPrice(copper: 50);
        }

        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ModContent.ItemType<Placeables.Blocks.Razewood>(), 25).AddTile(TileID.WorkBenches).Register();
        }
    }
}
