using AAMod.Content.Biomes.Inferno;
using AAMod.Content.Biomes.Mire;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace AAMod.Content
{
    public class ChaosMenu : ModMenu
    {
        public override string DisplayName 
            => "Ancients Awakened (Chaos)";

        public override Asset<Texture2D> Logo 
            => ModContent.Request<Texture2D>("AAMod/Assets/Textures/Menu/Icon");

        public override Asset<Texture2D> SunTexture 
            => ModContent.Request<Texture2D>("AAMod/Assets/Textures/Menu/Sun");

        public override Asset<Texture2D> MoonTexture 
            => ModContent.Request<Texture2D>("AAMod/Assets/Textures/Menu/Moon");

        public override ModSurfaceBackgroundStyle MenuBackgroundStyle
            => Main.dayTime ? ModContent.GetInstance<InfernoSurfaceBgStyle>() : ModContent.GetInstance<MireSurfaceBgStyle>();

        public override int Music
            => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/ChaosMenu");

        public override void OnSelected()
        {
            SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Custom/MenuSelect").WithVolume(0.8f));
        }

        public override bool PreDrawLogo(SpriteBatch spriteBatch, ref Vector2 logoDrawCenter, ref float logoRotation, ref float logoScale, ref Color drawColor)
        {
            logoScale = 0.9f;

            return true;
        }
    }
}
