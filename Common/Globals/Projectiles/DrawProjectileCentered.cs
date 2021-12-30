using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AAMod.Common.Globals.Projectiles
{
    public sealed class DrawProjectileCentered : GlobalProjectile
    {
        public bool Enabled;

        public override bool InstancePerEntity => true;

        public override bool PreDraw(Projectile projectile, ref Color lightColor)
        {
            if (Enabled)
            {
                Texture2D texture = TextureAssets.Projectile[projectile.type].Value;
                SpriteEffects effects = projectile.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

                Rectangle frame = texture.Frame(1, Main.projFrames[projectile.type], 0, projectile.frame);

                Vector2 origin = frame.Size() / 2f + new Vector2(projectile.ModProjectile.DrawOriginOffsetX, projectile.ModProjectile.DrawOriginOffsetY);
                Vector2 position = projectile.Center - Main.screenPosition + new Vector2(projectile.ModProjectile.DrawOffsetX, projectile.gfxOffY);

                Main.EntitySpriteDraw(texture, position, frame, lightColor, projectile.rotation, origin, projectile.scale, effects, 0);
            }

            return !Enabled;
        }
    }
}
