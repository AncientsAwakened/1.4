using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Weapons.Melee
{
    public class DragonClaws : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dragon Claws");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.DamageType = DamageClass.Melee;
			Item.autoReuse = true;
			Item.useTurn = true;

			Item.damage = 25;
			Item.knockBack = 2f;

			Item.width = 26;
			Item.height = 24;

			Item.useTime = Item.useAnimation = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.UseSound = SoundID.Item1;

			Item.rare = ItemRarityID.Green;
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.NextBool(4))
			{
				Dust dust = Dust.NewDustDirect(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Flare);
				dust.noGravity = true;
				dust.fadeIn = 1f;
				dust.scale = Main.rand.NextFloat(0.6f, 1f);
			}
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit) 
			=> target.AddBuff(BuffID.OnFire, 180);

		public override void OnHitPvp(Player player, Player target, int damage, bool crit) 
			=> target.AddBuff(BuffID.OnFire, 180);
	}
}
