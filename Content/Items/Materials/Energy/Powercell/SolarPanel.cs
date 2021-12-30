using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Materials.Energy.Powercell
{
    public class SolarPanel : ModItem
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Solar Panel");
			Tooltip.SetDefault("A Powersource for the Sunny Days.");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
        }
		public override void SetDefaults()
        {
            Item.maxStack = 1;
            Item.width = 38;
            Item.height = 30;
            Item.rare = ItemRarityID.Cyan;
        }
        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient(ItemID.CopperBar, 5).AddIngredient(ItemID.IronBar, 10).AddIngredient(ItemID.BlackInk).AddTile(TileID.WorkBenches).Register();
            CreateRecipe().AddIngredient(ItemID.CopperBar, 5).AddIngredient(ItemID.LeadBar, 10).AddIngredient(ItemID.BlackInk).AddTile(TileID.WorkBenches).Register();
            CreateRecipe().AddIngredient(ItemID.TinBar, 5).AddIngredient(ItemID.IronBar, 10).AddIngredient(ItemID.BlackInk).AddTile(TileID.WorkBenches).Register();
            CreateRecipe().AddIngredient(ItemID.TinBar, 5).AddIngredient(ItemID.LeadBar, 10).AddIngredient(ItemID.BlackInk).AddTile(TileID.WorkBenches).Register();
        }
    }
}