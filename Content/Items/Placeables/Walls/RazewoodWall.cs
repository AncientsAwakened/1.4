using AAMod.Content.Walls.Biomes.Inferno;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Placeables.Walls
{
    public class RazewoodWall : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Razewood Wall");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 400;
        }

        public override void SetDefaults()
        {
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.consumable = true;

            Item.maxStack = 999;
            Item.width = Item.height = 32;

            Item.useTime = 10;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;

            Item.createWall = ModContent.WallType<Razewood>();
        }
    }
}
