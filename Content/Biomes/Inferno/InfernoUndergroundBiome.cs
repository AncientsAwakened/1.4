using AAMod.Common.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Content.Biomes.Inferno
{
    public class InfernoUndergroundBiome : ModBiome
    {
        public override void SetStaticDefaults() 
            => DisplayName.SetDefault("Underground Inferno");

        public override SceneEffectPriority Priority 
            => SceneEffectPriority.BiomeHigh;

        public override void SpecialVisuals(Player player)
        {
            if (Main.UseHeatDistortion)
            {
                player.ManageSpecialBiomeVisuals("HeatDistortion", true);
            }
        }
        public override void OnLeave(Player player)
        {
            player.ManageSpecialBiomeVisuals("HeatDistortion", false);
        }
        public override ModUndergroundBackgroundStyle UndergroundBackgroundStyle 
            => ModContent.Find<ModUndergroundBackgroundStyle>("AAMod/InfernoUndergroundBgStyle");

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/InfernoCaverns");

        public override string BestiaryIcon => "Assets/Textures/Bestiary/InfernoIcon";
        public override string BackgroundPath => "Assets/Textures/Map/InfernoMap";
        public override Color? BackgroundColor => base.BackgroundColor;

        public override bool IsBiomeActive(Player player)
        {
            return (player.ZoneRockLayerHeight || player.ZoneDirtLayerHeight) && ModContent.GetInstance<TileCountSystem>().InfernoTileCount > 100;
        } 
    }
}