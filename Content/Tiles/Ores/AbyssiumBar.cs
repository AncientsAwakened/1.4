using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Content.Tiles.Ores
{
    public class AbyssiumBar : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileShine[Type] = 1100;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.addTile(Type);

            DustType = DustID.BlueMoss;
            SoundType = SoundID.Dig;

            AddMapEntry(new Color(200, 200, 200), Language.GetText("MapObject.MetalBar"));
        }

        public override bool Drop(int i, int j)
        {
            Tile tile = Framing.GetTileSafely(i, j);
            int style = tile.frameX / 18;

            if (style == 0)
            {
                Item.NewItem(i * 16, j * 16, 16, 16, ModContent.ItemType<Items.Materials.AbyssiumBar>());
            }

            return true;
        }
    }
}
