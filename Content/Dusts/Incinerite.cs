using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Content.Dusts
{
    public class Incinerite : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noLight = false;
            dust.noGravity = true;

            dust.scale = 0.9f;
        }

        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;
            dust.rotation += dust.velocity.X;

            dust.scale -= 0.025f;

            if (dust.scale < 0.3f)
            {
                dust.active = false;
            }

            float strength = dust.scale * 1.4f;

            if (strength > 1f)
            {
                strength = 1f;
            }
            
            Lighting.AddLight(dust.position, new Vector3(0.8f * strength, 0.2f * strength, 0.2f * strength));

            return false;
        }
    }
}