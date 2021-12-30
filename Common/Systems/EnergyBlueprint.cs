using AAMod.Common.Globals;
using AAMod.Common.Systems.UI;
using AAMod.Content.Items.Materials.Energy;
using AAMod.Content.Items.Weapons.FreedomStars;
using AAMod.Content.Items.Weapons.Energy;
using AAMod.Content.Tiles.Energy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using ReLogic.Content;

namespace AAMod.Common.Systems
{
    class EnergyBlueprint : UIState
    {
        #region DoNotTouch
        private CoolantUI coolant;
        private OverclockUI overclock;
        private PowercellUI powercell;
        private ScrapUI scrap;
        private ShardUI shard;
        private Fragment1UI fragment1;
        private Fragment2UI fragment2;
        private Fragment3UI fragment3;
        public OutputUI output;
        public override void OnInitialize()
        {
            coolant = new CoolantUI(ItemSlot.Context.BankItem, 1f)
            {
                Left = { Pixels = 1040 },
                Top = { Pixels = 350 },
                Width = { Pixels = 32 },
                Height = { Pixels = 32 },
                CoolantFunc = item => item.IsAir || !item.IsAir && item.GetGlobalItem<AAGlobalItemModular>().Coolant && !item.favorited
            };
            Append(coolant);
            overclock = new OverclockUI(ItemSlot.Context.BankItem, 1f)
            {
                Left = { Pixels = 480 },
                Top = { Pixels = 450 },
                Width = { Pixels = 32 },
                Height = { Pixels = 32 },
                OverclockFunc = item => item.IsAir || !item.IsAir && item.GetGlobalItem<AAGlobalItemModular>().Overclock && !item.favorited
            };
            Append(overclock);
            powercell = new PowercellUI(ItemSlot.Context.BankItem, 1f)
            {
                Left = { Pixels = 760 },
                Top = { Pixels = 480 },
                Width = { Pixels = 32 },
                Height = { Pixels = 32 },
                PowercellFunc = item => item.IsAir || !item.IsAir && item.GetGlobalItem<AAGlobalItemModular>().Powercell && !item.favorited
            };
            Append(powercell);
            scrap = new ScrapUI(this, ItemSlot.Context.BankItem, 1f)
            {
                Left = { Pixels = 460 },
                Top = { Pixels = 370 },
                Width = { Pixels = 32 },
                Height = { Pixels = 32 },
                ScrapFunc = item => item.IsAir || !item.IsAir && item.GetGlobalItem<AAGlobalItemModular>().Scrap && !item.favorited && (!output.Item.TryGetGlobalItem(out AAGlobalItemModular _) || (output.Item.TryGetGlobalItem(out AAGlobalItemModular _) && output.Item.GetGlobalItem<AAGlobalItemModular>().Freedom == -1 && output.Item.type != ModContent.ItemType<FreedomStarBusted>()))
            };
            Append(scrap);
            shard = new ShardUI(this, ItemSlot.Context.BankItem, 1f)
            {
                Left = { Pixels = 460 },
                Top = { Pixels = 320 },
                Width = { Pixels = 32 },
                Height = { Pixels = 32 },
                ShardFunc = item => item.IsAir || !item.IsAir && item.GetGlobalItem<AAGlobalItemModular>().Shard && !item.favorited && (!output.Item.TryGetGlobalItem(out AAGlobalItemModular _) || (output.Item.TryGetGlobalItem(out AAGlobalItemModular _) && output.Item.GetGlobalItem<AAGlobalItemModular>().Freedom == -1 && output.Item.type != ModContent.ItemType<FreedomStarBusted>()))
            };
            Append(shard);
            fragment1 = new Fragment1UI(this, ItemSlot.Context.BankItem, 1f)
            {
                Left = { Pixels = 695 },
                Top = { Pixels = 400 },
                Width = { Pixels = 32 },
                Height = { Pixels = 32 },
                FragmentFunc = item => item.IsAir || !item.IsAir && item.GetGlobalItem<AAGlobalItemModular>().Fragment && !item.favorited && (!output.Item.TryGetGlobalItem(out AAGlobalItemModular _) || (output.Item.TryGetGlobalItem(out AAGlobalItemModular _) && output.Item.GetGlobalItem<AAGlobalItemModular>().Freedom == -1 && output.Item.type != ModContent.ItemType<FreedomStarBusted>()))
            };
            Append(fragment1);
            fragment2 = new Fragment2UI(this, ItemSlot.Context.BankItem, 1f)
            {
                Left = { Pixels = 760 },
                Top = { Pixels = 400 },
                Width = { Pixels = 32 },
                Height = { Pixels = 32 },
                FragmentFunc = item => item.IsAir || !item.IsAir && item.GetGlobalItem<AAGlobalItemModular>().Fragment && !item.favorited && (!output.Item.TryGetGlobalItem(out AAGlobalItemModular _) || (output.Item.TryGetGlobalItem(out AAGlobalItemModular _) && output.Item.GetGlobalItem<AAGlobalItemModular>().Freedom == -1 && output.Item.type != ModContent.ItemType<FreedomStarBusted>()))
            };
            Append(fragment2);
            fragment3 = new Fragment3UI(this, ItemSlot.Context.BankItem, 1f)
            {
                Left = { Pixels = 825 },
                Top = { Pixels = 400 },
                Width = { Pixels = 32 },
                Height = { Pixels = 32 },
                FragmentFunc = item => item.IsAir || !item.IsAir && item.GetGlobalItem<AAGlobalItemModular>().Fragment && !item.favorited && (!output.Item.TryGetGlobalItem(out AAGlobalItemModular _) || (output.Item.TryGetGlobalItem(out AAGlobalItemModular _) && output.Item.GetGlobalItem<AAGlobalItemModular>().Freedom == -1 && output.Item.type != ModContent.ItemType<FreedomStarBusted>()))
            };
            Append(fragment3);
            output = new OutputUI(ItemSlot.Context.BankItem, 1f)
            {
                Left = { Pixels = 1040 },
                Top = { Pixels = 480 },
                Width = { Pixels = 32 },
                Height = { Pixels = 32 },
                OutputFunc = item => item.IsAir || !item.IsAir && item.GetGlobalItem<AAGlobalItemModular>().OutputWeapon && !item.favorited && !item.GetGlobalItem<AAGlobalItemModular>().CheatedIn && ((coolant.Item.IsAir || item.GetGlobalItem<AAGlobalItemModular>().OutputCoolant == -1) && (overclock.Item.IsAir || item.GetGlobalItem<AAGlobalItemModular>().OutputOverclock == -1) && (powercell.Item.IsAir || item.GetGlobalItem<AAGlobalItemModular>().OutputPowercell == -1) && (fragment1.Item.IsAir || item.GetGlobalItem<AAGlobalItemModular>().OutputFragment[1 - 1] == 0) && (fragment2.Item.IsAir || item.GetGlobalItem<AAGlobalItemModular>().OutputFragment[2 - 1] == 0) && (fragment3.Item.IsAir || item.GetGlobalItem<AAGlobalItemModular>().OutputFragment[3 - 1] == 0) && (shard.Item.IsAir || item.GetGlobalItem<AAGlobalItemModular>().OutputShard == 0)) || ((item.GetGlobalItem<AAGlobalItemModular>().Freedom != -1 || item.type == ModContent.ItemType<FreedomStarBusted>()) && scrap.Item.IsAir && shard.Item.IsAir && fragment1.Item.IsAir && fragment2.Item.IsAir && fragment3.Item.IsAir && (powercell.Item.IsAir || item.GetGlobalItem<AAGlobalItemModular>().OutputPowercell == -1) && (overclock.Item.IsAir || item.GetGlobalItem<AAGlobalItemModular>().OutputOverclock == -1) && (coolant.Item.IsAir || item.GetGlobalItem<AAGlobalItemModular>().OutputCoolant == -1))
            };
            Append(output);
        }
        private bool Created;
        private bool Modifying;
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (!Main.LocalPlayer.IsTileTypeInInteractionRange(ModContent.TileType<Blueprint>()))
            {
                UISystem system = ModContent.GetInstance<UISystem>();
                system.DeactivateUI<EnergyBlueprint>();
            }
        }
        public override void OnDeactivate()
        {
            if (!output.Item.IsAir && (output.Item.GetGlobalItem<AAGlobalItemModular>().Freedom != -1 || output.Item.type == ModContent.ItemType<FreedomStarBusted>()))
            {
                output.Item.TurnToAir();
                output.Item = new Item();
                output.Item.SetDefaults(ModContent.ItemType<FreedomStarBusted>());
                Main.LocalPlayer.QuickSpawnClonedItem(output.Item, output.Item.stack);
                output.Item.TurnToAir();
            }
            if (!coolant.Item.IsAir)
            {
                Main.LocalPlayer.QuickSpawnClonedItem(coolant.Item, coolant.Item.stack);
                coolant.Item.TurnToAir();
            }
            if (!overclock.Item.IsAir)
            {
                Main.LocalPlayer.QuickSpawnClonedItem(overclock.Item, overclock.Item.stack);
                overclock.Item.TurnToAir();
            }
            if (!powercell.Item.IsAir)
            {
                Main.LocalPlayer.QuickSpawnClonedItem(powercell.Item, powercell.Item.stack);
                powercell.Item.TurnToAir();
            }
            if (!fragment1.Item.IsAir)
            {
                Main.LocalPlayer.QuickSpawnClonedItem(fragment1.Item, fragment1.Item.stack);
                fragment1.Item.TurnToAir();
            }
            if (!fragment2.Item.IsAir)
            {
                Main.LocalPlayer.QuickSpawnClonedItem(fragment2.Item, fragment2.Item.stack);
                fragment2.Item.TurnToAir();
            }
            if (!fragment3.Item.IsAir)
            {
                Main.LocalPlayer.QuickSpawnClonedItem(fragment3.Item, fragment3.Item.stack);
                fragment3.Item.TurnToAir();
            }
            if (!scrap.Item.IsAir)
            {
                Main.LocalPlayer.QuickSpawnClonedItem(scrap.Item, scrap.Item.stack);
                scrap.Item.TurnToAir();
            }
            if (!shard.Item.IsAir)
            {
                Main.LocalPlayer.QuickSpawnClonedItem(shard.Item, shard.Item.stack);
                shard.Item.TurnToAir();
            }
        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            Main.hidePlayerCraftingMenu = true;
            Texture2D Canvas = ModContent.Request<Texture2D>("AAMod/Assets/Textures/BlueprintCanvas").Value;
            spriteBatch.Draw(Canvas, new Rectangle(382, 262, 796, 306), Color.White);
            Texture2D X = ModContent.Request<Texture2D>("AAMod/Assets/Textures/BlueprintX").Value;
            spriteBatch.Draw(X, new Rectangle(1142, 270, 28, 28), Color.White);
            int XX = 1142;
            int XY = 270;
            bool HoveringX = Main.mouseX > XX && Main.mouseX < XX + 28 && Main.mouseY > XY && Main.mouseY < XY + 28 && !PlayerInput.IgnoreMouseInterface;
            if (HoveringX)
            {
                Main.LocalPlayer.mouseInterface = true;
                if (Main.mouseLeftRelease && Main.mouseLeft)
                {
                    UISystem system = ModContent.GetInstance<UISystem>();
                    system.DeactivateUI<EnergyBlueprint>();
                }
            }
            if (coolant != null && overclock != null && powercell != null && fragment1 != null && fragment2 != null && fragment3 != null && scrap != null && shard != null && output != null)
            {
                if (Created && (output.Item.IsAir || coolant.Item.IsAir || overclock.Item.IsAir || powercell.Item.IsAir || fragment1.Item.IsAir || fragment2.Item.IsAir || fragment3.Item.IsAir || scrap.Item.IsAir))
                {
                    if (output.Item.IsAir || scrap.Item.IsAir)
                    {
                        scrap.Item.TurnToAir();
                    }
                    if (output.Item.IsAir && !coolant.Item.IsAir)
                    {
                        coolant.Item.TurnToAir();
                    }
                    if (output.Item.IsAir && !overclock.Item.IsAir)
                    {
                        overclock.Item.TurnToAir();
                    }
                    if (output.Item.IsAir && !powercell.Item.IsAir)
                    {
                        powercell.Item.TurnToAir();
                    }
                    if (output.Item.IsAir && !fragment1.Item.IsAir)
                    {
                        fragment1.Item.TurnToAir();
                    }
                    if (output.Item.IsAir && !fragment2.Item.IsAir)
                    {
                        fragment2.Item.TurnToAir();
                    }
                    if (output.Item.IsAir && !fragment3.Item.IsAir)
                    {
                        fragment3.Item.TurnToAir();
                    }
                    if (output.Item.IsAir && !shard.Item.IsAir)
                    {
                        shard.Item.TurnToAir();
                    }
                    Created = false;
                    output.Item.TurnToAir();
                }
                if (!coolant.Item.IsAir && !overclock.Item.IsAir && !powercell.Item.IsAir && !fragment1.Item.IsAir && !fragment2.Item.IsAir && !fragment3.Item.IsAir && !scrap.Item.IsAir)
                {
                    #endregion
                    #region BeginHere
                    if (fragment1.Item.type == ModContent.ItemType<RangedFragment>() && fragment2.Item.type == ModContent.ItemType<MagicFragment>() && fragment3.Item.type == ModContent.ItemType<RangedFragment>())
                    {
                        Created = true;
                        output.Item = new Item();
                        output.Item.SetDefaults(ModContent.ItemType<MobianBuster>());
                        if (shard.Item.type == ModContent.ItemType<AcidShard>())
                        {
                            output.Item.GetGlobalItem<AAGlobalItemModular>().OutputShard = 1;
                        }
                        if (shard.Item.type == ModContent.ItemType<DarkShard>())
                        {
                            output.Item.GetGlobalItem<AAGlobalItemModular>().OutputShard = 2;
                        }
                        if (shard.Item.type == ModContent.ItemType<ElectricShard>())
                        {
                            output.Item.GetGlobalItem<AAGlobalItemModular>().OutputShard = 3;
                        }
                        if (shard.Item.type == ModContent.ItemType<FireShard>())
                        {
                            output.Item.GetGlobalItem<AAGlobalItemModular>().OutputShard = 4;
                        }
                        if (shard.Item.type == ModContent.ItemType<IceShard>())
                        {
                            output.Item.GetGlobalItem<AAGlobalItemModular>().OutputShard = 5;
                        }
                        if (shard.Item.type == ModContent.ItemType<LightShard>())
                        {
                            output.Item.GetGlobalItem<AAGlobalItemModular>().OutputShard = 6;
                        }
                        if (shard.Item.type == ModContent.ItemType<PowerShard>())
                        {
                            output.Item.GetGlobalItem<AAGlobalItemModular>().OutputShard = 7;
                        }
                        if (shard.Item.type == ModContent.ItemType<PsychicShard>())
                        {
                            output.Item.GetGlobalItem<AAGlobalItemModular>().OutputShard = 8;
                        }
                        if (shard.Item.type == ItemID.None)
                        {
                            output.Item.GetGlobalItem<AAGlobalItemModular>().OutputShard = 0;
                        }
                        output.Item.GetGlobalItem<AAGlobalItemModular>().OutputFragment[1 - 1] = 3;
                        output.Item.GetGlobalItem<AAGlobalItemModular>().OutputFragment[2 - 1] = 1;
                        output.Item.GetGlobalItem<AAGlobalItemModular>().OutputFragment[3 - 1] = 3;
                        output.Item.GetGlobalItem<AAGlobalItemModular>().OutputOverclock = overclock.Item.GetGlobalItem<AAGlobalItemModular>().OverclockValue;
                        output.Item.GetGlobalItem<AAGlobalItemModular>().OutputCoolant = coolant.Item.type;
                        output.Item.GetGlobalItem<AAGlobalItemModular>().OutputPowercell = powercell.Item.type;
                        output.Item.GetGlobalItem<AAGlobalItemModular>().OutputScrap = scrap.Item.GetGlobalItem<AAGlobalItemModular>().ScrapLevel;
                    }
                    if (fragment1.Item.type == ModContent.ItemType<MeleeFragment>() && fragment2.Item.type == ModContent.ItemType<MeleeFragment>() && fragment3.Item.type == ModContent.ItemType<MeleeFragment>())
                    {
                        Created = true;
                        output.Item = new Item();
                        output.Item.SetDefaults(ModContent.ItemType<LaserBaton>());
                        if (shard.Item.type == ModContent.ItemType<AcidShard>())
                        {
                            output.Item.GetGlobalItem<AAGlobalItemModular>().OutputShard = 1;
                        }
                        if (shard.Item.type == ModContent.ItemType<DarkShard>())
                        {
                            output.Item.GetGlobalItem<AAGlobalItemModular>().OutputShard = 2;
                        }
                        if (shard.Item.type == ModContent.ItemType<ElectricShard>())
                        {
                            output.Item.GetGlobalItem<AAGlobalItemModular>().OutputShard = 3;
                        }
                        if (shard.Item.type == ModContent.ItemType<FireShard>())
                        {
                            output.Item.GetGlobalItem<AAGlobalItemModular>().OutputShard = 4;
                        }
                        if (shard.Item.type == ModContent.ItemType<IceShard>())
                        {
                            output.Item.GetGlobalItem<AAGlobalItemModular>().OutputShard = 5;
                        }
                        if (shard.Item.type == ModContent.ItemType<LightShard>())
                        {
                            output.Item.GetGlobalItem<AAGlobalItemModular>().OutputShard = 6;
                        }
                        if (shard.Item.type == ModContent.ItemType<PowerShard>())
                        {
                            output.Item.GetGlobalItem<AAGlobalItemModular>().OutputShard = 7;
                        }
                        if (shard.Item.type == ModContent.ItemType<PsychicShard>())
                        {
                            output.Item.GetGlobalItem<AAGlobalItemModular>().OutputShard = 8;
                        }
                        if (shard.Item.type == ItemID.None)
                        {
                            output.Item.GetGlobalItem<AAGlobalItemModular>().OutputShard = 0;
                        }
                        output.Item.GetGlobalItem<AAGlobalItemModular>().OutputFragment[1 - 1] = 2;
                        output.Item.GetGlobalItem<AAGlobalItemModular>().OutputFragment[2 - 1] = 2;
                        output.Item.GetGlobalItem<AAGlobalItemModular>().OutputFragment[3 - 1] = 2;
                        output.Item.GetGlobalItem<AAGlobalItemModular>().OutputOverclock = overclock.Item.GetGlobalItem<AAGlobalItemModular>().OverclockValue;
                        output.Item.GetGlobalItem<AAGlobalItemModular>().OutputCoolant = coolant.Item.type;
                        output.Item.GetGlobalItem<AAGlobalItemModular>().OutputPowercell = powercell.Item.type;
                        output.Item.GetGlobalItem<AAGlobalItemModular>().OutputScrap = scrap.Item.GetGlobalItem<AAGlobalItemModular>().ScrapLevel;
                    }
                    #endregion
                    #region EndHere
                }
                if (!Created && !output.Item.IsAir && output.Item.GetGlobalItem<AAGlobalItemModular>().Freedom == -1 && output.Item.type != ModContent.ItemType<FreedomStarBusted>())
                {
                    if (output.Item.GetGlobalItem<AAGlobalItemModular>().OutputCoolant != -1)
                    {
                        coolant.Item = new Item();
                        coolant.Item.SetDefaults(output.Item.GetGlobalItem<AAGlobalItemModular>().OutputCoolant);
                        output.Item.GetGlobalItem<AAGlobalItemModular>().OutputCoolant = -1;
                    }
                    if (output.Item.GetGlobalItem<AAGlobalItemModular>().OutputOverclock != -1)
                    {
                        overclock.Item = new Item();
                        overclock.Item.SetDefaults(ModContent.ItemType<VoidOverclockUnit>());
                        overclock.Item.GetGlobalItem<AAGlobalItemModular>().OverclockValue = output.Item.GetGlobalItem<AAGlobalItemModular>().OutputOverclock;
                        output.Item.GetGlobalItem<AAGlobalItemModular>().OutputOverclock = -1;
                    }
                    if (output.Item.GetGlobalItem<AAGlobalItemModular>().OutputPowercell != -1)
                    {
                        powercell.Item = new Item();
                        powercell.Item.SetDefaults(output.Item.GetGlobalItem<AAGlobalItemModular>().OutputPowercell);
                        output.Item.GetGlobalItem<AAGlobalItemModular>().OutputPowercell = -1;
                    }
                    if (output.Item.GetGlobalItem<AAGlobalItemModular>().OutputScrap != -1)
                    {
                        output.Item.GetGlobalItem<AAGlobalItemModular>().OutputScrap = -1;
                    }
                    if (output.Item.GetGlobalItem<AAGlobalItemModular>().OutputShard != 0)
                    {
                        switch (output.Item.GetGlobalItem<AAGlobalItemModular>().OutputShard)
                        {
                            case 1:
                                {
                                    shard.Item = new Item();
                                    shard.Item.SetDefaults(ModContent.ItemType<AcidShard>());
                                    output.Item.GetGlobalItem<AAGlobalItemModular>().OutputShard = 0;
                                    break;
                                }
                            case 2:
                                {
                                    shard.Item = new Item();
                                    shard.Item.SetDefaults(ModContent.ItemType<DarkShard>());
                                    output.Item.GetGlobalItem<AAGlobalItemModular>().OutputShard = 0;
                                    break;
                                }
                            case 3:
                                {
                                    shard.Item = new Item();
                                    shard.Item.SetDefaults(ModContent.ItemType<ElectricShard>());
                                    output.Item.GetGlobalItem<AAGlobalItemModular>().OutputShard = 0;
                                    break;
                                }
                            case 4:
                                {
                                    shard.Item = new Item();
                                    shard.Item.SetDefaults(ModContent.ItemType<FireShard>());
                                    output.Item.GetGlobalItem<AAGlobalItemModular>().OutputShard = 0;
                                    break;
                                }
                            case 5:
                                {
                                    shard.Item = new Item();
                                    shard.Item.SetDefaults(ModContent.ItemType<IceShard>());
                                    output.Item.GetGlobalItem<AAGlobalItemModular>().OutputShard = 0;
                                    break;
                                }
                            case 6:
                                {
                                    shard.Item = new Item();
                                    shard.Item.SetDefaults(ModContent.ItemType<LightShard>());
                                    output.Item.GetGlobalItem<AAGlobalItemModular>().OutputShard = 0;
                                    break;
                                }
                            case 7:
                                {
                                    shard.Item = new Item();
                                    shard.Item.SetDefaults(ModContent.ItemType<PowerShard>());
                                    output.Item.GetGlobalItem<AAGlobalItemModular>().OutputShard = 0;
                                    break;
                                }
                            case 8:
                                {
                                    shard.Item = new Item();
                                    shard.Item.SetDefaults(ModContent.ItemType<PsychicShard>());
                                    output.Item.GetGlobalItem<AAGlobalItemModular>().OutputShard = 0;
                                    break;
                                }
                        }
                    }
                    switch (output.Item.GetGlobalItem<AAGlobalItemModular>().OutputFragment[1 - 1])
                    {
                        case 1:
                            {
                                fragment1.Item = new Item();
                                fragment1.Item.SetDefaults(ModContent.ItemType<MagicFragment>());
                                output.Item.GetGlobalItem<AAGlobalItemModular>().OutputFragment[1 - 1] = 0;
                                break;
                            }
                        case 2:
                            {
                                fragment1.Item = new Item();
                                fragment1.Item.SetDefaults(ModContent.ItemType<MeleeFragment>());
                                output.Item.GetGlobalItem<AAGlobalItemModular>().OutputFragment[1 - 1] = 0;
                                break;
                            }
                        case 3:
                            {
                                fragment1.Item = new Item();
                                fragment1.Item.SetDefaults(ModContent.ItemType<RangedFragment>());
                                output.Item.GetGlobalItem<AAGlobalItemModular>().OutputFragment[1 - 1] = 0;
                                break;
                            }
                        case 4:
                            {
                                fragment1.Item = new Item();
                                fragment1.Item.SetDefaults(ModContent.ItemType<SummonFragment>());
                                output.Item.GetGlobalItem<AAGlobalItemModular>().OutputFragment[1 - 1] = 0;
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                    switch (output.Item.GetGlobalItem<AAGlobalItemModular>().OutputFragment[2 - 1])
                    {
                        case 1:
                            {
                                fragment2.Item = new Item();
                                fragment2.Item.SetDefaults(ModContent.ItemType<MagicFragment>());
                                output.Item.GetGlobalItem<AAGlobalItemModular>().OutputFragment[2 - 1] = 0;
                                break;
                            }
                        case 2:
                            {
                                fragment2.Item = new Item();
                                fragment2.Item.SetDefaults(ModContent.ItemType<MeleeFragment>());
                                output.Item.GetGlobalItem<AAGlobalItemModular>().OutputFragment[2 - 1] = 0;
                                break;
                            }
                        case 3:
                            {
                                fragment2.Item = new Item();
                                fragment2.Item.SetDefaults(ModContent.ItemType<RangedFragment>());
                                output.Item.GetGlobalItem<AAGlobalItemModular>().OutputFragment[2 - 1] = 0;
                                break;
                            }
                        case 4:
                            {
                                fragment2.Item = new Item();
                                fragment2.Item.SetDefaults(ModContent.ItemType<SummonFragment>());
                                output.Item.GetGlobalItem<AAGlobalItemModular>().OutputFragment[2 - 1] = 0;
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                    switch (output.Item.GetGlobalItem<AAGlobalItemModular>().OutputFragment[3 - 1])
                    {
                        case 1:
                            {
                                fragment3.Item = new Item();
                                fragment3.Item.SetDefaults(ModContent.ItemType<MagicFragment>());
                                output.Item.GetGlobalItem<AAGlobalItemModular>().OutputFragment[3 - 1] = 0;
                                break;
                            }
                        case 2:
                            {
                                fragment3.Item = new Item();
                                fragment3.Item.SetDefaults(ModContent.ItemType<MeleeFragment>());
                                output.Item.GetGlobalItem<AAGlobalItemModular>().OutputFragment[3 - 1] = 0;
                                break;
                            }
                        case 3:
                            {
                                fragment3.Item = new Item();
                                fragment3.Item.SetDefaults(ModContent.ItemType<RangedFragment>());
                                output.Item.GetGlobalItem<AAGlobalItemModular>().OutputFragment[3 - 1] = 0;
                                break;
                            }
                        case 4:
                            {
                                fragment3.Item = new Item();
                                fragment3.Item.SetDefaults(ModContent.ItemType<SummonFragment>());
                                output.Item.GetGlobalItem<AAGlobalItemModular>().OutputFragment[3 - 1] = 0;
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }
                if ((!output.Item.IsAir && output.Item.GetGlobalItem<AAGlobalItemModular>().Freedom == -1 && output.Item.type != ModContent.ItemType<FreedomStarBusted>()) && (coolant.Item.IsAir || overclock.Item.IsAir || powercell.Item.IsAir || fragment1.Item.IsAir || fragment2.Item.IsAir || fragment3.Item.IsAir || scrap.Item.IsAir))
                {
                    output.Item.TurnToAir();
                }
                if (output.Item.TryGetGlobalItem(out AAGlobalItemModular _) && (output.Item.GetGlobalItem<AAGlobalItemModular>().Freedom != -1 || output.Item.type == ModContent.ItemType<FreedomStarBusted>()) && (coolant.Item.IsAir || overclock.Item.IsAir || powercell.Item.IsAir))
                {
                    if (!Modifying && coolant.Item.IsAir && output.Item.GetGlobalItem<AAGlobalItemModular>().OutputCoolant != -1)
                    {
                        coolant.Item = new Item();
                        coolant.Item.SetDefaults(output.Item.GetGlobalItem<AAGlobalItemModular>().OutputCoolant);
                        output.Item.GetGlobalItem<AAGlobalItemModular>().OutputCoolant = -1;
                    }
                    if (!Modifying && overclock.Item.IsAir && output.Item.GetGlobalItem<AAGlobalItemModular>().OutputOverclock != -1)
                    {
                        overclock.Item = new Item();
                        overclock.Item.SetDefaults(ModContent.ItemType<VoidOverclockUnit>());
                        overclock.Item.GetGlobalItem<AAGlobalItemModular>().OverclockValue = output.Item.GetGlobalItem<AAGlobalItemModular>().OutputOverclock;
                        output.Item.GetGlobalItem<AAGlobalItemModular>().OutputOverclock = -1;
                    }
                    if (!Modifying && powercell.Item.IsAir && output.Item.GetGlobalItem<AAGlobalItemModular>().OutputPowercell != -1)
                    {
                        powercell.Item = new Item();
                        powercell.Item.SetDefaults(output.Item.GetGlobalItem<AAGlobalItemModular>().OutputPowercell);
                        output.Item.GetGlobalItem<AAGlobalItemModular>().OutputPowercell = -1;
                    }
                    if (output.Item.GetGlobalItem<AAGlobalItemModular>().Freedom != -1)
                    {
                        output.Item.TurnToAir();
                        output.Item = new Item();
                        output.Item.SetDefaults(ModContent.ItemType<FreedomStarBusted>());
                    }
                }
                if (!output.Item.IsAir && (output.Item.GetGlobalItem<AAGlobalItemModular>().Freedom != -1 || output.Item.type == ModContent.ItemType<FreedomStarBusted>()))
                {
                    Modifying = true;
                    if (!coolant.Item.IsAir)
                    {
                        output.Item.GetGlobalItem<AAGlobalItemModular>().OutputCoolant = coolant.Item.type;
                    }
                    if (!overclock.Item.IsAir)
                    {
                        output.Item.GetGlobalItem<AAGlobalItemModular>().OutputOverclock = overclock.Item.GetGlobalItem<AAGlobalItemModular>().OverclockValue;
                    }
                    if (!powercell.Item.IsAir)
                    {
                        output.Item.GetGlobalItem<AAGlobalItemModular>().OutputPowercell = powercell.Item.type;
                    }
                    if (!scrap.Item.IsAir)
                    {
                        Main.LocalPlayer.QuickSpawnClonedItem(scrap.Item, scrap.Item.stack);
                        scrap.Item.TurnToAir();
                    }
                    if (!shard.Item.IsAir)
                    {
                        Main.LocalPlayer.QuickSpawnClonedItem(shard.Item, shard.Item.stack);
                        shard.Item.TurnToAir();
                    }
                    if (!fragment1.Item.IsAir)
                    {
                        Main.LocalPlayer.QuickSpawnClonedItem(fragment1.Item, fragment1.Item.stack);
                        fragment1.Item.TurnToAir();
                    }
                    if (!fragment2.Item.IsAir)
                    {
                        Main.LocalPlayer.QuickSpawnClonedItem(fragment2.Item, fragment2.Item.stack);
                        fragment2.Item.TurnToAir();
                    }
                    if (!fragment3.Item.IsAir)
                    {
                        Main.LocalPlayer.QuickSpawnClonedItem(fragment3.Item, fragment3.Item.stack);
                        fragment3.Item.TurnToAir();
                    }
                }
                if (Modifying && output.Item.IsAir)
                {
                    if (!coolant.Item.IsAir)
                    {
                        coolant.Item.TurnToAir();
                    }
                    if (!overclock.Item.IsAir)
                    {
                        overclock.Item.TurnToAir();
                    }
                    if (!powercell.Item.IsAir)
                    {
                        powercell.Item.TurnToAir();
                    }
                    Modifying = false;
                }
                if (output.Item.type == ModContent.ItemType<FreedomStarBusted>() && !coolant.Item.IsAir && !overclock.Item.IsAir && !powercell.Item.IsAir)
                {
                    output.Item.TurnToAir();
                    output.Item = new Item();
                    output.Item.SetDefaults(ModContent.ItemType<FreedomStar>());
                    output.Item.GetGlobalItem<AAGlobalItemModular>().OutputCoolant = coolant.Item.type;
                    output.Item.GetGlobalItem<AAGlobalItemModular>().OutputOverclock = overclock.Item.GetGlobalItem<AAGlobalItemModular>().OverclockValue;
                    output.Item.GetGlobalItem<AAGlobalItemModular>().OutputPowercell = powercell.Item.type;
                }
            }
        }
    }
    #endregion
    #region DoNotTouch
    class CoolantUI : UIElement
    {
        internal Item Item;
        private readonly int _context;
        public Asset<Texture2D> SlotTexture;
        public Asset<Texture2D> EmptyTexture;
        private readonly float _scale;
        internal Func<Item, bool> CoolantFunc;
        public CoolantUI(int context = ItemSlot.Context.BankItem, float scale = 1f)
        {
            _context = context;
            _scale = scale;
            SlotTexture = ModContent.Request<Texture2D>("AAMod/Assets/Textures/BlueprintItemSlot");
            EmptyTexture = ModContent.Request<Texture2D>("AAMod/Assets/Textures/BlueprintCoolant");
            Item = new Item();
            Item.SetDefaults(0);
            Width.Set(TextureAssets.InventoryBack9.Value.Width * scale, 0f);
            Height.Set(TextureAssets.InventoryBack9.Value.Height * scale, 0f);
        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            float oldScale = Main.inventoryScale;
            Main.inventoryScale = _scale;
            Rectangle rectangle = GetDimensions().ToRectangle();
            if (ContainsPoint(Main.MouseScreen) && !PlayerInput.IgnoreMouseInterface)
            {
                Main.LocalPlayer.mouseInterface = true;
                if (CoolantFunc == null || CoolantFunc(Main.mouseItem))
                {
                    ItemSlot.Handle(ref Item, _context);
                }
            }
            if (Item.type != ItemID.None)
            {
                Asset<Texture2D> itemTexture = TextureAssets.Item[Item.type];
                if (ModContent.HasAsset($"AAMod/Content/Items/Materials/Energy/Coolant/{Item.Name.Replace(" ", "")}_NoAnim"))
                {
                    itemTexture = ModContent.Request<Texture2D>($"AAMod/Content/Items/Materials/Energy/Coolant/{Item.Name.Replace(" ", "")}_NoAnim");
                }
                Vector2 itemPosition = rectangle.Center() - new Vector2(itemTexture.Width(), itemTexture.Height()) / 2f;
                Rectangle itemRectangle = new((int)itemPosition.X, (int)itemPosition.Y, itemTexture.Width(), itemTexture.Height());
                int offset = (int)(16f * _scale);
                int maxWidth = (int)((SlotTexture.Width() - offset) * _scale);
                int maxHeight = (int)((SlotTexture.Height() - offset) * _scale);
                Rectangle clippedRectangle = new(rectangle.X + offset / 2, rectangle.Y + offset / 2, maxWidth, maxHeight);
                if (itemRectangle.Width > maxWidth)
                {
                    itemRectangle = clippedRectangle;
                }
                if (itemRectangle.Height > maxHeight)
                {
                    itemRectangle = clippedRectangle;
                }
                spriteBatch.Draw(SlotTexture.Value, rectangle.TopLeft(), Color.White);
                spriteBatch.Draw(itemTexture.Value, itemRectangle, Color.White);
                if (ModContent.HasAsset($"AAMod/Content/Items/Materials/Energy/Coolant/{Item.Name.Replace(" ", "")}_GlowNoAnim"))
                {
                    Asset<Texture2D> glowMask = ModContent.Request<Texture2D>($"AAMod/Content/Items/Materials/Energy/Coolant/{Item.Name.Replace(" ", "")}_GlowNoAnim");
                    spriteBatch.Draw(glowMask.Value, itemRectangle, Color.White);
                }
            }
            else
            {
                spriteBatch.Draw(EmptyTexture.Value, rectangle.TopLeft(), Color.White);
            }
            Main.inventoryScale = oldScale;
        }
    }
    class OverclockUI : UIElement
    {
        internal Item Item;
        private readonly int _context;
        public Asset<Texture2D> SlotTexture;
        public Asset<Texture2D> EmptyTexture;
        private readonly float _scale;
        internal Func<Item, bool> OverclockFunc;
        public OverclockUI(int context = ItemSlot.Context.BankItem, float scale = 1f)
        {
            _context = context;
            _scale = scale;
            SlotTexture = ModContent.Request<Texture2D>("AAMod/Assets/Textures/BlueprintItemSlot");
            EmptyTexture = ModContent.Request<Texture2D>("AAMod/Assets/Textures/BlueprintOverclock");
            Item = new Item();
            Item.SetDefaults(0);
            Width.Set(TextureAssets.InventoryBack9.Value.Width * scale, 0f);
            Height.Set(TextureAssets.InventoryBack9.Value.Height * scale, 0f);
        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            float oldScale = Main.inventoryScale;
            Main.inventoryScale = _scale;
            Rectangle rectangle = GetDimensions().ToRectangle();
            if (ContainsPoint(Main.MouseScreen) && !PlayerInput.IgnoreMouseInterface)
            {
                Main.LocalPlayer.mouseInterface = true;
                if (OverclockFunc == null || OverclockFunc(Main.mouseItem))
                {
                    ItemSlot.Handle(ref Item, _context);
                }
            }
            if (Item.type != ItemID.None)
            {
                Asset<Texture2D> itemTexture = TextureAssets.Item[Item.type];
                if (ModContent.HasAsset($"AAMod/Content/Items/Materials/Energy/VoidOverclockUnit_NoAnim"))
                {
                    itemTexture = ModContent.Request<Texture2D>("AAMod/Content/Items/Materials/Energy/VoidOverclockUnit_NoAnim");
                }
                Vector2 itemPosition = rectangle.Center() - new Vector2(itemTexture.Width(), itemTexture.Height()) / 2f;
                Rectangle itemRectangle = new((int)itemPosition.X, (int)itemPosition.Y, itemTexture.Width(), itemTexture.Height());
                int offset = (int)(16f * _scale);
                int maxWidth = (int)((SlotTexture.Width() - offset) * _scale);
                int maxHeight = (int)((SlotTexture.Height() - offset) * _scale);
                Rectangle clippedRectangle = new(rectangle.X + offset / 2, rectangle.Y + offset / 2, maxWidth, maxHeight);
                if (itemRectangle.Width > maxWidth)
                {
                    itemRectangle = clippedRectangle;
                }
                if (itemRectangle.Height > maxHeight)
                {
                    itemRectangle = clippedRectangle;
                }
                spriteBatch.Draw(SlotTexture.Value, rectangle.TopLeft(), Color.White);
                spriteBatch.Draw(itemTexture.Value, itemRectangle, Color.White);
                if (ModContent.HasAsset("AAMod/Content/Items/Materials/Energy/VoidOverclockUnit_GlowNoAnim"))
                {
                    Asset<Texture2D> glowMask = ModContent.Request<Texture2D>("AAMod/Content/Items/Materials/Energy/VoidOverclockUnit_GlowNoAnim");
                    spriteBatch.Draw(glowMask.Value, itemRectangle, Color.White);
                }
            }
            else
            {
                spriteBatch.Draw(EmptyTexture.Value, rectangle.TopLeft(), Color.White);
            }
            Main.inventoryScale = oldScale;
        }
    }
    class PowercellUI : UIElement
    {
        internal Item Item;
        private readonly int _context;
        public Asset<Texture2D> SlotTexture;
        public Asset<Texture2D> EmptyTexture;
        private readonly float _scale;
        internal Func<Item, bool> PowercellFunc;
        public PowercellUI(int context = ItemSlot.Context.BankItem, float scale = 1f)
        {
            _context = context;
            _scale = scale;
            SlotTexture = ModContent.Request<Texture2D>("AAMod/Assets/Textures/BlueprintItemSlot");
            EmptyTexture = ModContent.Request<Texture2D>("AAMod/Assets/Textures/BlueprintPowercell");
            Item = new Item();
            Item.SetDefaults(0);
            Width.Set(TextureAssets.InventoryBack9.Value.Width * scale, 0f);
            Height.Set(TextureAssets.InventoryBack9.Value.Height * scale, 0f);
        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            float oldScale = Main.inventoryScale;
            Main.inventoryScale = _scale;
            Rectangle rectangle = GetDimensions().ToRectangle();
            if (ContainsPoint(Main.MouseScreen) && !PlayerInput.IgnoreMouseInterface)
            {
                Main.LocalPlayer.mouseInterface = true;
                if (PowercellFunc == null || PowercellFunc(Main.mouseItem))
                {
                    ItemSlot.Handle(ref Item, _context);
                }
            }
            if (Item.type != ItemID.None)
            {
                Asset<Texture2D> itemTexture = TextureAssets.Item[Item.type];
                if (ModContent.HasAsset($"AAMod/Content/Items/Materials/Energy/Powercell/{Item.Name.Replace(" ", "")}_NoAnim"))
                {
                    itemTexture = ModContent.Request<Texture2D>($"AAMod/Content/Items/Materials/Energy/Powercell/{Item.Name.Replace(" ", "")}_NoAnim");
                }
                Vector2 itemPosition = rectangle.Center() - new Vector2(itemTexture.Width(), itemTexture.Height()) / 2f;
                Rectangle itemRectangle = new((int)itemPosition.X, (int)itemPosition.Y, itemTexture.Width(), itemTexture.Height());
                int offset = (int)(16f * _scale);
                int maxWidth = (int)((SlotTexture.Width() - offset) * _scale);
                int maxHeight = (int)((SlotTexture.Height() - offset) * _scale);
                Rectangle clippedRectangle = new(rectangle.X + offset / 2, rectangle.Y + offset / 2, maxWidth, maxHeight);
                if (itemRectangle.Width > maxWidth)
                {
                    itemRectangle = clippedRectangle;
                }
                if (itemRectangle.Height > maxHeight)
                {
                    itemRectangle = clippedRectangle;
                }
                spriteBatch.Draw(SlotTexture.Value, rectangle.TopLeft(), Color.White);
                spriteBatch.Draw(itemTexture.Value, itemRectangle, Color.White);
                if (ModContent.HasAsset($"AAMod/Content/Items/Materials/Energy/Powercell/{Item.Name.Replace(" ", "")}_GlowNoAnim"))
                {
                    Asset<Texture2D> glowMask = ModContent.Request<Texture2D>($"AAMod/Content/Items/Materials/Energy/Powercell/{Item.Name.Replace(" ", "")}_GlowNoAnim");
                    spriteBatch.Draw(glowMask.Value, itemRectangle, Color.White);
                }
            }
            else
            {
                spriteBatch.Draw(EmptyTexture.Value, rectangle.TopLeft(), Color.White);
            }
            Main.inventoryScale = oldScale;
        }
    }
    class ScrapUI : UIElement
    {
        internal Item Item;
        private readonly int _context;
        public Asset<Texture2D> SlotTexture;
        public Asset<Texture2D> EmptyTexture;
        private EnergyBlueprint output;
        private readonly float _scale;
        internal Func<Item, bool> ScrapFunc;
        public ScrapUI(EnergyBlueprint outty, int context = ItemSlot.Context.BankItem, float scale = 1f)
        {
            output = outty;
            _context = context;
            _scale = scale;
            SlotTexture = ModContent.Request<Texture2D>("AAMod/Assets/Textures/BlueprintItemSlot");
            EmptyTexture = ModContent.Request<Texture2D>("AAMod/Assets/Textures/BlueprintScrap");
            Item = new Item();
            Item.SetDefaults(0);
            Width.Set(TextureAssets.InventoryBack9.Value.Width * scale, 0f);
            Height.Set(TextureAssets.InventoryBack9.Value.Height * scale, 0f);
        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            float oldScale = Main.inventoryScale;
            Main.inventoryScale = _scale;
            Rectangle rectangle = GetDimensions().ToRectangle();
            if (ContainsPoint(Main.MouseScreen) && !PlayerInput.IgnoreMouseInterface)
            {
                Main.LocalPlayer.mouseInterface = true;
                if (ScrapFunc == null || ScrapFunc(Main.mouseItem))
                {
                    ItemSlot.Handle(ref Item, _context);
                }
            }
            if (Item.type != ItemID.None && (!output.output.Item.TryGetGlobalItem(out AAGlobalItemModular _) || (output.output.Item.TryGetGlobalItem(out AAGlobalItemModular _) && output.output.Item.GetGlobalItem<AAGlobalItemModular>().Freedom == -1 && output.output.Item.type != ModContent.ItemType<FreedomStarBusted>())))
            {
                Asset<Texture2D> itemTexture = TextureAssets.Item[Item.type];
                if (Item.TryGetGlobalItem(out AAGlobalItemModular _) && ModContent.HasAsset($"AAMod/Content/Items/Materials/Energy/Scraps_NoAnim"))
                {
                    itemTexture = ModContent.Request<Texture2D>($"AAMod/Content/Items/Materials/Energy/Scraps_NoAnim");
                }
                Vector2 itemPosition = rectangle.Center() - new Vector2(itemTexture.Width(), itemTexture.Height()) / 2f;
                Rectangle itemRectangle = new((int)itemPosition.X, (int)itemPosition.Y, itemTexture.Width(), itemTexture.Height());
                int offset = (int)(16f * _scale);
                int maxWidth = (int)((SlotTexture.Width() - offset) * _scale);
                int maxHeight = (int)((SlotTexture.Height() - offset) * _scale);
                Rectangle clippedRectangle = new(rectangle.X + offset / 2, rectangle.Y + offset / 2, maxWidth, maxHeight);
                if (itemRectangle.Width > maxWidth)
                {
                    itemRectangle = clippedRectangle;
                }
                if (itemRectangle.Height > maxHeight)
                {
                    itemRectangle = clippedRectangle;
                }
                spriteBatch.Draw(SlotTexture.Value, rectangle.TopLeft(), Color.White);
                spriteBatch.Draw(itemTexture.Value, itemRectangle, Color.White);
                if (Item.TryGetGlobalItem(out AAGlobalItemModular _) && ModContent.HasAsset($"AAMod/Content/Items/Materials/Energy/Scraps_GlowNoAnim"))
                {
                    Asset<Texture2D> glowMask = ModContent.Request<Texture2D>($"AAMod/Content/Items/Materials/Energy/Scraps_GlowNoAnim");
                    spriteBatch.Draw(glowMask.Value, itemRectangle, Color.White);
                }
            }
            else if (output.output.Item.TryGetGlobalItem(out AAGlobalItemModular _) && (output.output.Item.GetGlobalItem<AAGlobalItemModular>().Freedom != -1 || output.output.Item.type == ModContent.ItemType<FreedomStarBusted>()))
            {
                spriteBatch.Draw(SlotTexture.Value, rectangle.TopLeft(), Color.Gray);
            }
            else
            {
                spriteBatch.Draw(EmptyTexture.Value, rectangle.TopLeft(), Color.White);
            }
            Main.inventoryScale = oldScale;
        }
    }
    class ShardUI : UIElement
    {
        internal Item Item;
        private readonly int _context;
        public Asset<Texture2D> SlotTexture;
        public Asset<Texture2D> EmptyTexture;
        private EnergyBlueprint output;
        private readonly float _scale;
        internal Func<Item, bool> ShardFunc;
        public ShardUI(EnergyBlueprint outty, int context = ItemSlot.Context.BankItem, float scale = 1f)
        {
            output = outty;
            _context = context;
            _scale = scale;
            SlotTexture = ModContent.Request<Texture2D>("AAMod/Assets/Textures/BlueprintItemSlot");
            EmptyTexture = ModContent.Request<Texture2D>("AAMod/Assets/Textures/BlueprintShard");
            Item = new Item();
            Item.SetDefaults(0);
            Width.Set(TextureAssets.InventoryBack9.Value.Width * scale, 0f);
            Height.Set(TextureAssets.InventoryBack9.Value.Height * scale, 0f);
        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            float oldScale = Main.inventoryScale;
            Main.inventoryScale = _scale;
            Rectangle rectangle = GetDimensions().ToRectangle();
            if (ContainsPoint(Main.MouseScreen) && !PlayerInput.IgnoreMouseInterface)
            {
                Main.LocalPlayer.mouseInterface = true;
                if (ShardFunc == null || ShardFunc(Main.mouseItem))
                {
                    ItemSlot.Handle(ref Item, _context);
                }
            }
            if (Item.type != ItemID.None && (!output.output.Item.TryGetGlobalItem(out AAGlobalItemModular _) || (output.output.Item.TryGetGlobalItem(out AAGlobalItemModular _) && output.output.Item.GetGlobalItem<AAGlobalItemModular>().Freedom == -1 && output.output.Item.type != ModContent.ItemType<FreedomStarBusted>())))
            {
                Asset<Texture2D> itemTexture = TextureAssets.Item[Item.type];
                if (ModContent.HasAsset($"AAMod/Content/Items/Materials/Energy/{Item.Name.Replace(" ", "")}_NoAnim"))
                {
                    itemTexture = ModContent.Request<Texture2D>($"AAMod/Content/Items/Materials/Energy/{Item.Name.Replace(" ", "")}_NoAnim");
                }
                Vector2 itemPosition = rectangle.Center() - new Vector2(itemTexture.Width(), itemTexture.Height()) / 2f;
                Rectangle itemRectangle = new((int)itemPosition.X, (int)itemPosition.Y, itemTexture.Width(), itemTexture.Height());
                int offset = (int)(16f * _scale);
                int maxWidth = (int)((SlotTexture.Width() - offset) * _scale);
                int maxHeight = (int)((SlotTexture.Height() - offset) * _scale);
                Rectangle clippedRectangle = new(rectangle.X + offset / 2, rectangle.Y + offset / 2, maxWidth, maxHeight);
                if (itemRectangle.Width > maxWidth)
                {
                    itemRectangle = clippedRectangle;
                }
                if (itemRectangle.Height > maxHeight)
                {
                    itemRectangle = clippedRectangle;
                }
                spriteBatch.Draw(SlotTexture.Value, rectangle.TopLeft(), Color.White);
                spriteBatch.Draw(itemTexture.Value, itemRectangle, Color.White);
                if (ModContent.HasAsset($"AAMod/Content/Items/Materials/Energy/{Item.Name.Replace(" ", "")}_GlowNoAnim"))
                {
                    Asset<Texture2D> glowMask = ModContent.Request<Texture2D>($"AAMod/Content/Items/Materials/Energy/{Item.Name.Replace(" ", "")}_GlowNoAnim");
                    spriteBatch.Draw(glowMask.Value, itemRectangle, Color.White);
                }
            }
            else if (output.output.Item.TryGetGlobalItem(out AAGlobalItemModular _) && (output.output.Item.GetGlobalItem<AAGlobalItemModular>().Freedom != -1 || output.output.Item.type == ModContent.ItemType<FreedomStarBusted>()))
            {
                spriteBatch.Draw(SlotTexture.Value, rectangle.TopLeft(), Color.Gray);
            }
            else
            {
                spriteBatch.Draw(EmptyTexture.Value, rectangle.TopLeft(), Color.White);
            }
            Main.inventoryScale = oldScale;
        }
    }
    class Fragment1UI : UIElement
    {
        internal Item Item;
        private readonly int _context;
        public Asset<Texture2D> SlotTexture;
        public Asset<Texture2D> EmptyTexture;
        private EnergyBlueprint output;
        private readonly float _scale;
        internal Func<Item, bool> FragmentFunc;
        public Fragment1UI(EnergyBlueprint outty, int context = ItemSlot.Context.BankItem, float scale = 1f)
        {
            output = outty;
            _context = context;
            _scale = scale;
            SlotTexture = ModContent.Request<Texture2D>("AAMod/Assets/Textures/BlueprintItemSlot");
            EmptyTexture = ModContent.Request<Texture2D>("AAMod/Assets/Textures/BlueprintFragment");
            Item = new Item();
            Item.SetDefaults(0);
            Width.Set(TextureAssets.InventoryBack9.Value.Width * scale, 0f);
            Height.Set(TextureAssets.InventoryBack9.Value.Height * scale, 0f);
        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            float oldScale = Main.inventoryScale;
            Main.inventoryScale = _scale;
            Rectangle rectangle = GetDimensions().ToRectangle();
            if (ContainsPoint(Main.MouseScreen) && !PlayerInput.IgnoreMouseInterface)
            {
                Main.LocalPlayer.mouseInterface = true;
                if (FragmentFunc == null || FragmentFunc(Main.mouseItem))
                {
                    ItemSlot.Handle(ref Item, _context);
                }
            }
            if (Item.type != ItemID.None && (!output.output.Item.TryGetGlobalItem(out AAGlobalItemModular _) || (output.output.Item.TryGetGlobalItem(out AAGlobalItemModular _) && output.output.Item.GetGlobalItem<AAGlobalItemModular>().Freedom == -1 && output.output.Item.type != ModContent.ItemType<FreedomStarBusted>())))
            {
                Asset<Texture2D> itemTexture = TextureAssets.Item[Item.type];
                if (ModContent.HasAsset($"AAMod/Content/Items/Materials/Energy/{Item.Name.Replace(" ", "")}_NoAnim"))
                {
                    itemTexture = ModContent.Request<Texture2D>($"AAMod/Content/Items/Materials/Energy/{Item.Name.Replace(" ", "")}_NoAnim");
                }
                Vector2 itemPosition = rectangle.Center() - new Vector2(itemTexture.Width(), itemTexture.Height()) / 2f;
                Rectangle itemRectangle = new((int)itemPosition.X, (int)itemPosition.Y, itemTexture.Width(), itemTexture.Height());
                int offset = (int)(16f * _scale);
                int maxWidth = (int)((SlotTexture.Width() - offset) * _scale);
                int maxHeight = (int)((SlotTexture.Height() - offset) * _scale);
                Rectangle clippedRectangle = new(rectangle.X + offset / 2, rectangle.Y + offset / 2, maxWidth, maxHeight);
                if (itemRectangle.Width > maxWidth)
                {
                    itemRectangle = clippedRectangle;
                }
                if (itemRectangle.Height > maxHeight)
                {
                    itemRectangle = clippedRectangle;
                }
                spriteBatch.Draw(SlotTexture.Value, rectangle.TopLeft(), Color.White);
                spriteBatch.Draw(itemTexture.Value, itemRectangle, Color.White);
                if (ModContent.HasAsset($"AAMod/Content/Items/Materials/Energy/{Item.Name.Replace(" ", "")}_GlowNoAnim"))
                {
                    Asset<Texture2D> glowMask = ModContent.Request<Texture2D>($"AAMod/Content/Items/Materials/Energy/{Item.Name.Replace(" ", "")}_GlowNoAnim");
                    spriteBatch.Draw(glowMask.Value, itemRectangle, Color.White);
                }
            }
            else if (output.output.Item.TryGetGlobalItem(out AAGlobalItemModular _) && (output.output.Item.GetGlobalItem<AAGlobalItemModular>().Freedom != -1 || output.output.Item.type == ModContent.ItemType<FreedomStarBusted>()))
            {
                spriteBatch.Draw(SlotTexture.Value, rectangle.TopLeft(), Color.Gray);
            }
            else
            {
                spriteBatch.Draw(EmptyTexture.Value, rectangle.TopLeft(), Color.White);
            }
            Main.inventoryScale = oldScale;
        }
    }
    class Fragment2UI : UIElement
    {
        internal Item Item;
        private readonly int _context;
        public Asset<Texture2D> SlotTexture;
        public Asset<Texture2D> EmptyTexture;
        private EnergyBlueprint output;
        private readonly float _scale;
        internal Func<Item, bool> FragmentFunc;
        public Fragment2UI(EnergyBlueprint outty, int context = ItemSlot.Context.BankItem, float scale = 1f)
        {
            output = outty;
            _context = context;
            _scale = scale;
            SlotTexture = ModContent.Request<Texture2D>("AAMod/Assets/Textures/BlueprintItemSlot");
            EmptyTexture = ModContent.Request<Texture2D>("AAMod/Assets/Textures/BlueprintFragment");
            Item = new Item();
            Item.SetDefaults(0);
            Width.Set(TextureAssets.InventoryBack9.Value.Width * scale, 0f);
            Height.Set(TextureAssets.InventoryBack9.Value.Height * scale, 0f);
        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            float oldScale = Main.inventoryScale;
            Main.inventoryScale = _scale;
            Rectangle rectangle = GetDimensions().ToRectangle();
            if (ContainsPoint(Main.MouseScreen) && !PlayerInput.IgnoreMouseInterface)
            {
                Main.LocalPlayer.mouseInterface = true;
                if (FragmentFunc == null || FragmentFunc(Main.mouseItem))
                {
                    ItemSlot.Handle(ref Item, _context);
                }
            }
            if (Item.type != ItemID.None && (!output.output.Item.TryGetGlobalItem(out AAGlobalItemModular _) || (output.output.Item.TryGetGlobalItem(out AAGlobalItemModular _) && output.output.Item.GetGlobalItem<AAGlobalItemModular>().Freedom == -1 && output.output.Item.type != ModContent.ItemType<FreedomStarBusted>())))
            {
                Asset<Texture2D> itemTexture = TextureAssets.Item[Item.type];
                if (ModContent.HasAsset($"AAMod/Content/Items/Materials/Energy/{Item.Name.Replace(" ", "")}_NoAnim"))
                {
                    itemTexture = ModContent.Request<Texture2D>($"AAMod/Content/Items/Materials/Energy/{Item.Name.Replace(" ", "")}_NoAnim");
                }
                Vector2 itemPosition = rectangle.Center() - new Vector2(itemTexture.Width(), itemTexture.Height()) / 2f;
                Rectangle itemRectangle = new((int)itemPosition.X, (int)itemPosition.Y, itemTexture.Width(), itemTexture.Height());
                int offset = (int)(16f * _scale);
                int maxWidth = (int)((SlotTexture.Width() - offset) * _scale);
                int maxHeight = (int)((SlotTexture.Height() - offset) * _scale);
                Rectangle clippedRectangle = new(rectangle.X + offset / 2, rectangle.Y + offset / 2, maxWidth, maxHeight);
                if (itemRectangle.Width > maxWidth)
                {
                    itemRectangle = clippedRectangle;
                }
                if (itemRectangle.Height > maxHeight)
                {
                    itemRectangle = clippedRectangle;
                }
                spriteBatch.Draw(SlotTexture.Value, rectangle.TopLeft(), Color.White);
                spriteBatch.Draw(itemTexture.Value, itemRectangle, Color.White);
                if (ModContent.HasAsset($"AAMod/Content/Items/Materials/Energy/{Item.Name.Replace(" ", "")}_GlowNoAnim"))
                {
                    Asset<Texture2D> glowMask = ModContent.Request<Texture2D>($"AAMod/Content/Items/Materials/Energy/{Item.Name.Replace(" ", "")}_GlowNoAnim");
                    spriteBatch.Draw(glowMask.Value, itemRectangle, Color.White);
                }
            }
            else if (output.output.Item.TryGetGlobalItem(out AAGlobalItemModular _) && (output.output.Item.GetGlobalItem<AAGlobalItemModular>().Freedom != -1 || output.output.Item.type == ModContent.ItemType<FreedomStarBusted>()))
            {
                spriteBatch.Draw(SlotTexture.Value, rectangle.TopLeft(), Color.Gray);
            }
            else
            {
                spriteBatch.Draw(EmptyTexture.Value, rectangle.TopLeft(), Color.White);
            }
            Main.inventoryScale = oldScale;
        }
    }
    class Fragment3UI : UIElement
    {
        internal Item Item;
        private readonly int _context;
        public Asset<Texture2D> SlotTexture;
        public Asset<Texture2D> EmptyTexture;
        private EnergyBlueprint output;
        private readonly float _scale;
        internal Func<Item, bool> FragmentFunc;
        public Fragment3UI(EnergyBlueprint outty, int context = ItemSlot.Context.BankItem, float scale = 1f)
        {
            output = outty;
            _context = context;
            _scale = scale;
            SlotTexture = ModContent.Request<Texture2D>("AAMod/Assets/Textures/BlueprintItemSlot");
            EmptyTexture = ModContent.Request<Texture2D>("AAMod/Assets/Textures/BlueprintFragment");
            Item = new Item();
            Item.SetDefaults(0);
            Width.Set(TextureAssets.InventoryBack9.Value.Width * scale, 0f);
            Height.Set(TextureAssets.InventoryBack9.Value.Height * scale, 0f);
        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            float oldScale = Main.inventoryScale;
            Main.inventoryScale = _scale;
            Rectangle rectangle = GetDimensions().ToRectangle();
            if (ContainsPoint(Main.MouseScreen) && !PlayerInput.IgnoreMouseInterface)
            {
                Main.LocalPlayer.mouseInterface = true;
                if (FragmentFunc == null || FragmentFunc(Main.mouseItem))
                {
                    ItemSlot.Handle(ref Item, _context);
                }
            }
            if (Item.type != ItemID.None && (!output.output.Item.TryGetGlobalItem(out AAGlobalItemModular _) || (output.output.Item.TryGetGlobalItem(out AAGlobalItemModular _) && output.output.Item.GetGlobalItem<AAGlobalItemModular>().Freedom == -1 && output.output.Item.type != ModContent.ItemType<FreedomStarBusted>())))
            {
                Asset<Texture2D> itemTexture = TextureAssets.Item[Item.type];
                if (ModContent.HasAsset($"AAMod/Content/Items/Materials/Energy/{Item.Name.Replace(" ", "")}_NoAnim"))
                {
                    itemTexture = ModContent.Request<Texture2D>($"AAMod/Content/Items/Materials/Energy/{Item.Name.Replace(" ", "")}_NoAnim");
                }
                Vector2 itemPosition = rectangle.Center() - new Vector2(itemTexture.Width(), itemTexture.Height()) / 2f;
                Rectangle itemRectangle = new((int)itemPosition.X, (int)itemPosition.Y, itemTexture.Width(), itemTexture.Height());
                int offset = (int)(16f * _scale);
                int maxWidth = (int)((SlotTexture.Width() - offset) * _scale);
                int maxHeight = (int)((SlotTexture.Height() - offset) * _scale);
                Rectangle clippedRectangle = new(rectangle.X + offset / 2, rectangle.Y + offset / 2, maxWidth, maxHeight);
                if (itemRectangle.Width > maxWidth)
                {
                    itemRectangle = clippedRectangle;
                }
                if (itemRectangle.Height > maxHeight)
                {
                    itemRectangle = clippedRectangle;
                }
                spriteBatch.Draw(SlotTexture.Value, rectangle.TopLeft(), Color.White);
                spriteBatch.Draw(itemTexture.Value, itemRectangle, Color.White);
                if (ModContent.HasAsset($"AAMod/Content/Items/Materials/Energy/{Item.Name.Replace(" ", "")}_GlowNoAnim"))
                {
                    Asset<Texture2D> glowMask = ModContent.Request<Texture2D>($"AAMod/Content/Items/Materials/Energy/{Item.Name.Replace(" ", "")}_GlowNoAnim");
                    spriteBatch.Draw(glowMask.Value, itemRectangle, Color.White);
                }
            }
            else if (output.output.Item.TryGetGlobalItem(out AAGlobalItemModular _) && (output.output.Item.GetGlobalItem<AAGlobalItemModular>().Freedom != -1 || output.output.Item.type == ModContent.ItemType<FreedomStarBusted>()))
            {
                spriteBatch.Draw(SlotTexture.Value, rectangle.TopLeft(), Color.Gray);
            }
            else
            {
                spriteBatch.Draw(EmptyTexture.Value, rectangle.TopLeft(), Color.White);
            }
            Main.inventoryScale = oldScale;
        }
    }
    class OutputUI : UIElement
    {
        internal Item Item;
        private readonly int _context;
        public Asset<Texture2D> SlotTexture;
        public Asset<Texture2D> EmptyTexture;
        private readonly float _scale;
        internal Func<Item, bool> OutputFunc;
        public OutputUI(int context = ItemSlot.Context.BankItem, float scale = 1f)
        {
            _context = context;
            _scale = scale;
            SlotTexture = ModContent.Request<Texture2D>("AAMod/Assets/Textures/BlueprintItemSlot");
            EmptyTexture = ModContent.Request<Texture2D>("AAMod/Assets/Textures/BlueprintOutput");
            Item = new Item();
            Item.SetDefaults(0);
            Width.Set(TextureAssets.InventoryBack9.Value.Width * scale, 0f);
            Height.Set(TextureAssets.InventoryBack9.Value.Height * scale, 0f);
        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            float oldScale = Main.inventoryScale;
            Main.inventoryScale = _scale;
            Rectangle rectangle = GetDimensions().ToRectangle();
            if (ContainsPoint(Main.MouseScreen) && !PlayerInput.IgnoreMouseInterface)
            {
                Main.LocalPlayer.mouseInterface = true;
                if (OutputFunc == null || OutputFunc(Main.mouseItem))
                {
                    ItemSlot.Handle(ref Item, _context);
                }
            }
            if (Item.type != ItemID.None)
            {
                Asset<Texture2D> itemTexture = TextureAssets.Item[Item.type];
                if (ModContent.HasAsset($"AAMod/Content/Items/Weapons/Energy/{Item.Name.Replace(" ", "")}_NoAnim"))
                {
                    itemTexture = ModContent.Request<Texture2D>($"AAMod/Content/Items/Weapons/Energy/{Item.Name.Replace(" ", "")}_NoAnim");
                }
                Vector2 itemPosition = rectangle.Center() - new Vector2(itemTexture.Width(), itemTexture.Height()) / 2f;
                Rectangle itemRectangle = new((int)itemPosition.X, (int)itemPosition.Y, itemTexture.Width(), itemTexture.Height());
                int offset = (int)(16f * _scale);
                int maxWidth = (int)((SlotTexture.Width() - offset) * _scale);
                int maxHeight = (int)((SlotTexture.Height() - offset) * _scale);
                Rectangle clippedRectangle = new(rectangle.X + offset / 2, rectangle.Y + offset / 2, maxWidth, maxHeight);
                if (itemRectangle.Width > maxWidth)
                {
                    itemRectangle = clippedRectangle;
                }
                if (itemRectangle.Height > maxHeight)
                {
                    itemRectangle = clippedRectangle;
                }
                spriteBatch.Draw(SlotTexture.Value, rectangle.TopLeft(), Color.White);
                spriteBatch.Draw(itemTexture.Value, itemRectangle, Color.White);
                if (Item.GetGlobalItem<AAGlobalItemModular>().Freedom == -1 && ModContent.HasAsset($"AAMod/Content/Items/Weapons/Energy/{Item.Name.Replace(" ", "")}_GlowNoAnim"))
                {
                    Asset<Texture2D> glowMask = ModContent.Request<Texture2D>($"AAMod/Content/Items/Weapons/Energy/{Item.Name.Replace(" ", "")}_GlowNoAnim");
                    switch (Item.GetGlobalItem<AAGlobalItemModular>().OutputShard)
                    {
                        case 1:
                            {
                                Color color = new(12, 240, 21);
                                spriteBatch.Draw(glowMask.Value, itemRectangle, color);
                                break;
                            }
                        case 2:
                            {
                                Color color = new(30, 30, 30);
                                spriteBatch.Draw(glowMask.Value, itemRectangle, color);
                                break;
                            }
                        case 3:
                            {
                                Color color = new(232, 219, 36);
                                spriteBatch.Draw(glowMask.Value, itemRectangle, color);
                                break;
                            }
                        case 4:
                            {
                                Color color = new(195, 13, 13);
                                spriteBatch.Draw(glowMask.Value, itemRectangle, color);
                                break;
                            }
                        case 5:
                            {
                                Color color = new(12, 186, 187);
                                spriteBatch.Draw(glowMask.Value, itemRectangle, color);
                                break;
                            }
                        case 6:
                            {
                                Color color = new(234, 234, 234);
                                spriteBatch.Draw(glowMask.Value, itemRectangle, color);
                                break;
                            }
                        case 7:
                            {
                                Color color = new(195, 73, 13);
                                spriteBatch.Draw(glowMask.Value, itemRectangle, color);
                                break;
                            }
                        case 8:
                            {
                                Color color = new(226, 18, 173);
                                spriteBatch.Draw(glowMask.Value, itemRectangle, color);
                                break;
                            }
                        default:
                            {
                                Color color = new(23, 13, 182);
                                spriteBatch.Draw(glowMask.Value, itemRectangle, color);
                                break;
                            }
                    }
                }
                if (Item.GetGlobalItem<AAGlobalItemModular>().Freedom != -1)
                {
                    switch (Item.GetGlobalItem<AAGlobalItemModular>().Freedom)
                    {
                        case 1:
                            {
                                Color color = new(12, 240, 21);
                                Asset<Texture2D> glowMask = ModContent.Request<Texture2D>($"AAMod/Content/Items/Weapons/FreedomStars/FreedomStar_GlowNoAnim");
                                spriteBatch.Draw(glowMask.Value, itemRectangle, color);
                                break;
                            }
                        case 2:
                            {
                                Color color = new(30, 30, 30);
                                Asset<Texture2D> glowMask = ModContent.Request<Texture2D>($"AAMod/Content/Items/Weapons/FreedomStars/FreedomStar_GlowNoAnim");
                                spriteBatch.Draw(glowMask.Value, itemRectangle, color);
                                break;
                            }
                        case 3:
                            {
                                Color color = new(232, 219, 36);
                                Asset<Texture2D> glowMask = ModContent.Request<Texture2D>($"AAMod/Content/Items/Weapons/FreedomStars/FreedomStar_GlowNoAnim");
                                spriteBatch.Draw(glowMask.Value, itemRectangle, color);
                                break;
                            }
                        case 4:
                            {
                                Color color = new(195, 13, 13);
                                Asset<Texture2D> glowMask = ModContent.Request<Texture2D>($"AAMod/Content/Items/Weapons/FreedomStars/FreedomStar_GlowNoAnim");
                                spriteBatch.Draw(glowMask.Value, itemRectangle, color);
                                break;
                            }
                        case 5:
                            {
                                Color color = new(12, 186, 187);
                                Asset<Texture2D> glowMask = ModContent.Request<Texture2D>($"AAMod/Content/Items/Weapons/FreedomStars/FreedomStar_GlowNoAnim");
                                spriteBatch.Draw(glowMask.Value, itemRectangle, color);
                                break;
                            }
                        case 6:
                            {
                                Color color = new(234, 234, 234);
                                Asset<Texture2D> glowMask = ModContent.Request<Texture2D>($"AAMod/Content/Items/Weapons/FreedomStars/FreedomStar_GlowNoAnim");
                                spriteBatch.Draw(glowMask.Value, itemRectangle, color);
                                break;
                            }
                        case 7:
                            {
                                Color color = new(195, 73, 13);
                                Asset<Texture2D> glowMask = ModContent.Request<Texture2D>($"AAMod/Content/Items/Weapons/FreedomStars/FreedomStar_GlowNoAnim");
                                spriteBatch.Draw(glowMask.Value, itemRectangle, color);
                                break;
                            }
                        case 8:
                            {
                                Color color = new(226, 18, 173);
                                Asset<Texture2D> glowMask = ModContent.Request<Texture2D>($"AAMod/Content/Items/Weapons/FreedomStars/FreedomStar_GlowNoAnim");
                                spriteBatch.Draw(glowMask.Value, itemRectangle, color);
                                break;
                            }
                        default:
                            {
                                Color color = new(23, 13, 182);
                                Asset<Texture2D> glowMask = ModContent.Request<Texture2D>($"AAMod/Content/Items/Weapons/FreedomStars/FreedomStar_GlowNoAnim");
                                spriteBatch.Draw(glowMask.Value, itemRectangle, color);
                                break;
                            }
                    }
                }
            }
            else
            {
                spriteBatch.Draw(EmptyTexture.Value, rectangle.TopLeft(), Color.White);
            }
            Main.inventoryScale = oldScale;
        }
        #endregion
    }
}