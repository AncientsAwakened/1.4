using AAMod.Content.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Weapons.Ranged
{
    public class Railjaw : ModItem
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Railjaw");
			Tooltip.SetDefault("Fires long ranged lava balls" + "\nUses gel as ammo");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

		public override void SetDefaults()
        {
            Item.noMelee = true;
            Item.autoReuse = false;

            Item.width = 48;
            Item.height = 26;

            Item.DamageType = DamageClass.Ranged;
            Item.damage = 58;
            Item.knockBack = 6f;

            Item.useTime = Item.useAnimation = 60;

            Item.shootSpeed = 16f;
            Item.shoot = ModContent.ProjectileType<RailjawLavaBlob>();

			Item.UseSound = SoundID.DD2_BetsyFireballShot;
			Item.useAmmo = AmmoID.Gel;

			Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Green;
        }

        public override Vector2? HoldoutOffset() 
            => new(4f, -1f);
    }
}
