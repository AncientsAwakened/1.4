using AAMod.Content.NPCs.Bosses.Grips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace AAMod.Common.Players
{
    class PlayerKills : ModPlayer
    {
        private bool Clapped;
        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            if (damage >= Player.statLife && (npc.type == ModContent.NPCType<GrappleClap>() || npc.type == ModContent.NPCType<OffGuardClap>()))
            {
                Clapped = true;
            }
        }
        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (Clapped)
            {
                damageSource = PlayerDeathReason.ByCustomReason($"{Player.name} got clapped");
                genGore = false;
                Clapped = false;
            }
            return true;
        }
    }
}
