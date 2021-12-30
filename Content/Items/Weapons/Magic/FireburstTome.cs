using AAMod.Content.Projectiles.Typeless;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Weapons.Magic
{
	public class FireburstTome : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fireburst Tomb");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.DamageType = DamageClass.Magic;
			Item.autoReuse = true;

			Item.damage = 10;
			Item.mana = 4;

			Item.width = 30;
			Item.height = 34;

			Item.useTime = Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;

			Item.shoot = ModContent.ProjectileType<IncineriteFireball>();
			Item.shootSpeed = 8f;

			Item.UseSound = SoundID.Item43;

			Item.rare = ItemRarityID.Green;	
		}
	}
}
