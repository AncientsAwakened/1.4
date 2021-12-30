using AAMod.Content.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Weapons.Magic
{
    public class DragonsBreath : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dragons Breath");
			Tooltip.SetDefault("'Turn them into ash!'");

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
			Item.shoot = ProjectileID.Flames;

			Item.UseSound = SoundID.Item34;
            Item.rare = ItemRarityID.Orange;

			Item.value = Item.buyPrice(gold: 1);
		}

		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			int numberProjectiles = Main.rand.Next(2, 4);
			float rotation = MathHelper.ToRadians(5f);

			position += Vector2.Normalize(velocity) * -25f;

			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 0.8f;		
				Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
			}

			return false;
		}

		public override void AddRecipes()
			=> CreateRecipe()
			.AddIngredient(ModContent.ItemType<IncineriteBar>(), 12)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
