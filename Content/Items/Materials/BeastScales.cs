using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Materials
{
    public class BeastScales : ModItem
    {

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Beast Scales");
			Tooltip.SetDefault("'They feel rough'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
        }
		public override void SetDefaults()
        {
            Item.maxStack = 999;

            Item.width = 22;
            Item.height = 24;
            Item.rare = ItemRarityID.Green;
        }
    }
}
