using AAMod.Common.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Content.Biomes.RedMush
{
    public class RedMushUndergroundBiome : ModBiome
    {
        public override void SetStaticDefaults() 
            => DisplayName.SetDefault("Underground Red Mushland");

        public override SceneEffectPriority Priority 
            => SceneEffectPriority.BiomeHigh;

        public override ModWaterStyle WaterStyle => ModContent.Find<ModWaterStyle>("AAMod/RedMushWaterStyle");

        public override ModUndergroundBackgroundStyle UndergroundBackgroundStyle 
            => ModContent.Find<ModUndergroundBackgroundStyle>("AAMod/RedMushUndergroundBgStyle");

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/RedMushland");

        public override string BestiaryIcon => "Assets/Textures/Bestiary/RedMushIcon";
        public override string BackgroundPath => "Assets/Textures/Map/RedMushMap";
        public override Color? BackgroundColor => base.BackgroundColor;

        public override bool IsBiomeActive(Player player)
        {
            return (player.ZoneRockLayerHeight || player.ZoneDirtLayerHeight) && ModContent.GetInstance<TileCountSystem>().MireTileCount > 100;
        } 
    }
}