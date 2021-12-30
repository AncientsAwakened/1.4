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
    class Bar : UIState
    {
        public ProgressBarStruggle element;
        public override void OnInitialize()
        {
            element = new ProgressBarStruggle();
            Append(element);
        }
    }
    class ProgressBarStruggle : UIElement
    {
        public int TheProgress { get; private set; } = 250;
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Main.LocalPlayer.FindBuffIndex(ModContent.BuffType<Struggle>()) != -1)
            {
                TheProgress = 250 - Main.LocalPlayer.buffTime[Main.LocalPlayer.FindBuffIndex(ModContent.BuffType<Struggle>())];
            }
            else if (Main.LocalPlayer.FindBuffIndex(ModContent.BuffType<StruggleAlt>()) != -1)
            {
                TheProgress = 250 - Main.LocalPlayer.buffTime[Main.LocalPlayer.FindBuffIndex(ModContent.BuffType<StruggleAlt>())];
            }
            else
            {
                TheProgress = 250;
            }
            if (TheProgress <= 100)
            {
                Main.LocalPlayer.GetModPlayer<StrugglePlayer>().FailInstant = true;
            }
        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            Texture2D texture1 = ModContent.Request<Texture2D>("AAMod/Assets/Textures/ProgressE").Value;
            Texture2D texture2 = ModContent.Request<Texture2D>("AAMod/Assets/Textures/ProgressF").Value;
            Texture2D FrontTexture = ModContent.Request<Texture2D>("AAMod/icon_small").Value;
            spriteBatch.Draw(texture1, new Rectangle((Main.screenWidth / 2) - 68, (Main.screenHeight / 2) + 40, texture1.Width, texture1.Height), Main.LocalPlayer.GetModPlayer<StrugglePlayer>().FailInstant ? Color.Red : new Color(200, 70, 200));
            if (Main.LocalPlayer.FindBuffIndex(ModContent.BuffType<Struggle>()) != -1)
            {
                spriteBatch.Draw(texture2, new Rectangle((Main.screenWidth / 2) - 68, (Main.screenHeight / 2) + 40, Main.LocalPlayer.GetModPlayer<StrugglePlayer>().FailInstant ? 0 : (int)(texture2.Width * ((250 - Main.LocalPlayer.buffTime[Main.LocalPlayer.FindBuffIndex(ModContent.BuffType<Struggle>())]) / 250f)), texture2.Height), Main.LocalPlayer.GetModPlayer<StrugglePlayer>().FailInstant ? Color.Red : new Color(70, 200, 70));
            }
            else if (Main.LocalPlayer.FindBuffIndex(ModContent.BuffType<StruggleAlt>()) != -1)
            {
                spriteBatch.Draw(texture2, new Rectangle((Main.screenWidth / 2) - 68, (Main.screenHeight / 2) + 40, Main.LocalPlayer.GetModPlayer<StrugglePlayer>().FailInstant ? 0 : (int)(texture2.Width * ((250 - Main.LocalPlayer.buffTime[Main.LocalPlayer.FindBuffIndex(ModContent.BuffType<StruggleAlt>())]) / 250f)), texture2.Height), Main.LocalPlayer.GetModPlayer<StrugglePlayer>().FailInstant ? Color.Red : new Color(70, 200, 70));
            }
            else
            {
                spriteBatch.Draw(texture2, new Rectangle((Main.screenWidth / 2) - 68, (Main.screenHeight / 2) + 40, Main.LocalPlayer.GetModPlayer<StrugglePlayer>().FailInstant ? 0 : texture2.Width, texture2.Height), Main.LocalPlayer.GetModPlayer<StrugglePlayer>().FailInstant ? Color.Red : new Color(70, 200, 70));
            }
            spriteBatch.Draw(FrontTexture, new Rectangle((Main.screenWidth / 2) - 68, (Main.screenHeight / 2) + 39, FrontTexture.Width, FrontTexture.Height), Color.White);
        }
    }
}