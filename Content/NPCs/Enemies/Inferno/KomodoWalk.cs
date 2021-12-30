using AAMod.Content.Biomes.Inferno;
using AAMod.Content.Dusts;
using AAMod.Content.Items.Materials;
using AAMod.Content.Items.Placeables.Banners;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.NPCs.Enemies.Inferno
{
	public class KomodoWalk : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Komodo");
            NPCDebuffImmunityData debuffData = new()
            {
                SpecificallyImmuneTo = new int[]
               {
                    BuffID.OnFire,
               }
            };
            NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);

            Main.npcFrameCount[NPC.type] = 4;
        }

        public override void SetDefaults()
        {
            NPC.width = 66;
            NPC.height = 30;

            NPC.lavaImmune = true;

            NPC.lifeMax = 100;
            NPC.damage = 20;
            NPC.defense = 14;
            NPC.aiStyle = -1;

            Banner = NPC.type;
            BannerItem = ModContent.ItemType<KomodoBanner>();

            NPC.knockBackResist = 0.2f;
			NPC.value = 300f;

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
                new FlavorTextBestiaryInfoElement("Large reptiles that are much more agile than they let on to be. When near a wall, they will cling to it and crawl along it with ludicrous speed.")
            });
        }

        private int frame;

        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            if (NPC.velocity.X != 0 || NPC.velocity.Y != 0) 
                NPC.frameCounter++;

            if (NPC.frameCounter >= 8f)
            {
                frame++;

                NPC.frameCounter = 0f;
            }

            if (frame > 3)
                frame = 0;

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

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>(NPC.ModNPC.Texture + "_Glow").Value;
            SpriteEffects effects = NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Vector2 origin = NPC.frame.Size() / 2f + new Vector2(0f, NPC.ModNPC.DrawOffsetY);
            Vector2 position = NPC.Center - screenPos + new Vector2(0f, NPC.gfxOffY);
            Main.EntitySpriteDraw(texture, position, NPC.frame, Color.White, NPC.rotation, origin, NPC.scale, effects, 0);
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DragonScale>(), 1, 1, 3));
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];

            NPC.TargetClosest();

            NPC.velocity.X = NPC.DirectionTo(player.Center).X + 0.5f;

            if (Main.netMode != NetmodeID.MultiplayerClient && NPC.velocity.Y == 0f)
			{
				int num = (int)NPC.Center.X / 16;
				int num2 = (int)NPC.Center.Y / 16;
				bool flag = false;

				for (int i = num - 1; i <= num + 1; i++)
                    for (int j = num2 - 1; j <= num2 + 1; j++)
                        if (Main.tile[i, j] != null && Main.tile[i, j].wall > 0)
                            flag = true;
                
				if (flag)
                    NPC.Transform(ModContent.NPCType<KomodoWall>());
            }

            NPC.velocity.Y += 0.2f;
        }

        public override void OnHitPlayer(Player player, int damage, bool crit) => player.AddBuff(BuffID.OnFire, 120);

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
				for (int i = 0; i < 4; i++)
				{
                    Gore.NewGore(NPC.position, NPC.velocity, ModContent.Find<ModGore>("AAMod/KomodoGore" + (i + 1)).Type, 1);
                }
			}
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.player.InModBiome(ModContent.GetInstance<InfernoSurfaceBiome>()) && Main.dayTime ? 0.2f : 0f;
        }
    }
}
