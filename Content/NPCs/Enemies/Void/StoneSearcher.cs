using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Bestiary;
using AAMod.Content.Biomes.Void;
using Terraria.GameContent;

namespace AAMod.Content.NPCs.Enemies.Void
{
    public class StoneSearcher : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stone Searcher");
        }

        public override void SetDefaults()
        {
           
            
            NPC.width = 19;
            NPC.height = 15;

            NPC.lifeMax = 120;
            NPC.defense = 5;
            NPC.damage = 20;

            NPC.knockBackResist = 0.1f;
			NPC.value = 200f;

            NPC.aiStyle = 10;
            AIType = NPCID.CursedSkull;
            NPC.noGravity = true;

            NPC.HitSound = SoundID.NPCHit42;
            NPC.DeathSound = SoundID.DD2_ExplosiveTrapExplode;

            NPC.noTileCollide = false;
            SpawnModBiomes = new int[1] { ModContent.GetInstance<VoidBiome>().Type };
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
                new FlavorTextBestiaryInfoElement("Odd contraptions patrolling about the void, they do nothing but stare. But who's watching...?")
            });
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>(NPC.ModNPC.Texture + "_Glow").Value;
            SpriteEffects effects = NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos, NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, effects, 0);
            spriteBatch.Draw(texture, NPC.Center - screenPos, NPC.frame, Color.White, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, effects, 0);

            return false;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            int amount = NPC.life <= 0 ? 10 : 2;

            for (int i = 0; i < amount; i++)
            {
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width + 4, NPC.height + 4, DustID.OrangeTorch, Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f));
                dust.noGravity = true;
                dust.velocity *= 0.8f;
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.player.InModBiome(ModContent.GetInstance<VoidBiome>()) ? 1f : 0f;
        }
    }
}
