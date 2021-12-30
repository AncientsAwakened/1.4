using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Tiles.Biomes.Inferno
{
    public class RazewoodTree : ModTree
    {
        public override Texture2D GetTexture() 
            => ModContent.Request<Texture2D>("AAMod/Content/Tiles/Biomes/Inferno/RazewoodTree").Value;

        public override Texture2D GetBranchTextures(int i, int j, int trunkOffset, ref int frame) 
            => ModContent.Request<Texture2D>("AAMod/Content/Tiles/Biomes/Inferno/RazewoodTree_Branches").Value;

        public override Texture2D GetTopTextures(int i, int j, ref int frame, ref int frameWidth, ref int frameHeight, ref int xOffsetLeft, ref int yOffset) 
            => ModContent.Request<Texture2D>("AAMod/Content/Tiles/Biomes/Inferno/RazewoodTree_Tops").Value;

        public override int DropWood() 
            => ModContent.ItemType<Items.Placeables.Blocks.Razewood>();

        public override int CreateDust() 
            => DustID.BorealWood;
    }
}
