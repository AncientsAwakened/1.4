using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using System;
using Terraria.Graphics.Effects;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace AAMod.Content.Biomes.Inferno
{
	public class InfernoSky : CustomSky
	{
		public static bool Open = false;
		public override void Deactivate(params object[] args)
		{
			skyActive = false;
		}
		public override float GetCloudAlpha()
		{
			if (skyActive && !Main.dayTime)
			{
				Color.Lerp(Color.Black, Color.Black, 20);
			}
			return opacity;
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
		public static Vector2 InfernoC;
		public static Vector2 InfernoM;
		public static Vector2 InfernoF;
		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{

			if (maxDepth >= 3E+38f && minDepth < 3E+38f)
			{
				spriteBatch.Draw(ModContent.Request<Texture2D>("AAMod/Assets/Textures/Backgrounds/Inferno/InfernoSky").Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), new Color(opacity * opacity, opacity * opacity, opacity * opacity, opacity));
			}
			spriteBatch.Draw(ModContent.Request<Texture2D>("AAMod/Assets/Textures/Backgrounds/Inferno/InfernoBackgroundFar").Value, new Vector2(Main.screenWidth / 3f, Main.screenHeight * 2.0f) - InfernoF * 0.2f, new Rectangle(0, 0, 1024, 900), new Color(opacity, opacity, opacity, opacity), 0, new Vector2(512, 600), 1.0f, SpriteEffects.None, 0f);
			spriteBatch.Draw(ModContent.Request<Texture2D>("AAMod/Assets/Textures/Backgrounds/Inferno/InfernoBackgroundMiddle").Value, new Vector2(Main.screenWidth / 3f, Main.screenHeight * 2.0f) - InfernoM * 0.2f, new Rectangle(0, 0, 1024, 900), new Color(opacity, opacity, opacity, opacity), 0, new Vector2(512, 600), 1.4f, SpriteEffects.None, 0f);
			spriteBatch.Draw(ModContent.Request<Texture2D>("AAMod/Assets/Textures/Backgrounds/Inferno/InfernoBackgroundClose").Value, new Vector2(Main.screenWidth / 3f, Main.screenHeight * 2.0f) - InfernoC * 0.2f, new Rectangle(0, 0, 1024, 900), new Color(opacity, opacity, opacity, opacity), 0, new Vector2(512, 600), 1.6f, SpriteEffects.None, 0f);
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
			if (skyActive && !Main.dayTime)
			{
				opacity += 0.01f;
			}
			if (skyActive && Main.dayTime)
			{
				opacity -= 0.01f;
			}
			if (!skyActive)
			{
				opacity = 0f;
			}
		}

		private bool skyActive;

		private float opacity;
	}

}
