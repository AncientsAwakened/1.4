using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Armor.Sparkle
{
    [AutoloadEquip(EquipType.Legs)]
    public class SparkleGreaves : ModItem
    {
		public override string Texture => AAMod.PlaceholderTexture;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sparkle Greaves");
			Tooltip.SetDefault("5% increased magic/summon damage");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;

            Item.defense = 1;

            Item.value = Item.sellPrice(copper: 50);
			Item.rare = ItemRarityID.Blue;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Magic) += 0.05f;
			player.GetDamage(DamageClass.Summon) += 0.05f;
		}

		public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ItemID.FallenStar, 10).AddTile(TileID.WorkBenches).Register();
        }
    }
}
