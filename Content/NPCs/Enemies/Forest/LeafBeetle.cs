using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace AAMod.Content.NPCs.Enemies.Forest
{
    public class LeafBeetle : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Leaf Beetle");
            Main.npcFrameCount[NPC.type] = 2;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 30;
            NPC.damage = 10;
            NPC.defense = 5;

            NPC.width = 16;
            NPC.height = 10;

            NPC.value = Item.sellPrice(copper: 20);

            NPC.aiStyle = 3;

            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath16;

            AIType = NPCID.CyanBeetle;
            AnimationType = NPCID.CyanBeetle;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
               BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
                new FlavorTextBestiaryInfoElement("chonker2.")
            });
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            int amount = NPC.life <= 0 ? 10 : 2;

            for (int i = 0; i < amount; i++)
            {
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width + 4, NPC.height + 4, DustID.GreenBlood, Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f));
                dust.velocity *= 0.8f;
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            float baseChance = SpawnCondition.OverworldDay.Chance;
            float multiplier = Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type == TileID.Grass ? 0.25f : 0f;

            return baseChance * multiplier;
        }

    }
}
