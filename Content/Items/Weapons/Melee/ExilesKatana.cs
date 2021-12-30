using AAMod.Content.Items.Materials;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Weapons.Melee
{
    public class ExilesKatana : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Exile's Katana");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.useTurn = false;

            Item.damage = 14;
            Item.knockBack = 3f;

            Item.width = 62;
            Item.height = 66;

            Item.useTime = Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;

            Item.rare = ItemRarityID.Green;

            Item.UseSound = SoundID.Item1;

            Item.value = Item.sellPrice(silver: 2);
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
            => target.AddBuff(BuffID.Poisoned, 300);

        public override void OnHitPvp(Player player, Player target, int damage, bool crit)
            => target.AddBuff(BuffID.Poisoned, 300);

        public override void AddRecipes()
            => CreateRecipe()
            .AddIngredient(ModContent.ItemType<AbyssiumBar>(), 14)
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}
