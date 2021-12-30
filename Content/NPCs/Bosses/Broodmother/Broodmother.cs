using AAMod.Content.Biomes.Inferno;
using AAMod.Content.Items.Materials;
using AAMod.Content.Items.Placeables.Trophies;
using AAMod.Content.Items.TreasureBags;
using AAMod.Content.Items.Vanity.BossMasks;
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

namespace AAMod.Content.NPCs.Bosses.Broodmother
{
    [AutoloadBossHead]
    public class Broodmother : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Broodmother");
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
            {
                SpecificallyImmuneTo = new int[]
                {
                    BuffID.Confused,
                    BuffID.OnFire
                }
            };
            NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                CustomTexturePath = "ExampleMod/Assets/Textures/Bestiary/Broodmother_Preview",
                PortraitScale = 0.6f,
                PortraitPositionYOverride = 0f,
            };
            Main.npcFrameCount[Type] = 1;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 4000;
            NPC.defense = 10;
            NPC.damage = 30;
            NPC.width = 426;
            NPC.height = 242;
            NPC.lavaImmune = true;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.aiStyle = 120;
            NPC.boss = true;
            NPC.npcSlots = 10f;
            BossBag = ModContent.ItemType<BroodmotherTreasureBag>();
            NPC.HitSound = SoundID.NPCHit56;
            NPC.DeathSound = SoundID.NPCDeath1;
            if (!Main.dedServ)
            {
                Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Broodmother");
            }
            SpawnModBiomes = new int[1] { ModContent.GetInstance<InfernoUndergroundBiome>().Type };
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
                new FlavorTextBestiaryInfoElement("The Queen of the Brood Hives, literally. This towering creature spends most of her time lumbering around the Inferno in defense of her hives, though when she's not savagely protecting her progeny, she fancies a little nap.")
            });
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(BossBag));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BroodmotherTrophy>(), 10));

            LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<BroodmotherMask>(), 7));

            notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1, ModContent.ItemType<Railjaw>(), ModContent.ItemType<HeatersMaw>()));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ScorchedScale>(), 1, 15, 30));

            npcLoot.Add(notExpertRule);
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 2f;
            return null;
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
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
