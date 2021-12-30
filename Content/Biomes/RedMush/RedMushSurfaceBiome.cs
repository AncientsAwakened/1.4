using AAMod.Common.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Content.Biomes.RedMush
{
    public class RedMushSurfaceBiome : ModBiome
    {
        public override void SetStaticDefaults() 
            => DisplayName.SetDefault("Red Mushland");

        public override SceneEffectPriority Priority 
            => SceneEffectPriority.BiomeHigh;

        public override void SpecialVisuals(Player player)
        {
            Terraria.Graphics.Effects.Filters.Scene["AA:FogOverlay"]?.GetShader().UseOpacity(0.2f).UseIntensity(1f).UseColor(Color.LightGreen).UseImage(ModContent.Request<Texture2D>("AAMod/Assets/Textures/Noise/Perlin", AssetRequestMode.ImmediateLoad).Value);
            player.ManageSpecialBiomeVisuals("AA:FogOverlay", player.InModBiome(ModContent.GetInstance<RedMushSurfaceBiome>()));
        }
        public override void OnLeave(Player player)
        {
            player.ManageSpecialBiomeVisuals("AA:FogOverlay", false);
        }

        public override ModWaterStyle WaterStyle => ModContent.Find<ModWaterStyle>("AAMod/RedMushWaterStyle");

        public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle 
            => ModContent.Find<ModSurfaceBackgroundStyle>("AAMod/RedMushSurfaceBgStyle");

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/RedMushland");

        public override string BestiaryIcon => "Assets/Textures/Bestiary/RedMushIcon";
        public override string BackgroundPath => "Assets/Textures/Map/RedMushMap";
        public override Color? BackgroundColor => base.BackgroundColor;

        public override bool IsBiomeActive(Player player)
        {
            return (player.ZoneSkyHeight || player.ZoneOverworldHeight) && ModContent.GetInstance<TileCountSystem>().RedMushTileCount > 100;
        }
    }
}