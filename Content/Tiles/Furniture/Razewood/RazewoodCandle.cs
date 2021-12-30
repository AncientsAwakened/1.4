using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Content.Tiles.Furniture.Razewood
{
	public class RazewoodCandle : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;

			TileObjectData.newTile.CopyFrom(TileObjectData.StyleOnTable1x1);

			TileObjectData.newTile.StyleWrapLimit = 2;
			TileObjectData.newTile.StyleMultiplier = 2;

			TileObjectData.newTile.StyleHorizontal = true;

			TileObjectData.newTile.DrawYOffset = 2;

			TileObjectData.addTile(Type);

			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Razewood Candle");
			AddMapEntry(new Color(100, 87, 65), name);

			ItemDrop = ModContent.ItemType<Items.Placeables.Furniture.Razewood.RazewoodCandle>();

			SoundType = SoundID.Dig;
			DustType = DustID.BorealWood;

			AdjTiles = new int[] 
			{ 
				TileID.Candles 
			};

			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
		}

		public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

		public override void HitWire(int i, int j)
		{
			if (Main.tile[i, j].frameX >= 18)
			{
				Main.tile[i, j].frameX -= 18;
			}
			else
			{
				Main.tile[i, j].frameX += 18;
			}
		}

		public override bool RightClick(int i, int j)
		{
			Main.LocalPlayer.PickTile(i, j, 100);

			return true;
		}

		public override void MouseOver(int i, int j)
		{
			Player player = Main.LocalPlayer;

			player.cursorItemIconEnabled = true;
			player.cursorItemIconID = ModContent.ItemType<Items.Placeables.Furniture.Razewood.RazewoodCandle>();

			player.noThrow = 2;
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			Tile tile = Main.tile[i, j];

			if (tile.frameX < 18)
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
			Main.spriteBatch.Draw(ModContent.Request<Texture2D>("AAMod/Content/Tiles/Furniture/Razewood/RazewoodCandle_Glow").Value, new Vector2((i * 16) - (int)Main.screenPosition.X, (j * 16) - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.frameX, tile.frameY, 16, height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
		}

		public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
		{
			if (i % 2 == 1)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}
		}
	}
}
