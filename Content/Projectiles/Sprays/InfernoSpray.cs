using AAMod.Content.Dusts;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Projectiles.Sprays
{
    public class InfernoSpray : Spray
    {
        public override int DustType 
			=> ModContent.DustType<InfernoSolution>();

        public override void SetStaticDefaults() 
			=> DisplayName.SetDefault("Inferno Spray");

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

					if (TileID.Sets.Conversion.Stone[tileType])
					{
                        ConvertTile(k, l, (ushort)ModContent.TileType<Tiles.Biomes.Inferno.Torchstone>());
					}
					else if (TileID.Sets.Conversion.Grass[tileType])
					{
                        ConvertTile(k, l, (ushort)ModContent.TileType<Tiles.Biomes.Inferno.InfernoGrass>());
					}

                    int wallType = Main.tile[k, l].wall;

                    if (WallID.Sets.Conversion.Grass[wallType])
                    {
                        ConvertWall(i, j, (ushort)ModContent.WallType<Walls.Biomes.Inferno.InfernoGrass>());
                    }
                    else if (WallID.Sets.Conversion.Stone[wallType])
                    {
                        ConvertWall(i, j, (ushort)ModContent.WallType<Walls.Biomes.Inferno.Torchstone>());
                    }
				}
			}
		}
	}
}
