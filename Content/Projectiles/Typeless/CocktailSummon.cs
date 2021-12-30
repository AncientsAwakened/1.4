using AAMod.Content.NPCs.Bosses.Grips;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAMod.Content.Projectiles.Typeless
{
    class CocktailSummon : ModProjectile
    {
        public override void SetDefaults()
		{
			Projectile.ignoreWater = false;
			Projectile.tileCollide = true;
			Projectile.width = 14; 
			Projectile.height = 46;
            Projectile.penetrate = 1;
		}
        public override void AI()
        {
			Projectile.velocity.Y += 0.2f;
            Projectile.rotation += 0.04f * (float)Projectile.direction;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (!Main.dayTime && !NPC.AnyNPCs(ModContent.NPCType<MireGrip>()) && !NPC.AnyNPCs(ModContent.NPCType<InfernoGrip>()) && !NPC.AnyNPCs(ModContent.NPCType<OffGuardClap>()) && !NPC.AnyNPCs(ModContent.NPCType<FistBump>()))
            {
                SoundEngine.PlaySound(SoundID.Shatter, Projectile.position);
                SoundEngine.PlaySound(SoundID.Roar, Projectile.position, 0);
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    Main.NewText("The Grips of Chaos have awoken!", 175, 75);
                }
                else if (Main.netMode == NetmodeID.Server)
                {
                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("The Grips of Chaos have awoken!"), new Color(175, 75, 255));
                }
                NPC.NewNPC((int)Main.player[Projectile.owner].Center.X + 1040, (int)Main.player[Projectile.owner].Center.Y, ModContent.NPCType<MireGrip>());
            }
            else
            {
                SoundEngine.PlaySound(SoundID.Shatter, Projectile.position);
            }
            return true;
        }
    }
}
