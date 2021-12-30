using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AAMod.Common.Globals.NPCs
{
    public sealed class DrawNPCCentered : GlobalNPC
    {
        public bool Enabled;

        public override bool InstancePerEntity => true;

        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (Enabled)
            {
                Texture2D texture = TextureAssets.Npc[npc.type].Value;
                SpriteEffects effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

                Vector2 origin = npc.frame.Size() / 2f + new Vector2(0f, npc.ModNPC.DrawOffsetY);
                Vector2 position = npc.Center - screenPos + new Vector2(0f, npc.gfxOffY);

                Main.EntitySpriteDraw(texture, position, npc.frame, drawColor, npc.rotation, origin, npc.scale, effects, 0);
            }

            return !Enabled;
        }
    }
}
