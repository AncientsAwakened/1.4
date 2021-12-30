using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Materials.Energy
{
    public class MagicFragment : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magic Fragment");
            Tooltip.SetDefault("Wonderous for the Talent");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
        }
        public override void SetDefaults()
        {
            Item.maxStack = 1;
            Item.width = 28;
            Item.height = 32;
            Item.rare = ItemRarityID.Cyan;
        }
    }
}