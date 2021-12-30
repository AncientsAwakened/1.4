using AAMod.Common.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Content.Buffs
{
    class SlowingIce : ModBuff
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Slowing Ice");
			Description.SetDefault("Speed Down");
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
			Main.pvpBuff[Type] = true;
		}
        public override void Update(NPC npc, ref int buffIndex)
        {
			if (npc.velocity.X > 2f || npc.velocity.Y > 2f || npc.velocity.X > -2f || npc.velocity.X > -2f)
            {
                npc.velocity /= 1.2f;
            }
		}
        public override void Update(Player player, ref int buffIndex)
        {
            if (player.velocity.X > 2f || player.velocity.Y > 2f || player.velocity.X > -2f || player.velocity.X > -2f)
            {
                player.velocity /= 1.2f;
            }
        }
    }
}
