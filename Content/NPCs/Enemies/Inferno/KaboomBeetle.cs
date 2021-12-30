using AAMod.Content.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Microsoft.Xna.Framework.Graphics;
using AAMod.Content.Biomes.Inferno;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using AAMod.Content.Dusts;

namespace AAMod.Content.NPCs.Enemies.Inferno
{
	public class KaboomBeetle : ModNPC
    {
        public enum States
        {
            Idle,
            Hidden,
            Explode
        }

        public States State
        {
            get => (States) NPC.ai[0];
            set => NPC.ai[0] = (int) value;
        }

        private int frame;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Kaboom Beetle");
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
            NPC.width = 20;
            NPC.height = 40;
            
            NPC.lavaImmune = true;

            NPC.lifeMax = 30;
            NPC.defense = 5;
            NPC.damage = 10;

            NPC.knockBackResist = 0.3f;

			NPC.value = 50f;

			NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.noTileCollide = false;
            SpawnModBiomes = new int[1] { ModContent.GetInstance<InfernoSurfaceBiome>().Type };
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
                new FlavorTextBestiaryInfoElement("A shy, explosive bug that only emerges after dark. If they feel even slightly threatened, they detonate.")
            });
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;

            NPC.frameCounter++;
            if (NPC.frameCounter >= 5f)
            {
                frame++;
                NPC.frameCounter = 0f;
            }

            if (State == States.Idle && frame > 4)
                frame = 0;
            else if (State == States.Hidden && frame > 7)
                frame = 7;
            else if (State == States.Explode && frame == 10 && NPC.frameCounter == 0f)
                Explode();
            else if (frame > 15)
                NPC.life = 0;

            NPC.frame.Y = frame * frameHeight;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>(NPC.ModNPC.Texture + "_Glow").Value;
            SpriteEffects effects = NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos, NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, effects, 0);
            spriteBatch.Draw(texture, NPC.Center - screenPos, NPC.frame, Color.White, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, effects, 0);

            return false;
        }

        public override void AI()
        {
            NPC.TargetClosest();

            Player player = Main.player[NPC.target];

            NPC.knockBackResist = State == States.Hidden ? 0f : 0.4f;

            switch (State)
            {
                case States.Idle:
                    NPC.velocity.X = NPC.DirectionTo(player.Center).X + 0.5f;

                    // TODO - Jumping

                    break;

                case States.Hidden:
                    NPC.velocity.X *= 0.98f;

                    if (NPC.target == player.whoAmI && player.Distance(NPC.Center) < 8f * 16f)
                    {
                        State = States.Explode;
                        NPC.netUpdate = true;
                    }

                    if (player.Distance(NPC.Center) > 8f * 16f)
                    {
                        State = States.Idle;
                        NPC.netUpdate = true;
                    }
                    break;

                case States.Explode:
                    NPC.velocity.X *= 0.98f;
                    break;

                default:
                    State = States.Idle;
                    break;
            }

            NPC.velocity.Y += 0.2f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DragonScale>(), 1));
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
                for (int i = 0; i < 4; i++)
                    Gore.NewGore(NPC.position, NPC.velocity, ModContent.Find<ModGore>("AAMod/KaboomBeetleGore" + (i + 1)).Type, 1);
        }

        private void Explode()
        {
            SoundEngine.PlaySound(SoundID.Item14, NPC.position);

            // TODO - Resize hitbox

            NPC.life = NPC.lifeMax;
            NPC.dontTakeDamage = true;

            for (int i = 0; i < 4; i++)
                Gore.NewGore(NPC.position, NPC.velocity, ModContent.Find<ModGore>("AAMod/KaboomBeetleGore" + (i + 1)).Type, 1);

            for (int j = 0; j < Main.rand.Next(2, 4); j++)
            {
                Gore.NewGore(NPC.Center, NPC.velocity, Main.rand.Next(61, 64));
                NPC.netUpdate = true;
            }

            NPC.netUpdate = true;
        }

        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            if (State != States.Hidden)
                State = States.Hidden;
        }

        public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
        {
            if (State != States.Hidden)
                State = States.Hidden;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.player.InModBiome(ModContent.GetInstance<InfernoSurfaceBiome>()) && !Main.dayTime ? 0.2f : 0f;
        }
    }
}
