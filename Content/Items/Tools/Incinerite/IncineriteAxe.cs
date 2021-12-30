using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Tools.Incinerite
{
    public class IncineriteAxe : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Incinerite Axe");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.useTurn = true;

            Item.useTime = 14;
            Item.useAnimation = 30;
            Item.damage = 10;
            
            Item.knockBack = 3f;

            Item.axe = 16;

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
            .AddIngredient(ModContent.ItemType<Materials.IncineriteBar>(), 10)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
