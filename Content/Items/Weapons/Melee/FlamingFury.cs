using AAMod.Content.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Weapons.Melee
{
	public class FlamingFury : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flaming Fury");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.DamageType = DamageClass.Melee;
			// item.autoReuse = false;

			Item.damage = 20;
			Item.knockBack = 2f;

			Item.useTime = Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Swing;

			Item.shoot = ModContent.ProjectileType<IncineriteFireball>();
			Item.shootSpeed = 12f;

			Item.rare = ItemRarityID.Green;

			Item.UseSound = SoundID.Item1;
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.NextBool(4))
			{
				var dust = Dust.NewDustDirect(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Flare);
				dust.noGravity = true;
				dust.fadeIn = 1f;
				dust.scale = Main.rand.NextFloat(0.6f, 1f);
			}
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit) => target.AddBuff(BuffID.OnFire, 180);

		public override void OnHitPvp(Player player, Player target, int damage, bool crit) => target.AddBuff(BuffID.OnFire, 180);
	}
}
