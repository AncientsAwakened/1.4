using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace AAMod.Content.Biomes.Mire
{
	public class MireWaterStyle : ModWaterStyle
	{

		public override int ChooseWaterfallStyle() => Find<ModWaterfallStyle>("AAMod/MireWaterfallStyle").Slot;

		public override int GetSplashDust() => Find<ModDust>("AAMod/MireWaterSplash").Type;

		public override int GetDropletGore() => Find<ModGore>("AAMod/MireWaterDroplet").Type;

		public override void LightColorMultiplier(ref float r, ref float g, ref float b)
		{
			r = 0f;
			g = 0f;
			b = 1f;
		}

		public override Color BiomeHairColor()
		{
			return Color.DarkGreen;
		}
	}
}