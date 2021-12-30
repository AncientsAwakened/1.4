using AAMod.Content.Dusts;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Projectiles.Sprays
{
    public class RedMushSpray : Spray
    {
        public override int DustType 
			=> ModContent.DustType<MushSolution>();

        public override void SetStaticDefaults() 
			=> DisplayName.SetDefault("Red Mushland Spray");

        public override void Convert(int i, int j, int size = 4)
		{
			for (int k = i - size; k <= i + size; k++)
			{
				for (int l = j - size; l <= j + size; l++)
				{
					if (!WorldGen.InWorld(k, l, 1) || !(Math.Abs(l - j) < Math.Sqrt(size * size + size * size)))
					{
						continue;
					}

					int tileType = Main.tile[k, l].type;

					if (TileID.Sets.Conversion.Grass[tileType])
					{
                        ConvertTile(k, l, (ushort)ModContent.TileType<Tiles.Biomes.RedMush.RedMushGrass>());
					}
					else if (tileType == TileID.Dirt)
					{
						ConvertTile(k, l, (ushort)ModContent.TileType<Tiles.Biomes.RedMush.Rott>());
					}
				}
			}
		}
	}
}
