using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace AAMod.Content.Projectiles.Enemies
{
    public class FlamespitterSpit : ModProjectile
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Fireball");

        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;

            Projectile.hostile = true;

            Projectile.timeLeft = 300;
            Projectile.aiStyle = 0;
        }

        public override void AI()
        {
            Projectile.ai[0]++;
            if (Projectile.ai[0] > 15f)
                Projectile.velocity.Y += 0.1f;
            if (Projectile.ai[0] % 2 == 0)
                Dust.NewDust(Projectile.Center, 0, 0, DustID.Flare);

            Projectile.rotation += 0.5f;
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
                Dust.NewDust(Projectile.Center, 0, 0, DustID.Flare, Main.rand.Next(-3, 4), Main.rand.Next(-3, 4));
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);

            return true;
        }
    }
}
