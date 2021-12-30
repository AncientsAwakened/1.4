using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Ammo
{
    public class DragonArrow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Arrow");

            Tooltip.SetDefault("Upon hitting an enemy or tile, this arrow will split into a fireball which sets " +
                               "enemies on fire for 3 seconds");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
        }

        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Ranged;
            Item.consumable = true;

            Item.maxStack = 999;
            Item.damage = 8;

            Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.DragonArrow>();
            Item.shootSpeed = 4f;

            Item.ammo = AmmoID.Arrow;
            Item.rare = ItemRarityID.Green;
        }
    }
}
