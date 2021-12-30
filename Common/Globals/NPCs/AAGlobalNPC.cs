using AAMod.Content.Items.Materials.Energy;
using AAMod.Content.Items.Weapons.Typeless;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Common.Globals.NPCs
{
	class AAGlobalNPC : GlobalNPC
	{
		public override void SetupShop(int type, Chest shop, ref int nextSlot)
		{
			if (type == NPCID.Demolitionist)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<FragGrenade>());
				nextSlot++;
			}
		}
        public override void ModifyGlobalLoot(GlobalLoot globalLoot)
        {
			globalLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(1000, new int[] { ModContent.ItemType<AcidShard>(), ModContent.ItemType<DarkShard>(), ModContent.ItemType<ElectricShard>(), ModContent.ItemType<FireShard>(), ModContent.ItemType<IceShard>(), ModContent.ItemType<LightShard>(), ModContent.ItemType<PowerShard>(), ModContent.ItemType<PsychicShard>() } ));
		}
    }
}
