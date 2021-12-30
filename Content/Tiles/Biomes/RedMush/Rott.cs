using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Tiles.Biomes.RedMush
{
	public class Rott : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileMergeDirt[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileMerge[Type][ModContent.TileType<RedMushGrass>()] = true;

            TileID.Sets.ChecksForMerge[Type] = true;
            TileID.Sets.CanBeClearedDuringOreRunner[Type] = true;

            AddMapEntry(new Color(127, 105, 77));

            DustType = DustID.Dirt;
            SoundType = SoundID.Dig;
            ItemDrop = ModContent.ItemType<Items.Placeables.Blocks.Rott>();
        }

        public override void NumDust(int i, int j, bool fail, ref int num) 
            => num = fail ? 1 : 3;
    }
}
