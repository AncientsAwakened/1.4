using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Materials
{
    public class ToughLeather : ModItem
    {

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tough Leather");
			Tooltip.SetDefault("'It smells rubbery...'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
        }
		public override void SetDefaults()
        {
            Item.maxStack = 999;

            Item.rare = ItemRarityID.Green;
        }
    }
}
