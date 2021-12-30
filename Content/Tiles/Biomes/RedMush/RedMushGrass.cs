using AAMod.Content.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Tiles.Biomes.RedMush
{
    public class RedMushGrass : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileBlendAll[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileMerge[Type][ModContent.TileType<Rott>()] = true;

            TileID.Sets.Grass[Type] = true;
            TileID.Sets.ChecksForMerge[Type] = true;
            TileID.Sets.NeedsGrassFraming[Type] = true;
            TileID.Sets.NeedsGrassFramingDirt[Type] = ModContent.TileType<Rott>();
            TileID.Sets.CanBeDugByShovel[Type] = true;
            TileID.Sets.ResetsHalfBrickPlacementAttempt[Type] = true;
            TileID.Sets.DoesntPlaceWithTileReplacement[Type] = true;
            TileID.Sets.SpreadOverground[Type] = true;
            TileID.Sets.SpreadUnderground[Type] = true;
            TileID.Sets.CanBeClearedDuringOreRunner[Type] = true;

            TileID.Sets.Conversion.Grass[Type] = true;
            TileID.Sets.Conversion.MergesWithDirtInASpecialWay[Type] = true;

            SetModTree(new RedMushTree());

            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;

            AddMapEntry(new Color(183, 133, 71));

            MinPick = 10;
            MineResist = 0.1f;
            DustType = ModContent.DustType<MushDust>();
            SoundType = SoundID.Dig;
            ItemDrop = ModContent.ItemType<Items.Placeables.Blocks.Rott>();
        }

        public override void NumDust(int i, int j, bool fail, ref int num) 
            => num = fail ? 1 : 3;

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (!fail)
            {
                fail = true;
                Framing.GetTileSafely(i, j).type = (ushort)ModContent.TileType<Rott>();
            }
        }

        public override void FloorVisuals(Player player)
        {
            if (player.velocity.X != 0f && Main.rand.NextBool(20))
            {
                Dust dust = Dust.NewDustDirect(player.Bottom, 0, 0, DustType, 0f, -Main.rand.NextFloat(2f));
                dust.noGravity = true;
                dust.fadeIn = 1f;
            }
        }

        public override void RandomUpdate(int i, int j)
        {
            Tile tile = Framing.GetTileSafely(i, j);
            Tile tileAbove = Framing.GetTileSafely(i, j - 1);

            if (!tileAbove.IsActive && tile.IsActive && Main.rand.NextBool(5) && tileAbove.LiquidAmount <= 0)
            {
                WorldGen.PlaceObject(i, j - 1, ModContent.TileType<RedMushFoliage>(), true, Main.rand.Next(11));
                NetMessage.SendObjectPlacment(-1, i, j - 1, ModContent.TileType<RedMushFoliage>(), Main.rand.Next(11), 0, -1, -1);
            }
            if (Main.rand.NextBool(4))
                WorldGen.SpreadGrass(i + Main.rand.Next(-1, 1), j + Main.rand.Next(-1, 1), ModContent.TileType<Rott>(), Type, false, 0);
        }

        public override int SaplingGrowthType(ref int style)
        {
            style = 0;
            return ModContent.TileType<RedMushSapling>();
        }
    }
}
