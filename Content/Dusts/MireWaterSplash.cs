using Terraria;
using Terraria.ModLoader;

namespace AAMod.Content.Dusts
{
	public class MireWaterSplash : ModDust
    {
		public override void OnSpawn(Dust dust)
		{
			dust.alpha = 170;
			dust.velocity *= 0.5f;
			dust.velocity.Y += 1f;
		}
	}
}
