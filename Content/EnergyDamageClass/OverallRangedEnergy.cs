using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace AAMod.Content.EnergyDamageClass
{
    class OverallRangedEnergy : DamageClass
    {
        public override void SetStaticDefaults()
        {
            ClassName.SetDefault("Energy damage (Ranged damage)");
        }
        protected override float GetBenefitFrom(DamageClass damageClass)
        {
            if (damageClass == Ranged)
            {
                return 1.1f;
            }
            return 0f;
        }
    }
}
