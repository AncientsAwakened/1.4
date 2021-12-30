using AAMod.Content.Dusts;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Materials
{
    public class DragonScale : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Scale");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
        }

        public override void SetDefaults()
        {
            Item.maxStack = 999;

			Item.width = 18;
			Item.height = 22;
			Item.rare = ItemRarityID.Green;
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
