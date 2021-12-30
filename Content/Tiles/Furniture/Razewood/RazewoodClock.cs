using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Content.Tiles.Furniture.Razewood
{
	public class RazewoodClock : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;

			TileID.Sets.HasOutlines[Type] = true;
			TileID.Sets.Clock[Type] = true;

			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);

			TileObjectData.newTile.Height = 5;
			TileObjectData.newTile.CoordinateHeights = new[]
			{
				16, 16, 16, 16, 16
			};

			TileObjectData.newTile.Origin = new Point16(1, 1);

			TileObjectData.newTile.StyleHorizontal = true;
			// TileObjectData.newTile.LavaDeath = false;

			TileObjectData.newTile.DrawYOffset = 2;

			TileObjectData.addTile(Type);

			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Razewood Clock");
			AddMapEntry(new Color(100, 87, 65), name);

			SoundType = SoundID.Dig;
			DustType = DustID.BorealWood;
		}

		public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

		public override void KillMultiTile(int i, int j, int frameX, int frameY) => Item.NewItem(new Vector2(i, j) * 16f, ModContent.ItemType<Items.Placeables.Furniture.Razewood.RazewoodClock>());

		public override bool HasSmartInteract() => true;

		public override bool RightClick(int i, int j)
		{
			string text = "AM";

			double time = Main.time;

			if (!Main.dayTime)
			{
				time += 54000.0;
			}

			time = time / 86400.0 * 24.0;
			time = time - 7.5 - 12.0;

			if (time < 0.0)
			{
				time += 24.0;
			}
			else if (time >= 12.0)
			{
				text = "PM";
			}

			int intTime = (int)time;

			double deltaTime = time - intTime;

			deltaTime = (int)(deltaTime * 60.0);

			string text2 = string.Concat(deltaTime);

			if (deltaTime < 10.0)
			{
				text2 = "0" + text2;
			}

			if (intTime > 12)
			{
				intTime -= 12;
			}
			else if (intTime == 0)
			{
				intTime = 12;
			}

			var newText = string.Concat("Time: ", intTime, ":", text2, " ", text);

			Main.NewText(newText, new Color(255, 240, 20));

			return true;
		}
	}
}
