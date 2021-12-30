using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Content.Biomes.RedMush
{
    public class RedMushSurfaceBgStyle : ModSurfaceBackgroundStyle
	{
		public override void ModifyFarFades(float[] fades, float transitionSpeed)
		{
			for (int i = 0; i < fades.Length; i++)
			{
				if (i == Slot)
				{
					fades[i] += transitionSpeed;
				}
				else
				{
					fades[i] -= transitionSpeed;
				}

				fades[i] = MathHelper.Clamp(fades[i], 0f, 1f);
			}
		}

		public override int ChooseFarTexture()
			=> BackgroundTextureLoader.GetBackgroundSlot("AAMod/Assets/Textures/Backgrounds/RedMush/RedMushBackgroundFar");

		public override int ChooseMiddleTexture()
			=> BackgroundTextureLoader.GetBackgroundSlot("AAMod/Assets/Textures/Backgrounds/RedMush/RedMushBackgroundMiddle");

		public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
			=> BackgroundTextureLoader.GetBackgroundSlot("AAMod/Assets/Textures/Backgrounds/RedMush/RedMushBackgroundClose");
	}
}