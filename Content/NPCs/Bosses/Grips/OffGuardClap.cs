﻿using AAMod.Content.Items.TreasureBags;
using AAMod.Content.Items.Vanity.BossMasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.NPCs.Bosses.Grips
{
    class OffGuardClap : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Grips of Chaos");
            NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
            {
                SpecificallyImmuneTo = new int[]
               {
                    BuffID.Poisoned,
                    BuffID.OnFire,
                    BuffID.Confused
               }
            };
            NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);
            NPCID.Sets.NPCBestiaryDrawModifiers value = new(0) { Hide = true };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }
        public override void SetDefaults()
        {
            NPC.width = 142;
            NPC.height = 140;
            NPC.damage = 20;
            NPC.defense = 0;
            NPC.lifeMax = 2500;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.value = Item.buyPrice(gold: 5);
            NPC.SpawnWithHigherTime(30);
            NPC.boss = true;
            BossBag = ModContent.ItemType<GripsTreasureBag>();
            NPC.npcSlots = 10f;
            NPC.aiStyle = -1;
            if (!Main.dedServ)
            {
                Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Grips");
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(BossBag));

            LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<MireGripMask>(), 7));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<InfernoGripMask>(), 7));

            var parameters = new DropOneByOne.Parameters()
            {
                ChanceNumerator = 1,
                ChanceDenominator = 1,
                MinimumStackPerChunkBase = 1,
                MaximumStackPerChunkBase = 1,
                MinimumItemDropsCount = 12,
                MaximumItemDropsCount = 15,
            };

            npcLoot.Add(notExpertRule);
        }
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>(NPC.ModNPC.Texture + "_Glow").Value;
            SpriteEffects effects = NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Vector2 origin = NPC.frame.Size() / 2f + new Vector2(0f, NPC.ModNPC.DrawOffsetY);
            Vector2 position = NPC.Center - screenPos + new Vector2(0f, NPC.gfxOffY);
            Main.EntitySpriteDraw(texture, position, NPC.frame, Color.White, NPC.rotation, origin, NPC.scale, effects, 0);
        }
        private int AnimTempTime = 0;
        private bool check = false;
        private bool start = true;
        public ref float MireHP => ref NPC.ai[0];
        public ref float InfernoHP => ref NPC.ai[1];
        public override void AI()
        {
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest();
            }
            Player player = Main.player[NPC.target];
            if (player.dead)
            {
                NPC.velocity.Y -= 0.12f;
                NPC.EncourageDespawn(10);
                return;
            }
            NPC.life = (int)MireHP + (int)InfernoHP;
            if (start)
            {
                SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Custom/GripClap").WithVolume(3.0f), NPC.Center);
                start = false;
            }
            AnimTempTime++;
            if (AnimTempTime >= 90) // replace with animation frame changes
            {
                NPC.NewNPC((int)NPC.position.X + (NPC.width / 2), (int)NPC.Center.Y + (NPC.height / 2), ModContent.NPCType<MireGrip>(), 0, 1f, MireHP, 1f, InfernoHP);
                NPC.active = false;
                NPC.life = 0;
                return;
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            MireHP -= (int)damage * 11 / 25;
            InfernoHP -= (int)damage * 14 / 25;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            check = true;
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            if (check)
            {
                return false;
            }
            return true;
        }
        public override void OnKill()
        {
            NPC.NewNPC((int)NPC.Center.X + 60, (int)NPC.Center.Y, ModContent.NPCType<MireGripDefeat>());
            NPC.NewNPC((int)NPC.Center.X - 60, (int)NPC.Center.Y, ModContent.NPCType<InfernoGripDefeat>());
        }
    }
}