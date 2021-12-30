using AAMod.Content.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace AAMod.Content.Projectiles.Typeless
{
	public class IncineriteFireball : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fireball");

			Main.projFrames[Projectile.type] = 4;

			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
		}

		public override void SetDefaults()
		{
			Projectile.friendly = true;
			Projectile.tileCollide = true;

			Projectile.width = Projectile.height = 16;

			Projectile.timeLeft = 180;
			Projectile.aiStyle = 0;
		}

		public override void AI()
		{
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

			Projectile.frameCounter++;

			if (Projectile.frameCounter > 5)
			{
				Projectile.frame++;

				Projectile.frameCounter = 0;
			}

			if (Projectile.frame > 3)
			{
				Projectile.frame = 0;
			}

			if (Main.rand.NextBool(10))
			{
				var dust = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Flare, -Projectile.velocity.X / 2f, -Projectile.velocity.Y / 2f);
				dust.noGravity = true;
				dust.fadeIn = 1f;
				dust.scale = Main.rand.NextFloat(0.6f, 1f);

				Projectile.netUpdate = true;
			}
		}

		public override void Kill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Item14, Projectile.position);

			Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.position, Vector2.Zero, ModContent.ProjectileType<IncineriteExplosion>(), Projectile.damage, (int)Projectile.knockBack, Projectile.owner);

			for (int i = 0; i < 20; i++)
			{
				int dust = Dust.NewDust(Projectile.position, Projectile.width,
					Projectile.height, DustID.Flare, 0f, 0f, 50, default, 2f);
				Main.dust[dust].velocity *= 5f;
			}

		}

		/*
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Projectile.DrawProjectileTrailCentered(spriteBatch, lightColor);

			return Projectile.DrawProjectileCentered(spriteBatch, lightColor);
		}
		*/
	}
}
