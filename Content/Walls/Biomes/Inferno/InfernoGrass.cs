using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Walls.Biomes.Inferno
{
	public class InfernoGrass : ModWall
    {
        public override void SetStaticDefaults()
        {
			Main.wallHouse[Type] = false;

			AddMapEntry(new Color(61, 57, 31));

			SoundType = SoundID.Grass;
			DustType = DustID.Flare;
			ItemDrop = ItemID.None;
		}

		public override void NumDust(int i, int j, bool fail, ref int num) 
			=> num = 2;
    }
}
