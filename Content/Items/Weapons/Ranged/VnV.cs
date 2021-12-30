using AAMod.Content.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Weapons.Ranged
{
	public class VnV : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Vice & Vehemence");

			Tooltip.SetDefault("'WARNING: The devils may cry.'");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.width = 46; 
			Item.height = 38;
			Item.scale = 1f;
			Item.rare = ItemRarityID.Blue;

			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.autoReuse = true;
			Item.UseSound = SoundID.Item11;

			Item.DamageType = DamageClass.Ranged;
			Item.damage = 20;
			Item.knockBack = 5f;
			Item.noMelee = true;

			Item.shoot = ModContent.ProjectileType<IncineriteFireball>();
			Item.shootSpeed = 12f;
		}

		public override Vector2? HoldoutOffset() {
			return new Vector2(1f, -5f);
		}

	}
}
