using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Armor.Sparkle
{
    [AutoloadEquip(EquipType.Body)]
    public class SparkleBreastplate : ModItem
    {
		public override string Texture => AAMod.PlaceholderTexture;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sparkle Breastplate");
			Tooltip.SetDefault("6% increased magic/summon damage");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 20;

            Item.defense = 2;

            Item.value = Item.sellPrice(copper: 60);
			Item.rare = ItemRarityID.Blue;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Magic) += 0.06f;
			player.GetDamage(DamageClass.Summon) += 0.06f;
		}


		public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ItemID.FallenStar, 20).AddTile(TileID.WorkBenches).Register();
        }
    }
}
