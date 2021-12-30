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
    class MindWarp : ModBuff
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mind Warp");
			Description.SetDefault("Random Chaos!");
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
			Main.pvpBuff[Type] = true;
		}
        public override void Update(NPC npc, ref int buffIndex)
        {
			if (Main.rand.Next(270) == 0)
            {
                int Chaos = Main.rand.Next(3);
                if (Chaos == 0)
                {
                    if (!npc.HasBuff(ModContent.BuffType<MindSplit>()))
                    {
                        npc.AddBuff(ModContent.BuffType<MindSplit>(), 90);
                    }
                }
                else if (Chaos == 1)
                {
                    if (!npc.HasBuff(ModContent.BuffType<MindThrottle>()))
                    {
                        npc.AddBuff(ModContent.BuffType<MindThrottle>(), 90);
                    }
                }
                else
                {
                    if (!npc.HasBuff(ModContent.BuffType<MindSmash>()))
                    {
                        npc.AddBuff(ModContent.BuffType<MindSmash>(), 90);
                    }
                }
            }
		}
        public override void Update(Player player, ref int buffIndex)
        {
            if (Main.rand.Next(270) == 0)
            {
                int Chaos = Main.rand.Next(3);
                if (Chaos == 0)
                {
                    if (!player.HasBuff(ModContent.BuffType<MindSplit>()))
                    {
                        player.AddBuff(ModContent.BuffType<MindSplit>(), 90);
                    }
                }
                else if (Chaos == 1)
                {
                    if (!player.HasBuff(ModContent.BuffType<MindThrottle>()))
                    {
                        player.AddBuff(ModContent.BuffType<MindThrottle>(), 90);
                    }
                }
                else
                {
                    if (!player.HasBuff(ModContent.BuffType<MindSmash>()))
                    {
                        player.AddBuff(ModContent.BuffType<MindSmash>(), 90);
                    }
                }
            }
        }
    }
}
