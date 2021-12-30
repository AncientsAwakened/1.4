using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Projectiles.Magic
{
    public class MawLavaBlast : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Maw Lava Blast");

            Main.projFrames[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;

            Projectile.width = Projectile.height = 10;

            Projectile.timeLeft = 300;
            Projectile.aiStyle = 0;
        }

        public override void AI()
        {
            Projectile.ai[0]++;

            if (Projectile.ai[0] > 10f)
            {
                Projectile.velocity.Y += 0.5f;
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

            if (Projectile.frame > 1)
            {
                Projectile.frame = 0;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) 
            => target.AddBuff(BuffID.OnFire, 180);

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);

            for (int i = 0; i < 3; i++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.Flare, Main.rand.NextFloat(-3f, 4f), Main.rand.NextFloat(-3f, 4f));
            }

            Projectile.velocity.Y = -Projectile.oldVelocity.Y / 2f;
            Projectile.ai[1]++;

            return Projectile.ai[1] > 1;
        }
    }
}
