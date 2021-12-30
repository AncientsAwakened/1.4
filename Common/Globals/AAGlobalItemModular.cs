using AAMod.Content.Items.Materials.Energy;
using AAMod.Content.Items.Materials.Energy.Coolant;
using AAMod.Content.Items.Materials.Energy.Powercell;
using AAMod.Content.Items.Weapons.FreedomStars;
using AAMod.Content.Items.Weapons.Energy;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System.Collections.Generic;
using AAMod.Content.EnergyDamageClass;
using AAMod.Content.Buffs;

namespace AAMod.Common.Globals
{
	public class AAGlobalItemModular : GlobalItem
    {
        #region SaveValues
        public override bool InstancePerEntity => true;
		public bool Coolant = false;
		public bool Overclock = false;
		public int OverclockValue = 0;
		public bool Powercell = false;
		public bool Shard = false;
		public bool Scrap = false;
		public int ScrapLevel = 0;
		public bool Fragment = false;
		public bool OutputWeapon = false;
		public bool CheatedIn = false;
		public int OutputCoolant = -1;
		public int OutputOverclock = -1;
		public int OutputPowercell = -1;
		public int OutputShard = 0;
		public int OutputScrap = -1;
		public int Freedom = -1;
		public int[] OutputFragment = new int[] { 0, 0, 0 };
		public int CurrentEnergy = 0;
		private int EnergyGrowthFormula = 0;
		public bool EnergyCooldown = true;
		private int EnergyGrowthCooldown = 0;
		public bool Displaying = false;
		public bool FreedomSwitchCooldown = false;
		public int MagicNERF = 0;
		public int MeleeNERF = 0;
		public int RangedNERF = 0;
		public int SummonNERF = 0;
		public int NERFFREEDOMSTAR = 0;
		private int FreedomSwitch = 0;
		public override void SaveData(Item item, TagCompound tag)
		{
			if (item.type == ModContent.ItemType<VoidOverclockUnit>())
            {
				tag["Value"] = OverclockValue;
			}
			if (Freedom != -1)
			{
				tag["Freedom"] = Freedom;
			}
			if (OutputCoolant != -1)
            {
				tag["Coolant"] = OutputCoolant;
			}
			if (OutputOverclock != -1)
			{
				tag["Overclock"] = OutputOverclock;
			}
			if (OutputPowercell != -1)
			{
				tag["Powercell"] = OutputPowercell;
			}
			if (OutputShard != 0)
			{
				tag["Shard"] = OutputShard;
			}
			if (OutputScrap != -1)
			{
				tag["Scrap"] = OutputScrap;
			}
			if (OutputFragment[1 - 1] != 0 && OutputFragment[2 - 1] != 0 && OutputFragment[3 - 1] != 0)
			{
				tag["Fragment"] = OutputFragment;
			}
			if (OutputWeapon && !CheatedIn)
            {
				tag["Energy"] = CurrentEnergy;
            }
		}
		public override void LoadData(Item item ,TagCompound tag)
		{
			if (tag.ContainsKey("Value"))
			{
				OverclockValue = tag.Get<int>("Value");
			}
			if (tag.ContainsKey("Freedom"))
			{
				Freedom = tag.Get<int>("Freedom");
			}
			if (tag.ContainsKey("Coolant"))
			{
				OutputCoolant = tag.Get<int>("Coolant");
			}
			if (tag.ContainsKey("Overclock"))
			{
				OutputOverclock = tag.Get<int>("Overclock");
			}
			if (tag.ContainsKey("Powercell"))
			{
				OutputPowercell = tag.Get<int>("Powercell");
			}
			if (tag.ContainsKey("Shard"))
			{
				OutputShard = tag.Get<int>("Shard");
			}
			if (tag.ContainsKey("Scrap"))
			{
				OutputScrap = tag.Get<int>("Scrap");
			}
			if (tag.ContainsKey("Fragment"))
			{
				OutputFragment = tag.Get<int[]>("Fragment");
			}
			if (tag.ContainsKey("Energy"))
			{
				CurrentEnergy = tag.Get<int>("Energy");
			}
		}
		public override GlobalItem Clone(Item item, Item itemClone)
		{
			return base.Clone(item, itemClone);
		}
		#endregion
		public override void SetDefaults(Item item)
		{
			if (item.type == ModContent.ItemType<AcidShard>() || item.type == ModContent.ItemType<DarkShard>() || item.type == ModContent.ItemType<ElectricShard>() || item.type == ModContent.ItemType<FireShard>() || item.type == ModContent.ItemType<IceShard>() || item.type == ModContent.ItemType<LightShard>() || item.type == ModContent.ItemType<PowerShard>() || item.type == ModContent.ItemType<PsychicShard>())
            {
				Shard = true;
            }
			if (item.type == ModContent.ItemType<MagicFragment>() || item.type == ModContent.ItemType<MeleeFragment>() || item.type == ModContent.ItemType<RangedFragment>() || item.type == ModContent.ItemType<SummonFragment>())
			{
				Fragment = true;
			}
			if (item.type == ModContent.ItemType<TheCoolAnt>())
            {
				Coolant = true;
            }
			if (item.type == ModContent.ItemType<VoidOverclockUnit>())
            {
				Overclock = true;
				OverclockValue = 1000;
            }
			if (item.type == ModContent.ItemType<SolarPanel>())
            {
				Powercell = true;
            }
			if (item.type == ModContent.ItemType<ScrapTier1>())
            {
				Scrap = true;
				ScrapLevel = 1;
            }
			if (item.type == ModContent.ItemType<ScrapTier2>())
			{
				Scrap = true;
				ScrapLevel = 2;
			}
			if (item.type == ModContent.ItemType<ScrapTier3>())
			{
				Scrap = true;
				ScrapLevel = 3;
			}
			if (item.type == ModContent.ItemType<ScrapTier4>())
			{
				Scrap = true;
				ScrapLevel = 4;
			}
			if (item.type == ModContent.ItemType<ScrapTier5>())
			{
				Scrap = true;
				ScrapLevel = 5;
			}
			if (item.type == ModContent.ItemType<ScrapTier6>())
			{
				Scrap = true;
				ScrapLevel = 6;
			}
			if (item.type == ModContent.ItemType<ScrapTier7>())
			{
				Scrap = true;
				ScrapLevel = 7;
			}
			if (item.type == ModContent.ItemType<ScrapTier8>())
			{
				Scrap = true;
				ScrapLevel = 8;
			}
			if (item.type == ModContent.ItemType<ScrapTier9>())
			{
				Scrap = true;
				ScrapLevel = 9;
			}
			if (item.type == ModContent.ItemType<ScrapTier10>())
			{
				Scrap = true;
				ScrapLevel = 10;
			}
			if (item.type == ModContent.ItemType<ScrapTier11>())
			{
				Scrap = true;
				ScrapLevel = 11;
			}
			if (item.type == ModContent.ItemType<ScrapTier12>())
			{
				Scrap = true;
				ScrapLevel = 12;
			}
			if (item.type == ModContent.ItemType<ScrapTier13>())
			{
				Scrap = true;
				ScrapLevel = 13;
			}
			if (item.type == ModContent.ItemType<MobianBuster>() || item.type == ModContent.ItemType<LaserBaton>() || item.type == ModContent.ItemType<FreedomStarBusted>() || item.type == ModContent.ItemType<FreedomStar>() || item.type == ModContent.ItemType<FreedomStarAcid>() || item.type == ModContent.ItemType<FreedomStarLight>())
            {
				OutputWeapon = true;
			}
			if (item.type == ModContent.ItemType<FreedomStar>())
			{
				Freedom = 0;
			}
			if (item.type == ModContent.ItemType<FreedomStarAcid>())
			{
				Freedom = 1;
			}
			if (item.type == ModContent.ItemType<FreedomStarLight>())
			{
				Freedom = 6;
			}
		}
		public void CoolantAbility(int item, Player player)
		{
			if (item == ModContent.ItemType<TheCoolAnt>())
            {
				player.AddBuff(BuffID.Frostburn, 90);
			}
		}
		public string CoolantText(int item)
		{
			if (item == ModContent.ItemType<TheCoolAnt>())
            {
				return "Coolant Ability: When the energy meter runs out, the player is inflicted with Frostburn";
			}
			return "N/A";
		}
		public int PowercellAbility(int item, int TimeNeed, bool isCharging = false)
		{
			if (item == ModContent.ItemType<SolarPanel>())
            {
				if (!Main.IsItRaining && Main.dayTime && TimeNeed >= 90)
                {
					return 3;
                }
				else if (Main.IsItRaining && !Main.IsItStorming && TimeNeed >= 90)
                {
					return 1;
                }
				return 0;
            }
			return 0;
		}
		public string PowercellText(int item)
		{
			if (item == ModContent.ItemType<SolarPanel>())
			{
				return "Powercell Ability: Charges Better during daytime and not raining too hard";
			}
			return "N/A";
		}
        public override void UpdateInventory(Item item, Player player)
        {
			if (OutputWeapon && OutputPowercell != -1 && !EnergyCooldown && !Displaying)
            {
				EnergyGrowthFormula++;
				if (PowercellAbility(OutputPowercell, EnergyGrowthFormula) != 0)
                {
					CurrentEnergy += PowercellAbility(OutputPowercell, EnergyGrowthFormula);
					EnergyGrowthFormula = 0;
                }
            }
			if (OutputWeapon && !CheatedIn && EnergyCooldown && !Displaying && !FreedomSwitchCooldown)
			{
				EnergyGrowthCooldown++;
				if (EnergyGrowthCooldown >= 300)
				{
					EnergyCooldown = false;
					EnergyGrowthCooldown = 0;
				}
			}
			if (FreedomSwitchCooldown && !CheatedIn)
			{
				FreedomSwitch++;
				if (FreedomSwitch >= 60)
				{
					FreedomSwitchCooldown = false;
					FreedomSwitch = 0;
				}
			}
			if (OutputWeapon && OutputCoolant != -1 && CurrentEnergy <= 0 && item == player.HeldItem && !Displaying)
			{
				CoolantAbility(OutputCoolant, Main.LocalPlayer);
			}
			if (OutputOverclock == -1 && CurrentEnergy != 0 && !Displaying)
			{
				CurrentEnergy = 0;
			}
			else if (CurrentEnergy > OutputOverclock && !Displaying)
			{
				CurrentEnergy = OutputOverclock;
			}
			if (CurrentEnergy < 0 && !Displaying)
			{
				CurrentEnergy = 0;
			}
			if (Displaying)
            {
				for (int i = 0; i < 59; i++)
				{
					if (item == player.inventory[i])
					{
						Displaying = false;
						EnergyCooldown = true;
					}
				}
			}
			if (!CheatedIn && OutputWeapon && (OutputCoolant == -1 || OutputOverclock == -1 || OutputPowercell == -1 || OutputScrap == -1 || OutputFragment[1 - 1] == 0 || OutputFragment[2 - 1] == 0 || OutputFragment[3 - 1] == 0) && Freedom == -1 && item.type != ModContent.ItemType<FreedomStarBusted>())
			{
				CheatedIn = true;
			}
			if (!CheatedIn && Freedom != -1 && (OutputCoolant == -1 || OutputOverclock == -1 || OutputPowercell == -1))
			{
				CheatedIn = true;
			}
			MagicNERF = 0;
			MeleeNERF = 0;
			RangedNERF = 0;
			SummonNERF = 0;
			NERFFREEDOMSTAR = 0;
			for (int i = 0; i < 59; i++)
            {
				if (player.inventory[i].DamageType == ModContent.GetInstance<OverallMagicEnergy>())
                {
					MagicNERF++;
                }
				if (player.inventory[i].DamageType == ModContent.GetInstance<OverallMeleeEnergy>())
                {
					MeleeNERF++;
                }
				if (player.inventory[i].DamageType == ModContent.GetInstance<OverallRangedEnergy>())
				{
					RangedNERF++;
				}
				if (player.inventory[i].DamageType == ModContent.GetInstance<OverallSummonEnergy>())
				{
					SummonNERF++;
				}
				if (player.inventory[i].TryGetGlobalItem(out AAGlobalItemModular _) && player.inventory[i].GetGlobalItem<AAGlobalItemModular>().Freedom != -1)
                {
					NERFFREEDOMSTAR++;
				}
			}
			if (MagicNERF >= 2)
            {
				player.AddBuff(ModContent.BuffType<ConflictingEnergy>(), 3);
			}
			if (MeleeNERF >= 2)
            {
				player.AddBuff(ModContent.BuffType<ConflictingEnergy>(), 3);
			}
			if (RangedNERF >= 2)
			{
				player.AddBuff(ModContent.BuffType<ConflictingEnergy>(), 3);
			}
			if (SummonNERF >= 2)
			{
				player.AddBuff(ModContent.BuffType<ConflictingEnergy>(), 3);
			}
			if (NERFFREEDOMSTAR >= 1 && (MagicNERF >= 1 || MeleeNERF >= 1 || RangedNERF >= 2 || SummonNERF >= 1))
            {
				player.AddBuff(ModContent.BuffType<ConflictingEnergy>(), 3);
            }
			if (item.type == ModContent.ItemType<MobianBuster>() && OutputFragment[1 - 1] != 0 && OutputFragment[2 - 1] != 0 && OutputFragment[3 - 1] != 0)
			{
				OutputFragment[1 - 1] = 3;
				OutputFragment[2 - 1] = 1;
				OutputFragment[3 - 1] = 3;
			}
			if (item.type == ModContent.ItemType<LaserBaton>() && OutputFragment[1 - 1] != 0 && OutputFragment[2 - 1] != 0 && OutputFragment[3 - 1] != 0)
			{
				OutputFragment[1 - 1] = 2;
				OutputFragment[2 - 1] = 2;
				OutputFragment[3 - 1] = 2;
			}
		}
    }
}