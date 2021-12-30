using Terraria;
using Terraria.ModLoader;

namespace AAMod.Common.Systems
{
    public class WindowTitleSystem : ModSystem
    {
        public override void OnModLoad()
        {
            string[] titles =
            {
                "AA? You mean like the battery?",
                "Now with more final forms than Dragon Ball Z!",
                "It's like the old version, but with 8 less musicians",
                "Press space",
                "This.. This is just... I'm gonna... Maybe I... BEANS",
                "Fan service not included",
                "Also try Spirit!",
                "Also try Secrets of the Shadows!",
                "Also try Calamity!",
                "Also try Thorium!",
                "Also try Shadows of Abaddon!",
                "Also try Elements Awoken!",
                "Also try Mod of Redemption!",
                "Happy Dallinsday!",
                "Happy birthday!",
                "NYEHEHEHEHEHEHEHEHEHEHEHEHEHE...",
                "Made by devs that hate each other less... probably",
                "Do not question about the Tuna Can",
                "Its a feature™️",
                "Its ABOUT TIME.",
                "The mod that allows you to fight someone with their own daughter",
                "Now with less telefrags!",
                "Who live in a sunken ship under the Lovecraftian sea!",
                "Now you can kill all the bunnies you want! Please don’t",
                "Surtur on a lawn chair, what will he do?",
                "Screw you! I'm gonna go play Minecraft!",
                "You scream into the void, the void shoots you back!"
            };

            Main.instance.Window.Title = $"Ancients Awakened: {titles[Main.rand.Next(titles.Length)]}";
        }
    }
}