using AAMod.Content.Items.Materials.Energy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Weapons.Typeless
{
    class FragGrenade : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frag Grenage");
            Tooltip.SetDefault("You already know");
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Grenade);
            Item.buyPrice(gold: 2);
            Item.damage = 0;
            Item.shoot = ModContent.ProjectileType<FragmentGrenade>();
        }
    }
    class FragmentGrenade : ModProjectile
    {
        public override string Texture => "AAMod/Content/Items/Weapons/Typeless/FragGrenade";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frag Grenage");
        }
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Grenade);
            Projectile.timeLeft = 600;
        }
        public override void Kill(int timeLeft)
        {
            int num = Main.rand.Next(4);
            for (int i = 0; i < num; i++)
            {
                int random = Main.rand.Next(4);
                if (random == 0)
                {
                    Item.NewItem(Projectile.Center, 32, 32, ModContent.ItemType<MagicFragment>());
                }
                else if (random == 1)
                {
                    Item.NewItem(Projectile.Center, 32, 32, ModContent.ItemType<MeleeFragment>());
                }
                else if (random == 2)
                {
                    Item.NewItem(Projectile.Center, 32, 32, ModContent.ItemType<RangedFragment>());
                }
                else
                {
                    Item.NewItem(Projectile.Center, 32, 32, ModContent.ItemType<SummonFragment>());
                }
            }
        }
        public override void PostDraw(Color lightColor)
        {
            Asset<Texture2D> glowMask = ModContent.Request<Texture2D>("AAMod/Content/Items/Weapons/Typeless/FragGrenade_Glow");
            Main.EntitySpriteDraw(glowMask.Value, Projectile.position, null, lightColor, Projectile.rotation, new Vector2(0, 0), Projectile.scale, SpriteEffects.None, 0);
        }
    }
}
