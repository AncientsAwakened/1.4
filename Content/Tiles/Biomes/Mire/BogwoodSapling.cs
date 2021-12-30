using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Content.Tiles.Biomes.Mire
{
    public class BogwoodSapling : ModTile
    {
        public override void SetStaticDefaults()

        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileID.Sets.CommonSapling[Type] = true;
            TileID.Sets.TreeSapling[Type] = true;

            TileObjectData.newTile.Origin = new Point16(0, 1);

            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);

            TileObjectData.newTile.Width = 1;
            TileObjectData.newTile.Height = 2;

            TileObjectData.newTile.CoordinateHeights = new[]
            {
                16,
                18
            };

            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;

            TileObjectData.newTile.AnchorValidTiles = new int[]
            {
                 ModContent.TileType<MireGrass>()
            };

            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.DrawFlipHorizontal = true;
            TileObjectData.newTile.LavaDeath = true;

            TileObjectData.newTile.WaterPlacement = LiquidPlacement.NotAllowed;

            TileObjectData.newTile.RandomStyleRange = 3;

            TileObjectData.addTile(Type);

            ModTranslation tileName = CreateMapEntryName();
            tileName.SetDefault("Bogwood Sapling");

            AddMapEntry(new Color(200, 200, 200));

            AdjTiles = new int[]
            {
                TileID.Saplings
            };

            SoundType = SoundID.Dig;
            DustType = DustID.BorealWood;
        }

        public override void RandomUpdate(int i, int j)
        {
            if (!WorldGen.genRand.NextBool(20))
            {
                return;
            }

            bool success = WorldGen.GrowTree(i, j);
            bool isPlayerNear = WorldGen.PlayerLOS(i, j);

            if (success && isPlayerNear)
            {
                WorldGen.TreeGrowFXCheck(i, j);
            }
        }

        public override void SetSpriteEffects(int i, int j, ref SpriteEffects effects)
        {
            if (i % 2 == 1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
        }
    }
}
