using AAMod.Common.Globals;
using AAMod.Common.Systems;
using AAMod.Common.Systems.UI;
using AAMod.Content.Buffs;
using AAMod.Content.EnergyDamageClass;
using AAMod.Content.Projectiles.Typeless;
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
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Weapons.Energy
{
    class LaserBaton : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Laser Baton");
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Arkhalis);
            Item.DamageType = ModContent.GetInstance<OverallMeleeEnergy>();
            Item.damage = 17;
            Item.shoot = ModContent.ProjectileType<LaserBatonProjectile>();
            Item.channel = true;
        }
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Asset<Texture2D> glowMask = ModContent.Request<Texture2D>("AAMod/Content/Items/Weapons/Energy/LaserBaton_Glow");
            switch (Item.GetGlobalItem<AAGlobalItemModular>().OutputShard)
            {
                case 1:
                    {
                        Color color = new(12, 240, 21);
                        spriteBatch.Draw(glowMask.Value, position, frame, color, 0f, origin, scale, SpriteEffects.None, 0f);
                        break;
                    }
                case 2:
                    {
                        Color color = new(30, 30, 30);
                        spriteBatch.Draw(glowMask.Value, position, frame, color, 0f, origin, scale, SpriteEffects.None, 0f);
                        break;
                    }
                case 3:
                    {
                        Color color = new(232, 219, 36);
                        spriteBatch.Draw(glowMask.Value, position, frame, color, 0f, origin, scale, SpriteEffects.None, 0f);
                        break;
                    }
                case 4:
                    {
                        Color color = new(195, 13, 13);
                        spriteBatch.Draw(glowMask.Value, position, frame, color, 0f, origin, scale, SpriteEffects.None, 0f);
                        break;
                    }
                case 5:
                    {
                        Color color = new(12, 186, 187);
                        spriteBatch.Draw(glowMask.Value, position, frame, color, 0f, origin, scale, SpriteEffects.None, 0f);
                        break;
                    }
                case 6:
                    {
                        Color color = new(234, 234, 234);
                        spriteBatch.Draw(glowMask.Value, position, frame, color, 0f, origin, scale, SpriteEffects.None, 0f);
                        break;
                    }
                case 7:
                    {
                        Color color = new(195, 73, 13);
                        spriteBatch.Draw(glowMask.Value, position, frame, color, 0f, origin, scale, SpriteEffects.None, 0f);
                        break;
                    }
                case 8:
                    {
                        Color color = new(226, 18, 173);
                        spriteBatch.Draw(glowMask.Value, position, frame, color, 0f, origin, scale, SpriteEffects.None, 0f);
                        break;
                    }
                default:
                    {
                        Color color = new(23, 13, 182);
                        spriteBatch.Draw(glowMask.Value, position, frame, color, 0f, origin, scale, SpriteEffects.None, 0f);
                        break;
                    }
            }
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Asset<Texture2D> glowMask = ModContent.Request<Texture2D>("AAMod/Content/Items/Weapons/Energy/LaserBaton_Glow");
            switch (Item.GetGlobalItem<AAGlobalItemModular>().OutputShard)
            {
                case 1:
                    {
                        Color color = new(12, 240, 21);
                        spriteBatch.Draw(glowMask.Value, new Vector2(Item.position.X - Main.screenPosition.X + Item.width * 0.5f, Item.position.Y - Main.screenPosition.Y + Item.height - glowMask.Height() * 0.5f + 2f), new Rectangle(0, 0, glowMask.Width(), glowMask.Height()), color, rotation, glowMask.Size() * 0.5f, scale, SpriteEffects.None, 0f);
                        break;
                    }
                case 2:
                    {
                        Color color = new(30, 30, 30);
                        spriteBatch.Draw(glowMask.Value, new Vector2(Item.position.X - Main.screenPosition.X + Item.width * 0.5f, Item.position.Y - Main.screenPosition.Y + Item.height - glowMask.Height() * 0.5f + 2f), new Rectangle(0, 0, glowMask.Width(), glowMask.Height()), color, rotation, glowMask.Size() * 0.5f, scale, SpriteEffects.None, 0f);
                        break;
                    }
                case 3:
                    {
                        Color color = new(232, 219, 36);
                        spriteBatch.Draw(glowMask.Value, new Vector2(Item.position.X - Main.screenPosition.X + Item.width * 0.5f, Item.position.Y - Main.screenPosition.Y + Item.height - glowMask.Height() * 0.5f + 2f), new Rectangle(0, 0, glowMask.Width(), glowMask.Height()), color, rotation, glowMask.Size() * 0.5f, scale, SpriteEffects.None, 0f);
                        break;
                    }
                case 4:
                    {
                        Color color = new(195, 13, 13);
                        spriteBatch.Draw(glowMask.Value, new Vector2(Item.position.X - Main.screenPosition.X + Item.width * 0.5f, Item.position.Y - Main.screenPosition.Y + Item.height - glowMask.Height() * 0.5f + 2f), new Rectangle(0, 0, glowMask.Width(), glowMask.Height()), color, rotation, glowMask.Size() * 0.5f, scale, SpriteEffects.None, 0f);
                        break;
                    }
                case 5:
                    {
                        Color color = new(12, 186, 187);
                        spriteBatch.Draw(glowMask.Value, new Vector2(Item.position.X - Main.screenPosition.X + Item.width * 0.5f, Item.position.Y - Main.screenPosition.Y + Item.height - glowMask.Height() * 0.5f + 2f), new Rectangle(0, 0, glowMask.Width(), glowMask.Height()), color, rotation, glowMask.Size() * 0.5f, scale, SpriteEffects.None, 0f);
                        break;
                    }
                case 6:
                    {
                        Color color = new(234, 234, 234);
                        spriteBatch.Draw(glowMask.Value, new Vector2(Item.position.X - Main.screenPosition.X + Item.width * 0.5f, Item.position.Y - Main.screenPosition.Y + Item.height - glowMask.Height() * 0.5f + 2f), new Rectangle(0, 0, glowMask.Width(), glowMask.Height()), color, rotation, glowMask.Size() * 0.5f, scale, SpriteEffects.None, 0f);
                        break;
                    }
                case 7:
                    {
                        Color color = new(195, 73, 13);
                        spriteBatch.Draw(glowMask.Value, new Vector2(Item.position.X - Main.screenPosition.X + Item.width * 0.5f, Item.position.Y - Main.screenPosition.Y + Item.height - glowMask.Height() * 0.5f + 2f), new Rectangle(0, 0, glowMask.Width(), glowMask.Height()), color, rotation, glowMask.Size() * 0.5f, scale, SpriteEffects.None, 0f);
                        break;
                    }
                case 8:
                    {
                        Color color = new(226, 18, 173);
                        spriteBatch.Draw(glowMask.Value, new Vector2(Item.position.X - Main.screenPosition.X + Item.width * 0.5f, Item.position.Y - Main.screenPosition.Y + Item.height - glowMask.Height() * 0.5f + 2f), new Rectangle(0, 0, glowMask.Width(), glowMask.Height()), color, rotation, glowMask.Size() * 0.5f, scale, SpriteEffects.None, 0f);
                        break;
                    }
                default:
                    {
                        Color color = new(23, 13, 182);
                        spriteBatch.Draw(glowMask.Value, new Vector2(Item.position.X - Main.screenPosition.X + Item.width * 0.5f, Item.position.Y - Main.screenPosition.Y + Item.height - glowMask.Height() * 0.5f + 2f), new Rectangle(0, 0, glowMask.Width(), glowMask.Height()), color, rotation, glowMask.Size() * 0.5f, scale, SpriteEffects.None, 0f);
                        break;
                    }
            }
        }
        #region RequiredForEnergyWeapon
        public override bool CanShoot(Player player)
        {
            return !Item.GetGlobalItem<AAGlobalItemModular>().CheatedIn && Item.GetGlobalItem<AAGlobalItemModular>().CurrentEnergy > 0;
        }
        public override void UpdateInventory(Player player)
        {
            Item.damage = Item.GetGlobalItem<AAGlobalItemModular>().OutputScrap != -1 ? (Item.GetGlobalItem<AAGlobalItemModular>().OutputScrap * 3) + 17 : 0;
            if (Item.GetGlobalItem<AAGlobalItemModular>().MeleeNERF >= 2)
            {
                Item.damage -= ((6 * Item.GetGlobalItem<AAGlobalItemModular>().MeleeNERF) - 6);
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
                        line.text = Item.damage + " Acid Energy damage (Melee damage)";
                        line.overrideColor = new Color(12, 240, 21);
                    }
                    if (Item.GetGlobalItem<AAGlobalItemModular>().OutputShard == 2)
                    {
                        line.text = Item.damage + " Necrotic Energy damage (Melee damage)";
                        line.overrideColor = new Color(30, 30, 30);
                    }
                    if (Item.GetGlobalItem<AAGlobalItemModular>().OutputShard == 3)
                    {
                        line.text = Item.damage + " Electric Energy damage (Melee damage)";
                        line.overrideColor = new Color(232, 219, 36);
                    }
                    if (Item.GetGlobalItem<AAGlobalItemModular>().OutputShard == 4)
                    {
                        line.text = Item.damage + " Fire Energy damage (Melee damage)";
                        line.overrideColor = new Color(195, 13, 13);
                    }
                    if (Item.GetGlobalItem<AAGlobalItemModular>().OutputShard == 5)
                    {
                        line.text = Item.damage + " Ice Energy damage (Melee damage)";
                        line.overrideColor = new Color(12, 186, 187);
                    }
                    if (Item.GetGlobalItem<AAGlobalItemModular>().OutputShard == 6)
                    {
                        line.text = Item.damage + " Radiant Energy damage (Melee damage)";
                        line.overrideColor = new Color(234, 234, 234);
                    }
                    if (Item.GetGlobalItem<AAGlobalItemModular>().OutputShard == 7)
                    {
                        line.text = Item.damage + " Force Energy damage (Melee damage)";
                        line.overrideColor = new Color(195, 73, 13);
                    }
                    if (Item.GetGlobalItem<AAGlobalItemModular>().OutputShard == 8)
                    {
                        line.text = Item.damage + " Psychic Energy damage (Melee damage)";
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
    class LaserBatonProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Laser Baton");
            Main.projFrames[Projectile.type] = 28;
        }
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Arkhalis);
            Projectile.DamageType = ModContent.GetInstance<OverallMeleeEnergy>();
            Projectile.aiStyle = ProjectileID.Arkhalis;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            Color color = new();
            switch (Main.player[Projectile.owner].HeldItem.GetGlobalItem<AAGlobalItemModular>().OutputShard)
            {
                case 1:
                    {
                        color = new(12, 240, 21, 0);
                        break;
                    }
                case 2:
                    {
                        color = new(30, 30, 30, 0);
                        break;
                    }
                case 3:
                    {
                        color = new(232, 219, 36, 0);
                        break;
                    }
                case 4:
                    {
                        color = new(195, 13, 13, 0);
                        break;
                    }
                case 5:
                    {
                        color = new(12, 186, 187, 0);
                        break;
                    }
                case 6:
                    {
                        color = new(234, 234, 234, 0);
                        break;
                    }
                case 7:
                    {
                        color = new(195, 73, 13, 0);
                        break;
                    }
                case 8:
                    {
                        color = new(226, 18, 173, 0);
                        break;
                    }
                default:
                    {
                        color = new(23, 13, 182, 0);
                        break;
                    }
            }
            return color * Projectile.Opacity;
        }
        private int counter = 0;
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            player.heldProj = Projectile.type;
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

            Projectile.rotation = new Vector2(SpeedX, SpeedY).ToRotation() + (Projectile.spriteDirection == 1 ? MathHelper.ToRadians(0f) : MathHelper.ToRadians(180f));
            Projectile.spriteDirection = player.direction;
            Projectile.Center = player.RotatedRelativePoint(player.MountedCenter);
            Projectile.velocity = Vector2.Zero;
            player.itemAnimation = ItemUseStyleID.Shoot;
            Projectile.frame++;
            if (Projectile.frame == 28)
            {
                Projectile.Kill();
            }
            counter++;
            if (counter % 10 == 0)
            {
                SoundEngine.PlaySound(SoundID.Item1);
                player.HeldItem.GetGlobalItem<AAGlobalItemModular>().CurrentEnergy--;
                player.HeldItem.GetGlobalItem<AAGlobalItemModular>().EnergyCooldown = true;
                counter = 0;
            }
            if (!player.channel)
            {
                Projectile.Kill();
            }
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
            Main.player[Projectile.owner].HeldItem.GetGlobalItem<AAGlobalItemModular>().CurrentEnergy -= damage / 4;
            Main.player[Projectile.owner].HeldItem.GetGlobalItem<AAGlobalItemModular>().EnergyCooldown = true;
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
            Main.player[Projectile.owner].HeldItem.GetGlobalItem<AAGlobalItemModular>().CurrentEnergy -= damage / 2;
            Main.player[Projectile.owner].HeldItem.GetGlobalItem<AAGlobalItemModular>().EnergyCooldown = true;
        }
        #endregion
    }
}
