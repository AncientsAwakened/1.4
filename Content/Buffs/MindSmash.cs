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
    class MindSmash : ModBuff
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mind Smash");
			Description.SetDefault("It's not right!");
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
			Main.pvpBuff[Type] = true;
		}
        public override void Update(NPC npc, ref int buffIndex)
        {
			npc.defense -= 10;
		}
        public override void Update(Player player, ref int buffIndex)
        {
			player.statDefense -= 10;
        }
    }
}
