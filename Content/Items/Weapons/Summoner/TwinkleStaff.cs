using AAMod.Content.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.Content.Buffs.Minions;
using Terraria;
using AAMod.Content.Projectiles.Minions;

namespace AAMod.Content.Items.Weapons.Summoner
{
	public class TwinkleStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Twinkle Staff");
			Tooltip.SetDefault("Summons a little star to fight for you");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;
			ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
		}

		public override void SetDefaults()
		{
			Item.damage = 11;
			Item.knockBack = 3f;
			Item.mana = 10;
			Item.width = 56;
			Item.height = 56;
			Item.useTime = 36;
			Item.useAnimation = 36;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.value = Item.sellPrice(silver: 10);
			Item.rare = ItemRarityID.Cyan;
			Item.UseSound = SoundID.Item44;

			Item.noMelee = true;
			Item.DamageType = DamageClass.Summon;
			Item.buffType = ModContent.BuffType<TwinkleStarBuff>();
			Item.shoot = ModContent.ProjectileType<TwinkleStar>();
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			position = Main.MouseWorld;
		}

		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			player.AddBuff(Item.buffType, 2);

			var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
			projectile.originalDamage = Item.damage;

			return false;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.FallenStar, 5).AddRecipeGroup("IronBar", 12).AddTile(TileID.Anvils).Register();
		}
	}
}