using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Tiles.Biomes.Mire
{
	public class Depthstone : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileMergeDirt[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileMerge[Type][ModContent.TileType<Darkmud>()] = true;

            TileID.Sets.Conversion.Stone[Type] = true;
            TileID.Sets.Stone[Type] = true;

            AddMapEntry(new Color(43, 46, 62));

            DustType = DustID.BlueMoss;
            SoundType = SoundID.Tink;
            ItemDrop = ModContent.ItemType<Items.Placeables.Blocks.Depthstone>();
        }

        public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
    }
}
