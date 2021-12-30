using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Tiles.Biomes.Mire
{
	public class Darkmud : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileMergeDirt[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileMerge[Type][ModContent.TileType<MireGrass>()] = true;
            Main.tileMerge[Type][ModContent.TileType<Depthstone>()] = true;

            TileID.Sets.ChecksForMerge[Type] = true;
            TileID.Sets.CanBeClearedDuringOreRunner[Type] = true;

            AddMapEntry(new Color(30, 36, 32));

            DustType = DustID.BlueMoss;
            SoundType = SoundID.Dig;
            ItemDrop = ModContent.ItemType<Items.Placeables.Blocks.Darkmud>();
        }

        public override void NumDust(int i, int j, bool fail, ref int num) 
            => num = fail ? 1 : 3;
    }
}
