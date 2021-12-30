using AAMod.Common.Globals.NPCs;
using AAMod.Content.Biomes.Inferno;
using AAMod.Content.Dusts;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.NPCs.Enemies.Inferno
{
    public class OilSlime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Oil Slime");

            Main.npcFrameCount[NPC.type] = 2;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 40;
            NPC.damage = 10;
            NPC.defense = 2;

            NPC.width = 24;
            NPC.height = 18;

            NPC.alpha = 50;
            NPC.value = Item.sellPrice(copper: 20);

			NPC.aiStyle = NPCAIStyleID.Slime;
            AIType = NPCID.BlueSlime;

            AnimationType = NPCID.BlueSlime;

            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;

            NPC.GetGlobalNPC<DrawNPCCentered>().Enabled = true;
            SpawnModBiomes = new int[1] { ModContent.GetInstance<InfernoSurfaceBiome>().Type };
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
                new FlavorTextBestiaryInfoElement("Okay, now this is just ridiculous. A slime made of oil? In the Inferno!? How has it survived for so long!?")
            });
        }

        public override void AI()
        {
            if (NPC.HasBuff(BuffID.OnFire) || NPC.HasBuff(BuffID.Burning) || NPC.lavaWet)
            {
                if (!NPC.HasBuff(BuffID.OnFire))
                {
                    NPC.AddBuff(BuffID.OnFire, 180);
                }
            }
            else
            {
                if (Main.rand.NextBool(5))
                {
                    Dust dust = Dust.NewDustDirect(NPC.Center, 0, 0, ModContent.DustType<Oil>(), Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f));
                    dust.velocity *= 0.8f;
                }
            }

            float rotation = NPC.velocity.X != 0f ? NPC.velocity.X * 0.1f : 0f;
            NPC.rotation = Utils.AngleLerp(NPC.rotation, rotation, 0.1f);
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            int amount = NPC.life <= 0 ? 10 : 2;

            for (int i = 0; i < amount; i++)
            {
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width + 4, NPC.height + 4, ModContent.DustType<Oil>(), Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f));
                dust.velocity *= 0.8f;
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.player.InModBiome(ModContent.GetInstance<InfernoSurfaceBiome>()) && Main.dayTime ? 0.2f : 0f;
        }
    }
}
