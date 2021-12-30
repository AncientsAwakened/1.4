using Microsoft.Xna.Framework;

namespace AAMod.Content.Projectiles.Magic
{
	public static class Vector2Extensions
	{
		// prevent 
		public static void SafeNormalize(this ref Vector2 vec)
		{
			if (vec != Vector2.Zero)
			{
				vec.Normalize();
			}
		}
	}
}