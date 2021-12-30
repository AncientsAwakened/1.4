using AAMod.Content.Biomes.Inferno;
using AAMod.Content.Dusts;
using AAMod.Content.Items.Placeables.Trophies;
using AAMod.Content.Items.TreasureBags;
using AAMod.Content.Items.Vanity.BossMasks;
using AAMod.Content.Items.Weapons.Magic;
using AAMod.Content.Items.Weapons.Melee;
using AAMod.Content.Items.Weapons.Ranged;
using AAMod.Content.Projectiles.Bosses;
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
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.NPCs.Bosses.Grips
{
    [AutoloadBossHead]
    class InfernoGrip : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Inferno Grip");
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
            {
                SpecificallyImmuneTo = new int[]
                {
                    BuffID.OnFire,
                    BuffID.Confused
                }
            };
            NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);
            Main.npcFrameCount[Type] = 5;
        }
        public override void SetDefaults()
        {
            NPC.width = 144;
            NPC.height = 106;
            NPC.damage = 12;
            NPC.defense = 10;
            NPC.lifeMax = 1400;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.value = Item.buyPrice(gold: 5);
            NPC.SpawnWithHigherTime(30);
            NPC.boss = true;
            BossBag = ModContent.ItemType<GripsTreasureBag>();
            NPC.npcSlots = 5f;
            NPC.aiStyle = -1;
            if (!Main.dedServ)
            {
                Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Grips");
            }
            SpawnModBiomes = new int[1] { ModContent.GetInstance<InfernoSurfaceBiome>().Type };
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
				new FlavorTextBestiaryInfoElement("Massive clawed abomination that stalks the Inferno at night. This one specialises in pyromancy, burning large threats down to ashes. Drawn out of its home to follow a powerful scent, It arrives to destroy any and all opponents.")
            });
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            if (MireHand == -1)
            {
                npcLoot.Add(ItemDropRule.BossBag(BossBag));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<InfernoGripTrophy>(), 10));

                LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());

                notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<InfernoGripMask>(), 7));

                notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1, ModContent.ItemType<TwinTalons>(), ModContent.ItemType<VnV>(), ModContent.ItemType<ClawCast>()));

                npcLoot.Add(notExpertRule);
            }
        }
        public bool Initialize
        {
            get => NPC.ai[0] == 0f;
            set => NPC.ai[0] = value ? 0f : 1f;
        }

        private int MireHand = -1;
        private int DeltaTime = 950;
        private float AttackTimer = 0f;
        private bool CorrectSide = true;
        private int harmfulCooldown = 0;
        public ref float HP => ref NPC.ai[1];
        public ref float AttackVar => ref NPC.ai[2];
        public ref float GripSync => ref NPC.ai[3];
        public override void FindFrame(int frameHeight)
        {
            Player player = Main.player[NPC.target];
            if (player.dead && NPC.CountNPCS(ModContent.NPCType<MireGrip>()) == 0)
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
            else if (AttackVar == 4f || AttackVar == 5f)
            {
                NPC.frame.Y = 3 * frameHeight;
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
            if (NPC.CountNPCS(ModContent.NPCType<MireGrip>()) == 1 && MireHand == -1)
            {
                MireHand = NPC.FindFirstNPC(ModContent.NPCType<MireGrip>());
            }
            if (!Initialize && NPC.CountNPCS(ModContent.NPCType<MireGrip>()) == 0 && MireHand != -1)
            {
                MireHand = -1;
            }
            if (Main.dayTime)
            {
                NPC.velocity.Y -= 0.12f;
                NPC.EncourageDespawn(10);
                return;
            }
            if (Initialize)
            {
                if (MireHand != -1)
                {
                    NPC.velocity.X += 0.12f;
                    if (NPC.Hitbox.Intersects(Main.npc[MireHand].Hitbox))
                    {
                        return;
                    }
                }
                else
                {
                    MireHand = NPC.NewNPC((int)NPC.Center.X + 2080, (int)NPC.Center.Y + (NPC.height / 2), ModContent.NPCType<MireGrip>(), NPC.whoAmI);
                    return;
                }
            }
            if (player.dead && NPC.CountNPCS(ModContent.NPCType<MireGrip>()) == 0)
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
            else if (player.dead && NPC.CountNPCS(ModContent.NPCType<MireGrip>()) == 1)
            {
                NPC.velocity.X = ((Main.npc[MireHand].Center.X / 2f) - (NPC.Center.X / 2f)) / 12f;
                NPC.velocity.Y = ((Main.npc[MireHand].Center.Y / 2f) - (NPC.Center.Y / 2f)) / 12f;
                if (NPC.Hitbox.Intersects(Main.npc[MireHand].Hitbox))
                {
                    return;
                }
            }
            if (!Initialize && !player.dead)
            {
                if (AttackVar == 1f)
                {
                    NPC.life = ((int)HP >= 1) ? (int)HP : 1;
                    AttackVar = 6f;
                    harmfulCooldown = 10;
                    NPC.netUpdate = true;
                }
                if (AttackVar == 0f)
                {
                    NPC.life = ((int)HP >= 1) ? (int)HP : 1;
                    harmfulCooldown = 10;
                    AttackVar = 6f;
                    NPC.netUpdate = true;
                }
                if (harmfulCooldown > 0)
                {
                    harmfulCooldown--;
                }
                if (AttackVar == 6f && MireHand != -1 && GripSync != 0f && Main.expertMode)
                {
                    AttackVar = Main.npc[MireHand].ai[2];
                    AttackTimer = 0;
                    NPC.netUpdate = true;
                }
                if (AttackVar == 6f && MireHand != -1 && GripSync == 0f && Main.expertMode)
                {
                    AttackVar = Main.rand.Next(2, 5);
                    AttackTimer = 0;
                    NPC.netUpdate = true;
                }
                if (AttackVar == 6f && (MireHand == -1 || !Main.expertMode))
                {
                    AttackTimer++;
                    if (Main.netMode != NetmodeID.MultiplayerClient && Main.expertMode ? AttackTimer >= 300f : AttackTimer >= 450f)
                    {
                        AttackVar = Main.rand.Next(2, 5);
                        AttackTimer = 0f;
                        NPC.netUpdate = true;
                    }
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
                        if (MireHand == -1)
                        {
                            if (player.Center.X < NPC.Center.X)
                            {
                                CorrectSide = false;
                                NPC.spriteDirection = 1;
                            }
                            else
                            {
                                CorrectSide = true;
                                NPC.spriteDirection = -1;
                            }
                        }
                        else
                        {
                            if (NPC.ai[0] == 2f)
                            {
                                if (CorrectSide)
                                {
                                    CorrectSide = false;
                                    NPC.spriteDirection = 1;
                                    NPC.ai[0] = 1;
                                }
                                else
                                {
                                    CorrectSide = true;
                                    NPC.spriteDirection = -1;
                                    NPC.ai[0] = 1;
                                }
                            }
                        }
                    }
                    if (!NPC.noTileCollide)
                    {
                        NPC.noTileCollide = true;
                    }
                }
                if (AttackVar == 2f)
                {
                    float speed = 12f;
                    AttackTimer++;
                    if (AttackTimer >= 70f && AttackTimer < 240f)
                    {
                        speed -= 0.1f;
                        if (AttackTimer % 10 == 0) 
                        {
                            SoundEngine.PlaySound(SoundID.Item34);
                        }
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), new Vector2(NPC.position.X + (CorrectSide ? NPC.width - 12 : 12), NPC.position.Y + 28), (new Vector2((player.Center.X / 2f) - (NPC.Center.X / 2f), (player.Center.Y / 2f) - (NPC.Center.Y / 2f)).RotatedBy(CorrectSide ? -19.3f : 19.3) / 40f), ModContent.ProjectileType<Furyflame>(), 8 / 3, 0f);
                            Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), new Vector2(NPC.position.X + (CorrectSide ? NPC.width - 12 : 12), NPC.position.Y + 78), (new Vector2((player.Center.X / 2f) - (NPC.Center.X / 2f), (player.Center.Y / 2f) - (NPC.Center.Y / 2f)).RotatedBy(CorrectSide ? 69.3f : -69.3f) / 40f), ModContent.ProjectileType<Furyflame>(), 8 / 3, 0f);
                        }
                    }
                    if (AttackTimer == 250f)
                    {
                        AttackVar = 6f;
                        AttackTimer = 0f;
                        NPC.netUpdate = true;
                        return;
                    }
                    NPC.velocity.X = (((player.Center.X / 2f) - (NPC.Center.X / 2f)) - (CorrectSide ? 140 : -140)) / speed;
                    NPC.velocity.Y = (((player.Center.Y / 2f) - (NPC.Center.Y / 2f)) - 30) / speed;
                }
                if (AttackVar == 3f)
                {
                    if (Main.expertMode)
                    {
                        float speed = 16f;
                        AttackTimer++;
                        if (AttackTimer < 70f)
                        {
                            NPC.velocity.X = (((player.Center.X / 2f) - (NPC.Center.X / 2f)) - (CorrectSide ? 280 : -280)) / speed;
                            NPC.velocity.Y = (((player.Center.Y / 2f) - (NPC.Center.Y / 2f)) - 20) / speed;
                        }
                        if (AttackTimer >= 70f && AttackTimer < 140f)
                        {
                            NPC.velocity.X = (((player.Center.X / 2f) - (NPC.Center.X / 2f)) + (CorrectSide ? 280 : -280)) / speed;
                            NPC.velocity.Y = (((player.Center.Y / 2f) - (NPC.Center.Y / 2f)) - 20) / speed;
                        }
                        if (Main.netMode != NetmodeID.MultiplayerClient && AttackTimer == 140f)
                        {
                            NPC.spriteDirection = (CorrectSide ? 1 : -1);
                        }
                        if (AttackTimer >= 140f && AttackTimer < 210f)
                        {
                            NPC.velocity.X = (((player.Center.X / 2f) - (NPC.Center.X / 2f)) - (CorrectSide ? 280 : -280)) / speed;
                            NPC.velocity.Y = (((player.Center.Y / 2f) - (NPC.Center.Y / 2f)) - 20) / speed;
                        }
                        if (Main.netMode != NetmodeID.MultiplayerClient && AttackTimer == 210f)
                        {
                            NPC.spriteDirection = (CorrectSide ? -1 : 1);
                        }
                        if (AttackTimer >= 210f && AttackTimer < 250f)
                        {
                            NPC.velocity.X = (((player.Center.X / 2f) - (NPC.Center.X / 2f)) - (CorrectSide ? 280 : -280)) / speed;
                            NPC.velocity.Y = (((player.Center.Y / 2f) - (NPC.Center.Y / 2f)) - 20) / speed;
                        }
                        if (AttackTimer == 250f)
                        {
                            AttackVar = 6f;
                            AttackTimer = 0f;
                            NPC.netUpdate = true;
                            return;
                        }
                    }
                    else
                    {
                        float speed = 20f;
                        AttackTimer++;
                        if (AttackTimer < 80f)
                        {
                            NPC.velocity.X = (((player.Center.X / 2f) - (NPC.Center.X / 2f)) - (CorrectSide ? 280 : -280)) / speed;
                            NPC.velocity.Y = (((player.Center.Y / 2f) - (NPC.Center.Y / 2f)) - 20) / speed;
                        }
                        if (AttackTimer >= 80f && AttackTimer < 160f)
                        {
                            NPC.velocity.X = (((player.Center.X / 2f) - (NPC.Center.X / 2f)) + (CorrectSide ? 280 : -280)) / speed;
                            NPC.velocity.Y = (((player.Center.Y / 2f) - (NPC.Center.Y / 2f)) - 20) / speed;
                        }
                        if (Main.netMode != NetmodeID.MultiplayerClient && AttackTimer == 160f)
                        {
                            NPC.spriteDirection = (CorrectSide ? 1 : -1);
                        }
                        if (AttackTimer >= 160f && AttackTimer < 240f)
                        {
                            NPC.velocity.X = (((player.Center.X / 2f) - (NPC.Center.X / 2f)) - (CorrectSide ? 280 : -280)) / speed;
                            NPC.velocity.Y = (((player.Center.Y / 2f) - (NPC.Center.Y / 2f)) - 20) / speed;
                        }
                        if (Main.netMode != NetmodeID.MultiplayerClient && AttackTimer == 240f)
                        {
                            NPC.spriteDirection = (CorrectSide ? -1 : 1);
                        }
                        if (AttackTimer >= 240f && AttackTimer < 250f)
                        {
                            NPC.velocity.X = (((player.Center.X / 2f) - (NPC.Center.X / 2f)) - (CorrectSide ? 280 : -280)) / speed;
                            NPC.velocity.Y = (((player.Center.Y / 2f) - (NPC.Center.Y / 2f)) - 20) / speed;
                        }
                        if (AttackTimer == 250f)
                        {
                            AttackVar = 6f;
                            AttackTimer = 0f;
                            NPC.netUpdate = true;
                            return;
                        }
                    }
                }
                if (AttackVar == 4f)
                {
                    float speed = 20f;
                    AttackTimer++;
                    if (AttackTimer >= 0f && AttackTimer < 170f)
                    {
                        NPC.velocity.X = (((player.Center.X / 2f) - (NPC.Center.X / 2f)) + (CorrectSide ? -10 : 10)) / speed;
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
                        AttackTimer = 0f;
                        NPC.netUpdate = true;
                        return;
                    }
                }
                if (AttackVar == 5f)
                {
                    float speed = 20f;
                    AttackTimer++;
                    if (AttackTimer >= 0f && AttackTimer < 170f)
                    {
                        NPC.velocity.X = ((player.Center.X / 2f) - (NPC.Center.X / 2f)) / speed;
                        NPC.velocity.Y = (((player.Center.Y / 2f) - (NPC.Center.Y / 2f)) - 160) / speed;
                    }
                    if (AttackTimer >= 170f && AttackTimer < 240f)
                    {
                        NPC.velocity.X = 0f;
                        NPC.velocity.Y += 1.4f;
                        if (NPC.Hitbox.Intersects(Main.npc[MireHand].Hitbox))
                        {
                            return;
                        }
                    }
                    if (AttackTimer == 250f)
                    {
                        AttackVar = 6f;
                        AttackTimer = 0f;
                        NPC.netUpdate = true;
                        return;
                    }
                }
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (AttackVar == 3f || (AttackVar == 4f && Main.expertMode))
            {
                target.AddBuff(BuffID.OnFire, 90);
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            int amount = NPC.life <= 0 ? 10 : 2;

            for (int i = 0; i < amount; i++)
            {
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width + 4, NPC.height + 4, ModContent.DustType<Incinerite>(), Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f));
                dust.velocity *= 0.8f;
            }
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            if (harmfulCooldown > 0)
            {
                return false;
            }
            if (AttackVar == 5)
            {
                return false;
            }
            if (Initialize && MireHand != -1 && NPC.Right.X + 40 >= Main.npc[MireHand].Left.X)
            {
                return false;
            }
            return true;
        }

        public override void OnKill()
        {
            NPC.NewNPC((int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<InfernoGripDefeat>());
        }
    }
}