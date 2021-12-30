using AAMod.Content.Tiles.Biomes.Inferno;
using AAMod.Content.Tiles.Biomes.Mire;
using AAMod.Content.Tiles.Biomes.RedMush;
using AAMod.Content.Tiles.Biomes.Void;
using System;
using Terraria.ModLoader;

namespace AAMod.Common.Systems 
{
    public class TileCountSystem : ModSystem
    {
        public int InfernoTileCount { get; set; }
        public int MireTileCount { get; set; }
        public int VoidTileCount { get; set; }
        public int RedMushTileCount { get; set; }

        public override void ResetNearbyTileEffects()
        {
            InfernoTileCount = 0;
            MireTileCount = 0;
            VoidTileCount = 0;
            RedMushTileCount = 0;
        }

        public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
        {
            InfernoTileCount = tileCounts[ModContent.TileType<InfernoGrass>()]
                + tileCounts[ModContent.TileType<Torchstone>()]
                + tileCounts[ModContent.TileType<Torchslag>()]
                + tileCounts[ModContent.TileType<Singestone>()];
            MireTileCount = tileCounts[ModContent.TileType<MireGrass>()]
                + tileCounts[ModContent.TileType<Depthstone>()]
                + tileCounts[ModContent.TileType<Darkmud>()];
            VoidTileCount = tileCounts[ModContent.TileType<Doomstone>()];
            RedMushTileCount = tileCounts[ModContent.TileType<RedMushGrass>()]
                + tileCounts[ModContent.TileType<Rott>()];
        }
    }
}
