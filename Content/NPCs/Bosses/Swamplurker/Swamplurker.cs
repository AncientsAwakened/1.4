using AAMod.Content.Biomes.Mire;
using AAMod.Content.Items.Materials;
using AAMod.Content.Items.Placeables.Trophies;
using AAMod.Content.Items.TreasureBags;
using AAMod.Content.Items.Weapons.Magic;
using AAMod.Content.Items.Weapons.Ranged;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.NPCs.Bosses.Swamplurker
{
    [AutoloadBossHead]
    public class Swamplurker : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Swamplurker");
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
            {
                SpecificallyImmuneTo = new int[]
                {
                    BuffID.Confused,
                    BuffID.Poisoned
                }
            };
            NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);
            Main.npcFrameCount[Type] = 1;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 4500;
            NPC.defense = 10;
            NPC.damage = 40;
            NPC.width = 210;
            NPC.height = 160;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = 123;
            NPC.boss = true;
            NPC.npcSlots = 10f;
            BossBag = ModContent.ItemType<SwamplurkerTreasureBag>();
            NPC.HitSound = SoundID.NPCHit6;
            NPC.DeathSound = SoundID.NPCDeath1;
            if (!Main.dedServ)
            {
                Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Swamplurker");
            }
            SpawnModBiomes = new int[1] { ModContent.GetInstance<MireUndergroundBiome>().Type };
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.6f * bossLifeScale);
            NPC.damage = (int)(NPC.damage * 0.8f);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                new FlavorTextBestiaryInfoElement("The Queen of all Hydras . . . figuratively. She lives in a cave made of bones, meant to drive off predators wishing to poach her eggs, if the stench wasn't good enough. It seems the natives used to worship her ancestors, though they have since then mysteriously vanished.")
            });
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(BossBag));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SwamplurkerTrophy>(), 10));

            LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<BeastScales>(), 30));

            npcLoot.Add(notExpertRule);
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 2f;
            return null;
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
