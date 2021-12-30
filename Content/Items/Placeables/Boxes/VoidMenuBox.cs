using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Placeables.Boxes
{
	public class VoidMenuBox : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Music Box (Void Menu)");
			Tooltip.SetDefault("ProVGM - The Ancient Void");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Sounds/Music/VoidMenu"), ModContent.ItemType<VoidMenuBox>(), ModContent.TileType<Tiles.Boxes.VoidMenuBox>());
		}

		public override void SetDefaults()
		{
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.accessory = true;

			Item.width = 32;
			Item.height = 20;

			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;

			Item.createTile = ModContent.TileType<Tiles.Boxes.VoidMenuBox>();

			Item.rare = ItemRarityID.LightRed;
		}
	}
}
