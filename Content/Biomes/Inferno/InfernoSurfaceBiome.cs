using AAMod.Common.Systems;
using Terraria.Graphics.Effects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Content.Biomes.Inferno
{
    public class InfernoSurfaceBiome : ModBiome
    {
        public override void SetStaticDefaults() 
            => DisplayName.SetDefault("The Inferno");

        public override SceneEffectPriority Priority 
            => SceneEffectPriority.BiomeHigh;

        public override void SpecialVisuals(Player player)
        {
            if (Main.UseHeatDistortion)
            {
                player.ManageSpecialBiomeVisuals("HeatDistortion", true);
                SkyManager.Instance.Activate("InfernoSky");
                Filters.Scene["AA:FogOverlay"]?.GetShader().UseOpacity(!Main.dayTime ? 0.25f : 0f).UseIntensity(1f).UseColor(Color.DimGray).UseImage(ModContent.Request<Texture2D>("AAMod/Assets/Textures/Noise/Perlin", AssetRequestMode.ImmediateLoad).Value);
                player.ManageSpecialBiomeVisuals("AA:FogOverlay", player.InModBiome(ModContent.GetInstance<InfernoSurfaceBiome>()));
            }
        }
        public override void OnLeave(Player player)
        {
            SkyManager.Instance.Deactivate("InfernoSky");
            player.ManageSpecialBiomeVisuals("HeatDistortion", false);
            player.ManageSpecialBiomeVisuals("AA:FogOverlay", false);
        }
        public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle 
            => ModContent.Find<ModSurfaceBackgroundStyle>("AAMod/InfernoSurfaceBgStyle");

        public override int Music => Main.dayTime ? MusicLoader.GetMusicSlot(Mod, "Sounds/Music/InfernoDay") : MusicLoader.GetMusicSlot(Mod, "Sounds/Music/InfernoNight");

        public override string BestiaryIcon => "Assets/Textures/Bestiary/InfernoIcon";
        public override string BackgroundPath => "Assets/Textures/Map/InfernoMap";
        public override Color? BackgroundColor => base.BackgroundColor;

        public override bool IsBiomeActive(Player player)
        {
            return (player.ZoneSkyHeight || player.ZoneOverworldHeight) && ModContent.GetInstance<TileCountSystem>().InfernoTileCount > 100;
        }
    }
}