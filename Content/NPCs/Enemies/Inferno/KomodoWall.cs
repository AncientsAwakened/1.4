using AAMod.Content.Dusts;
using AAMod.Content.Items.Materials;
using AAMod.Content.Items.Placeables.Banners;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.NPCs.Enemies.Inferno
{
	public class KomodoWall : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Komodo");
			NPCDebuffImmunityData debuffData = new()
			{
				SpecificallyImmuneTo = new int[]
			  {
					BuffID.OnFire,
			  }
			};
			NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);

			Main.npcFrameCount[NPC.type] = 4;
			NPCID.Sets.NPCBestiaryDrawModifiers value = new(0) { Hide = true };
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
		}

        public override void SetDefaults()
        {
            NPC.width = 52;
            NPC.height = 46;

            NPC.noGravity = true;
            NPC.lavaImmune = true;

			NPC.lifeMax = 100;
            NPC.damage = 20;
			NPC.defense = 14;
            NPC.aiStyle = -1;

            Banner = ModContent.NPCType<KomodoWalk>();
            BannerItem = ModContent.ItemType<KomodoBanner>();
            AnimationType = NPCID.JungleCreeperWall;

            NPC.knockBackResist = 0.2f;
			NPC.value = 200f;

			NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;

			DrawOffsetY = 30;
        }

        public override void AI()
        {
            float MaxSpeed = 2f;
            float Acceleration = 0.08f;

            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
                NPC.TargetClosest();

            Vector2 vector = Main.player[NPC.target].Center - NPC.Center;
			float distance = vector.Length();

			if (distance == 0f)
			{
				vector.X = NPC.velocity.X;
				vector.Y = NPC.velocity.Y;
			}
			else
			{
				distance = MaxSpeed / distance;
				vector.X *= distance;
				vector.Y *= distance;
			}

			if (Main.player[NPC.target].dead)
			{
				vector.X = NPC.direction * MaxSpeed / 2f;
				vector.Y = -MaxSpeed / 2f;
			}

			if (!Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, 
                Main.player[NPC.target].width, Main.player[NPC.target].height))
			{
				NPC.ai[0] += 1f;
				if (NPC.ai[0] > 0f)
                    NPC.velocity.Y += 0.023f;
                else
                    NPC.velocity.Y -= 0.023f;

                if (NPC.ai[0] < -100f || NPC.ai[0] > 100f)
                    NPC.velocity.X += 0.023f;
                else
                    NPC.velocity.X -= 0.023f;
                if (NPC.ai[0] > 200f)
                    NPC.ai[0] = -200f;
				
				NPC.velocity.X += vector.X * 0.007f;
				NPC.velocity.Y += vector.Y * 0.007f;

				NPC.rotation = vector.ToRotation() + 1.57f;

				if (NPC.velocity.X > 1.5f || NPC.velocity.X < -1.5f)
                    NPC.velocity.X *= 0.9f;
                if (NPC.velocity.Y > 1.5f || NPC.velocity.Y < -1.5f)
                    NPC.velocity.Y *= 0.9f;

                NPC.velocity.X = MathHelper.Clamp(NPC.velocity.X, -3f, 3f);
				NPC.velocity.Y = MathHelper.Clamp(NPC.velocity.Y, -3f, 3f);
			}
			else
			{
				if (NPC.velocity.X < vector.X)
				{
					NPC.velocity.X += Acceleration;
					if (NPC.velocity.X < 0f && vector.X > 0f)
                        NPC.velocity.X += Acceleration;
                }
				else if (NPC.velocity.X > vector.X)
				{
					NPC.velocity.X -= Acceleration;
					if (NPC.velocity.X > 0f && vector.X < 0f)
                        NPC.velocity.X -= Acceleration;
                }
				if (NPC.velocity.Y < vector.Y)
				{
					NPC.velocity.Y += Acceleration;
					if (NPC.velocity.Y < 0f && vector.Y > 0f)
                        NPC.velocity.Y += Acceleration;
					
				}
				else if (NPC.velocity.Y > vector.Y)
				{
					NPC.velocity.Y -= Acceleration;
					if (NPC.velocity.Y > 0f && vector.Y < 0f)
                        NPC.velocity.Y -= Acceleration;
                }

				NPC.rotation = vector.ToRotation() + 1.57f;
            }

			float num2 = 0.5f;

			if (NPC.collideX)
			{
				NPC.netUpdate = true;
				NPC.velocity.X = NPC.oldVelocity.X * -num2;

				if (NPC.direction == -1 && NPC.velocity.X > 0f && NPC.velocity.X < 2f)
                    NPC.velocity.X = 2f;

                if (NPC.direction == 1 && NPC.velocity.X < 0f && NPC.velocity.X > -2f)
                    NPC.velocity.X = -2f;
            }

			if (NPC.collideY)
			{
				NPC.netUpdate = true;
				NPC.velocity.Y = NPC.oldVelocity.Y * -num2;

				if (NPC.velocity.Y > 0f && NPC.velocity.Y < 1.5f)
                    NPC.velocity.Y = 2f;
				
				if (NPC.velocity.Y < 0f && NPC.velocity.Y > -1.5f)
                    NPC.velocity.Y = -2f;
            }

			if ((NPC.velocity.X > 0f && NPC.oldVelocity.X < 0f) || (NPC.velocity.X < 0f && NPC.oldVelocity.X > 0f) || (NPC.velocity.Y > 0f && 
                NPC.oldVelocity.Y < 0f || NPC.velocity.Y < 0f && NPC.oldVelocity.Y > 0f) && !NPC.justHit)
                NPC.netUpdate = true;
			
			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				int npcX = (int)NPC.Center.X / 16;
				int npcY = (int)NPC.Center.Y / 16;
				bool flag = false;

				for (int i = npcX - 1; i <= npcX + 1; i++)
                    for (int j = npcY - 1; j <= npcY + 1; j++)
					{
						if (Main.tile[i, j] == null)
                            break;
						
						if (Main.tile[i, j].wall > 0)
                            flag = true;
                    }
				
				if (!flag)
                    NPC.Transform(ModContent.NPCType<KomodoWalk>());
            }
        }

		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			Texture2D texture = ModContent.Request<Texture2D>(NPC.ModNPC.Texture + "_Glow").Value;
			SpriteEffects effects = NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos, NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, effects, 0);
			spriteBatch.Draw(texture, NPC.Center - screenPos, NPC.frame, Color.White, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, effects, 0);

			return false;
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DragonScale>(), 1, 1, 3));
		}

		public override void OnHitPlayer(Player player, int damage, bool crit) => player.AddBuff(BuffID.OnFire, 120);

		public override void HitEffect(int hitDirection, double damage)
        {
			int amount = NPC.life <= 0 ? 10 : 2;

			for (int i = 0; i < amount; i++)
			{
				Dust dust = Dust.NewDustDirect(NPC.position, NPC.width + 4, NPC.height + 4, ModContent.DustType<Incinerite>(), Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f));
				dust.velocity *= 0.8f;
			}
			if (NPC.life <= 0)
			{
				for (int i = 0; i < 4; i++)
				{
					Gore.NewGore(NPC.position, NPC.velocity, ModContent.Find<ModGore>("AAMod/KomodoGore" + (i + 1)).Type, 1);
				}
			}
		}
    }
}
