using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Tiles.Biomes.Inferno
{
	public class Razewood : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileMergeDirt[Type] = true;

            AddMapEntry(new Color(100, 87, 65));

            DustType = DustID.BorealWood;
            SoundType = SoundID.Dig;
            ItemDrop = ModContent.ItemType<Items.Placeables.Blocks.Razewood>();
        }

        public override void NumDust(int i, int j, bool fail, ref int num) 
            => num = fail ? 1 : 3;
    }
}
