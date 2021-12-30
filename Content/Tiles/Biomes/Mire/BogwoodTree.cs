using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Tiles.Biomes.Mire
{
    public class BogwoodTree : ModTree
    {
        public override Texture2D GetTexture() 
            => ModContent.Request<Texture2D>("AAMod/Content/Tiles/Biomes/Mire/BogwoodTree").Value;

        public override Texture2D GetBranchTextures(int i, int j, int trunkOffset, ref int frame) 
            => ModContent.Request<Texture2D>("AAMod/Content/Tiles/Biomes/Mire/BogwoodTree_Branches").Value;

        public override Texture2D GetTopTextures(int i, int j, ref int frame, ref int frameWidth, ref int frameHeight, ref int xOffsetLeft, ref int yOffset) 
            => ModContent.Request<Texture2D>("AAMod/Content/Tiles/Biomes/Mire/BogwoodTree_Tops").Value;

        public override int DropWood() 
            => ModContent.ItemType<Items.Placeables.Blocks.Bogwood>();

        public override int CreateDust() 
            => DustID.BorealWood;
    }
}
