using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Weapons.Melee
{
	public class BleedingBone : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bleeding Bone");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.DamageType = DamageClass.Melee;
			Item.autoReuse = false;

			Item.damage = 15;
			Item.knockBack = 5f;

			Item.useTime = Item.useAnimation = 23;
			Item.useStyle = ItemUseStyleID.Swing;

			Item.rare = ItemRarityID.Blue;

			Item.UseSound = SoundID.Item1;
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.NextBool(4))
			{
				Dust dust = Dust.NewDustDirect(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Blood);
				dust.noGravity = true;
				dust.fadeIn = 1f;
				dust.scale = Main.rand.NextFloat(0.6f, 1f);
			}
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit) 
			=> target.AddBuff(BuffID.Poisoned, 180);

		public override void OnHitPvp(Player player, Player target, int damage, bool crit) 
			=> target.AddBuff(BuffID.Poisoned, 180);
	}
}
