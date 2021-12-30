using AAMod.Content.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Weapons.Magic
{
    public class HeatersMaw : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heaters Maw");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.noMelee = true;
            Item.autoReuse = true;

            Item.DamageType = DamageClass.Magic;
            Item.damage = 14;
            Item.mana = 6;

			Item.useTime = Item.useAnimation = 28;
            Item.useStyle = ItemUseStyleID.Shoot;

            Item.shootSpeed = 12f;
            Item.shoot = ModContent.ProjectileType<MawLavaBlast>();

            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.DD2_BetsyFireballShot;
        }

        public override Vector2? HoldoutOffset() 
            => new(4f, 0f);

        public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int numberProjectiles = Main.rand.Next(2, 5);

            position += Vector2.Normalize(velocity) * 45f;
            float rotation = MathHelper.ToRadians(30f);

            for (int i = 0; i < numberProjectiles; i++) {
                Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .8f;
                
                Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
            }

            return false;
        }
    }
}
