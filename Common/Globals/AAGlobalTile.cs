using AAMod.Content.Items.Materials.Energy.Coolant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Common.Globals
{
    class AAGlobalTile : GlobalTile
    {
        public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (type == TileID.Plants)
            {
                if (Main.rand.Next(200) == 0)
                {
                    Item.NewItem(i * 16, j * 16, 32, 32, ModContent.ItemType<TheCoolAnt>());
                }
            }
        }
    }
}
