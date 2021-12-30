using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Tools.Incinerite
{
    public class Stonebreaker : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stonebreaker");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.useTurn = true;

            Item.useTime = 12;
            Item.useAnimation = 17;
            Item.damage = 15;

            Item.knockBack = 3f;

            Item.pick = 100;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.rare = ItemRarityID.Green;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(4))
            {
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<Dusts.Incinerite>());
            }
        }

        public override void AddRecipes()
            => CreateRecipe()
            .AddIngredient(ModContent.ItemType<DragonDigger>())
            .AddIngredient(ModContent.ItemType<Materials.IncineriteBar>(), 20)
            .AddIngredient(ItemID.NightmarePickaxe).AddIngredient(ItemID.MoltenPickaxe)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
