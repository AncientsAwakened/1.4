using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Bases
{
    public abstract class TileItem<TModItem> : ModItem where TModItem : ModTile
    {
        public sealed override void SetDefaults()
        {
            Item.consumable = true;
            Item.autoReuse = true;
            Item.useTurn = true;

            Item.useTime = 10;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;

            Item.createTile = ModContent.TileType<TModItem>();

            SafeSetDefaults();
        }

        public virtual void SafeSetDefaults() { }
    }
}
