using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using AAMod.Utilities;

namespace AAMod.Content.Projectiles.Magic
{
	public class PhantomClaw1 : ModProjectile
	{
		public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.InsanityShadowFriendly;
		int TimeToLive = 60;
		NPC targetNPC;
		Vector2 baseOffset;
		float fadeInFrames = 20;
		float travelDistance;
		float travelVelocity = 12;
		public override void SetDefaults()
		{
			base.SetDefaults();
			Projectile.width = 32;
			Projectile.height = 32;
			Projectile.timeLeft = TimeToLive;
			Projectile.tileCollide = false;
			Projectile.penetrate = -1;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
			Projectile.hostile = false;
			Projectile.friendly = true;
		}

		public override void AI()
		{
			if (Projectile.ai[0] == -1)
			{
				Projectile.Kill();
				return;
			}
			if (targetNPC == default)
			{
				Projectile.ai[0] = AAHelper.GetNearestNPC(Projectile.Center);
				if (Projectile.ai[0] != -1)
				{
					targetNPC = Main.npc[(int)Projectile.ai[0]];
				}
				baseOffset = Projectile.Center - targetNPC.Center;
				travelDistance = baseOffset.Length();
				Projectile.spriteDirection = -Math.Sign(baseOffset.X);
				Projectile.rotation = baseOffset.ToRotation() + (Projectile.spriteDirection == 1 ? MathHelper.Pi : 0);
			}

			bool isMoving = TimeToLive - Projectile.timeLeft > 20;
			Projectile.friendly = isMoving;
			if (targetNPC.active)
			{
				Projectile.Center = targetNPC.Center + baseOffset;
				Projectile.velocity = Vector2.Zero;
				Vector2 offset = baseOffset;
				offset.SafeNormalize();
				Projectile.position = targetNPC.Center + travelDistance * offset;
				if (isMoving)
				{
					travelDistance -= travelVelocity;
				}
				else
				{
					travelDistance += 1;
				}
			}
			else
			{
				Projectile.timeLeft = Math.Min(Projectile.timeLeft, (int)fadeInFrames);
			}
			Projectile.Opacity = isMoving ? Math.Min(1, Projectile.timeLeft / fadeInFrames) : Math.Min(1, (TimeToLive - Projectile.timeLeft) / fadeInFrames);
			if (Projectile.timeLeft < fadeInFrames)
			{
				Projectile.rotation += -Math.Sign(baseOffset.X) * MathHelper.TwoPi / fadeInFrames;
				travelVelocity *= 0.75f;
			}
		}


		public override bool PreDraw(ref Color lightColor)
		{
			Texture2D texture = Terraria.GameContent.TextureAssets.Projectile[Type].Value;
			Rectangle bounds = texture.Bounds;
			Vector2 origin = bounds.Center.ToVector2();
			float scale = 0.5f;
			Color glowColor = Color.Violet * Projectile.Opacity * 0.5f;
			Color mainColor = Color.Black * Projectile.Opacity;
			Vector2 pos = Projectile.Center - Main.screenPosition;
			SpriteEffects effects = Projectile.spriteDirection == 1 ? 0 : SpriteEffects.FlipHorizontally;
			for (int i = -1; i <= 1; i += 1)
			{
				for (int j = -1; j <= 1; j += 1)
				{
					Vector2 offset = 2 * new Vector2(i, j).RotatedBy(Projectile.rotation);
					Main.EntitySpriteDraw(texture, pos + offset,
						bounds, glowColor, Projectile.rotation, origin, scale, effects, 0);
				}
			}
			Main.EntitySpriteDraw(texture, pos,
				bounds, mainColor, Projectile.rotation, origin, scale, effects, 0);
			return false;
		}

	}
}