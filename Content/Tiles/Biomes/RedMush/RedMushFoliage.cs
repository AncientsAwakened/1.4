using AAMod.Content.Dusts;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Content.Tiles.Biomes.RedMush
{
    public class RedMushFoliage : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileCut[Type] = true;
            Main.tileSolid[Type] = false;
            Main.tileLighted[Type] = false;
            TileID.Sets.SwaysInWindBasic[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);

            TileObjectData.newTile.LavaDeath = true;
            TileObjectData.newTile.WaterDeath = false;

            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.CoordinateWidth = 16;

            TileObjectData.newTile.CoordinateHeights = new[] { 22 };

            TileObjectData.newTile.DrawYOffset = -2;

            TileObjectData.newTile.Style = 0;

            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.UsesCustomCanPlace = true;

            for (int i = 0; i < 6; i++)
            {
                TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
                TileObjectData.addSubTile(TileObjectData.newSubTile.Style);
            }

            TileObjectData.addTile(Type);

            DustType = ModContent.DustType<MushDust>();
            SoundType = SoundID.Grass;
            ItemDrop = ItemID.Mushroom;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
            => num = 2;
    }
}
