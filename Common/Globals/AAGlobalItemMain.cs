using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Common.Globals
{
	public class AAGlobalItemMain : GlobalItem
    {
		public override void SetDefaults(Item item)
		{
			if (item.type == ItemID.SpikyBall)
			{
				item.ammo = item.type;
			}
		}
    }
}

