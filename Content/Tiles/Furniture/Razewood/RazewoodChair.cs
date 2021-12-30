using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Content.Tiles.Furniture.Razewood
{
	public class RazewoodChair : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileID.Sets.DisableSmartCursor[Type] = true;

			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);

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
			name.SetDefault("Razewood Chair");
			AddMapEntry(new Color(100, 87, 65), name);

			SoundType = SoundID.Dig;
			DustType = DustID.BorealWood;

			AdjTiles = new int[] 
			{ 
				TileID.Chairs 
			};

			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsChair);
		}

		public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

		public override void KillMultiTile(int i, int j, int frameX, int frameY) => Item.NewItem(new Vector2(i, j) * 16f, ModContent.ItemType<Items.Placeables.Furniture.Razewood.RazewoodChair>());
	}
}
