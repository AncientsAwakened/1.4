using AAMod.Common.Globals;
using AAMod.Common.Systems.UI;
using AAMod.Content.Items.Weapons.FreedomStars;
using AAMod.Content.Tiles.Energy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
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

namespace AAMod.Common.Systems
{
    class EnergyDisplay : UIState
    {
        private ShowcaseUI showcase;
        public Display modTile;
        public EnergyDisplay(ModTile tile)
        {
            modTile = (Display)tile;
        }
        public override void OnInitialize()
        {
            showcase = new ShowcaseUI(ItemSlot.Context.BankItem, 1f)
            {
                Left = { Pixels = 0 },
                Top = { Pixels = 0 },
                Width = { Pixels = 32 },
                Height = { Pixels = 32 },
                ShowcaseFunc = item => item.IsAir || !item.IsAir && item.GetGlobalItem<AAGlobalItemModular>().OutputWeapon && !item.GetGlobalItem<AAGlobalItemModular>().CheatedIn && item.type != ModContent.ItemType<FreedomStarBusted>()
            };
            Append(showcase);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (!Main.LocalPlayer.IsTileTypeInInteractionRange(ModContent.TileType<Display>()))
            {
                UISystem system = ModContent.GetInstance<UISystem>();
                system.DeactivateUI<EnergyDisplay>();
            }
        }
        public override void OnActivate()
        {
            base.OnActivate();
            showcase.Item = modTile.item;
        }
        public override void OnDeactivate()
        {
            base.OnActivate();
            modTile.item = showcase.Item;
        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            Main.hidePlayerCraftingMenu = true;
            modTile.item = showcase.Item;
            showcase.Left.Pixels = (modTile.TileX * 16) - (int)Main.screenPosition.X + 8;
            showcase.Top.Pixels = (modTile.TileY * 16) - (int)Main.screenPosition.Y - 38;
        }
    }
    class ShowcaseUI : UIElement
    {
        internal Item Item;
        private readonly int _context;
        public Asset<Texture2D> FullTexture;
        public Asset<Texture2D> EmptyTexture;
        private readonly float _scale;
        internal Func<Item, bool> ShowcaseFunc;
        public ShowcaseUI(int context = ItemSlot.Context.BankItem, float scale = 1f)
        {
            _context = context;
            _scale = scale;
            FullTexture = ModContent.Request<Texture2D>("AAMod/Assets/Textures/GlassFull");
            EmptyTexture = ModContent.Request<Texture2D>("AAMod/Assets/Textures/GlassEmpty");
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
            if (Item == null)
            {
                Item = new Item();
                Item.SetDefaults(0);
            }
            if (ContainsPoint(Main.MouseScreen) && !PlayerInput.IgnoreMouseInterface)
            {
                Main.LocalPlayer.mouseInterface = true;
                if (ShowcaseFunc == null || ShowcaseFunc(Main.mouseItem))
                {
                    ItemSlot.Handle(ref Item, _context);
                }
            }
            Rectangle rectangle = GetDimensions().ToRectangle();
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
                int maxWidth = (int)((FullTexture.Width() - offset) * _scale);
                int maxHeight = (int)((FullTexture.Height() - offset) * _scale);
                Rectangle clippedRectangle = new(rectangle.X + offset / 2, rectangle.Y + offset / 2, maxWidth, maxHeight);
                if (itemRectangle.Width > maxWidth)
                {
                    itemRectangle = clippedRectangle;
                }
                if (itemRectangle.Height > maxHeight)
                {
                    itemRectangle = clippedRectangle;
                }
                spriteBatch.Draw(EmptyTexture.Value, rectangle.TopLeft(), Color.White);
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
                spriteBatch.Draw(FullTexture.Value, rectangle.TopLeft(), Color.White);
            }
            else
            {
                spriteBatch.Draw(EmptyTexture.Value, rectangle.TopLeft(), Color.White);
            }
            Main.inventoryScale = oldScale;
        }
    }
}
