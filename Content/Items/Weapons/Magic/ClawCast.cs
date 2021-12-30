using AAMod.Content.Items.Materials;
using AAMod.Content.Projectiles.Magic;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Weapons.Magic
{
    public class ClawCast : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Claw Caster");
			Tooltip.SetDefault("'Summons phantom claws to slash your enemies!'");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

	    public override void SetDefaults()
		{
			Item.autoReuse = true;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Magic;
			Item.damage = 15;
			Item.mana = 3;
			Item.knockBack = 4f;
			Item.width = Item.height = 56;
			Item.useTime = 15;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.shootSpeed = 12f;
			Item.shoot = ModContent.ProjectileType<PhantomClaw1>();
			Item.UseSound = SoundID.Item34;
            Item.rare = ItemRarityID.Orange;
			Item.value = Item.buyPrice(gold: 1);
		}
		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{

			float angle = Main.rand.NextFloat(MathHelper.PiOver4, -MathHelper.Pi - MathHelper.PiOver4);
			Vector2 spawnPlace = (type == ModContent.ProjectileType<PhantomClaw1>()) ? Vector2.Normalize(new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle))) * 100f : Vector2.Zero;
			if (Collision.CanHit(position, 0, 0, position + spawnPlace, 0, 0))
				position += spawnPlace;

			for (float num2 = 0.0f; (double)num2 < 10; ++num2)
			{
				int dustIndex = Dust.NewDust(position, 2, 2, DustID.Wraith, 0f, 0f, 0, default, 1f);
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].velocity = Vector2.Normalize(spawnPlace.RotatedBy(Main.rand.NextFloat(MathHelper.TwoPi))) * 1.6f;
			}
		}

		public override void AddRecipes()
			=> CreateRecipe()
			.AddIngredient(ModContent.ItemType<IncineriteBar>(), 12)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
