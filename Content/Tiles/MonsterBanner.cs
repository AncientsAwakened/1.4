using AAMod.Content.Items.Placeables.Banners;
using AAMod.Content.NPCs.Critters.Mire;
using AAMod.Content.NPCs.Enemies.Inferno;
using AAMod.Content.NPCs.Enemies.Mire;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Content.Tiles
{
    public class MonsterBanner : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.CoordinateHeights = new[]
            {
                16, 16, 16
            };
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.AnchorTop = 
                new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.SolidBottom, 
                    TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.StyleWrapLimit = 111;
            TileObjectData.addTile(Type);

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Banner");
            AddMapEntry(new Color(13, 88, 130), name);

            DustType = -1;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            int style = frameX / 18;
            int item;

            switch (style)
            {
                case 0:
                    item = ModContent.ItemType<GatorBanner>();
                    break;

                case 1:
                    item = ModContent.ItemType<EarwigBanner>();
                    break;

                case 2:
                    item = ModContent.ItemType<GiantMosquitoBanner>();
                    break;

                case 3:
                    item = ModContent.ItemType<AcidFrogBanner>();
                    break;

                case 5:
                    item = ModContent.ItemType<CragBanner>();
                    break;

                case 6:
                    item = ModContent.ItemType<OilSlimeBanner>();
                    break;

                case 7:
                    item = ModContent.ItemType<FlamebruteBanner>();
                    break;

                case 8:
                    item = ModContent.ItemType<FlamespitterBanner>();
                    break;

                case 9:
                    item = ModContent.ItemType<KomodoBanner>();
                    break;

                default:
                    return;
            }

            Item.NewItem(i * 16, j * 16, 16, 48, item);
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (!closer)
                return;

            Player player = Main.LocalPlayer;
            int style = Main.tile[i, j].frameX / 18;
            int type = 0;

            switch (style)
            {
                case 0:
                     type = ModContent.NPCType<Gator>();
                    break;

                case 1:
                    // type = ModContent.NPCType<Earwig>();
                    break;

                case 2:
                     type = ModContent.NPCType<GiantMosquito>();
                    break;

                case 3:
                     type = ModContent.NPCType<AcidFrog>();
                    break;

                case 5:
                     type = ModContent.NPCType<Crag>();
                    break;

                case 6:
                    type = ModContent.NPCType<OilSlime>();
                    break;

                case 7:
                     type = ModContent.NPCType<Flamebrute>();
                    break;

                case 8:
                     type = ModContent.NPCType<Flamespitter>();
                    break;

                case 9:
                     type = ModContent.NPCType<KomodoWalk>();
                    break;

                default:
                    return;
            }

            Main.SceneMetrics.NPCBannerBuff[type] = true;
            Main.SceneMetrics.hasBanner = true;
        }

        public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
        {
            if (i % 2 == 1)
                spriteEffects = SpriteEffects.FlipHorizontally;
        }
    }
}