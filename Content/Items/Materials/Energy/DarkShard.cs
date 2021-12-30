using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Materials.Energy
{
    public class DarkShard : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dark Shard");
            Tooltip.SetDefault("It's purpose glows with Necrotic Energy");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
        }
        public override void SetDefaults()
        {
            Item.maxStack = 1;
            Item.width = 20;
            Item.height = 22;
            Item.rare = ItemRarityID.Cyan;
        }
    }
}