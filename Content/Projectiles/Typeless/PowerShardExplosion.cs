using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Projectiles.Typeless
{
    class PowerShardExplosion : ModProjectile
    {
        public override string Texture => AAMod.InvisibleTexture;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Power Shard Explosion");
        }
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.aiStyle = 0;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 2;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 30;
        }
        public override void AI()
        {
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Flare, Projectile.velocity.X, Projectile.velocity.Y, 0, new Color(195, 73, 13));
            dust.scale *= 1f;
            dust.noGravity = true;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }
    }
}
