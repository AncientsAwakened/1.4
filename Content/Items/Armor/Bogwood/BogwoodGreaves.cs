using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Armor.Bogwood
{
	[AutoloadEquip(EquipType.Legs)]
    public class BogwoodGreaves : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bogwood Greaves");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;

            Item.defense = 1;

            Item.value = Item.sellPrice(copper: 50);
        }

        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ModContent.ItemType<Placeables.Blocks.Bogwood>(), 25).AddTile(TileID.WorkBenches).Register();
        }
    }
}
