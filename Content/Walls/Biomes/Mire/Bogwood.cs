using AAMod.Content.Items.Placeables.Walls;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Walls.Biomes.Mire
{
    public class Bogwood : ModWall
    {
        public override void SetStaticDefaults()
        {
            Main.wallHouse[Type] = true;

            AddMapEntry(new Color(30, 36, 32));

            SoundType = SoundID.Dig;
            DustType = DustID.BorealWood;
            ItemDrop = ModContent.ItemType<BogwoodWall>();
        }

        public override void NumDust(int i, int j, bool fail, ref int num) 
            => num = fail ? 1 : 3;
    }
}
