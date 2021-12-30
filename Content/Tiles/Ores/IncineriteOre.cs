﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Tiles.Ores
{
    public class IncineriteOre : ModTile
    {
		public override void SetStaticDefaults()
		{
			Main.tileSpelunker[Type] = true;
			Main.tileOreFinderPriority[Type] = 410;
			Main.tileShine2[Type] = true;
			Main.tileShine[Type] = 975;
			Main.tileMergeDirt[Type] = true;
			Main.tileSolid[Type] = true;
			Main.tileBlockLight[Type] = true;

			TileID.Sets.Ore[Type] = true;

			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Incinerite Ore");
			AddMapEntry(new Color(173, 77, 43), name);

			DustType = DustID.Flare;
			ItemDrop = ModContent.ItemType<Items.Materials.IncineriteOre>();
			SoundType = SoundID.Tink;
			SoundStyle = 1;
		}

		public override void NumDust(int i, int j, bool fail, ref int num) 
			=> num = fail ? 1 : 3;

        public override bool CanExplode(int i, int j) 
			=> NPC.downedBoss2;

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.8f;
            g = 0f;
            b = 0.2f;
        }
    }
}