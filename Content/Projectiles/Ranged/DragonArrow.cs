using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.Common.Globals.Projectiles;

namespace AAMod.Content.Projectiles.Ranged
{
	public class DragonArrow : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dragon Arrow");

			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
		}

		public override void SetDefaults()
		{
			Projectile.arrow = true;
			Projectile.friendly = true;
			Projectile.ignoreWater = true;

			Projectile.width = Projectile.height = 10;

			Projectile.timeLeft = 180;

			Projectile.aiStyle = 1;
			AIType = ProjectileID.WoodenArrowFriendly;
		}

		/// <summary>
		/// Makes the arrow act like an arrow
		/// </summary>
		public override void AI()
		{
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

			if (Main.rand.NextBool(10))
			{
				var dust = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Flare, -Projectile.velocity.X / 2f, -Projectile.velocity.Y / 2f);
				dust.noGravity = true;
				dust.fadeIn = 1f;
				dust.scale = Main.rand.NextFloat(0.6f, 1f);

				Projectile.netUpdate = true;
			}
		}

		/// <summary>
		/// Plays a sound when the arrow collides with a tile
		/// </summary>
		/// <param name="oldVelocity"></param>
		/// <returns></returns>
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
			SoundEngine.PlaySound(SoundID.Dig, Projectile.position);

			return true;
		}

		/// <summary>
		/// Spawns an explosion on impact
		/// </summary>
		/// <param name="timeLeft"></param>
		public override void Kill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Item14, Projectile.position);

			Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.position, Vector2.Zero, ModContent.ProjectileType<Typeless.IncineriteExplosion>(), Projectile.damage, (int)Projectile.knockBack, Projectile.owner);
		}

		/*
        public override bool PreDraw(ref Color lightColor)
        {
            DrawProjectileTrailCentered(lightColor);

            return PDrawProjectileCentered(lightColor);
        }
		*/
    }
}
