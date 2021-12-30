using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.Content.Projectiles.Ranged;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;

namespace AAMod.Content.Items.Weapons.Ranged
{
	public class DragonFlamebow : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dragon Flamebow");
			Tooltip.SetDefault("Converts wooden arrows into dragon arrows");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.DamageType = DamageClass.Ranged;
			Item.noMelee = true;
			Item.autoReuse = false;

			Item.damage = 20;

			Item.useTime = Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;

            
			Item.useAmmo = AmmoID.Arrow;
			Item.shoot = ProjectileID.WoodenArrowFriendly;
			Item.shootSpeed = 10f;

			Item.rare = ItemRarityID.Green;

			Item.UseSound = SoundID.Item5;
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			if (type == ProjectileID.WoodenArrowFriendly)
			{
				type = ModContent.ProjectileType<DragonArrow>();
			}
		}
	}
}
