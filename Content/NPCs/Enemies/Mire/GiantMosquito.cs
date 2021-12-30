using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using AAMod.Content.Items.Materials;
using AAMod.Content.Biomes.Mire;
using Terraria.GameContent.Bestiary;

namespace AAMod.Content.NPCs.Enemies.Mire
{
    public class GiantMosquito : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Giant Mosquito");
			Main.npcFrameCount[NPC.type] = 3;
		}
		public override void SetDefaults()
		{
			NPC.width = 32;
			NPC.height = 29;

			NPC.lifeMax = 150;
			NPC.damage = 20;
			NPC.defense = 2;

			NPC.knockBackResist = 1f;
			NPC.value = 200f;

			NPC.aiStyle = -1;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			SpawnModBiomes = new int[1] { ModContent.GetInstance<MireSurfaceBiome>().Type };
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
				new FlavorTextBestiaryInfoElement("Mosquitos grown to monstrous size. They've been observed to hoist prey into the air, then dropping them back down once they're complete drained.")
			});
		}

		private int frame;
		public override void FindFrame(int frameHeight)
		{
			NPC.spriteDirection = NPC.direction;
			if (++NPC.frameCounter > 3)
			{
				frame++;
				NPC.frameCounter = 0;
				if (frame > 2)
				{
					frame = 0;
				}
			}
			NPC.frame.Y = frame * frameHeight;
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot) => Item.NewItem(NPC.getRect(), ModContent.ItemType<BeastScales>(), Main.rand.Next(1, 3));

		public override void AI()
		{
			NPC.TargetClosest(true);
			Player player = Main.player[NPC.target];


			if (NPC.ai[0] == 0f)
			{
				NPC.velocity = Vector2.Normalize(player.Center - NPC.Center) * 4f;
			}
			
		}

		public override void OnHitPlayer(Player player, int damage, bool crit)
		{ 
			player.AddBuff(BuffID.Bleeding, 300);
			if (NPC.life != NPC.lifeMax)
			{
				NPC.life += 20;
				NPC.HealEffect(20);
			}
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			int amount = NPC.life <= 0 ? 10 : 2;

			for (int i = 0; i < amount; i++)
			{
				Dust dust = Dust.NewDustDirect(NPC.position, NPC.width + 4, NPC.height + 4, DustID.Blood, Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f));
				dust.velocity *= 0.8f;
			}
			if (NPC.life <= 0)
			{
				for (int i = 0; i < 3; i++)
				{
					Gore.NewGore(NPC.position, NPC.velocity, ModContent.Find<ModGore>("AAMod/MosquitoGore" + (i + 1)).Type, 1);
				}
			}
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return spawnInfo.player.InModBiome(ModContent.GetInstance<MireSurfaceBiome>()) ? 1f : 0f;
		}
	}
}