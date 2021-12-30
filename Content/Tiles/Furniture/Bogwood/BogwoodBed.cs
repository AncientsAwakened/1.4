using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Content.Tiles.Furniture.Bogwood
{
	public class BogwoodBed : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileID.Sets.CanBeSleptIn[Type] = true;
			TileID.Sets.IsValidSpawnPoint[Type] = true;
			TileID.Sets.DisableSmartCursor[Type] = true;

			TileID.Sets.HasOutlines[Type] = true;

			TileObjectData.newTile.CopyFrom(TileObjectData.Style4x2);

			TileObjectData.newTile.Origin = new Point16(1, 1);

			TileObjectData.newTile.DrawYOffset = 2;

			TileObjectData.addTile(Type);

			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Bogwood Bed");
			AddMapEntry(new Color(52, 50, 36), name);

			SoundType = SoundID.Dig;
			DustType = DustID.BorealWood;

			AdjTiles = new int[]
			{
				TileID.Beds
			};
		}

		public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

		public override void KillMultiTile(int i, int j, int frameX, int frameY) => Item.NewItem(new Vector2(i, j) * 16f, ModContent.ItemType<Items.Placeables.Furniture.Bogwood.BogwoodBed>());

		public override bool HasSmartInteract() => true;

		public override bool RightClick(int i, int j)
		{
			Player player = Main.LocalPlayer;

			Tile tile = Main.tile[i, j];

			int spawnX = i - tile.frameX / 18;
			int spawnY = j + 2;

			spawnX += tile.frameX >= 72 ? 5 : 2;

			if (tile.frameY % 38 != 0)
			{
				spawnY--;
			}

			player.FindSpawn();

			var textColor = new Color(255, 240, 20);

			if (player.SpawnX == spawnX && player.SpawnY == spawnY)
			{
				player.RemoveSpawn();

				Main.NewText(Language.GetTextValue("Game.SpawnPointRemoved"), textColor);
			}
			else if (Player.CheckSpawn(spawnX, spawnY))
			{
				player.ChangeSpawn(spawnX, spawnY);

				Main.NewText(Language.GetTextValue("Game.SpawnPointSet"), textColor);
			}

			return true;
		}

		public override void MouseOver(int i, int j)
		{
			Player player = Main.LocalPlayer;

			player.cursorItemIconEnabled = true;
			player.cursorItemIconID = ModContent.ItemType<Items.Placeables.Furniture.Bogwood.BogwoodBed>();

			player.noThrow = 2;
		}
	}
}
