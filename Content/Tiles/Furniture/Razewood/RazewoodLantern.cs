using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Content.Tiles.Furniture.Razewood
{
	public class RazewoodLantern : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;

			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);

			TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);

			TileObjectData.newSubTile.LavaPlacement = LiquidPlacement.Allowed;

			TileObjectData.addTile(Type);

			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Razewood Lantern");
			AddMapEntry(new Color(100, 87, 65), name);

			SoundType = SoundID.Dig;
			DustType = DustID.BorealWood;

			AdjTiles = new int[] 
			{ 
				TileID.HangingLanterns 
			};

			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
		}

		public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

		public override void KillMultiTile(int i, int j, int frameX, int frameY) => Item.NewItem(new Vector2(i, j) * 16f, ModContent.ItemType<Items.Placeables.Furniture.Razewood.RazewoodLantern>());

		public override void HitWire(int i, int j)
		{
			int left = i - Main.tile[i, j].frameX / 18 % 1;
			int top = j - Main.tile[i, j].frameY / 18 % 2;

			for (int x = left; x < left + 1; x++)
			{
				for (int y = top; y < top + 2; y++)
				{
					if (Main.tile[x, y].frameX >= 18)
					{
						Main.tile[x, y].frameX -= 18;
					}
					else
					{
						Main.tile[x, y].frameX += 18;
					}
				}
			}

			if (Wiring.running)
			{
				Wiring.SkipWire(left, top);
				Wiring.SkipWire(left, top + 1);
			}

			NetMessage.SendTileSquare(-1, left, top + 1, 2);
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
	}
}
