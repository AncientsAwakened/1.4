using AAMod.Content.Dusts;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Materials
{
    public class IncineriteBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Incinerite Bar");
            Tooltip.SetDefault("'Embued with Dragons' fury'");

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

            Item.createTile = ModContent.TileType<Tiles.Ores.IncineriteBar>();
        }

        public override void PostUpdate()
        {
            if (Main.rand.NextBool(50))
            {
                Dust.NewDust(Item.position, Item.width, Item.height, ModContent.DustType<Incinerite>());
            }
        }

        public override void AddRecipes() 
            => CreateRecipe()
            .AddIngredient(ModContent.ItemType<IncineriteOre>(), 4)
            .AddTile(TileID.Furnaces)
            .Register();
    }
}