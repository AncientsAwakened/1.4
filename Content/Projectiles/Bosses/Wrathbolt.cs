using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Projectiles.Bosses
{
    class Wrathbolt : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.damage = 12;
            Projectile.hostile = true;
            Projectile.timeLeft = 300;
            Projectile.aiStyle = 0;
        }
        public override void AI()
        {
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Flare_Blue, Projectile.velocity.X, Projectile.velocity.Y);
            dust.scale *= 2f;
            dust.noGravity = true;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 180);
        }
    }
}