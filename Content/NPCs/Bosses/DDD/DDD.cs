using AAMod.Content.Biomes.Void;
using AAMod.Content.Items.Placeables.Trophies;
using AAMod.Content.Items.TreasureBags;
using AAMod.Utilities.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.NPCs.Bosses.DDD
{
    [AutoloadBossHead]
    public class DDD : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Drone Dispensary Delta");

            NPCID.Sets.TrailCacheLength[NPC.type] = 8;
            NPCID.Sets.TrailingMode[NPC.type] = 3;

        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 5000;
            NPC.defense = 20;
            NPC.width = NPC.height = 64;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.aiStyle = 114;
            AIType = 22;
            NPC.boss = true;
            NPC.npcSlots = 10f;
            BossBag = ModContent.ItemType<DDDTreasureBag>();
            NPC.HitSound = SoundID.NPCHit42;
            NPC.DeathSound = SoundID.DD2_ExplosiveTrapExplode;
            SpawnModBiomes = new int[1] { ModContent.GetInstance<VoidBiome>().Type };
            if (!Main.dedServ)
            {
                Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/DDD");
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                new FlavorTextBestiaryInfoElement("01000001 00100000 01110100 01101000 01101111 01110101 01110011 01100001 01101110 01100100 00100000 01100100 01100101 01100001 01110100 01101000 01110011 00100000 01100110 01101111 01110010 00100000 01110100 01101000 01100101 00100000 01101100 01101001 01100110 01100101 00100000 01101111 01100110 00100000 01101111 01101110 01100101 00101110 00100000 01001001 01110011 00100000 01101001 01110100 00100000 01110010 01100101 01100001 01101100 01101100 01111001 00100000 01110111 01101111 01110010 01110100 01101000 00100000 01110100 01101000 01101001 01110011 00100000 01101101 01110101 01100011 01101000 00111111")
            });
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(BossBag));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DDDTrophy>(), 10));

            LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());

            notExpertRule.OnSuccess(ItemDropRule.Common(ItemID.IronBar));

            npcLoot.Add(notExpertRule);
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.HealingPotion;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 2f;
            return null;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            NPC.DrawNPCAfterimageCentered(spriteBatch, drawColor, null, 0.8f, 0.2f, 2);

            DrawHovers(spriteBatch);

            return NPC.DrawNPCCentered(spriteBatch, drawColor);
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D glow = ModContent.Request<Texture2D>("AAMod/Content/NPCs/Bosses/DDD/DDD_Glow").Value;

            NPC.DrawNPCCentered(spriteBatch, Color.White, glow);
        }

        private readonly List<Hover> hovers = new();

        private void DrawHovers(SpriteBatch spriteBatch)
        {
            NPC.ai[0]++;

            if (NPC.ai[0] > 10)
            {
                hovers.Add(new Hover(NPC.Center - new Vector2(65f, 10f), NPC.rotation));
                hovers.Add(new Hover(NPC.Center + new Vector2(65f, -10f), NPC.rotation));

                NPC.ai[0] = 0;
            }
            
            for (int i = 0; i < hovers.Count; i++)
            {
                Hover hover = hovers[i];

                hover.Update();
                hover.Draw(spriteBatch);

                hovers[i] = hover;

                if (hover.Alpha <= 0f || hover.Scale <= 0f)
                {
                    hovers.RemoveAt(i);
                }
            }
        }

        private struct Hover
        {
            public Vector2 Position;

            public Vector2 Velocity;

            public float Scale;

            public float Alpha;

            public float Rotation;

            public Hover(Vector2 position, float rotation = 0f)
            {
                Position = position;
                Velocity = new Vector2(0f, 12f);

                Rotation = rotation;
                Scale = 1f;
                Alpha = 1f;
            }

            public void Update()
            {
                Position += Velocity;
                Velocity *= 0.9f;

                Scale -= 0.01f;
                Alpha = Scale;
            }

            public void Draw(SpriteBatch spriteBatch)
            {
                spriteBatch.End();

                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, default, default, default, default, Main.GameViewMatrix.TransformationMatrix);

                Texture2D hover = ModContent.Request<Texture2D>("AAMod/Content/NPCs/Bosses/DDD/DDD_Hover").Value;

                spriteBatch.Draw(hover, Position.ToDrawPosition(), null, new Color(241, 171, 110) * Alpha, Rotation, hover.GetCenter(), Scale, SpriteEffects.None, 0f);

                spriteBatch.End();

                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, default, default, default, default, Main.GameViewMatrix.TransformationMatrix);
            }
        }
    }
}
