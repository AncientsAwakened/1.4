#region DoNotTouch
using AAMod.Common.Globals;
using AAMod.Common.Systems.UI;
using AAMod.Content.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace AAMod.Common.Systems
{
    class EnergyMeter : UIState
    {
        public EnergyGauge element;
        public override void OnInitialize()
        {
            element = new EnergyGauge();
            Append(element);
        }
        public override void Update(GameTime gameTime)
        {
            if (Main.LocalPlayer.HeldItem.TryGetGlobalItem(out AAGlobalItemModular _) && !Main.LocalPlayer.HeldItem.GetGlobalItem<AAGlobalItemModular>().OutputWeapon)
            {
                UISystem system = ModContent.GetInstance<UISystem>();
                system.DeactivateUI<EnergyMeter>();
            }
        }
    }
    class EnergyGauge : UIElement
    {
        private bool[] EnergyPercent1 = new bool[] { true, true, true, true, true, true, true, true, true, true };
        private bool[] EnergyPercent2 = new bool[] { true, true, true, true, true, true, true, true, true, true };
        public Item item = Main.LocalPlayer.HeldItem;
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            Texture2D Bars = ModContent.Request<Texture2D>("AAMod/Assets/Textures/EnergyBar").Value;
            for (int i = 0; i < 10; i++)
            {
                if (item.GetGlobalItem<AAGlobalItemModular>().CurrentEnergy <= (item.GetGlobalItem<AAGlobalItemModular>().OutputOverclock / 20) * (i + 10))
                {
                    EnergyPercent1[i] = false;
                }
                else
                {
                    EnergyPercent1[i] = true;
                }
            }
            for (int i = 0; i < 10; i++)
            {
                if (item.GetGlobalItem<AAGlobalItemModular>().CurrentEnergy <= (item.GetGlobalItem<AAGlobalItemModular>().OutputOverclock / 20) * i)
                {
                    EnergyPercent2[i] = false;
                }
                else
                {
                    EnergyPercent2[i] = true;
                }
            }
            for (int i = 0; i < 10; i++)
            {
                spriteBatch.Draw(Bars, new Rectangle((Main.screenWidth / 2) + 80 + (i * 12), (Main.screenHeight / 2) - 316, Bars.Width, Bars.Height), EnergyPercent1[i] ? new Color(23, 13, 182) : Color.White);
            }
            for (int i = 0; i < 10; i++)
            {
                spriteBatch.Draw(Bars, new Rectangle((Main.screenWidth / 2) + 80 + (i * 12), (Main.screenHeight / 2) - 300, Bars.Width, Bars.Height), EnergyPercent2[i] ? new Color(23, 13, 182) : Color.White);
            }
        }
    }
}
#endregion