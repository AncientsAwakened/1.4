using AAMod.Common.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Content.Biomes.Void
{
    public class VoidBiome : ModBiome
    {
        public override void SetStaticDefaults() 
            => DisplayName.SetDefault("The Void");

        public override SceneEffectPriority Priority 
            => SceneEffectPriority.BiomeHigh;

        public override int Music =>  MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Void");

        public override string BestiaryIcon => "Assets/Textures/Bestiary/VoidIcon";
        public override string BackgroundPath => "Assets/Textures/Map/VoidMap";
        public override Color? BackgroundColor => base.BackgroundColor;

        public override bool IsBiomeActive(Player player)
        {
            return (player.ZoneSkyHeight || player.ZoneOverworldHeight) && ModContent.GetInstance<TileCountSystem>().VoidTileCount > 100;
        }
    }
}