using AAMod.Common.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Content.Biomes.Mire
{
    public class MireUndergroundBiome : ModBiome
    {
        public override void SetStaticDefaults() 
            => DisplayName.SetDefault("Underground Mire");

        public override SceneEffectPriority Priority 
            => SceneEffectPriority.BiomeHigh;

        public override ModWaterStyle WaterStyle => ModContent.Find<ModWaterStyle>("AAMod/MireWaterStyle");

        public override ModUndergroundBackgroundStyle UndergroundBackgroundStyle 
            => ModContent.Find<ModUndergroundBackgroundStyle>("AAMod/MireUndergroundBgStyle");

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/MireCaverns");

        public override string BestiaryIcon => "Assets/Textures/Bestiary/MireIcon";
        public override string BackgroundPath => "Assets/Textures/Map/MireMap";
        public override Color? BackgroundColor => base.BackgroundColor;

        public override bool IsBiomeActive(Player player)
        {
            return (player.ZoneRockLayerHeight || player.ZoneDirtLayerHeight) && ModContent.GetInstance<TileCountSystem>().MireTileCount > 100;
        } 
    }
}