using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Common.Globals.NPCs
{
    public sealed class DrawNPCAfterimage : GlobalNPC
    {
        public bool Enabled;

        public float InitialOpacity = 0.8f;
        public float OpacityDegrade = 0.1f;
 
        public int StepSize = 1;

        public override bool InstancePerEntity => true;

        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (Enabled)
            {
                Texture2D texture = TextureAssets.Npc[npc.type].Value;

                SpriteEffects effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
                Vector2 origin = npc.frame.Size() / 2f;

                for (int i = 0; i < NPCID.Sets.TrailCacheLength[npc.type]; i += StepSize)
                {
                    Vector2 position = npc.oldPos[i] - screenPos + npc.Hitbox.Size() / 2f + new Vector2(0f, npc.gfxOffY);
                    float opacity = InitialOpacity - OpacityDegrade * i / StepSize;

                    Main.EntitySpriteDraw(texture, position, npc.frame, drawColor * opacity, npc.oldRot[i], origin, npc.scale, effects, 0);
                }
            }

            return true;
        }
    }
}
