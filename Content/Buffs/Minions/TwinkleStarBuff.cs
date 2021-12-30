using Terraria.ModLoader;
using Terraria;
using AAMod.Content.Projectiles.Minions;

namespace AAMod.Content.Buffs.Minions
{
	public class TwinkleStarBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Twinkle Star");
			Description.SetDefault("'How I wonder what you are...'");

			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			if (player.ownedProjectileCounts[ModContent.ProjectileType<TwinkleStar>()] > 0)
			{
				player.buffTime[buffIndex] = 18000;
			}
			else
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}