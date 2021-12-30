using Terraria.ModLoader;

namespace AAMod.Content.Biomes.Inferno
{
    public class InfernoUndergroundBgStyle : ModUndergroundBackgroundStyle
	{
		public override void FillTextureArray(int[] textureSlots)
		{
			textureSlots[0] = BackgroundTextureLoader.GetBackgroundSlot("AAMod/Assets/Textures/Backgrounds/Inferno/InfernoUndergroundBackground1");
			textureSlots[1] = BackgroundTextureLoader.GetBackgroundSlot("AAMod/Assets/Textures/Backgrounds/Inferno/InfernoUndergroundBackground");
			textureSlots[2] = BackgroundTextureLoader.GetBackgroundSlot("AAMod/Assets/Textures/Backgrounds/Inferno/InfernoCavernBackground1");
			textureSlots[3] = BackgroundTextureLoader.GetBackgroundSlot("AAMod/Assets/Textures/Backgrounds/Inferno/InfernoCavernBackground");
		}
	}
}
