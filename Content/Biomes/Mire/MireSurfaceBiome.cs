using AAMod.Common.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;

namespace AAMod.Content.Biomes.Mire
{
    public class MireSurfaceBiome : ModBiome
    {
        public override void SetStaticDefaults() 
            => DisplayName.SetDefault("The Mire");

        public override SceneEffectPriority Priority 
            => SceneEffectPriority.BiomeHigh;

        public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle
    => ModContent.Find<ModSurfaceBackgroundStyle>("AAMod/MireSurfaceBgStyle");
        public override void SpecialVisuals(Player player)
        {
            Filters.Scene["AA:FogOverlay"]?.GetShader().UseOpacity(!Main.dayTime ? 0.05f : 0.8f).UseIntensity(0.90f).UseColor(Color.DarkSeaGreen).UseImage(ModContent.Request<Texture2D>("AAMod/Assets/Textures/Noise/Perlin", AssetRequestMode.ImmediateLoad).Value);
            player.ManageSpecialBiomeVisuals("AA:FogOverlay", player.InModBiome(ModContent.GetInstance<MireSurfaceBiome>()));
            SkyManager.Instance.Activate("MireSky");
            if (Main.bloodMoon)
            {
                Filters.Scene["AA:FogOverlay"]?.GetShader().UseOpacity(Main.dayTime ? 0.02f : 0.4f).UseIntensity(0.75f).UseColor(Color.Crimson).UseImage(ModContent.Request<Texture2D>("AAMod/Assets/Textures/Noise/Perlin", AssetRequestMode.ImmediateLoad).Value);
            }
        }
        public override void OnLeave(Player player)
        {
            player.ManageSpecialBiomeVisuals("AA:FogOverlay", false);
            SkyManager.Instance.Deactivate("MireSky");
        }

        public override ModWaterStyle WaterStyle => ModContent.Find<ModWaterStyle>("AAMod/MireWaterStyle");



        public override int Music => Main.dayTime ? MusicLoader.GetMusicSlot(Mod, "Sounds/Music/MireDay") : MusicLoader.GetMusicSlot(Mod, "Sounds/Music/MireNight");

        public override string BestiaryIcon => "Assets/Textures/Bestiary/MireIcon";
        public override string BackgroundPath => "Assets/Textures/Map/MireMap";
        public override Color? BackgroundColor => base.BackgroundColor;

        public override bool IsBiomeActive(Player player)
        {
            return (player.ZoneSkyHeight || player.ZoneOverworldHeight) && ModContent.GetInstance<TileCountSystem>().MireTileCount > 100;
        }
    }
}