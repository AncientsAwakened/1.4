using Microsoft.Xna.Framework;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Weapons.Ranged
{
	public class RustySlingshot : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rusty Slingshot");
			Tooltip.SetDefault("Uses spiky balls as ammo");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
        {
			Item.DamageType = DamageClass.Ranged;
			Item.noMelee = true;
            Item.autoReuse = false;

            Item.useTime = 30;
            Item.useAnimation = 30;

            Item.damage = 20;
            Item.knockBack = 3f;

			Item.useAmmo = ItemID.SpikyBall;
			Item.shoot = ProjectileID.SpikyBall;
			Item.shootSpeed = 11f;
			Item.UseSound = SoundID.Item5;

			Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Green;
		}

        public override Vector2? HoldoutOffset() => new Vector2(0f, 0f);
    }
}
