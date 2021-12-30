using AAMod.Content.Items.Placeables.Banners;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using AAMod.Content.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Bestiary;
using AAMod.Content.Biomes.Inferno;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using AAMod.Content.Dusts;

namespace AAMod.Content.NPCs.Enemies.Inferno
{
    public abstract class Crag : ModNPC
    {
        public override string Texture => AAMod.InvisibleTexture;
    }
    public class SmallCrag : ModNPC
    {
        public enum States
        {
            Idle,
            Dash
        }
        public States State
        {
            get => (States)NPC.ai[0];
            set => NPC.ai[0] = (int)value;
        }

        public float Cooldown
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }

        public float speed;
        public float walkSpeed;
        public float runSpeed;

        public float[] oldRotation = new float[3];
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crag");
            NPCDebuffImmunityData debuffData = new()
            {
                SpecificallyImmuneTo = new int[]
              {
                    BuffID.OnFire,
              }
            };
            NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);

            Main.npcFrameCount[NPC.type] = 5;

            NPCID.Sets.TrailCacheLength[NPC.type] = 3;
            NPCID.Sets.TrailingMode[NPC.type] = 1;
        }
        public override void SetDefaults()
        {
            NPC.width = 32;
            NPC.height = 34;

            NPC.lifeMax = 40;
            NPC.defense = 5;
            NPC.damage = 20;

            walkSpeed = 1f;
            runSpeed = 8f;

			NPC.value = 50f;

			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;

            NPC.lavaImmune = true;

            Banner = NPC.type;
            BannerItem = ModContent.ItemType<CragBanner>();
            NPC.noTileCollide = false;
            SpawnModBiomes = new int[1] { ModContent.GetInstance<InfernoSurfaceBiome>().Type };
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
                new FlavorTextBestiaryInfoElement("Precious.")
            });
        }
        private int frame;
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;

            if (State == States.Idle)
            {
                if (NPC.velocity.X != 0f || NPC.velocity.Y != 0f)
                    NPC.frameCounter++;

                if (NPC.frameCounter >= 5f)
                {
                    frame++;
                    NPC.frameCounter = 0f;
                }

                if (frame > 3)
                    frame = 0;
            }
            else
                frame = 4;

            NPC.frame.Y = frame * frameHeight;
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
			{
				for (int i = 0; i < 3; i++)
				{
                    Gore.NewGore(NPC.position, NPC.velocity, ModContent.Find<ModGore>("AAMod/SmallCragGore" + (i + 1)).Type, 1);
                }
			}
		}
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>("AAMod/Content/NPCs/Enemies/Inferno/SmallCrag").Value;
            var effects = NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            Texture2D glowTex = ModContent.Request<Texture2D>(NPC.ModNPC.Texture + "_Glow").Value;

            if (State == States.Dash)
                for (int i = 0; i < NPCID.Sets.TrailCacheLength[NPC.type]; i++)
                {
                    Vector2 oldPos = NPC.oldPos[i];
                    Color color = NPC.GetAlpha(Color.Orange) * ((NPC.oldPos.Length - i) / (float)NPC.oldPos.Length / 2f);

                    Main.spriteBatch.Draw
                    (
                        texture,
                        oldPos + NPC.Size / 2f - Main.screenPosition + new Vector2(0, NPC.gfxOffY) + new Vector2(0, 6),
                        NPC.frame,
                        color,
                        oldRotation[i],
                        NPC.frame.Size() / 2f,
                        NPC.scale,
                        effects,
                        0f
                    );
                }

            spriteBatch.Draw(texture, NPC.Center - screenPos + new Vector2(0, 6), NPC.frame, NPC.GetAlpha(drawColor), NPC.rotation, NPC.frame.Size() / 2, NPC.scale, effects, 0);
            spriteBatch.Draw(glowTex, NPC.Center - screenPos + new Vector2(0, 6), NPC.frame, NPC.GetAlpha(Color.White), NPC.rotation, NPC.frame.Size() / 2, NPC.scale, effects, 0);

            return false;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.player.InModBiome(ModContent.GetInstance<InfernoSurfaceBiome>()) && !Main.dayTime ? 0.2f : 0f;
        }
        public override void AI()
        {
            for (int k = NPC.oldPos.Length - 1; k > 0; k--)
                oldRotation[k] = oldRotation[k - 1];

            oldRotation[0] = NPC.rotation;

            NPC.TargetClosest();

            Player player = Main.player[NPC.target];

            NPC.knockBackResist = State == States.Idle ? 0.25f : 0f;
            speed = MathHelper.SmoothStep(speed, State == States.Dash ? runSpeed : walkSpeed, 0.1f);

            switch (State)
            {
                case States.Idle:
                    NPC.rotation = 0f;

                    Cooldown++;
                    if (Cooldown >= 300f && player.Distance(NPC.Center) < 30f * 16f && NPC.target == player.whoAmI)
                    {
                        State = States.Dash;
                        Cooldown = 0f;

                        NPC.netUpdate = true;
                    }
                    break;

                case States.Dash:
                    NPC.rotation += Math.Abs(NPC.velocity.X) / 20f * NPC.direction;

                    Cooldown++;
                    if (Cooldown >= 300f)
                    {
                        State = States.Idle;
                        Cooldown = 0f;

                        NPC.velocity.Y = -4f;

                        NPC.netUpdate = true;
                    }
                    break;

                default:
                    State = States.Idle;
                    break;
            }

            NPC.velocity.X = NPC.DirectionTo(player.Center).X + 0.75f;
            NPC.velocity.Y += 0.2f;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            State = States.Idle;

            target.AddBuff(BuffID.OnFire, 180);
        }
        public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
        {
            State = States.Dash;
        }
        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            State = States.Dash;
        }
    }


    public class NormalCrag : ModNPC
    {
        public enum States
        {
            Idle,
            Dash
        }
        public States State
        {
            get => (States)NPC.ai[0];
            set => NPC.ai[0] = (int)value;
        }

        public float Cooldown
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }

        public float speed;
        public float walkSpeed;
        public float runSpeed;

        public float[] oldRotation = new float[3];
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crag");

            Main.npcFrameCount[NPC.type] = 5;
            NPCDebuffImmunityData debuffData = new()
            {
                SpecificallyImmuneTo = new int[]
              {
                    BuffID.OnFire,
              }
            };
            NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);

            NPCID.Sets.TrailCacheLength[NPC.type] = 3;
            NPCID.Sets.TrailingMode[NPC.type] = 1;
        }
        public override void SetDefaults()
        {
            NPC.width = 40;
            NPC.height = 38;

            NPC.lifeMax = 50;
            NPC.defense = 7;
            NPC.damage = 30;

            walkSpeed = 0.75f;
            runSpeed = 6f;

			NPC.value = 100f;

			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;

            NPC.lavaImmune = true;

            Banner = NPC.type;
            BannerItem = ModContent.ItemType<CragBanner>();
            NPC.noTileCollide = false;
            SpawnModBiomes = new int[1] { ModContent.GetInstance<InfernoSurfaceBiome>().Type };
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
                new FlavorTextBestiaryInfoElement("Precious.")
            });
        }
        private int frame;
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;

            if (State == States.Idle)
            {
                if (NPC.velocity.X != 0f || NPC.velocity.Y != 0f)
                    NPC.frameCounter++;

                if (NPC.frameCounter >= 5f)
                {
                    frame++;
                    NPC.frameCounter = 0f;
                }

                if (frame > 3)
                    frame = 0;
            }
            else
                frame = 4;

            NPC.frame.Y = frame * frameHeight;
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
				for (int i = 0; i < 5; i++)
				{
                    Gore.NewGore(NPC.position, NPC.velocity, ModContent.Find<ModGore>("AAMod/CragGore" + (i + 1)).Type, 1);
                }
			}
		}

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>("AAMod/Content/NPCs/Enemies/Inferno/NormalCrag").Value;
            var effects = NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            Texture2D glowTex = ModContent.Request<Texture2D>(NPC.ModNPC.Texture + "_Glow").Value;

            if (State == States.Dash)
                for (int i = 0; i < NPCID.Sets.TrailCacheLength[NPC.type]; i++)
                {
                    Vector2 oldPos = NPC.oldPos[i];
                    Color color = NPC.GetAlpha(Color.Orange) * ((NPC.oldPos.Length - i) / (float)NPC.oldPos.Length / 2f);

                    Main.spriteBatch.Draw
                    (
                        texture,
                        oldPos + NPC.Size / 2f - Main.screenPosition + new Vector2(0, NPC.gfxOffY) + new Vector2(0, 6),
                        NPC.frame,
                        color,
                        oldRotation[i],
                        NPC.frame.Size() / 2f,
                        NPC.scale,
                        effects,
                        0f
                    );
                }

            spriteBatch.Draw(texture, NPC.Center - screenPos + new Vector2(0, 6), NPC.frame, NPC.GetAlpha(drawColor), NPC.rotation, NPC.frame.Size() / 2, NPC.scale, effects, 0);
            spriteBatch.Draw(glowTex, NPC.Center - screenPos + new Vector2(0, 6), NPC.frame, NPC.GetAlpha(Color.White), NPC.rotation, NPC.frame.Size() / 2, NPC.scale, effects, 0);

            return false;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.player.InModBiome(ModContent.GetInstance<InfernoSurfaceBiome>()) && !Main.dayTime ? 0.2f : 0f;
        }
        public override void AI()
        {
            for (int k = NPC.oldPos.Length - 1; k > 0; k--)
                oldRotation[k] = oldRotation[k - 1];

            oldRotation[0] = NPC.rotation;

            NPC.TargetClosest();

            Player player = Main.player[NPC.target];

            NPC.knockBackResist = State == States.Idle ? 0.25f : 0f;
            speed = MathHelper.SmoothStep(speed, State == States.Dash ? runSpeed : walkSpeed, 0.1f);

            switch (State)
            {
                case States.Idle:
                    NPC.rotation = 0f;

                    Cooldown++;
                    if (Cooldown >= 300f && player.Distance(NPC.Center) < 30f * 16f && NPC.target == player.whoAmI)
                    {
                        State = States.Dash;
                        Cooldown = 0f;

                        NPC.netUpdate = true;
                    }
                    break;

                case States.Dash:
                    NPC.rotation += Math.Abs(NPC.velocity.X) / 20f * NPC.direction;

                    Cooldown++;
                    if (Cooldown >= 300f)
                    {
                        State = States.Idle;
                        Cooldown = 0f;

                        NPC.velocity.Y = -4f;

                        NPC.netUpdate = true;
                    }
                    break;

                default:
                    State = States.Idle;
                    break;
            }

            NPC.velocity.X = NPC.DirectionTo(player.Center).X + 0.75f;
            NPC.velocity.Y += 0.2f;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            State = States.Idle;

            target.AddBuff(BuffID.OnFire, 180);
        }
        public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
        {
            State = States.Dash;
        }
        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            State = States.Dash;
        }
    }

    public class LargeCrag : ModNPC
    {
        public enum States
        {
            Idle,
            Dash
        }
        public States State
        {
            get => (States)NPC.ai[0];
            set => NPC.ai[0] = (int)value;
        }

        public float Cooldown
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }

        public float speed;
        public float walkSpeed;
        public float runSpeed;

        public float[] oldRotation = new float[3];
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crag");

            Main.npcFrameCount[NPC.type] = 5;
            NPCDebuffImmunityData debuffData = new()
            {
                SpecificallyImmuneTo = new int[]
              {
                    BuffID.OnFire,
              }
            };
            NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);

            NPCID.Sets.TrailCacheLength[NPC.type] = 3;
            NPCID.Sets.TrailingMode[NPC.type] = 1;
        }
        public override void SetDefaults()
        {
            NPC.width = 46;
            NPC.height = 54;

            NPC.lifeMax = 60;
            NPC.defense = 10;
            NPC.damage = 40;

            walkSpeed = 0.5f;
            runSpeed = 4f;

			NPC.value = 200f;

			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;

            NPC.lavaImmune = true;

            Banner = NPC.type;
            BannerItem = ModContent.ItemType<CragBanner>();
            NPC.noTileCollide = false;
            SpawnModBiomes = new int[1] { ModContent.GetInstance<InfernoSurfaceBiome>().Type };
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
                new FlavorTextBestiaryInfoElement("Precious.")
            });
        }
        private int frame;
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;

            if (State == States.Idle)
            {
                if (NPC.velocity.X != 0f || NPC.velocity.Y != 0f)
                    NPC.frameCounter++;

                if (NPC.frameCounter >= 5f)
                {
                    frame++;
                    NPC.frameCounter = 0f;
                }

                if (frame > 3)
                    frame = 0;
            }
            else
                frame = 4;

            NPC.frame.Y = frame * frameHeight;
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
            if (NPC.life <= 0)
			{
				for (int i = 0; i < 6; i++)
				{
                    Gore.NewGore(NPC.position, NPC.velocity, ModContent.Find<ModGore>("AAMod/LargeCragGore" + (i + 1)).Type, 1);
                }
			}
		}
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>("AAMod/Content/NPCs/Enemies/Inferno/LargeCrag").Value;
            var effects = NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            Texture2D glowTex = ModContent.Request<Texture2D>(NPC.ModNPC.Texture + "_Glow").Value;

            if (State == States.Dash)
                for (int i = 0; i < NPCID.Sets.TrailCacheLength[NPC.type]; i++)
                {
                    Vector2 oldPos = NPC.oldPos[i];
                    Color color = NPC.GetAlpha(Color.Orange) * ((NPC.oldPos.Length - i) / (float)NPC.oldPos.Length / 2f);

                    Main.spriteBatch.Draw
                    (
                        texture,
                        oldPos + NPC.Size / 2f - Main.screenPosition + new Vector2(0, NPC.gfxOffY) + new Vector2(0, 6),
                        NPC.frame,
                        color,
                        oldRotation[i],
                        NPC.frame.Size() / 2f,
                        NPC.scale,
                        effects,
                        0f
                    );
                }

            spriteBatch.Draw(texture, NPC.Center - screenPos + new Vector2(0, 6), NPC.frame, NPC.GetAlpha(drawColor), NPC.rotation, NPC.frame.Size() / 2, NPC.scale, effects, 0);
            spriteBatch.Draw(glowTex, NPC.Center - screenPos + new Vector2(0, 6), NPC.frame, NPC.GetAlpha(Color.White), NPC.rotation, NPC.frame.Size() / 2, NPC.scale, effects, 0);

            return false;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.player.InModBiome(ModContent.GetInstance<InfernoSurfaceBiome>()) && !Main.dayTime ? 0.2f : 0f;
        }
        public override void AI()
        {
            for (int k = NPC.oldPos.Length - 1; k > 0; k--)
                oldRotation[k] = oldRotation[k - 1];

            oldRotation[0] = NPC.rotation;

            NPC.TargetClosest();

            Player player = Main.player[NPC.target];

            NPC.knockBackResist = State == States.Idle ? 0.25f : 0f;
            speed = MathHelper.SmoothStep(speed, State == States.Dash ? runSpeed : walkSpeed, 0.1f);

            switch (State)
            {
                case States.Idle:
                    NPC.rotation = 0f;

                    Cooldown++;
                    if (Cooldown >= 300f && player.Distance(NPC.Center) < 30f * 16f && NPC.target == player.whoAmI)
                    {
                        State = States.Dash;
                        Cooldown = 0f;

                        NPC.netUpdate = true;
                    }
                    break;

                case States.Dash:
                    NPC.rotation += Math.Abs(NPC.velocity.X) / 20f * NPC.direction;

                    Cooldown++;
                    if (Cooldown >= 300f)
                    {
                        State = States.Idle;
                        Cooldown = 0f;

                        NPC.velocity.Y = -4f;

                        NPC.netUpdate = true;
                    }
                    break;

                default:
                    State = States.Idle;
                    break;
            }

            NPC.velocity.X = NPC.DirectionTo(player.Center).X + 0.75f;
            NPC.velocity.Y += 0.2f;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            State = States.Idle;

            target.AddBuff(BuffID.OnFire, 180);
        }
        public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
        {
            State = States.Dash;
        }
        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            State = States.Dash;
        }
    }
}
