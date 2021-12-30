using AAMod.Content.Items.Placeables.Walls;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Walls.Biomes.Inferno
{
    public class Razewood : ModWall
    {
        public override void SetStaticDefaults()
        {
            Main.wallHouse[Type] = true;

            AddMapEntry(new Color(52, 45, 33));

            SoundType = SoundID.Dig;
            DustType = DustID.Stone;
            ItemDrop = ModContent.ItemType<RazewoodWall>();
        }

        public override void NumDust(int i, int j, bool fail, ref int num) 
            => num = fail ? 1 : 3;
    }
}
