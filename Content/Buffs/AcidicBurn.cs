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
    class AcidicBurn : ModBuff
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Acidic Burn");
			Description.SetDefault("Really Bad Ouch!");
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
			Main.pvpBuff[Type] = true;
		}
        public override void Update(NPC npc, ref int buffIndex)
        {
			npc.lifeRegen -= 32;
		}
        public override void Update(Player player, ref int buffIndex)
        {
			if (player.lifeRegen > 0)
			{
				player.lifeRegen = 0;
			}
			player.lifeRegenTime = 0;
			player.lifeRegen -= 32;
        }
    }
}
