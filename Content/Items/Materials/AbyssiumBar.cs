using AAMod.Content.Dusts;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Materials
{
    public class AbyssiumBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Abyssium Bar");
            Tooltip.SetDefault("'Embued with Hydras' wrath'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
        }

        public override void SetDefaults()
        {
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.consumable = true;

            Item.maxStack = 999;
            Item.width = 30;
            Item.height = 24;

            Item.useTime = 10;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;

            Item.rare = ItemRarityID.Green;

            Item.createTile = ModContent.TileType<Tiles.Ores.AbyssiumBar>();
        }

        public override void PostUpdate()
        {
            if (Main.rand.NextBool(50))
            {
                Dust.NewDust(Item.position, Item.width, Item.height, DustID.BlueMoss);
            }
        }

        public override void AddRecipes() 
            => CreateRecipe()
            .AddIngredient(ModContent.ItemType<AbyssiumOre>(), 4)
            .AddTile(TileID.Furnaces)
            .Register();
    }
}