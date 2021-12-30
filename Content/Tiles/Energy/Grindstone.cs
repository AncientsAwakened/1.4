using AAMod.Content.Items.Placeables.Energy;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Content.Tiles.Energy
{
    class Grindstone : ModTile
    {
		public override void SetStaticDefaults()
		{
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = false;
			Main.tileFrameImportant[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.addTile(Type);
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Grindstone");
			AddMapEntry(new Color(148, 108, 76), name);
		}
		private int frameCounter = 0;
		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 32, ModContent.ItemType<GrindstoneItem>());
		}
        public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
        {
			frameCounter++;
            if(frameCounter >= 5)
            {
				frameCounter = 0;
				frameYOffset += 36;
				if(frameYOffset >= 144)
                {
					frameYOffset = 0;
                }
            }
        }
    }
}
