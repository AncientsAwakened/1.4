using AAMod.Content.Biomes.Inferno;
using AAMod.Content.Dusts;
using AAMod.Content.Items.Materials;
using AAMod.Content.Items.Placeables.Banners;
using AAMod.Content.Projectiles.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.NPCs.Enemies.Inferno
{
	public class Flamespitter : ModNPC
	{
        public enum States
        {
            Idle,
            Spit
        }

        public States State
        {
            get => (States) NPC.ai[0];
            set => NPC.ai[0] = (int) value;
        }

        public float SpitCooldown
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }

        private int frame;

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flamespitter");
            NPCDebuffImmunityData debuffData = new()
            {
                SpecificallyImmuneTo = new int[]
              {
                    BuffID.OnFire,
              }
            };
            NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);

            Main.npcFrameCount[NPC.type] = 16;
		}

        public override void SetDefaults()
        {
            NPC.width = 32;
            NPC.height = 12;
            DrawOffsetY = 5f;

            NPC.lavaImmune = true;

            NPC.damage = 40;
            NPC.defense = 15;
            NPC.lifeMax = 100;
            NPC.aiStyle = -1;

            NPC.value = 200f; // Item.sellPrice()
            NPC.knockBackResist = 0f;

            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;

            Banner = NPC.type;
            BannerItem = ModContent.ItemType<FlamespitterBanner>();
            SpawnModBiomes = new int[1] { ModContent.GetInstance<InfernoSurfaceBiome>().Type };
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
                new FlavorTextBestiaryInfoElement("Stealthy predators that hide withing the cracks of rocks and ambush their prey with long ranged fireballs. Be careful where you tread.")
            });
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;

            if (State == States.Idle)
                frame = 0;
            else
            {
                NPC.frameCounter++;
                if (NPC.frameCounter >= 5f)
                {
                    frame++;
                    NPC.frameCounter = 0f;
                }

                if (frame > 15)
                {
                    frame = 0;
                    State = States.Idle;
                }
            }

            int height = 12;
            switch (frame)
            {
                case 0:
                    height = 12;
                    break;
                case 1:
                    height = 14;
                    break;
                case 2:
                    height = 20;
                    break;
                case 3:
                    height = 32;
                    break;
                case 4:
                    height = 62;
                    break;
                case 5:
                    height = 42;
                    break;
                case 6:
                    height = 52;
                    break;
                case 7:
                    height = 52;
                    break;
                case 8:
                    height = 52;
                    break;
                case 9:
                    height = 50;
                    break;
                case 10:
                    height = 52;
                    break;
                case 11:
                    height = 46;
                    break;
                case 12:
                    height = 52;
                    break;
                case 13:
                    height = 64;
                    break;
                case 14:
                    height = 58;
                    break;
                case 15:
                    height = 26;
                    break;
            }

            NPC.position.Y -= (height - NPC.height);
            NPC.height = height;

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
            Player player = Main.player[NPC.target];

            NPC.TargetClosest();

            switch (State)
            {
                case States.Idle:
                    SpitCooldown++;
                    if (SpitCooldown >= Main.rand.Next(3, 5) * 60f && NPC.target == player.whoAmI)
                    {
                        State = States.Spit;
                        SpitCooldown = 0f;

                        NPC.netUpdate = true;
                    }
                    break;

                case States.Spit:
                    if (NPC.target != player.whoAmI)
                    {
                        State = States.Idle;

                        NPC.netUpdate = true;
                    }

                    if (frame == 11 && NPC.frameCounter == 0f)
                    {
                        Vector2 velocity = Vector2.Normalize(player.Center - NPC.Center) * 8f;

                        Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), NPC.Center, velocity, ModContent.ProjectileType<FlamespitterSpit>(), NPC.damage / 4, 2f);
                    }
                    break;

                default:
                    State = States.Idle;
                    break;
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DragonScale>(), 1, 1, 3));
        }

        public override void HitEffect(int hitDirection, double damage)
		{
            int amount = NPC.life <= 0 ? 10 : 2;

            for (int i = 0; i < amount; i++)
            {
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width + 4, NPC.height + 4, ModContent.DustType<Incinerite>(), Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f));
                dust.velocity *= 0.8f;
            }
            if (NPC.life <= 0)
			{
				for (int i = 0; i < 2; i++)
				{
                    Gore.NewGore(NPC.position, NPC.velocity, ModContent.Find<ModGore>("AAMod/FlamespitterGore" + (i + 1)).Type, 1);
                }
			}
		}

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.player.InModBiome(ModContent.GetInstance<InfernoSurfaceBiome>()) ? 0.2f : 0f;
        }
    }
}