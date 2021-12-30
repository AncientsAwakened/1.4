using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace AAMod.Content.EnergyDamageClass
{
    class OverallMeleeEnergy : DamageClass
    {
        public override void SetStaticDefaults()
        {
            ClassName.SetDefault("Energy damage (Melee damage)");
        }
        protected override float GetBenefitFrom(DamageClass damageClass)
        {
            if (damageClass == Melee)
            {
                return 1.1f;
            }
            return 0f;
        }
    }
}
