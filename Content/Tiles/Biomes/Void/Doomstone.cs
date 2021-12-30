using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Tiles.Biomes.Void
{
	public class Doomstone : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileMergeDirt[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;

            TileID.Sets.Conversion.Stone[Type] = true;
            TileID.Sets.Stone[Type] = true;

            AddMapEntry(new Color(78, 65, 78));

            DustType = DustID.Stone;
            SoundType = SoundID.Tink;
            ItemDrop = ModContent.ItemType<Items.Placeables.Blocks.Doomstone>();
        }

        public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
    }
}
