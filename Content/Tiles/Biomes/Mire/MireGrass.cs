using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Tiles.Biomes.Mire
{
	public class MireGrass : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileBlendAll[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileMerge[Type][ModContent.TileType<Darkmud>()] = true;

            TileID.Sets.Grass[Type] = true;
            TileID.Sets.ChecksForMerge[Type] = true;
            TileID.Sets.NeedsGrassFraming[Type] = true;
            TileID.Sets.NeedsGrassFramingDirt[Type] = ModContent.TileType<Darkmud>();
            TileID.Sets.CanBeDugByShovel[Type] = true;
            TileID.Sets.ResetsHalfBrickPlacementAttempt[Type] = true;
            TileID.Sets.DoesntPlaceWithTileReplacement[Type] = true;
            TileID.Sets.SpreadOverground[Type] = true;
            TileID.Sets.SpreadUnderground[Type] = true;
            TileID.Sets.CanBeClearedDuringOreRunner[Type] = true;

            TileID.Sets.Conversion.Grass[Type] = true;
            TileID.Sets.Conversion.MergesWithDirtInASpecialWay[Type] = true;

            SetModTree(new BogwoodTree());

            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;

            AddMapEntry(new Color(27, 60, 45));

            MinPick = 10;
            MineResist = 0.1f;
            DustType = DustID.GreenMoss;
            SoundType = SoundID.Dig;
            ItemDrop = ModContent.ItemType<Items.Placeables.Blocks.Darkmud>();
        }

        public override void NumDust(int i, int j, bool fail, ref int num) 
            => num = fail ? 1 : 3;

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (!fail)
            {
                fail = true;
                Framing.GetTileSafely(i, j).type = (ushort)ModContent.TileType<Darkmud>();
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
                WorldGen.PlaceObject(i, j - 1, ModContent.TileType<MireFoliage>(), true, Main.rand.Next(14));
                NetMessage.SendObjectPlacment(-1, i, j - 1, ModContent.TileType<MireFoliage>(), Main.rand.Next(14), 0, -1, -1);
            }
            if (Main.rand.NextBool(4))
                WorldGen.SpreadGrass(i + Main.rand.Next(-1, 1), j + Main.rand.Next(-1, 1), ModContent.TileType<Darkmud>(), Type, false, 0);
        }

        public override int SaplingGrowthType(ref int style)
        {
            style = 0;
            return ModContent.TileType<BogwoodSapling>();
        }
    }
}
