using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Armor.GoblinLeather
{
    [AutoloadEquip(EquipType.Legs)]
    public class GoblinLeatherGreaves : ModItem
    {
        public override string Texture => AAMod.PlaceholderTexture;

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Goblin Leather Greaves");
			Tooltip.SetDefault("'15% reduced movement speed'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
		public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;

            Item.defense = 3;

            Item.value = Item.sellPrice(copper: 50);
			Item.rare = ItemRarityID.Green;
		}

		public override void UpdateEquip(Player player) => player.moveSpeed -= 0.15f;

		public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ModContent.ItemType<Materials.ToughLeather>(), 10).AddTile(TileID.WorkBenches).Register();
        }
    }
}
