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
    class StruggleAlt : ModBuff
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Press Space");
			Description.SetDefault("Press Space");
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<StrugglePlayer>().Grappled = true;
			if (player.buffTime[buffIndex] > 250)
			{
				player.buffTime[buffIndex] = 250;
			}
			if (player.buffTime[buffIndex] < 250 && player.buffTime[buffIndex] > 0)
			{
				player.buffTime[buffIndex] += Main.rand.Next(1, 4);
			}
			player.GetModPlayer<StrugglePlayer>().GrappledIndex = buffIndex;
		}
	}
}
