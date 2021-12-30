using AAMod.Common.Globals;
using AAMod.Common.Systems;
using AAMod.Common.Systems.UI;
using AAMod.Content.Buffs;
using AAMod.Content.EnergyDamageClass;
using AAMod.Content.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Weapons.Energy
{
    class MobianBuster : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mobian Buster");
            Tooltip.SetDefault($"A standard issue Mobian blaster.\nHold the use button to charge, and then release a powerful Charged Shot!\n'Remember, the charged shot fires when you RELEASE the trigger, not the other way around.'\n- Tails");
        }
        public override void SetDefaults()
        {
            Item.DamageType = ModContent.GetInstance<OverallRangedEnergy>();
            Item.noMelee = true;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.width = 46;
            Item.height = 32;
            Item.rare = ItemRarityID.Cyan;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.damage = 32;
            Item.shoot = ModContent.ProjectileType<MobianBusterProjectile>();
        }
        #region RequiredForEnergyWeapon
        public override bool CanShoot(Player player)
        {
            return !Item.GetGlobalItem<AAGlobalItemModular>().CheatedIn && Item.GetGlobalItem<AAGlobalItemModular>().CurrentEnergy > 0;
        }
        public override void UpdateInventory(Player player)
        {
            Item.damage = Item.GetGlobalItem<AAGlobalItemModular>().OutputScrap != -1 ? (Item.GetGlobalItem<AAGlobalItemModular>().OutputScrap * 4) + 32 : 0;
            if (Item.GetGlobalItem<AAGlobalItemModular>().RangedNERF >= 2)
            {
                Item.damage -= ((8 * Item.GetGlobalItem<AAGlobalItemModular>().RangedNERF) - 8);
            }
            UISystem system = ModContent.GetInstance<UISystem>();
            if (Item.GetGlobalItem<AAGlobalItemModular>().CheatedIn)
            {
                system.DeactivateUI<EnergyMeter>();
            }
            else if (player.HeldItem.TryGetGlobalItem(out AAGlobalItemModular _) && player.HeldItem.GetGlobalItem<AAGlobalItemModular>().OutputWeapon)
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
                    if (Item.GetGlobalItem<AAGlobalItemModular>().OutputShard == 0)
                    {
                        line.overrideColor = new Color(23, 13, 182);
                    }
                    if (Item.GetGlobalItem<AAGlobalItemModular>().OutputShard == 1)
                    {
                        line.text = Item.damage + " Acid Energy damage (Ranged damage)";
                        line.overrideColor = new Color(12, 240, 21);
                    }
                    if (Item.GetGlobalItem<AAGlobalItemModular>().OutputShard == 2)
                    {
                        line.text = Item.damage + " Necrotic Energy damage (Ranged damage)";
                        line.overrideColor = new Color(30, 30, 30);
                    }
                    if (Item.GetGlobalItem<AAGlobalItemModular>().OutputShard == 3)
                    {
                        line.text = Item.damage + " Electric Energy damage (Ranged damage)";
                        line.overrideColor = new Color(232, 219, 36);
                    }
                    if (Item.GetGlobalItem<AAGlobalItemModular>().OutputShard == 4)
                    {
                        line.text = Item.damage + " Fire Energy damage (Ranged damage)";
                        line.overrideColor = new Color(195, 13, 13);
                    }
                    if (Item.GetGlobalItem<AAGlobalItemModular>().OutputShard == 5)
                    {
                        line.text = Item.damage + " Ice Energy damage (Ranged damage)";
                        line.overrideColor = new Color(12, 186, 187);
                    }
                    if (Item.GetGlobalItem<AAGlobalItemModular>().OutputShard == 6)
                    {
                        line.text = Item.damage + " Radiant Energy damage (Ranged damage)";
                        line.overrideColor = new Color(234, 234, 234);
                    }
                    if (Item.GetGlobalItem<AAGlobalItemModular>().OutputShard == 7)
                    {
                        line.text = Item.damage + " Force Energy damage (Ranged damage)";
                        line.overrideColor = new Color(195, 73, 13);
                    }
                    if (Item.GetGlobalItem<AAGlobalItemModular>().OutputShard == 8)
                    {
                        line.text = Item.damage + " Psychic Energy damage (Ranged damage)";
                        line.overrideColor = new Color(226, 18, 173);
                    }
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
        #endregion
    }
    public class MobianBusterProjectile : ModProjectile
    {
        public int counter = 0;
        public int chargeLevel = 0;
        public override string Texture => "AAMod/Content/Items/Weapons/Energy/MobianBuster2";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mobian Buster");
        }
        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 32;
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

            Main.EntitySpriteDraw(texture, new Vector2((int)position.X + (26 * Projectile.spriteDirection), (int)position.Y + (6 * Projectile.spriteDirection)).RotatedBy(Rot, player.MountedCenter - Main.screenPosition), sourceRectangle, Color.White, new Vector2(SpeedX, SpeedY).ToRotation() + MathHelper.ToRadians(90), origin, Projectile.scale, spriteEffects, 0);
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
                if ((counter - 90) % 3 == 0)
                {
                    player.HeldItem.GetGlobalItem<AAGlobalItemModular>().CurrentEnergy--;
                    player.HeldItem.GetGlobalItem<AAGlobalItemModular>().EnergyCooldown = true;
                }
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

            if (!player.channel || player.HeldItem.GetGlobalItem<AAGlobalItemModular>().CurrentEnergy <= 0 || player.HeldItem.type != ModContent.ItemType<MobianBuster>())
            {
                Projectile.Kill();
            }
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.owner == Main.myPlayer && player.HeldItem.GetGlobalItem<AAGlobalItemModular>().CurrentEnergy > 0 && player.HeldItem.type == ModContent.ItemType<MobianBuster>())
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
                        if (player.HeldItem.GetGlobalItem<AAGlobalItemModular>().CurrentEnergy >= 10)
                        {
                            SoundEngine.PlaySound(SoundID.Item, (int)Projectile.position.X, (int)Projectile.position.Y, 89);
                            player.HeldItem.GetGlobalItem<AAGlobalItemModular>().CurrentEnergy -= 10;
                            player.HeldItem.GetGlobalItem<AAGlobalItemModular>().EnergyCooldown = true;
                            Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(), vector2.X, vector2.Y, SpeedX, SpeedY, ModContent.ProjectileType<MobianBusterNormalShot>(), Projectile.damage, 1f, Projectile.owner);
                        }
                        break;
                    case 2:
                        if (player.HeldItem.GetGlobalItem<AAGlobalItemModular>().CurrentEnergy >= 30)
                        {
                            SoundEngine.PlaySound(SoundID.Item, (int)Projectile.position.X, (int)Projectile.position.Y, 88);
                            player.HeldItem.GetGlobalItem<AAGlobalItemModular>().CurrentEnergy -= 30;
                            player.HeldItem.GetGlobalItem<AAGlobalItemModular>().EnergyCooldown = true;
                            Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(), vector2.X, vector2.Y, SpeedX, SpeedY, ModContent.ProjectileType<MobianBusterChargeShot>(), Projectile.damage * 2, 1f, Projectile.owner);
                        }
                        break;
                    case 3:
                        if (player.HeldItem.GetGlobalItem<AAGlobalItemModular>().CurrentEnergy >= 30)
                        {
                            SoundEngine.PlaySound(SoundID.Item, (int)Projectile.position.X, (int)Projectile.position.Y, 88);
                            player.HeldItem.GetGlobalItem<AAGlobalItemModular>().CurrentEnergy -= 30;
                            player.HeldItem.GetGlobalItem<AAGlobalItemModular>().EnergyCooldown = true;
                            Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(), vector2.X, vector2.Y, SpeedX, SpeedY, ModContent.ProjectileType<MobianBusterChargeShot>(), Projectile.damage * 2, 1f, Projectile.owner);
                        }
                        break;
                }
            }
        }
    }
    class MobianBusterNormalShot : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mobian Buster Shot");
            Main.projFrames[Projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.DamageType = ModContent.GetInstance<OverallRangedEnergy>();
            Projectile.ignoreWater = true;
            Projectile.extraUpdates = 2;
        }
        public override void AI()
        {
            if (Main.rand.Next(2) == 0)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.GreenFairy,
                    Projectile.velocity.X, Projectile.velocity.Y, 200, Scale: 1f);
                dust.velocity += Projectile.velocity * 0.3f;
                dust.velocity *= 0.2f;
            }
            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 2)
                {
                    Projectile.frame = 0;
                }
            }
            Projectile.rotation = Projectile.velocity.ToRotation();
        }
        #region ShardEffects
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Main.player[Projectile.owner].HeldItem.GetGlobalItem<AAGlobalItemModular>().OutputShard != 0)
            {
                switch (Main.player[Projectile.owner].HeldItem.GetGlobalItem<AAGlobalItemModular>().OutputShard)
                {
                    case 1:
                        {
                            break;
                        }
                    case 2:
                        {
                            damage += damage / 2;
                            break;
                        }
                    case 3:
                        {
                            if (target.velocity.X <= 10f || target.velocity.Y <= 10f || target.velocity.X <= -10f || target.velocity.Y <= -10f)
                            {
                                damage += damage / 5;
                            }
                            else if (target.velocity.X <= 7f || target.velocity.Y <= 7f || target.velocity.X <= -7f || target.velocity.Y <= -7f)
                            {
                                damage += damage / 2;
                            }
                            else if (target.velocity.X <= 3f || target.velocity.Y <= 3f || target.velocity.X <= -3f || target.velocity.Y <= -3f)
                            {
                                damage += (damage / 5) * 4;
                            }
                            else
                            {
                                damage = (damage * 2) + (damage / 2);
                            }
                            break;
                        }
                    case 4:
                        {
                            if (target.velocity.X > 10f || target.velocity.Y > 10f || target.velocity.X > -10f || target.velocity.Y > -10f)
                            {
                                damage += damage / 5;
                            }
                            else if (target.velocity.X > 7f || target.velocity.Y > 7f || target.velocity.X > -7f || target.velocity.Y > -7f)
                            {
                                damage += damage / 2;
                            }
                            else if (target.velocity.X > 3f || target.velocity.Y > 3f || target.velocity.X > -3f || target.velocity.Y > -3f)
                            {
                                damage += (damage / 5) * 4;
                            }
                            else
                            {
                                damage = (damage * 2) + (damage / 2);
                            }
                            break;
                        }
                    case 5:
                        {
                            break;
                        }
                    case 6:
                        {
                            damage *= 2;
                            break;
                        }
                    case 7:
                        {
                            break;
                        }
                    case 8:
                        {
                            break;
                        }
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.player[Projectile.owner].HeldItem.GetGlobalItem<AAGlobalItemModular>().OutputShard != 0)
            {
                switch (Main.player[Projectile.owner].HeldItem.GetGlobalItem<AAGlobalItemModular>().OutputShard)
                {
                    case 1:
                        {
                            target.AddBuff(ModContent.BuffType<AcidicBurn>(), 90);
                            break;
                        }
                    case 2:
                        {
                            target.AddBuff(ModContent.BuffType<NecroticWound>(), 90);
                            break;
                        }
                    case 3:
                        {
                            break;
                        }
                    case 4:
                        {
                            break;
                        }
                    case 5:
                        {
                            target.AddBuff(ModContent.BuffType<SlowingIce>(), 180);
                            break;
                        }
                    case 6:
                        {
                            break;
                        }
                    case 7:
                        {
                            for (int i = 0; i < 16; i++)
                            {
                                Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(), Projectile.Center, Vector2.One.RotatedByRandom(MathHelper.ToRadians(180)), ModContent.ProjectileType<PowerShardExplosion>(), Projectile.damage / 2, 0f, Projectile.owner);
                            }
                            break;
                        }
                    case 8:
                        {
                            target.AddBuff(ModContent.BuffType<MindWarp>(), 270);
                            break;
                        }
                }
            }
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            if (Main.player[Projectile.owner].HeldItem.GetGlobalItem<AAGlobalItemModular>().OutputShard != 0)
            {
                switch (Main.player[Projectile.owner].HeldItem.GetGlobalItem<AAGlobalItemModular>().OutputShard)
                {
                    case 1:
                        {
                            break;
                        }
                    case 2:
                        {
                            damage += damage / 2;
                            break;
                        }
                    case 3:
                        {
                            if (target.velocity.X <= 10f || target.velocity.Y <= 10f || target.velocity.X <= -10f || target.velocity.Y <= -10f)
                            {
                                damage += damage / 5;
                            }
                            else if (target.velocity.X <= 7f || target.velocity.Y <= 7f || target.velocity.X <= -7f || target.velocity.Y <= -7f)
                            {
                                damage += damage / 2;
                            }
                            else if (target.velocity.X <= 3f || target.velocity.Y <= 3f || target.velocity.X <= -3f || target.velocity.Y <= -3f)
                            {
                                damage += (damage / 5) * 4;
                            }
                            else
                            {
                                damage = (damage * 2) + (damage / 2);
                            }
                            break;
                        }
                    case 4:
                        {
                            if (target.velocity.X > 10f || target.velocity.Y > 10f || target.velocity.X > -10f || target.velocity.Y > -10f)
                            {
                                damage += damage / 5;
                            }
                            else if (target.velocity.X > 7f || target.velocity.Y > 7f || target.velocity.X > -7f || target.velocity.Y > -7f)
                            {
                                damage += damage / 2;
                            }
                            else if (target.velocity.X > 3f || target.velocity.Y > 3f || target.velocity.X > -3f || target.velocity.Y > -3f)
                            {
                                damage += (damage / 5) * 4;
                            }
                            else
                            {
                                damage = (damage * 2) + (damage / 2);
                            }
                            break;
                        }
                    case 5:
                        {
                            break;
                        }
                    case 6:
                        {
                            damage *= 2;
                            break;
                        }
                    case 7:
                        {
                            break;
                        }
                    case 8:
                        {
                            break;
                        }
                }
            }
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (Main.player[Projectile.owner].HeldItem.GetGlobalItem<AAGlobalItemModular>().OutputShard != 0)
            {
                switch (Main.player[Projectile.owner].HeldItem.GetGlobalItem<AAGlobalItemModular>().OutputShard)
                {
                    case 1:
                        {
                            target.AddBuff(ModContent.BuffType<AcidicBurn>(), 90);
                            break;
                        }
                    case 2:
                        {
                            target.AddBuff(ModContent.BuffType<NecroticWound>(), 90);
                            break;
                        }
                    case 3:
                        {
                            break;
                        }
                    case 4:
                        {
                            break;
                        }
                    case 5:
                        {
                            target.AddBuff(ModContent.BuffType<SlowingIce>(), 180);
                            break;
                        }
                    case 6:
                        {
                            break;
                        }
                    case 7:
                        {
                            for (int i = 0; i < 16; i++)
                            {
                                Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(), Projectile.Center, Vector2.One.RotatedByRandom(MathHelper.ToRadians(180)), ModContent.ProjectileType<PowerShardExplosion>(), Projectile.damage / 2, 0f, Projectile.owner);
                            }
                            break;
                        }
                    case 8:
                        {
                            target.AddBuff(ModContent.BuffType<MindWarp>(), 270);
                            break;
                        }
                }
            }
        }
        #endregion
    }
    class MobianBusterChargeShot : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mobian Buster Blast");
            Main.projFrames[Projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.DamageType = ModContent.GetInstance<OverallRangedEnergy>();
            Projectile.ignoreWater = true;
            Projectile.extraUpdates = 2;
        }
        public override void AI()
        {
            if (Main.rand.Next(2) == 0)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.GreenFairy,
                    Projectile.velocity.X, Projectile.velocity.Y, 200, Scale: 1f);
                dust.velocity += Projectile.velocity * 0.3f;
                dust.velocity *= 0.2f;
            }
            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 2)
                {
                    Projectile.frame = 0;
                }
            }
            Projectile.rotation = Projectile.velocity.ToRotation();
        }
        #region ShardEffects
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Main.player[Projectile.owner].HeldItem.GetGlobalItem<AAGlobalItemModular>().OutputShard != 0)
            {
                switch (Main.player[Projectile.owner].HeldItem.GetGlobalItem<AAGlobalItemModular>().OutputShard)
                {
                    case 1:
                        {
                            break;
                        }
                    case 2:
                        {
                            damage += damage / 2;
                            break;
                        }
                    case 3:
                        {
                            if (target.velocity.X <= 10f || target.velocity.Y <= 10f || target.velocity.X <= -10f || target.velocity.Y <= -10f)
                            {
                                damage += damage / 5;
                            }
                            else if (target.velocity.X <= 7f || target.velocity.Y <= 7f || target.velocity.X <= -7f || target.velocity.Y <= -7f)
                            {
                                damage += damage / 2;
                            }
                            else if (target.velocity.X <= 3f || target.velocity.Y <= 3f || target.velocity.X <= -3f || target.velocity.Y <= -3f)
                            {
                                damage += (damage / 5) * 4;
                            }
                            else
                            {
                                damage = (damage * 2) + (damage / 2);
                            }
                            break;
                        }
                    case 4:
                        {
                            if (target.velocity.X > 10f || target.velocity.Y > 10f || target.velocity.X > -10f || target.velocity.Y > -10f)
                            {
                                damage += damage / 5;
                            }
                            else if (target.velocity.X > 7f || target.velocity.Y > 7f || target.velocity.X > -7f || target.velocity.Y > -7f)
                            {
                                damage += damage / 2;
                            }
                            else if (target.velocity.X > 3f || target.velocity.Y > 3f || target.velocity.X > -3f || target.velocity.Y > -3f)
                            {
                                damage += (damage / 5) * 4;
                            }
                            else
                            {
                                damage = (damage * 2) + (damage / 2);
                            }
                            break;
                        }
                    case 5:
                        {
                            break;
                        }
                    case 6:
                        {
                            damage *= 2;
                            break;
                        }
                    case 7:
                        {
                            break;
                        }
                    case 8:
                        {
                            break;
                        }
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.player[Projectile.owner].HeldItem.GetGlobalItem<AAGlobalItemModular>().OutputShard != 0)
            {
                switch (Main.player[Projectile.owner].HeldItem.GetGlobalItem<AAGlobalItemModular>().OutputShard)
                {
                    case 1:
                        {
                            target.AddBuff(ModContent.BuffType<AcidicBurn>(), 90);
                            break;
                        }
                    case 2:
                        {
                            target.AddBuff(ModContent.BuffType<NecroticWound>(), 90);
                            break;
                        }
                    case 3:
                        {
                            break;
                        }
                    case 4:
                        {
                            break;
                        }
                    case 5:
                        {
                            target.AddBuff(ModContent.BuffType<SlowingIce>(), 180);
                            break;
                        }
                    case 6:
                        {
                            break;
                        }
                    case 7:
                        {
                            for (int i = 0; i < 16; i++)
                            {
                                Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(), Projectile.Center, Vector2.One.RotatedByRandom(MathHelper.ToRadians(180)), ModContent.ProjectileType<PowerShardExplosion>(), Projectile.damage / 2, 0f, Projectile.owner);
                            }
                            break;
                        }
                    case 8:
                        {
                            target.AddBuff(ModContent.BuffType<MindWarp>(), 270);
                            break;
                        }
                }
            }
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            if (Main.player[Projectile.owner].HeldItem.GetGlobalItem<AAGlobalItemModular>().OutputShard != 0)
            {
                switch (Main.player[Projectile.owner].HeldItem.GetGlobalItem<AAGlobalItemModular>().OutputShard)
                {
                    case 1:
                        {
                            break;
                        }
                    case 2:
                        {
                            damage += damage / 2;
                            break;
                        }
                    case 3:
                        {
                            if (target.velocity.X <= 10f || target.velocity.Y <= 10f || target.velocity.X <= -10f || target.velocity.Y <= -10f)
                            {
                                damage += damage / 5;
                            }
                            else if (target.velocity.X <= 7f || target.velocity.Y <= 7f || target.velocity.X <= -7f || target.velocity.Y <= -7f)
                            {
                                damage += damage / 2;
                            }
                            else if (target.velocity.X <= 3f || target.velocity.Y <= 3f || target.velocity.X <= -3f || target.velocity.Y <= -3f)
                            {
                                damage += (damage / 5) * 4;
                            }
                            else
                            {
                                damage = (damage * 2) + (damage / 2);
                            }
                            break;
                        }
                    case 4:
                        {
                            if (target.velocity.X > 10f || target.velocity.Y > 10f || target.velocity.X > -10f || target.velocity.Y > -10f)
                            {
                                damage += damage / 5;
                            }
                            else if (target.velocity.X > 7f || target.velocity.Y > 7f || target.velocity.X > -7f || target.velocity.Y > -7f)
                            {
                                damage += damage / 2;
                            }
                            else if (target.velocity.X > 3f || target.velocity.Y > 3f || target.velocity.X > -3f || target.velocity.Y > -3f)
                            {
                                damage += (damage / 5) * 4;
                            }
                            else
                            {
                                damage = (damage * 2) + (damage / 2);
                            }
                            break;
                        }
                    case 5:
                        {
                            break;
                        }
                    case 6:
                        {
                            damage *= 2;
                            break;
                        }
                    case 7:
                        {
                            break;
                        }
                    case 8:
                        {
                            break;
                        }
                }
            }
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (Main.player[Projectile.owner].HeldItem.GetGlobalItem<AAGlobalItemModular>().OutputShard != 0)
            {
                switch (Main.player[Projectile.owner].HeldItem.GetGlobalItem<AAGlobalItemModular>().OutputShard)
                {
                    case 1:
                        {
                            target.AddBuff(ModContent.BuffType<AcidicBurn>(), 90);
                            break;
                        }
                    case 2:
                        {
                            target.AddBuff(ModContent.BuffType<NecroticWound>(), 90);
                            break;
                        }
                    case 3:
                        {
                            break;
                        }
                    case 4:
                        {
                            break;
                        }
                    case 5:
                        {
                            target.AddBuff(ModContent.BuffType<SlowingIce>(), 180);
                            break;
                        }
                    case 6:
                        {
                            break;
                        }
                    case 7:
                        {
                            for (int i = 0; i < 16; i++)
                            {
                                Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(), Projectile.Center, Vector2.One.RotatedByRandom(MathHelper.ToRadians(180)), ModContent.ProjectileType<PowerShardExplosion>(), Projectile.damage / 2, 0f, Projectile.owner);
                            }
                            break;
                        }
                    case 8:
                        {
                            target.AddBuff(ModContent.BuffType<MindWarp>(), 270);
                            break;
                        }
                }
            }
        }
        #endregion
    }
}