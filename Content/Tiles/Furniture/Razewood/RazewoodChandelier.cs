using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Content.Tiles.Furniture.Razewood
{
	public class RazewoodChandelier : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;

			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);

			TileObjectData.newTile.Origin = new Point16(1, 0);

			TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, 1, 1);
			TileObjectData.newTile.AnchorBottom = AnchorData.Empty;

			TileObjectData.newTile.LavaDeath = true;

			TileObjectData.newTile.StyleWrapLimit = 37;

			// TileObjectData.newTile.StyleHorizontal = false;
			TileObjectData.newTile.StyleLineSkip = 2;

			TileObjectData.newTile.Direction = TileObjectDirection.None;

			TileObjectData.addTile(Type);

			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Razewood Chandelier");
			AddMapEntry(new Color(100, 87, 65), name);

			SoundType = SoundID.Dig;
			DustType = DustID.BorealWood;

			AdjTiles = new int[] 
			{ 
				TileID.Chandeliers 
			};

			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
		}

		public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

		public override void KillMultiTile(int i, int j, int frameX, int frameY) => Item.NewItem(new Vector2(i, j) * 16f, ModContent.ItemType<Items.Placeables.Furniture.Razewood.RazewoodChandelier>());

		public override void HitWire(int i, int j)
		{
			int left = i - Main.tile[i, j].frameX / 18 % 3;
			int top = j - Main.tile[i, j].frameY / 18 % 2;

			for (int x = left; x < left + 3; x++)
			{
				for (int y = top; y < top + 3; y++)
				{
					if (Main.tile[x, y].frameX >= 54)
					{
						Main.tile[x, y].frameX -= 54;
					}
					else
					{
						Main.tile[x, y].frameX += 54;
					}
				}
			}

			if (Wiring.running)
			{
				Wiring.SkipWire(left, top);
				Wiring.SkipWire(left, top + 1);
				Wiring.SkipWire(left + 1, top);
				Wiring.SkipWire(left + 1, top + 1);
			}

			NetMessage.SendTileSquare(-1, left, top + 3, 3);
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			Tile tile = Main.tile[i, j];

			if (tile.frameX < 18 * 3)
			{
				r = 0.8f;
				g = 0.2f;
				b = 0f;
			}
		}

		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			Tile tile = Main.tile[i, j];
			Vector2 zero = new(Main.offScreenRange, Main.offScreenRange);
			if (Main.drawToScreen)
			{
				zero = Vector2.Zero;
			}
			int height = tile.frameY == 36 ? 18 : 16;
			Main.spriteBatch.Draw(ModContent.Request<Texture2D>("AAMod/Content/Tiles/Furniture/Razewood/RazewoodChandelier_Glow").Value, new Vector2((i * 16) - (int)Main.screenPosition.X, (j * 16) - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.frameX, tile.frameY, 16, height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
		}
	}
}
