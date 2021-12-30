using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.NPCs.Bosses.Grips
{
    class InfernoGripDefeat : ModNPC 
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Inferno Grip");

            NPCID.Sets.NPCBestiaryDrawModifiers value = new(0) { Hide = true };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }
        public override void SetDefaults()
        {
            NPC.width = 80;
            NPC.height = 110;
            NPC.damage = 0;
            NPC.defense = 0;
            NPC.immortal = true;
            NPC.dontTakeDamage = true;
            NPC.lifeMax = 1;
            NPC.npcSlots = 0f;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.value = Item.buyPrice(gold: 5);
            NPC.SpawnWithHigherTime(30);
            NPC.aiStyle = -1;           
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>(NPC.ModNPC.Texture + "_Glow").Value;
            SpriteEffects effects = NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos, NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, effects, 0);
            spriteBatch.Draw(texture, NPC.Center - screenPos, NPC.frame, Color.White, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, effects, 0);

            return false;
        }
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
            {
                NPC.velocity.X = 0f;
                NPC.velocity.Y -= 0.15f;
                NPC.EncourageDespawn(10);
                return;
            }
        }
    }
}