using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using static Terraria.ModLoader.ModContent;

namespace AAMod.Content.Projectiles.Magic
{
	public interface ClawSub
	{
		Projectile projectile { get; }
		string Texture { get; }
	}

	public static class SpinningBladeDrawer
	{
		public static void DrawBlade(ClawSub self, Color lightColor, float rotation)
		{
			Vector2 pos = self.projectile.Center;
			Texture2D texture = Terraria.GameContent.TextureAssets.Projectile[self.projectile.type].Value;
			Rectangle bounds = texture.Bounds;
			Vector2 origin = new Vector2(bounds.Width / 2, bounds.Height / 2);
			Main.EntitySpriteDraw(texture, pos - Main.screenPosition,
				bounds, lightColor, rotation,
				origin, 1, 0, 0);
		}
		public static void DrawGlow(ClawSub self)
		{
			Vector2 pos = self.projectile.Center;
			Texture2D texture = Request<Texture2D>(self.Texture + "_Glow").Value;
			Rectangle bounds = texture.Bounds;
			Vector2 origin = new Vector2(bounds.Width / 2, bounds.Height / 2);
			Main.EntitySpriteDraw(texture, pos - Main.screenPosition,
				bounds, Color.White, 0,
				origin, 1, 0, 0);
		}
	}
}