using AAMod.Common.Systems;
using AAMod.Common.Systems.UI;
using AAMod.Content.Items.Placeables.Energy;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Content.Tiles.Energy
{
    class Blueprint : ModTile
    {
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileID.Sets.FramesOnKillWall[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
			TileObjectData.addTile(Type);
			AddMapEntry(new Color(10, 10, 200), Language.GetText("Blueprint"));
		}
		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 32, ModContent.ItemType<BlueprintItem>());
		}
        public override bool RightClick(int i, int j)
        {
			if (Main.LocalPlayer.IsTileTypeInInteractionRange(Type))
            {
				UISystem system = ModContent.GetInstance<UISystem>();
				int index = system.InterfaceLayers.FindIndex(layer => layer.Name == "Vanilla: Inventory");
				system.ActivateUI(new EnergyBlueprint(), index);
				return true;
			}
			return false;
        }
    }
}
