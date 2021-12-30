using AAMod.Common.Globals;
using AAMod.Common.Systems.UI;
using AAMod.Content.Buffs;
using AAMod.Content.Items.Weapons.FreedomStars;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.UI;

namespace AAMod.Common.Systems
{
    class FreedomStarUI : UIState
    {
        public FreedomUI free;
        public override void OnInitialize()
        {
            free = new FreedomUI()
            {
                Left = { Pixels = 0 },
                Top = { Pixels = 0 }
            };
            Append(free);
        }
        public override void Update(GameTime gameTime)
        {
            if (Main.LocalPlayer.HeldItem != free.item)
            {
                UISystem system = ModContent.GetInstance<UISystem>();
                system.DeactivateUI<FreedomStarUI>();
            }
        }
    }
    class FreedomUI : UIElement
    {
        public Item item = Main.LocalPlayer.HeldItem;
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            Asset<Texture2D> UI = ModContent.Request<Texture2D>("AAMod/Assets/Textures/FreedomUI");
            Asset<Texture2D> Shard1 = ModContent.Request<Texture2D>("AAMod/Content/Items/Materials/Energy/AcidShard");
            Asset<Texture2D> Shard2 = ModContent.Request<Texture2D>("AAMod/Content/Items/Materials/Energy/DarkShard");
            Asset<Texture2D> Shard3 = ModContent.Request<Texture2D>("AAMod/Content/Items/Materials/Energy/ElectricShard");
            Asset<Texture2D> Shard4 = ModContent.Request<Texture2D>("AAMod/Content/Items/Materials/Energy/FireShard");
            Asset<Texture2D> Shard5 = ModContent.Request<Texture2D>("AAMod/Content/Items/Materials/Energy/IceShard");
            Asset<Texture2D> Shard6 = ModContent.Request<Texture2D>("AAMod/Content/Items/Materials/Energy/LightShard");
            Asset<Texture2D> Shard7 = ModContent.Request<Texture2D>("AAMod/Content/Items/Materials/Energy/PowerShard");
            Asset<Texture2D> Shard8 = ModContent.Request<Texture2D>("AAMod/Content/Items/Materials/Energy/PsychicShard");
            int ShardY = (Main.screenHeight / 2) - 30;
            spriteBatch.Draw(UI.Value, new Rectangle((Main.screenWidth / 2) - 79, (Main.screenHeight / 2) - 30, UI.Width(), UI.Height()), Color.White);
            spriteBatch.Draw(Shard1.Value, new Rectangle((Main.screenWidth / 2) - 75, (Main.screenHeight / 2) - 26, 6, 6), Color.White);
            int Shard1X = (Main.screenWidth / 2) - 79;
            bool Hovering1 = Main.mouseX > Shard1X && Main.mouseX < Shard1X + 14 && Main.mouseY > ShardY && Main.mouseY < ShardY + 14 && !PlayerInput.IgnoreMouseInterface;
            if (Hovering1)
            {
                Main.LocalPlayer.mouseInterface = true;
                if (Main.mouseLeftRelease && Main.mouseLeft)
                {
                    int Check1 = Main.LocalPlayer.HeldItem.GetGlobalItem<AAGlobalItemModular>().OutputCoolant;
                    int Check2 = Main.LocalPlayer.HeldItem.GetGlobalItem<AAGlobalItemModular>().OutputOverclock;
                    int Check3 = Main.LocalPlayer.HeldItem.GetGlobalItem<AAGlobalItemModular>().OutputPowercell;
                    int Energy = Main.LocalPlayer.HeldItem.GetGlobalItem<AAGlobalItemModular>().CurrentEnergy;
                    Main.LocalPlayer.HeldItem.SetDefaults(ModContent.ItemType<FreedomStarAcid>());
                    Main.LocalPlayer.HeldItem.GetGlobalItem<AAGlobalItemModular>().OutputCoolant = Check1;
                    Main.LocalPlayer.HeldItem.GetGlobalItem<AAGlobalItemModular>().OutputOverclock = Check2;
                    Main.LocalPlayer.HeldItem.GetGlobalItem<AAGlobalItemModular>().OutputPowercell = Check3;
                    Main.LocalPlayer.HeldItem.GetGlobalItem<AAGlobalItemModular>().CurrentEnergy = Energy;
                    Main.LocalPlayer.HeldItem.GetGlobalItem<AAGlobalItemModular>().EnergyCooldown = true;
                    Main.LocalPlayer.HeldItem.GetGlobalItem<AAGlobalItemModular>().FreedomSwitchCooldown = true;
                    UISystem system = ModContent.GetInstance<UISystem>();
                    system.DeactivateUI<FreedomStarUI>();
                }
            }
            spriteBatch.Draw(UI.Value, new Rectangle((Main.screenWidth / 2) - 61, (Main.screenHeight / 2) - 30, UI.Width(), UI.Height()), Color.White);
            spriteBatch.Draw(Shard2.Value, new Rectangle((Main.screenWidth / 2) - 57, (Main.screenHeight / 2) - 26, 6, 6), Color.White);
            int Shard2X = (Main.screenWidth / 2) - 61;
            bool Hovering2 = Main.mouseX > Shard2X && Main.mouseX < Shard2X + 14 && Main.mouseY > ShardY && Main.mouseY < ShardY + 14 && !PlayerInput.IgnoreMouseInterface;
            if (Hovering2)
            {
                Main.LocalPlayer.mouseInterface = true;
                if (Main.mouseLeftRelease && Main.mouseLeft)
                {
                    UISystem system = ModContent.GetInstance<UISystem>();
                    system.DeactivateUI<FreedomStarUI>();
                }
            }
            spriteBatch.Draw(UI.Value, new Rectangle((Main.screenWidth / 2) - 43, (Main.screenHeight / 2) - 30, UI.Width(), UI.Height()), Color.White);
            spriteBatch.Draw(Shard3.Value, new Rectangle((Main.screenWidth / 2) - 39, (Main.screenHeight / 2) - 26, 6, 6), Color.White);
            int Shard3X = (Main.screenWidth / 2) - 43;
            bool Hovering3 = Main.mouseX > Shard3X && Main.mouseX < Shard3X + 14 && Main.mouseY > ShardY && Main.mouseY < ShardY + 14 && !PlayerInput.IgnoreMouseInterface;
            if (Hovering3)
            {
                Main.LocalPlayer.mouseInterface = true;
                if (Main.mouseLeftRelease && Main.mouseLeft)
                {
                    UISystem system = ModContent.GetInstance<UISystem>();
                    system.DeactivateUI<FreedomStarUI>();
                }
            }
            spriteBatch.Draw(UI.Value, new Rectangle((Main.screenWidth / 2) - 25, (Main.screenHeight / 2) - 30, UI.Width(), UI.Height()), Color.White);
            spriteBatch.Draw(Shard4.Value, new Rectangle((Main.screenWidth / 2) - 21, (Main.screenHeight / 2) - 26, 6, 6), Color.White);
            int Shard4X = (Main.screenWidth / 2) - 25;
            bool Hovering4 = Main.mouseX > Shard4X && Main.mouseX < Shard4X + 14 && Main.mouseY > ShardY && Main.mouseY < ShardY + 14 && !PlayerInput.IgnoreMouseInterface;
            if (Hovering4)
            {
                Main.LocalPlayer.mouseInterface = true;
                if (Main.mouseLeftRelease && Main.mouseLeft)
                {
                    UISystem system = ModContent.GetInstance<UISystem>();
                    system.DeactivateUI<FreedomStarUI>();
                }
            }
            spriteBatch.Draw(UI.Value, new Rectangle((Main.screenWidth / 2) - 7, (Main.screenHeight / 2) - 30, UI.Width(), UI.Height()), Color.White);
            int ShardX = (Main.screenWidth / 2) - 7;
            bool Hovering = Main.mouseX > ShardX && Main.mouseX < ShardX + 14 && Main.mouseY > ShardY && Main.mouseY < ShardY + 14 && !PlayerInput.IgnoreMouseInterface;
            if (Hovering)
            {
                Main.LocalPlayer.mouseInterface = true;
                if (Main.mouseLeftRelease && Main.mouseLeft)
                {
                    int Check1 = Main.LocalPlayer.HeldItem.GetGlobalItem<AAGlobalItemModular>().OutputCoolant;
                    int Check2 = Main.LocalPlayer.HeldItem.GetGlobalItem<AAGlobalItemModular>().OutputOverclock;
                    int Check3 = Main.LocalPlayer.HeldItem.GetGlobalItem<AAGlobalItemModular>().OutputPowercell;
                    int Energy = Main.LocalPlayer.HeldItem.GetGlobalItem<AAGlobalItemModular>().CurrentEnergy;
                    Main.LocalPlayer.HeldItem.SetDefaults(ModContent.ItemType<FreedomStar>());
                    Main.LocalPlayer.HeldItem.GetGlobalItem<AAGlobalItemModular>().OutputCoolant = Check1;
                    Main.LocalPlayer.HeldItem.GetGlobalItem<AAGlobalItemModular>().OutputOverclock = Check2;
                    Main.LocalPlayer.HeldItem.GetGlobalItem<AAGlobalItemModular>().OutputPowercell = Check3;
                    Main.LocalPlayer.HeldItem.GetGlobalItem<AAGlobalItemModular>().CurrentEnergy = Energy;
                    Main.LocalPlayer.HeldItem.GetGlobalItem<AAGlobalItemModular>().EnergyCooldown = true;
                    Main.LocalPlayer.HeldItem.GetGlobalItem<AAGlobalItemModular>().FreedomSwitchCooldown = true;
                    UISystem system = ModContent.GetInstance<UISystem>();
                    system.DeactivateUI<FreedomStarUI>();
                }
            }
            spriteBatch.Draw(UI.Value, new Rectangle((Main.screenWidth / 2) + 11, (Main.screenHeight / 2) - 30, UI.Width(), UI.Height()), Color.White);
            spriteBatch.Draw(Shard5.Value, new Rectangle((Main.screenWidth / 2) + 15, (Main.screenHeight / 2) - 26, 6, 6), Color.White);
            int Shard5X = (Main.screenWidth / 2) + 11;
            bool Hovering5 = Main.mouseX > Shard5X && Main.mouseX < Shard5X + 14 && Main.mouseY > ShardY && Main.mouseY < ShardY + 14 && !PlayerInput.IgnoreMouseInterface;
            if (Hovering5)
            {
                Main.LocalPlayer.mouseInterface = true;
                if (Main.mouseLeftRelease && Main.mouseLeft)
                {
                    UISystem system = ModContent.GetInstance<UISystem>();
                    system.DeactivateUI<FreedomStarUI>();
                }
            }
            spriteBatch.Draw(UI.Value, new Rectangle((Main.screenWidth / 2) + 29, (Main.screenHeight / 2) - 30, UI.Width(), UI.Height()), Color.White);
            spriteBatch.Draw(Shard6.Value, new Rectangle((Main.screenWidth / 2) + 33, (Main.screenHeight / 2) - 26, 6, 6), Color.White);
            int Shard6X = (Main.screenWidth / 2) + 29;
            bool Hovering6 = Main.mouseX > Shard6X && Main.mouseX < Shard6X + 14 && Main.mouseY > ShardY && Main.mouseY < ShardY + 14 && !PlayerInput.IgnoreMouseInterface;
            if (Hovering6)
            {
                Main.LocalPlayer.mouseInterface = true;
                if (Main.mouseLeftRelease && Main.mouseLeft)
                {
                    int Check1 = Main.LocalPlayer.HeldItem.GetGlobalItem<AAGlobalItemModular>().OutputCoolant;
                    int Check2 = Main.LocalPlayer.HeldItem.GetGlobalItem<AAGlobalItemModular>().OutputOverclock;
                    int Check3 = Main.LocalPlayer.HeldItem.GetGlobalItem<AAGlobalItemModular>().OutputPowercell;
                    int Energy = Main.LocalPlayer.HeldItem.GetGlobalItem<AAGlobalItemModular>().CurrentEnergy;
                    Main.LocalPlayer.HeldItem.SetDefaults(ModContent.ItemType<FreedomStarLight>());
                    Main.LocalPlayer.HeldItem.GetGlobalItem<AAGlobalItemModular>().OutputCoolant = Check1;
                    Main.LocalPlayer.HeldItem.GetGlobalItem<AAGlobalItemModular>().OutputOverclock = Check2;
                    Main.LocalPlayer.HeldItem.GetGlobalItem<AAGlobalItemModular>().OutputPowercell = Check3;
                    Main.LocalPlayer.HeldItem.GetGlobalItem<AAGlobalItemModular>().CurrentEnergy = Energy;
                    Main.LocalPlayer.HeldItem.GetGlobalItem<AAGlobalItemModular>().EnergyCooldown = true;
                    Main.LocalPlayer.HeldItem.GetGlobalItem<AAGlobalItemModular>().FreedomSwitchCooldown = true;
                    UISystem system = ModContent.GetInstance<UISystem>();
                    system.DeactivateUI<FreedomStarUI>();
                }
            }
            spriteBatch.Draw(UI.Value, new Rectangle((Main.screenWidth / 2) + 47, (Main.screenHeight / 2) - 30, UI.Width(), UI.Height()), Color.White);
            spriteBatch.Draw(Shard7.Value, new Rectangle((Main.screenWidth / 2) + 51, (Main.screenHeight / 2) - 26, 6, 6), Color.White);
            int Shard7X = (Main.screenWidth / 2) + 47;
            bool Hovering7 = Main.mouseX > Shard7X && Main.mouseX < Shard7X + 14 && Main.mouseY > ShardY && Main.mouseY < ShardY + 14 && !PlayerInput.IgnoreMouseInterface;
            if (Hovering7)
            {
                Main.LocalPlayer.mouseInterface = true;
                if (Main.mouseLeftRelease && Main.mouseLeft)
                {
                    UISystem system = ModContent.GetInstance<UISystem>();
                    system.DeactivateUI<FreedomStarUI>();
                }
            }
            spriteBatch.Draw(UI.Value, new Rectangle((Main.screenWidth / 2) + 65, (Main.screenHeight / 2) - 30, UI.Width(), UI.Height()), Color.White);
            spriteBatch.Draw(Shard8.Value, new Rectangle((Main.screenWidth / 2) + 69, (Main.screenHeight / 2) - 26, 6, 6), Color.White);
            int Shard8X = (Main.screenWidth / 2) + 65;
            bool Hovering8 = Main.mouseX > Shard8X && Main.mouseX < Shard8X + 14 && Main.mouseY > ShardY && Main.mouseY < ShardY + 14 && !PlayerInput.IgnoreMouseInterface;
            if (Hovering8)
            {
                Main.LocalPlayer.mouseInterface = true;
                if (Main.mouseLeftRelease && Main.mouseLeft)
                {
                    UISystem system = ModContent.GetInstance<UISystem>();
                    system.DeactivateUI<FreedomStarUI>();
                }
            }
        }
    }
}