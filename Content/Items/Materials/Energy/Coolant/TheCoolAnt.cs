using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Materials.Energy.Coolant
{
    public class TheCoolAnt : ModItem
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Cool Ant");
			Tooltip.SetDefault("Wow! What a Cool Ant! Seems Useless!");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
        }
		public override void SetDefaults()
        {
            Item.maxStack = 1;
            Item.width = 24;
            Item.height = 34;
            Item.rare = ItemRarityID.Cyan;
        }
    }
}