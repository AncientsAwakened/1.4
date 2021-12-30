using AAMod.Content.Dusts;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Materials
{
    public class ScorchedScale : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scorched Scale");
            Tooltip.SetDefault("'Even when torn from the mother, it's still warm...'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
        }

        public override void SetDefaults()
        {
            Item.maxStack = 999;

			Item.width = 24;
			Item.height = 22;
			Item.rare = ItemRarityID.Green;
        }

        public override void PostUpdate()
        {
            if (Main.rand.NextBool(25))
            {
                Dust.NewDust(Item.position, Item.width, Item.height, ModContent.DustType<Incinerite>());
            }
        }
    }
}
