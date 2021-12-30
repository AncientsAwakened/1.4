using AAMod.Common.Globals;
using AAMod.Common.Systems;
using AAMod.Content.EnergyDamageClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Content.Buffs
{
    class ConflictingEnergy : ModBuff
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Conflicting Energy");
			Description.SetDefault("Too Many Modular Weapons! Energy Damage Decreased!");
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}
	}
}
