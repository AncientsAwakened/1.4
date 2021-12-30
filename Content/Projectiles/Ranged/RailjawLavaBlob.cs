using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Projectiles.Ranged
{
    public class RailjawLavaBlob : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Railjaw Lava Blob");

            Main.projFrames[Projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;

            Projectile.timeLeft = 300;
            Projectile.aiStyle = 0;
        }

        public override void AI()
        {
            Projectile.ai[0]++;

            if (Projectile.ai[0] > 10f)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.Flare);
                Projectile.netUpdate = true;
            }

            if (Main.rand.NextBool(10))
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.Flare);
                Projectile.netUpdate = true;
            }

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

            Projectile.frameCounter++;

            if (Projectile.frameCounter >= 5)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }

            if (Projectile.frame > 2)
            {
                Projectile.frame = 0;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) 
            => target.AddBuff(BuffID.OnFire, 180);

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);

            return true;
        }
    }
}
