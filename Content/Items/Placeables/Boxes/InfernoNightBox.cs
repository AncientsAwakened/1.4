using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Content.Items.Placeables.Boxes
{
	public class InfernoNightBox : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Music Box (Inferno Night)");
			Tooltip.SetDefault("ProVGM - Ember Storm");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Sounds/Music/InfernoNight"), ModContent.ItemType<InfernoNightBox>(), ModContent.TileType<Tiles.Boxes.InfernoNightBox>());
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

			Item.createTile = ModContent.TileType<Tiles.Boxes.InfernoNightBox>();

			Item.rare = ItemRarityID.LightRed;
		}
	}
}
