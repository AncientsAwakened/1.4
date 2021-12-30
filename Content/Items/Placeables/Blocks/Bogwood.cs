using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Placeables.Blocks
{
	public class Bogwood : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bogwood");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.consumable = true;

            Item.maxStack = 999;
            Item.width = 18;
            Item.height = 24;

            Item.useTime = 10;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;

            Item.createTile = ModContent.TileType<Tiles.Biomes.Mire.Bogwood>();
        }
    }
}
