using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using System;

namespace AAMod.Common.Systems.UI
{
    public sealed class UISystem : ModSystem
    {
        public List<GameInterfaceLayer> InterfaceLayers;

        private Dictionary<Type, UIEntry> entries;
        private GameTime lastGameTime;

        public override void OnModLoad()
        {
            if (!Main.dedServ)
            {
                entries = new();
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            foreach (UIEntry entry in entries.Values)
            {
                if (entry.UserInterface != null && entry.UserInterface.IsVisible)
                {
                    entry.UserInterface.Update(gameTime);
                }
            }

            lastGameTime = gameTime;
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            foreach (UIEntry entry in entries.Values)
            {
                string name = $"{AAMod.AbbreviationPrefix} {entry.State.GetType().Name}";

                layers.Insert(entry.InsertionIndex, new LegacyGameInterfaceLayer(name, delegate {
                    if (lastGameTime != null && entry.UserInterface != null && entry.UserInterface.IsVisible)
                    {
                        entry.UserInterface.Draw(Main.spriteBatch, lastGameTime);
                    }

                    return true;
                }));
            }

            InterfaceLayers = layers;
        }

        public T ActivateUI<T>(T state, int insertionIndex) where T : UIState
        {
            state.Activate();

            UserInterface userInterface = new();
            userInterface.IsVisible = true;
            userInterface.SetState(state);

            entries[typeof(T)] = new UIEntry(state, userInterface, insertionIndex);

            return state;
        }

        public void DeactivateUI<T>() where T : UIState
        {
            if (TryGetUIEntry<T>(out UIEntry entry))
            {
                entry.UserInterface.IsVisible = false;
                entry.UserInterface.SetState(null);
                entry.State.Deactivate();

                entries.Remove(typeof(T));
            }
        }

        public bool TryGetUIEntry<T>(out UIEntry entry) where T : UIState => entries.TryGetValue(typeof(T), out entry);
    }
}