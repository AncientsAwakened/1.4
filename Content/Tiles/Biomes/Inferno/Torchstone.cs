using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Tiles.Biomes.Inferno
{
	public class Torchstone : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileMergeDirt[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileMerge[Type][ModContent.TileType<InfernoGrass>()] = true;
            Main.tileMerge[Type][ModContent.TileType<Torchslag>()] = true;
            Main.tileMerge[Type][ModContent.TileType<Singestone>()] = true;

            TileID.Sets.Conversion.Stone[Type] = true;
            TileID.Sets.Stone[Type] = true;

            AddMapEntry(new Color(61, 64, 75));

            DustType = DustID.Ash;
            SoundType = SoundID.Tink;
            ItemDrop = ModContent.ItemType<Items.Placeables.Blocks.Torchstone>();
        }

        public override void NumDust(int i, int j, bool fail, ref int num) 
            => num = fail ? 1 : 3;
    }
}
