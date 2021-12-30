using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace AAMod.Content.Skies
{
	public abstract class SmartCustomSky : CustomSky
	{
		public virtual Texture2D Texture => (Texture2D)ModContent.Request<Texture2D>(GetType().FullName.Replace(".", "/"));

		public bool BlackBackground;

		public EffectPriority SkyPriority;

		public Color Color;

		public float SkyOpacity;

        public SmartCustomSky(EffectPriority skyPriority = EffectPriority.High, Color? color = null, bool blackBackground = true, float opacity = 0.5f)
        {
            SkyPriority = skyPriority;
            Color = color ?? Color.White;
            BlackBackground = blackBackground;
            Opacity = opacity;
        }

        public bool Active;

        public float Intensity;

        public sealed override void Update(GameTime gameTime)
        {
			Intensity = Active ? Math.Min(1f, 0.01f + Intensity) : Math.Max(0f, Intensity - 0.01f);

			SafeUpdate(gameTime);
        }

        public sealed override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
			if (maxDepth >= 3.40282347E+38f && minDepth < 3.40282347E+38f)
            {
				if (BlackBackground)
                {
					spriteBatch.Draw(TextureAssets.BlackTile.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Black * Intensity);
				}

				spriteBatch.Draw(Texture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color * Math.Min(1f, (Main.screenPosition.Y - 800f) / 1000f * Intensity));
			}

			SafeDraw(spriteBatch, minDepth, maxDepth);
        }

        public override Color OnTileColor(Color inColor)
		{
			var value = inColor.ToVector4();

			return new Color(Vector4.Lerp(value, Vector4.One, Intensity * 0.5f));
		}

		public override void Activate(Vector2 position, params object[] args)
		{
			Intensity = 0.002f;

			Active = true;
		}

		public override float GetCloudAlpha() => 1f - Intensity;

		public override void Deactivate(params object[] args) => Active = false;

		public override void Reset() => Active = false;

		public override bool IsActive() => Active || Intensity > 0.001f;

		public virtual void SafeUpdate(GameTime gameTime) { }

		public virtual void SafeDraw(SpriteBatch spriteBatch, float minDepth, float maxDepth) { }
	}
}
