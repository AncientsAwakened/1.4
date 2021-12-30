using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.Generation;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace AAMod.Common.Systems
{
    class WorldSystem : ModSystem
    {
        /*public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int BiomesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Micro Biomes"));
            if (BiomesIndex != -1)
            {
                tasks.Insert(BiomesIndex + 1, new PassLegacy("Biome Loader", AABiomes));
            }
        }

        private void AABiomes(GenerationProgress progress, GameConfiguration configuration)
        {
            
        }*/
    }
}
