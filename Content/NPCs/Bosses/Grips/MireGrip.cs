using AAMod.Content.Buffs;
using AAMod.Content.Projectiles.Bosses;
using AAMod.Content.Items.TreasureBags;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.Content.Items.Vanity.BossMasks;
using Terraria.GameContent.ItemDropRules;
using Terraria.Audio;
using AAMod.Content.Biomes.Mire;
using AAMod.Content.Items.Weapons.Melee;
using AAMod.Content.Items.Weapons.Ranged;
using AAMod.Content.Items.Weapons.Magic;
using AAMod.Content.Items.Placeables.Trophies;

namespace AAMod.Content.NPCs.Bosses.Grips
{
    [AutoloadBossHead]
    class MireGrip : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mire Grip");
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
            {
                SpecificallyImmuneTo = new int[]
                {
                    BuffID.Poisoned,
                    BuffID.Confused
				}
            };
            NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);
            Main.npcFrameCount[Type] = 7;
        }
        public override void SetDefaults()
        {
            NPC.width = 144;
            NPC.height = 106;
            NPC.damage = 8;
            NPC.defense = 7;
            NPC.lifeMax = 1100;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.value = Item.buyPrice(gold: 5);
            NPC.SpawnWithHigherTime(30);
            NPC.boss = true;
            NPC.npcSlots = 5f;
            BossBag = ModContent.ItemType<GripsTreasureBag>();
            NPC.aiStyle = -1;
            if (!Main.dedServ)
            {
                Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Grips");
            }
            SpawnModBiomes = new int[1] { ModContent.GetInstance<MireSurfaceBiome>().Type };
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                new FlavorTextBestiaryInfoElement("Massive clawed abomination that stalks the Mire at night. With razor sharp fingertips and aquamancy at its disposal, it tears apart large threats without mercy. Drawn out of its home to follow a powerful scent, It arrives to destroy any and all opponents.")
            });
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            if (NPC.CountNPCS(ModContent.NPCType<InfernoGrip>()) == 0)
            {
                npcLoot.Add(ItemDropRule.BossBag(BossBag));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MireGripTrophy>(), 10));

                LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());

                notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<MireGripMask>(), 7));

                notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1, ModContent.ItemType<TwinTalons>(), ModContent.ItemType<VnV>(), ModContent.ItemType<ClawCast>()));

                npcLoot.Add(notExpertRule);
            }
        }
        public bool Initialize
        {
            get => NPC.ai[0] == 0f;
            set => NPC.ai[0] = value ? 0f : 1f;
        }
        private int InfernoHand = -1;
        private int DeltaTime = 0;
        private float AttackTimer = 0f;
        private bool CorrectSide = true;
        private int harmfulCooldown = 0;
        private bool Grappled = false;
        public ref float HP => ref NPC.ai[1];
        public ref float AttackVar => ref NPC.ai[2];
        public ref float SpawningHP => ref NPC.ai[3];
        public override void FindFrame(int frameHeight)
        {
            Player player = Main.player[NPC.target];
            if (player.dead && NPC.CountNPCS(ModContent.NPCType<InfernoGrip>()) == 0)
            {
                NPC.frame.Y = 4 * frameHeight;
            }
            else if (AttackVar == 2f)
            {
                NPC.frame.Y = 2 * frameHeight;
            }
            else if (AttackVar == 3f || (player.dead && NPC.CountNPCS(ModContent.NPCType<InfernoGrip>()) == 1))
            {
                NPC.frame.Y = 1 * frameHeight;
            }
            else if (AttackVar == 4f && AttackTimer < 170 && AttackTimer >= 30 && Main.expertMode)
            {
                NPC.frame.Y = 6 * frameHeight;
            }
            else if (AttackVar == 4f && ((AttackTimer >= 170 || AttackTimer < 30) || !Main.expertMode))
            {
                NPC.frame.Y = 3 * frameHeight;
            }
            else if (player.HasBuff(ModContent.BuffType<Struggle>()) || player.HasBuff(ModContent.BuffType<StruggleAlt>()))
            {
                NPC.frame.Y = 5 * frameHeight;
            }
            else
            {
                NPC.frame.Y = 0 * frameHeight;
            }
        }
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>(NPC.ModNPC.Texture + "_Glow").Value;
            SpriteEffects effects = NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Vector2 origin = NPC.frame.Size() / 2f + new Vector2(0f, NPC.ModNPC.DrawOffsetY);
            Vector2 position = NPC.Center - screenPos + new Vector2(0f, NPC.gfxOffY);
            Main.EntitySpriteDraw(texture, position, NPC.frame, Color.White, NPC.rotation, origin, NPC.scale, effects, 0);
        }
        public override void AI()
        {
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest();
            }
            Player player = Main.player[NPC.target];
            if (NPC.CountNPCS(ModContent.NPCType<InfernoGrip>()) > 1 || NPC.CountNPCS(ModContent.NPCType<MireGrip>()) > 1 || NPC.CountNPCS(ModContent.NPCType<OffGuardClap>()) > 1 || NPC.CountNPCS(ModContent.NPCType<FistBump>()) > 1)
            {
                NPC.active = false;
                NPC.life = 0;
                return;
            }
            if (NPC.CountNPCS(ModContent.NPCType<InfernoGrip>()) == 1 && InfernoHand == -1)
            {
                InfernoHand = NPC.FindFirstNPC(ModContent.NPCType<InfernoGrip>());
            }
            if (!Initialize && NPC.CountNPCS(ModContent.NPCType<InfernoGrip>()) == 0 && InfernoHand != -1)
            {
                InfernoHand = -1;
            }
            if (Main.dayTime && Grappled)
            {
                NPC.velocity.Y += 0.12f;
                NPC.EncourageDespawn(10);
                return;
            }
            if (Main.dayTime && !Grappled)
            {
                NPC.velocity.X = 0f;
                NPC.velocity.Y -= 0.12f;
                NPC.EncourageDespawn(10);
                return;
            }
            if (Initialize)
            {
                if (InfernoHand != -1)
                {
                    NPC.velocity.X -= 0.12f;
                    if (NPC.Hitbox.Intersects(Main.npc[InfernoHand].Hitbox))
                    {
                        NPC.NewNPC((int)NPC.position.X, (int)NPC.Center.Y + (NPC.height / 2), ModContent.NPCType<OffGuardClap>(), 0, NPC.life, Main.npc[InfernoHand].life);
                        Main.npc[InfernoHand].active = false;
                        Main.npc[InfernoHand].life = 0;
                        NPC.active = false;
                        NPC.life = 0;
                        return;
                    }
                }
                else
                {
                    InfernoHand = NPC.NewNPC((int)NPC.Center.X - 2080, (int)NPC.Center.Y + (NPC.height / 2), ModContent.NPCType<InfernoGrip>(), NPC.whoAmI);
                    return;
                }
            }
            if (!Initialize && player.dead && NPC.CountNPCS(ModContent.NPCType<InfernoGrip>()) == 0)
            {
                NPC.velocity.X = 0f;
                if (!NPC.noTileCollide)
                {
                    NPC.noTileCollide = true;
                }
                NPC.velocity.Y += 0.12f;
                NPC.EncourageDespawn(10);
                return;
            }
            else if (!Initialize && player.dead && NPC.CountNPCS(ModContent.NPCType<InfernoGrip>()) == 1)
            {
                NPC.velocity.X = ((Main.npc[InfernoHand].Center.X / 2f) - (NPC.Center.X / 2f)) / 12f;
                NPC.velocity.Y = ((Main.npc[InfernoHand].Center.Y / 2f) - (NPC.Center.Y / 2f)) / 12f;
                if (NPC.Hitbox.Intersects(Main.npc[InfernoHand].Hitbox))
                {
                    NPC.NewNPC((int)((Main.npc[InfernoHand].Center.X + NPC.Center.X) / 2), (int)((Main.npc[InfernoHand].Center.Y + NPC.Center.Y) / 2) + (NPC.height / 2), ModContent.NPCType<FistBump>());
                    Main.npc[InfernoHand].active = false;
                    Main.npc[InfernoHand].life = 0;
                    NPC.active = false;
                    NPC.life = 0;
                    return;
                }
            }
            if (!Initialize && !player.dead)
            {
                if (AttackVar == 1f)
                {
                    NPC.life = ((int)HP >= 1) ? (int)HP : 1;
                    InfernoHand = NPC.NewNPC((int)NPC.Center.X, (int)NPC.Center.Y + (NPC.height / 2), ModContent.NPCType<InfernoGrip>(), NPC.whoAmI, 1f, SpawningHP, 1f, 1f);
                    AttackVar = 6f;
                    harmfulCooldown = 10;
                    NPC.netUpdate = true;
                    return;
                }
                if (AttackVar == 0f)
                {
                    NPC.life = ((int)HP >= 1) ? (int)HP : 1;
                    InfernoHand = NPC.NewNPC((int)NPC.Center.X, (int)NPC.Center.Y + (NPC.height / 2), ModContent.NPCType<InfernoGrip>(), NPC.whoAmI, 1f, SpawningHP, 0f, 1f);
                    harmfulCooldown = 10;
                    AttackVar = 6f;
                    NPC.netUpdate = true;
                    return;
                }
                if (harmfulCooldown > 0)
                {
                    harmfulCooldown--;
                }
                if (AttackVar == 6f)
                {
                    DeltaTime++;
                    if (DeltaTime == 1900)
                    {
                        DeltaTime = 0;
                    }
                    NPC.velocity.X = (((player.Center.X / 2f) - (NPC.Center.X / 2f)) + MathF.Cos(DeltaTime / 60f) * 210) / 5f;
                    NPC.velocity.Y = (((player.Center.Y / 2f) - (NPC.Center.Y / 2f)) + MathF.Sin(DeltaTime / 60f) * 120) / 5f;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        if (player.Center.X >= NPC.Center.X)
                        {
                            if (CorrectSide && InfernoHand != -1)
                            {
                                Main.npc[InfernoHand].ai[0] = 2f;
                            }
                            CorrectSide = false;
                            NPC.spriteDirection = 1;
                        }
                        else
                        {
                            if (!CorrectSide && InfernoHand != -1)
                            {
                                Main.npc[InfernoHand].ai[0] = 2;
                            }
                            CorrectSide = true;
                            NPC.spriteDirection = -1;
                        }
                    }
                    AttackTimer++;
                    if (Main.netMode != NetmodeID.MultiplayerClient && Main.expertMode ? AttackTimer >= 300f : AttackTimer >= 450f)
                    {
                        if (Main.expertMode)
                        {
                            if (InfernoHand != -1)
                            {
                                Main.npc[InfernoHand].ai[3] = Main.rand.Next(10);
                                if (Main.npc[InfernoHand].ai[3] != 0f)
                                {
                                    AttackVar = Main.rand.Next(2, 6);
                                }
                                else
                                {
                                    AttackVar = Main.rand.Next(2, 5);
                                }
                            }
                            else
                            {
                                AttackVar = Main.rand.Next(2, 5);
                            }
                        }
                        else
                        {
                            AttackVar = Main.rand.Next(2, 5);
                        }
                        AttackTimer = 0f;
                        NPC.netUpdate = true;
                    }
                }
                if (AttackVar == 2f)
                {
                    if (Main.expertMode)
                    {
                        float speed = 10f;
                        AttackTimer++;
                        if (AttackTimer == 70f)
                        {
                            NPC.rotation -= CorrectSide ? 0.3f : -0.3f;
                            speed = 12f;
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), new Vector2(NPC.position.X + (CorrectSide ? 12 : NPC.width - 12), NPC.position.Y + 58), (new Vector2((player.Center.X / 2f) - (NPC.Center.X / 2f), (player.Center.Y / 2f) - (NPC.Center.Y / 2f)) / 30f), ModContent.ProjectileType<Wrathbolt>(), 12 / 3, 0f);
                                SoundEngine.PlaySound(SoundID.Item21);
                            }
                        }
                        if (AttackTimer == 71f)
                        {
                            speed = 14f;
                            NPC.rotation -= CorrectSide ? 0.3f : -0.3f;
                        }
                        if (AttackTimer == 72f)
                        {
                            speed = 16f;
                            NPC.rotation -= CorrectSide ? 0.3f : -0.3f;
                        }
                        if (AttackTimer == 73f)
                        {
                            speed = 14f;
                            NPC.rotation += CorrectSide ? 0.3f : -0.3f;
                        }
                        if (AttackTimer == 74f)
                        {
                            speed = 12f;
                            NPC.rotation += CorrectSide ? 0.3f : -0.3f;
                        }
                        if (AttackTimer == 75f)
                        {
                            speed = 10f;
                            NPC.rotation += CorrectSide ? 0.3f : -0.3f;
                        }
                        if (AttackTimer == 140f)
                        {
                            NPC.rotation -= CorrectSide ? 0.3f : -0.3f;
                            speed = 12f;
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), new Vector2(NPC.position.X + (CorrectSide ? 12 : NPC.width - 12), NPC.position.Y + 58), (new Vector2((player.Center.X / 2f) - (NPC.Center.X / 2f), (player.Center.Y / 2f) - (NPC.Center.Y / 2f)) / 30f), ModContent.ProjectileType<Wrathbolt>(), 12 / 3, 0f);
                                SoundEngine.PlaySound(SoundID.Item21);
                            }
                        }
                        if (AttackTimer == 141f)
                        {
                            speed = 14f;
                            NPC.rotation -= CorrectSide ? 0.3f : -0.3f;
                        }
                        if (AttackTimer == 142f)
                        {
                            speed = 16f;
                            NPC.rotation -= CorrectSide ? 0.3f : -0.3f;
                        }
                        if (AttackTimer == 143f)
                        {
                            speed = 14f;
                            NPC.rotation += CorrectSide ? 0.3f : -0.3f;
                        }
                        if (AttackTimer == 144f)
                        {
                            speed = 12f;
                            NPC.rotation += CorrectSide ? 0.3f : -0.3f;
                        }
                        if (AttackTimer == 145f)
                        {
                            speed = 10f;
                            NPC.rotation += CorrectSide ? 0.3f : -0.3f;
                        }
                        if (AttackTimer == 210f)
                        {
                            NPC.rotation -= CorrectSide ? 0.3f : -0.3f;
                            speed = 12f;
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), new Vector2(NPC.position.X + (CorrectSide ? 12 : NPC.width - 12), NPC.position.Y + 58), (new Vector2((player.Center.X / 2f) - (NPC.Center.X / 2f), (player.Center.Y / 2f) - (NPC.Center.Y / 2f)) / 30f), ModContent.ProjectileType<Wrathbolt>(), 12 / 3, 0f);
                                SoundEngine.PlaySound(SoundID.Item21);
                            }
                        }
                        if (AttackTimer == 211f)
                        {
                            speed = 14f;
                            NPC.rotation -= CorrectSide ? 0.3f : -0.3f;
                        }
                        if (AttackTimer == 212f)
                        {
                            speed = 16f;
                            NPC.rotation -= CorrectSide ? 0.3f : -0.3f;
                        }
                        if (AttackTimer == 213f)
                        {
                            speed = 14f;
                            NPC.rotation += CorrectSide ? 0.3f : -0.3f;
                        }
                        if (AttackTimer == 214f)
                        {
                            speed = 12f;
                            NPC.rotation += CorrectSide ? 0.3f : -0.3f;
                        }
                        if (AttackTimer == 215f)
                        {
                            speed = 10f;
                            NPC.rotation += CorrectSide ? 0.3f : -0.3f;
                        }
                        if (AttackTimer == 250f)
                        {
                            AttackVar = 6f;
                            if (InfernoHand != -1)
                            {
                                Main.npc[InfernoHand].ai[3] = 1f;
                            }
                            AttackTimer = 0f;
                            NPC.netUpdate = true;
                            return;
                        }
                        NPC.velocity.X = (((player.Center.X / 2f) - (NPC.Center.X / 2f)) + (CorrectSide ? 240 : -240)) / speed;
                        NPC.velocity.Y = (((player.Center.Y / 2f) - (NPC.Center.Y / 2f)) - 30) / speed;
                    }
                    else
                    {
                        float speed = 10f;
                        AttackTimer++;
                        if (AttackTimer == 70f)
                        {
                            NPC.rotation -= CorrectSide ? 0.3f : -0.3f;
                            speed = 12f;
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), new Vector2(NPC.position.X + (CorrectSide ? 12 : NPC.width - 12), NPC.position.Y + 58), (new Vector2((player.Center.X / 2f) - (NPC.Center.X / 2f), (player.Center.Y / 2f) - (NPC.Center.Y / 2f)) / 40f), ModContent.ProjectileType<Wrathbolt>(), 12 / 3, 0f);
                                SoundEngine.PlaySound(SoundID.Item21);
                            }
                        }
                        if (AttackTimer == 71f)
                        {
                            speed = 14f;
                            NPC.rotation -= CorrectSide ? 0.3f : -0.3f;
                        }
                        if (AttackTimer == 72f)
                        {
                            speed = 16f;
                            NPC.rotation -= CorrectSide ? 0.3f : -0.3f;
                        }
                        if (AttackTimer == 73f)
                        {
                            speed = 14f;
                            NPC.rotation += CorrectSide ? 0.3f : -0.3f;
                        }
                        if (AttackTimer == 74f)
                        {
                            speed = 12f;
                            NPC.rotation += CorrectSide ? 0.3f : -0.3f;
                        }
                        if (AttackTimer == 75f)
                        {
                            speed = 10f;
                            NPC.rotation += CorrectSide ? 0.3f : -0.3f;
                        }
                        if (AttackTimer == 140f)
                        {
                            NPC.rotation -= CorrectSide ? 0.3f : -0.3f;
                            speed = 12f;
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), new Vector2(NPC.position.X + (CorrectSide ? 12 : NPC.width - 12), NPC.position.Y + 58), (new Vector2((player.Center.X / 2f) - (NPC.Center.X / 2f), (player.Center.Y / 2f) - (NPC.Center.Y / 2f)) / 40f), ModContent.ProjectileType<Wrathbolt>(), 12 / 3, 0f);
                                SoundEngine.PlaySound(SoundID.Item21);
                            }
                        }
                        if (AttackTimer == 141f)
                        {
                            speed = 14f;
                            NPC.rotation -= CorrectSide ? 0.3f : -0.3f;
                        }
                        if (AttackTimer == 142f)
                        {
                            speed = 16f;
                            NPC.rotation -= CorrectSide ? 0.3f : -0.3f;
                        }
                        if (AttackTimer == 143f)
                        {
                            speed = 14f;
                            NPC.rotation += CorrectSide ? 0.3f : -0.3f;
                        }
                        if (AttackTimer == 144f)
                        {
                            speed = 12f;
                            NPC.rotation += CorrectSide ? 0.3f : -0.3f;
                        }
                        if (AttackTimer == 145f)
                        {
                            speed = 10f;
                            NPC.rotation += CorrectSide ? 0.3f : -0.3f;
                        }
                        if (AttackTimer == 210f)
                        {
                            NPC.rotation -= CorrectSide ? 0.3f : -0.3f;
                            speed = 12f;
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), new Vector2(NPC.position.X + (CorrectSide ? 12 : NPC.width - 12), NPC.position.Y + 58), (new Vector2((player.Center.X / 2f) - (NPC.Center.X / 2f), (player.Center.Y / 2f) - (NPC.Center.Y / 2f)) / 40f), ModContent.ProjectileType<Wrathbolt>(), 12 / 3, 0f);
                                SoundEngine.PlaySound(SoundID.Item21);
                            }
                        }
                        if (AttackTimer == 211f)
                        {
                            speed = 14f;
                            NPC.rotation -= CorrectSide ? 0.3f : -0.3f;
                        }
                        if (AttackTimer == 212f)
                        {
                            speed = 16f;
                            NPC.rotation -= CorrectSide ? 0.3f : -0.3f;
                        }
                        if (AttackTimer == 213f)
                        {
                            speed = 14f;
                            NPC.rotation += CorrectSide ? 0.3f : -0.3f;
                        }
                        if (AttackTimer == 214f)
                        {
                            speed = 12f;
                            NPC.rotation += CorrectSide ? 0.3f : -0.3f;
                        }
                        if (AttackTimer == 215f)
                        {
                            speed = 10f;
                            NPC.rotation += CorrectSide ? 0.3f : -0.3f;
                        }
                        if (AttackTimer == 250f)
                        {
                            AttackVar = 6f;
                            if (InfernoHand != -1)
                            {
                                Main.npc[InfernoHand].ai[3] = 1f;
                            }
                            AttackTimer = 0f;
                            NPC.netUpdate = true;
                            return;
                        }
                        NPC.velocity.X = (((player.Center.X / 2f) - (NPC.Center.X / 2f)) + (CorrectSide ? 240 : -240)) / speed;
                        NPC.velocity.Y = (((player.Center.Y / 2f) - (NPC.Center.Y / 2f)) - 30) / speed;
                    }
                }
                if (AttackVar == 3f)
                {
                    float speed = 20f;
                    AttackTimer++;
                    if (AttackTimer < 80f)
                    {
                        NPC.velocity.X = (((player.Center.X / 2f) - (NPC.Center.X / 2f)) + (CorrectSide ? 280 : -280)) / speed;
                        NPC.velocity.Y = (((player.Center.Y / 2f) - (NPC.Center.Y / 2f)) + 20) / speed;
                    }
                    if (AttackTimer >= 80f && AttackTimer < 160f)
                    {
                        NPC.velocity.X = (((player.Center.X / 2f) - (NPC.Center.X / 2f)) - (CorrectSide ? 280 : -280)) / speed;
                        NPC.velocity.Y = (((player.Center.Y / 2f) - (NPC.Center.Y / 2f)) + 20) / speed;
                    }
                    if (Main.netMode != NetmodeID.MultiplayerClient && AttackTimer == 160f)
                    {
                        NPC.spriteDirection = (CorrectSide ? 1 : -1);
                    }
                    if (AttackTimer >= 160f && AttackTimer < 240f)
                    {
                        NPC.velocity.X = (((player.Center.X / 2f) - (NPC.Center.X / 2f)) + (CorrectSide ? 280 : -280)) / speed;
                        NPC.velocity.Y = (((player.Center.Y / 2f) - (NPC.Center.Y / 2f)) + 20) / speed;
                    }
                    if (Main.netMode != NetmodeID.MultiplayerClient && AttackTimer == 240f)
                    {
                        NPC.spriteDirection = (CorrectSide ? -1 : 1);
                    }
                    if (AttackTimer >= 240f && AttackTimer < 250f)
                    {
                        NPC.velocity.X = (((player.Center.X / 2f) - (NPC.Center.X / 2f)) + (CorrectSide ? 280 : -280)) / speed;
                        NPC.velocity.Y = (((player.Center.Y / 2f) - (NPC.Center.Y / 2f)) + 20) / speed;
                    }
                    if (AttackTimer == 250f)
                    {
                        AttackVar = 6f;
                        if (InfernoHand != -1)
                        {
                            Main.npc[InfernoHand].ai[3] = 1f;
                        }
                        AttackTimer = 0f;
                        NPC.netUpdate = true;
                        return;
                    }
                }
                if (AttackVar == 4f)
                {
                    if (Main.expertMode)
                    {
                        float speed = 20f;
                        AttackTimer++;
                        if (AttackTimer >= 0f && AttackTimer < 170f)
                        {
                            NPC.velocity.X = (((player.Center.X / 2f) - (NPC.Center.X / 2f)) + (CorrectSide ? 10 : -10)) / speed;
                            NPC.velocity.Y = (((player.Center.Y / 2f) - (NPC.Center.Y / 2f)) - 160) / speed;
                        }
                        if (AttackTimer == 70)
                        {
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                int fingerChoice = Main.rand.Next(4);
                                if (fingerChoice == 0)
                                {
                                    Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), new Vector2(NPC.position.X + (CorrectSide ? 12 : NPC.width - 12), NPC.position.Y + (NPC.height - 20)), new Vector2(0, 6f), ModContent.ProjectileType<Wrathbolt>(), 12 / 3, 0f);
                                    SoundEngine.PlaySound(SoundID.Item21);
                                }
                                else if (fingerChoice == 1)
                                {
                                    Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), new Vector2(NPC.position.X + (CorrectSide ? 32 : NPC.width - 32), NPC.position.Y + (NPC.height - 20)), new Vector2(0, 6f), ModContent.ProjectileType<Wrathbolt>(), 12 / 3, 0f);
                                    SoundEngine.PlaySound(SoundID.Item21);
                                }
                                else if (fingerChoice == 2)
                                {
                                    Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), new Vector2(NPC.position.X + (CorrectSide ? 42 : NPC.width - 42), NPC.position.Y + (NPC.height - 20)), new Vector2(0, 6f), ModContent.ProjectileType<Wrathbolt>(), 12 / 3, 0f);
                                    SoundEngine.PlaySound(SoundID.Item21);
                                }
                                else
                                {
                                    Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), new Vector2(NPC.position.X + (CorrectSide ? 62 : NPC.width - 62), NPC.position.Y + (NPC.height - 20)), new Vector2(0, 6f), ModContent.ProjectileType<Wrathbolt>(), 12 / 3, 0f);
                                    SoundEngine.PlaySound(SoundID.Item21);
                                }
                            }
                        }
                        if (AttackTimer == 105)
                        {
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                int fingerChoice = Main.rand.Next(4);
                                if (fingerChoice == 0)
                                {
                                    Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), new Vector2(NPC.position.X + (CorrectSide ? 12 : NPC.width - 12), NPC.position.Y + (NPC.height - 20)), new Vector2(0, 6f), ModContent.ProjectileType<Wrathbolt>(), 12 / 3, 0f);
                                    SoundEngine.PlaySound(SoundID.Item21);
                                }
                                else if (fingerChoice == 1)
                                {
                                    Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), new Vector2(NPC.position.X + (CorrectSide ? 32 : NPC.width - 32), NPC.position.Y + (NPC.height - 20)), new Vector2(0, 6f), ModContent.ProjectileType<Wrathbolt>(), 12 / 3, 0f);
                                    SoundEngine.PlaySound(SoundID.Item21);
                                }
                                else if (fingerChoice == 2)
                                {
                                    Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), new Vector2(NPC.position.X + (CorrectSide ? 42 : NPC.width - 42), NPC.position.Y + (NPC.height - 20)), new Vector2(0, 6f), ModContent.ProjectileType<Wrathbolt>(), 12 / 3, 0f);
                                    SoundEngine.PlaySound(SoundID.Item21);
                                }
                                else
                                {
                                    Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), new Vector2(NPC.position.X + (CorrectSide ? 62 : NPC.width - 62), NPC.position.Y + (NPC.height - 20)), new Vector2(0, 6f), ModContent.ProjectileType<Wrathbolt>(), 12, 0f);
                                    SoundEngine.PlaySound(SoundID.Item21);
                                }
                            }
                        }
                        if (AttackTimer == 140)
                        {
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                int fingerChoice = Main.rand.Next(4);
                                if (fingerChoice == 0)
                                {
                                    Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), new Vector2(NPC.position.X + (CorrectSide ? 12 : NPC.width - 12), NPC.position.Y + (NPC.height - 20)), new Vector2(0, 6f), ModContent.ProjectileType<Wrathbolt>(), 12 / 3, 0f);
                                    SoundEngine.PlaySound(SoundID.Item21);
                                }
                                else if (fingerChoice == 1)
                                {
                                    Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), new Vector2(NPC.position.X + (CorrectSide ? 32 : NPC.width - 32), NPC.position.Y + (NPC.height - 20)), new Vector2(0, 6f), ModContent.ProjectileType<Wrathbolt>(), 12 / 3, 0f);
                                    SoundEngine.PlaySound(SoundID.Item21);
                                }
                                else if (fingerChoice == 2)
                                {
                                    Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), new Vector2(NPC.position.X + (CorrectSide ? 42 : NPC.width - 42), NPC.position.Y + (NPC.height - 20)), new Vector2(0, 6f), ModContent.ProjectileType<Wrathbolt>(), 12 / 3, 0f);
                                    SoundEngine.PlaySound(SoundID.Item21);
                                }
                                else
                                {
                                    Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), new Vector2(NPC.position.X + (CorrectSide ? 62 : NPC.width - 62), NPC.position.Y + (NPC.height - 20)), new Vector2(0, 6f), ModContent.ProjectileType<Wrathbolt>(), 12 / 3, 0f);
                                    SoundEngine.PlaySound(SoundID.Item21);
                                }
                            }
                        }
                        if (AttackTimer >= 170f && AttackTimer < 240f)
                        {
                            NPC.velocity.X = 0f;
                            NPC.velocity.Y += 1.4f;
                            NPC.noTileCollide = false;
                        }
                        if (AttackTimer == 250f)
                        {
                            NPC.noTileCollide = true;
                            AttackVar = 6f;
                            if (InfernoHand != -1)
                            {
                                Main.npc[InfernoHand].ai[3] = 1f;
                            }
                            AttackTimer = 0f;
                            NPC.netUpdate = true;
                            return;
                        }
                    }
                    else
                    {
                        float speed = 20f;
                        AttackTimer++;
                        if (AttackTimer >= 0f && AttackTimer < 170f)
                        {
                            NPC.velocity.X = (((player.Center.X / 2f) - (NPC.Center.X / 2f)) + (CorrectSide ? 10 : -10)) / speed;
                            NPC.velocity.Y = (((player.Center.Y / 2f) - (NPC.Center.Y / 2f)) - 160) / speed;
                        }
                        if (AttackTimer >= 170f && AttackTimer < 240f)
                        {
                            NPC.velocity.X = 0f;
                            NPC.velocity.Y += 1.4f;
                            NPC.noTileCollide = false;
                        }
                        if (AttackTimer == 250f)
                        {
                            NPC.noTileCollide = true;
                            AttackVar = 6f;
                            if (InfernoHand != -1)
                            {
                                Main.npc[InfernoHand].ai[3] = 1f;
                            }
                            AttackTimer = 0f;
                            NPC.netUpdate = true;
                            return;
                        }
                    }
                }
                if (AttackVar == 5f)
                {
                    float speed = 20f;
                    AttackTimer++;
                    if (AttackTimer >= 0f && AttackTimer < 170f)
                    {
                        NPC.velocity.X = ((player.Center.X / 2f) - (NPC.Center.X / 2f)) / speed;
                        NPC.velocity.Y = ((player.Center.Y / 2f) - (NPC.Center.Y / 2f)) / speed;
                        if (player.Center.X >= NPC.Center.X)
                        {
                            NPC.spriteDirection = 1;
                        }
                        else
                        {
                            NPC.spriteDirection = -1;
                        }
                    }
                    if (AttackTimer >= 170f && AttackTimer < 240f)
                    {
                        if (player.Center.X >= NPC.Center.X)
                        {
                            NPC.spriteDirection = 1;
                        }
                        else
                        {
                            NPC.spriteDirection = -1;
                        }
                        if (NPC.Hitbox.Intersects(Main.npc[InfernoHand].Hitbox))
                        {
                            NPC.NewNPC((int)(Main.npc[InfernoHand].Center.X + NPC.Center.X) / 2, (int)((Main.npc[InfernoHand].Center.Y + NPC.Center.Y) / 2) + (NPC.height / 2), ModContent.NPCType<GrappleClap>(), 0, NPC.life, Main.npc[InfernoHand].life, DeltaTime);
                            Main.npc[InfernoHand].active = false;
                            Main.npc[InfernoHand].life = 0;
                            NPC.active = false;
                            NPC.life = 0;
                            return;
                        }
                    }
                    if (AttackTimer == 250f)
                    {
                        AttackVar = 6f;
                        if (InfernoHand != -1)
                        {
                            Main.npc[InfernoHand].ai[3] = 1f;
                        }
                        Grappled = false;
                        AttackTimer = 0f;
                        NPC.netUpdate = true;
                        return;
                    }
                }
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (AttackVar == 5f && !target.HasBuff(ModContent.BuffType<Struggle>()) && !target.HasBuff(ModContent.BuffType<Struggle>()) && !Grappled)
            {
                Grappled = true;
                if (Main.rand.Next(100) != 0)
                {
                    target.AddBuff(ModContent.BuffType<Struggle>(), (int)(Math.Abs((720 - AttackTimer) / 8)));
                }
                else
                {
                    target.AddBuff(ModContent.BuffType<StruggleAlt>(), (int)(Math.Abs((720 - AttackTimer) / 8)));
                }
            }
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            if (AttackVar == 5f && !Grappled)
            {
                damage = 0;
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            int amount = NPC.life <= 0 ? 10 : 2;

            for (int i = 0; i < amount; i++)
            {
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width + 4, NPC.height + 4, DustID.GreenMoss, Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f));
                dust.velocity *= 0.8f;
            }
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            if (harmfulCooldown > 0)
            {
                return false;
            }
            if (AttackVar == 5f && Grappled)
            {
                return false;
            }
            if (Initialize && InfernoHand != -1 && NPC.Left.X - 40 <= Main.npc[InfernoHand].Right.X)
            {
                return false;
            }
            return true;
        }
        public override void OnKill()
        {
            NPC.NewNPC((int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<MireGripDefeat>());
        }
    }
}