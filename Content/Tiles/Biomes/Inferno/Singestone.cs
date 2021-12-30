using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Tiles.Biomes.Inferno
{
	public class Singestone : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileMergeDirt[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileMerge[Type][ModContent.TileType<Torchstone>()] = true;
            Main.tileMerge[Type][ModContent.TileType<Torchslag>()] = true;

            TileID.Sets.Conversion.Stone[Type] = true;
            TileID.Sets.Stone[Type] = true;

            AddMapEntry(new Color(61, 64, 75));

            DustType = DustID.Ash;
            SoundType = SoundID.Tink;
            ItemDrop = ModContent.ItemType<Items.Placeables.Blocks.Singestone>();
        }

        public override void NumDust(int i, int j, bool fail, ref int num) 
            => num = fail ? 1 : 3;
    }
}
