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
    class NecroticWound : ModBuff
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Necrotic Wound");
			Description.SetDefault("Ouch!");
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
			Main.pvpBuff[Type] = true;
		}
        public override void Update(NPC npc, ref int buffIndex)
        {
			npc.lifeRegen -= 20;
		}
        public override void Update(Player player, ref int buffIndex)
        {
			if (player.lifeRegen > 0)
			{
				player.lifeRegen = 0;
			}
			player.lifeRegenTime = 0;
			player.lifeRegen -= 20;
        }
    }
}
