using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using System;
using Terraria.Graphics.Effects;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Content.Biomes.Mire
{
    public class MireSky : CustomSky
    {
		public static bool Open = false;
		public override void Deactivate(params object[] args)
		{
			skyActive = false;
		}

		public override void Reset()
		{
			skyActive = false;
		}

		public override bool IsActive()
		{
			return skyActive || opacity > 0f;
		}

		public override void Activate(Vector2 position, params object[] args)
		{
			skyActive = true;
		}
		public static Vector2 MireC;
		public static Vector2 MireM;
		public static Vector2 MireF;
		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {

			if (maxDepth >= 3E+38f && minDepth < 3E+38f && !Main.bloodMoon)
			{
				spriteBatch.Draw(ModContent.Request<Texture2D>("AAMod/Assets/Textures/Backgrounds/Mire/MireSky").Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), new Color(opacity * opacity, opacity * opacity, opacity * opacity, opacity));
			}
			else if (maxDepth >= 3E+38f && minDepth < 3E+38f && Main.bloodMoon)
            {
				spriteBatch.Draw(ModContent.Request<Texture2D>("AAMod/Assets/Textures/Backgrounds/Mire/FMSky").Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), new Color(opacity * opacity, opacity * opacity, opacity * opacity, opacity));
				return;
			}
			spriteBatch.Draw(ModContent.Request<Texture2D>("AAMod/Assets/Textures/Backgrounds/Mire/MireBackgroundFar").Value, new Vector2(Main.screenWidth /3f, Main.screenHeight * 2.0f) - MireF * 0.2f, new Rectangle(0, 0, 1024, 900), new Color(opacity, opacity, opacity, opacity), 0, new Vector2(512, 600), 1.2f, SpriteEffects.None, 0f);
			spriteBatch.Draw(ModContent.Request<Texture2D>("AAMod/Assets/Textures/Backgrounds/Mire/MireBackgroundMiddle").Value, new Vector2(Main.screenWidth / 3f, Main.screenHeight * 2.0f) - MireM * 0.2f, new Rectangle(0, 0, 1024, 900), new Color(opacity, opacity, opacity, opacity), 0, new Vector2(512, 600), 1.6f, SpriteEffects.None, 0f);
			spriteBatch.Draw(ModContent.Request<Texture2D>("AAMod/Assets/Textures/Backgrounds/Mire/MireBackgroundClose").Value, new Vector2(Main.screenWidth / 3f, Main.screenHeight * 2.0f) - MireC * 0.2f, new Rectangle(0, 0, 1024, 900), new Color(opacity, opacity, opacity, opacity), 0, new Vector2(512, 600), 1.8f, SpriteEffects.None, 0f);
		}
		public override void Update(GameTime gameTime)
		{
			if (skyActive && opacity < 1f)
			{
				opacity += 0.02f;
				return;
			}
			if (!skyActive && opacity > 0f)
			{
				opacity -= 0.02f;
			}
			if (!skyActive)
            {
				opacity = 0f;
            }
			if (skyActive && !Main.dayTime)
			{
				opacity -= 0.02f;
			}
			if (skyActive && Main.dayTime)
			{
				opacity += 0.02f;
			}
		}
		public override float GetCloudAlpha()
		{
			return (1f - opacity) * 0.97f + 0.03f;
		}

		private bool skyActive;

		private float opacity;
	}

}
