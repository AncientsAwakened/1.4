using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Audio;

namespace AAMod.Content.Tiles.Furniture.Bogwood
{
    public class BogwoodDresser : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;
			Main.tileTable[Type] = true;
			Main.tileContainer[Type] = true;

			TileID.Sets.HasOutlines[Type] = true;

			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);

			TileObjectData.newTile.Origin = new Point16(1, 1);

			TileObjectData.newTile.HookCheckIfCanPlace = new PlacementHook(Chest.FindEmptyChest, -1, 0, true);
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(Chest.AfterPlacement_Hook, -1, 0, false);

			TileObjectData.newTile.AnchorInvalidTiles = new[]
			{
				127
			};

			TileObjectData.newTile.StyleHorizontal = true;

			TileObjectData.addTile(Type);

			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Bogwood Dresser");
			AddMapEntry(new Color(52, 50, 36), name);

			SoundType = SoundID.Dig;
			DustType = DustID.BorealWood;

			AdjTiles = new int[]
			{
				TileID.Dressers
			};

			ContainerName.SetDefault("Bogwood Dresser");
			DresserDrop = ModContent.ItemType<Items.Placeables.Furniture.Bogwood.BogwoodDresser>();

			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
		}

		public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

		public override void KillMultiTile(int i, int j, int frameX, int frameY) => Item.NewItem(new Vector2(i, j) * 16f, ModContent.ItemType<Items.Placeables.Furniture.Bogwood.BogwoodDresser>());

		public override bool HasSmartInteract() => true;

		public override bool RightClick(int i, int j)
		{
			Player player = Main.LocalPlayer;

			if (Main.tile[Player.tileTargetX, Player.tileTargetY].frameY == 0)
			{
				Main.CancelClothesWindow(true);

				// Main.mouseRightRelease = false;

				int left = Main.tile[Player.tileTargetX, Player.tileTargetY].frameX / 18;
				int top = Player.tileTargetY - Main.tile[Player.tileTargetX, Player.tileTargetY].frameY / 18;

				left %= 3;
				left = Player.tileTargetX - left;

				if (player.sign > -1)
				{
					SoundEngine.PlaySound(SoundID.MenuClose);

					player.sign = -1;

					// Main.editSign = false;
					Main.npcChatText = string.Empty;
				}

				if (Main.editChest)
				{
					SoundEngine.PlaySound(SoundID.MenuTick);

					// Main.editChest = false;
					Main.npcChatText = string.Empty;
				}

				if (player.editedChestName)
				{
					NetMessage.SendData(MessageID.SyncPlayerChest, -1, -1, NetworkText.FromLiteral(Main.chest[player.chest].name), player.chest, 1f);

					// player.editedChestName = false;
				}

				if (Main.netMode == NetmodeID.MultiplayerClient)
				{
					if (left == player.chestX && top == player.chestY && player.chest != -1)
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
					player.piggyBankProjTracker.Clear();

					int num213 = Chest.FindChest(left, top);

					if (num213 != -1)
					{
						Main.stackSplit = 600;

						if (num213 == player.chest)
						{
							player.chest = -1;

							Recipe.FindRecipes();

							SoundEngine.PlaySound(SoundID.MenuClose);
						}
						else if (num213 != player.chest && player.chest == -1)
						{
							player.chest = num213;

							Main.playerInventory = true;
							// Main.recBigList = false;

							SoundEngine.PlaySound(SoundID.MenuOpen);

							player.chestX = left;
							player.chestY = top;
						}
						else
						{
							player.chest = num213;

							Main.playerInventory = true;
							// Main.recBigList = false;

							SoundEngine.PlaySound(SoundID.MenuTick);

							player.chestX = left;
							player.chestY = top;
						}

						Recipe.FindRecipes();
					}
				}
			}
			else
			{
				// Main.playerInventory = false;

				player.chest = -1;

				Recipe.FindRecipes();

				Main.interactedDresserTopLeftX = Player.tileTargetX;
				Main.interactedDresserTopLeftY = Player.tileTargetY;

				Main.OpenClothesWindow();
			}

			return true;
		}

		public override void MouseOverFar(int i, int j)
		{
			Player player = Main.LocalPlayer;

			Tile tile = Main.tile[Player.tileTargetX, Player.tileTargetY];

			int left = Player.tileTargetX;
			int top = Player.tileTargetY;

			left -= tile.frameX % 54 / 18;

			if (tile.frameY % 36 != 0)
			{
				top--;
			}

			int chestIndex = Chest.FindChest(left, top);

			player.cursorItemIconID = -1;

			if (chestIndex < 0)
			{
				player.cursorItemIconText = Language.GetTextValue("LegacyDresserType.0");
			}
			else
			{
				player.cursorItemIconText = Main.chest[chestIndex].name != "" ? Main.chest[chestIndex].name : "Bogwood Dresser";

				if (player.cursorItemIconText == "Bogwood Dresser")
				{
					player.cursorItemIconID = ModContent.ItemType<Items.Placeables.Furniture.Bogwood.BogwoodDresser>();
					player.cursorItemIconText = "";
				}
			}

			player.cursorItemIconEnabled = true;

			player.noThrow = 2;

			if (player.cursorItemIconText == "")
			{
				// player.showItemIcon = false;
				player.cursorItemIconID = 0;
			}
		}

		public override void MouseOver(int i, int j)
		{
			Player player = Main.LocalPlayer;

			Tile tile = Main.tile[Player.tileTargetX, Player.tileTargetY];

			int left = Player.tileTargetX;
			int top = Player.tileTargetY;

			left -= tile.frameX % 54 / 18;

			if (tile.frameY % 36 != 0)
			{
				top--;
			}

			int num138 = Chest.FindChest(left, top);

			player.cursorItemIconID = -1;

			if (num138 < 0)
			{
				player.cursorItemIconText = Language.GetTextValue("LegacyDresserType.0");
			}
			else
			{
				player.cursorItemIconText = Main.chest[num138].name != "" ? Main.chest[num138].name : "Bogwood Dresser";

				if (player.cursorItemIconText == "Bogwood Dresser")
				{
					player.cursorItemIconID = ModContent.ItemType<Items.Placeables.Furniture.Bogwood.BogwoodDresser>();
					player.cursorItemIconText = "";
				}
			}

			player.cursorItemIconEnabled = true;

			player.noThrow = 2;

			if (Main.tile[Player.tileTargetX, Player.tileTargetY].frameY > 0)
			{
				player.cursorItemIconID = ItemID.FamiliarShirt;
			}
		}
	}
}
