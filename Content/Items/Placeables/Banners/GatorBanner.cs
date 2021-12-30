using AAMod.Content.Tiles;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Placeables.Banners
{
    public class GatorBanner : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gator Banner");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.consumable = true;

            Item.maxStack = 99;
            Item.useTime = 10;
            Item.useAnimation = 15;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.createTile = ModContent.TileType<MonsterBanner>();
            Item.rare = ItemRarityID.Blue;

            Item.placeStyle = 0;
        }
    }
}