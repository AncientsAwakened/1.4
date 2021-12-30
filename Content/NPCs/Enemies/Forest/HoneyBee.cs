using AAMod.Content.Items.Consumables;
using AAMod.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace AAMod.Content.NPCs.Enemies.Forest
{
    public class HoneyBee : ModNPC
    {
        public enum ActionState
        {
            Begin,
            Move,
            Pause,
            Alert
        }
        // Setting NPC.ai[0] as an ActionState that was created above
        public ActionState AIState
        {
            get => (ActionState)NPC.ai[0];
            set => NPC.ai[0] = (int)value;
        }
        // This and the one below is just giving the 2 floats 'NPC.ai[1]' and 'NPC.ai[2]' different names for easier readability
        public ref float AITimer => ref NPC.ai[1];
        public ref float TimerRand => ref NPC.ai[2];

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Honey Bee");
            Main.npcFrameCount[NPC.type] = 3;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 40;
            NPC.damage = 10;
            NPC.defense = 2;

            NPC.width = 26;
            NPC.height = 18;

            NPC.value = Item.sellPrice(copper: 20);

            NPC.aiStyle = -1;

            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;

            NPC.noGravity = true;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
               BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
                new FlavorTextBestiaryInfoElement("chonker.")
            });
        }
        private Vector2 origin;
        private Vector2 moveTo;
        public override void AI()
        {
            Player player = Main.player[NPC.target]; // Makes a Player called player that is set as the npc's target, used for referencing the npc's target
            NPC.TargetClosest();
            NPC.LookByVelocity(); // From Helper method I made, makes the npc face the direction of it's velocity

            switch (AIState) // Switches are like if statements but not. Cleaner, but can't do stuff like < or > or != etc. just setting a case.
            {
                // All number variables default as 0 unless specified, ActionState is 0 (aka Begin) so this'll run the moment the npc spawns
                case ActionState.Begin:
                    // This sets the origin Vector2 as the NPC's center, since this only runs once, it won't change as the npc moves.
                    origin = NPC.Center;
                    SetMoveTo(); // Since the same code is used 3 times, I made it its own method to reduce file size.
                    // Sets the AIState as Move, which'll change the switch case to the one below, making this one only run in 1 frame
                    AIState = ActionState.Move;
                    NPC.netUpdate = true; // I just put this below stuff whenever I change a lot of variables at once
                    break;
                case ActionState.Move:
                    if (NPC.DistanceSQ(moveTo) < 10 * 10 || AITimer++ > 180 || NPC.collideX || NPC.collideY) // If the npc is 10 pixels close to moveTo's Vector2 OR if 3 seconds have passed, the if statement will run.
                    {
                        AITimer = 0; // Resets AITimer to 0, which'll be used in the Pause ActionState
                        TimerRand = Main.rand.Next(30, 121); // Sets TimerRand to a random number between 30-120 (121 is intentional)
                        AIState = ActionState.Pause; // Changes the switch's case to ActionState.Pause, the one below
                        NPC.netUpdate = true;
                    }
                    else
                        NPC.Move(moveTo, 1.5f, 20); // Move is a thing from my helper, it smoothly moves the npc to a Vector2, with 2nd param being speed and 3rd being turn resistance (Higher turn resistance means slower it takes to get to max speed)

                    if (NPC.Sight(player, 200, false, true)) // Sight is a thing from my helper, check the params, 200 is the view distance
                    {
                        AITimer = 0; // Resets stuff
                        TimerRand = 0;
                        AIState = ActionState.Alert; // Changes to case ActionState.Alert
                    }
                    break;
                case ActionState.Pause:
                    NPC.velocity *= 0.98f; // This runs every frame meaning it multiplies by 0.98 every frame making the npc slow down smoothly.
                    if (AITimer++ > TimerRand) // AITimer++ is the same as 'AITimer += 1' but cleaner, if AITimer is greater than TimerRand (Which was set back in the Move case) then do stuff.
                    {
                        SetMoveTo();
                        AIState = ActionState.Move; // Sets case back to Move
                        NPC.netUpdate = true;
                    }
                    break;
                case ActionState.Alert:
                    // Moves npc to player's center, which is what moveTo was set to back in Move, this time speed is 2 so it'll move a bit faster.
                    NPC.Move(player.Center, 4, 20);
                    if (!NPC.Sight(player, 400, false, true)) // If NPC can no longer see the player or player goes 400 pixels away from it, do stuff.
                    {
                        origin = NPC.Center; // Sets the origin to where they are when they stopped chasing the player
                        SetMoveTo();
                        AIState = ActionState.Move;
                        NPC.netUpdate = true;
                    }
                    break;
            }
        }
        public void SetMoveTo()
        {
            bool foundPos = false;
            int attempts = 0;
            while (!foundPos && attempts <= 1000)
            {
                attempts++;
                // This sets the moveTo Vector2 as a random position around origin, in a radius of 300. (Main.rand.Next() is used for random numbers)
                moveTo = origin + new Vector2(Main.rand.Next(-300, 300), Main.rand.Next(-300, 300));

                int tilePosY = GetFirstTileFloor((int)(moveTo.X / 16), (int)(moveTo.Y / 16)); // GetFirstTileFloor gets the tile coords of the first solid tile under the npc (aka the ground)
                int dist = (tilePosY * 16) - (int)moveTo.Y; // dist is the distance between the npc and the ground

                if (!Collision.CanHit(NPC.Center, NPC.width, NPC.height, moveTo, NPC.width, NPC.height) || dist < 60) // Check for if theres any tiles blocking the npc's way to moveTo OR if dist is less than 60
                    continue; // This repeats whatever is inside the while statement, basically a loop

                foundPos = true;
            }
        }
        public static int GetFirstTileFloor(int x, int startY, bool solid = true)
        {
            if (!WorldGen.InWorld(x, startY)) return startY;
            for (int y = startY; y < Main.maxTilesY - 10; y++)
            {
                Tile tile = Framing.GetTileSafely(x, y);
                if (tile is { IsActive: true } && (!solid || Main.tileSolid[tile.type])) { return y; }
            }
            return Main.maxTilesY - 10;
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.rotation = NPC.velocity.X * 0.05f; // Makes the npc tilt based on how fast it moves on the x axis
            NPC.spriteDirection = NPC.direction;
            if (++NPC.frameCounter > 3)
            {
                NPC.frame.Y += frameHeight;
                NPC.frameCounter = 0;
                if (NPC.frame.Y > 2 * frameHeight)
                    NPC.frame.Y = 0;
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<HoneycombChunk>(), 1, 1));
        }

        public override void OnHitPlayer(Player player, int damage, bool crit) => player.AddBuff(BuffID.Poisoned, 180);

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            float baseChance = SpawnCondition.OverworldDay.Chance;
            float multiplier = Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type == TileID.Grass ? 0.25f : 0f;

            return baseChance * multiplier;
        }

    }
}
