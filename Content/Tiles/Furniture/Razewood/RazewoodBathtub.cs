using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Content.Tiles.Furniture.Razewood
{
    public class RazewoodBathtub : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;

			TileObjectData.newTile.CopyFrom(TileObjectData.Style4x2);

			TileObjectData.newTile.Origin = new Point16(1, 1);

			TileObjectData.newTile.DrawYOffset = 2;

			TileObjectData.addTile(Type);

			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Razewood Bathtub");
			AddMapEntry(new Color(100, 87, 65), name);

			SoundType = SoundID.Dig;
			DustType = DustID.BorealWood;
		}

		public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

		public override void KillMultiTile(int i, int j, int frameX, int frameY) => Item.NewItem(new Vector2(i, j) * 16f, ModContent.ItemType<Items.Placeables.Furniture.Razewood.RazewoodBathtub>());
	}
}
