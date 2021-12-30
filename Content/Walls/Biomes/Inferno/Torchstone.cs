using AAMod.Content.Items.Placeables.Walls;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Walls.Biomes.Inferno
{
    public class Torchstone : ModWall
    {
        public override void SetStaticDefaults()
        {
            Main.wallHouse[Type] = false;

            AddMapEntry(new Color(39, 41, 48));

            SoundType = SoundID.Dig;
            DustType = DustID.Stone;
            ItemDrop = ModContent.ItemType<TorchstoneWall>();
        }

        public override void NumDust(int i, int j, bool fail, ref int num) 
            => num = fail ? 1 : 3;
    }
}
