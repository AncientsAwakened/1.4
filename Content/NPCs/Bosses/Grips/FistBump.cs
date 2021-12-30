using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.NPCs.Bosses.Grips
{
    class FistBump : ModNPC 
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Grips of Chaos");

            NPCID.Sets.NPCBestiaryDrawModifiers value = new(0) { Hide = true };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }
        public override void SetDefaults()
        {
            NPC.width = 150;
            NPC.height = 62;
            NPC.damage = 0;
            NPC.defense = 0;
            NPC.immortal = true;
            NPC.lifeMax = 1;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.value = Item.buyPrice(gold: 5);
            NPC.SpawnWithHigherTime(30);
            NPC.boss = true;
            NPC.npcSlots = 5f;
            NPC.aiStyle = -1;
            if (!Main.dedServ)
            {
                Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Grips");
            }
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
        public bool Initialize
        {
            get => NPC.ai[0] == 0f;
            set => NPC.ai[0] = value ? 0f : 1f;
        }
        public override void AI()
        {
            if (Initialize)
            {
                NPC.velocity = Vector2.Zero;
                Initialize = false;
            }
            AnimTempTime++;
            if (AnimTempTime >= 90) // replace with animation frame changes
            {
                NPC.velocity.X = 0f;
                NPC.velocity.Y -= 0.12f;
                NPC.EncourageDespawn(10);
                return;
            }
        }
    }
}