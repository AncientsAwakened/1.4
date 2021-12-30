using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Common.Globals.Projectiles
{
    public sealed class DrawProjectileAfterimage : GlobalProjectile
    {
        public bool Enabled;

        public float InitialOpacity = 0.8f;
        public float OpacityDegrade = 0.1f;

        public int StepSize = 1;

        public override bool InstancePerEntity => true;

        public override bool PreDraw(Projectile projectile, ref Color lightColor)
        {
            if (Enabled)
            {
                Texture2D texture = TextureAssets.Projectile[projectile.type].Value;
                SpriteEffects effects = projectile.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

                Rectangle frame = texture.Frame(1, Main.projFrames[projectile.type], 0, projectile.frame);
                Vector2 origin = frame.Size() / 2f + new Vector2(projectile.ModProjectile.DrawOriginOffsetX, projectile.ModProjectile.DrawOriginOffsetY);

                for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[projectile.type]; i += StepSize)
                {
                    Vector2 position = projectile.oldPos[i] - Main.screenPosition + projectile.Hitbox.Size() / 2f + new Vector2(0f, projectile.gfxOffY);
                    float opacity = InitialOpacity - OpacityDegrade * i / StepSize;

                    Main.EntitySpriteDraw(texture, position, frame, lightColor * opacity, projectile.rotation, origin, projectile.scale, effects, 0);
                }
            }

            return true;
        }
    }
}
