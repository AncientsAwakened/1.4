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
    class Furyflame : ModProjectile
    {
        public override string Texture => AAMod.InvisibleTexture;
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.damage = 8;
            Projectile.hostile = true;
            Projectile.aiStyle = 0;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 2;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 90;
        }
        public override void AI()
        {
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Flare, Projectile.velocity.X, Projectile.velocity.Y);
            dust.scale *= 5f;
            dust.noGravity = true;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }
    }
}