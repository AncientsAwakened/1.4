using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Bases
{
    public abstract class WallItem<TModItem> : ModItem where TModItem : ModWall
    {
        public sealed override void SetDefaults()
        {
            Item.consumable = true;
            Item.autoReuse = true;
            Item.useTurn = true;

            Item.useTime = 10;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;

            Item.createWall = ModContent.WallType<TModItem>();

            SafeSetDefaults();
        }

        public virtual void SafeSetDefaults() { }
    }
}
