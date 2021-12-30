using AAMod.Content.Items.Placeables.Furniture.Bogwood;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Content.Tiles.Furniture.Bogwood
{
    public class BogwoodDoorClosed : ModTile
    {
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileSolid[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileID.Sets.NotReallySolid[Type] = true;
			TileID.Sets.DrawsWalls[Type] = true;
			TileID.Sets.HasOutlines[Type] = true;
			TileID.Sets.DisableSmartCursor[Type] = true;

			TileObjectData.newTile.Width = 1;
			TileObjectData.newTile.Height = 3;

			TileObjectData.newTile.Origin = Point16.Zero;

			TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);

			TileObjectData.newTile.UsesCustomCanPlace = true;

			TileObjectData.newTile.CoordinateHeights = new[]
			{
				16, 16, 16
			};

			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;

			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = new Point16(0, 1);

			TileObjectData.addAlternate(0);

			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Origin = new Point16(0, 2);

			TileObjectData.addAlternate(0);

			TileObjectData.addTile(Type);

			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Razewood Door");
			AddMapEntry(new Color(52, 50, 36), name);

			SoundType = SoundID.Dig;
			DustType = DustID.BorealWood;

			AdjTiles = new int[]
			{
				TileID.OpenDoor
			};

			OpenDoorID = ModContent.TileType<BogwoodDoorOpen>();

			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsDoor);
		}

		public override bool HasSmartInteract() => true;

		public override void NumDust(int i, int j, bool fail, ref int num) => num = 1;

		public override void KillMultiTile(int i, int j, int frameX, int frameY) => Item.NewItem(new Vector2(i, j) * 16f, ModContent.ItemType<BogwoodDoor>());

		public override void MouseOver(int i, int j)
		{
			Player player = Main.LocalPlayer;

			player.cursorItemIconEnabled = true;
			player.cursorItemIconID = ModContent.ItemType<BogwoodDoor>();

			player.noThrow = 2;
		}
	}
}
