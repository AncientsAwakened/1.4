using AAMod.Common.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Content.Buffs
{
    class MindThrottle : ModBuff
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mind Throttle");
			Description.SetDefault("It's weird!");
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
			Main.pvpBuff[Type] = true;
		}
        public override void Update(NPC npc, ref int buffIndex)
        {
			if (Main.rand.Next(90) == 0)
            {
				npc.Center = new Vector2(npc.Center.X + Main.rand.Next(-120, 121), npc.Center.Y + Main.rand.Next(-120, 121));
			}
		}
        public override void Update(Player player, ref int buffIndex)
        {
			if (Main.rand.Next(90) == 0)
			{
				player.Center = new Vector2(player.Center.X + Main.rand.Next(-120, 121), player.Center.Y + Main.rand.Next(-120, 121));
			}
		}
    }
}
