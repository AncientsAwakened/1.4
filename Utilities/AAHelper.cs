using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace AAMod.Utilities
{
    public static class AAHelper
    {
        public static Vector2 PolarVector(float radius, float theta) =>
            new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta)) * radius;

        public delegate bool SpecialCondition(NPC possibleTarget);

        //used for homing projectile
        public static bool ClosestNPC(ref NPC target, float maxDistance, Vector2 position,
            bool ignoreTiles = false, int overrideTarget = -1, SpecialCondition specialCondition = null)
        {
            //very advance users can use a delegate to insert special condition into the function like only targeting enemies not currently having local iFrames, but if a special condition isn't added then just return it true
            specialCondition ??= delegate { return true; };

            bool foundTarget = false;
            //If you want to priorities a certain target this is where it's processed, mostly used by minions that have a target priority
            if (overrideTarget != -1 && (Main.npc[overrideTarget].Center - position).Length() < maxDistance)
            {
                target = Main.npc[overrideTarget];
                return true;
            }

            //this is the meat of the targetting logic, it loops through every NPC to check if it is valid the miniomum distance and target selected are updated so that the closest valid NPC is selected
            foreach (NPC npc in Main.npc.Take(Main.maxNPCs))
            {
                float distance = (npc.Center - position).Length();
                if (!(distance < maxDistance) || !npc.active || !npc.chaseable || npc.dontTakeDamage || npc.friendly ||
                    npc.lifeMax <= 5 || NPCID.Sets.TakesDamageFromHostilesWithoutBeingFriendly[npc.type] || npc.immortal ||
                    !Collision.CanHit(position, 0, 0, npc.Center, 0, 0) && !ignoreTiles ||
                    !specialCondition(npc))
                    continue;

                target = npc;
                foundTarget = true;
                maxDistance = (target.Center - position).Length();
            }

            return foundTarget;
        }

        public static bool ClosestNPCToNPC(this NPC npc, ref NPC target, float maxDistance,
            Vector2 position, bool ignoreTiles = false)
        {
            bool foundTarget = false;
            //this is the meat of the targeting logic, it loops through every NPC to check if it is valid the minimum distance and target selected are updated so that the closest valid NPC is selected
            foreach (NPC candidate in Main.npc.Take(Main.maxNPCs))
            {
                float distance = (candidate.Center - position).Length();
                if (!(distance < maxDistance) || !candidate.active || candidate.type == npc.type ||
                    !Collision.CanHit(position, 0, 0, candidate.Center, 0, 0) && !ignoreTiles)
                    continue;

                target = candidate;
                foundTarget = true;
                maxDistance = (target.Center - position).Length();
            }

            return foundTarget;
        }

        //used by minions to give each minion of the same type a unique identifier so they don't stack
        public static int MinionHordeIdentity(Projectile projectile)
        {
            int identity = 0;
            for (int p = 0; p < 1000; p++)
            {
                if (!Main.projectile[p].active || Main.projectile[p].type != projectile.type ||
                    Main.projectile[p].owner != projectile.owner)
                    continue;

                if (projectile.whoAmI == p)
                    break;

                identity++;
            }

            return identity;
        }

        public static void SlowRotation(this ref float currentRotation, float targetAngle, float speed)
        {
            int f = 1; //this is used to switch rotation direction
            float actDirection = new Vector2((float)Math.Cos(currentRotation), (float)Math.Sin(currentRotation))
                .ToRotation();
            targetAngle = new Vector2((float)Math.Cos(targetAngle), (float)Math.Sin(targetAngle)).ToRotation();

            //this makes f 1 or -1 to rotate the shorter distance
            if (Math.Abs(actDirection - targetAngle) > Math.PI)
            {
                f = -1;
            }
            else
            {
                f = 1;
            }

            if (actDirection <= targetAngle + speed * 2 && actDirection >= targetAngle - speed * 2)
            {
                actDirection = targetAngle;
            }
            else if (actDirection <= targetAngle)
            {
                actDirection += speed * f;
            }
            else if (actDirection >= targetAngle)
            {
                actDirection -= speed * f;
            }

            actDirection = new Vector2((float)Math.Cos(actDirection), (float)Math.Sin(actDirection)).ToRotation();
            currentRotation = actDirection;
        }

        public static float AngularDifference(float angle1, float angle2)
        {
            angle1 = PolarVector(1f, angle1).ToRotation();
            angle2 = PolarVector(1f, angle2).ToRotation();
            if (Math.Abs(angle1 - angle2) > Math.PI)
            {
                return (float)Math.PI * 2 - Math.Abs(angle1 - angle2);
            }

            return Math.Abs(angle1 - angle2);
        }

        private static float X(float t,
            float x0, float x1, float x2, float x3)
        {
            return (float)(
                x0 * Math.Pow(1 - t, 3) +
                x1 * 3 * t * Math.Pow(1 - t, 2) +
                x2 * 3 * Math.Pow(t, 2) * (1 - t) +
                x3 * Math.Pow(t, 3)
            );
        }

        private static float Y(float t,
            float y0, float y1, float y2, float y3)
        {
            return (float)(
                y0 * Math.Pow(1 - t, 3) +
                y1 * 3 * t * Math.Pow(1 - t, 2) +
                y2 * 3 * Math.Pow(t, 2) * (1 - t) +
                y3 * Math.Pow(t, 3)
            );
        }

        public static void DrawBezier(SpriteBatch spriteBatch, Texture2D texture, string glowMaskTexture,
            Color drawColor, Vector2 startingPos, Vector2 endPoints, Vector2 c1, Vector2 c2, float chainsPerUse,
            float rotDis)
        {
            for (float i = 0; i <= 1; i += chainsPerUse)
            {
                if (i == 0)
                    continue;

                Vector2 distBetween = new(X(i, startingPos.X, c1.X, c2.X, endPoints.X) -
                                          X(i - chainsPerUse, startingPos.X, c1.X, c2.X, endPoints.X),
                    Y(i, startingPos.Y, c1.Y, c2.Y, endPoints.Y) -
                    Y(i - chainsPerUse, startingPos.Y, c1.Y, c2.Y, endPoints.Y));
                float projTrueRotation = distBetween.ToRotation() - (float)Math.PI / 2 + rotDis;
                spriteBatch.Draw(texture,
                    new Vector2(X(i, startingPos.X, c1.X, c2.X, endPoints.X) - Main.screenPosition.X,
                        Y(i, startingPos.Y, c1.Y, c2.Y, endPoints.Y) - Main.screenPosition.Y),
                    new Rectangle(0, 0, texture.Width, texture.Height), drawColor, projTrueRotation,
                    new Vector2(texture.Width * 0.5f, texture.Height * 0.5f), 1, SpriteEffects.None, 0f);
            }
        }

        public static bool BossActive()
        {
            foreach (NPC npc in Main.npc.Take(Main.maxNPCs))
            {
                if (!npc.active || !npc.boss)
                    continue;

                return true;
            }
            return false;
        }

        public static float GradToRad(float grad) => grad * (float)Math.PI / 180.0f;

        public static Vector2 RandomPosition(Vector2 pos1, Vector2 pos2) =>
            new(Main.rand.Next((int)pos1.X, (int)pos2.X) + 1, Main.rand.Next((int)pos1.Y, (int)pos2.Y) + 1);

        public static int GetNearestAlivePlayer(this NPC npc)
        {
            float nearestPlayerDist = 4815162342f;
            int nearestPlayer = -1;

            foreach (Player player in Main.player)
            {
                if (!(player.Distance(npc.Center) < nearestPlayerDist) || !player.active)
                    continue;

                nearestPlayerDist = player.Distance(npc.Center);
                nearestPlayer = player.whoAmI;
            }

            return nearestPlayer;
        }

        public static int GetNearestAlivePlayer(this Projectile projectile)
        {
            float nearestPlayerDist = 4815162342f;
            int nearestPlayer = -1;
            foreach (Player player in Main.player)
            {
                if (!(player.Distance(projectile.Center) < nearestPlayerDist) || !player.active)
                    continue;

                nearestPlayerDist = player.Distance(projectile.Center);
                nearestPlayer = player.whoAmI;
            }

            return nearestPlayer;
        }

        public static Vector2 GetNearestAlivePlayerVector(this NPC npc)
        {
            float nearestPlayerDist = 4815162342f;
            Vector2 nearestPlayer = Vector2.Zero;

            foreach (Player player in Main.player)
            {
                if (!(player.Distance(npc.Center) < nearestPlayerDist) || !player.active)
                    continue;

                nearestPlayerDist = player.Distance(npc.Center);
                nearestPlayer = player.Center;
            }

            return nearestPlayer;
        }

        public static Vector2 VelocityFptp(Vector2 pos1, Vector2 pos2, float speed)
        {
            Vector2 move = pos2 - pos1;
            return move * (speed / (float)Math.Sqrt(move.X * move.X + move.Y * move.Y));
        }

        public static float RadToGrad(float rad) => rad * 180.0f / (float)Math.PI;

        public static int GetNearestNPC(Vector2 point, bool friendly = false, bool noBoss = false)
        {
            float nearestNPCDist = -1;
            int nearestNPC = -1;

            foreach (NPC npc in Main.npc.Take(Main.maxNPCs))
            {
                if (!npc.active)
                    continue;

                if (noBoss && npc.boss)
                    continue;

                if (!friendly && (npc.friendly || npc.lifeMax <= 5))
                    continue;

                if (nearestNPCDist != -1 && !(npc.Distance(point) < nearestNPCDist))
                    continue;

                nearestNPCDist = npc.Distance(point);
                nearestNPC = npc.whoAmI;
            }

            return nearestNPC;
        }

        public static int GetNearestPlayer(Vector2 point, bool alive = false)
        {
            float nearestPlayerDist = -1;
            int nearestPlayer = -1;

            foreach (Player player in Main.player)
            {
                if (alive && (!player.active || player.dead))
                    continue;

                if (nearestPlayerDist != -1 && !(player.Distance(point) < nearestPlayerDist))
                    continue;

                nearestPlayerDist = player.Distance(point);
                nearestPlayer = player.whoAmI;
            }

            return nearestPlayer;
        }

        public static Vector2 VelocityToPoint(Vector2 a, Vector2 b, float speed)
        {
            Vector2 move = b - a;
            return move * (speed / (float)Math.Sqrt(move.X * move.X + move.Y * move.Y));
        }

        public static Vector2 RandomPointInArea(Vector2 a, Vector2 b) =>
            new(Main.rand.Next((int)a.X, (int)b.X) + 1, Main.rand.Next((int)a.Y, (int)b.Y) + 1);

        public static Vector2 RandomPointInArea(Rectangle area) =>
            new(Main.rand.Next(area.X, area.X + area.Width), Main.rand.Next(area.Y, area.Y + area.Height));

        public static float RotateBetween2Points(Vector2 a, Vector2 b) => (float)Math.Atan2(a.Y - b.Y, a.X - b.X);

        public static Vector2 CenterPoint(Vector2 a, Vector2 b) => new((a.X + b.X) / 2.0f, (a.Y + b.Y) / 2.0f);

        public static Vector2 PolarPos(Vector2 point, float distance, float angle, int xOffset = 0, int yOffset = 0)
        {
            Vector2 returnedValue = new()
            {
                X = distance * (float)Math.Sin(RadToGrad(angle)) + point.X + xOffset,
                Y = distance * (float)Math.Cos(RadToGrad(angle)) + point.Y + yOffset
            };

            return returnedValue;
        }

        public static bool Chance(float chance) => Main.rand.NextFloat() <= chance;
        public static bool GenChance(float chance) => WorldGen.genRand.NextFloat() <= chance;

        public static Vector2 SmoothFromTo(Vector2 from, Vector2 to, float smooth = 60f) => from + (to - from) / smooth;

        public static float DistortFloat(float @float, float percent)
        {
            float distortNumber = @float * percent;
            int counter = 0;

            while (distortNumber.ToString(CultureInfo.InvariantCulture).Split(',').Length > 1)
            {
                distortNumber *= 10;
                counter++;
            }

            return @float + Main.rand.Next(0, (int)distortNumber + 1) / (float)Math.Pow(10, counter) *
                (Main.rand.Next(2) == 0 ? -1 : 1);
        }

        public static Vector2 FoundPosition(Vector2 tilePos)
        {
            Vector2 screen = new(Main.screenWidth / 2f, Main.screenHeight / 2f);
            Vector2 fullScreen = tilePos - Main.mapFullscreenPos;
            fullScreen *= Main.mapFullscreenScale / 16;
            fullScreen = fullScreen * 16 + screen;
            Vector2 draw = new((int)fullScreen.X, (int)fullScreen.Y);
            return draw;
        }

        public static void MoveTowards(this NPC npc, Vector2 playerTarget, float speed, float turnResistance)
        {
            Vector2 move = playerTarget - npc.Center;
            float length = move.Length();

            if (length > speed)
                move *= speed / length;

            move = (npc.velocity * turnResistance + move) / (turnResistance + 1f);
            length = move.Length();

            if (length > speed)
                move *= speed / length;

            npc.velocity = move;
        }

        public static bool NextBool(this UnifiedRandom rand, int chance, int total) => rand.Next(total) < chance;

        public static Vector2 Spread(float xy) =>
            new(Main.rand.NextFloat(-xy, xy - 1), Main.rand.NextFloat(-xy, xy - 1));

        public static Vector2 SpreadUp(float xy) => new(Main.rand.NextFloat(-xy, xy - 1), Main.rand.NextFloat(-xy, 0));

        public static void CreateDust(Player player, int dust, int count)
        {
            for (int i = 0; i < count; i++)
                Dust.NewDust(player.position, player.width, player.height / 2, dust);
        }

        public static Vector2 RotateVector(Vector2 origin, Vector2 vecToRot, float rot) =>
            new((float)(Math.Cos(rot) * (vecToRot.X - (double)origin.X) -
                         Math.Sin(rot) * (vecToRot.Y - (double)origin.Y)) +
                origin.X, (float)(Math.Sin(rot) * (vecToRot.X - (double)origin.X) +
                                   Math.Cos(rot) * (vecToRot.Y - (double)origin.Y)) + origin.Y);

        public static bool Contains(this Rectangle rect, Vector2 pos) => rect.Contains((int)pos.X, (int)pos.Y);

        public static bool AnyProjectiles(int type)
        {
            for (int i = 0; i < Main.maxProjectiles; i++)
                if (Main.projectile[i].active && Main.projectile[i].type == type)
                    return true;

            return false;
        }

        public static int CountProjectiles(int type)
        {
            int p = 0;

            for (int i = 0; i < Main.maxProjectiles; i++)
                if (Main.projectile[i].active && Main.projectile[i].type == type)
                    p++;

            return p;
        }

        public static Vector2 GetOrigin(Texture2D tex, int frames = 1)
        {
            return new(tex.Width / 2f, tex.Height / frames / 2);
        }

        public static Vector2 GetOrigin(Rectangle rect, int frames = 1)
        {
            return new(rect.Width / 2f, rect.Height / frames / 2f);
        }

        #region NPC Methods

        /// <summary>
        /// For methods that have 'this NPC npc', instead of doing TTHelper.Shoot(), you can do npc.Shoot() instead.
        /// For shooting projectiles easier. 'aimed' will make the projectile shoot at the target without the extra code, if thats true, also set 'speed'.
        /// 'speed' and 'spread' is only needed if 'aimed' is true. 'spread' is optional.
        /// Example: npc.Shoot(npc.Center, ModContent.ProjectileType<Bullet>(), 40, new Vector2(-5, 0), false, false, SoundID.Item1);
        /// </summary>
        public static void Shoot(this NPC npc, Vector2 position, int projType, int damage, Vector2 velocity,
            bool customSound, LegacySoundStyle sound, string soundString = "", float ai0 = 0, float ai1 = 0)
        {
            Mod mod = AAMod.Instance;
            if (customSound && !Main.dedServ)
                SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(mod, soundString), npc.position);
            else
                SoundEngine.PlaySound(sound, (int)npc.position.X, (int)npc.position.Y);

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Projectile.NewProjectile(npc.GetProjectileSpawnSource(), position, velocity, projType, damage / 4, 0,
                    Main.myPlayer, ai0, ai1);
            }
        }

        /// <summary>
        /// For spawning NPCs from NPCs without any extra stuff.
        /// </summary>
        public static void SpawnNPC(int posX, int posY, int npcType, float ai0 = 0, float ai1 = 0, float ai2 = 0,
            float ai3 = 0)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                int index = NPC.NewNPC(posX, posY, npcType, 0, ai0, ai1, ai2, ai3);
                if (Main.netMode == NetmodeID.Server && index < Main.maxNPCs)
                {
                    NetMessage.SendData(MessageID.SyncNPC, number: index);
                }
            }
        }

        /// <summary>
        /// A simple Dash method for npcs charging at the player, use npc.Dash(20, false); for example.
        /// </summary>
        public static void Dash(this NPC npc, int speed, bool directional, LegacySoundStyle sound,
            Vector2 target)
        {
            Player player = Main.player[npc.target];
            SoundEngine.PlaySound(sound, (int)npc.position.X, (int)npc.position.Y);
            if (target == Vector2.Zero)
            {
                target = player.Center;
            }

            if (directional)
            {
                npc.velocity = npc.DirectionTo(target) * speed;
            }
            else
            {
                npc.velocity.X = target.X > npc.Center.X ? speed : -speed;
            }
        }

        /// <summary>
        /// Makes the npc flip to the direction of the player. npc.LookAtPlayer();
        /// </summary>
        public static void LookAtEntity(this NPC npc, Entity target, bool opposite = false)
        {
            int dir = 1;
            if (opposite)
                dir = -1;
            if (target.Center.X > npc.Center.X)
            {
                npc.spriteDirection = dir;
                npc.direction = dir;
            }
            else
            {
                npc.spriteDirection = -dir;
                npc.direction = -dir;
            }
        }

        public static void LookAtEntity(this Projectile projectile, Entity target, bool opposite = false)
        {
            int dir = 1;
            if (opposite)
                dir = -1;
            if (target.Center.X > projectile.Center.X)
            {
                projectile.spriteDirection = dir;
                projectile.direction = dir;
            }
            else
            {
                projectile.spriteDirection = -dir;
                projectile.direction = -dir;
            }
        }

        /// <summary>
        /// Makes the npc flip to the direction of it's X velocity. npc.LookByVelocity();
        /// </summary>
        public static void LookByVelocity(this NPC npc)
        {
            if (npc.velocity.X > 0)
            {
                npc.spriteDirection = 1;
                npc.direction = 1;
            }
            else if (npc.velocity.X < 0)
            {
                npc.spriteDirection = -1;
                npc.direction = -1;
            }
        }
        public static void LookByVelocity(this Projectile projectile)
        {
            if (projectile.velocity.X > 0)
            {
                projectile.spriteDirection = 1;
                projectile.direction = 1;
            }
            else if (projectile.velocity.X < 0)
            {
                projectile.spriteDirection = -1;
                projectile.direction = -1;
            }
        }

        /// <summary>
        /// Moves the npc to a position without turn resistance. npc.MoveToVector2(new Vector2(player.Center + 100, player.Center - 30), 10);
        /// </summary>
        public static void MoveToVector2(this NPC npc, Vector2 p, float moveSpeed)
        {
            float velMultiplier = 1f;
            Vector2 dist = p - npc.Center;
            float length = dist == Vector2.Zero ? 0f : dist.Length();
            if (length < moveSpeed)
            {
                velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
            }

            if (length < 100f)
            {
                moveSpeed *= 0.5f;
            }

            if (length < 50f)
            {
                moveSpeed *= 0.5f;
            }

            npc.velocity = length == 0f ? Vector2.Zero : Vector2.Normalize(dist);
            npc.velocity *= moveSpeed;
            npc.velocity *= velMultiplier;
        }

        public static void MoveToVector2(this Projectile projectile, Vector2 p, float moveSpeed)
        {
            float velMultiplier = 1f;
            Vector2 dist = p - projectile.Center;
            float length = dist == Vector2.Zero ? 0f : dist.Length();
            if (length < moveSpeed)
            {
                velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
            }

            if (length < 100f)
            {
                moveSpeed *= 0.5f;
            }

            if (length < 50f)
            {
                moveSpeed *= 0.5f;
            }

            projectile.velocity = length == 0f ? Vector2.Zero : Vector2.Normalize(dist);
            projectile.velocity *= moveSpeed;
            projectile.velocity *= velMultiplier;
        }

        /// <summary>
        /// Moves the npc to a Vector2.
        /// The lower the turnResistance, the less time it takes to adjust direction.
        /// Example: npc.MoveToPlayer(new Vector2(100, 0), 10, 14);
        /// toPlayer makes the vector consider the player.Center for you.
        /// </summary>
        public static void Move(this NPC npc, Vector2 vector, float speed, float turnResistance = 10f,
            bool toPlayer = false)
        {
            Player player = Main.player[npc.target];
            Vector2 moveTo = toPlayer ? player.Center + vector : vector;
            Vector2 move = moveTo - npc.Center;
            float magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }

            move = (npc.velocity * turnResistance + move) / (turnResistance + 1f);
            magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }

            npc.velocity = move;
        }

        public static void Move(this Projectile projectile, Vector2 vector, float speed, float turnResistance = 10f,
            bool toPlayer = false)
        {
            Player player = Main.player[projectile.owner];
            Vector2 moveTo = toPlayer ? player.Center + vector : vector;
            Vector2 move = moveTo - projectile.Center;
            float magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }

            move = (projectile.velocity * turnResistance + move) / (turnResistance + 1f);
            magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }

            projectile.velocity = move;
        }

        public static void MoveToNPC(this NPC npc, NPC target, Vector2 vector, float speed,
            float turnResistance = 10f)
        {
            Vector2 moveTo = target.Center + vector;
            Vector2 move = moveTo - npc.Center;
            float magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }

            move = (npc.velocity * turnResistance + move) / (turnResistance + 1f);
            magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }

            npc.velocity = move;
        }

        public static float Magnitude(Vector2 mag) // For the Move code above
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }

        /// <summary>
        /// Sight method for NPCs.
        /// </summary>
        /// <param name="range">Sets how close the target would need to be before the Sight is true.</param>
        /// <param name="lineOfSight">Sets if Sight can be blocked by the target standing behind tiles.</param>
        /// <param name="facingTarget">Sets if Sight requires the NPC to face the target's direction.</param>
        /// <param name="canSeeHiding">Sets if the enemy can't see invisible players or enemies.</param>
        /// <param name="blind">Sets if the enemy can't see the target if they don't move much.</param>
        /// <param name="moveThreshold">Sets how much velocity is needed before being detectable, use if 'blind' is true.</param>
        public static bool Sight(this NPC npc, Entity codable, float range = -1, bool facingTarget = true,
            bool lineOfSight = false, bool canSeeHiding = false, bool blind = false, float moveThreshold = 2)
        {
            if (codable == null || !codable.active || (codable is Player && (codable as Player).dead))
                return false;

            if (!canSeeHiding && codable is Player && (codable as Player).invis)
                return false;
            if (blind && codable.velocity.Length() <= moveThreshold)
                return false;

            if (lineOfSight)
            {
                if (!Collision.CanHit(npc.position, npc.width, npc.height, codable.position, codable.width, codable.height))
                    return false;
            }

            if (range >= 0)
            {
                if (npc.DistanceSQ(codable.Center) > range * range)
                    return false;
            }

            if (facingTarget)
            {
                if (npc.DistanceSQ(codable.Center) <= (codable.width + 32) * (codable.width + 32))
                    return true;

                return npc.Center.X > codable.Center.X && npc.spriteDirection == -1 ||
                       npc.Center.X < codable.Center.X && npc.spriteDirection == 1;
            }

            return true;
        }

        /// <summary>
        /// Checks if the npc can fall through platforms.
        /// </summary>
        /// <param name="canJump">Bool for if it can fall, set this to a bool in the npc.</param>
        /// <param name="yOffset">Offset so the npc doesnt fall when the player is on the same plaform as it.</param>
        public static void JumpDownPlatform(this NPC npc, ref bool canJump, int yOffset = 12)
        {
            Player player = Main.player[npc.target];
            Point tile = npc.Bottom.ToTileCoordinates();
            if (Main.tileSolidTop[Framing.GetTileSafely(tile.X, tile.Y).type] && Main.tile[tile.X, tile.Y].IsActive &&
                npc.Center.Y + yOffset < player.Center.Y)
            {
                Point tile2 = npc.BottomRight.ToTileCoordinates();
                canJump = true;
                if (Main.tile[tile.X - 1, tile.Y - 1].IsActiveUnactuated &&
                    Main.tileSolid[Framing.GetTileSafely(tile.X - 1, tile.Y - 1).type] ||
                    Main.tile[tile2.X + 1, tile2.Y - 1].IsActiveUnactuated &&
                    Main.tileSolid[Framing.GetTileSafely(tile2.X + 1, tile2.Y - 1).type] || npc.collideX)
                {
                    npc.velocity.X = 0;
                }
            }
        }

        public static void JumpDownPlatform(this NPC npc, Vector2 vector, ref bool canJump, int yOffset = 12)
        {
            Point tile = npc.Bottom.ToTileCoordinates();
            if (Main.tileSolidTop[Framing.GetTileSafely(tile.X, tile.Y).type] && Main.tile[tile.X, tile.Y].IsActive &&
                npc.Center.Y + yOffset < vector.Y)
            {
                Point tile2 = npc.BottomRight.ToTileCoordinates();
                canJump = true;
                if (Main.tile[tile.X - 1, tile.Y - 1].IsActiveUnactuated &&
                    Main.tileSolid[Framing.GetTileSafely(tile.X - 1, tile.Y - 1).type] ||
                    Main.tile[tile2.X + 1, tile2.Y - 1].IsActiveUnactuated &&
                    Main.tileSolid[Framing.GetTileSafely(tile2.X + 1, tile2.Y - 1).type] || npc.collideX)
                {
                    npc.velocity.X = 0;
                }
            }
        }

        /// <summary>
        /// Checks for if the npc has any buffs on it.
        /// </summary>
        public static bool NPCHasAnyBuff(this NPC npc)
        {
            for (int i = 0; i < BuffLoader.BuffCount; i++)
            {
                if (npc.HasBuff(i))
                {
                    return true;
                }
            }

            return false;
        }

        public static Vector2 FindGround(this NPC npc, int range, Func<int, int, bool> canTeleportTo = null)
        {
            int tileX = (int)npc.position.X / 16;
            int tileY = (int)npc.position.Y / 16;
            int teleportCheckCount = 0;
            while (teleportCheckCount < 100)
            {
                teleportCheckCount++;
                int tpTileX = Main.rand.Next(tileX - range, tileX + range);
                int tpTileY = Main.rand.Next(tileY - range, tileY + range);
                for (int tpY = tpTileY; tpY < tileY + range; tpY++)
                {
                    if ((tpY < tileY - 4 || tpY > tileY + 4 || tpTileX < tileX - 4 || tpTileX > tileX + 4) &&
                        (tpY < tileY - 1 || tpY > tileY + 1 || tpTileX < tileX - 1 || tpTileX > tileX + 1) &&
                        Main.tile[tpTileX, tpY].IsActiveUnactuated)
                    {
                        if (canTeleportTo != null && canTeleportTo(tpTileX, tpY) ||
                            Main.tile[tpTileX, tpY - 1].LiquidType != 2 &&
                            Main.tileSolid[Main.tile[tpTileX, tpY].type] &&
                            !Collision.SolidTiles(tpTileX - 1, tpTileX + 1, tpY - 4, tpY - 1))
                        {
                            return new Vector2(tpTileX, tpY);
                        }
                    }
                }
            }
            return new Vector2(npc.Center.X, npc.Center.Y);
        }

        public static Vector2 FindGroundPlayer(this NPC npc, int distFromPlayer, Func<int, int, bool> canTeleportTo = null)
        {
            int playerTileX = (int)Main.player[npc.target].position.X / 16;
            int playerTileY = (int)Main.player[npc.target].position.Y / 16;
            int tileX = (int)npc.position.X / 16;
            int tileY = (int)npc.position.Y / 16;
            int teleportCheckCount = 0;

            while (teleportCheckCount < 1000)
            {
                teleportCheckCount++;
                int tpTileX = Main.rand.Next(playerTileX - distFromPlayer, playerTileX + distFromPlayer);
                int tpTileY = Main.rand.Next(playerTileY - distFromPlayer, playerTileY + distFromPlayer);
                for (int tpY = tpTileY; tpY < playerTileY + distFromPlayer; tpY++)
                {
                    if ((tpY < playerTileY - 4 || tpY > playerTileY + 4 || tpTileX < playerTileX - 4 || tpTileX > playerTileX + 4) &&
                        (tpY < tileY - 1 || tpY > tileY + 1 || tpTileX < tileX - 1 || tpTileX > tileX + 1) &&
                        Main.tile[tpTileX, tpY].IsActiveUnactuated)
                    {
                        if (canTeleportTo != null && canTeleportTo(tpTileX, tpY) ||
                            Main.tile[tpTileX, tpY - 1].LiquidType != 2 &&
                            Main.tileSolid[Main.tile[tpTileX, tpY].type] &&
                            !Collision.SolidTiles(tpTileX - 1, tpTileX + 1, tpY - 4, tpY - 1))
                        {
                            return new Vector2(tpTileX, tpY) * 16;
                        }
                    }
                }
            }
            return new Vector2(npc.Center.X, npc.Center.Y);
        }

        public static void LockMoveRadius(this NPC npc, Player player, int radius = 1000)
        {
            if (npc.Distance(player.Center) > radius)
            {
                Vector2 movement = npc.Center - player.Center;
                float difference = movement.Length() - radius;
                movement.Normalize();
                movement *= difference < 17f ? difference : 17f;
                player.position += movement;
            }
        }
        #endregion
    }
}
