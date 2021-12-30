using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Content.Tiles.Furniture.Bogwood
{
	public class BogwoodChair : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileID.Sets.DisableSmartCursor[Type] = true;

			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);

			TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;

			TileObjectData.newTile.StyleWrapLimit = 2;
			TileObjectData.newTile.StyleMultiplier = 2;

			TileObjectData.newTile.StyleHorizontal = true;

			TileObjectData.newTile.DrawYOffset = 2;

			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);

			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;

			TileObjectData.addAlternate(1);

			TileObjectData.addTile(Type);

			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Bogwood Chair");
			AddMapEntry(new Color(52, 50, 36), name);

			ItemDrop = ModContent.ItemType<Items.Placeables.Furniture.Bogwood.BogwoodChair>();

			SoundType = SoundID.Dig;
			DustType = DustID.BorealWood;

			AdjTiles = new int[]
			{
				TileID.Chairs
			};

			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsChair);
		}

		public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
	}
}
