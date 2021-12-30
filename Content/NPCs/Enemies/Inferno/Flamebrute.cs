using System;
using AAMod.Content.Items.Materials;
using AAMod.Content.Items.Placeables.Banners;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using AAMod.Content.Projectiles.Enemies;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Bestiary;
using AAMod.Content.Biomes.Inferno;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using AAMod.Content.Dusts;

namespace AAMod.Content.NPCs.Enemies.Inferno
{
	public class Flamebrute : ModNPC
    {
        public enum States
        {
            Idle,
            Spit,
            Death
        }

        public States State
        {
            get => (States)NPC.ai[0];
            set => NPC.ai[0] = (int)value;
        }

        public float AttackTimer
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }

        private int frame;

        private float speed;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flamebrute");
            NPCDebuffImmunityData debuffData = new()
            {
                SpecificallyImmuneTo = new int[]
              {
                    BuffID.OnFire,
              }
            };
            NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);

            Main.npcFrameCount[NPC.type] = 17;
        }

        public override void SetDefaults()
        {
            NPC.width = 40;
            NPC.height = 80;

            NPC.lavaImmune = true;

            NPC.lifeMax = 70;
            NPC.defense = 5;
            NPC.damage = 20;

            NPC.knockBackResist = 0.1f;
			NPC.value = 200f;

			Banner = NPC.type;
			BannerItem = ModContent.ItemType<FlamebruteBanner>();

            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;

            NPC.noTileCollide = false;
            SpawnModBiomes = new int[1] { ModContent.GetInstance<InfernoSurfaceBiome>().Type };
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
                new FlavorTextBestiaryInfoElement("A particularly unruly resident of the Inferno that stores excess heat in its belly. They are known to spit fireballs at anything that catches their attention.")
            });
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;

            NPC.frameCounter++;
            if (NPC.frameCounter >= 8f)
            {
                frame++;
                NPC.frameCounter = 0f;
            }

            if (State == States.Idle && frame > 5)
                frame = 0;
            else if (State == States.Spit && frame > 11)
                State = States.Idle;
            else if (State == States.Death && frame > 16)
            {
                NPC.life = 0;

                NPC.HitEffect(0, 0);
                NPC.checkDead();
            }

            NPC.frame.Y = frame * frameHeight;
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
            NPC.TargetClosest();

            Player player = Main.player[NPC.target];

            switch (State)
            {
                case States.Idle:
                    speed = 0.75f;

                    if (Math.Abs(NPC.Center.X - player.Center.X) < 64f)
                        NPC.velocity.X *= 0.98f;
                    else
                        NPC.velocity.X = NPC.direction * speed;

                    AttackTimer++;
                    if (AttackTimer >= Main.rand.Next(3, 6) * 60 && NPC.target == player.whoAmI)
                    {
                        State = States.Spit;
                        AttackTimer = 0f;

                        NPC.netUpdate = true;
                    }
                    break;

                case States.Spit:
                    NPC.velocity.X *= 0.98f;

                    if (NPC.target != player.whoAmI)
                    {
                        State = States.Idle;

                        // npc.netUpdate = false;
                    }

                    if (frame == 9 && NPC.frameCounter == 0f)
                    {
                        Vector2 velocity = Vector2.Normalize(player.Center - NPC.Center) * 8f;
                        Vector2 offset = new Vector2(0f, -20f);

                        Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), NPC.Center + offset, velocity, ModContent.ProjectileType<FlamespitterSpit>(), NPC.damage / 2, 3f);
                    }
                    break;

                case States.Death:
                    NPC.velocity.X = 0f;
                    break;

                default:
                    State = States.Idle;
                    break;
            }

            NPC.velocity.Y += 0.2f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DragonScale>(), 1, 2, 4));
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            int amount = NPC.life <= 0 ? 10 : 2;

            for (int i = 0; i < amount; i++)
            {
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width + 4, NPC.height + 4, ModContent.DustType<Incinerite>(), Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f));
                dust.velocity *= 0.8f;
            }
            if (NPC.life <= 0 && State == States.Death)
            {
                for (int i = 0; i < 7; i++)
                    Gore.NewGore(NPC.position, NPC.velocity, ModContent.Find<ModGore>("AAMod/FlamebruteGore" + (i + 1)).Type, 1);

                for (int i = 0; i < 3; i++)
                    Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), NPC.Center, new Vector2(Main.rand.NextFloat(-3f, 4f), -Main.rand.NextFloat(-3f, 4f)),
                        ModContent.ProjectileType<FlamespitterSpit>(), NPC.damage / 4, 3f);
            }
        }

        public override bool CheckDead()
        {
            if (State != States.Death)
            {
                State = States.Death;
                frame = 10;

                NPC.life = NPC.lifeMax;
                NPC.damage = 0;

                NPC.dontTakeDamage = true;
                NPC.netUpdate = true;

                return false;
            }

            return true;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.player.InModBiome(ModContent.GetInstance<InfernoSurfaceBiome>()) && Main.dayTime? 0.25f : 0f;
        }
    }
}
