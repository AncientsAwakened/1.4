using AAMod.Content.Biomes.RedMush;
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

namespace AAMod.Content.NPCs.Bosses.Monarch
{
    [AutoloadBossHead]
    public class MushroomMonarch : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mushroom Monarch");
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
            NPC.lifeMax = 400;
            NPC.defense = 4;
            NPC.damage = 30;
            NPC.width = 46;
            NPC.height = 80;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = 8;
            AIType = NPCID.FireImp;
            NPC.boss = true;
            NPC.npcSlots = 10f;
            BossBag = ModContent.ItemType<MonarchTreasureBag>();
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            if (!Main.dedServ)
            {
                Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Monarch");
            }
            SpawnModBiomes = new int[1] { ModContent.GetInstance<RedMushSurfaceBiome>().Type };
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.5f * bossLifeScale);
            NPC.damage = (int)(NPC.damage * 0.8f);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                new FlavorTextBestiaryInfoElement("Once a normal man, he was made king of the mushrooms when they infected his body, often seen protecting them from those wanting to destroy them. He protects his domain viciously . . . or maybe someone elses?")
            });
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(BossBag));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MonarchTrophy>(), 10));

            LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());

            notExpertRule.OnSuccess(ItemDropRule.Common(ItemID.Mushroom));

            npcLoot.Add(notExpertRule);
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 2f;
            return null;
        }

        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
                damage *= 0.2;
            return true;
        }
    }
}
