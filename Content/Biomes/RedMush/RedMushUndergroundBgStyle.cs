using Terraria.ModLoader;

namespace AAMod.Content.Biomes.RedMush
{
    public class RedMushUndergroundBgStyle : ModUndergroundBackgroundStyle
	{
		public override void FillTextureArray(int[] textureSlots)
		{
			textureSlots[0] = BackgroundTextureLoader.GetBackgroundSlot("AAMod/Assets/Textures/Backgrounds/RedMush/RedMushUndergroundBackground1");
			textureSlots[1] = BackgroundTextureLoader.GetBackgroundSlot("AAMod/Assets/Textures/Backgrounds/RedMush/RedMushUndergroundBackground");
			textureSlots[2] = BackgroundTextureLoader.GetBackgroundSlot("AAMod/Assets/Textures/Backgrounds/RedMush/RedMushCavernBackground1");
			textureSlots[3] = BackgroundTextureLoader.GetBackgroundSlot("AAMod/Assets/Textures/Backgrounds/RedMush/RedMushCavernBackground");
		}
	}
}
