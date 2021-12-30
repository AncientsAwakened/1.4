using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using ReLogic.Content;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;

namespace AAMod.Common.Loaders
{
    internal sealed class EffectLoader : ILoadable
    {
        public void Load(Mod mod)
        {
            TmodFile file = typeof(Mod).GetProperty("File", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(AAMod.Instance) as TmodFile;
            IEnumerable<TmodFile.FileEntry> entries = file.Where(fileName => fileName.Name.StartsWith("Assets/Effects/") && fileName.Name.EndsWith(".xnb"));

            foreach (TmodFile.FileEntry entry in entries)
            {
                string path = entry.Name.Replace(".xnb", string.Empty);
                string name = Path.GetFileNameWithoutExtension(path);

                LoadEffect(mod, path, name);
            }
        }

        public void Unload() { }

        private static void LoadEffect(Mod mod, string path, string name)
        {
            Ref<Effect> effectRef = new(mod.Assets.Request<Effect>(path, AssetRequestMode.ImmediateLoad).Value);

            Filter filter = new(new ScreenShaderData(effectRef, name + "Pass"), EffectPriority.High);
            Filter effect = Filters.Scene[AAMod.AbbreviationPrefix + name] = filter;

            effect.Load();

            mod.Logger.Debug($"Loaded screen effect: {name}");
        }
    }
}
