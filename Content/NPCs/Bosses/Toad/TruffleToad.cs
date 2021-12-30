using AAMod.Content.Items.Placeables.Trophies;
using AAMod.Content.Items.TreasureBags;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.NPCs.Bosses.Toad
{
    [AutoloadBossHead]
    public class TruffleToad : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Truffle Toad");
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
            {
                SpecificallyImmuneTo = new int[]
                {
                    BuffID.Confused
                }
            };
            NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);
            Main.npcFrameCount[Type] = 25;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 3400;
            NPC.defense = 6;
            NPC.damage = 24;
            NPC.width = 98;
            NPC.height = 82;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = 1;
            AIType = NPCID.Pinky;
            NPC.boss = true;
            NPC.npcSlots = 10f;
            BossBag = ModContent.ItemType<ToadTreasureBag>();
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            if (!Main.dedServ)
            {
                Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Toad");
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundMushroom,
                new FlavorTextBestiaryInfoElement("This frog has grown large from its symbiotic relationship with these glowing mushrooms, seen carrying them on its back, and using their spores to poison its prey.")
            });
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(BossBag));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ToadTrophy>(), 10));

            LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());

            notExpertRule.OnSuccess(ItemDropRule.Common(ItemID.GlowingMushroom));

            npcLoot.Add(notExpertRule);
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 2f;
            return null;
        }

        private int frame;
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            if (++NPC.frameCounter > 4)
            {
                frame++;
                NPC.frameCounter = 0;
                if (frame > 3)
                {
                    frame = 0;
                }
            }
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
    }
}
