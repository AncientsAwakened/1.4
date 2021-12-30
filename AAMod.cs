using AAMod.Content.Biomes.Inferno;
using AAMod.Content.Biomes.Mire;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace AAMod 
{
    public class AAMod : Mod
    {
        public const string Abbreviation = "AA";
        public const string AbbreviationPrefix = Abbreviation + ":";
        public const string InvisibleTexture = "AAMod/Assets/Textures/InvisibleTexture";
        public const string PlaceholderTexture = "AAMod/Assets/Textures/PlaceholderTexture";
        public override void Load()
        {
            SkyManager.Instance["InfernoSky"] = new InfernoSky();
            SkyManager.Instance["MireSky"] = new MireSky();
            base.Load();
        }
        public static AAMod Instance => ModContent.GetInstance<AAMod>();
    }
}
