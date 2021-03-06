using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Audio;

namespace AAMod.Content.Tiles.Furniture.Bogwood
{
    public class BogwoodChest : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileContainer[Type] = true;
			Main.tileSpelunker[Type] = true;
			Main.tileShine2[Type] = true;
			Main.tileShine[Type] = 1200;
			TileID.Sets.HasOutlines[Type] = true;
			TileID.Sets.BasicChest[Type] = true;
			TileID.Sets.DisableSmartCursor[Type] = true;

			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);

			TileObjectData.newTile.Origin = new Point16(0, 1);

			TileObjectData.newTile.CoordinateHeights = new[]
			{
				16,
				18
			};

			TileObjectData.newTile.HookCheckIfCanPlace = new PlacementHook(Chest.FindEmptyChest, -1, 0, true);
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(Chest.AfterPlacement_Hook, -1, 0, false);

			TileObjectData.newTile.AnchorInvalidTiles = new[]
			{
				127
			};

			TileObjectData.newTile.StyleHorizontal = true;
			// TileObjectData.newTile.LavaDeath = false;

			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);

			TileObjectData.newTile.DrawYOffset = 2;

			TileObjectData.addTile(Type);

			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Bogwood Chest");
			AddMapEntry(new Color(52, 50, 36), name, MapChestName);

			SoundType = SoundID.Dig;
			DustType = DustID.BorealWood;

			AdjTiles = new int[]
			{
				TileID.Containers
			};

			ContainerName.SetDefault("Bogwood Chest");
			ChestDrop = ModContent.ItemType<Items.Placeables.Furniture.Bogwood.BogwoodChest>();
		}

		public override void NumDust(int i, int j, bool fail, ref int num) => num = 1;

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(new Vector2(i, j) * 16f, ChestDrop);

			Chest.DestroyChest(i, j);
		}

		public string MapChestName(string name, int i, int j)
		{
			int left = i;
			int top = j;

			Tile tile = Main.tile[i, j];

			if (tile.frameX % 36 != 0)
			{
				left--;
			}

			if (tile.frameY != 0)
			{
				top--;
			}

			int chest = Chest.FindChest(left, top);

			if (Main.chest[chest].name == "")
			{
				return name;
			}

			return name + ": " + Main.chest[chest].name;
		}

		public override bool RightClick(int i, int j)
		{
			Player player = Main.LocalPlayer;

			Tile tile = Main.tile[i, j];

			// Main.mouseRightRelease = false;

			int left = i;
			int top = j;

			if (tile.frameX % 36 != 0)
			{
				left--;
			}

			if (tile.frameY != 0)
			{
				top--;
			}

			if (player.sign >= 0)
			{
				SoundEngine.PlaySound(SoundID.MenuClose);

				// Main.editSign = false;
				Main.npcChatText = "";

				player.sign = -1;
			}

			if (Main.editChest)
			{
				SoundEngine.PlaySound(SoundID.MenuTick);

				// Main.editChest = false;
				Main.npcChatText = "";
			}

			if (player.editedChestName)
			{
				NetMessage.SendData(MessageID.SyncPlayerChest, -1, -1, NetworkText.FromLiteral(Main.chest[player.chest].name), player.chest, 1f);

				// player.editedChestName = false;
			}

			if (Main.netMode == NetmodeID.MultiplayerClient)
			{
				if (left == player.chestX && top == player.chestY && player.chest >= 0)
				{
					player.chest = -1;

					Recipe.FindRecipes();

					SoundEngine.PlaySound(SoundID.MenuClose);
				}
				else
				{
					NetMessage.SendData(MessageID.RequestChestOpen, -1, -1, null, left, top);

					Main.stackSplit = 600;
				}
			}
			else
			{
				int chest = Chest.FindChest(left, top);

				if (chest >= 0)
				{
					Main.stackSplit = 600;

					if (chest == player.chest)
					{
						player.chest = -1;

						SoundEngine.PlaySound(SoundID.MenuClose);
					}
					else
					{
						Main.playerInventory = true;
						// Main.recBigList = false;

						player.chest = chest;

						player.chestX = left;
						player.chestY = top;

						SoundEngine.PlaySound(player.chest < 0 ? SoundID.MenuOpen : SoundID.MenuTick);
					}

					Recipe.FindRecipes();
				}
			}

			return true;
		}

		public override void MouseOver(int i, int j)
		{
			Player player = Main.LocalPlayer;

			Tile tile = Main.tile[i, j];

			int left = i;
			int top = j;

			if (tile.frameX % 36 != 0)
			{
				left--;
			}

			if (tile.frameY != 0)
			{
				top--;
			}

			int chest = Chest.FindChest(left, top);

			player.cursorItemIconID = -1;

			if (chest < 0)
			{
				player.cursorItemIconText = Language.GetTextValue("LegacyChestType.0");
			}
			else
			{
				player.cursorItemIconText = Main.chest[chest].name.Length > 0 ? Main.chest[chest].name : "Bogwood Chest";

				if (player.cursorItemIconText == "Bogwood Chest")
				{
					player.cursorItemIconID = ChestDrop;
					player.cursorItemIconText = "";
				}
			}

			player.noThrow = 2;
			player.cursorItemIconEnabled = true;
		}

		public override void MouseOverFar(int i, int j)
		{
			MouseOver(i, j);

			Player player = Main.LocalPlayer;

			if (player.cursorItemIconText == "")
			{
				// player.showItemIcon = false;
				player.cursorItemIconID = 0;
			}
		}
	}
}
