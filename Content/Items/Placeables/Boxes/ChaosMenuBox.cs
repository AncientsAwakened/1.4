using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Placeables.Boxes
{
	public class ChaosMenuBox : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Music Box (Chaos Menu)");
			Tooltip.SetDefault("ProVGM - The Ancient Chaos");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Sounds/Music/ChaosMenu"), ModContent.ItemType<ChaosMenuBox>(), ModContent.TileType<Tiles.Boxes.ChaosMenuBox>());
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

			Item.createTile = ModContent.TileType<Tiles.Boxes.ChaosMenuBox>();

			Item.rare = ItemRarityID.LightRed;
		}
	}
}
