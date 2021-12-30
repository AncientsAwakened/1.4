using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Projectiles.Typeless
{
	public class IncineriteExplosion : ModProjectile
	{
		public override void SetStaticDefaults() => DisplayName.SetDefault("Incinerite Explosion");

		public override void SetDefaults()
		{
			Projectile.friendly = true;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;

			Projectile.width = Projectile.height = 60;

			Projectile.timeLeft = 5;
			Projectile.knockBack = 6f;
			Projectile.penetrate = -1;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) => target.AddBuff(BuffID.OnFire, 180);

		public override void OnHitPlayer(Player target, int damage, bool crit) => target.AddBuff(BuffID.OnFire, 180);

		// public override bool PreDraw(ref Color lightColor) => Projectile.DrawProjectileCentered(lightColor);
    }
}
