using AAMod.Content.Biomes.Mire;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.NPCs.Critters.Mire
{
	public class AcidFrog : ModNPC
	{
		private ref float DirectionTimer => ref NPC.ai[0];

		private int frame;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Acid Frog");

			Main.npcFrameCount[NPC.type] = 5;
		}

		public override void SetDefaults()
		{
			NPC.width = 35;
			NPC.height = 20;

			NPC.lifeMax = 50;

			NPC.aiStyle = -1;
			AIType = -1;

			NPC.knockBackResist = 0.3f;

			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			SpawnModBiomes = new int[1] { ModContent.GetInstance<MireSurfaceBiome>().Type };
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
				new FlavorTextBestiaryInfoElement("A rather large frog.")
			});
		}

		public override void FindFrame(int frameHeight)
		{
			NPC.spriteDirection = NPC.direction;

			if (NPC.velocity.Y == 0f)
			{
				frame--;
			}
			else
			{
				frame++;
			}

			frame = (int)MathHelper.Clamp(frame, 0, 4);
			NPC.frame.Y = frame * frameHeight;
		}

		public override void AI()
		{
			if (NPC.life != NPC.lifeMax)
			{
				ChooseRandomDirection();
			}

			if (NPC.velocity.Y < 0f)
			{
				NPC.velocity.X += NPC.direction * 0.1f;
			}
			else
			{
				NPC.velocity.X *= 0.92f;
			}

			NPC.velocity.Y += 0.3f;

			Collision.StepUp(ref NPC.position, ref NPC.velocity, NPC.width, NPC.height, ref NPC.stepSpeed, ref NPC.gfxOffY);
		}

		private void ChooseRandomDirection()
		{
			DirectionTimer++;

			if (DirectionTimer > Main.rand.Next(2, 5) * 60f)
			{
				NPC.velocity.Y = -Main.rand.NextFloat(6f, 8f);

				NPC.direction = Main.rand.NextBool() ? -1 : 1;
				NPC.netUpdate = true;

				DirectionTimer = 0f;
			}
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return spawnInfo.player.InModBiome(ModContent.GetInstance<MireSurfaceBiome>()) && Main.dayTime ? 1f : 0f;
		}
	}
}
