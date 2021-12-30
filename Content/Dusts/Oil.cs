using Terraria;
using Terraria.ModLoader;

namespace AAMod.Content.Dusts
{
	public class Oil : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noLight = true;
            dust.noGravity = true;

            dust.scale = Main.rand.NextFloat(0.6f, 1f);
        }

        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;
            dust.rotation += dust.velocity.X * 0.1f;

            dust.scale -= 0.025f;

            if (dust.scale < 0.2f)
            {
                dust.active = false;
            }

            return false;
        }
    }
}