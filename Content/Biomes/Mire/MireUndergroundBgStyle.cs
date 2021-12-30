using Terraria.ModLoader;

namespace AAMod.Content.Biomes.Mire
{
    public class MireUndergroundBgStyle : ModUndergroundBackgroundStyle
	{
		public override void FillTextureArray(int[] textureSlots)
		{
			textureSlots[0] = BackgroundTextureLoader.GetBackgroundSlot("AAMod/Assets/Textures/Backgrounds/Mire/MireUndergroundBackground1");
			textureSlots[1] = BackgroundTextureLoader.GetBackgroundSlot("AAMod/Assets/Textures/Backgrounds/Mire/MireUndergroundBackground");
			textureSlots[2] = BackgroundTextureLoader.GetBackgroundSlot("AAMod/Assets/Textures/Backgrounds/Mire/MireCavernBackground1");
			textureSlots[3] = BackgroundTextureLoader.GetBackgroundSlot("AAMod/Assets/Textures/Backgrounds/Mire/MireCavernBackground");
		}
	}
}
