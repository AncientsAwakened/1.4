using AAMod.Content.Biomes.Mire;
using AAMod.Content.Items.Materials;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.NPCs.Enemies.Mire
{
    public class Gator : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gator");

			Main.npcFrameCount[NPC.type] = 1;
		}

		public override void SetDefaults()
		{
			NPC.width = 90;
			NPC.height = 25;

			NPC.lifeMax = 70;
			NPC.damage = 20;
			NPC.defense = 2;

			NPC.aiStyle = 3;

			NPC.knockBackResist = 0.5f;
			NPC.value = 200f;

			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;

			AIType = NPCID.GoblinWarrior;
			SpawnModBiomes = new int[1] { ModContent.GetInstance<MireSurfaceBiome>().Type };
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
				new FlavorTextBestiaryInfoElement("Ruthless predators residing in the mire, when they catch prey, they violently tear them to shreads with powerful jaws.")
			});
		}

		public override void FindFrame(int frameHeight)
		{
			NPC.spriteDirection = NPC.direction;
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BeastScales>(), 1, 1, 3));
		}


		public override void OnHitPlayer(Player player, int damage, bool crit) => player.AddBuff(BuffID.Bleeding, 180);

		public override void HitEffect(int hitDirection, double damage)
		{
			int amount = NPC.life <= 0 ? 10 : 2;

			for (int i = 0; i < amount; i++)
			{
				Dust dust = Dust.NewDustDirect(NPC.position, NPC.width + 4, NPC.height + 4, DustID.Blood, Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f));
				dust.velocity *= 0.8f;
			}
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return spawnInfo.player.InModBiome(ModContent.GetInstance<MireSurfaceBiome>()) && !Main.dayTime ? 1f : 0f;
		}
	}
}