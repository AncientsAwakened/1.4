using AAMod.Common.Systems;
using AAMod.Common.Systems.UI;
using AAMod.Content.NPCs.Bosses.Grips;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace AAMod.Content.Buffs
{
    class Struggle : ModBuff
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Grappled");
			Description.SetDefault("Struggling to loosen yourself");
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
	public class StrugglePlayer : ModPlayer
    {
		public bool Grappled;
		public int GrappledIndex;
		public bool FailInstant;
		private int FailAnim;
		private bool triggerSet;
		public override void ProcessTriggers(TriggersSet triggersSet)
		{
			if (Grappled && triggersSet.Jump && !triggerSet && !FailInstant)
            {
				Player.buffTime[GrappledIndex] -= Main.rand.Next(4, 7) * 5;
				triggerSet = true;
            }
			if (!triggersSet.Jump && triggerSet)
            {
				triggerSet = false;
            }
		}
        public override bool CanUseItem(Item item)
        {
			if (Grappled)
            {
				return false;
            }
			return true;
        }
        public override bool CanShoot(Item item)
        {
			if (Grappled)
			{
				return false;
			}
			return true;
		}
        public override void PreUpdateMovement()
        {
            if (Grappled)
            {
				Player.velocity = Vector2.Zero;
            }
        }
        public override void PostUpdate()
        {
			if (Grappled && !NPC.AnyNPCs(ModContent.NPCType<MireGrip>()) && Main.dayTime)
            {
				Player.KillMe(PlayerDeathReason.ByCustomReason($"{Player.name} got sent to Brazil"), 10, 0);
            }
        }
        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (Grappled && !NPC.AnyNPCs(ModContent.NPCType<MireGrip>()) && Main.dayTime)
            {
				genGore = false;
            }
			return true;
        }
        public override void PostUpdateBuffs()
        {
			if (Grappled)
            {
				Player.invis = true;
			}
			if (Grappled && !FailInstant)
            {
				UISystem system = ModContent.GetInstance<UISystem>();
				int index = system.InterfaceLayers.FindIndex(layer => layer.Name == "Vanilla: Mouse Text");
				system.ActivateUI(new Bar(), index);
			}
			if (Grappled && !Player.HasBuff(ModContent.BuffType<Struggle>()) && !Player.HasBuff(ModContent.BuffType<StruggleAlt>()))
            {
				Grappled = false;
				FailInstant = false;
				UISystem system = ModContent.GetInstance<UISystem>();
				system.DeactivateUI<Bar>();
				FailAnim = 0;
			}
			if (FailInstant)
            {
				FailAnim++;
				if (FailAnim == 1)
                {
					SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Custom/FailIndicator"), Player.position);
				}
				if (FailAnim == 15)
                {
					UISystem system = ModContent.GetInstance<UISystem>();
					system.DeactivateUI<Bar>();
				}
			}
			if ((Player.HasBuff(ModContent.BuffType<Struggle>()) || Player.HasBuff(ModContent.BuffType<StruggleAlt>())) && Player.buffTime[GrappledIndex] <= 0 && !FailInstant)
			{
				Player.ClearBuff(ModContent.BuffType<Struggle>());
                Player.ClearBuff(ModContent.BuffType<StruggleAlt>());
			}
			if ((Player.HasBuff(ModContent.BuffType<Struggle>()) || Player.HasBuff(ModContent.BuffType<StruggleAlt>())) && !NPC.AnyNPCs(ModContent.NPCType<MireGrip>()) && !Main.dayTime)
            {
				Player.ClearBuff(ModContent.BuffType<Struggle>());
				Player.ClearBuff(ModContent.BuffType<StruggleAlt>());
			}
        }
    }
}
