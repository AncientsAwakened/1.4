using AAMod.Common.Globals;
using AAMod.Common.Systems;
using AAMod.Common.Systems.UI;
using AAMod.Content.EnergyDamageClass;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Weapons.FreedomStars
{
    class FreedomStarLight : ModItem
    {
        public override string Texture => "AAMod/Content/Items/Weapons/FreedomStars/FreedomStar";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Freedom Star");
            Tooltip.SetDefault($"Tails' trusty blaster.\nHold the use button to charge, and then release a powerful Charged Shot!\n'Kept you waiting, huh?'\n- Tails");
        }
        public override void SetDefaults()
        {
            Item.DamageType = ModContent.GetInstance<OverallRangedEnergy>();
            Item.noMelee = true;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.width = 44;
            Item.height = 32;
            Item.rare = ItemRarityID.Cyan;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.damage = 40;
            Item.shoot = ModContent.ProjectileType<FreedomProjectileLight>();
        }
        public override bool AltFunctionUse(Player player)
        {
            UISystem system = ModContent.GetInstance<UISystem>();
            if (Item.GetGlobalItem<AAGlobalItemModular>().CheatedIn)
            {
                system.DeactivateUI<FreedomStarUI>();
            }
            else if (player.HeldItem.TryGetGlobalItem(out AAGlobalItemModular _) && player.HeldItem.GetGlobalItem<AAGlobalItemModular>().OutputWeapon)
            {
                int index = system.InterfaceLayers.FindIndex(layer => layer.Name == "Vanilla: Inventory");
                system.ActivateUI(new FreedomStarUI(), index);
            }
            else
            {
                system.DeactivateUI<FreedomStarUI>();
            }
            return false;
        }
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Asset<Texture2D> glowMask = ModContent.Request<Texture2D>("AAMod/Content/Items/Weapons/FreedomStars/FreedomStar_Glow");
            spriteBatch.Draw(glowMask.Value, position, frame, new Color(234, 234, 234), 0f, origin, scale, SpriteEffects.None, 0f);
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Asset<Texture2D> glowMask = ModContent.Request<Texture2D>("AAMod/Content/Items/Weapons/FreedomStars/FreedomStar_Glow");
            spriteBatch.Draw(glowMask.Value, new Vector2(Item.position.X - Main.screenPosition.X + Item.width * 0.5f, Item.position.Y - Main.screenPosition.Y + Item.height - glowMask.Height() * 0.5f + 2f), new Rectangle(0, 0, glowMask.Width(), glowMask.Height()), new Color(234, 234, 234), rotation, glowMask.Size() * 0.5f, scale, SpriteEffects.None, 0f);
        }
        public override bool CanShoot(Player player)
        {
            return !Item.GetGlobalItem<AAGlobalItemModular>().CheatedIn && Item.GetGlobalItem<AAGlobalItemModular>().CurrentEnergy > 0 && !Item.GetGlobalItem<AAGlobalItemModular>().FreedomSwitchCooldown;
        }
        public override void UpdateInventory(Player player)
        {
            Item.damage = 40;
            if (Item.GetGlobalItem<AAGlobalItemModular>().NERFFREEDOMSTAR >= 2)
            {
                Item.damage /= ((10 * Item.GetGlobalItem<AAGlobalItemModular>().NERFFREEDOMSTAR) - 10);
            }
            if (Item.GetGlobalItem<AAGlobalItemModular>().MagicNERF >= 1)
            {
                Item.damage -= (10 * Item.GetGlobalItem<AAGlobalItemModular>().MagicNERF);
            }
            if (Item.GetGlobalItem<AAGlobalItemModular>().MeleeNERF >= 1)
            {
                Item.damage -= (10 * Item.GetGlobalItem<AAGlobalItemModular>().MeleeNERF);
            }
            if (Item.GetGlobalItem<AAGlobalItemModular>().RangedNERF >= 2)
            {
                Item.damage -= ((10 * Item.GetGlobalItem<AAGlobalItemModular>().RangedNERF) - 10);
            }
            if (Item.GetGlobalItem<AAGlobalItemModular>().SummonNERF >= 1)
            {
                Item.damage -= (10 * Item.GetGlobalItem<AAGlobalItemModular>().SummonNERF);
            }
            UISystem system = ModContent.GetInstance<UISystem>();
            if (Item.GetGlobalItem<AAGlobalItemModular>().CheatedIn)
            {
                system.DeactivateUI<EnergyMeter>();
            }
            else if (player.HeldItem == Item)
            {
                int index = system.InterfaceLayers.FindIndex(layer => layer.Name == "Vanilla: Inventory");
                system.ActivateUI(new EnergyMeter(), index);
            }
            else
            {
                system.DeactivateUI<EnergyMeter>();
            }
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line in tooltips)
            {
                if (line.mod == "Terraria" && line.Name == "Damage")
                {
                    line.text = Item.damage + " Radiant Energy damage (Ranged damage)";
                    line.overrideColor = new Color(234, 234, 234);
                }
            }
            if (Item.GetGlobalItem<AAGlobalItemModular>().CheatedIn)
            {
                TooltipLine tooltip = new(Mod, "AACheat: Warning", "This is a CHEATED Modular Weapon and may not be used for Modification or Use.") { overrideColor = Color.Purple };
                tooltips.Add(tooltip);
            }
            if (Item.GetGlobalItem<AAGlobalItemModular>().OutputCoolant != -1)
            {
                TooltipLine tooltip = new(Mod, "AAEnergy: Coolant", Item.GetGlobalItem<AAGlobalItemModular>().CoolantText(Item.GetGlobalItem<AAGlobalItemModular>().OutputCoolant)) { overrideColor = Color.Blue };
                tooltips.Add(tooltip);
            }
            if (Item.GetGlobalItem<AAGlobalItemModular>().OutputPowercell != -1)
            {
                TooltipLine tooltip = new(Mod, "AAEnergy: Powercell", Item.GetGlobalItem<AAGlobalItemModular>().PowercellText(Item.GetGlobalItem<AAGlobalItemModular>().OutputPowercell)) { overrideColor = Color.Green };
                tooltips.Add(tooltip);
            }
            if (Item.GetGlobalItem<AAGlobalItemModular>().OutputOverclock != -1)
            {
                TooltipLine tooltip = new(Mod, "AAEnergy: Overclock", $"Overclock Ability: {Item.GetGlobalItem<AAGlobalItemModular>().CurrentEnergy} / {Item.GetGlobalItem<AAGlobalItemModular>().OutputOverclock}") { overrideColor = Color.Red };
                tooltips.Add(tooltip);
            }
        }
    }
    public class FreedomProjectileLight : ModProjectile
    {
        public int counter = 0;
        public int chargeLevel = 0;
        public override string Texture => "AAMod/Content/Items/Weapons/FreedomStars/FreedomStar2";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Freedom Star");
        }
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 44;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = ModContent.GetInstance<OverallRangedEnergy>();
            Projectile.ignoreWater = true;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            Rectangle sourceRectangle = new(0, 0, texture.Width, texture.Height);
            Vector2 origin = sourceRectangle.Size() / 2f;

            var position = Projectile.Center - Main.screenPosition + new Vector2(Projectile.spriteDirection == 1 ? 3f : -3f, -12f);

            Player player = Main.player[Projectile.owner];

            float num1 = 12f;
            Vector2 vector2 = new(player.position.X + player.width * 0.5f, player.position.Y + player.height * 0.5f);
            float f1 = Main.mouseX + Main.screenPosition.X - vector2.X;
            float f2 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
            if (player.gravDir == -1.0)
                f2 = Main.screenPosition.Y + Main.screenHeight - Main.mouseY - vector2.Y;
            float num4 = (float)Math.Sqrt(f1 * (double)f1 + f2 * (double)f2);
            float num5;
            if (float.IsNaN(f1) && float.IsNaN(f2) || f1 == 0.0 && f2 == 0.0)
            {
                f1 = player.direction;
                f2 = 0.0f;
                num5 = num1;
            }
            else
                num5 = num1 / num4;
            float SpeedX = f1 * num5;
            float SpeedY = f2 * num5;

            var Rot = new Vector2(SpeedX, SpeedY).ToRotation() + (Projectile.spriteDirection == 1 ? MathHelper.ToRadians(0f) : MathHelper.ToRadians(180f));

            Asset<Texture2D> glowMask = ModContent.Request<Texture2D>("AAMod/Content/Items/Weapons/FreedomStars/FreedomStar2_Glow");

            Color color = new(234, 234, 234);

            Main.EntitySpriteDraw(texture, new Vector2((int)position.X + (26 * Projectile.spriteDirection), (int)position.Y + (6 * Projectile.spriteDirection)).RotatedBy(Rot, player.MountedCenter - Main.screenPosition), sourceRectangle, Color.White, new Vector2(SpeedX, SpeedY).ToRotation() + MathHelper.ToRadians(90), origin, Projectile.scale, spriteEffects, 0);
            Main.EntitySpriteDraw(glowMask.Value, new Vector2((int)position.X + (26 * Projectile.spriteDirection), (int)position.Y + (6 * Projectile.spriteDirection)).RotatedBy(Rot, player.MountedCenter - Main.screenPosition), sourceRectangle, color, new Vector2(SpeedX, SpeedY).ToRotation() + MathHelper.ToRadians(90), origin, Projectile.scale, spriteEffects, 0);

            return false;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            float num = 1.57079637f;
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            Projectile.ai[0] += 1f;
            int num2 = 0;
            if (Projectile.ai[0] >= 30f)
            {
                num2++;
            }
            if (Projectile.ai[0] >= 60f)
            {
                num2++;
            }
            if (Projectile.ai[0] >= 90f)
            {
                num2++;
            }
            int num3 = 24;
            int num4 = 6;
            Projectile.ai[1] += 1f;
            bool flag = false;
            if (Projectile.ai[1] >= num3 - num4 * num2)
            {
                Projectile.ai[1] = 0f;
                flag = true;
            }
            if (Projectile.ai[1] == 1f && Projectile.ai[0] != 1f)
            {
                Vector2 vector2 = Vector2.UnitX * 24f;
                vector2 = vector2.RotatedBy(Projectile.rotation - 1.57079637f, default);
                Vector2 value = Projectile.Center + vector2;
                for (int i = 0; i < 3; i++)
                {
                    int num5 = Dust.NewDust(value - Vector2.One * 8f, 16, 16, DustID.GreenFairy, Projectile.velocity.X / 2f, Projectile.velocity.Y / 2f, 100);
                    Main.dust[num5].position.Y -= 0.3f;
                    Main.dust[num5].velocity *= 0.66f;
                    Main.dust[num5].noGravity = true;
                    Main.dust[num5].scale = 1.4f;
                }
            }
            if (flag && Main.myPlayer == Projectile.owner)
            {
                if (player.channel && !player.noItems && !player.CCed)
                {
                    float scaleFactor = player.inventory[player.selectedItem].shootSpeed * Projectile.scale;
                    Vector2 vector3 = vector;
                    Vector2 value2 = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY) - vector3;
                    if (player.gravDir == -1f)
                    {
                        value2.Y = Main.screenHeight - Main.mouseY + Main.screenPosition.Y - vector3.Y;
                    }
                    Vector2 vector4 = Vector2.Normalize(value2);
                    if (float.IsNaN(vector4.X) || float.IsNaN(vector4.Y))
                    {
                        vector4 = -Vector2.UnitY;
                    }
                    vector4 *= scaleFactor;
                    if (vector4.X != Projectile.velocity.X || vector4.Y != Projectile.velocity.Y)
                    {
                        Projectile.netUpdate = true;
                    }
                    Projectile.velocity = vector4;
                }
            }
            Projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - Projectile.Size / 2f;
            Projectile.rotation = Projectile.velocity.ToRotation() + num;
            if (Projectile.rotation >= MathHelper.ToRadians(360f))
            {
                Projectile.rotation -= MathHelper.ToRadians(360f);
            }
            if (Projectile.rotation < 0)
            {
                Projectile.rotation += MathHelper.ToRadians(360f);
            }
            Projectile.direction = Projectile.rotation <= MathHelper.ToRadians(180f) && Projectile.rotation >= MathHelper.ToRadians(0f) ? 1 : -1;
            Projectile.spriteDirection = Projectile.direction;
            Projectile.timeLeft = 2;
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;

            float num1 = 12f;
            Vector2 vector5 = new(player.position.X + player.width * 0.5f, player.position.Y + player.height * 0.5f);
            float f1 = Main.mouseX + Main.screenPosition.X - vector5.X;
            float f2 = Main.mouseY + Main.screenPosition.Y - vector5.Y;
            if (player.gravDir == -1.0)
                f2 = Main.screenPosition.Y + Main.screenHeight - Main.mouseY - vector5.Y;
            float num8 = (float)Math.Sqrt(f1 * (double)f1 + f2 * (double)f2);
            float num9;
            if (float.IsNaN(f1) && float.IsNaN(f2) || f1 == 0.0 && f2 == 0.0)
            {
                f1 = player.direction;
                f2 = 0.0f;
                num9 = num1;
            }
            else
                num9 = num1 / num8;
            float SpeedX = f1 * num9;
            float SpeedY = f2 * num9;

            var Rot = new Vector2(SpeedX, SpeedY).ToRotation() + (Projectile.spriteDirection == 1 ? MathHelper.ToRadians(0f) : MathHelper.ToRadians(180f));

            player.itemRotation = Rot;

            counter++;

            if (counter >= 90)
            {
                SoundEngine.PlaySound(SoundID.Item, (int)Projectile.position.X, (int)Projectile.position.Y, 93, 1, 1f);
                player.HeldItem.GetGlobalItem<AAGlobalItemModular>().CurrentEnergy--;
                player.HeldItem.GetGlobalItem<AAGlobalItemModular>().EnergyCooldown = true;
                chargeLevel = 3;
            }

            else if (counter >= 30)
            {
                SoundEngine.PlaySound(SoundID.Item, (int)Projectile.position.X, (int)Projectile.position.Y, 93);
                chargeLevel = 2;
            }

            else if (counter >= 0)
            {
                SoundEngine.PlaySound(SoundID.Item, (int)Projectile.position.X, (int)Projectile.position.Y, 13);
                chargeLevel = 1;
            }

            if (!player.channel || player.HeldItem.GetGlobalItem<AAGlobalItemModular>().CurrentEnergy <= 0)
            {
                Projectile.Kill();
            }
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.owner == Main.myPlayer && player.HeldItem.GetGlobalItem<AAGlobalItemModular>().CurrentEnergy > 0)
            {
                float num1 = 12f;
                Vector2 vector2 = new(player.position.X + player.width * 0.5f, player.position.Y + player.height * 0.5f);
                float f1 = Main.mouseX + Main.screenPosition.X - vector2.X;
                float f2 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
                if (player.gravDir == -1.0)
                    f2 = Main.screenPosition.Y + Main.screenHeight - Main.mouseY - vector2.Y;
                float num4 = (float)Math.Sqrt(f1 * (double)f1 + f2 * (double)f2);
                float num5;
                if (float.IsNaN(f1) && float.IsNaN(f2) || f1 == 0.0 && f2 == 0.0)
                {
                    f1 = player.direction;
                    f2 = 0.0f;
                    num5 = num1;
                }
                else
                    num5 = num1 / num4;
                float SpeedX = f1 * num5;
                float SpeedY = f2 * num5;
                switch (chargeLevel)
                {
                    case 1:
                        if (player.HeldItem.GetGlobalItem<AAGlobalItemModular>().CurrentEnergy >= 50)
                        {
                            SoundEngine.PlaySound(SoundID.Item, (int)Projectile.position.X, (int)Projectile.position.Y, 89);
                            player.HeldItem.GetGlobalItem<AAGlobalItemModular>().CurrentEnergy -= 50;
                            player.HeldItem.GetGlobalItem<AAGlobalItemModular>().EnergyCooldown = true;
                            Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(), vector2.X, vector2.Y, SpeedX, SpeedY, ModContent.ProjectileType<FreedomNormalShotLight>(), Projectile.damage, 1f, Projectile.owner);
                        }
                        break;
                    case 2:
                        if (player.HeldItem.GetGlobalItem<AAGlobalItemModular>().CurrentEnergy >= 150)
                        {
                            SoundEngine.PlaySound(SoundID.Item, (int)Projectile.position.X, (int)Projectile.position.Y, 88);
                            player.HeldItem.GetGlobalItem<AAGlobalItemModular>().CurrentEnergy -= 150;
                            player.HeldItem.GetGlobalItem<AAGlobalItemModular>().EnergyCooldown = true;
                            Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(), vector2.X, vector2.Y, SpeedX, SpeedY, ModContent.ProjectileType<FreedomChargeShotLight>(), Projectile.damage * 2, 1f, Projectile.owner);
                        }
                        break;
                    case 3:
                        if (player.HeldItem.GetGlobalItem<AAGlobalItemModular>().CurrentEnergy >= 150)
                        {
                            SoundEngine.PlaySound(SoundID.Item, (int)Projectile.position.X, (int)Projectile.position.Y, 88);
                            player.HeldItem.GetGlobalItem<AAGlobalItemModular>().CurrentEnergy -= 150;
                            player.HeldItem.GetGlobalItem<AAGlobalItemModular>().EnergyCooldown = true;
                            Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(), vector2.X, vector2.Y, SpeedX, SpeedY, ModContent.ProjectileType<FreedomChargeShotLight>(), Projectile.damage * 2, 1f, Projectile.owner);
                        }
                        break;
                }
            }
        }
    }
    class FreedomNormalShotLight : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Radiant Freedom Bullet");
        }
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 2;
            Projectile.friendly = true;
            Projectile.DamageType = ModContent.GetInstance<OverallRangedEnergy>();
            Projectile.ignoreWater = true;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
        }
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item, (int)Projectile.position.X, (int)Projectile.position.Y, 89);
            Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(), Projectile.Bottom, new Vector2(0, -1), ModContent.ProjectileType<FreedomNormalShotLightSecondary>(), Projectile.damage * 2, 1f, Projectile.owner);
        }
    }
    class FreedomChargeShotLight : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Radiant Freedom Shot");
        }
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 6;
            Projectile.friendly = true;
            Projectile.DamageType = ModContent.GetInstance<OverallRangedEnergy>();
            Projectile.ignoreWater = true;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
        }
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item, (int)Projectile.position.X, (int)Projectile.position.Y, 88);
            Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(), Projectile.Bottom, new Vector2(0, -1), ModContent.ProjectileType<FreedomChargeShotLightSecondary>(), Projectile.damage * 2, 1f, Projectile.owner);
        }
    }
    class FreedomNormalShotLightSecondary : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Radiant Freedom Sky Light");
        }
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 256;
            Projectile.friendly = false;
            Projectile.DamageType = ModContent.GetInstance<OverallRangedEnergy>();
            Projectile.ignoreWater = true;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
        }
        private bool fading;
        private int waitTime;
        public override void AI()
        {
            if (Projectile.alpha == 255 && !fading)
            {
                Projectile.position.Y = Projectile.position.Y - Projectile.height / 2f;
            }
            Projectile.velocity.Y = 0;
            if (Projectile.alpha > 0 && !fading)
            {
                Projectile.alpha -= 5;
                if (Projectile.alpha <= 200)
                {
                    Projectile.friendly = true;
                }
                if (Projectile.alpha == 0)
                {
                    fading = true;
                    waitTime = 60;
                }
            }
            if (waitTime > 0)
            {
                waitTime--;
            }
            if (Projectile.alpha < 255 && fading && waitTime == 0)
            {
                Projectile.alpha += 5;
                if (Projectile.alpha > 200)
                {
                    Projectile.friendly = false;
                }
                if (Projectile.alpha == 255)
                {
                    Projectile.Kill();
                }
            }
        }
    }
    class FreedomChargeShotLightSecondary : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Radiant Freedom Sky Beam");
        }
        public override void SetDefaults()
        {
            Projectile.width = 48;
            Projectile.height = 336;
            Projectile.friendly = false;
            Projectile.DamageType = ModContent.GetInstance<OverallRangedEnergy>();
            Projectile.ignoreWater = true;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
        }
        private bool fading;
        private int waitTime;
        public override void AI()
        {
            if (Projectile.alpha == 255 && !fading)
            {
                Projectile.position.Y = Projectile.position.Y - Projectile.height / 2f; 
            }
            Projectile.velocity.Y = 0;
            if (Projectile.alpha > 0 && !fading)
            {
                Projectile.alpha -= 5;
                if (Projectile.alpha <= 200)
                {
                    Projectile.friendly = true;
                }
                if (Projectile.alpha == 0)
                {
                    fading = true;
                    waitTime = 60;
                }
            }
            if (waitTime > 0)
            {
                waitTime--;
            }
            if (Projectile.alpha < 255 && fading && waitTime == 0)
            {
                Projectile.alpha += 5;
                if (Projectile.alpha > 200)
                {
                    Projectile.friendly = false;
                }
                if (Projectile.alpha == 255)
                {
                    Projectile.Kill();
                }
            }
        }
    }
}