using AAMod.Content.Dusts;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Materials
{
    public class AbyssiumOre : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Abyssium Ore");
            Tooltip.SetDefault("'It feels cold in your hands'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.consumable = true;

            Item.maxStack = 999;
            Item.width = 22;
            Item.height = 20;

            Item.useTime = 10;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;

            Item.rare = ItemRarityID.Blue;

            Item.createTile = ModContent.TileType<Tiles.Ores.AbyssiumOre>();
        }

        public override void PostUpdate()
        {
            if (Main.rand.NextBool(50))
            {
                Dust.NewDust(Item.position, Item.width, Item.height, ModContent.DustType<Incinerite>());
            }
        }
    }
}
